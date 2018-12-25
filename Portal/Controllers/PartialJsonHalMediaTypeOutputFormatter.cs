using System;
using System.Buffers;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using PartialResponse.AspNetCore.Mvc.Formatters;
using WebApi.Hal;
using WebApi.Hal.JsonConverters;

namespace OpenSim.Portal.Controllers
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

            resourceConverter = new ResourceConverter(hypermediaResolver, serializerSettings);
            Initialize();
        }

        public PartialJsonHalMediaTypeOutputFormatter(
            JsonSerializerSettings serializerSettings,
            ArrayPool<char> charPool,
            bool ignoreCase) :
            base(serializerSettings, charPool, ignoreCase)
        {
            resourceConverter = new ResourceConverter(serializerSettings);
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
