using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp_backend.ViewModel.Calendar
{
    public class CalendarExtendedProps
    {
        public long ScheduleId { get; set; }
        public string Summary { get; set; }
        public int UsageTime { get; set; }
        public bool CategoryMasterFlg { get; set; }
        public long CategoryMasterId { get; set; }
    }

    public class CalendarEventViewModel
    {
        public long Id { get; set; }
        public bool AllDay { get; set; }
        public string Title { get; set; }
        public DateTime? Start { get; set; }
        public string StartHour { get; set; }
        public string StartMinute { get; set; }
        public DateTime? End { get; set; }
        public string EndHour { get; set; }
        public string EndMinute { get; set; }
        public string TextColor { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public CalendarExtendedProps ExtendedProps { get; set; }
    }
}
