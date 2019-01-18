using System.Linq;
using OpenSim.Portal.Controllers.Presentation;
using OpenSim.Portal.Controllers.User;
using WebApi.Hal;

namespace OpenSim.Portal.Controllers.Simulation
{
    public class SimulationEmbeddedRelationSchema : ResourceEmbeddedRelationsSchema<SimulationResource, Model.Simulation>
    {
        public SimulationEmbeddedRelationSchema()
        {
            RegisterEmbeddedRelation(LinkTemplates.Simulations.Author.Rel, (resource, model, relationName, userManager) => 
                resource.Author = new UserInfoResource(userManager.Users.Single(u => u.Id == model.AuthorId), relationName));

            RegisterEmbeddedRelation(LinkTemplates.Simulations.GetReferences.Rel, (resource, model, relationName) => 
                resource.References = new ResourceList<SimulationResource>(relationName, 
                    model.References.Select(s => new SimulationResource(s, relationName))));

            RegisterEmbeddedRelation(LinkTemplates.Simulations.GetConsumers.Rel, (resource, model, relationName) => 
                resource.Consumers = new ResourceList<SimulationResource>(relationName, 
                    model.Consumers.Select(s => new SimulationResource(s, relationName))));

            RegisterEmbeddedRelation(LinkTemplates.Simulations.GetPresentations.Rel, (resource, model, relationName) => 
                resource.Presentations = new ResourceList<PresentationResource>(relationName, 
                    model.Presentations.Select(p => new PresentationResource(p, relationName))));
        }
    }
}