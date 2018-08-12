using System.Collections.Generic;
using OpenSim.WebServer.Model;
using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    public class SimulationResource : Representation
    {
        private readonly Simulation simulation;

        public SimulationResource(Simulation simulation)
        {
            this.simulation = simulation;
        }

        public long Id => simulation.Id;
        public string Name => simulation.Name;
        public string Description => simulation.Description;

        public UserInfoResource Author { get; set; }
        public IEnumerable<SimulationResource> References { get; set; }
        public IEnumerable<SimulationResource> Consumers { get; set; }
        public IEnumerable<PresentationResource> Presentations { get; set; }

        #region HAL

        public override string Rel { get; set; } = LinkTemplates.Simulations.GetSimulation.Rel;
        
        public override string Href
        {
            get => LinkTemplates.Simulations.GetSimulation.CreateLink(new { id = Id }).Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            //if (simulation.References != null)
            //    foreach (var reference in simulation.References)
            //        Links.Add(LinkTemplates.Simulations.GetReference.CreateLink(new { id = reference.Id }));

            if (simulation.References != null)
                Links.Add(LinkTemplates.Simulations.GetReferences.CreateLink(new { id = Id }));

            //if (simulation.Consumers != null)
            //    foreach (var consumer in simulation.Consumers)
            //        Links.Add(LinkTemplates.Simulations.GetConsumer.CreateLink(new { id = consumer.Id }));

            if (simulation.Consumers != null)
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