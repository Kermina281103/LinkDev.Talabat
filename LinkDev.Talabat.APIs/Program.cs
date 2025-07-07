
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs
{
    public class Program
    {


        // [FromServices]
        //public static StoreDbContext dbContext { get; set; } = null!;

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services 
            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            // builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddPersistenceService(builder.Configuration);
            
            #endregion

            var app = builder.Build();

            #region DataBase Initialzation 
            await app.InitializeStoreContextAsync();
            #endregion


            #region Configure Kestrel MiddleWare 

            {// Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    // app.MapOpenApi();
                    app.UseSwagger();
                    app.UseSwaggerUI();

                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseAuthorization();
                app.MapControllers();
                #endregion

                app.Run();
            }
        }
    }
}