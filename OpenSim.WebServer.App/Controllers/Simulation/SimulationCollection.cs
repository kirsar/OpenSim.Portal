using System.Linq;
using System.Collections.Generic;
using OpenSim.WebServer.Model;
using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
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