namespace TruthOrDrink.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
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
            Vibration.Default.Vibrate(500);
            await Shell.Current.GoToAsync("//main");
        }

        Vibration.Default.Vibrate(1000);
        return;
    }
}