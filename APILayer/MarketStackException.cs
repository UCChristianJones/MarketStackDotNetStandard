using APILayer.errors;
using System;

namespace APILayer
{
    public class MarketStackException : Exception
    {
        public response Error;

        public MarketStackException(response error)
        {
            Error = error;
            // ToDo: add inner exceptions for each of the context.symbols
        }

        public override string Message => Error.error.message;
    }
}
