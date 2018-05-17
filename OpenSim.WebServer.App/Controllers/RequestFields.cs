using Microsoft.AspNetCore.Http;

namespace OpenSim.WebServer.App.Controllers
{
    public static class RequestFields
    {
        public static bool HasFieldsQuery(this HttpRequest request) =>
            request.Query.ContainsKey("fields");
    }
}
