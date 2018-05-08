using System.Collections.Generic;
using System.Linq;
using OpenSim.WebServer.Model;
using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    public class ServerResource : Representation
    {
        private readonly Server server;
      
        public ServerResource(Server server)
        {
            this.server = server;
        }

        public long Id => server.Id;
        public string Name => server.Name;
        public string Description => server.Description;
        public bool IsRunning => server.IsRunning;
        public bool IsCustomUiAvailable => server.IsCustomUiAvailable;

        public UserInfoResource Author { get; set; }
        public IEnumerable<SimulationResource> Simulations { get; set; } 
        public IEnumerable<PresentationResource> Presentations { get; set; }
      
        #region HAL

        public override string Rel
        {
            get => LinkTemplates.Servers.Server.Rel;
            set { }
        }

        public override string Href
        {
            get => LinkTemplates.Servers.Server.CreateLink(new {id = Id}).Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            if (Simulations != null)
                Links.Add(LinkTemplates.Servers.Simulations.CreateLink(new { id = Id }));
            if (Presentations != null)
                Links.Add(LinkTemplates.Servers.Presentations.CreateLink(new { id = Id }));
        }

        #endregion
    }
}
