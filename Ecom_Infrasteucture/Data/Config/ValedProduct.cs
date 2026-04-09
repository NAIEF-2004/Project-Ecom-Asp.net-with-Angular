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
    public class ValedProduct : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(90);
            builder.Property(z => z.NewPrice).HasPrecision(18, 2);
        }
    }
}
