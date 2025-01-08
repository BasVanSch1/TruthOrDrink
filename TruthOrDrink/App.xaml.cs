using TruthOrDrink.Data;
using TruthOrDrink.Views;

namespace TruthOrDrink
{
    public partial class App : Application
    {
        private readonly DataGenerator _dataGenerator;
        public App()
        {
            _dataGenerator = IPlatformApplication.Current.Services.GetRequiredService<DataGenerator>();
            
            InitializeComponent();
            GenerateFakeData(); // Generate fake data for testing

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new LoginPage()));
        }

        private void GenerateFakeData()
        {
            for (int i = 0; i < 15; i++)
            {
                _dataGenerator.GenerateQuestion();
                _dataGenerator.GenerateProfile();
            }

            return;
        }
    }
}