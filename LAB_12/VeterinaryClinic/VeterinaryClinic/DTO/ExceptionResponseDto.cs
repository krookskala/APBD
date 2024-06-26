﻿using System.Net;

namespace VeterinaryClinic.DTO
{

    public class ExceptionResponseDto
    {
        public HttpStatusCode StatusCode { get; }
        public string Message { get; }

        public ExceptionResponseDto(HttpStatusCode statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
}