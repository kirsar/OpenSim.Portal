using System;
using System.Buffers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebApi.Hal;

namespace OpenSim.WebServer.App.Startup
{
    /// <summary>
    /// Sets up JSON formatter options for <see cref="MvcOptions"/>.
    /// </summary>
    public class MvcJsonHalMediaSetup : IConfigureOptions<MvcOptions>
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly JsonSerializerSettings jsonSerializerSettings;
        private readonly ArrayPool<char> charPool;
        private readonly ObjectPoolProvider objectPoolProvider;

        public MvcJsonHalMediaSetup(
            ILoggerFactory loggerFactory,
            IOptions<MvcJsonOptions> jsonOptions,
            ArrayPool<char> charPool,
            ObjectPoolProvider objectPoolProvider)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            if (jsonOptions == null)
            {
                throw new ArgumentNullException(nameof(jsonOptions));
            }

            if (charPool == null)
            {
                throw new ArgumentNullException(nameof(charPool));
            }

            if (objectPoolProvider == null)
            {
                throw new ArgumentNullException(nameof(objectPoolProvider));
            }

            this.loggerFactory = loggerFactory;
            jsonSerializerSettings = jsonOptions.Value.SerializerSettings;
            this.charPool = charPool;
            this.objectPoolProvider = objectPoolProvider;
        }

        public void Configure(MvcOptions options)
        {
            options.OutputFormatters.Add(new JsonHalMediaTypeOutputFormatter(jsonSerializerSettings, charPool));

            // Register JsonPatchInputFormatter before JsonInputFormatter, otherwise
            // JsonInputFormatter would consume "application/json-patch+json" requests
            // before JsonPatchInputFormatter gets to see them.

            var jsonInputPatchLogger = loggerFactory.CreateLogger<JsonHalMediaTypeInputFormatter>();
            options.InputFormatters.Add(new JsonHalMediaTypeInputFormatter(
                                            jsonInputPatchLogger,
                                            new JsonSerializerSettings(),
                                            charPool,
                                            objectPoolProvider));
        }
    }
}
