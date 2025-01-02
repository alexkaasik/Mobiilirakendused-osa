namespace Mobiilirakendused
{
	public partial class StartPage : ContentPage
	{
		
		ScrollView sv;
		VerticalStackLayout vst;
		public List<ContentPage> page = new List<ContentPage>() { new Valgusfoor(), new RBG_Cube() };
		public List<string> text = new List<string> { "Valgusfoor", "RBG_Cube" };

		public StartPage()
		{
			Title = "Avaleht";
			vst = new VerticalStackLayout() { BackgroundColor = Colors.White };
			for (int i = 0; i < text.Count; i++)
			{
				Button btn = new Button()
				{
					Text = text[i],
					BackgroundColor = Colors.Grey,
					TextColor = Colors.Black,
					FontFamily = "Socafe 400",
					BorderWidth = 8,
					ZIndex = i
				};
				vst.Add(btn);
				btn.Clicked += Button_Clicked;
			}
			sv = new ScrollView { Content = vst };
			Content = sv;
		}

		private async void Button_Clicked(object? sender, EventArgs e)
		{
			Button btn = sender as Button;
			await Navigation.PushAsync(page[btn.ZIndex]);
		}
	}
}