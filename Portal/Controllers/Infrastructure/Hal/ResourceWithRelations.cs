using System.Collections.Generic;
using WebApi.Hal;

namespace OpenSim.Portal.Controllers
{

    public abstract class ResourceWithRelations<TResource, TModel> : Representation, IResourceWithRelations
        where TResource : ResourceWithRelations<TResource, TModel>
    {
        private readonly TModel model;
     
        protected ResourceWithRelations(TModel model)
        {
            this.model = model;
        }

        public abstract void EmbedRelations(FieldsTreeNode embeddedFieldNode, IEmbeddedRelationsSchema schema);

        protected internal void EmbedRelations(FieldsTreeNode embeddedFieldNode, IEmbeddedRelationsSchema schema, 
            ResourceEmbeddedRelationsSchema<TResource, TModel> resourceSchema)
        {
            foreach (var relationNode in embeddedFieldNode.Nodes)
                EmbedRelation(relationNode, schema, resourceSchema);
        }

        private void EmbedRelation(FieldsTreeNode relationNode, IEmbeddedRelationsSchema schema, 
            ResourceEmbeddedRelationsSchema<TResource, TModel> resourceSchema)
        {
            var relation = resourceSchema[relationNode.Value]((TResource)this, model, relationNode.Value);
            if (relation == null)
                return;

            EmbedRelationsOfRelation(relation, relationNode, schema);
        }

        private static void EmbedRelationsOfRelation(object relation, FieldsTreeNode relationNode, IEmbeddedRelationsSchema schema)
        {
            var embeddedOfRelation = relationNode.Nodes.GetEmbeddedFieldNode();
            if (embeddedOfRelation == null)
                return;

            if (relation is IEnumerable<IResourceWithRelations> relations)
                foreach (var item in relations)
                    item.EmbedRelations(embeddedOfRelation, schema);
            else
                ((IResourceWithRelations) relation).EmbedRelations(embeddedOfRelation, schema);
        }
    }
}