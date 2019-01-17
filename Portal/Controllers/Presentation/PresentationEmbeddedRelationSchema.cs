using System.Linq;
using OpenSim.Portal.Controllers.Simulation;
using OpenSim.Portal.Controllers.User;
using WebApi.Hal;

namespace OpenSim.Portal.Controllers.Presentation
{
    public class PresentationEmbeddedRelationSchema : ResourceEmbeddedRelationsSchema<PresentationResource, Model.Presentation>
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