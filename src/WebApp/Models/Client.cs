using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Client
    {
        public Client()
        {
            Tokens = new HashSet<Token>();
        }

        [Key]
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string DomainAddress { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }

    }
}
