using Microsoft.AspNetCore.Identity;
using OpenSim.Portal.Controllers.Presentation;
using OpenSim.Portal.Controllers.User;
using WebApi.Hal;

namespace OpenSim.Portal.Controllers.Simulation
{
    public sealed class SimulationResource : ResourceWithRelations<SimulationResource, Model.Simulation>
    {
        private readonly Model.Simulation simulation;

        public SimulationResource(Model.Simulation simulation) : base(simulation)
        {
            this.simulation = simulation;
        }

        public SimulationResource(Model.Simulation simulation, string relationName) : this(simulation)
        {
            Rel = relationName;
        }

        public long Id => simulation.Id;
        public string Name => simulation.Name;
        public string Description => simulation.Description;

        public UserInfoResource Author { get; set; }
        public ResourceList<SimulationResource> References { get; set; }
        public ResourceList<SimulationResource> Consumers { get; set; }
        public ResourceList<PresentationResource> Presentations { get; set; }

        public override void EmbedRelations(FieldsTreeNode embeddedFieldNode, IEmbeddedRelationsSchema schema,
            UserManager<Model.User> userManager) =>
            EmbedRelations(embeddedFieldNode, schema, schema.Simulation, userManager);
     
        #region HAL

        public override string Rel { get; set; } = LinkTemplates.Simulations.GetItem.Rel;
        
        public override string Href
        {
            get => LinkTemplates.Simulations.GetItem.CreateLink(new { id = Id }).Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            //if (simulation.References != null)
            //    foreach (var reference in simulation.References)
            //        Links.Add(LinkTemplates.Simulations.GetReference.CreateLink(new { id = reference.Id }));

            //if (simulation.References != null)
                Links.Add(LinkTemplates.Simulations.GetReferences.CreateLink(new { id = Id }));

            //if (simulation.Consumers != null)
            //    foreach (var consumer in simulation.Consumers)
            //        Links.Add(LinkTemplates.Simulations.GetConsumer.CreateLink(new { id = consumer.Id }));

            //if (simulation.Consumers != null)
                Links.Add(LinkTemplates.Simulations.GetConsumers.CreateLink(new { id = Id }));

            //if (simulation.Presentations != null)
            //    foreach (var presentation in simulation.Presentations)
            //        Links.Add(LinkTemplates.Presentations.GetPresentation.CreateLink(new { id = presentation.Id }));

            if (simulation.Presentations != null)
                Links.Add(LinkTemplates.Simulations.GetPresentations.CreateLink(new { id = Id }));
        }

        #endregion
    }
}