using AutoMapper;
using Ecom_Api.ExtentionsToJWT;
using Ecom_Api.MiddleWare;
using Ecom_Core.Entites.Prudact;
using Ecom_Core.Interface;
using Ecom_Infrasteucture;
using Ecom_Infrasteucture.Data;
using Ecom_Infrasteucture.Reposetores;
using Microsoft.AspNetCore.Identity;
using System.Reflection;


namespace Ecom_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //لزمتني من اجل دالة الخاصة بحماية من ارسال الريكوستات بشكل متكرر
            //rate limet
            builder.Services.AddMemoryCache();
            builder.Services.AddCors(op =>
                op.AddPolicy("CorsPolicy", op =>
                {
                    op.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:4200");
                })
            );

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            builder.Services.InfrastructureConfiguration(builder.Configuration);
            builder.Services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddCustomJwtAuth(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecom API v1");
                });
            }

            app.UseCors("CorsPolicy");

            //البوابة الاساسية للمشروع هي المديل وير
            app.UseMiddleware<ExceptionsMiddleWare>();
            //في حال الفرونت طلب اند بوينت غير موجودة 
            //اخذه لكونترولر خاص بالخطء انا انشاته
            app.UseWhen(
                context => !context.Request.Path.StartsWithSegments("/swagger"),
                appBuilder => { appBuilder.UseStatusCodePagesWithReExecute("/Error/{0}"); }
            );

            app.UseHttpsRedirection();

            app.UseAuthentication();//للتاكد من التوكن
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
