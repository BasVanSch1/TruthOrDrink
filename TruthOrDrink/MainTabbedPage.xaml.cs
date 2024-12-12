using TruthOrDrink.Data;

namespace TruthOrDrink;

public partial class MainTabbedPage : TabbedPage
{
    public MainTabbedPage()
	{
        InitializeComponent();
		CurrentPage = Children[1]; // opens the second page (Home) when the TabbedPage is opened
	}

}