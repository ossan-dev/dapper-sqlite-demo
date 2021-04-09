using dapper_sqlite_demo.LogMaster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_sqlite_demo.Database
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Log> Logs { get; set; }
    }
}
