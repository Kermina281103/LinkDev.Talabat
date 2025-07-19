
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LinkDev.Talabat.Application;
using LinkDev.Talabat.APIs.Controllers.Errors;
using Microsoft.Extensions.Options;

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

            builder.Services.AddControllers()
                .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly)
                .ConfigureApiBehaviorOptions(options =>
                {
                options.SuppressModelStateInvalidFilter = false;
                    options.InvalidModelStateResponseFactory = (actionContext) =>
                    {
                        var errors = actionContext.ModelState.Where(p => p.Value!.Errors.Count > 0)
                              .Select(p => new ApiValidationErrorResponse.ValidationError()
                              {
                                  Field = p.Key,
                                  Errors = p.Value!.Errors.Select(E => E.ErrorMessage)
                              }
                            );


                        return new BadRequestObjectResult(new ApiValidationErrorResponse()
                        {
                            Errors = errors
                        });
                    };
                });
            ///or 
           
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = false;
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value!.Errors.Count > 0)
                               .Select(p => new ApiValidationErrorResponse.ValidationError()
                               {
                                   Field = p.Key,
                                   Errors = p.Value!.Errors.Select(E => E.ErrorMessage)
                               }
                );

                    return new BadRequestObjectResult(new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    });
                };
            });
            
            
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            // builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddPersistenceService(builder.Configuration);
            builder.Services.AddApplicationServices();
            
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
                app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();
                #endregion

                app.Run();
            }
        }
    }
}