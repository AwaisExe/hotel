using INFRASTRUCTURE.Invariant;
using INFRASTRUCTURE.Persistence;
using INFRASTRUCTURE.Validator;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.Utility
{
    public class JsonExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Func<object, Task> _clearCacheHeadersDelegate;
        private readonly JsonSerializer _serializer;

        public JsonExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            _serializer = new JsonSerializer
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var request = context.Request;
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception middlewareError)
            {

                if (context.Response.HasStarted)
                {
                    throw new Exception("The response has already started, the error page middleware will not be executed.");
                }

                // reset body
                if (context.Response.Body.CanSeek)
                    context.Response.Body.SetLength(0L);

                context.Response.StatusCode = 500;
                context.Response.OnStarting(_clearCacheHeadersDelegate, context.Response);

                await WriteContent(context, middlewareError).ConfigureAwait(false);
                return;
            }
        }

        private async Task WriteContent(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            switch (exception)
            {
                case ValidationException validationException:
                    var errors = validationException.ValidationResultModel;
                    context.Response.StatusCode = errors.StatusCode;
                    using (var writer = new StreamWriter(context.Response.Body))
                    {
                        _serializer.Serialize(writer, errors);
                        await writer.FlushAsync().ConfigureAwait(false);
                    }
                    break;

                case DomainException domainException:
                    context.Response.StatusCode = domainException.StatusCode;
                    using (var writer = new StreamWriter(context.Response.Body))
                    {
                        if (!domainException.IsResponseList) { ResponseModel(domainException, writer); }
                        else { ResponseListModel(domainException, writer); }
                        await writer.FlushAsync().ConfigureAwait(false);
                    }

                    break;
                default:
                    using (var writer = new StreamWriter(context.Response.Body))
                    {
                        ResponseModel(exception, writer);
                        await writer.FlushAsync().ConfigureAwait(false);
                    }

                    break;
            }
        }
        private void ResponseModel(Exception exception, StreamWriter writer)
        {
            _serializer.Serialize(writer, new EntityResponseModel<dynamic>()
            {
                Data = default,
                ReturnStatus = false,
                StatusCode = 500,
                ReturnMessage = new List<string> { exception.Message }
            });
        }
        private void ResponseListModel(DomainException domainException, StreamWriter writer)
        {
            _serializer.Serialize(writer, new EntityResponseListModel<dynamic>()
            {
                Data = default,
                ReturnStatus = false,
                StatusCode = domainException.StatusCode,
                ReturnMessage = new List<string> { domainException.Message }
            });
        }
        private void ResponseModel(DomainException domainException, StreamWriter writer)
        {
            _serializer.Serialize(writer, new EntityResponseModel<dynamic>()
            {
                Data = default,
                ReturnStatus = false,
                StatusCode = domainException.StatusCode,
                ReturnMessage = new List<string> { domainException.Message }
            });
        }
    }
}
