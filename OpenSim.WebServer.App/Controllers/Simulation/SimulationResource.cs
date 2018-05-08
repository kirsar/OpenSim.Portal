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
        public IEnumerable<Simulation> Consumers { get; set; }
        public IEnumerable<Presentation> Presentations { get; set; }

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