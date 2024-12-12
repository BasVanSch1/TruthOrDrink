using TruthOrDrink.Models;

namespace TruthOrDrink.Views;

public partial class GameSettingsPage : ContentPage
{
    private readonly Game Game = Game.Instance;
	public GameSettingsPage()
	{
        // TODO:
        //  retrieve previously saved settings and populate them accordingly.
        //  save the settings.        

		InitializeComponent();
        PopulateSettings();
    }

	private void PopulateSettings()
	{
        // ItemPicker items
        List<String> QuestionKinds = ["Personal", "Pre-made", "Mixed"];
        List<int> QuestionLevels = [1, 2, 3, 4, 5];

        List<QuestionType> questionTypes = new(Enum.GetValues<QuestionType>()); // same as: 'new List<QuestionType>(Enum.GetValues<QuestionType>())'
        List<QuestionCategory> questionCategories = new(Enum.GetValues<QuestionCategory>());

        // Populating ItemPickers
        QuestionKindPicker.ItemsSource = QuestionKinds;
        QuestionKindPicker.SelectedItem = QuestionKinds[0]; 

        QuestionLevelPicker.ItemsSource = QuestionLevels;
        QuestionLevelPicker.SelectedItem = QuestionLevels[Game.QuestionLevel - 1]; // since QuestionLevel is an Integer and starts from 1, I use the value from Game.QuestionsLevel and subtract 1 to get the correct index.

        // Populating CollectionViews
        QuestionCategoryCollectionView.ItemsSource = questionCategories;
        QuestionTypeCollectionView.ItemsSource = questionTypes;

    }

    private async void SaveSettings(object sender, EventArgs e)
    {
        Game.QuestionLevel = (int)QuestionLevelPicker.SelectedItem; // set QuestionLevel

        // Set amount of question per category (todo)

        // Set enabled question types (checkbox) (todo)

        await Navigation.PopAsync(); // navigate back
    }
}