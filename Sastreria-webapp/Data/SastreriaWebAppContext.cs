using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SastreriaWebApp.Areas.Identity.Data;
using SastreriaWebApp.Models;

namespace SastreriaWebApp.Data
{
    public class SastreriaWebAppContext : IdentityDbContext<IdentityUser>
    {
        public SastreriaWebAppContext (DbContextOptions<SastreriaWebAppContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<SastreriaWebApp.Models.Order> Orders { get; set; }
        public DbSet<SastreriaWebApp.Models.Client> Clients { get; set; }

        public DbSet<SastreriaWebApp.Models.Payment> payments { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    }
}
