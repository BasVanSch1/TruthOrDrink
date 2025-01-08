using Microsoft.Maui;
using System.Collections.ObjectModel;
using TruthOrDrink.Data;
using TruthOrDrink.Models;

namespace TruthOrDrink.Views;

public partial class GameSettingsPage : ContentPage
{
    private readonly TruthOrDrinkDatabase _database;
    private readonly Game Game = Game.Instance;
    private Dictionary<QuestionCategory, int> questionCategories = [];
    private Dictionary<QuestionType, bool> questionTypes = [];
    public GameSettingsPage()
	{       
        _database = IPlatformApplication.Current.Services.GetRequiredService<TruthOrDrinkDatabase>();

        InitializeComponent();
        PopulateSettings();
    }

	private void PopulateSettings()
	{
        // ItemPicker items
        List<String> QuestionKinds = ["Personal", "Pre-made", "Mixed"];
        List<int> QuestionLevels = [1, 2, 3, 4, 5];
        
        questionCategories = Game.QuestionCategories;
        questionTypes = Game.QuestionTypes;

        // Populating ItemPickers
        QuestionKindPicker.ItemsSource = QuestionKinds;
        QuestionKindPicker.SelectedItem = QuestionKinds[0];

        QuestionLevelPicker.ItemsSource = QuestionLevels;
        QuestionLevelPicker.SelectedItem = QuestionLevels[Game.QuestionLevel - 1]; // since QuestionLevel is an Integer and starts from 1, use the value from Game.QuestionLevel and subtract 1 to get the correct index.

        // Populating CollectionViews
        QuestionCategoryCollectionView.ItemsSource = questionCategories.ToList();
        QuestionTypeCollectionView.ItemsSource = questionTypes.ToList();

    }

    private async void SaveSettings(object sender, EventArgs e)
    {
        Game.QuestionLevel = (int)QuestionLevelPicker.SelectedItem; // set QuestionLevel
        Game.QuestionCategories = questionCategories; // update the Game.QuestionCategories dictionary with the new values.
        Game.QuestionTypes = questionTypes;
        Game.Questions = []; // clear the questions list

        List<QuestionType> enabledQuestionTypes = [];
        foreach (KeyValuePair<QuestionType, bool> kvp in Game.QuestionTypes) // Get a list of all enabled questionTypes, this is later on used to get questions from the database.
        {
            if (kvp.Value) // If kvp.Value == true
            {
                enabledQuestionTypes.Add(kvp.Key);
            }
        }

        foreach (KeyValuePair<QuestionCategory, int> kvp in Game.QuestionCategories) // Populate the Game.Questions list
        {
            if (kvp.Value > 0)
            {
                List<Question>? questions = await _database.GetQuestions(kvp.Key, enabledQuestionTypes, kvp.Value, Game.QuestionLevel);
                if (questions != null && questions.Count > 0)
                {
                    Game.AddQuestions(questions);
                }
            }
        }
        await Navigation.PopAsync(); // navigate back
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry && entry.BindingContext is KeyValuePair<QuestionCategory, int> kvp)
        {
            if (int.TryParse(entry.Text, out int newValue))
            {
               questionCategories[kvp.Key] = newValue;
            }
        }
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkbox && checkbox.BindingContext is KeyValuePair<QuestionType, bool> kvp)
        {
            questionTypes[kvp.Key] = e.Value;
        }
    }
}