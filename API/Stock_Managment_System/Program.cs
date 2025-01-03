namespace Stock_Managment_System
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Identity.Web;
    using Microsoft.Identity.Web.Resource;
    using SMS.Data;
    using SMS.Models;
    using Microsoft.Extensions.Configuration;
    using SMS.Web.Infrastructure.Extensions;
    using SMS.Services.Interfaces;
    using SMS.Factory;
    using SMS.Repository;
    using SMS.Services;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()  // Allow any origin (you can specify one if needed)
                          .AllowAnyHeader()   // Allow any headers
                          .AllowAnyMethod();  // Allow any HTTP method (GET, POST, etc.)
                });
            });


            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<SMSDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false; 
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                
                
                // Disable account confirmation requirement
            })
                    .AddRoles<IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<SMSDbContext>();



            // Add services to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IFactoryService, FactoryService>();
            builder.Services.AddScoped<IRepositoryService, RepositoryService>();

            //  builder.Services.AddApplicationServices(typeof(IRepositoryService));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
