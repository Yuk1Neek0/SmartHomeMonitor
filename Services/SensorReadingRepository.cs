using SmartHomeMonitor.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SmartHomeMonitor.Services
{
    public class SensorReadingRepository : IRepository<SensorReading>
    {
        private SQLiteAsyncConnection _database;
        private readonly string _databasePath;

        public SensorReadingRepository()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _databasePath = Path.Combine(appDataPath, "smarthome.db3");
        }

        public async Task InitializeAsync()
        {
            if (_database != null)
                return;

            _database = new SQLiteAsyncConnection(_databasePath);
            await _database.CreateTableAsync<SensorReading>();
        }

        public async Task<List<SensorReading>> GetAllAsync()
        {
            await InitializeAsync();
            return await _database.Table<SensorReading>()
                .OrderByDescending(r => r.Timestamp)
                .ToListAsync();
        }

        public async Task<SensorReading> GetByIdAsync(int id)
        {
            await InitializeAsync();
            return await _database.Table<SensorReading>()
                .Where(r => r.ReadingId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> InsertAsync(SensorReading item)
        {
            await InitializeAsync();
            return await _database.InsertAsync(item);
        }

        public async Task<int> UpdateAsync(SensorReading item)
        {
            await InitializeAsync();
            return await _database.UpdateAsync(item);
        }

        public async Task<int> DeleteAsync(SensorReading item)
        {
            await InitializeAsync();
            return await _database.DeleteAsync(item);
        }

        public async Task<int> DeleteAllAsync()
        {
            await InitializeAsync();
            return await _database.DeleteAllAsync<SensorReading>();
        }

        public async Task<List<SensorReading>> GetReadingsByDeviceAsync(int deviceId)
        {
            await InitializeAsync();
            return await _database.Table<SensorReading>()
                .Where(r => r.DeviceId == deviceId)
                .OrderByDescending(r => r.Timestamp)
                .ToListAsync();
        }
    }
}
