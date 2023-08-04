﻿using System.Net;

namespace Application.Common.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpException(string message, HttpStatusCode statusCode) : base(message) 
        { 
            StatusCode = statusCode;
        }

        public HttpException(string message, HttpStatusCode statusCode, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
