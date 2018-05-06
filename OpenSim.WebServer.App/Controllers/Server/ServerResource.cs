using System.Collections.Generic;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    public class ServerResource : DynamicRepresentation
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

        public UserDetails Author => /*AreRelationsEmbedded ? */server.Author.ToUserDetails()/* : null*/;
        public IEnumerable<Simulation> Simulations => AreRelationsEmbedded ? server.Simulations : null;
        public IEnumerable<Presentation> Presentations => AreRelationsEmbedded ? server.Presentations : null;
      
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
