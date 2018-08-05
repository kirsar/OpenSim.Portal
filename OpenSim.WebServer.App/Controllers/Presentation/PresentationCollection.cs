using System.Collections.Generic;
using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    public class PresentationCollection : SimpleListRepresentation<PresentationResource>
    {
        public PresentationCollection(IList<PresentationResource> presentations) : base(presentations)
        {
        }

        #region HAL

        public override string Href
        {
            get => LinkTemplates.Presentations.GetPresentation.Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            Links.Add(new Link { Href = Href, Rel = "self" });
        }

        #endregion
    }
}