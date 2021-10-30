using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.DB.Base;

namespace ToDoApp_backend.DB
{
    public class User : DbBase
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string NameKana { get; set; }
        public long LicenseId { get; set; } = 0;
    }
}
