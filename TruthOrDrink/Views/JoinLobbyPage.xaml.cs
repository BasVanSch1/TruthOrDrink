namespace TruthOrDrink.Views;

public partial class JoinLobbyPage : ContentPage
{
	public JoinLobbyPage()
	{
		InitializeComponent();
	}

    private async void JoinBySessionCodeBtn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new PlayerLobbyPage());
    }
}