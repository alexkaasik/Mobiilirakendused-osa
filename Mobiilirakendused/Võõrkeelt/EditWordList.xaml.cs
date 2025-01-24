using System;
using System.Collections.ObjectModel;

namespace Mobiilirakendused;

public partial class EditWordList : ContentPage
{

    private WordDictionary _WordDictionary;
    private ObservableCollection<WordDictionary> _WordDictionarys;
    private bool _IsNewWord;

    public TaskCompletionSource<bool> EditingCompleted { get; } = new TaskCompletionSource<bool>();

    public EditWordList(WordDictionary SelectedWordDictionary, ObservableCollection<WordDictionary> CurrentWordDictionaryList)
    {
        InitializeComponent();

        _WordDictionarys = CurrentWordDictionaryList;

        if (SelectedWordDictionary == null)
        {
            _WordDictionary = new WordDictionary();
            _IsNewWord = true;
        }
        else
        {
            _WordDictionary = SelectedWordDictionary;
            _IsNewWord = false;
        }

        BindingContext = _WordDictionary;

        var cancelButton = new Button { Text = "Cancel" };
        cancelButton.Clicked += (s, e) =>
        {
            // Set EditingCompleted to false to notify
            EditingCompleted.TrySetResult(false);
            Navigation.PopAsync();
        };

        //Content = new VerticalStackLayout { Children = { saveButton, cancelButton } };
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_IsNewWord)
        {

            _WordDictionarys.Add(_WordDictionary);
        }
        else
        {

            var index = _WordDictionarys.IndexOf(_WordDictionary);
            if (index >= 0)
            {
                _WordDictionarys[index] = _WordDictionary;
            }
        }
        EditingCompleted.TrySetResult(true);
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        EditingCompleted.TrySetResult(false);
        await Navigation.PopAsync();
    }
}