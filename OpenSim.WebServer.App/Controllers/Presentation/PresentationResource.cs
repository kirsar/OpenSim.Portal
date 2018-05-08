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
        public IEnumerable<Simulation> Simulations { get; set; }

        #region HAL

        public override string Rel
        {
            get => LinkTemplates.Presentations.Presentation.Rel;
            set { }
        }

        public override string Href
        {
            get => LinkTemplates.Presentations.Presentation.CreateLink(new { id = Id }).Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            if (Simulations != null)
                Links.Add(LinkTemplates.Presentations.Simulations.CreateLink(new { id = Id }));
        }

        #endregion
    }
}