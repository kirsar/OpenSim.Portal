using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenSim.Portal.Controllers;
using OpenSim.Portal.Model;
using OpenSim.Portal.Model.Presentation;
using OpenSim.Portal.Model.Server;
using OpenSim.Portal.Model.Simulation;
using OpenSim.Portal.Model.User;

namespace OpenSim.Portal.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

            services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("OpenSim.Portal"));

            services.AddIdentity<User, IdentityRole<long>>()
                .AddEntityFrameworkStores<UserDbContext>();
            
            services.AddSingleton<IServerRepository, ServerRepository>();
            services.AddSingleton<ISimulationRepository, SimulationRepository>();
            services.AddSingleton<IPresentationRepository, PresentationRepository>();

            services.AddSingleton<IEmbeddedRelationsSchema, EmbeddedRelationsSchema>();

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                //{
                //    HotModuleReplacement = true
                //});
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseSession();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCors(cors => cors.WithOrigins("http://localhost:4200").AllowAnyHeader());
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");

                //routes.MapSpaFallbackRoute(
                //    name: "spa-fallback",
                //    defaults: new { controller = "Home", action = "Index" });
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                spa.UseAngularCliServer(env.IsDevelopment() ? "start-dev" : "start-prod");
            });

            app.SeedIdentity();
            app.SeedContent();
        }
    }
}
