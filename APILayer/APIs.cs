using APILayer.Errors;
using APILayer.DataObject;
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
                    else throw new MarketStackException(JsonConvert.DeserializeObject<ErrorResponse>(content));
                }
            }
            return null;
        }
        
        public enum Intervals { _1min, _5min, _10min, _15min, _30min, _1hour, _3hour, _6hour, _12hour, _24hour }

        public Task<EODResponse> GetEod(string[] symbols, DateTime? from = null, DateTime? to = null, int limit = 100, int offset = 0)
        {
            List<string> extraData = new List<string>
            {
                $"symbols={string.Join(",", symbols)}",
                $"limit={limit}",
                $"offset={offset}"
            };
            if (from.HasValue) extraData.Add($"date_from={from.Value.ToString("s")}");
            if (to.HasValue) extraData.Add($"date_to={to.Value.ToString("s")}");
            return GetData<EODResponse>("eod", extraData.ToArray());

        }

        public Task<IntradayResponse> GetIntraDay(string[] symbols, Intervals interval = Intervals._1hour, DateTime? from = null, DateTime? to = null, int limit = 100, int offset = 0)
        {
            List<string> extraData = new List<string>
            {
                $"symbols={string.Join(",", symbols)}",
                $"limit={limit}",
                $"offset={offset}",
                $"interval={Enum.GetName(typeof(Intervals), interval).Replace("_","")}"
            };
            if (from.HasValue) extraData.Add($"date_from={from.Value.ToString("s")}");
            if (to.HasValue) extraData.Add($"date_to={to.Value.ToString("s")}");
            return GetData<IntradayResponse>("intraday", extraData.ToArray());
        }

        public Task<TickerResponse> GetTickers(string[] symbols, int limit = 100, int offset = 0) 
        {
            List<string> extraData = new List<string>
            {
                $"limit={limit}",
                $"offset={offset}"
            };
            if (symbols.Length > 0) extraData.Add($"symbols={string.Join(",", symbols)}");
            return GetData<TickerResponse>("tickers", extraData.ToArray());
        }

        public Task<TickerResponse> GetTickers(string exchange, int limit = 100, int offset = 0) 
        {
            List<string> extraData = new List<string>
            {
                $"limit={limit}",
                $"offset={offset}",
                $"exchange={exchange}"
            };
            return GetData<TickerResponse>("tickers", extraData.ToArray());
        }

        public Task<ExchangeResponse> GetExchanges(int limit = 1000, int offset = 0)
        {
            List<string> extraData = new List<string>
            {
                $"limit={limit}",
                $"offset={offset}"
            };
            return GetData<ExchangeResponse>("exchanges", extraData.ToArray());
        }

        public Task<CurrencyResponse> GetCurrencies(int limit = 1000, int offset = 0)
        {
            List<string> extraData = new List<string>
            {
                $"limit={limit}",
                $"offset={offset}"
            };
            return GetData<CurrencyResponse>("currencies", extraData.ToArray());
        }

        public Task<TimezoneResponse> GetTimeZones(int limit = 1000, int offset = 0)
        {
            List<string> extraData = new List<string>
            {
                $"limit={limit}",
                $"offset={offset}"
            };
            return GetData<TimezoneResponse>("timezones", extraData.ToArray());
        }
    }
}
