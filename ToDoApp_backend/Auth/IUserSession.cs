using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp_backend.Auth
{
    public interface IUserSession
    {
        long UserId { get; set; }
        string Name { get; set; }
    }
}
