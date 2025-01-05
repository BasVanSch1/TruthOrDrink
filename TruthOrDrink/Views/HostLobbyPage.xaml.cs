using QRCoder;
using TruthOrDrink.Helpers;
using TruthOrDrink.Models;

namespace TruthOrDrink.Views;

public partial class HostLobbyPage : ContentPage
{
    private Game Game;
	public HostLobbyPage()
	{
        Game = Game.Instance;
        InitializeComponent();
        GenerateSessionCode();
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
        await Navigation.PushAsync(new GamePage());
    }

    private async void GameQuestionsBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GameQuestionsPage());
    }
}