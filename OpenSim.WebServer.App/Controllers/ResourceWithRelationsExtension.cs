using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using OpenSim.WebServer.App.Controllers;
using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    internal static class ResourceWithRelationsExtension
    {
        internal static T EmbedRelations<T>(this T resource, HttpRequest request, IEmbeddedRelationsSchema embeddedRelationsSchema) 
            where T : IResourceWithRelations
        {
            var fields = request.GetFieldsDefinition();
            var embeddedField = fields.GetEmbeddedFieldNode();
            if (embeddedField == null)
                return resource;

            resource.EmbedRelations(embeddedField, embeddedRelationsSchema);

            return resource;
        }

        internal static TCollection EmbedRelations<TCollection, TResource>(this TCollection collection, 
            HttpRequest request, IEmbeddedRelationsSchema embeddedRelationsSchema) 
            where TCollection : SimpleListRepresentation<TResource>
            where TResource : IResourceWithRelations
        {
            var fields = request.GetFieldsDefinition();
            var collectionNode = fields.FirstOrDefault()?.Nodes.Single();
            var embeddedField = collectionNode?.Nodes.GetEmbeddedFieldNode();
            if (embeddedField == null)
                return collection;

            foreach (var resource in collection.ResourceList)
                resource.EmbedRelations(embeddedField, embeddedRelationsSchema);

            return collection;
        }

        #region Inference Helpers

        internal static ServerCollection EmbedRelations(this ServerCollection collection,
            HttpRequest request, IEmbeddedRelationsSchema schema) =>
                EmbedRelations<ServerCollection, ServerResource>(collection, request, schema);

        internal static SimulationCollection EmbedRelations(this SimulationCollection collection,
            HttpRequest request, IEmbeddedRelationsSchema schema) =>
                EmbedRelations<SimulationCollection, SimulationResource>(collection, request, schema);

        internal static PresentationCollection EmbedRelations(this PresentationCollection collection,
            HttpRequest request, IEmbeddedRelationsSchema schema) =>
                EmbedRelations<PresentationCollection, PresentationResource>(collection, request, schema);

        #endregion

        private static IEnumerable<FieldsTreeNode> GetFieldsDefinition(this HttpRequest request) => 
            request.TryGetFields(out var fields) 
                ? fields.Values.Select(f => f.Parts).UnfoldFieldsTree() 
                : Enumerable.Empty<FieldsTreeNode>();
    }
}