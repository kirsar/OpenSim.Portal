using System.Collections.Generic;
using WebApi.Hal;

namespace OpenSim.Portal.Controllers.Presentation
{
    public class PresentationCollection : CollectionRepresentation<PresentationResource>
    {
        public PresentationCollection(string resourceName, IEnumerable<PresentationResource> presentations) : 
            base(resourceName, presentations)
        {
        }

        #region HAL

        public override string Href
        {
            get => LinkTemplates.Presentations.Get.Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            Links.Add(new Link { Href = Href, Rel = "self" });
        }

        #endregion
    }
}