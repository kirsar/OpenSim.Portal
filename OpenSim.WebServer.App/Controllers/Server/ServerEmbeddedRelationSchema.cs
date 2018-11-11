using System.Linq;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    public class ServerEmbeddedRelationSchema : ResourseEmbeddedRelationsSchema<ServerResource, Server>
    {
        public ServerEmbeddedRelationSchema()
        {
            RegisterEmbeddedRelation("author",
                (resource, model) => resource.Author = new UserInfoResource(model.Author) { Rel = "author" });
            RegisterEmbeddedRelation("simulations", 
                (resource, model) => resource.Simulations = model.Simulations?.Select(s => new SimulationResource(s)).ToList());
            RegisterEmbeddedRelation("presentations", 
                (resource, model) => resource.Presentations = model.Presentations?.Select(p => new PresentationResource(p)).ToList());
        }
    }
}