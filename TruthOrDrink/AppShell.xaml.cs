using TruthOrDrink.Views;

namespace TruthOrDrink
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("loginpage", typeof(LoginPage));
            Routing.RegisterRoute("mainpage", typeof(MainPage));
            Routing.RegisterRoute("createquestionpage", typeof(CreateQuestionPage));
            Routing.RegisterRoute("forgotpasswordpage", typeof(ForgotPasswordPage));
            Routing.RegisterRoute("gamepage", typeof(GamePage));
            Routing.RegisterRoute("gamequestionspage", typeof(GameQuestionsPage));
            Routing.RegisterRoute("gamesettingspage", typeof(GameSettingsPage));
            Routing.RegisterRoute("hostaddquestionconfirmpage", typeof(HostAddQuestionConfirmPage));
            Routing.RegisterRoute("hostlobbypage", typeof(HostLobbyPage));
            Routing.RegisterRoute("hostscanquestionpage", typeof(HostScanQuestionPage));
            Routing.RegisterRoute("howtoplaypage", typeof(HowToPlayPage));
            Routing.RegisterRoute("joinlobbypage", typeof(JoinLobbyPage));
            Routing.RegisterRoute("loadingpage", typeof(LoadingPage));
            Routing.RegisterRoute("personalquestionspage", typeof(PersonalQuestionsPage));
            Routing.RegisterRoute("playerlobbypage", typeof(PlayerLobbyPage));
            Routing.RegisterRoute("playersharequestionpage", typeof(PlayerShareQuestionPage));
            Routing.RegisterRoute("profileeditpage", typeof(ProfileEditPage));
            Routing.RegisterRoute("profilepage", typeof(ProfilePage));
            Routing.RegisterRoute("newpage1", typeof(NewPage1)); // testing page
        }
    }
}
