
using System;

namespace APILayer.Errors
{
    public class MarketStackException : Exception
    {
        public ErrorResponse Error;

        public MarketStackException(ErrorResponse error)
        {
            Error = error;
            // ToDo: add inner exceptions for each of the context.symbols
        }

        public override string Message => Error.error.message;
    }

    public class symbols
    {
        public string key { get; set; }
        public string message { get; set; }
    }

    public class context
    {
        public symbols[] symbols { get; set; }
    }

    public class error
    {
        public string code { get; set; }
        public string message { get; set; }
        public context context { get; set; }
    }

    public class ErrorResponse
    {
        public error error { get; set; }
    }
}
