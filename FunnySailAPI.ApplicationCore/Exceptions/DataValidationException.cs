using FunnySailAPI.ApplicationCore.Models.Globals;
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
        public ExceptionTypesEnum ExceptionType { get; }

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

        public DataValidationException(string enTitle, string esTitle, ExceptionTypesEnum exceptionType)
            : this(enTitle)
        {
            string enMessage = "";
            string esMessage = "";

            switch (exceptionType)
            {
                case ExceptionTypesEnum.NotFound:
                    enMessage = $"{enTitle} not found.";
                    esMessage = $"{esTitle} no existe.";
                    break;

                case ExceptionTypesEnum.IsRequired:
                    enMessage = $"{enTitle} is required.";
                    esMessage = $"{esTitle} es requerida.";
                    break;

                default: throw new NotImplementedException("Exception type not implemented");
            }

            EnMessage = enMessage;
            EsMessage = esMessage;
            ExceptionType = exceptionType;
        }
    }
}
