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
			string scores = $"Scoreboard:\n";
            foreach (var player in Game.Players)
			{
				scores += $"{player.DisplayName}: {player.Truths} Truths & {player.Drinks} Drinks.\n";
			}

			await DisplayAlert("Game Finished", scores, "END GAME");
            await Navigation.PopAsync();
            Game.Reset(true); // Reset the game but keep the settings
        }
	}

    private void NextQuestionBtn_Clicked(object sender, EventArgs e)
    {
        // Check if both players have answered the question
        if (AnswerPlayerOne.SelectedItem == null || AnswerPlayerTwo.SelectedItem == null || AnswerPlayerOne.SelectedIndex == 0 || AnswerPlayerTwo.SelectedIndex == 0)
        {
            DisplayAlert("Error", "Beide spelers moeten de vraag beantwoorden.", "OK");
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

        NextQuestion();
    }
}