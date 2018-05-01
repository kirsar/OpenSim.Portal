using System;
using System.Buffers;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using PartialResponse.AspNetCore.Mvc.Formatters;
using WebApi.Hal.JsonConverters;

namespace WebApi.Hal
{
    public class PartialJsonHalMediaTypeOutputFormatter : PartialJsonOutputFormatter
    {
        private const string _mediaTypeHeaderValueName = "application/hal+json";

        private readonly LinksConverter _linksConverter = new LinksConverter();

        private readonly ResourceConverter _resourceConverter;
        private readonly EmbeddedResourceConverter _embeddedResourceConverter = new EmbeddedResourceConverter();

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

            _resourceConverter = new ResourceConverter(hypermediaResolver);
            Initialize();
        }

        public PartialJsonHalMediaTypeOutputFormatter(
            JsonSerializerSettings serializerSettings, 
            ArrayPool<char> charPool,
            bool ignoreCase) :
            base(serializerSettings, charPool, ignoreCase)
        {
            _resourceConverter = new ResourceConverter();
            Initialize();
        }

        private void Initialize()
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(_mediaTypeHeaderValueName));
            SerializerSettings.Converters.Add(_linksConverter);
            SerializerSettings.Converters.Add(_resourceConverter);
            SerializerSettings.Converters.Add(_embeddedResourceConverter);
            SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }
        
        protected override bool CanWriteType(Type type)
        {
            return typeof(Representation).IsAssignableFrom(type);
        }
    }
}
