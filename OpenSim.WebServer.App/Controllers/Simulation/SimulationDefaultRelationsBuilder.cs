using System.Linq;
using Microsoft.AspNetCore.Http;
using OpenSim.WebServer.App.Controllers;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    // TODO: in future we will have dynamically embedded relations 
    // for now we embedding relations manually
    internal static class SimulationDefaultRelationsBuilder
    {
        public static SimulationResource EmbedRelations(this SimulationResource simulationResource, Simulation simulation, HttpRequest request)
        {
            if (!request.HasFieldsQuery())
                return simulationResource;

            simulationResource.Author = new UserInfoResource(simulation.Author)
            {
                Rel = "author"
            };

            simulationResource.References = simulation.References?.Select(s => new SimulationResource(s)
            {
                Rel = LinkTemplates.Simulations.GetReference.Rel
            });

            simulationResource.Consumers = simulation.Consumers?.Select(s => new SimulationResource(s)
            {
                Rel = LinkTemplates.Simulations.GetConsumer.Rel
            });

            simulationResource.Presentations = simulation.Presentations?.Select(p => new PresentationResource(p));

            return simulationResource;
        }
    }
}