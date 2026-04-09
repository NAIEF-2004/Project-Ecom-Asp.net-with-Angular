using Ecom_Core.Entites.Prudact;
using Ecom_Core.Interface;
using Ecom_Core.Services;
using Ecom_Infrasteucture.Data;
using Ecom_Infrasteucture.Reposetores;
using Ecom_Infrasteucture.Reposetores.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom_Infrasteucture
{
    public static  class InfrastructureRegisturation
    {
        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // يمكنك استخدام أحد هذه الأنواع حسب الحاجة:
            // services.AddTransient<IGenericRepository<>, GenericRepository<>>();
            // services.AddScoped<IGenericRepository<>, GenericRepository<>>();
            // services.AddSingleton<IGenericRepository<>, GenericRepository<>>();

            //استبدلتن بطريقة اسهل unikwork
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<IPhotoRepository, PhotoRepository>();
            //هاد البديل عن كل القاءمة 
            //applay pattern unit of work

            

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //applay dbcontext
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Ecom")));

           

            //save
            services.AddSingleton<IImageManagmentService,ImageManagemintService>();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));

            return services;
        }

    }
}
