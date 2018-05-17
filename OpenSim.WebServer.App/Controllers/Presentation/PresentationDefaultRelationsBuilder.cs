using System.Linq;
using Microsoft.AspNetCore.Http;
using OpenSim.WebServer.App.Controllers;
using OpenSim.WebServer.Model;

namespace OpenSim.WebServer.Controllers
{
    // TODO: in future we will have dynamically embedded relations 
    // for now we embedding relations manually
    internal static class PresentationDefaultRelationsBuilder
    {
        public static PresentationResource EmbedRelations(this PresentationResource presentationResource, Presentation presentation, HttpRequest request)
        {
            if (!request.HasFieldsQuery())
                return presentationResource;

            presentationResource.Author = new UserInfoResource(presentation.Author)
            {
                Rel = "author"
            };

            presentationResource.Simulations = presentation.Simulations?.Select(s => new SimulationResource(s)
            {
                Author = new UserInfoResource(s.Author)
                {
                    Rel = "author"
                }
            });

            return presentationResource;
        }
    }
}