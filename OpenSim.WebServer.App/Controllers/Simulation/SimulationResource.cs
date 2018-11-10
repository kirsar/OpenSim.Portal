using System;
using System.Collections.Generic;
using System.Linq;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    public class SimulationResource : ResourceWithRelations
    {
        private readonly Simulation simulation;

        public SimulationResource(Simulation simulation)
        {
            this.simulation = simulation;

            RegisterRelation("author", () => Author = new UserInfoResource(simulation.Author)
                { Rel = "author" });
            RegisterRelation("references", () => References = simulation.References?.Select(s => new SimulationResource(s)
                { Rel = LinkTemplates.Simulations.GetReference.Rel }));
            RegisterRelation("consumers", () => Consumers = simulation.Consumers?.Select(s => new SimulationResource(s)
                { Rel = LinkTemplates.Simulations.GetConsumer.Rel }));
            RegisterRelation("presentations", () => Presentations = simulation.Presentations?.Select(p => new PresentationResource(p)));
        }

        public long Id => simulation.Id;
        public string Name => simulation.Name;
        public string Description => simulation.Description;

        public UserInfoResource Author { get; private set; }
        public IEnumerable<SimulationResource> References { get; private set; }
        public IEnumerable<SimulationResource> Consumers { get; private set; }
        public IEnumerable<PresentationResource> Presentations { get; private set; }
        
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