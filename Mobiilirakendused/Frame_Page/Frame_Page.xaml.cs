namespace Mobiilirakendused;

public partial class Frame_Page : ContentPage
{
	Frame fr;
	Label lbl;
	Grid gr;
	public Frame_Page()
	{
		//InitializeComponent();
		Title = "";

		lbl = new Label
		{
			Text = "Raami",
			FontSize = Device.GetNamedSize(NamedSize.Subtitle, typeof(Label)),
		};

		gr = new Grid
		{
			//Padding = 4,
			ColumnSpacing = 4,
			RowSpacing = 4,

			RowDefinitions =
				{
					new RowDefinition{Height=new GridLength(1,GridUnitType.Star) },
					new RowDefinition{Height=new GridLength(1,GridUnitType.Star) },
					new RowDefinition{Height=new GridLength(1,GridUnitType.Star) },
				},
			ColumnDefinitions =
				{
					new ColumnDefinition{Width=new GridLength(1,GridUnitType.Star)},
					new ColumnDefinition{Width=new GridLength(1,GridUnitType.Star)},
					new ColumnDefinition{Width=new GridLength(1,GridUnitType.Star)},
				}
		};

		gr.Add(new BoxView { Color = Colors.Grey }, 0, 0);
		gr.Add(new BoxView { Color = Colors.Black }, 1, 0);
		gr.Add(new BoxView { Color = Colors.Grey }, 2, 0);
		gr.Add(new BoxView { Color = Colors.Black }, 0, 1);
		gr.Add(new BoxView { Color = Colors.Grey }, 1, 1);
		gr.Add(new BoxView { Color = Colors.Black }, 2, 1);
		gr.Add(new BoxView { Color = Colors.Grey }, 0, 2);
		gr.Add(new BoxView { Color = Colors.Black }, 1, 2);
		gr.Add(new BoxView { Color = Colors.Grey }, 2, 2);

		fr = new Frame
		{
			Content = gr,
			BorderColor = Color.FromRgb(124, 42, 92),
			CornerRadius = 12,
			VerticalOptions = LayoutOptions.FillAndExpand
		};
		StackLayout st = new StackLayout
		{
			Children = { lbl, fr }
		};

		Content = st;

	}
}