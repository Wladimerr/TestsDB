using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForTests.Shared.Models
{
    public class AuthedUserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public bool IsAdmin { get; set; }

        public AuthedUserModel(int id, string login, string password, string fullName, bool isAdmin)
        {
            Id = id;
            Login = login;
            Password = password;
            FullName = fullName;
            IsAdmin = isAdmin;
        }
    }
}
