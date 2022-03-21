using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.DB.Base;

namespace ToDoApp_backend.DB
{
    public class SysMenu :DbBase
    {
        public long UserId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int SortNo { get; set; }
    }
}
