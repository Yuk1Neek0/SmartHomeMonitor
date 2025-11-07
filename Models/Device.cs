using SQLite;
using System;

namespace SmartHomeMonitor.Models
{
    public class Device
    {
        [PrimaryKey, AutoIncrement]
        public int DeviceId { get; set; }

        public int UserId { get; set; }

        [MaxLength(100)]
        public string DeviceName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(50)]
        public string DeviceType { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime RegisteredAt { get; set; } = DateTime.Now;
    }
}
