using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tameenk.Identity.DAL
{
    public partial class TameenkIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public TameenkIdentityDbContext()
        {
        }

        public TameenkIdentityDbContext(DbContextOptions<TameenkIdentityDbContext> options)
            : base(options)
        {
        }       

    }
}
