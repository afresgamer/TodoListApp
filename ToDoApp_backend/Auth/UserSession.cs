using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp_backend.Auth
{
    public class UserSession : IUserSession
    {
        public long UserId { get; set; }
        public string Name { get; set; }
    }
}
