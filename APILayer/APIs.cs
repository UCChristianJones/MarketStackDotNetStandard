using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace APILayer
{
    public class MarketStack
    {
        const string BaseURL = "https://api.marketstack.com/v1";
        string _key;

        public MarketStack(string key)
        {
            _key = key;
        }

        string GetURL(string area, params string[] extraData)
        {
            string ret = $"{BaseURL}/{area}?access_key={_key}"; // build up the basic URL without extra data
            if (extraData.Length > 0) ret += $"&{string.Join("&", extraData)}"; // add the extra data if there is any
            return ret;
        }

        async Task<T> GetData<T>(string area, params string[] extraData) where T: class
        {
            string url = GetURL(area, extraData);
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = await client.GetAsync(url);
                if (await resp.Content.ReadAsStringAsync() is string content && !string.IsNullOrWhiteSpace(content))
                {
                    if (resp.IsSuccessStatusCode) return JsonConvert.DeserializeObject<T>(content);
                    else throw new MarketStackException(JsonConvert.DeserializeObject<errors.response>(content));
                }
            }
            return null;
        }
        
        public enum Intervals { _1Min, _5Min, _10Min, _15Min, _30Min, _1Hour, _3Hour, _6Hour, _12Hour, _24Hour }

        public Task<eod.response> GetEod(string[] symbols, DateTime? from = null, DateTime? to = null, int limit = 100, int offset = 0)
        {
            List<string> extraData = new List<string>
            {
                $"symbols={string.Join(",", symbols)}",
                $"limit={limit}",
                $"offset={offset}"
            };
            if (from.HasValue) extraData.Add($"date_from={from.Value.ToString("s")}");
            if (to.HasValue) extraData.Add($"date_to={to.Value.ToString("s")}");
            return GetData<eod.response>("eod", extraData.ToArray());

        }

        public Task<intraday.response> GetIntraDay(string[] symbols, Intervals interval = Intervals._1Hour, DateTime? from = null, DateTime? to = null, int limit = 100, int offset = 0)
        {
            List<string> extraData = new List<string>
            {
                $"symbols={string.Join(",", symbols)}",
                $"limit={limit}",
                $"offset={offset}"
            };
            if (from.HasValue) extraData.Add($"date_from={from.Value.ToString("s")}");
            if (to.HasValue) extraData.Add($"date_to={to.Value.ToString("s")}");
            return GetData<intraday.response>("intraday", extraData.ToArray());
        }

        public Task<tickers.response> GetTickers(string[] symbols, int limit = 100, int offset = 0) 
        {
            List<string> extraData = new List<string>
            {
                $"limit={limit}",
                $"offset={offset}"
            };
            if (symbols.Length > 0) extraData.Add($"symbols={string.Join(",", symbols)}");
            return GetData<tickers.response>("tickers", extraData.ToArray());
        }

        public Task<exchanges.response> GetExchanges()
        {
            List<string> extraData = new List<string>
            {
                $"limit=1000"
            };
            return GetData<exchanges.response>("exchanges", extraData.ToArray());
        }

        public Task<currencies.response> GetCurrencies()
        {
            List<string> extraData = new List<string>
            {
                $"limit=1000"
            };
            return GetData<currencies.response>("currencies", extraData.ToArray());
        }

        public Task<timezones.response> GetTimeZones()
        {
            List<string> extraData = new List<string>
            {
                $"limit=1000"
            };
            return GetData<timezones.response>("timezones", extraData.ToArray());
        }
    }
}
