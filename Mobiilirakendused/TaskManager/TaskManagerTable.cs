using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mobiilirakendused
{
    public class TaskManagerTable : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Desciption { get; set; }
        public DateTime TaskDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        private bool _isTask;
        public bool IsTask
        {
            get => _isTask;
            set
            {
                if (_isTask != value)
                {
                    _isTask = value;
                   
                    OnPropertyChanged(nameof(IsTask));
                }
            }
        }

        private bool _isCompleted;

        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                OnPropertyChanged(nameof(IsCompleted));
            }
        }

        private double _taskOpacity;

        public double TaskOpacity
        {
            get => _taskOpacity;
            set
            {

                _taskOpacity = (IsCompleted) ? 0.5 : 1.0;
                OnPropertyChanged(nameof(TaskOpacity));
            }
        }

        //public double TaskOpacity => IsCompleted ? 0.5 : 1.0;

        




        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}