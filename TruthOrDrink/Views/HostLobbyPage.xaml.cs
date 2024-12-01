using QRCoder;
using TruthOrDrink.Helpers;

namespace TruthOrDrink.Views;

public partial class HostLobbyPage : ContentPage
{
	public HostLobbyPage()
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

    private void GameSettingsBtn_Clicked(object sender, EventArgs e)
    {

    }

    private void StartGameBtn_Clicked(object sender, EventArgs e)
    {

    }

    private void GameQuestionsBtn_Clicked(object sender, EventArgs e)
    {

    }
}