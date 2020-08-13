using APILayer.DataObject;
using System;
using System.Threading;
using System.Threading.Tasks;
using static APILayer.MarketStack;

namespace APILayer
{
    public static class Extensions
    {
        static void AddToArray<T>(ref T[] arr, Response<T> resp) where T : class
        {
            for (int i = 0, to = resp.pagination.count; i < to; i++) arr[i+ resp.pagination.offset] = resp.data[i]; 
        }

        static async Task<T[]> GetAllData<T>(Func<int/*offset*/, Task<Response<T>>> method) where T : class
        {
            var data = await method(0);
            T[] ret = new T[data.pagination.total];
            AddToArray(ref ret, data);
            object lok = new object();
            int runningThreads = 0;
            for (int offset = data.pagination.offset + data.pagination.limit, total = data.pagination.total; offset < total; offset += data.pagination.limit)
            {
                lock(lok) runningThreads++;
                int newOffset = offset;
#pragma warning disable CS4014
                Task.Run(async () =>
                {
                    try
                    {
                        var dat = await method(newOffset);
                        AddToArray(ref ret, dat);
                    }
                    finally
                    {
                        lock (lok) runningThreads--;
                    }
                });
#pragma warning restore CS4014
            }
            while (runningThreads != 0) Thread.Sleep(15);
            return ret;
        }

        public static Task<EODData[]> GetAllEod(this MarketStack me, string[] symbols, DateTime? from = null, DateTime? to = null) =>
            GetAllData<EODData>(async offset => await me.GetEod(symbols, from, to, 1000, offset));

        public static Task<IntradayData[]> GetAllIntraDay(this MarketStack me, string[] symbols, Intervals interval = Intervals._1hour, DateTime? from = null, DateTime? to = null) =>
            GetAllData<IntradayData>(async offset => await me.GetIntraDay(symbols, interval, from, to, 1000, offset));

        public static Task<TickerData[]> GetAllTickers(this MarketStack me, string[] symbols) =>
            GetAllData<TickerData>(async offset => await me.GetTickers(symbols, 1000, offset));
        public static Task<TickerData[]> GetAllTickers(this MarketStack me, string exchange) =>
            GetAllData<TickerData>(async offset => await me.GetTickers(exchange, 1000, offset));

        public static Task<ExchangeData[]> GetAllExchanges(this MarketStack me) =>
            GetAllData<ExchangeData>(async offset => await me.GetExchanges(1000, offset));

        public static Task<CurrencyData[]> GetAllCurrencies(this MarketStack me) =>
            GetAllData<CurrencyData>(async offset => await me.GetCurrencies(1000, offset));

        public static Task<TimezoneData[]> GetAllTimeZones(this MarketStack me) =>
            GetAllData<TimezoneData>(async offset => await me.GetTimeZones(1000, offset));

    }
}
