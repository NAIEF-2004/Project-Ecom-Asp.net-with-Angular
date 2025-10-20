using Ecom_Core.Entites.Prudact;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Infrasteucture.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>option):base(option)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Prudact> Prudacts { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }

        protected virtual void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
