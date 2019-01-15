using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OpenSim.Portal.Model.Presentation;
using OpenSim.Portal.Model.Server;
using OpenSim.Portal.Model.Simulation;

namespace OpenSim.Portal.Model
{
    public static class Seed
    {
        private const string User1 = "user";
        private const string User1Password = "User123$";
        private const string User1Description = "Regular user";

        private const string User2 = "UmbrellaCorp";
        private const string User2Password = "Umbrella123$";
        private const string User2Description = "Huge vendor of maritime simulators";

        private const string UserRole = "user";

        public static async void SeedIdentity(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var provider = scope.ServiceProvider;
                
                var userManager = provider.GetService<UserManager<User.User>>();
                var roleManager = provider.GetService<RoleManager<IdentityRole<long>>>();

                await roleManager.CreateAsync(new IdentityRole<long>(UserRole));

                var user1 = new User.User(User1, User1Description);
                ThrowIfError(await userManager.CreateAsync(user1, User1Password));
                ThrowIfError(await userManager.AddToRoleAsync(user1, UserRole));

                var user2 = new User.User(User2, User2Description);
                ThrowIfError(await userManager.CreateAsync(user2, User2Password));
                ThrowIfError(await userManager.AddToRoleAsync(user2, UserRole));
            }
        }

        private static void ThrowIfError(IdentityResult identityResult)
        {
            if (!identityResult.Succeeded)
                throw new InvalidOperationException(identityResult.Errors
                    .Aggregate(string.Empty, (r, e) => r += $"{e.Description}. "));
        }

        public static async void SeedContent(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                var userManager = services.GetService<UserManager<User.User>>();

                var user = await userManager.FindByNameAsync(User1);
                var corporation = await userManager.FindByNameAsync(User2);

                var seaCurrent = new Simulation.Simulation
                {
                    Name = "Sea Current",
                    Description = "Engine to provide sea current effects, applied to floating objects, " +
                                  "such as drifting or" + DummyText,
                    Author = corporation,
                };

                var simpleShip = new Simulation.Simulation
                {
                    Name = "Simple Ship",
                    Description = "Simulation of simple ship without any mechanics or hydrodynamics. " +
                                  "Can be used to emulate far distance traffic" + DummyText,
                    Author = user,
                    References = new[] {seaCurrent}
                };

                seaCurrent.Consumers = new[] {simpleShip};

                var dummy1 = new Simulation.Simulation
                {
                    Name = "Dummy Simulation 1",
                    Description = "Simulation of something without anything. " +
                                  "Can be used to emulate something" + DummyText,
                    Author = user,
                };

                var dummy2 = new Simulation.Simulation
                {
                    Name = "Dummy Simulation 2",
                    Description = "Simulation of something without anything. " +
                                  "Can be used to emulate something" + DummyText,
                    Author = user,
                };

                var dummy3 = new Simulation.Simulation
                {
                    Name = "Dummy Simulation 3",
                    Description = "Simulation of something without anything. " +
                                  "Can be used to emulate something" + DummyText,
                    Author = user,
                };

                var experimentalBuoy = new Simulation.Simulation
                {
                    Name = "Hydrodynamic buoy",
                    Description = "Experimental model of buoy with full hydrodynamics",
                    Author = user,
                };

                var chart = new Presentation.Presentation
                {
                    Name = "Chart",
                    Description = "Electronic chart in Mercator projection",
                    Author = corporation,
                    Simulations = new[] {simpleShip, seaCurrent}
                };

                var steeringPanel = new Presentation.Presentation
                {
                    Name = "Steering Device",
                    Description = "Basic steering panel with throttle and rudder control",
                    Author = user,
                    Simulations = new[] {simpleShip}
                };

                simpleShip.Presentations = new[] {steeringPanel, chart};
                seaCurrent.Presentations = new[] {chart};

                var runningServer = new Server.Server
                {
                    Name = "Just some server",
                    Description = "Server to have some data available to test portal front-end",
                    IsRunning = true,
                    Author = corporation,
                    Simulations = new[] {simpleShip, seaCurrent, dummy1, dummy2, dummy3},
                    Presentations = new[] {steeringPanel, chart}
                };

                var stoppedServer = new Server.Server
                {
                    Name = "Another test server",
                    Description = "One more entry to test portal front-end",
                    Author = user,
                    IsRunning = false,
                    Simulations = new[] {experimentalBuoy}
                };

                var simulations = services.GetService<ISimulationRepository>();
                var presentations = services.GetService<IPresentationRepository>();
                var servers = services.GetService<IServerRepository>();

                simulations.Add(simpleShip);
                simulations.Add(seaCurrent);
                simulations.Add(experimentalBuoy);
                simulations.Add(dummy1);
                simulations.Add(dummy2);
                simulations.Add(dummy3);

                presentations.Add(chart);
                presentations.Add(steeringPanel);

                servers.Add(runningServer);
                servers.Add(stoppedServer);
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
