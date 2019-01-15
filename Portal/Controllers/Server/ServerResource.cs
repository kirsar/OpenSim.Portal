using System.Collections.Generic;
using OpenSim.Portal.Controllers.Presentation;
using OpenSim.Portal.Controllers.Simulation;
using OpenSim.Portal.Controllers.User;

namespace OpenSim.Portal.Controllers.Server
{
    public class ServerResource : ResourceWithRelations<ServerResource, Model.Server.Server>
    {
        private readonly Model.Server.Server server;
     
        public ServerResource() : base(null)
        {
        }

        public ServerResource(Model.Server.Server server) : base(server)
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
        public List<SimulationResource> Simulations { get; set; }
        public List<PresentationResource> Presentations { get; set; }
    
        public override void EmbedRelations(FieldsTreeNode embeddedFieldNode, IEmbeddedRelationsSchema schema) =>
            EmbedRelations(embeddedFieldNode, schema, schema.Server);

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
            Links.Add(LinkTemplates.Servers.Author.CreateLink(new { id = server.Author.Id} ));

            if (server.Simulations != null)
                Links.Add(LinkTemplates.Servers.GetSimulations.CreateLink(new { id = Id }));

            //if (server.Simulations != null)
            //    foreach (var simulation in server.Simulations)
            //        Links.Add(LinkTemplates.Simulations.GetSimulation.CreateLink(new { id = simulation.Id }));

            if (server.Presentations != null)
                Links.Add(LinkTemplates.Servers.GetPresentations.CreateLink(new { id = Id }));

            //if (server.Presentations != null)
            //    foreach (var presentation in server.Presentations)
            //        Links.Add(LinkTemplates.Presentations.GetPresentation.CreateLink(new { id = presentation.Id }));
        }

        #endregion
    }
}
