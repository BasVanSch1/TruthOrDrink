using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace TruthOrDrink.Views;

public partial class JoinLobbyPage : ContentPage
{
    public JoinLobbyPage()
	{
        InitializeComponent();
        DisplayAlert("NOTE", "Het joinen van een lobby is nog niet af. Ook is het scannen van een QR-code heel langzaam wanneer de debugger is attached.", "OK");
        
        QRCodeScanner.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.OneDimensional,
            AutoRotate = true,
            Multiple = false,
            TryHarder = true
        };
    }

    private async void JoinBySessionCodeBtn_Clicked(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(SessionCodeEntry.Text) && SessionCodeEntry.Text.Length <= 4 && SessionCodeEntry.Text.Length >= 1)
        {
            if (int.TryParse(SessionCodeEntry.Text, out int sessionCode))
            {
                if (sessionCode >= 1 && sessionCode <= 9999)
                {
                    await Navigation.PushAsync(new PlayerLobbyPage(sessionCode));
                    return;
                }
            }
        }

        await DisplayAlert("Error", "Please enter a valid session code. (between 1 and 9999)", "OK");
    }

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e) // Het scannen van QR-codes is heeeeeeeeel langzaam wanneer de debugger is gekoppeld. zonder is hij nog steeds langaam maar wel iets sneller.
    {
        var result = e.Results.LastOrDefault();
        if (result != null)
        {
            // Om één of andere manier kun je niet direct een Entry aanpassen vanuit de event handler. dit moet je via de MainThread doen.
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SessionCodeEntry.Text = result.Value; // Whenever a barcode is detected, set the text of the SessionCodeEntry to the last scanned value.
            });
        }
    }
}