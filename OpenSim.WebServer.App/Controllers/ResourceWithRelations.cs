using System;
using System.Collections.Generic;
using System.Linq;
using OpenSim.WebServer.App.Controllers;
using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    public class ResourceWithRelations : Representation
    {
        private Dictionary<string, Func<object>> relationsFactory = new Dictionary<string, Func<object>>();

        protected void RegisterRelation(string relation, Func<object> registerAction) =>
            relationsFactory.Add(relation, registerAction);

        public void EmbedRelations(FieldsTreeNode embeddedField)
        {
            foreach (var relationNode in embeddedField.Nodes)
                EmbedRelation(relationNode);
        }

        private void EmbedRelation(FieldsTreeNode relationNode)
        {
            var relation = relationsFactory[relationNode.Value]();
            if (relation == null)
                return;

            EmbedRelationsOfRelation(relation, relationNode);
        }

        private static void EmbedRelationsOfRelation(object relation, FieldsTreeNode relationNode)
        {
            var embeddedOfRelation = relationNode.GetEmbeddedNode();
            if (embeddedOfRelation == null)
                return;

            if (relation is IEnumerable<ResourceWithRelations> relations)
                foreach (var item in relations)
                    item.EmbedRelations(embeddedOfRelation);
            else
                ((ResourceWithRelations) relation).EmbedRelations(embeddedOfRelation);
        }
    }
}