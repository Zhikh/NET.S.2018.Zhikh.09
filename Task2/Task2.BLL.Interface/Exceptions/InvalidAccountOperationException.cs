﻿using System;
using System.Runtime.Serialization;

namespace Task2.BLL.Exceptions
{
    public class InvalidAccountOperationException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAccountOperationException"/> class.
        /// </summary>
        public InvalidAccountOperationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAccountOperationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">  A message that describes the error. </param>
        public InvalidAccountOperationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAccountOperationException"/> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">  The error message that explains the reason for the exception. </param>
        /// <param name="inner">
        /// The exception that is the cause of the current exception. If the innerException
        /// parameter is not a null reference, the current exception is raised in a catch
        /// block that handles the inner exception.
        /// </param>
        public InvalidAccountOperationException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAccountOperationException"/> class
        /// with serialized data.
        /// </summary>
        /// <param name="info"> The object that holds the serialized object data. </param>
        /// <param name="context"> The contextual information about the source or destination. </param>
        protected InvalidAccountOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
