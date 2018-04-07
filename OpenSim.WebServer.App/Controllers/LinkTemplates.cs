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
            public static Link GetServers { get { return new Link("server", "~/server"); } }

            /// <summary>
            /// /servers/{id}
            /// </summary>
            public static Link Server { get { return new Link("server", "~/server/{id}"); } }

            /// <summary>
            /// /servers/{id}/simulations
            /// </summary>
            public static Link Simulations { get { return new Link("server", "~/server/{id}/simulations"); } }

            /// <summary>
            /// /servers/{id}/presentations
            /// </summary>
            public static Link Presentations { get { return new Link("server", "~/server/{id}/presentations"); } }
        }
    }
}
