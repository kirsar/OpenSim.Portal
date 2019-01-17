using System.Collections.Generic;
using WebApi.Hal;

namespace OpenSim.Portal.Controllers.Server
{
    public class ServerCollection : CollectionRepresentation<ServerResource>
    {
        public ServerCollection(string resourceName, IEnumerable<ServerResource> servers) : 
            base(resourceName, servers)
        {
        }

        #region HAL

        public override string Href
        {
            get => LinkTemplates.Servers.Get.Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            Links.Add(new Link { Href = Href, Rel = "self" });
        }

        #endregion
    }
}