using System.Collections.Generic;
using WebApi.Hal;

namespace OpenSim.Portal.Controllers.Presentation
{
    public class PresentationCollection : CollectionRepresentation<PresentationResource>
    {
        public PresentationCollection(IEnumerable<PresentationResource> presentations) : 
            base(LinkTemplates.Simulations.GetPresentations.Href, presentations)
        {
        }

        #region HAL

        public override string Href
        {
            get => LinkTemplates.Presentations.GetPresentations.Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            Links.Add(new Link { Href = Href, Rel = "self" });
        }

        #endregion
    }
}