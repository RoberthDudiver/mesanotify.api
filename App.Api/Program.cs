using App.Api.Application.UseCase.V1.PermissionOperation.Command.Create;
using App.Api.Common;
using App.Core.Domain.Entities;
using App.Infrastructure.Data;
using App.Infrastructure.Repositories;
using Elastic.CommonSchema;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Serilog;
using System.Reflection;

namespace App.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(builder.Configuration.GetConnectionString("MongoDBConnection")));
            builder.Services.AddScoped<IMongoDatabase>(sp => sp.GetRequiredService<IMongoClient>().GetDatabase("CallRest"));


            builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);
            builder.Services.AddTransient(typeof(IRepository<Permission>), typeof(Repository<Permission>));

            builder.Services.AddTransient(typeof(IMongoRepository<Restaurant>), typeof(MongoRepository<Restaurant>));
            builder.Services.AddTransient(typeof(IMongoRepository<Calls>), typeof(MongoRepository<Calls>));



            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or initializing the database.");
                }
            }


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();

            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}