using TruthOrDrink.Models;

namespace TruthOrDrink.Views;

public partial class WaitingOnQuestionPage : ContentPage
{
    public WaitingOnQuestionPage(AnswerType answerType, string? playerName = null) // if playerName is not provided, it will count as both players having to perform the action.
    {
        InitializeComponent();

        if (answerType == AnswerType.Truth)
        {
            AnswerTypeImage.Source = "truth.png";
            if (playerName != null)
            {
                PlayerNameAndAction.Text = $"Speler {playerName} moet de waarheid vertellen!";
            }
            else
            {
                PlayerNameAndAction.Text = $"Beide spelers moeten de waarheid vertellen!";
            }
        }
        else if (answerType == AnswerType.Drink)
        {
            AnswerTypeImage.Source = "drink.png";
            if (playerName != null)
            {
                PlayerNameAndAction.Text = $"Speler {playerName} moet drinken!";
            }
            else
            {
                PlayerNameAndAction.Text = $"Beide spelers moeten drinken!";
            }
        }
    }

    private async void NextQuestionBtn_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopAsync(); // close the waiting page.
    }
}