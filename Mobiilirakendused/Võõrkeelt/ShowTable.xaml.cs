namespace Mobiilirakendused;

public partial class ShowTable : ContentPage
{
    public ShowTable(string answer)
    {
        //InitializeComponent();
        // Shows translation of the currenctly selected word.

        Button PopupFrame = new Button
        {
            BackgroundColor = Colors.White,
            TextColor = Colors.Black,
            VerticalOptions = LayoutOptions.Fill,
            HorizontalOptions = LayoutOptions.Fill,
            FontSize = 40,
            Text = answer
        };

        PopupFrame.Clicked += ReturnToList;

        Content = PopupFrame;
    }

    private async void ReturnToList(object? sender, EventArgs e) { await Navigation.PopAsync(); }
}