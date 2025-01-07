using QRCoder;
using TruthOrDrink.Helpers;
using TruthOrDrink.Logic;

namespace TruthOrDrink.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        GenerateQuote();
	}

    private async void HostGameBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HostLobbyPage());
    }

    private async void JoinGameBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new JoinLobbyPage());
    }

    private async void PersonalQuestionsBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PersonalQuestionsPage());
    }

    private async void HowToPlayBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HowToPlayPage());
    }

    private async void GenerateQuote()
    {
        FactLabel.Text = await FactLogic.GetRandomFact();
    }
}