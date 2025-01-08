using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDrink.Models
{
    internal class Player
    {
        public string DisplayName { get; set; } = "Local Player";
        public int Truths { get; set; } = 0;
        public int Drinks { get; set; } = 0;
        public string? ProfileUsername { get; set; }
        public Profile? Profile { get; set; }

        public void AnswerQuestion(AnswerType answer)
        {
            if (answer == AnswerType.Truth)
            {
                // TODO: add game logic
                Truths++;
            }
            else if (answer == AnswerType.Drink)
            {
                // TODO: add game logic
                Drinks++;
            }
        }
    }
}
