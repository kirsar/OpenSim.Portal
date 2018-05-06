using System.Collections.Generic;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    public class SimulationResource : DynamicRepresentation
    {
        private readonly Simulation simulation;

        public SimulationResource(Simulation simulation)
        {
            this.simulation = simulation;
        }

        public long Id => simulation.Id;
        public string Name => simulation.Name;
        public string Description => simulation.Description;
        public UserDetails Author => AreRelationsEmbedded ? simulation.Author.ToUserDetails() : null;
        public IEnumerable<Simulation> References => AreRelationsEmbedded ? simulation.References : null;
        public IEnumerable<Simulation> Consumers => AreRelationsEmbedded ? simulation.Consumers : null;
        public IEnumerable<Presentation> Presentations => AreRelationsEmbedded ? simulation.Presentations : null;

        #region HAL

        public override string Rel
        {
            get => LinkTemplates.Simulations.Simulation.Rel;
            set { }
        }

        public override string Href
        {
            get => LinkTemplates.Simulations.Simulation.CreateLink(new { id = Id }).Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            if (References != null)
                Links.Add(LinkTemplates.Simulations.References.CreateLink(new { id = Id }));
            if (Presentations != null)
                Links.Add(LinkTemplates.Simulations.Presentations.CreateLink(new { id = Id }));
       }

        #endregion
    }
}