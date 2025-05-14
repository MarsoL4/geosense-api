using GeoSense.API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GeoSense.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Garante que o appsettings.Development.json será lido corretamente
            var connectionString = builder.Configuration.GetConnectionString("Oracle");

            // Registra o DbContext com a conexão Oracle
            builder.Services.AddDbContext<MotoContext>(options =>
                options.UseOracle(connectionString)); // <- UseOracle, não UseSqlServer

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}