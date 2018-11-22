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

        public abstract void EmbedRelations(IEnumerable<FieldsTreeNode> fields, IEmbeddedRelationsSchema schema);

        protected internal void EmbedRelations(IEnumerable<FieldsTreeNode> fields, IEmbeddedRelationsSchema schema, 
            ResourseEmbeddedRelationsSchema<TResource, TModel> resourseSchema)
        {
            foreach (var relationNode in fields)
                EmbedRelation(relationNode, schema, resourseSchema);
        }

        private void EmbedRelation(FieldsTreeNode relationNode, IEmbeddedRelationsSchema schema, 
            ResourseEmbeddedRelationsSchema<TResource, TModel> resourceSchema)
        {
            var relation = resourceSchema[relationNode.Value]((TResource)this, model);
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
                    item.EmbedRelations(embeddedOfRelation.Nodes, schema);
            else
                ((IResourceWithRelations) relation).EmbedRelations(embeddedOfRelation.Nodes, schema);
        }
    }
}