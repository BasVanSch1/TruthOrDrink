using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDrink.Models
{
    internal class Game
    {
        public List<GameQuestion> Questions { get; set; } = [];
        public List<Player> Players { get; set; } = [];
        public Dictionary<QuestionCategory, int> QuestionCategories { get; set; } = [];
        public List<QuestionType> QuestionTypes { get; set; } = [];
        public int QuestionLevel { get; set; } = 1;

        public static Game Instance { get; } = new Game();

        public void StartGame() { }
        public void StopGame() { }
        public void GiveNextQuestion() { }
        public void AddPlayer(Player player) { }
        public void RemovePlayer(Player player) { }
        public void SetQuestions(List<Question> questions) { }

    }
}
