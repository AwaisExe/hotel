using System;
using System.Runtime.Serialization;

namespace INFRASTRUCTURE.Persistence
{
    [Serializable]
    public class DbConcurrencyException : BaseExceptions
    {
        public DbConcurrencyException()
        {
        }

        public DbConcurrencyException(string message)
            : base(message)
        {
        }

        public DbConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected DbConcurrencyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
