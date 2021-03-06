﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using OpenSim.Portal.Controllers.Presentation;
using OpenSim.Portal.Controllers.Simulation;
using OpenSim.Portal.Controllers.User;

namespace OpenSim.Portal.Controllers.Server
{
    public class ServerResource : ResourceWithRelations<ServerResource, Model.Server>
    {
        private readonly Model.Server server;
     
        public ServerResource() : base(null)
        {
        }

        public ServerResource(Model.Server server) : base(server)
        {
            this.server = server;

            Id = server.Id;
            Name = server.Name;
            Description = server.Description;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
      
        public UserInfoResource Author { get; set; }
        public List<SimulationResource> Simulations { get; set; }
        public List<PresentationResource> Presentations { get; set; }
    
        public override void EmbedRelations(
            FieldsTreeNode embeddedFieldNode, 
            IEmbeddedRelationsSchema schema,
            UserManager<Model.User> userManager) =>
            EmbedRelations(embeddedFieldNode, schema, schema.Server, userManager);

        #region HAL

        public override string Rel
        {
            get => LinkTemplates.Servers.GetItem.Rel;
            set { }
        }

        public override string Href
        {
            get => LinkTemplates.Servers.GetItem.CreateLink(new {id = Id}).Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            Links.Add(LinkTemplates.Servers.Author.CreateLink(new { id = server.AuthorId} ));

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
