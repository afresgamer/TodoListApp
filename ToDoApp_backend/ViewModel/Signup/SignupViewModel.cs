using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.ViewModel.Signup
{
    public class SignupViewModel
    {
        public string UserName { get; set; } = "";
        public string UserNameKana { get; set; } = "";
        public string MailAddress { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
