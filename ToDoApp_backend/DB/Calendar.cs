using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.DB.Base;

namespace ToDoApp_backend.DB
{
    public class Calendar : DbBase
    {
        public long UserId { get; set; }
        public bool AllDay { get; set; }
        public string Title { get; set; }
        public DateTime? Start { get; set; }
        public string StartHour { get; set; }
        public string StartMinute { get; set; }
        public DateTime? End { get; set; }
        public string EndHour { get; set; }
        public string EndMinute { get; set; }
        public long ScheduleId { get; set; }
    }
}
