namespace TruthOrDrink.Views;

public partial class NewPage1 : ContentPage
{
	public NewPage1()
	{
		InitializeComponent();
	}

    private async void HomeToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//main");
    }

    private async void LogoutToolbarItem_Clicked(object sender, EventArgs e)
    {
        await SecureStorage.SetAsync("IsAuthenticated", "false");
        await Shell.Current.GoToAsync("//login");
    }
}