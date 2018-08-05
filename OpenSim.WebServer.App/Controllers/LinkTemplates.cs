using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    public static class LinkTemplates
    {
        public static class Servers
        {
            public static Link GetServers => new Link("servers", "~/servers");
            public static Link GetServer => new Link("servers", "~/servers/{id}");
        }

        public static class Simulations
        {
            public static Link GetSimulations => new Link("simulations", "~/simulations");
            public static Link GetSimulation => new Link("simulations", "~/simulations/{id}");
            public static Link GetReference => new Link("references", "~/simulations/{id}");
            public static Link GetConsumer => new Link("consumers", "~/simulations/{id}");
        }

        public static class Presentations
        {
            public static Link GetPresentations => new Link("presentations", "~/presentations");
            public static Link GetPresentation => new Link("presentations", "~/presentations/{id}");
        }

        public static class Users
        {
            public static Link User => new Link("users", "~/users/{id}");
        }
    }
}
