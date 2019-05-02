using System.Diagnostics;
using FluentValidation.AspNetCore;
using Flash.Trades.Domain.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flash.Trades.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Dependency Injection
            services.AddDependencies(Configuration);

            // CORS
            services.AddCors(o => o.AddDefaultPolicy(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));

            services.AddMvc()
                .AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<TradeValidator>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment() || Debugger.IsAttached)
                app.UseDeveloperExceptionPage();
            
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseMvc();
        }
    }
}