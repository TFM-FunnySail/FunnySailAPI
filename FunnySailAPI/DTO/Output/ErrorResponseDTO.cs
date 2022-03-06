using FunnySailAPI.ApplicationCore.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunnySailAPI.DTO.Output
{
    public class ErrorResponseDTO
    {
        public string EnMessage { get; set; }
        public string EsMessage { get; set; }
        public bool Success { get; set; }

        public ErrorResponseDTO(Exception ex)
        {
            EnMessage = ex.Message;
            EsMessage = ex.Message;
            Success = false;
        }

        public ErrorResponseDTO(string enMessage, string esMessage)
        {
            EnMessage = enMessage;
            EsMessage = esMessage;
            Success = false;
        }

        public ErrorResponseDTO(DataValidationException dataValidation)
        {
            EnMessage = dataValidation.EnMessage;
            EsMessage = dataValidation.EsMessage;
            Success = false;
        }
    }
}
