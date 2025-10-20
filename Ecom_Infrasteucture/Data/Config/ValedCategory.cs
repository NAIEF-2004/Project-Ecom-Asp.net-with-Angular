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
    public class ValedCategory : IEntityTypeConfiguration<Category>
    {

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Id).IsRequired();
            //عملية السيدنك 
            //مشان لو قسمس فارغة تعطى قيم بدائية وما يصير خطء
            builder.HasData(
                new Category { Id=1,Name="test",Description="test"}
                );
        }
    }
}
