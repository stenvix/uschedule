using USchedule.Core.Enums;

namespace USchedule.Models.DTO
{
    public class DbConfig
    {
        public DatabaseProvider Provider { get; set; }
        public string Connection { get; set; }
    }
}