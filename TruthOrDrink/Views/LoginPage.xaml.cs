namespace TruthOrDrink.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
	{
		InitializeComponent();
        Loaded += OnPageLoaded;
	}

    private void Username_TextChanged(object sender, EventArgs e)
    {
        UsernameError.IsVisible = false;
    }

    private void Password_TextChanged(object sender, EventArgs e)
    {
        PasswordError.IsVisible = false;
    }

    private async void Login(object sender, EventArgs e)
    {
        bool inputValid = true;
        UsernameError.IsVisible = false;
        PasswordError.IsVisible = false;

        if (String.IsNullOrWhiteSpace(UsernameEntry.Text))
        {
            UsernameError.Text = "Username is required.";
            UsernameError.IsVisible = true;
            inputValid = false;
        }

        if (String.IsNullOrWhiteSpace(PasswordEntry.Text))
        {
            PasswordError.Text = "Password is required.";
            PasswordError.IsVisible = true;
            inputValid = false;
        }
        else if (PasswordEntry.Text != "123")
        {
            PasswordError.Text = "Password is incorrect.";
            PasswordError.IsVisible = true;
            inputValid = false;
        }

        if (inputValid)
        {
            await SecureStorage.SetAsync("IsAuthenticated", "true");
            Vibration.Vibrate(500);

            NavigateToMainPage();

            return;
        }

        Vibration.Vibrate(1000);
        return;
    }

    private async void OnPageLoaded(object? sender, EventArgs e)
    {
        string? authenticated = await SecureStorage.GetAsync("IsAuthenticated");

        if (authenticated != null && String.Equals(authenticated, "true", StringComparison.OrdinalIgnoreCase))
        {
            NavigateToMainPage();
        }
    }

    private async void NavigateToMainPage()
    {
        await Navigation.PushAsync(new MainTabbedPage()); // open page with all tabs in it (where the default page is the Homepage)
        Navigation.RemovePage(this); // remove the LoginPage from the navigation stack (so that it is no longer the root page and the user cannot go back to it.)
    }
}