using System.Collections.Generic;
using System.Linq;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    public class PresentationResource : ResourceWithRelations
    {
        private readonly Presentation presentation;

        public PresentationResource(Presentation presentation)
        {
            this.presentation = presentation;

            RegisterRelation("author", () => Author = new UserInfoResource(presentation.Author) { Rel = "author" });
            RegisterRelation("simulations", () => Simulations = presentation.Simulations?.Select(s => new SimulationResource(s)));
        }

        public long Id => presentation.Id;
        public string Name => presentation.Name;
        public string Description => presentation.Description;

        public UserInfoResource Author { get; private set; }
        public IEnumerable<SimulationResource> Simulations { get; private set; }

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