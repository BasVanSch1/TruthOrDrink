using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDrink.Models
{
    internal class GameQuestion : Question
    {
        public Dictionary<Player, AnswerType> AnsweredBy { get; set; } = [];
    }

    public enum AnswerType
    {
        None,
        Truth,
        Drink
    }
}
