using QRCoder;
using TruthOrDrink.Helpers;

namespace TruthOrDrink.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void HostGameBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HostLobbyPage());
    }

    private async void JoinGameBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("joinlobbypage");
    }

    private async void PersonalQuestionsBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PersonalQuestionsPage());
    }

    private async void HowToPlayBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HowToPlayPage());
    }
}