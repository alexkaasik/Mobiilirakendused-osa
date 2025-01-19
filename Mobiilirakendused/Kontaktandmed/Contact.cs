using System.ComponentModel;

public class Contact

{
    private string name = "";
    private string photo = "";
    private string email = "";
    private string phone = "";
    private string description = "";
    

    public string Name
    {
        get => name;
        set
        {
            if (name != value)
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    public string Photo
    {
        get => photo;
        set
        {
            if (photo != value)
            {
                photo = value;
                OnPropertyChanged(nameof(Photo));
            }
        }
    }

    public string Email
    {
        get => email;
        set
        {
            if (email != value)
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
    }

    public string Phone
    {
        get => phone;
        set
        {
            if (phone != value)
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
    }

    public string Description
    {
        get => description;
        set
        {
            if (description != value)
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
