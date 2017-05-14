# OmniKassa

The OmniKassa library is a C# API that can be used to communicate with the [OmniKassa](https://www.rabobank.nl/bedrijven/betalen/geld-ontvangen/rabo-omnikassa/) from the [Rabobank](https://www.rabobank.nl).

[![GitHub license](https://img.shields.io/badge/license-MIT-green.svg)](https://raw.githubusercontent.com/dlemstra/line-bot-sdk-dotnet/master/License.txt)
[![Twitter URL](https://img.shields.io/twitter/url/https/twitter.com/fold_left.svg?style=social&label=Follow%20%40MagickNET)](https://twitter.com/MagickNET)

|             |Build Status|Code Coverage|
|-------------|:----------:|:-----------:|
|**Linux/Mac**| -|[![codecov](https://codecov.io/gh/dlemstra/OmniKassa/branch/master/graph/badge.svg)](https://codecov.io/gh/dlemstra/OmniKassa)|
|**Windows**  | [![Build status](https://ci.appveyor.com/api/projects/status/3ol6woroo3qsmrml/branch/master?svg=true)](https://ci.appveyor.com/project/dlemstra/omnikassa/branch/master)|[![codecov](https://codecov.io/gh/dlemstra/OmniKassa/branch/master/graph/badge.svg)](https://codecov.io/gh/dlemstra/OmniKassa)|


### Supported Platforms
- .NET Framework (3.5 and higher)
- .NET Core (.NET Standard 1.3 and higher)

### Installation

The package for this library is available on [NuGet](https://www.nuget.org/packages/OmniKassa).

### API 

This library has two versions with a slightly different API. The .NET Standard 1.3 API is asynchronous and the .NET Framework library is synchronous. Below are examples for both versions of the library.

The first step is setting up the configuration for communicating with the OmniKassa. The interface for the configuration is called `IKassaConfiguration`. The library comes with an implementation for this class called `KassaConfiguration`.

```C#
IKassaConfiguration configuration = new KassaConfiguration()
{
    MerchantId = "DirkLemstra",
    SecretKey = "SuperSecret",
    KeyVersion = 1 // Optional
};
```

The `KassaConfiguration` class also has an `Url` property. This is already set to `https://payment-webinit.omnikassa.rabobank.nl/paymentServlet` but you can overide it if you need to. The default value for `KeyVersion` is set to `1` so this means you don't need to set it if you have never changed your `SecretKey`. The library also contains a test configuration that can be used to communicate with the test environment of OmniKassa.

```C#
IKassaConfiguration configuration = new TestKassaConfiguration();
```

The next step is creating a kassa. The kassa can be used to start a payment and to handle the payment response from OmniKassa.

```C#
IKassa kassa = new Kassa(configuration);
```

At this point you will need to create a `IPaymentRequest` to start a payment. Below are only the required and recommended properties. The `IPaymentRequest` interface also has a set of optional properties that can be found [here](https://github.com/dlemstra/OmniKassa/blob/master/src/OmniKassa/IPaymentRequest.cs).

```C#
IPaymentRequest request = new PaymentRequest
{
    // The amount that that customer needs to pay. (€ 5.42 in this example)
    Amount = 5.42m,

    // The url that the Rabo OmniKassa server will automatically notify with the current status after a payment or process.
    // This is optional but the Rabobank recommends you to specify it.
    AutomaticResponseUrl = new Uri("https://yoursite.com/HandlePaymentResponse"),

    // The currency code of the amount that the customer needs to pay.
    CurrencyCode = CurrencyCode.Euro,

    // The ID of the order that can be used to identify the customer.
    // This is optional but the Rabobank recommends you to specify it.
    OrderId = "Order42",

    // The page to which the customer is redirected after payment.
    ReturnUrl = new Uri("https://yoursite.com/OrderProcessed"),

    // A unique string to identify the transaction.
    TransactionReference = Guid.NewGuid().ToString()
};
```

To start a payment you will need to send a HTML document to the customer that will redirect the customer to the OmniKassa. The HTML can be retrieved with the `GetPaymentHtml` method of the `IKassa`.

```C#
// .NET Standard 1.3
string paymentHtml = await kassa.GetPaymentHtml(request)

// .NET Framework
string paymentHtml = kassa.GetPaymentHtml(request);
```

After that costumer has finished or aborted the payment in the OmniKassa they will be redirected to the `ReturnUrl` and the `AutomaticResponseUrl` will be called by the OmniKassa. On this url you can process the payment in your back-end system.

```C#
// .NET Standard 1.3
public async Task HandleResponse(IKassa kassa, Microsoft.AspNetCore.Http.HttpRequest request)
{
    IPaymentResponse response = await kassa.GetResponse(request);
    HandleResponse(response);
}

// .NET Standard 1.3
public void HandleResponse(IKassa kassa, System.Web.HttpRequest request)
{
    IPaymentResponse response = kassa.GetResponse(request);
    HandleResponse(response);
}
```

The response is automatically checked by the library and will throw an `InvalidOperationException` if the seal is incorrect. You can use the `Status` property to determine if the payment was succesful.

```C#
public void HandleResponse(IPaymentResponse response)
{
    if (response.Status == ResponseStatus.Successful)
        HandleSuccess(response);
    else
        HandleFailure(response);
}
```

The rest of the properties that are available in `IPaymentResponse` can be found [here](https://github.com/dlemstra/OmniKassa/blob/master/src/OmniKassa/IPaymentResponse.cs).


### Donate

If you have an uncontrollable urge to give me something for the time and effort I am putting into this project then please buy me something from my [amazon wish list](http://www.amazon.de/registry/wishlist/2XFZAC3J04WAY) or send me an [amazon gift card](https://www.amazon.de/Amazon-Gutschein-per-E-Mail-Amazon/dp/B0054PDOV8). If you prefer to use PayPal then [click here](https://www.paypal.me/DirkLemstra).

----
_A special thanks goes out to [De Friesland](https://www.defriesland.nl/) that allowed me to open source this project._