namespace Mobiilirakendused
{
	public partial class Valgusfoor : ContentPage
	{
		ScrollView sv;
		VerticalStackLayout MainBody;
		Frame LightFrame;

		Frame TopCircle;
		Frame MiddleCircle;
		Frame BottemCircle;

		bool Running = false;

		public Valgusfoor()
		{
			Title = "";
			BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(128, 128, 128);

			VerticalStackLayout LightStack = new VerticalStackLayout
			{
				Spacing = 20,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
			};

			MainBody = new VerticalStackLayout { VerticalOptions = LayoutOptions.Center };

			Frame TrafficLightBox = new Frame
			{
				Content = LightStack,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(100, 100, 100),
				BorderColor = Microsoft.Maui.Graphics.Color.FromRgb(0, 0, 0),

				//Padding = 40,
				CornerRadius = 32,
			};

			for (int i = 0; i < 3; i++)
			{
				LightFrame = new Frame
				{
					BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(200, 200, 200),
					BorderColor = Microsoft.Maui.Graphics.Color.FromRgb(0, 0, 0),
					WidthRequest = 120,
					HeightRequest = 120,
					CornerRadius = 100,
					Padding = 200,
				};

				LightStack.Add(LightFrame);
			}

			MainBody.Add(TrafficLightBox);

			HorizontalStackLayout HorizontalButtonLayout = new HorizontalStackLayout
			{
				Spacing = 20,
				Padding = 20,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				//BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(128, 128, 128),
			};

			Button StartButton = new Button
			{
				TextColor = Microsoft.Maui.Graphics.Color.FromRgb(0, 0, 0),
				BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(200, 200, 200),
				BorderColor = Microsoft.Maui.Graphics.Color.FromRgb(0, 0, 0),
				BorderWidth = 1,

				Text = "Sisse",
				FontFamily = "Comic San MS",
				FontSize = 28,

				WidthRequest = 120,
				HeightRequest = 60,

				//Padding = 0,
			};

			Button StopButton = new Button
			{
				TextColor = Microsoft.Maui.Graphics.Color.FromRgb(0, 0, 0),
				BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(200, 200, 200),
				BorderColor = Microsoft.Maui.Graphics.Color.FromRgb(0, 0, 0),
				BorderWidth = 1,

				Text = "Valja",
				FontFamily = "Comic San MS",
				FontSize = 28,

				WidthRequest = 120,
				HeightRequest = 60,

				//Padding = 0,
			};

			HorizontalButtonLayout.Add(StartButton);
			HorizontalButtonLayout.Add(StopButton);
			MainBody.Add(HorizontalButtonLayout);


			StartButton.Clicked += Blink;
			StopButton.Clicked += Freeze;

			TopCircle = LightStack.Children[0] as Frame;
			MiddleCircle = LightStack.Children[1] as Frame;
			BottemCircle = LightStack.Children[2] as Frame;


			sv = new ScrollView { Content = MainBody };
			Content = sv;
		}

		private void Freeze(object? sender, EventArgs e)
		{
			Running = false;
		}

		private async void Blink(object? sender, EventArgs e)
		{
			CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
			var token = _cancellationTokenSource.Token;

			if (Running) { return; }

			Running = true;

			while (Running)
			{
				await Task.Delay(1000, token);
				MiddleCircle.BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(200, 200, 200);
				TopCircle.BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(255, 0, 0);

				if (Running == false) { break; }

				await Task.Delay(1000, token);
				TopCircle.BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(200, 200, 200);
				MiddleCircle.BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(255, 255, 0);

				if (Running == false) { break; }

				await Task.Delay(1000, token);
				MiddleCircle.BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(200, 200, 200);
				BottemCircle.BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(0, 255, 0);

				if (Running == false) { break; }

				await Task.Delay(1000, token);
				BottemCircle.BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(200, 200, 200);
				MiddleCircle.BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(255, 255, 0);
			}
			BottemCircle.BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(200, 200, 200);
			MiddleCircle.BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(200, 200, 200);
			TopCircle.BackgroundColor = Microsoft.Maui.Graphics.Color.FromRgb(200, 200, 200);
		}
	}
}