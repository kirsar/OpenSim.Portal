using System.Linq;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    public class SimulationEmbeddedRelationSchema : ResourseEmbeddedRelationsSchema<SimulationResource, Simulation>
    {
        public SimulationEmbeddedRelationSchema()
        {
            RegisterEmbeddedRelation("author", (resource, model) => resource.Author = new UserInfoResource(model.Author)
                { Rel = "author" });
            RegisterEmbeddedRelation("references", (resource, model) => resource.References = model.References?.Select(s => new SimulationResource(s)
                { Rel = LinkTemplates.Simulations.GetReference.Rel }).ToList());
            RegisterEmbeddedRelation("consumers", (resource, model) => resource.Consumers = model.Consumers?.Select(s => new SimulationResource(s)
                { Rel = LinkTemplates.Simulations.GetConsumer.Rel }).ToList());
            RegisterEmbeddedRelation("presentations", (resource, model) => resource.Presentations = model.Presentations?.Select(p => new PresentationResource(p)).ToList());
        }
    }
}