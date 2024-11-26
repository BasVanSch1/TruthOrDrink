using TruthOrDrink.Views;

namespace TruthOrDrink
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("mainpage", typeof(MainPage));
            Routing.RegisterRoute("newpage1", typeof(NewPage1));
        }
    }
}
