using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenSim.Portal.Controllers;
using OpenSim.Portal.Model;

namespace OpenSim.Portal.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(options => options.OutputFormatters.RemoveType<JsonOutputFormatter>())
                .AddJsonHalFormatterServices();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist/out-tsc";
            });

            //services.AddMemoryCache();
            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = "OpenSim.Portal";
            //    options.Cookie.Expiration = TimeSpan.FromDays(7);
            //    options.Cookie.HttpOnly = false;
            //});

            services.AddContentDatabase(Configuration, Env);
            services.AddIdentityDatabase(Configuration, Env);

            services.AddIdentity<User, IdentityRole<long>>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IServerRepository, ServerRepository>();
            services.AddTransient<ISimulationRepository, SimulationRepository>();
            services.AddTransient<IPresentationRepository, PresentationRepository>();

            services.AddSingleton<IEmbeddedRelationsSchema, EmbeddedRelationsSchema>();

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddCors(options => options.AddPolicy("CORS", builder => builder
                .WithOrigins(
                    "http://localhost:3000",
                    "http://localhost:4200",
                    "http://localhost:5000",
                    "http://opensim-portal.westeurope.cloudapp.azure.com")
                .AllowAnyHeader()
                .AllowAnyMethod()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Env.IsDevelopment())
            {
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions { HotModuleReplacement = true );
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.UseSession();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseCors("CORS"); 
            app.UseMvc();

            if (Env.IsDevelopment())
            {
                app.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "ClientApp";
                    spa.UseAngularCliServer("start-dev");
                });
            }

            app.Seed();
            app.SeedContent(true);
        }
    }
}
