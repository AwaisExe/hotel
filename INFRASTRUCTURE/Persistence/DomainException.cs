using System;
using System.Net;

namespace INFRASTRUCTURE.Persistence
{
    public class DomainException : BaseExceptions
    {
        public DomainException(HttpStatusCode statusCode, string message, bool isResponseList = true) : base(message)
        {
            StatusCode = (int)statusCode;
            IsResponseList = isResponseList;
        }
        public DomainException(HttpStatusCode statusCode, string message, Exception exception, bool isResponseList = true) : base(message, exception)
        {
            StatusCode = (int)statusCode;
            IsResponseList = isResponseList;
        }
        public int StatusCode { get; }
        public bool IsResponseList { get; set; }

    }
}
