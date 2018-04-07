using System;
using Microsoft.Data.Edm.Csdl;
using Microsoft.Extensions.DependencyInjection;
using OpenSim.WebServer.App.Controllers.Presentation;
using OpenSim.WebServer.App.Controllers.Server;
using OpenSim.WebServer.App.Controllers.Simulation;
using OpenSim.WebServer.App.Controllers.User;

namespace OpenSim.WebServer.App.Controllers
{
    public class RepositoryConfiguration
    {
        private readonly IServiceProvider serviceProvider;

        public RepositoryConfiguration(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Seed()
        {
            var user = new User.User
            {
                Name = "user",
                Description = "description",
                Login = "login",
                Password = "pwd"
            };

            var seaCurrent = new Simulation.Simulation
            {
                Name = "Sea Current",
                Description = "Engine to provide sea current effects, applied to floating objects, " +
                              "such as drifting or" + DummyText,
                Author = user
            };

            var simpleShip = new Simulation.Simulation
            {
                Name = "Simple Ship",
                Description = "Simulation of simple ship without any mechanics or hydrodynamics. " +
                              "Can be used to emulate far distance traffic" + DummyText,
                Author = user,
                SupportedSimulations = new [] {seaCurrent}
            };

            var experinemtalBuoy = new Simulation.Simulation
            {
                Name = "Hydrodynamic buoy",
                Description = "Experimental model of buoy with full hydrodynamics",
                Author = user,
            };

            var chart = new Presentation.Presentation
            {
                Name = "Chart",
                Description = "Electronic chart in Mercator projection",
                Author = user,
                SupportedBy = new[] {simpleShip, seaCurrent}
            };

            var steeringPanel = new Presentation.Presentation
            {
                Name = "Steering Device",
                Description = "Basic steering panel with throttle and rudder control",
                Author = user,
                SupportedBy = new[] { simpleShip }
            };

            var runningServer = new Server.Server
            {
                Name = "Just some server",
                Description = "Server to have some data available to test portal front-end",
                Author = user,
                Simulations = new [] { simpleShip, seaCurrent },
                Presentations = new [] { steeringPanel, chart }
            };
            var stoppedServer = new Server.Server
            {
                Name = "Another test serfer",
                Description = "One more entry to test portal front-end",
                Simulations = new []{ experinemtalBuoy }
            };

            var users = serviceProvider.GetService<IUserRepository>();
            var simulations = serviceProvider.GetService<ISimulationRepository>();
            var presentations = serviceProvider.GetService<IPresentationRepository>();
            var servers = serviceProvider.GetService<IServerRepository>();

            users.Add(user);

            simulations.Add(simpleShip);
            simulations.Add(seaCurrent);
            simulations.Add(experinemtalBuoy);

            presentations.Add(chart);
            presentations.Add(steeringPanel);

            servers.Add(runningServer);
            servers.Add(stoppedServer);
        }

        private const string DummyText =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt " +
            "ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco " +
            "laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
            "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat " +
            "cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
    }
}
