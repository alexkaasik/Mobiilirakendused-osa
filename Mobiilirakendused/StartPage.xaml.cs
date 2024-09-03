namespace Mobiilirakendused
{  
    public partial class StartPage : ContentPage
    {

        Editor editor;
        Button Button_tagasi, Button_edasi, Button_algus;

        HorizontalStackLayout hsl;
        VerticalStackLayout vsl;

        ScrollView sv;

        List<string> tekseId = new List<string> { "Tagasi", "Aveleht", "Edasi" };
        List<ContentPage> leht = new List<ContentPage>() { new TextPage() };

        public StartPage(List<string> tekseId)
        {           

            vsl = new VerticalStackLayout{BackgroundColor = Color.FromRgb(10, 23, 11)};
            for (int i = 0; i < tekseId.Count; i++)
            {
                Button nupp = new Button
                {
                    Text = tekseId[i],
                    ZIndex = i,
                    FontFamily = "Socafe 400",
                    Background = Color.FromRgb(20,20,20),
                    TextColor = Color.FromRgb(255,255,255)
                };
                vsl.Add(nupp);
                nupp.Clicked += lehte_avamine;

            }

            this.tekseId = tekseId;
        }

        private async void lehte_avamine(object? sender, EventArgs e)
        {
            Button btn = sender as Button;
            await Navigation.PushAsync(leht[btn.ZIndex]);
        }
    }
}