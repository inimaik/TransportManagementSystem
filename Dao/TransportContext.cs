using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Dao
{
    public class TransportContext : DbContext
    {
        public TransportContext() : base(DBPropertyUtil.GetConnectionString())
        {
        }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Trip> Trips { get; set; }
    }
}
