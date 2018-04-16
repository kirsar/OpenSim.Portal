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