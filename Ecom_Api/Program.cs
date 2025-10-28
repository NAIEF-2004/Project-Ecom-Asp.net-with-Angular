using Ecom_Api.MiddleWare;
using Ecom_Core.Interface;
using Ecom_Infrasteucture;
using Ecom_Infrasteucture.Reposetores;
namespace Ecom_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.InfrastructureConfiguration(builder.Configuration);
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var app = builder.Build();
           

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //البوابة الاساسية للمشروع هي المديل وير
            app.UseMiddleware<ExceptionsMiddleWare>();
            //في حال الفرونت طلب اند بوينت غير موجودة 
            //اخذه لكونترولر خاص بالخطء انا انشاته
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
