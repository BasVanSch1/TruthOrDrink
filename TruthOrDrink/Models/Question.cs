using SQLite;

namespace TruthOrDrink.Models
{
    public class Question
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Description { get; set; }
        [NotNull]
        public QuestionType Type { get; set; }
        [NotNull]
        public QuestionCategory Category { get; set; }
        [NotNull]
        public int Level { get; set; } = 1;
        Profile? CreatedBy { get; set; } = null;
    }

    public enum QuestionType
    {
        Question,
        Assignment,
        Photo
    }

    public enum QuestionCategory
    {
        Normal,
        Spicy,
        Love,
        HappyHour
    }

}
