# MarketStackDotNetCore
Cross platform compatible DotNet standard API wrapper for access to "APILayer's" stock web services (https://marketstack.com)

### How to use:
```C# 
// Instantiate class - make sure you provide an API key
// If you do not have one, sign up and get one at: https://marketstack.com
MarketStack _stack = new MarketStack(""/*YOUR API KEY HERE*/);
//THEN
var res = await _stack.GetEod(new string[] { "TSLA" }, DateTime.Now.AddDays(-7), DateTime.Now, 500, 0);
//OR
var res = await _stack.GetIntraDay(new string[] { "TSLA" }, MarketStack.Intervals._24Hour, DateTime.Now.AddDays(-7), DateTime.Now, 500, 0);
//OR
var res = await _stack.GetTickers(new string[] { "TSLA" }, 500, 0);
//OR
var res = await _stack.GetExchanges();
//OR
var res = await _stack.GetCurrencies();
//OR
var res = await _stack.GetTimeZones();
```
