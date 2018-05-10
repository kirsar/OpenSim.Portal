using System.Linq;
using Microsoft.AspNetCore.Http;
using OpenSim.WebServer.App.Controllers;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    // TODO: in future we will have dynamically embedded relations 
    // for now we embedding relations manually
    internal static class ServerDefaultRelationsBuilder
    {
        public static ServerResource EmbedRelations(this ServerResource serverResource, HttpRequest request, Server server)
        {
            if (!request.HasFieldsQuery())
                return serverResource;

            serverResource.Author = new UserInfoResource(server.Author)
            {
                Rel = "author"
            };
            serverResource.Simulations = server.Simulations.Select(s => new SimulationResource(s));
            serverResource.Presentations = server.Presentations?.Select(p => new PresentationResource(p));

            return serverResource;
        }
    }
}