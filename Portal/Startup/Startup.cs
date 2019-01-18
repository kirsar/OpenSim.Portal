using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenSim.Portal.Controllers;
using OpenSim.Portal.Model;

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

            services.AddDbContext<PortalDbContext>(builder => 
                builder.UseSqlServer(Configuration["Data:ConnectionString"]));

            //services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("OpenSim.Portal"));
            services.AddDbContext<UserDbContext>(builder =>
                builder.UseSqlServer(Configuration["Identity:ConnectionString"]));
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions { HotModuleReplacement = true );
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.UseSession();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCors(cors => cors.WithOrigins(
                "http://localhost:4200",
                "http://localhost:5000")
                .AllowAnyHeader());
            app.UseMvc();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                spa.UseAngularCliServer(env.IsDevelopment() ? "start-dev" : "start-prod");
            });

            app.Seed();
            app.SeedContent();
        }
    }
}
