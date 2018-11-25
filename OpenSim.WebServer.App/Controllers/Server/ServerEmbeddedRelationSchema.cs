using System.Linq;
using OpenSim.WebServer.Model;
using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    public class ServerEmbeddedRelationSchema : ResourseEmbeddedRelationsSchema<ServerResource, Server>
    {
        public ServerEmbeddedRelationSchema()
        {
            RegisterEmbeddedRelation(LinkTemplates.Servers.Author.Rel,
                (resource, model) => resource.Author = new UserInfoResource(model.Author) { Rel = LinkTemplates.Servers.Author.Rel });

            RegisterEmbeddedRelation(LinkTemplates.Servers.GetSimulations.Rel, 
                (resource, model) => resource.Simulations =
                    new ResourceList<SimulationResource>(LinkTemplates.Servers.GetSimulations.Rel, 
                        model.Simulations?.Select(s => new SimulationResource(s)) ?? Enumerable.Empty<SimulationResource>()));

            RegisterEmbeddedRelation(LinkTemplates.Servers.GetPresentations.Rel, 
                (resource, model) => resource.Presentations = 
                    new ResourceList<PresentationResource>(LinkTemplates.Servers.GetPresentations.Rel, 
                        model.Presentations?.Select(p => new PresentationResource(p)) ?? Enumerable.Empty<PresentationResource>()));
        }
    }
}