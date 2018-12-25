using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace OpenSim.Portal.Startup
{
    public static class MvcJsonHalMediaExtensions
    {
        public static IMvcBuilder AddJsonHalFormatterServices(this IMvcBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<MvcOptions>, MvcJsonHalMediaSetup>());
            return builder;
        }
    }
}
