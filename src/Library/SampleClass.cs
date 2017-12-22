using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Library
{
    public class SampleClass
    {
        private readonly SampleSettings _settings;
        private readonly ILogger _logger;

        public SampleClass(IOptions<SampleSettings> options, ILogger<SampleClass> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public void Go()
        {
            _logger.LogDebug($"ComputerName : {_settings.ComputerName}");
            _logger.LogInformation($"ConfigValue : {_settings.ConfigValue}");
        }
    }
}
