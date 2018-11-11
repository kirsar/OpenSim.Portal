using System.Linq;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    public class PresentationEmbeddedRelationSchema : ResourseEmbeddedRelationsSchema<PresentationResource, Presentation>
    {
        public PresentationEmbeddedRelationSchema()
        {
            RegisterEmbeddedRelation("author", (resource, model) => resource.Author = new UserInfoResource(model.Author) { Rel = "author" });
            RegisterEmbeddedRelation("simulations", (resource, model) => resource.Simulations = model.Simulations?.Select(s => new SimulationResource(s)).ToList());
        }
    }
}