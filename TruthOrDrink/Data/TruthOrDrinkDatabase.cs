using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruthOrDrink.Helpers;
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
                var result = await Database.InsertAsync(question);
                await Database.CloseAsync();
                return result;

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

        public async Task<T?> GetAsync<T>(int id) where T : new()
        {
            if (id <= 0)
            {
                return default; // returnt null waar het kan en anders 0;
            }

            await Init();
            
            try
            {
                return await Database.GetAsync<T>(id);
            } catch (Exception)
            {
                return default;
            }
            
        }

        public async Task<T?> GetAsync<T>(string id) where T : new()
        {
            if (String.IsNullOrWhiteSpace(id))
            {
                return default;
            }

            await Init();

            try
            {
                return await Database.GetAsync<T>(id);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public async Task<bool> VerifyPassword(string username, string password)
        {
            Profile user;

            try
            {
                // get the user from the database
                user = await Database.GetAsync<Profile>(username);
            } catch (Exception)
            {
                return false;
            }
            // Verify the password hash
            return PasswordHasher.VerifyPassword(password, user.PasswordHash);
        }

        public async Task<int> RegisterUser(string username, string password, string email)
        {
            await Init();

            var existingProfile = await Database.Table<Profile>().Where(p => p.Username == username).FirstOrDefaultAsync();

            if (existingProfile is not null)
            {
                return -2; // User already exists
            }

            var hashedPassword = PasswordHasher.HashPassword(password);

            var profile = new Profile()
            {
                Username = username,
                PasswordHash = hashedPassword,
                Email = email,
                DisplayName = username
            };

            try
            {
                return await Database.InsertAsync(profile);
            }
            catch (Exception ex)
            {
                return -1; // Unkown error (failed to insert user into database)
            }
        }
    }
}
