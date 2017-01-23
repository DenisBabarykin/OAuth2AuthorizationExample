using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class UserContext : DbContext
    {
        public UserContext()
            : base("DbConnection")
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public DbSet<Client> Clients { get; set; }
    }
}
