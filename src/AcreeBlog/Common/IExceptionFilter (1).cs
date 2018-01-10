
namespace Microsoft.AspNetCore.Mvc.Filters
{
    // http://www.talkingdotnet.com/global-exception-handling-in-aspnet-core-webapi/
    public interface IExceptionFilter : IFilterMetadata
    {
        void OnException(ExceptionContext context);
    }
}
