using System.ComponentModel;

namespace Mobiilirakendused
{
    public class WordDictionary : INotifyPropertyChanged
    {
        private string word = "";
        private string translation = "";
        private string explanation = "";
        private bool learing = true; // Category 

        public string Word
        {
            get => word;
            set
            {
                if (word != value)
                {
                    word = value;
                    OnPropertyChanged(nameof(Word));
                }
            }
        }

        public string Translation
        {
            get => translation;
            set
            {
                if (translation != value)
                {
                    translation = value;
                    OnPropertyChanged(nameof(Translation));
                }
            }
        }

        public string Explanation
        {
            get => explanation;
            set
            {
                if (explanation != value)
                {
                    explanation = value;
                    OnPropertyChanged(nameof(Explanation));
                }
            }
        }

        public bool Learing
        {
            get => learing;
            set
            {
                if (learing != value)
                {
                    learing = value;
                    OnPropertyChanged(nameof(Learing));
                }
            }
        }
        public static string BoolToLearnString(bool BooleanQuery)
        {
            if (BooleanQuery)
            {
                return "Learning";
            }
            return "Learned";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
