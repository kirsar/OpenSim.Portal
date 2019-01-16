using System.Collections.Generic;
using WebApi.Hal;

namespace OpenSim.Portal.Controllers.Simulation
{
    public class SimulationCollection : CollectionRepresentation<SimulationResource>
    {
        public SimulationCollection(IEnumerable<SimulationResource> simulations) : 
            base(LinkTemplates.Simulations.GetSimulations.Href, simulations)
        {
        }

        #region HAL

        public override string Href
        {
            get => LinkTemplates.Simulations.GetSimulations.Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            Links.Add(new Link { Href = Href, Rel = "self" });
        }

        #endregion
    }
}