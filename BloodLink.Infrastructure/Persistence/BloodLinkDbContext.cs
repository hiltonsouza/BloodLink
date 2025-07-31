using BloodLink.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BloodLink.Infrastructure.Persistence
{
    public class BloodLinkDbContext : DbContext
    {
        public BloodLinkDbContext(DbContextOptions<BloodLinkDbContext> options) : base(options)
        {
        }

        public DbSet<Donor> Donors { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<BloodStock> BloodStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Configuring the Address entity as a value object
            modelBuilder.Entity<Donor>().OwnsOne(a => a.Address);
        }
    }

}
