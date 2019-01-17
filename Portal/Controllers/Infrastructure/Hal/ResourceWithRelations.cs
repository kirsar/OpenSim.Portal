using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
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

        public abstract void EmbedRelations(FieldsTreeNode embeddedFieldNode, IEmbeddedRelationsSchema schema,
            UserManager<Model.User> userManager);

        protected internal void EmbedRelations(FieldsTreeNode embeddedFieldNode, IEmbeddedRelationsSchema schema, 
            ResourceEmbeddedRelationsSchema<TResource, TModel> resourceSchema, UserManager<Model.User> userManager)
        { 
            foreach (var relationNode in embeddedFieldNode.Nodes)
                EmbedRelation(relationNode, schema, resourceSchema, userManager);
        }

        private void EmbedRelation(FieldsTreeNode relationNode, IEmbeddedRelationsSchema schema, 
            ResourceEmbeddedRelationsSchema<TResource, TModel> resourceSchema, UserManager<Model.User> userManager)
        {
            var relation = resourceSchema[relationNode.Value]((TResource)this, model, relationNode.Value, userManager);
            if (relation == null)
                return;

            EmbedRelationsOfRelation(relation, relationNode, schema, userManager);
        }

        private static void EmbedRelationsOfRelation(object relation, FieldsTreeNode relationNode,
            IEmbeddedRelationsSchema schema, UserManager<Model.User> userManager)
        {
            var embeddedOfRelation = relationNode.Nodes.GetEmbeddedFieldNode();
            if (embeddedOfRelation == null)
                return;

            if (relation is IEnumerable<IResourceWithRelations> relations)
                foreach (var item in relations)
                    item.EmbedRelations(embeddedOfRelation, schema, userManager);
            else
                ((IResourceWithRelations) relation).EmbedRelations(embeddedOfRelation, schema, userManager);
        }
    }
}