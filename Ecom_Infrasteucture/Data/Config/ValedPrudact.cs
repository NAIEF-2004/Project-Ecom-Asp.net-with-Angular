using Ecom_Core.Entites.Prudact;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Infrasteucture.Data.Config
{
    public class ValedPrudact : IEntityTypeConfiguration<Prudact>
    {
        public void Configure(EntityTypeBuilder<Prudact> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(90);
            builder.Property(z => z.Price).HasPrecision(18, 2);
            //seedData
            builder.HasData(
                new Prudact { Id = 1, Name = "test", Description = "test", CategoryId = 1, Price = 10 }
                );
        }
    }
}