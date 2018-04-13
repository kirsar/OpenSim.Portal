using System.Collections;
using System.Collections.Generic;
using WebApi.Hal;

namespace OpenSim.WebServer.App.Controllers.Server
{
    public class ServerCollection : SimpleListRepresentation<Server>
    {
        public ServerCollection(IList<Server> servers) : base(servers)
        {
        }

        protected override void CreateHypermedia()
        {
            Href = LinkTemplates.Servers.GetServers.Href;

            Links.Add(new Link { Href = Href, Rel = "self" });
        }
    }
}