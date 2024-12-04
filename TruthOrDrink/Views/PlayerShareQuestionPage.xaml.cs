using QRCoder;
using TruthOrDrink.Helpers;

namespace TruthOrDrink.Views;

public partial class PlayerShareQuestionPage : ContentPage
{
	public PlayerShareQuestionPage()
	{
		InitializeComponent();
        GenerateQRCode();
	}

    private void GenerateQRCode()
    {
        string questions = "all personal questions but then formatted in a way that the application can read them.";

        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(questions, QRCodeGenerator.ECCLevel.L);
        PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
        byte[] qrCodeBytes = qrCode.GetGraphic(20);

        QRImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
    }
}