using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDrink.Models
{
    internal class Game
    {
        public List<GameQuestion> Questions { get; set; } = [];
        public List<Player> Players { get; set; } = [];

        public Dictionary<QuestionCategory, int> QuestionCategories = [];

        public Dictionary<QuestionType, bool> QuestionTypes { get; set; } = [];

        public int QuestionLevel { get; set; } = 1;

        public static Game Instance { get { return instance; } } // public singleton instance, the instance can be accessed with this static property
        private static readonly Game instance = new Game(); // private property that stores the instance, is only initialized once.
        public Game()
        {
            PopulateCategoriesWithDefaults();
            PopulateTypesWithDefaults();
        }

        public void StartGame() { }
        public void StopGame() { }
        public void GiveNextQuestion() { }
        public void AddPlayer(Player player) { }
        public void RemovePlayer(Player player) { }
        public void SetQuestions(List<Question> questions) { }
        public void Reset()
        {
            Questions = [];
            Players = [];
            QuestionCategories = [];
            QuestionTypes = [];
            QuestionLevel = 1;

            PopulateCategoriesWithDefaults();
            PopulateTypesWithDefaults();
        }

        private void PopulateCategoriesWithDefaults()
        {
            foreach (QuestionCategory category in Enum.GetValues(typeof(QuestionCategory))) // populate the dictionary with all the categories and default values.
            {
                QuestionCategories.Add(category, 0);
            }
        }

        private void PopulateTypesWithDefaults()
        {
            foreach (QuestionType type in Enum.GetValues(typeof(QuestionType)))
            {
                QuestionTypes.Add(type, false);
            }
        }
    }
}
