using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToDoApp_backend.ViewModel.Schedule
{
    public class ScheduleViewModel
    {
        public string ScheduleName { get; set; }
        public string Summary { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public int UsageTime { get; set; }
        public bool NoticeSettingFlg { get; set; }
        public bool LineFlg { get; set; }
        public bool SlackFlg { get; set; }
        public string LineAccount { get; set; }
        public string LinePassword { get; set; }
        public string SlackAccount { get; set; }
        public string SlackPassword { get; set; }
        public bool CategoryMasterFlg { get; set; }
        public long CategoryMasterId { get; set; }
    }
}
