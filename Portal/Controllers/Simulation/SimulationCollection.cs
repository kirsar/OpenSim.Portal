using System.Collections.Generic;
using WebApi.Hal;

namespace OpenSim.Portal.Controllers.Simulation
{
    public class SimulationCollection : SimpleListRepresentation<SimulationResource>
    {
        public SimulationCollection(IList<SimulationResource> simulations) : base(simulations)
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