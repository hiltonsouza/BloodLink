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
    public class BloodStockConfiguration : IEntityTypeConfiguration<BloodStock>
    {
        public void Configure(EntityTypeBuilder<BloodStock> builder)
        {


            builder.HasKey(x => x.Id);

            //builder.Property(bs => bs.BloodType)
            //    .IsRequired();

            //builder
            //    .Property(bs => bs.FactorRh)
            //    .IsRequired();

            //builder.Property (bs => bs.BloodVolumeInML)
            //    .IsRequired();
        }
    }
}
