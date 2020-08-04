using System;

namespace APILayer.errors
{
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

    public class response
    {
        public error error { get; set; }
    }
}

namespace APILayer.eod
{
    public class pagination
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public int total { get; set; }
    }

    public class data
    {
        public DateTime date { get; set; }
        public string symbol { get; set; }
        public string exchange { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public double volume { get; set; }
        public double adj_open { get; set; }
        public double adj_high { get; set; }
        public double adj_low { get; set; }
        public double adj_close { get; set; }
        public double adj_volume { get; set; }
    }

    public class response
    {
        public pagination pagination { get; set; }
        public data[] data { get; set; }
    }
}

namespace APILayer.intraday
{
    public class pagination
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public int total { get; set; }
    }

    public class data
    {
        public DateTime date { get; set; }
        public string symbol { get; set; }
        public string exchange { get; set; }
        public double open { get; set; }
        public double high { get; set; }
        public double low { get; set; }
        public double close { get; set; }
        public double last { get; set; }
        public double volume { get; set; }
    }

    public class response
    {
        public pagination pagination { get; set; }
        public data[] data { get; set; }
    }
}

namespace APILayer.tickers
{
    public class pagination
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public int total { get; set; }
    }

    public class _timezone
    {
        public string timezone { get; set; }
        public string abbr { get; set; }
        public string abbr_dst { get; set; }
    }

    public class stock_exchange
    {
        public string name { get; set; }
        public string acronym { get; set; }
        public string mic { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string city { get; set; }
        public string website { get; set; }
        public _timezone timezone { get; set; }
    }

    public class data
    {
        public string name { get; set; }
        public string symbol { get; set; }
        public stock_exchange stock_exchange { get; set; }
    }

    public class response
    {
        public pagination pagination { get; set; }
        public data[] data { get; set; }
    }
}


namespace APILayer.exchanges
{
    public class pagination
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public int total { get; set; }
    }

    public class _timezone
    {
        public string timezone { get; set; }
        public string abbr { get; set; }
        public string abbr_dst { get; set; }
    }

    public class data
    {
        public string name { get; set; }
        public string acronym { get; set; }
        public string mic { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string city { get; set; }
        public string website { get; set; }
        public _timezone timezone { get; set; }
    }

    public class response
    {
        public pagination pagination { get; set; }
        public data[] data { get; set; }
    }
}

namespace APILayer.currencies
{
    public class pagination
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public int total { get; set; }
    }

    public class data
    {
        public string code { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string symbol_native { get; set; }
    }

    public class response
    {
        public pagination pagination { get; set; }
        public data[] data { get; set; }
    }
}

namespace APILayer.timezones
{
    public class pagination
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public int total { get; set; }
    }

    public class data
    {
        public string timezone { get; set; }
        public string abbr { get; set; }
        public string abbr_dst { get; set; }
    }

    public class response
    {
        public pagination pagination { get; set; }
        public data[] data { get; set; }
    }
}