using WebApi.Hal;

namespace OpenSim.Portal.Controllers
{
    public static class LinkTemplates
    {
        public static class Servers
        {
            public static Link Get => new Link("servers", "~/servers");
            public static Link GetItem => new Link("servers", "~/servers/{id}");

            public static Link Author => new Link("author", "~/users/{id}");
            public static Link GetSimulations => new Link("simulations", "~/servers/{id}/simulations");
            public static Link GetPresentations => new Link("presentations", "~/servers/{id}/presentations");
        }

        public static class Simulations
        {
            public static Link Get => new Link("simulations", "~/simulations");
            public static Link GetItem => new Link("simulations", "~/simulations/{id}");

            public static Link Author => new Link("author", "~/users/{id}");
            public static Link GetReferences => new Link("references", "~/simulations/{id}/references");
            public static Link GetConsumers => new Link("consumers", "~/simulations/{id}/consumers");
            public static Link GetPresentations => new Link("presentations", "~/simulations/{id}/presentations");
        }

        public static class Presentations
        {
            public static Link Get => new Link("presentations", "~/presentations");
            public static Link GetItem => new Link("presentations", "~/presentations/{id}");

            public static Link Author => new Link("author", "~/users/{id}");
            public static Link GetSimulations => new Link("simulations", "~/presentations/{id}/simulations");
        }

        public static class Users
        {
            public static Link GetItem => new Link("users", "~/users/{id}");
        }
    }
}
