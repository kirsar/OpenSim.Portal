using System.Linq;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    public class SimulationEmbeddedRelationSchema : ResourseEmbeddedRelationsSchema<SimulationResource, Simulation>
    {
        public SimulationEmbeddedRelationSchema()
        {
            RegisterEmbeddedRelation(LinkTemplates.Simulations.Author.Rel, (resource, model) => 
                resource.Author = new UserInfoResource(model.Author, LinkTemplates.Simulations.Author.Rel));

            RegisterEmbeddedRelation(LinkTemplates.Simulations.GetReferences.Rel, (resource, model) => 
                resource.References = model.References?.Select(s => new SimulationResource(s, LinkTemplates.Simulations.GetReference.Rel)).ToList());

            RegisterEmbeddedRelation(LinkTemplates.Simulations.GetConsumers.Rel, (resource, model) => 
                resource.Consumers = model.Consumers?.Select(s => new SimulationResource(s, LinkTemplates.Simulations.GetConsumers.Rel)).ToList());

            RegisterEmbeddedRelation(LinkTemplates.Simulations.GetPresentations.Rel, (resource, model) => 
                resource.Presentations = model.Presentations?.Select(p => new PresentationResource(p, LinkTemplates.Simulations.GetPresentations.Rel)).ToList());
        }
    }
}