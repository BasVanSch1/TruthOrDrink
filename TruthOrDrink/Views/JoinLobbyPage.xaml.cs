namespace TruthOrDrink.Views;

public partial class JoinLobbyPage : ContentPage
{
	public JoinLobbyPage()
	{
		InitializeComponent();
        DisplayAlert("NOTE", "De functionaliteit om een lobby te joinen is niet werkend op het moment. Deze functie wordt later toegevoegd.\nOp het moment is het alleen mogelijk om een lobby te hosten.", "OK");

    }

    private async void JoinBySessionCodeBtn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new PlayerLobbyPage());
    }
}