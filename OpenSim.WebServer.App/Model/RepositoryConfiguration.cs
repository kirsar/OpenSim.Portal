using System;
using Microsoft.Extensions.DependencyInjection;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.App.Model
{
    public class Repository
    {
        private readonly IServiceProvider serviceProvider;

        public Repository(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Seed()
        {
            var user = new User
            {
                Name = "user",
                Description = "description",
                Login = "login",
                Password = "pwd"
            };

            var corporation = new User
            {
                Name = "Umbrella corp.",
                Description = "Huge vendor of maritime simulators",
                Login = "umbrella",
                Password = "umbrella"
            };

            var seaCurrent = new Simulation
            {
                Name = "Sea Current",
                Description = "Engine to provide sea current effects, applied to floating objects, " +
                              "such as drifting or" + DummyText,
                Author = corporation,
            };

            var simpleShip = new Simulation
            {
                Name = "Simple Ship",
                Description = "Simulation of simple ship without any mechanics or hydrodynamics. " +
                              "Can be used to emulate far distance traffic" + DummyText,
                Author = user,
                References = new [] {seaCurrent}
            };

            seaCurrent.Consumers = new[] { simpleShip };

            var dummy1 = new Simulation
            {
                Name = "Dummy Simulation 1",
                Description = "Simulation of something without anything. " +
                              "Can be used to emulate something" + DummyText,
                Author = user,
            };

            var dummy2 = new Simulation
            {
                Name = "Dummy Simulation 2",
                Description = "Simulation of something without anything. " +
                             "Can be used to emulate something" + DummyText,
                Author = user,
            };

            var dummy3 = new Simulation
            {
                Name = "Dummy Simulation 3",
                Description = "Simulation of something without anything. " +
                             "Can be used to emulate something" + DummyText,
                Author = user,
            };

            var experimentalBuoy = new Simulation
            {
                Name = "Hydrodynamic buoy",
                Description = "Experimental model of buoy with full hydrodynamics",
                Author = user,
            };

            var chart = new Presentation
            {
                Name = "Chart",
                Description = "Electronic chart in Mercator projection",
                Author = corporation,
                Simulations = new[] {simpleShip, seaCurrent}
            };

            var steeringPanel = new Presentation
            {
                Name = "Steering Device",
                Description = "Basic steering panel with throttle and rudder control",
                Author = user,
                Simulations = new[] { simpleShip }
            };

            simpleShip.Presentations = new[] { steeringPanel, chart };
            seaCurrent.Presentations = new[] { chart };

            var runningServer = new Server
            {
                Name = "Just some server",
                Description = "Server to have some data available to test portal front-end",
                IsRunning = true,
                Author = corporation,
                Simulations = new [] { simpleShip, seaCurrent, dummy1, dummy2, dummy3 },
                Presentations = new [] { steeringPanel, chart }
            };

            var stoppedServer = new Server
            {
                Name = "Another test server",
                Description = "One more entry to test portal front-end",
                Author = user,
                IsRunning = false,
                Simulations = new [] { experimentalBuoy }
            };

            var users = serviceProvider.GetService<IUserRepository>();
            var simulations = serviceProvider.GetService<ISimulationRepository>();
            var presentations = serviceProvider.GetService<IPresentationRepository>();
            var servers = serviceProvider.GetService<IServerRepository>();

            users.Add(user);

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

        private const string DummyText =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt " +
            "ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco " +
            "laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
            "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat " +
            "cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
    }
}
