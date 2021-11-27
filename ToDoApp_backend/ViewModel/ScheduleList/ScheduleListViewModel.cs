using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp_backend.ViewModel.ScheduleList
{
    public class ScheduleListViewModel
    {
        public long ScheduleId { get; set; }
        public string ScheduleName { get; set; }
        public string Summary { get; set; }
        public string StartDay { get; set; }
        public string EndDay { get; set; }
        public string UsageTime { get; set; }
        //値保存用
        public long CategoryMasterId { get; set; }
        public string CategoryMasterName { get; set; }

        //削除フラグ(フロント用)
        public bool DeletedFlg { get; set; }
    }
}
