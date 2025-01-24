using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.IO;

namespace Mobiilirakendused;

public partial class WordList : ContentPage
{
    public ObservableCollection<WordDictionary> WordInfo { get; set; }

    public int Possition = 0;

    private Label WordLabel;
    private Label TranslationLabel;
    private Label ExplanationLabel;
    private Label LearingLabel;
    private CheckBox ShowLearningWord;

    private readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "WordList.json");

    public WordList()
    {
        //InitializeComponent();

        VerticalStackLayout VerticalMenuBody = new VerticalStackLayout();
        
        HorizontalStackLayout HorizontalButtonMenu = new HorizontalStackLayout { HorizontalOptions = LayoutOptions.Center };
        HorizontalStackLayout CheckerMenu = new HorizontalStackLayout { HorizontalOptions = LayoutOptions.Center };

        LoadWordListFromFile();

        if (WordInfo == null || WordInfo.Count == 0)
        {

            WordInfo = new ObservableCollection<WordDictionary>
            {
                new WordDictionary { Word = "Apple", Translation = "Õun", Explanation = "Fruit", Learing = false },
                new WordDictionary { Word = "Egg", Translation = "muuga", Explanation = "what come out chicken" },
                new WordDictionary { Word = "Banana", Translation = "Banaan", Explanation = "Minions", Learing = false },
                new WordDictionary { Word = "Cherry", Translation = "Kirss", Explanation = "Tree", Learing = false },
                new WordDictionary { Word = "Car", Translation = "Auto", Explanation = "Auto" },
                new WordDictionary { Word = "Film", Translation = "Film", Explanation = "Film" },
                new WordDictionary { Word = "Game", Translation = "Mäng", Explanation = "Mango", Learing = false },
                new WordDictionary { Word = "What", Translation = "Mida", Explanation = "Что" },
                new WordDictionary { Word = "Sword", Translation = "Mõõk", Explanation = "Weapon", Learing = false }
            };
        }

        WordLabel = new Label { Background = Colors.Grey, VerticalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Fill, FontSize = 40, HorizontalTextAlignment = TextAlignment.Center, HeightRequest = 200 };
        VerticalMenuBody.Add(WordLabel);

        //TranslationLabel = new Label();
        //VerticalMenuBody.Add(TranslationLabel);
        
        ExplanationLabel = new Label { Background = Colors.Grey, VerticalTextAlignment = TextAlignment.Center, HorizontalOptions = LayoutOptions.Fill, FontSize = 40, HorizontalTextAlignment = TextAlignment.Center, HeightRequest = 200 };
        VerticalMenuBody.Add(ExplanationLabel);
        
        //LearingLabel = new Label();
        //VerticalMenuBody.Add(LearingLabel);

        WordsLable();

        Label Spaceing = new Label { Padding = 20 };
        VerticalMenuBody.Add(Spaceing);

        Button ButtonPrevious = new Button
        {
            Text = "<-"
        };
        HorizontalButtonMenu.Add(ButtonPrevious);
        ButtonPrevious.Clicked += ButtonPreviousFunction;

        Button ButtonShow = new Button
        {
            Text = "Show"
        };
        HorizontalButtonMenu.Add(ButtonShow);
        ButtonShow.Clicked += ShowButtonFunction;

        Button ButtonNext = new Button
        {
            Text = "->"
        };
        HorizontalButtonMenu.Add(ButtonNext);
        ButtonNext.Clicked += ButtonNextFunction;

        VerticalMenuBody.Add(HorizontalButtonMenu);

        Label ShowLearningLabel = new Label { Text = "Show only learning word", VerticalOptions = LayoutOptions.Center };
        CheckerMenu.Add(ShowLearningLabel);

        ShowLearningWord = new CheckBox { IsChecked = false };
        ShowLearningWord.CheckedChanged += CheckerUpdater;
        CheckerMenu.Add(ShowLearningWord);
        
        VerticalMenuBody.Add(CheckerMenu);

        Button CreateButton = new Button
        {
            Text = "Create"
        };
        CreateButton.Clicked += CreateNewWord;
        VerticalMenuBody.Add(CreateButton);

        Button UpdateButton = new Button
        {
            Text = "Update"
        };
        UpdateButton.Clicked += UpdateWord;
        VerticalMenuBody.Add(UpdateButton);

        Button DeleteButton = new Button
        {
            Text = "Delete"
        };
        DeleteButton.Clicked += DeleteWord;
        VerticalMenuBody.Add(DeleteButton);

        Content = VerticalMenuBody;
    }

    private void UpdateButton_Clicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ButtonNextFunction(object? sender, EventArgs e)
    {
        Possition = Possition + 1;

        if (Possition >= WordInfo.Count() - 1)
        {
            Possition = 0;
        }

        while (!(WordInfo[Possition].Learing) && ShowLearningWord.IsChecked)
        {
            Possition = Possition + 1;

            if (Possition >= WordInfo.Count() - 1)
            {
                Possition = 0;
            }
        }

        WordsLable();
    }

    private void ButtonPreviousFunction(object? sender, EventArgs e)
    {

        Possition = Possition - 1;

        if (Possition < 0)
        {
            Possition = WordInfo.Count() - 1;
        }

        while (!(WordInfo[Possition].Learing) && ShowLearningWord.IsChecked)
        {
            Possition = Possition - 1;

            if (Possition < 0)
            {
                Possition = WordInfo.Count() - 1;
            }
        }



        WordsLable();
    }

    private void WordsLable()
    {
        WordLabel.Text = WordInfo[Possition].Word;
        //TranslationLabel.Text = "Translation: " + WordInfo[Possition].Translation;
        ExplanationLabel.Text = "Explanation:\n" + WordInfo[Possition].Explanation;
        //LearingLabel.Text = "Status: " + WordDictionary.BoolToLearnString(WordInfo[Possition].Learing);
    }

    private void CheckerUpdater(object? sender, EventArgs e)
    {
        if (!(WordInfo[Possition].Learing) && ShowLearningWord.IsChecked) { ButtonNextFunction(sender, e); }
    }

    private async void ShowButtonFunction(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new ShowTable(WordInfo[Possition].Translation));
    }
    
    private async void CreateNewWord(object? sender, EventArgs e)
    {

        var editPage = new EditWordList(null, WordInfo);
        await Navigation.PushAsync(editPage);


        var result = await editPage.EditingCompleted.Task;

        if (result)
        {
            SaveWordListToFile();
        }

        if (WordInfo.Count == 0) { CreateNewWord(sender, e); }
    }

    private async void UpdateWord(object? sender, EventArgs e)
    {
        var editPage = new EditWordList(WordInfo[Possition], WordInfo);
        await Navigation.PushAsync(editPage);

        
        var result = await editPage.EditingCompleted.Task;

        if (result) 
        {
            SaveWordListToFile();
            WordsLable(); // Refresh the displayed data
        }
    }

    private async void DeleteWord(object? sender, EventArgs e)
    {

        
        WordInfo.RemoveAt(Possition);
        Possition = Possition - 1;
        if (Possition < 0) Possition = 0;
        SaveWordListToFile();
        if (WordInfo.Count == 0)
        {
            CreateNewWord(sender, e);
        }
        else if (WordInfo.Count > 0)
        {
            ButtonNextFunction(sender, e);
        }
        
        
    }

    private void SaveWordListToFile()
    {
        try
        {
            var json = JsonSerializer.Serialize(WordInfo);
            File.WriteAllText(FilePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}");
        }
    }

    private void LoadWordListFromFile()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                var loadedWords = JsonSerializer.Deserialize<ObservableCollection<WordDictionary>>(json);
                if (loadedWords != null)
                {
                    WordInfo = loadedWords;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}");
            File.Create(FilePath).Close();
        }
    }
}