using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.DB.Base;

namespace ToDoApp_backend.DB
{
    public class Book : DbBase
    {
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Author { get; set; }
    }
}
