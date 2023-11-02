using App.Api.Common;
using App.Core.Domain.Entities;
using App.Infrastructure.Data;
using App.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR;

using MongoDB.Driver;
using Serilog;
using System.Reflection;
using App.Api.Hubs;
using Elastic.CommonSchema;

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
            builder.Services.AddTransient(typeof(IMongoRepository<App.Core.Domain.Entities.User>), typeof(MongoRepository<App.Core.Domain.Entities.User>));

            builder.Services.AddSignalR();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder => builder
                    .WithOrigins("http://localhost:3000") 
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            var app = builder.Build();
            app.MapHub<CallsHub>("/CallsHub");
            app.UseCors("AllowOrigin");


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