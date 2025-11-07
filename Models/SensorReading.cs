using SQLite;
using System;

namespace SmartHomeMonitor.Models
{
    public class SensorReading
    {
        [PrimaryKey, AutoIncrement]
        public int ReadingId { get; set; }

        public int DeviceId { get; set; }

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public double AirQuality { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        // Calculated property for display
        [Ignore]
        public string DisplayTimestamp => Timestamp.ToString("MMM dd, yyyy HH:mm:ss");

        [Ignore]
        public string TemperatureDisplay => $"{Temperature:F1}°C";

        [Ignore]
        public string HumidityDisplay => $"{Humidity:F1}%";

        [Ignore]
        public string AirQualityDisplay => $"{AirQuality:F0}";
    }
}
