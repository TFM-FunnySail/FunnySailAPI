using System;
using System.Collections.Generic;
using System.Text;

namespace FunnySailAPI.ApplicationCore.Exceptions
{
    [Serializable]
    public class DataValidationException : Exception
    {
        public string EnMessage { get; }
        public string EsMessage { get; }

        public DataValidationException() { }

        public DataValidationException(string message)
            : base(message) { }

        public DataValidationException(string message, Exception inner)
            : base(message, inner) { }

        public DataValidationException(string enMessage, string esMessage)
            : this(enMessage)
        {
            EnMessage = enMessage;
            EsMessage = esMessage;
        }
    }
}
