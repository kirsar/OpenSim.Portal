using System.Collections.Generic;
using WebApi.Hal;

namespace OpenSim.Portal.Controllers.Simulation
{
    public class SimulationCollection : CollectionRepresentation<SimulationResource>
    {
        public SimulationCollection(string resourceName, IEnumerable<SimulationResource> simulations) : 
            base(resourceName, simulations)
        {
        }

        #region HAL

        public override string Href
        {
            get => LinkTemplates.Simulations.Get.Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            Links.Add(new Link { Href = Href, Rel = "self" });
        }

        #endregion
    }
}