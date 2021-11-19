using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.DB.Base;

namespace ToDoApp_backend.DB
{
    public class Schedule : DbBase
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public int UsageTime { get; set; }
        public bool LineFlg { get; set; }
        public bool SlackFlg { get; set; }
        public string LineAccount { get; set; }
        public string LinePassword { get; set; }
        public string SlackAccount { get; set; }
        public string SlackPassword { get; set; }
        public long CategoryMasterId { get; set; }
    }
}
