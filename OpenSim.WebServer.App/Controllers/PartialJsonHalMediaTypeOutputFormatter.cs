using System;
using System.Buffers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using PartialResponse.AspNetCore.Mvc.Formatters;
using WebApi.Hal.JsonConverters;
using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    public class PartialJsonHalMediaTypeOutputFormatter : PartialJsonOutputFormatter
    {
        private const string _mediaTypeHeaderValueName = "application/hal+json";

        private readonly LinksConverter linksConverter = new LinksConverter();
        private readonly ResourceConverter resourceConverter;
        private readonly EmbeddedResourceConverter embeddedResourceConverter = new EmbeddedResourceConverter();

        public PartialJsonHalMediaTypeOutputFormatter(
            JsonSerializerSettings serializerSettings, 
            ArrayPool<char> charPool, 
            bool ignoreCase, 
            IHypermediaResolver hypermediaResolver) : 
            base(serializerSettings, charPool, ignoreCase)
        {
            if (hypermediaResolver == null)
            {
                throw new ArgumentNullException(nameof(hypermediaResolver));
            }

            resourceConverter = new ResourceConverter(hypermediaResolver);
            Initialize();
        }

        public PartialJsonHalMediaTypeOutputFormatter(
            JsonSerializerSettings serializerSettings, 
            ArrayPool<char> charPool,
            bool ignoreCase) :
            base(serializerSettings, charPool, ignoreCase)
        {
            resourceConverter = new ResourceConverter();
            Initialize();
        }

        private void Initialize()
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(_mediaTypeHeaderValueName));
            SerializerSettings.Converters.Add(linksConverter);
            SerializerSettings.Converters.Add(resourceConverter);
            SerializerSettings.Converters.Add(embeddedResourceConverter);
            SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }

        protected override bool CanWriteType(Type type)
        {
            return typeof(Representation).IsAssignableFrom(type);
        }
    }
}
