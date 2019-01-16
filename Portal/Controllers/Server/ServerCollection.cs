using System.Collections.Generic;
using WebApi.Hal;

namespace OpenSim.Portal.Controllers.Server
{
    public class ServerCollection : CollectionRepresentation<ServerResource>
    {
        public ServerCollection(IEnumerable<ServerResource> servers) : 
            base(LinkTemplates.Servers.GetServers.Href, servers)
        {
        }

        #region HAL

        public override string Href
        {
            get => LinkTemplates.Servers.GetServers.Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            Links.Add(new Link { Href = Href, Rel = "self" });
        }

        #endregion
    }
}