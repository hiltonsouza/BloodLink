using BloodLink.Core.Entities;
using BloodLink.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLink.Infrastructure.Persistence.Configurations
{
    public class DonorConfiguration : IEntityTypeConfiguration<Donor>
    {
        public void Configure(EntityTypeBuilder<Donor> builder)
        {
            builder
                .HasKey(d => d.Id);

            builder
                .OwnsOne(d => d.Address, a =>
                {
                    a.Property(ad => ad.Street).HasColumnName("Street");
                    a.Property(ad => ad.City).HasColumnName("City");
                    a.Property(ad => ad.State).HasColumnName("State");
                    a.Property(ad => ad.ZipCode).HasColumnName("ZipCode");
                });

            // Configuração para o enum BloodType
            builder.Property(d => d.BloodType)
                .HasConversion(
                    v => v.ToString(),
                    v => (BloodType)Enum.Parse(typeof(BloodType), v));

            builder
                .HasMany(d => d.Donations)         
                .WithOne(d => d.Donor)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(d => d.Email)
                .IsUnique();

        }
    }
}
