using SQLite;

namespace TruthOrDrink.Models
{
    internal class Profile
    {
        [PrimaryKey]
        string Username { get; set; }
        [NotNull]
        string PasswordHash { get; set; }
        [NotNull]
        string DisplayName { get; set; }
        List<Question>? PersonalQuestions { get; set; }
        [NotNull]
        int Truths { get; set; } = 0;
        [NotNull]
        int Drink { get; set; } = 0;
        [NotNull]
        int Games { get; set; } = 0;
    }
}
