using System.Collections.Generic;
using WebApi.Hal;
using WebApi.Hal.Interfaces;

namespace OpenSim.Portal.Controllers
{
    public class CollectionRepresentation<TResource> : Representation where TResource : IResource
    {
        protected CollectionRepresentation(string resourceName)
        {
            ResourceList = new ResourceList<TResource>(resourceName);
        }

        protected CollectionRepresentation(string resourceName, IEnumerable<TResource> list)
        {
            ResourceList = new ResourceList<TResource>(resourceName, list);
        }

        public IList<TResource> ResourceList { get; set; }
    }
}