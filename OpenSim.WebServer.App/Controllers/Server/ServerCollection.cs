using System.Linq;
using System.Collections.Generic;
using OpenSim.WebServer.Model;
using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    public class ServerCollection : SimpleListRepresentation<ServerResource>
    {
        public ServerCollection(IList<ServerResource> servers) : base(servers)
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