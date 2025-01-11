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

        // Methods
        public GameQuestion? GetNextQuestion() 
        {
            if (Questions.Count == 0) // extra check, if there are no questions in the game, which shouldn't be the case, return null.
            {
                return null;
            }

            foreach (var question in Questions) // iterate through all Questions in the game and get the next unanswered question.
            {
                if (question.AnsweredBy.Count == 0) // if the question has not been answered yet
                {
                    return question;
                }
            }

            return null; // if all questions have been answered, return null.
        }

        public List<Player>? ChoosePlayers()
        {
            if (Players.Count < 2)
            {
                return null;
            }

            var random = new Random();
            var players = new List<Player>();

            while (players.Count < 2)
            {
                var player = Players[random.Next(Players.Count)]; // get a random player from the playerlist
                if (!players.Contains(player)) // check if the player is not already selected
                {
                    players.Add(player);
                }
            }
            return players;
        }

        public void AddQuestions(List<Question> questions)
        {
            foreach (var question in questions)
            {
                var gameQuestion = new GameQuestion()
                {
                    Id = question.Id,
                    Text = question.Text,
                    Type = question.Type,
                    Category = question.Category,
                    Level = question.Level,
                    CreatedByUsername = question.CreatedByUsername,
                    AnsweredBy = []
                };

                Questions.Add(gameQuestion);
            }
        }
        public void Reset(bool keepSettings = false)
        {
            if (keepSettings == false)
            {
                Players = [];
                QuestionCategories = [];
                QuestionTypes = [];
                QuestionLevel = 1;

                PopulateCategoriesWithDefaults();
                PopulateTypesWithDefaults();
            }

            Questions = [];
        }

        private void PopulateCategoriesWithDefaults()
        {
            foreach (QuestionCategory category in Enum.GetValues(typeof(QuestionCategory))) // populate the dictionary with all the categories and default values.
            {
                QuestionCategories.Add(category, 1);
            }
        }

        private void PopulateTypesWithDefaults()
        {
            foreach (QuestionType type in Enum.GetValues<QuestionType>())
            {
                QuestionTypes.Add(type, true);
            }
        }
    }
}
