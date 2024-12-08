namespace NavigationTestMAUI;

public partial class AppTabbedPage : TabbedPage
{
	public AppTabbedPage()
	{
		InitializeComponent();
		CurrentPageChanged += OnCurrentPageChanged;
	}

	public void OnCurrentPageChanged(object? sender, EventArgs e)
	{
		if (CurrentPage is NavigationPage navigationPage)
		{
            navigationPage.PopToRootAsync(); // Pop to root whenever tab changes
		}
	}
}