using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using OpenSim.WebServer.App.Controllers;

namespace OpenSim.WebServer.Controllers
{
    internal static class ResourceWithRelationsExtension
    {
        internal static T EmbedRelations<T>(this T resource, HttpRequest request, IEmbeddedRelationsSchema embeddedRelationsSchema) 
            where T : IResourceWithRelations
        {
            var fields = request.GetFieldsDefinition();
            var embeddedField = fields.GetEmbeddedField();
            if (embeddedField == null)
                return resource;

            resource.EmbedRelations(fields, embeddedRelationsSchema);

            return resource;
        }

        internal static List<T> EmbedRelations<T>(this List<T> resources, HttpRequest request, IEmbeddedRelationsSchema embeddedRelationsSchema) 
            where T : IResourceWithRelations
        {
            var fields = request.GetFieldsDefinition();
            var embeddedField = fields.FirstOrDefault().GetEmbeddedNode();
            if (embeddedField == null)
                return resources;

            foreach (var resource in resources)
                resource.EmbedRelations(embeddedField.Nodes, embeddedRelationsSchema);

            return resources;
        }

        private static IEnumerable<FieldsTreeNode> GetFieldsDefinition(this HttpRequest request) => 
            request.TryGetFields(out var fields) 
                ? fields.Values.Select(f => f.Parts).UnfoldFieldsTree() 
                : Enumerable.Empty<FieldsTreeNode>();
    }
}