using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using techYard.API.Extensions;
using techYard.Data.Context;
using techYard.Data.Entities;
using techYard.Service.Services.AccountServices;

namespace techYard.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<techYardDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = false;
            })
                .AddEntityFrameworkStores<techYardDbContext>()
                .AddDefaultTokenProviders();
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Customer", policy => policy.RequireRole("Customer", "Admin"));
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTechYardServices();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                // Configure production settings
                app.UseExceptionHandler("/Home/Error"); // تحديد مسار صفحة الأخطاء
                app.UseHsts(); // تفعيل HSTS
            }

            app.UseStaticFiles(); // للسماح بتقديم الملفات الثابتة مثل الصور


            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
