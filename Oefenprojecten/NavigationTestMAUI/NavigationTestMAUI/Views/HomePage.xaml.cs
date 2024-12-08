namespace NavigationTestMAUI.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void NavigateToPage1Btn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomeTabPage1());
    }
}