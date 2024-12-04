using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruthOrDrink.Models;

namespace TruthOrDrink.Data
{
    public class TruthOrDrinkDatabase
    {
        SQLiteAsyncConnection Database;

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await Database.CreateTableAsync<Question>();
            await Database.CreateTableAsync<Profile>();
            
        }

        public async Task<List<Question>> GetQuestionsAsync()
        {
            await Init();

            return await Database.Table<Question>().ToListAsync();
        }
        public async Task<int> UpdateQuestionAsync(Question question)
        {
            await Init();

            if (question.Id == 0) // If question does not have an ID then insert a new question in the database.
            {
                return await Database.InsertAsync(question);
            }

            return await Database.UpdateAsync(question);
        }

        public async Task<Question>? GetQuestionAsync(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            await Init();

            // return the Question, otherwise return null on error.
            try
            {
                return await Database.GetAsync<Question>(id);
            } catch (Exception)
            {
                return null;
            }
        }
    }
}
