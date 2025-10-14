using EnvanterApp.Application;
using EnvanterApp.Domain.Entities.Identity;
using EnvanterApp.Infrastructure;
using EnvanterApp.Persistence;
using EnvanterApp.Persistence.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using System.Text;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;



namespace EnvanterApp.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices();
            builder.Services.AddPersistenceServices(builder.Configuration);

            //Cors ayarlarý daha sonra revize edilerek kýsýtlanacak.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidAudience = builder.Configuration["Token:Audience"],
                    ValidIssuer = builder.Configuration["Token:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen();

            //Logger log = new LoggerConfiguration()
            //    .WriteTo.Console()
            //    .WriteTo.File("logs/log.txt")
            //    .WriteTo.MSSqlServer(
            //        connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
            //        sinkOptions: new MSSqlServerSinkOptions
            //        {
            //            TableName = "Logs",
            //            AutoCreateSqlTable = true
            //        })
            //    .Enrich.FromLogContext()
            //    .MinimumLevel.Information()
            //    .CreateLogger();

            //builder.Host.UseSerilog(log);

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<EnvanterAppDbContext>();
                var userManager = scope.ServiceProvider.GetService<UserManager<Employee>>();
                var roleManager = scope.ServiceProvider.GetService<RoleManager<AppRole>>();

                await EnvanterAppDbContextSeedData.SeedData(context, userManager, roleManager);
            }

            app.Run();
        }
    }
}
