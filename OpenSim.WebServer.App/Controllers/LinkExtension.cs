using System.Globalization;
using WebApi.Hal;

namespace OpenSim.WebServer.App.Controllers
{
    public static class LinkExtension
    {
        public static long GetId(this Link link) =>
            long.Parse(link.Href.Substring(link.Rel.Length + 2), NumberStyles.Any, CultureInfo.InvariantCulture);
    }
}
