using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.Shared.Models
{
    public class UserRegisterModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FIO { get; set; }
    }
}
