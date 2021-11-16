using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.DB.Base;

namespace ToDoApp_backend.DB
{
    public class Setting : DbBase
    {
        public long UserId { get; set; }
        public bool NotificationSettingsFlg { get; set; }
        public bool CategoryMasterFlg { get; set; }
    }
}
