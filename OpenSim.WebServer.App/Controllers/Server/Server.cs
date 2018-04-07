using System.Collections.Generic;
using WebApi.Hal;

namespace OpenSim.WebServer.App.Controllers.Server
{
    public class Server : Representation
    {
        public Server()
        {
            Rel = LinkTemplates.Servers.Server.Rel;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsRunning { get; set; }
        public string Description { get; set; }
        public User.User Author { get; set; }
        public IEnumerable<Simulation.Simulation> Simulations { get; set; }
        public IEnumerable<Presentation.Presentation> Presentations { get; set; }
        public bool IsCustomUiAvailable { get; set; }

        protected override void CreateHypermedia()
        {
            Href = LinkTemplates.Servers.Server.CreateLink(new { id = Id }).Href;

            if (Simulations != null)
                Links.Add(LinkTemplates.Servers.Simulations.CreateLink(new { id = Id }));
            if (Presentations != null)
                Links.Add(LinkTemplates.Servers.Presentations.CreateLink(new { id = Id }));
        }
    }
}
