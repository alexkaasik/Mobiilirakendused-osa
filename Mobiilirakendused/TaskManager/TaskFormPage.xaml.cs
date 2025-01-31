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

        // Deletes if task saved date, is longer than day
        if (newTask.StartTime > newTask.EndTime) { await DisplayAlert("Invalid Time", "Start Time cannot be later than End Time.", "OK"); }
        // Set task to complete if in the same day and currenty time has surpassed save EndTime.
        else if (newTask.Title == null) { await DisplayAlert("Invalid title", "You can't have an empty title.", "OK"); }

        else
        {
            await _database.SaveTaskAsync(newTask);
            await Navigation.PopAsync();
        }
    }
}