using System.Collections.Generic;
using OpenSim.Portal.Controllers.Simulation;
using OpenSim.Portal.Controllers.User;

namespace OpenSim.Portal.Controllers.Presentation
{
    public sealed class PresentationResource : ResourceWithRelations<PresentationResource, Model.Presentation>
    {
        private readonly Model.Presentation presentation;

        public PresentationResource(Model.Presentation presentation) : base(presentation)
        {
            this.presentation = presentation;
        }

        public PresentationResource(Model.Presentation presentation, string relationName) : this(presentation)
        {
            Rel = relationName;
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
                Links.Add(LinkTemplates.Presentations.GetSimulations.CreateLink(new { id = Id }));

            //if (presentation.Simulations != null)
            //    foreach (var simulation in presentation.Simulations)
            //         Links.Add(LinkTemplates.Presentations.GetSimulation.CreateLink(new { id = simulation.Id }));
        }

        #endregion
    }
}