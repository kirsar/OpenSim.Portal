using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using OpenSim.WebServer.App.Controllers;

namespace OpenSim.WebServer.Controllers
{
    internal static class ResourceRelationsExtension
    {
        public static T EmbedRelations<T>(this T resource, HttpRequest request) where T : ResourceWithRelations
        {
            var embeddedNode = request.GetEmbeddedNode();
            if (embeddedNode == null)
                return resource;

            resource.EmbedRelations(embeddedNode);

            return resource;
        }

        public static IEnumerable<T> EmbedRelations<T>(this IEnumerable<T> resources, HttpRequest request) where T : ResourceWithRelations
        {
            var embeddedNode = request.GetEmbeddedNode();
            if (embeddedNode == null)
                return resources;

            foreach (var resource in resources)
                resource.EmbedRelations(embeddedNode);

            return resources;
        }

        private static FieldsTreeNode GetEmbeddedNode(this HttpRequest request)
        {
            if (!request.TryGetFields(out var fields))
                return null;

            var fieldsTree = fields.Values.Select(f => f.Parts).UnfoldFieldsTree();
            var resourceNode = fieldsTree.Nodes.Single();
            return resourceNode.GetEmbeddedNode();
        }
    }
}