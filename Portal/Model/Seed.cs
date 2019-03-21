using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OpenSim.Portal.Model
{
    public static class Seed
    {
        public static async void SeedContent(this IApplicationBuilder app, bool hasTestContent)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetService<PortalDbContext>();
                context.Database.Migrate();

                if (!hasTestContent || context.Servers.Any())
                    return;

                var userManager = services.GetService<UserManager<User>>();
                var user = await userManager.FindByNameAsync(UserSeed.User1);
                var corporation = await userManager.FindByNameAsync(UserSeed.User2);

                // presentations
                var chart = new Presentation
                {
                    Name = "Chart",
                    Description = "Electronic chart in Mercator projection",
                    AuthorId = corporation.Id,
                };

                var steeringPanel = new Presentation
                {
                    Name = "Steering Device",
                    Description = "Basic steering panel with throttle and rudder control",
                    AuthorId = user.Id,
                };

                // simulations
                var seaCurrent = new Simulation
                {
                    Name = "Sea Current",
                    Description = "Engine to provide sea current effects, applied to floating objects, " +
                                  "such as drifting or" + DummyText,
                    AuthorId = corporation.Id,
                };
                seaCurrent.AddPresentation(chart);

                var simpleShip = new Simulation
                {
                    Name = "Simple Ship",
                    Description = "Simulation of simple ship without any mechanics or hydrodynamics. " +
                                  "Can be used to emulate far distance traffic" + DummyText,
                    AuthorId = user.Id,
                };

                simpleShip.AddReference(seaCurrent);
                
                simpleShip.AddPresentation(chart);
                simpleShip.AddPresentation(steeringPanel);

                var dummy1 = new Simulation
                {
                    Name = "Dummy Simulation 1",
                    Description = "Simulation of something without anything. " +
                                  "Can be used to emulate something" + DummyText,
                    AuthorId = user.Id,
                };

                var dummy2 = new Simulation
                {
                    Name = "Dummy Simulation 2",
                    Description = "Simulation of something without anything. " +
                                  "Can be used to emulate something" + DummyText,
                    AuthorId = user.Id,
                };

                var dummy3 = new Simulation
                {
                    Name = "Dummy Simulation 3",
                    Description = "Simulation of something without anything. " +
                                  "Can be used to emulate something" + DummyText,
                    AuthorId = user.Id,
                };

                var experimentalBuoy = new Simulation
                {
                    Name = "Hydrodynamic buoy",
                    Description = "Experimental model of buoy with full hydrodynamics",
                    AuthorId = user.Id,
                };

                // servers
                var server1 = new Server
                {
                    Name = "Just some server",
                    Description = "Server to have some data available to test portal front-end",
                    AuthorId = corporation.Id,
                };

                server1.AddSimulation(simpleShip);
                server1.AddSimulation(seaCurrent);
                server1.AddSimulation(dummy1);
                server1.AddSimulation(dummy2);
                server1.AddSimulation(dummy3);
                        
                server1.AddPresentation(chart);
                server1.AddPresentation(steeringPanel);

                var server2 = new Server
                {
                    Name = "Another test server",
                    Description = "One more entry to test portal front-end",
                    AuthorId = user.Id,
                };

                server2.AddSimulation(experimentalBuoy);

                context.Simulations.Add(simpleShip);
                context.Simulations.Add(seaCurrent);
                context.Simulations.Add(experimentalBuoy);
                context.Simulations.Add(dummy1);
                context.Simulations.Add(dummy2);
                context.Simulations.Add(dummy3);

                context.Presentations.Add(chart);
                context.Presentations.Add(steeringPanel);

                context.Servers.Add(server1);
                context.Servers.Add(server2);

                context.SaveChanges();
            }
        }

        private const string DummyText =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt " +
            "ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco " +
            "laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
            "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat " +
            "cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
    }
}
