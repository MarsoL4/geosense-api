using GeoSense.API.AutoMapper;
using GeoSense.API.Infrastructure.Contexts;
using GeoSense.API.Services;
using GeoSense.Infrastructure.Repositories;
using GeoSense.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GeoSense.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IMotoRepository, MotoRepository>();
            builder.Services.AddScoped<MotoService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var connectionString = builder.Configuration.GetConnectionString("Oracle");
            builder.Services.AddDbContext<GeoSenseContext>(options =>
                options.UseOracle(connectionString));

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            builder.Services.AddEndpointsApiExplorer();

            // Configuração Swagger personalizada
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);

                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "GeoSense API",
                    Version = "v1",
                    Description = "API RESTful para gerenciamento de motos, vagas e pátios.\nEndpoints CRUD, paginação, HATEOAS e exemplos de payload."
                });
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}