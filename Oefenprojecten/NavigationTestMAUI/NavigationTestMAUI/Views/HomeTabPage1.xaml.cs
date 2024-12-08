namespace NavigationTestMAUI.Views;

public partial class HomeTabPage1 : ContentPage
{
	public HomeTabPage1()
	{
		InitializeComponent();
	}

    private async void NavigateToPage2Btn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new HomeTabPage2());
    }
}