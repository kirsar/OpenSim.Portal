using System.Collections.Generic;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    public class PresentationResource : DynamicRepresentation
    {
        private Presentation presentation;

        public PresentationResource(Presentation presentation)
        {
            this.presentation = presentation;
        }

        public long Id => presentation.Id;
        public string Name => presentation.Name;
        public string Description => presentation.Description;
        public UserDetails Author => AreRelationsEmbedded ? presentation.Author.ToUserDetails() : null;
        public IEnumerable<Simulation> Simulations => AreRelationsEmbedded ? presentation.Simulations : null;

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