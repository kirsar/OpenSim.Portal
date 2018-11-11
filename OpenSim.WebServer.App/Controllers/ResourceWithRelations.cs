using System.Collections.Generic;
using OpenSim.WebServer.App.Controllers;
using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
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
            ResourseEmbeddedRelationsSchema<TResource, TModel> resourseSchema)
        {
            foreach (var relationNode in embeddedFieldNode.Nodes)
                EmbedRelation(relationNode, schema, resourseSchema);
        }

        private void EmbedRelation(FieldsTreeNode relationNode, IEmbeddedRelationsSchema schema, 
            ResourseEmbeddedRelationsSchema<TResource, TModel> resourseSchema)
        {
            var relation = resourseSchema[relationNode.Value]((TResource)this, model);
            if (relation == null)
                return;

            EmbedRelationsOfRelation(relation, relationNode, schema);
        }

        private static void EmbedRelationsOfRelation(object relation, FieldsTreeNode relationNode, IEmbeddedRelationsSchema schema)
        {
            var embeddedOfRelation = relationNode.GetEmbeddedNode();
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