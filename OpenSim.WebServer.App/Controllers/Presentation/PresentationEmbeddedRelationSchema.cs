using System.Linq;
using OpenSim.WebServer.Model;
using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    public class PresentationEmbeddedRelationSchema : ResourseEmbeddedRelationsSchema<PresentationResource, Presentation>
    {
        public PresentationEmbeddedRelationSchema()
        {
            RegisterEmbeddedRelation(LinkTemplates.Presentations.Author.Rel, (resource, model, relationName) => 
                resource.Author = new UserInfoResource(model.Author, LinkTemplates.Presentations.Author.Rel));

            RegisterEmbeddedRelation(LinkTemplates.Simulations.GetSimulations.Rel, (resource, model, relationName) =>
                resource.Simulations = new ResourceList<SimulationResource>(relationName, 
                    model.Simulations.Select(s => new SimulationResource(s, relationName))));
        }
    }
}