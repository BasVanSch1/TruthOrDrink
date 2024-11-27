using TruthOrDrink.Data;
using TruthOrDrink.Models;

namespace TruthOrDrink.Views;

public partial class NewPage1 : ContentPage
{
    private TruthOrDrinkDatabase _database;
    public NewPage1(TruthOrDrinkDatabase db)
	{
		InitializeComponent();
        _database = db;
	}

    private async void HomeToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//main");
    }

    private async void LogoutToolbarItem_Clicked(object sender, EventArgs e)
    {
        await SecureStorage.SetAsync("IsAuthenticated", "false");
        await Shell.Current.GoToAsync("//login");
    }

    private async void GetQuestionsBtn_Clicked(object sender, EventArgs e)
    {
        var questions = await _database.GetQuestionsAsync();

        string message = "Alle Vraag IDs:\n";

        if (questions == null || questions.Count <= 0)
        {
            message = "Geen vragen in database";
        } else
        {
            foreach (var question in questions)
            {
                message += $"{question.Id}\n";
            }
        }


        await DisplayAlert("Vragen", message, "OK");
    }

    private async void CreateQuestionBtn_Clicked(object sender, EventArgs e)
    {
        var input = await DisplayPromptAsync("Vraag aanmaken", "Wat is de vraag?");

        if (input == null)
        {
            await DisplayAlert("Alert", "Invalid input", "OK");
            return;
        }

        var question = new Question()
        {
            Description = input,
            QuestionType = QuestionType.Question,
        };

        // save a new question to the database if it doesn't exist, otherwise update the existing one. (in this case question has no ID so it will create a new one)
        var result = await _database.UpdateQuestionAsync(question);

        if (result == 0)
        {
            await DisplayAlert("ERROR", "Failed to create new question (0 rows affected)", "OK");
            return;
        }
    }
}