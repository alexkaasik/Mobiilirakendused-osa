using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobiilirakendused
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<TaskManagerTable>().Wait();
        }

        public Task<List<TaskManagerTable>> GetTasksAsync()
        {
            return _database.Table<TaskManagerTable>().ToListAsync();
        }

        public Task<int> SaveTaskAsync(TaskManagerTable task)
        {
            if (task.Id != 0)
                return _database.UpdateAsync(task);
            else
                return _database.InsertAsync(task);
        }

        public Task<int> DeleteTaskAsync(TaskManagerTable task)
        {
            return _database.DeleteAsync(task);
        }
    }
}