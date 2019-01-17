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
            RegisterEmbeddedRelation(LinkTemplates.Presentations.Author.Rel, (resource, model, relationName, userManager) => 
                resource.Author = new UserInfoResource(userManager.Users.Single(u => u.Id == model.AuthorId), relationName));

            RegisterEmbeddedRelation(LinkTemplates.Simulations.Get.Rel, (resource, model, relationName) =>
                resource.Simulations = new ResourceList<SimulationResource>(relationName, 
                    model.Simulations.Select(s => new SimulationResource(s, relationName))));
        }
    }
}