using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp_backend.DB.Base;

namespace ToDoApp_backend.DB
{
    public class CategoryMaster : DbBase
    {
        public string CategoryName { get; set; }
        public string Summary { get; set; }
        public int SortNo { get; set; }
    }
}
