using TruthOrDrink.Data;
using TruthOrDrink.Models;

namespace TruthOrDrink.Views;

public partial class RegisterPage : ContentPage
{
    private readonly TruthOrDrinkDatabase _database;
    public RegisterPage()
    {
        InitializeComponent();
        _database = IPlatformApplication.Current.Services.GetRequiredService<TruthOrDrinkDatabase>();
    }

    private async void RegisterBtn_Clicked(object sender, EventArgs e)
    {
        var result = await _database.RegisterUser(UsernameEntry.Text, PasswordEntry.Text, EmailEntry.Text);

        if (result >= 1)
        {
            await DisplayAlert("Success", "User registered successfully.", "OK");
            await Navigation.PushAsync(new LoginPage());
            return;
        }

        switch (result)
        {
            case -1:
                RegisterError.Text = "An unknown error has occured during registering.";
                RegisterError.IsVisible = true;
                break;
            case -2:
                UsernameError.Text = "Username is already taken.";
                UsernameError.IsVisible = true;
                break;
            case -3:
                EmailError.Text = "Email is already taken.";
                EmailError.IsVisible = true;
                break;
        }
    }

    private void UsernameEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        UsernameError.IsVisible = false;
        RegisterError.IsVisible = false;
    }

    private void EmailEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        EmailError.IsVisible = false;
        RegisterError.IsVisible = false;
    }

    private void PasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        PasswordError.IsVisible = false;
        RegisterError.IsVisible = false;
    }
}