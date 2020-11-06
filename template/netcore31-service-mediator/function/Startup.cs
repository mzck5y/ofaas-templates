using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Handlers;

namespace Service
{
    public class Startup
    {
        #region Properties

        public IConfiguration Configuration { get; }
            
        #endregion

        #region Constructors

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
   
        #endregion

        #region Public Methods

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
               .AddNewtonsoftJson()
               .AddFluentValidation(cfg =>
               {
                   cfg.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
               });

            // MediatR Related
            services.AddMediatR(typeof(SampleRequest));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerPipelineBehavior<,>));
           

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncomment this if you whant to enable HTTPS
            // app.UseHttpsRedirection();

            app.UseCustomExceptionHandler(); // For handling validation errors and unhablded exceptions

            app.UseRouting();
            
            // Uncoment this if you whant to add authentication
            // app.UseAuthentication();

            // uncomment this if you whant to add authorization 
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/hc");
                endpoints.MapControllers();
            });
        }

        #endregion
    }
}
