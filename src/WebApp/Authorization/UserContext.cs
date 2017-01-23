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
        public const string connectionString = @"server=(localdb)\MSSQLLocalDB;Initial Catalog=userstore.mdf;Integrated Security=True;";

        public UserContext()
            : base(connectionString)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<Token> Tokens { get; set; }

        public DbSet<Client> Clients { get; set; }
    }
}
