using System;
using Microsoft.Extensions.DependencyInjection;
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
            serviceProvider.GetService<IServerRepository>().Seed();
        }
    }
}
