using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OpenSim.Portal.Controllers.Presentation;
using OpenSim.Portal.Controllers.Server;
using OpenSim.Portal.Controllers.Simulation;

namespace OpenSim.Portal.Controllers
{
    internal static class ResourceWithRelationsExtension
    {
        internal static T EmbedRelations<T>(this T resource, HttpRequest request,
            IEmbeddedRelationsSchema embeddedRelationsSchema, UserManager<Model.User> userManager) 
            where T : IResourceWithRelations
        {
            var fields = request.GetFieldsDefinition();
            var embeddedField = fields.GetEmbeddedFieldNode();
            if (embeddedField == null)
                return resource;

            resource.EmbedRelations(embeddedField, embeddedRelationsSchema, userManager);

            return resource;
        }

        internal static TCollection EmbedRelations<TCollection, TResource>(this TCollection collection,
            HttpRequest request, IEmbeddedRelationsSchema embeddedRelationsSchema, UserManager<Model.User> userManager) 
            where TCollection : CollectionRepresentation<TResource>
            where TResource : IResourceWithRelations
        {
            var fields = request.GetFieldsDefinition();
            var collectionNode = fields.FirstOrDefault()?.Nodes.Single();
            var embeddedField = collectionNode?.Nodes.GetEmbeddedFieldNode();
            if (embeddedField == null)
                return collection;

            foreach (var resource in collection.ResourceList)
                resource.EmbedRelations(embeddedField, embeddedRelationsSchema, userManager);

            return collection;
        }

        #region Inference Helpers

        internal static ServerCollection EmbedRelations(this ServerCollection collection,
            HttpRequest request, IEmbeddedRelationsSchema schema, UserManager<Model.User> userManager) =>
                EmbedRelations<ServerCollection, ServerResource>(collection, request, schema, userManager);

        internal static SimulationCollection EmbedRelations(this SimulationCollection collection,
            HttpRequest request, IEmbeddedRelationsSchema schema, UserManager<Model.User> userManager) =>
                EmbedRelations<SimulationCollection, SimulationResource>(collection, request, schema, userManager);

        internal static PresentationCollection EmbedRelations(this PresentationCollection collection,
            HttpRequest request, IEmbeddedRelationsSchema schema, UserManager<Model.User> userManager) =>
                EmbedRelations<PresentationCollection, PresentationResource>(collection, request, schema, userManager);

        #endregion
    }
}