using System.Collections.Generic;
using OpenSim.WebServer.Model;
using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    public class ServerResource : Representation
    {
        private readonly Server server;

        public ServerResource()
        {
        }

        public ServerResource(Server server)
        {
            this.server = server;

            Id = server.Id;
            Name = server.Name;
            Description = server.Description;
            IsRunning = server.IsRunning;
            IsCustomUiAvailable = server.IsCustomUiAvailable;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsRunning { get; set; }
        public bool IsCustomUiAvailable { get; set; }

        public UserInfoResource Author { get; set; }
        public IEnumerable<SimulationResource> Simulations { get; set; } 
        public IEnumerable<PresentationResource> Presentations { get; set; }
      
        #region HAL

        public override string Rel
        {
            get => LinkTemplates.Servers.GetServer.Rel;
            set { }
        }

        public override string Href
        {
            get => LinkTemplates.Servers.GetServer.CreateLink(new {id = Id}).Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            if (server.Simulations != null)
                foreach (var simulation in server.Simulations)
                    Links.Add(LinkTemplates.Simulations.GetSimulation.CreateLink(new { id = simulation.Id }));

            if (server.Presentations != null)
                foreach (var presentation in server.Presentations)
                    Links.Add(LinkTemplates.Presentations.GetPresentation.CreateLink(new { id = presentation.Id }));
        }

        #endregion
    }
}
