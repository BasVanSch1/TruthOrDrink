using QRCoder;
using System.Collections.ObjectModel;
using TruthOrDrink.Data;
using TruthOrDrink.Helpers;
using TruthOrDrink.Models;

namespace TruthOrDrink.Views;

public partial class HostLobbyPage : ContentPage
{
    private Game Game;
    private readonly TruthOrDrinkDatabase _database;
    private ObservableCollection<Player> PlayerList { get; set; }

    public HostLobbyPage()
	{
        _database = IPlatformApplication.Current.Services.GetRequiredService<TruthOrDrinkDatabase>();

        Game = Game.Instance;
        Game.Reset(); // Whenever a new lobby gets created, reset the game        

        InitializeComponent();
        GenerateSessionCode();

        PlayerList = new ObservableCollection<Player>();
        PlayerListCollectionView.ItemsSource = PlayerList;

        AddHost();
    }

    private void GenerateSessionCode()
    {
        int sessionCode = RandomHelper.GetRandomNumber(1, 9999);
        string formattedSessionCode = sessionCode.ToString("D4");

        QRCodeGenerator qrGenerator = new QRCodeGenerator();

        // create QR data
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(formattedSessionCode, QRCodeGenerator.ECCLevel.L);
        // Create the QRcode (PNG bytes)
        PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
        // Get the byte data (bytes of the image)
        byte[] qrCodeBytes = qrCode.GetGraphic(20);

        QRImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
        SessionCode.Text = formattedSessionCode;
        Game = Game.Instance;

    }

    private async void GameSettingsBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GameSettingsPage());
    }

    private async void StartGameBtn_Clicked(object sender, EventArgs e)
    {
        if (Game.Players.Count < 2)
        {
            await DisplayAlert("Error", "Minimum of 2 players needed to play this game.", "OK");
            return;
        }

        if (Game.Questions.Count <= 0)
        {
            await DisplayAlert("Error", "No questions available, please add questions in the settings.", "OK");
            return;
        }

        await Navigation.PushAsync(new GamePage());
    }

    private async void GameQuestionsBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GameQuestionsPage());
    }

    private async void AddHost()
    {
        // Add current logged in user as player
        string? username = await SecureStorage.GetAsync("Username");
        if (username == null)
        {
            await DisplayAlert("Error", "Failed to retrieve username, try logging back in.", "OK"); // Display error message

            // 'logout' and go to login page
            SecureStorage.Remove("IsAuthenticated");
            SecureStorage.Remove("Username");
            Navigation.InsertPageBefore(new LoginPage(), Navigation.NavigationStack[0]);
            await Navigation.PopToRootAsync();

            return;
        }

        Profile? profile = await _database.GetAsync<Profile>(username); // Get the profile from the database
        if (profile == null)
        {
            await DisplayAlert("Error", "Failed to find loggedin user in database", "OK"); // Display error message
            return;
        }

        Player host = new Player() // Convert the profile to a player
        {
            DisplayName = profile.DisplayName,
            ProfileUsername = profile.Username
        };

        Game.Players.Add(host); // Add the host as a player
        PlayerList.Add(host); // Add the host to the PlayerList collection
    }

    private void AddLocalPlayerBtn_Clicked(object sender, EventArgs e)
    {
        if (Game.Players.Count >= 4)
        {
            DisplayAlert("Error", "Maximum amount of players reached.", "OK");
            return;
        }

        Player newPlayer = new Player()
        {
            DisplayName = "Local Player " + (Game.Players.Count) // Use the count of players since the host is player 1. otherwise the first local player would be 'Local Player 2'.
        };

        Game.Players.Add(newPlayer);
        PlayerList.Add(newPlayer);
    }

    private void RemoveLocalPlayerBtn_Clicked(object sender, EventArgs e)
    {
        if (Game.Players.Count <= 1)
        {
            DisplayAlert("Error", "Minimum amount of players reached.", "OK");
            return;
        }

        Game.Players.RemoveAt(Game.Players.Count - 1); // Remove the last player from the list
        PlayerList.RemoveAt(PlayerList.Count - 1);
    }
}