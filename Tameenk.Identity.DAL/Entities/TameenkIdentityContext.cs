using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Tameenk.Identity.DAL
{
    public partial class TameenkIdentityContext : IdentityDbContext
    {       
        public TameenkIdentityContext()
        { }

        public TameenkIdentityContext(DbContextOptions<TameenkIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=TameenkIdentity;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
