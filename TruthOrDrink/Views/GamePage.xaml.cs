using TruthOrDrink.Models;

namespace TruthOrDrink.Views;

public partial class GamePage : ContentPage
{
	private readonly Game Game;
	private GameQuestion? CurrentQuestion;
    private List<Player>? ChosenPlayers;
	private int CurrentQuestionNumber = 0;

    public GamePage()
	{
		Game = Game.Instance;

		InitializeComponent();
		NextQuestion();

        AnswerPlayerOne.ItemsSource = Enum.GetNames<AnswerType>().ToList();
		AnswerPlayerOne.SelectedIndex = 0;
        AnswerPlayerTwo.ItemsSource = Enum.GetNames<AnswerType>().ToList();
		AnswerPlayerTwo.SelectedIndex = 0;

    }

	private async void NextQuestion()
	{
		CurrentQuestion = Game.GetNextQuestion();
		if (CurrentQuestion != null)
		{
			QuestionNumber.Text = $"Vraag {CurrentQuestionNumber + 1} / {Game.Questions.Count}";
			QuestionText.Text = CurrentQuestion.Text;
			CurrentQuestionNumber++;

            ChosenPlayers = Game.ChoosePlayers();

            if (ChosenPlayers == null)
            {
                await DisplayAlert("Error", "Something went wrong while choosing the players for this question.", "OK");
                return;
            }

            QuestionPlayerNameOne.Text = ChosenPlayers[0].DisplayName;
            QuestionPlayerNameTwo.Text = ChosenPlayers[1].DisplayName;

            AnswerPlayerOne.SelectedIndex = 0;
			AnswerPlayerTwo.SelectedIndex = 0;
        }
        else // If there are no questions left
		{
			// Get scores of all players
			string scores = Game.GetScoreboard();

            await DisplayAlert("Game Finished", scores, "END GAME");
            await Navigation.PopAsync();
            await Navigation.PopAsync(); // Because the Scoreboard Alert gets opened on the WaitingOnQuestionPage, we need to pop twice to get back to the HostLobbyPage. I can't seem to find a way to easily wait until the Waiting page is closed.
            Game.Reset(true); // Reset the game but keep the settings
        }
	}

    private async void NextQuestionBtn_Clicked(object sender, EventArgs e)
    {
        // Check if both players have answered the question
        if (AnswerPlayerOne.SelectedItem == null || AnswerPlayerTwo.SelectedItem == null || AnswerPlayerOne.SelectedIndex == 0 || AnswerPlayerTwo.SelectedIndex == 0)
        {
            await DisplayAlert("Error", "Beide spelers moeten de vraag beantwoorden.", "OK");
            return;
        }

        if (CurrentQuestion == null || ChosenPlayers == null)
        {
            return;
        }

        AnswerType answerOne = Enum.Parse<AnswerType>(AnswerPlayerOne.SelectedItem.ToString()); // ignore the possible null reference warning, since the selected item is check above.
        AnswerType answerTwo = Enum.Parse<AnswerType>(AnswerPlayerTwo.SelectedItem.ToString());

        CurrentQuestion.AnsweredBy.Add(ChosenPlayers[0], answerOne);
        CurrentQuestion.AnsweredBy.Add(ChosenPlayers[1], answerTwo);

        // Update the Player stats
        ChosenPlayers[0].AnswerQuestion(answerOne);
        ChosenPlayers[1].AnswerQuestion(answerTwo);

        AnswerType waitingAnswerType = AnswerType.Drink; // default value is Drink, this stays the same if both players chose to drink.
        string? playerNameToPerformAction = null; // this stays null if both players chose the same answer.

        if (answerOne == AnswerType.Truth && answerTwo == AnswerType.Truth) // if both chose to tell the truth
        {
            waitingAnswerType = AnswerType.Truth;
        }
        else if (answerOne == AnswerType.Truth) // if only player one chose to tell the truth
        {
            waitingAnswerType = AnswerType.Truth;
            playerNameToPerformAction = ChosenPlayers[0].DisplayName;
        }
        else if (answerTwo == AnswerType.Truth) // if only player two chose to tell the truth
        {
            waitingAnswerType = AnswerType.Truth;
            playerNameToPerformAction = ChosenPlayers[1].DisplayName;
        }
        await Navigation.PushAsync(new WaitingOnQuestionPage(waitingAnswerType, playerNameToPerformAction));

        NextQuestion();
    }
}