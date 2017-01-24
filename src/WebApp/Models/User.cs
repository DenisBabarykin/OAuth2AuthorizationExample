using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public User()
        {
            Tokens = new HashSet<Token>();
        }

        [Key]
        public string Login { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PassHash { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }

        public UserInfo CreateUserInfo()
        {
            return new UserInfo(Login, Name, Surname);
        }
    }
}
