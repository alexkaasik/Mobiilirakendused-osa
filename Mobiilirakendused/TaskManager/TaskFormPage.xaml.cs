using System.Data;

namespace Mobiilirakendused;

public partial class TaskFormPage : ContentPage
{
    private DatabaseService _database;
    private TaskManagerTable _task;

    public TaskFormPage(DatabaseService database, TaskManagerTable task = null)
    {
        InitializeComponent();
        _database = database;
        _task = task ?? new TaskManagerTable { TaskDate = DateTime.Now };

        BindingContext = _task;
    }

    private async void SaveTask_Clicked(object sender, EventArgs e)
    {
        var newTask = (TaskManagerTable)BindingContext;
        


        if (!newTask.IsTask) { newTask.EndTime = newTask.StartTime; }


        if (newTask.StartTime > newTask.EndTime) { await DisplayAlert("Invalid Time", "Start Time cannot be later than End Time.", "OK"); }
        else if (newTask.Title == null) { await DisplayAlert("Invalid title", "You can't have an empty title.", "OK"); }

        else
        {
            await _database.SaveTaskAsync(newTask);
            await Navigation.PopAsync();
        }
    }
}