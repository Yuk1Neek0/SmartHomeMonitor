using SmartHomeMonitor.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SmartHomeMonitor.Services
{
    public class DeviceRepository : IRepository<Models.Device>
    {
        private SQLiteAsyncConnection _database;
        private readonly string _databasePath;

        public DeviceRepository()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _databasePath = Path.Combine(appDataPath, "smarthome.db3");
        }

        public async Task InitializeAsync()
        {
            if (_database != null)
                return;
            _database = new SQLiteAsyncConnection(_databasePath);
            await _database.CreateTableAsync<Models.Device>();
        }

        public async Task<List<Models.Device>> GetAllAsync()
        {
            await InitializeAsync();
            return await _database.Table<Models.Device>()
                .OrderByDescending(d => d.RegisteredAt)
                .ToListAsync();
        }

        public async Task<Models.Device> GetByIdAsync(int id)
        {
            await InitializeAsync();
            return await _database.Table<Models.Device>()
                .Where(d => d.DeviceId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> InsertAsync(Models.Device item)
        {
            await InitializeAsync();
            return await _database.InsertAsync(item);
        }

        public async Task<int> UpdateAsync(Models.Device item)
        {
            await InitializeAsync();
            return await _database.UpdateAsync(item);
        }

        public async Task<int> DeleteAsync(Models.Device item)
        {
            await InitializeAsync();
            return await _database.DeleteAsync(item);
        }

        public async Task<int> DeleteAllAsync()
        {
            await InitializeAsync();
            return await _database.DeleteAllAsync<Models.Device>();
        }

        public async Task<List<Models.Device>> GetActiveDevicesAsync()
        {
            await InitializeAsync();
            return await _database.Table<Models.Device>()
                .Where(d => d.IsActive)
                .ToListAsync();
        }
    }
}
