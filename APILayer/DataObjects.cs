using System;

namespace APILayer.DataObject
{
    public class Pagination
    {
        ///Returns your pagination limit value.
        public int limit { get; set; }
        ///Returns your pagination offset value.
        public int offset { get; set; }
        ///Returns the results count on the current page.
        public int count { get; set; }
        ///Returns the total count of results available
        public int total { get; set; }
    }

    public abstract class Response<T> where T : class
    {
        public Pagination pagination { get; set; }
        public T[] data;
    }

    public class EODData
    {
        public DateTime date { get; set; }
        public string symbol { get; set; }
        public string exchange { get; set; }
        public double? open { get; set; }
        public double? high { get; set; }
        public double? low { get; set; }
        public double? close { get; set; }
        public double? volume { get; set; }
        public double? adj_open { get; set; }
        public double? adj_high { get; set; }
        public double? adj_low { get; set; }
        public double? adj_close { get; set; }
        public double? adj_volume { get; set; }
    }

    public class IntradayData
    {
        public DateTime date { get; set; }
        public string symbol { get; set; }
        public string exchange { get; set; }
        public double? open { get; set; }
        public double? high { get; set; }
        public double? low { get; set; }
        public double? close { get; set; }
        public double? last { get; set; }
        public double? volume { get; set; }
    }

    public class TickerData
    {
        public string name { get; set; }
        public string symbol { get; set; }
        public ExchangeData stock_exchange { get; set; }
    }

    public class ExchangeData
    {
        public string name { get; set; }
        public string acronym { get; set; }
        public string mic { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string city { get; set; }
        public string website { get; set; }
        public TimezoneData timezone { get; set; }
    }

    public class CurrencyData
    {
        public string code { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string symbol_native { get; set; }
    }

    public class TimezoneData
    {
        public string timezone { get; set; }
        public string abbr { get; set; }
        public string abbr_dst { get; set; }
    }

    public class EODResponse : Response<EODData> { }
    public class IntradayResponse : Response<IntradayData> { }
    public class TickerResponse : Response<TickerData> { }
    public class ExchangeResponse : Response<ExchangeData> { }
    public class CurrencyResponse : Response<CurrencyData> { }
    public class TimezoneResponse : Response<TimezoneData> { }
}