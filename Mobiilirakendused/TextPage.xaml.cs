using Microsoft.Maui.Platform;

namespace Mobiilirakendused
{
    public partial class TextPage : ContentPage
    {
        Label label;
        Editor editor;
        Button button;
        HorizontalStackLayout hsl;
        List<string> buttons = new List<string> { "Tagasi", "Aveleht", "Edasi" };

        public TextPage()
        {
            label = new Label
            {
                Text = "no",
                TextColor = Color.FromRgb(1, 1, 1),
                FontFamily = "Socafe 400",
                WidthRequest = DeviceDisplay.Current.MainDisplayInfo.Width / 0.8

            };
            editor = new Editor
            {
                Text = "no",
                TextColor = Color.FromRgb(1, 1, 1),
                FontFamily = "Socafe 400",
                WidthRequest = DeviceDisplay.Current.MainDisplayInfo.Width / 0.8
            };

            hsl = new HorizontalStackLayout();
            for (int i = 0; i < buttons.Count; i++)
            {
                Button b = new Button
                {
                    Text = buttons[i],
                    ZIndex = i,
                    WidthRequest = DeviceDisplay.Current.MainDisplayInfo.Width / 0.8
                };
                hsl.Add(b);
            }
            VerticalStackLayout vsl = new VerticalStackLayout
            {
                Children = { label, editor, hsl }
            };
            Content = vsl;
        }
        private async void Liik(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
        }

        private async void Liik(object sender, TappedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}