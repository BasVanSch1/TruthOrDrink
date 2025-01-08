using SQLite;

namespace TruthOrDrink.Models
{
    [Table("Question")]
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Text { get; set; }
        [NotNull]
        public QuestionType Type { get; set; } = QuestionType.Question;
        [NotNull]
        public QuestionCategory Category { get; set; } = QuestionCategory.Normal;
        [NotNull]
        public int Level { get; set; } = 1;
        public Profile? CreatedBy { get; set; } = null;
    }

    public enum QuestionType
    {
        Question,
        Assignment
    }

    public enum QuestionCategory
    {
        Normal,
        Spicy,
        Love,
        HappyHour
    }

}
