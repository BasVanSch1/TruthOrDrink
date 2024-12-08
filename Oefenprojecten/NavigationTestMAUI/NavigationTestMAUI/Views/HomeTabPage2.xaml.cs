namespace NavigationTestMAUI.Views;

public partial class HomeTabPage2 : ContentPage
{
	public HomeTabPage2()
	{
		InitializeComponent();
	}

    private async void NavigateToPage3Btn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new HomeTabPage3());
    }
}