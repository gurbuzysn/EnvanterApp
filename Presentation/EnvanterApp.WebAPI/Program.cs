using EnvanterApp.Application;
using EnvanterApp.Infrastructure;
using EnvanterApp.Persistence;
using EnvanterApp.WebAPI.Extensions;

namespace EnvanterApp.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.Host.AddSerilogLogging(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddCustomCors();
            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.Services.AddMinioClient(builder.Configuration);
            builder.Services.AddCustomControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            await app.SeedDataAsync();
            app.Run();
        }
    }
}
