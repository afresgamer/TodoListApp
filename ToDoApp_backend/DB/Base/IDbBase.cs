using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp_backend.DB.Base
{
    public interface IDbBase
    {
        long Id { get; set; }
        DateTime? CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
    }
}
