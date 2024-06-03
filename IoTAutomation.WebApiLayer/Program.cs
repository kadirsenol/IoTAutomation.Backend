
using IoTAutomation.DataAccessLayer.DBContexts;
using IoTAutomation.WebApiLayer.MyExtensions.AutoMapper;
using IoTAutomation.WebApiLayer.MyExtensions.Services;
using Microsoft.EntityFrameworkCore;

namespace IoTAutomation.WebApiLayer
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddManager(); //MyExtensions class
            builder.Services.AddTokenSetting(builder.Configuration); //MyExtensions class (Burada configuration nesnesi builderin içinde hazirda var. Ama baska yerde IConfiguration olarak olusturaman gerekli.)
            builder.Services.AddCorsSetting(); //MyExtensions class (Farkli originlerden(platformlardan) gelen tum istekleri kabul et)
            builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MyDb")));


            builder.Services.AddControllers();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();//Eklemis oldugum CorsSettingi kullanmasi icin gerekli middleware. // Postman ve browserden gelen isteklere uygulanmaz                        
            //app.UseCheckCorsOrigin(); // Trayýcýdan ve postmanden gelen farklý origin isteklerine uygulamak icin.

            app.UseHttpsRedirection();
            app.UseAuthentication();//JWT ile login islemi icin Authentication middleware eklentisi. JWT veya Cookie kullanmazsan buna gerek yok.
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
