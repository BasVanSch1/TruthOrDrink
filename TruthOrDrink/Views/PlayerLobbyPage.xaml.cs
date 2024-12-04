using QRCoder;
using TruthOrDrink.Helpers;

namespace TruthOrDrink.Views;

public partial class PlayerLobbyPage : ContentPage
{
	public PlayerLobbyPage()
	{
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
    }

    private void ReadyBtn_Clicked(object sender, EventArgs e)
    {
        CurrentPlayer.TextColor = new Color(0,128,0);
    }

    private async void ShareQuestionsBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PlayerShareQuestionPage());
    }

    private async void PersonalQuestionsBtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PersonalQuestionsPage());
    }
}