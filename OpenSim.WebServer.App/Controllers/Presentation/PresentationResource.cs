using System.Collections.Generic;
using OpenSim.WebServer.Model;
using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    public class PresentationResource : Representation
    {
        private readonly Presentation presentation;

        public PresentationResource(Presentation presentation)
        {
            this.presentation = presentation;
        }

        public long Id => presentation.Id;
        public string Name => presentation.Name;
        public string Description => presentation.Description;

        public UserInfoResource Author { get; set; }
        public IEnumerable<SimulationResource> Simulations { get; set; }

        #region HAL

        public override string Rel
        {
            get => LinkTemplates.Presentations.GetPresentation.Rel;
            set { }
        }

        public override string Href
        {
            get => LinkTemplates.Presentations.GetPresentation.CreateLink(new { id = Id }).Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            if (presentation.Simulations != null)
                foreach (var simulation in presentation.Simulations)
                     Links.Add(LinkTemplates.Simulations.GetSimulation.CreateLink(new { id = simulation.Id }));
        }

        #endregion
    }
}