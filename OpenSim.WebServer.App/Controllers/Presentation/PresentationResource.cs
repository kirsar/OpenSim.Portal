using System.Collections.Generic;
using OpenSim.WebServer.App.Controllers;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    public class PresentationResource : ResourceWithRelations<PresentationResource, Presentation>
    {
        private readonly Presentation presentation;

        public PresentationResource(Presentation presentation) : base(presentation)
        {
            this.presentation = presentation;
        }

        public long Id => presentation.Id;
        public string Name => presentation.Name;
        public string Description => presentation.Description;

        public UserInfoResource Author { get; set; }
        public IEnumerable<SimulationResource> Simulations { get; set; }

        public override void EmbedRelations(FieldsTreeNode embeddedFieldNode, IEmbeddedRelationsSchema schema) =>
            EmbedRelations(embeddedFieldNode, schema, schema.Presentation);
        
        #region HAL

        public override string Rel
        {
            get => LinkTemplates.Presentations.GetPresentation.Rel;
            set { }
        }

        public override string Href
        {
            get => LinkTemplates.Presentations.GetPresentation.CreateLink(new { id = Id }).Href;
            set { }
        }

        protected override void CreateHypermedia()
        {
            if (presentation.Simulations != null)
                foreach (var simulation in presentation.Simulations)
                     Links.Add(LinkTemplates.Simulations.GetSimulation.CreateLink(new { id = simulation.Id }));
        }

        #endregion
    }
}