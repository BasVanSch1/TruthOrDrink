using TruthOrDrink.Data;
using TruthOrDrink.Models;
using TruthOrDrink.Views;

namespace TruthOrDrink
{
    public partial class App : Application
    {
        private readonly TruthOrDrinkDatabase _database;
        private readonly DataGenerator _dataGenerator;
        public App()
        {
            _database = IPlatformApplication.Current.Services.GetRequiredService<TruthOrDrinkDatabase>();
            _dataGenerator = IPlatformApplication.Current.Services.GetRequiredService<DataGenerator>();
            
            InitializeComponent();
            GenerateFakeData(); // Generate fake data for testing

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new LoginPage()));
        }

        private async void GenerateFakeData()
        {
            int lastResult;
            for (int i = 0; i < 400; i++)
            {
                Question question = _dataGenerator.GenerateQuestion();
                lastResult = await _database.UpdateQuestionAsync(question);
            }

            return;
        }
    }
}