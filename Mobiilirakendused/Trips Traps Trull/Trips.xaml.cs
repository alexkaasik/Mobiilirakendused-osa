namespace Mobiilirakendused
{ 
	public partial class Trips : ContentPage
	{
		Frame fr;
		Label lbl;
		Grid gr;
		Random random;
		List<Button> buttons;
		bool win = false;
		bool vsbot = true;
		bool turn = new Random().Next(2) == 0;

		Button TurnBtn;
		Button SwampButton;

		public Trips()
		{

			Title = "";
			random = new Random();
			buttons = new List<Button>(); // List to store all buttons



			lbl = new Label
			{
				Text = "Tic Tac Toe",
				FontSize = Device.GetNamedSize(NamedSize.Subtitle, typeof(Label)),
				HorizontalOptions = LayoutOptions.Center
			};

			gr = new Grid();

			// Create the grid structure
			for (int i = 0; i < 3; i++)
			{
				gr.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
				gr.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			}

			// Populate grid with buttons
			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					Button button = new Button
					{
						Text = "",
						TextColor = Colors.Black,
						BorderColor = Colors.Black,
						BorderWidth = 4,
						BackgroundColor = Colors.White,
						FontSize = 40
					};

					button.Clicked += clicked;

					gr.Add(button, x, y);
					buttons.Add(button);
				}
			}

			Button ResetButten = new Button
			{
				Text = "Restart",
				TextColor = Colors.Black,
				BorderColor = Colors.Black,
				BorderWidth = 4,
				BackgroundColor = Colors.White,
				FontSize = 20,
				HeightRequest = 60
			};

			TurnBtn = new Button
			{
				Text = "",
				TextColor = Colors.Black,
				BorderColor = Colors.Black,
				BorderWidth = 4,
				BackgroundColor = Colors.White,
				FontSize = 20,
				HeightRequest = 60
			};

			SwampButton = new Button
			{
				Text = "PvP",
				TextColor = Colors.Black,
				BorderColor = Colors.Black,
				BorderWidth = 4,
				BackgroundColor = Colors.White,
				FontSize = 20,
				HeightRequest = 60
			};
			ResetButten.Clicked += ResetGame;
			SwampButton.Clicked += SwapGame;

			gr.Add(ResetButten, 0, 3);
			gr.Add(TurnBtn, 1, 3);
			gr.Add(SwampButton, 2, 3);


			// Create a frame to hold the grid
			fr = new Frame
			{
				Content = gr,
				BorderColor = Color.FromRgb(124, 42, 92),
				CornerRadius = 12,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			// StackLayout to hold the label and the frame
			StackLayout st = new StackLayout
			{
				Children = { lbl, fr },
				Padding = new Thickness(20)
			};

			Content = st;
			if (vsbot) { if (turn == false) { RandomSet(); turn = true; TurnBtn.Text = "Player 1"; } }
		}



		private void SwapGame(object? sender, EventArgs e)
		{
			if (vsbot == false)
			{
				vsbot = true;
				SwampButton.Text = "PvP";
			}
			else if (vsbot == true)
			{
				vsbot = false;
				SwampButton.Text = "Bot";
			}
			ResetGame(vsbot, e);
		}

		// Event handler for button clicks
		private void clicked(object? sender, EventArgs e)
		{


			// Check if the sender is a button
			if (sender is Button clickedButton)
			{
				if (clickedButton.Text != String.Empty || win) { return; }




				if (vsbot)
				{
					if (turn == true)
					{
						clickedButton.Text = "O";
						turn = false;
					}
					RoundCheck();
					RandomSet();
					RoundCheck();
					turn = true;
					TurnBtn.Text = "Player 1";
				}
				else
				{
					if (turn == true)
					{
						clickedButton.Text = "O";
						turn = false;
						RoundCheck();
						TurnBtn.Text = "Player 2";
					}

					else if (turn == false)
					{
						clickedButton.Text = "X";
						turn = true;
						RoundCheck();
						TurnBtn.Text = "Player 1";
					}
				}
			}
		}

		private void RoundCheck()
		{
			if (CheckForWin("O"))
			{
				DisplayAlert("Game Over", "Player Wins!", "OK");
				win = true;
				return;
			}
			if (CheckForWin("X"))
			{
				DisplayAlert("Game Over", "Computer Wins!", "Ok");
				win = true;
				return;
			}
			if (buttons.All(b => b.Text != String.Empty))
			{
				DisplayAlert("Game Over", "It's a Tie!", "OK");
				win = true;
				return;
			}
		}

		private void RandomSet()
		{
			if (win) { return; }
			Button randomButton;
			do
			{
				int randomIndex = random.Next(buttons.Count);
				randomButton = buttons[randomIndex];
			} while (randomButton.Text != String.Empty);

			randomButton.Text = "X";
		}


		// Method to check for a win condition
		private bool CheckForWin(string player)
		{
			// Check rows, columns, and diagonals
			return (CheckRow(0, player) || CheckRow(1, player) || CheckRow(2, player) || // Rows
					CheckColumn(0, player) || CheckColumn(1, player) || CheckColumn(2, player) || // Columns
					CheckDiagonals(player)); // Diagonals
		}

		// Helper method to check a specific row for a win
		private bool CheckRow(int row, string player)
		{
			return buttons[row * 3].Text == player &&
				   buttons[row * 3 + 1].Text == player &&
				   buttons[row * 3 + 2].Text == player;
		}

		// Helper method to check a specific column for a win
		private bool CheckColumn(int col, string player)
		{
			return buttons[col].Text == player &&
				   buttons[col + 3].Text == player &&
				   buttons[col + 6].Text == player;
		}

		// Helper method to check diagonals for a win
		private bool CheckDiagonals(string player)
		{
			return (buttons[0].Text == player && buttons[4].Text == player && buttons[8].Text == player) ||
				   (buttons[2].Text == player && buttons[4].Text == player && buttons[6].Text == player);
		}

		// Method to reset the game
		private void ResetGame(object? sender, EventArgs e)
		{
			foreach (var button in buttons)
			{
				button.Text = String.Empty;
			}
			win = false;
			turn = new Random().Next(2) == 0;
			if (vsbot) { if (turn == false) { RandomSet(); turn = true; TurnBtn.Text = "Player 1"; } }
		}
	}
}