using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthOrDrink.Models
{
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Description { get; set; }
        [NotNull]
        public QuestionType QuestionType { get; set; }
    }

    public enum QuestionType
    {
        Question,
        Assignment,
        Photo
    }
}
