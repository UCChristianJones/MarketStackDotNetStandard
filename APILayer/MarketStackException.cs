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
        }
    }
}
