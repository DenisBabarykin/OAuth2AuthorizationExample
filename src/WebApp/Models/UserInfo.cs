using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserInfo
    {
        public UserInfo(string login, string name, string surname)
        {
            Login = login;
            Name = name;
            Surname = surname;
        }

        public string Login { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
    }
}
