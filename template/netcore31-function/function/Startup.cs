using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace function
{
    public class Startup
    {
        #region Public Methods

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FunctionHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FunctionHandler function)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("/", async context =>
                {
                    await Invoke(function, context);
                });
            });
        }

        #endregion
        
        #region Private Methods

        private static async Task Invoke(FunctionHandler function, HttpContext context)
        {
            var funcHandler = function.GetType().GetMethod("Handle");

            HttpMethodAttribute attr = (HttpMethodAttribute)funcHandler.GetCustomAttributes(
                    typeof(HttpMethodAttribute), true).FirstOrDefault();

            string method = attr?.HttpMethods.FirstOrDefault() ?? "Post";

            if (string.Compare(method, context.Request.Method, true) != 0)
            {
                context.Response.StatusCode = 405;
                await context.Response.WriteAsync($"405 - {context.Request.Method} method allowed");
                return;
            }

            try
            {
                var (status, text) = await function.Handle(context.Request);

                context.Response.StatusCode = status;

                if (!string.IsNullOrEmpty(text))
                {
                    if (context.Request.ContentType.Contains("application/json"))
                    {
                        context.Response.Headers["Content-Type"] = "application/json";
                    }
                    else
                    {
                        context.Response.Headers["Content-Type"] = "plain/text";
                    }

                    await context.Response.WriteAsync(text);
                }
            }
            catch (NotImplementedException nie)
            {
                context.Response.StatusCode = 501;
                await context.Response.WriteAsync(nie.ToString());
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(ex.ToString());
            }
        }

        #endregion
    }
}
