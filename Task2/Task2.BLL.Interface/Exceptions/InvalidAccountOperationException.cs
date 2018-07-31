using System;
using System.Runtime.Serialization;

namespace Task2.BLL.Exceptions
{
    public class InvalidAccountOperationException : ApplicationException
    {
        public InvalidAccountOperationException()
        {
        }

        public InvalidAccountOperationException(string message) : base(message)
        {
        }

        public InvalidAccountOperationException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidAccountOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
