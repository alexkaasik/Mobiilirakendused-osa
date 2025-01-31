using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Mobiilirakendused
{
    public partial class TaskManager : ContentPage
    {
        private DatabaseService _database;
        

        public TaskManager()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tasks.db3");
            _database = new DatabaseService(dbPath);
            StartBackgroundTask();
            LoadTasks();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadTasks();
        }

        private async Task LoadTasks()
        {
            var tasksList = await _database.GetTasksAsync();
            
            TaskManagerView.ItemsSource = tasksList;
        }

        private async void AddTask_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskFormPage(_database));
        }

        private async void DeleteTask_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button?.CommandParameter is TaskManagerTable taskToDelete)
            {
                bool confirm = await DisplayAlert("Delete", "Are you sure you want to delete this task?", "Yes", "No");
                if (confirm)
                {
                    await _database.DeleteTaskAsync(taskToDelete);
                    await LoadTasks(); // Refresh the list after deletion
                }
            }
        }

        private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.BindingContext is TaskManagerTable task)
            {
                //task.IsCompleted = e.Value;

                await _database.SaveTaskAsync(task);
            }
        }

        private async void TaskManagerView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0)
            {
                var selectedTask = (TaskManagerTable)e.CurrentSelection[0];
                await Navigation.PushAsync(new TaskFormPage(_database, selectedTask));
                TaskManagerView.SelectedItem = null; // Deselect item
            }
        }



        public async Task StartBackgroundTask()
        {
            while (true)
            {
                await DeleteOldTasks();
                await LoadTasks(); 

                await Task.Delay(TimeSpan.FromHours(1));
            }
        }

        private async Task DeleteOldTasks()
        {
            var tasks = await _database.GetTasksAsync();
            foreach (var task in tasks)
            {
                if (task.TaskDate.AddDays(1) < DateTime.Now)
                {
                    await _database.DeleteTaskAsync(task);
                    continue;
                }
                if (task.TaskDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") && DateTime.Now.TimeOfDay > task.EndTime)
                {
                    task.IsCompleted = true;
                    await _database.SaveTaskAsync(task);
                }


                
            }
            await LoadTasks();
        }
    }
}
