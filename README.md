<img align="right" src="/AutoWrapper.Server/logo.png" />

# AutoWrapper.Server  [![Nuget](https://img.shields.io/nuget/v/AutoWrapper.Server?color=blue)](https://www.nuget.org/packages/AutoWrapper.Server) [![Nuget downloads](https://img.shields.io/nuget/dt/AutoWrapper.Server?color=green)](https://www.nuget.org/packages/AutoWrapper.Server)

`AutoWrapper.Server` is simple library that enables you unwrap the `Result` property of the [AutoWrapper](https://github.com/proudmonkey/AutoWrapper)'s `ApiResponse` object in your C# server-side code. The goal is to deserialize the `Result` object directly to your matching `Model` without having you to create the `ApiResponse` schema.

# Installation
1. Download and Install the latest `AutoWrapper.Server` from NuGet or via CLI:

```
PM> Install-Package AutoWrapper.Server -Version 2.0.0
```

2. Declare the following namespace in the class where you want to use it.

```csharp
using AutoWrapper.Server;
```

## Sample Usage


```csharp
[HttpGet]
public async Task<IEnumerable<PersonDTO>> Get()
{
    var client = HttpClientFactory.Create();
    var httpResponse = await client.GetAsync("https://localhost:5001/api/v1/persons");

    IEnumerable<PersonDTO> persons = null;
    if (httpResponse.IsSuccessStatusCode)
    {
        var jsonString = await httpResponse.Content.ReadAsStringAsync();
        persons = Unwrapper.Unwrap<IEnumerable<PersonDTO>>(jsonString);
    }

    return persons;
}
```

If you are using the `[AutoWrapperPropertyMap]` to replace the default `Result` property to something else like `Payload`, then you can use the following overload method below and pass the matching property:

```csharp
Unwrapper.Unwrap<IEnumerable<PersonDTO>>(jsonString, "payload");
```

## Using the UnwrappingResponseHandler
Alternatively you can use the `UnwrappingResponseHandler` like below:

```csharp
[HttpGet]
public async Task<IEnumerable<PersonDTO>> Get()
{
    var client = HttpClientFactory.Create(new UnwrappingResponseHandler());
    var httpResponse = await client.GetAsync("https://localhost:5001/api/v1/persons");

    IEnumerable<PersonDTO> persons = null;
    if (httpResponse.IsSuccessStatusCode)
    {
        var jsonString = await httpResponse.Content.ReadAsStringAsync();
        persons = JsonSerializer.Deserialize<IEnumerable<PersonDTO>>(jsonString);
    }

    return persons;
}
```

You can also pass the matching property to the handler like in the following:

```csharp
var client = HttpClientFactory.Create(new UnwrappingResponseHandler("payload"));
```
That's it. If you used [AutoWrapper](https://github.com/proudmonkey/AutoWrapper) or if you find this useful, please give it a star to show your support and share it to others. :)
