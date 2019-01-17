using System.Linq;
using OpenSim.Portal.Controllers.Presentation;
using OpenSim.Portal.Controllers.Simulation;
using OpenSim.Portal.Controllers.User;
using WebApi.Hal;

namespace OpenSim.Portal.Controllers.Server
{
    public class ServerEmbeddedRelationSchema : ResourceEmbeddedRelationsSchema<ServerResource, Model.Server>
    {
        public ServerEmbeddedRelationSchema()
        {
            RegisterEmbeddedRelation(LinkTemplates.Servers.Author.Rel, (resource, model, relationName) =>
                resource.Author = new UserInfoResource(model.Author, relationName));

            RegisterEmbeddedRelation(LinkTemplates.Servers.GetSimulations.Rel, (resource, model, relationName) =>
                resource.Simulations = new ResourceList<SimulationResource>(relationName,
                    model.Simulations.Select(s => new SimulationResource(s, relationName))));

            RegisterEmbeddedRelation(LinkTemplates.Servers.GetPresentations.Rel, (resource, model, relationName) =>
                resource.Presentations = new ResourceList<PresentationResource>(relationName,
                    model.Presentations.Select(p => new PresentationResource(p, relationName))));
        }
    }
}