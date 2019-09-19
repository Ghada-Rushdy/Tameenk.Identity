
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using Tameenk.Identity.Log.DAL;

namespace Tameenk.Identity.Log.DAL
{
    public class LogContext : DbContext
    {
        public LogContext() : base()
        {
        }
        public LogContext(DbContextOptions<LogContext> options): base(options)
        {
        }

        public DbSet<AuthenticationLog> AuthenticationLogs { get; set; }
    }
}
