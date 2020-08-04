using APILayer;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    class Program
    {
        static MarketStack _stack = new MarketStack(""/*YOUR API KEY HERE*/);

        static void Main(string[] args)
        {
            ShowEOD();
            ShowIntraDay();
            ShowTickers();
            ShowExchanges();
            ShowCurrencies();
            ShowTimeZones();
            Console.Read();
        }

        static void WriteLine(string data)
        {
            Console.WriteLine(data);
        }
        static void WriteLines(IEnumerable<string> data)
        {
            data.ToList().ForEach(lineString => WriteLine(lineString));
        }

        static void WritePaginatorData(int limit, int offset, int count, int total)
        {
            WriteLine($"Page: Limit[{limit}], Offset[{offset}], Count[{count}], Total[{total}]");
        }

        static async void ShowEOD()
        {
            WriteLine(nameof(ShowEOD));
            var res = await _stack.GetEod(new string[] { "TSLA" }, DateTime.Now.AddDays(-7), DateTime.Now, 500, 0);
            WritePaginatorData(res.pagination.limit, res.pagination.offset, res.pagination.count, res.pagination.total);
            WriteLines(res.data
                .Select(d => $"Page: Close[{d.close}], Date[{d.date}], Exchange[{d.exchange}], High[{d.high}], Low[{d.low}], Open[{d.open}], Symbol[{d.symbol}], Volume[{d.volume}], adj_close[{d.adj_close}], adj_high[{d.adj_high}], adj_low[{d.adj_low}], adj_open[{d.adj_open}], adj_volume[{d.adj_volume}]"));
        }

        static async void ShowIntraDay()
        {
            WriteLine(nameof(ShowIntraDay));
            var res = await _stack.GetIntraDay(new string[] { "TSLA" }, MarketStack.Intervals._24Hour, DateTime.Now.AddDays(-7), DateTime.Now, 500, 0);
            WritePaginatorData(res.pagination.limit, res.pagination.offset, res.pagination.count, res.pagination.total);
            WriteLines(res.data
                .Select(d => $"Page: Close[{d.close}], Date[{d.date}], Exchange[{d.exchange}], High[{d.high}], Last[{d.last}], Low[{d.low}], Open[{d.open}], Symbol[{d.symbol}], Volume[{d.volume}]"));
        }

        static async void ShowTickers()
        {
            WriteLine(nameof(ShowTickers));
            var res = await _stack.GetTickers(new string[] { "TSLA" }, 500, 0);
            WritePaginatorData(res.pagination.limit, res.pagination.offset, res.pagination.count, res.pagination.total);
            WriteLines(res.data
                .Select(d => $"Page: name[{d.name}], symbol[{d.symbol}], acronym[{d.stock_exchange.acronym}], city[{d.stock_exchange.city}], country[{d.stock_exchange.country}], country_code[{d.stock_exchange.country_code}], mic[{d.stock_exchange.mic}], StkExName[{d.stock_exchange.name}], timezone[{d.stock_exchange.timezone}], website[{d.stock_exchange.website}]"));
        }

        static async void ShowExchanges()
        {
            WriteLine(nameof(ShowExchanges));
            var res = await _stack.GetExchanges();
            WritePaginatorData(res.pagination.limit, res.pagination.offset, res.pagination.count, res.pagination.total);
            WriteLines(res.data
                .Select(d => $"Page: name[{d.name}], acronym[{d.acronym}], city[{d.city}], country[{d.country}], country_code[{d.country_code}], mic[{d.mic}], StkExName[{d.name}], timezone[{d.timezone}], website[{d.website}]"));
        }

        static async void ShowCurrencies()
        {
            WriteLine(nameof(ShowCurrencies));
            var res = await _stack.GetCurrencies();
            WritePaginatorData(res.pagination.limit, res.pagination.offset, res.pagination.count, res.pagination.total);
            WriteLines(res.data
                .Select(d => $"Page: name[{d.name}], symbol[{d.symbol}], code[{d.code}], symbol_native[{d.symbol_native}]"));

        }

        static async void ShowTimeZones()
        {
            WriteLine(nameof(ShowTimeZones));
            var res = await _stack.GetTimeZones();
            WritePaginatorData(res.pagination.limit, res.pagination.offset, res.pagination.count, res.pagination.total);
            WriteLines(res.data
                .Select(d => $"Page: abbr[{d.abbr}], abbr_dst[{d.abbr_dst}], timezone[{d.timezone}]"));
        }
    }
}
