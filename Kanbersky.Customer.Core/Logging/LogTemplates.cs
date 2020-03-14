namespace Kanbersky.Customer.Core.Logging
{
    public class LogTemplates
    {
        public static readonly string Error = $"ERROR: HTTP {"{" + nameof(LoggingModel.RequestMethod) + "}"} / {"{" + nameof(LoggingModel.RequestPathAndQuery) + "}"} responded as {"{" + nameof(LoggingModel.ResponseStatusCode) + "}"}";

        public static readonly string BadRequest = $"BAD REQUEST: HTTP {"{" + nameof(LoggingModel.RequestMethod) + "}"} / {"{" + nameof(LoggingModel.RequestPathAndQuery) + "}"} responded as {"{" + nameof(LoggingModel.ResponseStatusCode) + "}"}";
    }
}
