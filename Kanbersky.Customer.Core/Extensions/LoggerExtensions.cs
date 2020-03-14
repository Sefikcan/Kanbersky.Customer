using Kanbersky.Customer.Core.Logging;
using Serilog;
using System.Linq;

namespace Kanbersky.Customer.Core.Extensions
{
    public static class LoggerExtensions
    {
        public static ILogger HandleLogging(this ILogger logger,LoggingModel logginModel)
        {
            if (logginModel == null)
                return logger;

            logger = logger
                .ForContext(nameof(logginModel.RequestHost), logginModel.RequestHost)
                .ForContext(nameof(logginModel.RequestProtocol), logginModel.RequestProtocol)
                .ForContext(nameof(logginModel.RequestMethod), logginModel.RequestMethod)
                .ForContext(nameof(logginModel.ResponseStatusCode), logginModel.ResponseStatusCode)
                .ForContext(nameof(logginModel.RequestPath), logginModel.RequestPath)
                .ForContext(nameof(logginModel.RequestPathAndQuery), logginModel.RequestPathAndQuery);

            if (logginModel.RequestHeaders != null && logginModel.RequestHeaders.Any())
                logger = logger.ForContext(nameof(logginModel.RequestHeaders), logginModel.RequestHeaders, true);

            if (logginModel.ElapsedMilliseconds != null)
                logger = logger.ForContext(nameof(logginModel.ElapsedMilliseconds), logginModel.ElapsedMilliseconds);

            if (!string.IsNullOrEmpty(logginModel.RequestBody))
                logger = logger.ForContext(nameof(logginModel.RequestBody), logginModel.RequestBody);

            if (logginModel.Exception != null) logger = logger.ForContext(nameof(logginModel.Exception), logginModel.Exception, true);

            if (!string.IsNullOrEmpty(logginModel.Data))
                logger = logger.ForContext(nameof(logginModel.Data), logginModel.Data);

            return logger;
        }
    }
}
