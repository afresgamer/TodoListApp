using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp_backend.ViewModel.Schedule
{
    public class CalendarScheduleViewModel
    {
        public string Summary { get; set; }
        public int UsageTime { get; set; }
        public bool CategoryMasterFlg { get; set; }
        public long CategoryMasterId { get; set; }
    }
}
