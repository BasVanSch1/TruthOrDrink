using SQLite;

namespace TruthOrDrink.Models
{
    [Table("Profile")]
    public class Profile
    {
        [PrimaryKey]
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        [Unique]
        public string DisplayName { get; set; }
        List<Question>? PersonalQuestions { get; set; }
        public int Truths { get; set; } = 0;
        public int Drinks { get; set; } = 0;
        public int Games { get; set; } = 0;
    }
}
