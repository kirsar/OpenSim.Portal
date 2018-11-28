using System.Linq;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    public class PresentationEmbeddedRelationSchema : ResourseEmbeddedRelationsSchema<PresentationResource, Presentation>
    {
        public PresentationEmbeddedRelationSchema()
        {
            RegisterEmbeddedRelation(LinkTemplates.Presentations.Author.Rel,
                (resource, model) => resource.Author =
                    new UserInfoResource(model.Author, LinkTemplates.Presentations.Author.Rel));

            RegisterEmbeddedRelation(LinkTemplates.Presentations.GetSimulations.Rel, (resource, model) =>
                resource.Simulations = model.Simulations
                    ?.Select(s => new SimulationResource(s, LinkTemplates.Presentations.GetSimulations.Rel)).ToList());
        }
    }
}