using BloodLink.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLink.Infrastructure.Persistence.Configurations
{
    public class DonationConfiguration : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(d => d.Donor)
                .WithMany(d => d.Donations)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
