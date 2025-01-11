namespace TruthOrDrink.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
	{
        InitializeComponent(); 
    }

    private async void LogoutBtn_Clicked(object sender, EventArgs e)
    {
		SecureStorage.Remove("IsAuthenticated");
        SecureStorage.Remove("Username");
        Navigation.InsertPageBefore(new LoginPage(), Navigation.NavigationStack[0]); // Insert LoginPage before the root, thus making it the new root.
		await Navigation.PopToRootAsync(); // Go to the root page, which is now the LoginPage. This also clears the navigation stack up until the root page.
    }
}