using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomManagement
{
    public class RoomContext : DbContext
    {
        public const string connectionString = @"server=(localdb)\MSSQLLocalDB;Initial Catalog=roomstore.mdf;Integrated Security=True;";

        public RoomContext()
            : base(connectionString)
        { }

        public DbSet<Room> Rooms { get; set; }
    }
}
