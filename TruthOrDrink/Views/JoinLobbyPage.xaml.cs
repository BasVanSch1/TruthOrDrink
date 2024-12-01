namespace TruthOrDrink.Views;

public partial class JoinLobbyPage : ContentPage
{
	public JoinLobbyPage()
	{
		InitializeComponent();
	}

    private async void JoinBySessionCodeBtn_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync("playerlobbypage");
    }
}