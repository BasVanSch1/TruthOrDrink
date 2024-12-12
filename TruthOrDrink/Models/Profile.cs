using SQLite;

namespace TruthOrDrink.Models
{
    public class Profile
    {
        [PrimaryKey]
        public string Username { get; set; }
        [NotNull]
        public string PasswordHash { get; set; }
        [NotNull]
        public string Email { get; set; }
        [NotNull]
        public string DisplayName { get; set; }
        List<Question>? PersonalQuestions { get; set; } = null;
        [NotNull]
        public int Truths { get; set; } = 0;
        [NotNull]
        public int Drink { get; set; } = 0;
        [NotNull]
        public int Games { get; set; } = 0;
    }
}
