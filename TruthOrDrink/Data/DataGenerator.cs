using Bogus;
using TruthOrDrink.Models;

namespace TruthOrDrink.Data
{
    class DataGenerator
    {
        Faker<Question> QuestionFaker;
        Faker<Profile> ProfileFaker;

        public DataGenerator()
        {
            QuestionFaker = new Faker<Question>()
                .RuleFor(q => q.Text, f => f.Lorem.Sentence())
                .RuleFor(q => q.Category, f => f.PickRandom<QuestionCategory>())
                .RuleFor(q => q.Type, f => f.PickRandom<QuestionType>())
                .RuleFor(q => q.Level, f => f.Random.Number(1, 5));

            ProfileFaker = new Faker<Profile>()
                .RuleFor(p => p.Username, f => f.Internet.UserName())
                .RuleFor(p => p.DisplayName, f => f.Name.FullName())
                .RuleFor(p => p.Email, f => f.Internet.Email())
                .RuleFor(p => p.Truths, f => f.Random.Number(0, 100))
                .RuleFor(p => p.Drinks, f => f.Random.Number(0, 100))
                .RuleFor(p => p.Games, f => f.Random.Number(5, 25));
        }

        public Question GenerateQuestion()
        {
            return QuestionFaker.Generate();
        }

        public Profile GenerateProfile()
        {
            return ProfileFaker.Generate();
        }
    }
}
