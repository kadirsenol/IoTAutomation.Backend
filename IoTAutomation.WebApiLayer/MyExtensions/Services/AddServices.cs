using IoTAutomation.BussinesLayer.Abstract;
using IoTAutomation.BussinesLayer.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace IoTAutomation.WebApiLayer.MyExtensions.Services
{
    public static class AddServices
    {
        public static IServiceCollection AddManager(this IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ISolutionManager, SolutionManager>();
            services.AddScoped<ICartDetailManager, CartDetailManager>();
            services.AddScoped<IProfileImageManager, ProfileImageManager>();
            services.AddScoped<ISmartLightAppManager, SmartLightAppManager>();
            services.AddScoped<IPreOrderDetailManager, PreOrderDetailManager>();
            services.AddScoped<IPreOrderManager, PreOrderManager>();



            return services;

        }

        public static IServiceCollection AddTokenSetting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateAudience = true, // token üzerinde Audience doğrulamasını aktifleştirdik.
                        ValidateIssuer = true, //token üzerinde Issuer doğrulamasını aktifleştirdik.
                        ValidateLifetime = true, // token değerinin kullanım süresi doğrulamasını aktifleştirdik.
                        ValidateIssuerSigningKey = true, //token değerinin bu uygulamaya ait olup olmadığını anlamamızı sağlayan Security Key doğrulamasını aktifleştirdik.
                        ValidIssuer = "http://localhost:5051", //uygulamadaki tokenın Issuer değerini belirledik.
                        ValidAudience = "http://localhost:5051", //uygulamadaki tokenın Audience değerini belirledik.
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])), // Security Key doğrulaması için SymmetricSecurityKey nesnesi aracılığıyla mevcut keyi belirtiyoruz. Bunu screet dosyasına yaz ve buraya orayı refere et.
                        ClockSkew = TimeSpan.Zero //TimeSpan.Zero değeri ile token süresinin üzerine ekstra bir zaman eklemeksizin sıfır değerini belirtiyoruz.
                    });
            return services;
        }

        public static IServiceCollection AddCorsSetting(this IServiceCollection services)
        {
            services.AddCors(options => options.AddDefaultPolicy(builder =>
                                                                        builder.AllowAnyOrigin() //WithOrigins("https://kadirsenol.com") //Site olarak sadece buradan gelen isteklere izin ver.
                                                                        .AllowAnyHeader() //Gelen isteklerde butun basliklarin kabul edilmesi
                                                                        .AllowAnyMethod()));
            return services;

        }


    }
}
