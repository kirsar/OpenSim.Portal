using WebApi.Hal;

namespace OpenSim.WebServer.App.Controllers
{
    public static class LinkTemplates
    {
        public static class Servers
        {
            /// <summary>
            /// /servers
            /// </summary>
            public static Link GetServers => new Link("servers", "~/servers");

            /// <summary>
            /// /servers/{id}
            /// </summary>
            public static Link Server => new Link("servers", "~/servers/{id}");

            /// <summary>
            /// /servers/{id}/simulations
            /// </summary>
            public static Link Simulations => new Link("simulations", "~/servers/{id}/simulations");

            /// <summary>
            /// /servers/{id}/presentations
            /// </summary>
            public static Link Presentations => new Link("presentations", "~/servers/{id}/presentations");
        }
    }
}
