using APLayer.DataObject;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static APILayer.MarketStack;

namespace APILayer
{
    public static class Extensions
    {
        static async Task<T[]> GetAllData<T>(Func<int/*offset*/, Task<Response<T>>> method) where T : class
        {
            List<T> ret = new List<T>();
            var data = await method(0);
            ret.AddRange(data.data);
            for (int offset = data.pagination.offset + data.pagination.limit, total = data.pagination.total; offset < total; offset = data.pagination.offset + data.pagination.limit)
            {
                data = await method(offset);
                ret.AddRange(data.data);
            }
            return ret.ToArray();
        }

        public static Task<EODData[]> GetAllEod(this MarketStack me, string[] symbols, DateTime? from = null, DateTime? to = null) =>
            GetAllData<EODData>(async offset => await me.GetEod(symbols, from, to, 1000, offset));

        public static Task<IntradayData[]> GetAllIntraDay(this MarketStack me, string[] symbols, Intervals interval = Intervals._1Hour, DateTime? from = null, DateTime? to = null) =>
            GetAllData<IntradayData>(async offset => await me.GetIntraDay(symbols, interval, from, to, 1000, offset));

        public static Task<TickerData[]> GetAllTickers(this MarketStack me, string[] symbols) =>
            GetAllData<TickerData>(async offset => await me.GetTickers(symbols, 1000, offset));

        public static Task<ExchangeData[]> GetAllExchanges(this MarketStack me) =>
            GetAllData<ExchangeData>(async offset => await me.GetExchanges(1000, offset));

        public static Task<CurrencyData[]> GetAllCurrencies(this MarketStack me) =>
            GetAllData<CurrencyData>(async offset => await me.GetCurrencies(1000, offset));

        public static Task<TimezoneData[]> GetAllTimeZones(this MarketStack me) =>
            GetAllData<TimezoneData>(async offset => await me.GetTimeZones(1000, offset));

    }
}
