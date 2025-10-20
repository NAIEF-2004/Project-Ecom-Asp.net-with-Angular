using Ecom_Core.Interface;
using Ecom_Infrasteucture.Data;
using Ecom_Infrasteucture.Reposetores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            //services.AddScoped(typeof(IGenricRepository<>), typeof(GenricRepository<>));
            //services.AddScoped<ICategoryRepostiry, CategoryReposetiry>();
            //services.AddScoped<IPrudactRepostiry, PrudactRepostiry>();
            //services.AddScoped<IPhotoRepostiry, PhotoRepostiry>();
            //هاد البديل عن كل القاءمة 
            //applay pattern unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //applay dbcontext
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Ecom")));

            return services;
        }

    }
}
