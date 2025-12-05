using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace EnvanterApp.WebAPI.Extensions
{
    public static class SerilogExtensions
    {
        public static IHostBuilder AddSerilogLogging(this IHostBuilder host, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt")
                .WriteTo.MSSqlServer(
                    connectionString: configuration.GetConnectionString("DefaultConnection"),
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        AutoCreateSqlTable = true
                    })
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .CreateLogger();

            return host.UseSerilog(logger);
        }
    }
}
