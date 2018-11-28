using System.Linq;
using OpenSim.WebServer.Model;
using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    public class ServerEmbeddedRelationSchema : ResourseEmbeddedRelationsSchema<ServerResource, Server>
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