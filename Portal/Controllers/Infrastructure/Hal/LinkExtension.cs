using System.Globalization;
using WebApi.Hal;

namespace OpenSim.Portal.Controllers
{
    public static class LinkExtension
    {
        public static int GetId(this Link link) =>
            int.Parse(link.Href.Substring(link.Rel.Length + 2), NumberStyles.Any, CultureInfo.InvariantCulture);
    }
}
