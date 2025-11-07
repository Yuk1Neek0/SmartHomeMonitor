using SQLite;
using System;

namespace SmartHomeMonitor.Models
{
    public class Alert
    {
        [PrimaryKey, AutoIncrement]
        public int AlertId { get; set; }

        public int UserId { get; set; }

        public int DeviceId { get; set; }

        [MaxLength(50)]
        public string AlertType { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Message { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Severity { get; set; } = "Info";

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
