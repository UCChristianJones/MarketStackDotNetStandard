# MarketStackDotNetCore
Cross platform compatible DotNet standard API wrapper for access to "APILayer's" stock web services (https://marketstack.com)

### How to use:
```C# 
// Instantiate class - make sure you provide an API key
// If you do not have one, sign up and get one at: https://marketstack.com
MarketStack _stack = new MarketStack(""/*YOUR API KEY HERE*/);
//THEN
var res = await _stack.GetAllEod(new string[] { "TSLA" }, DateTime.Now.AddDays(-7), DateTime.Now);
//OR
var res = await _stack.GetAllIntraDay(new string[] { "TSLA" }, MarketStack.Intervals._24Hour, DateTime.Now.AddDays(-7), DateTime.Now);
//OR
var res = await _stack.GetAllTickers(new string[] { "TSLA" });
//OR
var res = await _stack.GetAllExchanges();
//OR
var res = await _stack.GetAllCurrencies();
//OR
var res = await _stack.GetAllTimeZones();
```
