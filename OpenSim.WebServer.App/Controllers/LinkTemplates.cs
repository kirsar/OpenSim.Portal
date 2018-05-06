using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    public static class LinkTemplates
    {
        public static class Servers
        {
            public static Link GetServers => new Link("servers", "~/servers");
            public static Link Server => new Link("servers", "~/servers/{id}");
            public static Link Simulations => new Link("simulations", "~/servers/{id}/simulations");
            public static Link Presentations => new Link("presentations", "~/servers/{id}/presentations");
        }

        public static class Simulations
        {
            public static Link GetSimulations => new Link("simulations", "~/servers");
            public static Link Simulation => new Link("simulations", "~/simulations/{id}");
            public static Link References => new Link("simulations", "~/simulations/{id}/references");
            public static Link Presentations => new Link("presentations", "~/simulations/{id}/presentations");
        }

        public static class Presentations
        {
            public static Link GetPresentations => new Link("presentations", "~/presentations");
            public static Link Presentation => new Link("presentations", "~/presentations/{id}");
            public static Link Simulations => new Link("presentations", "~/presentations/{id}/simulations");
        }

        public static class Users
        {
            public static Link User => new Link("users", "~/servers/{id}");
        }
    }
}
