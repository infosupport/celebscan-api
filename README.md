# Celebscan API

[![Stories in Ready](https://badge.waffle.io/infosupport/celebscan-api.png?label=Status:%20Available&title=Ready)](https://waffle.io/infosupport/celebscan-api?utm_source=badge)
[![Build Status](https://travis-ci.org/infosupport/celebscan-api.svg?branch=master)](https://travis-ci.org/infosupport/celebscan-api)

Cache + Image proxy for the celebscan application. This application is used by the 
[Celebscan](https://github.com/infosupport/celebscan) application as a wikipedia cache and image proxy.

## Prerequisites 
Make sure you have .NET core 1.1 installed with the SDK installed on your machine.
If you're running on Windows it comes with VS2017. You can also install the SDK on its own.

## Essential configuration
If you want to run this application in production, be sure to include a 
file `src/Celebscan.Service/appsettings.Production.json` with the following settings:

```
{
  "ApplicationInsights": {
   "InstrumentationKey": "..."
  }
}
```

The instrumentation key is the key you get from the Application Insights instance on Azure.
If you don't want to use Application Insights, you can leave these settings out.

## Installation
Run the following commands to build the application:

 - `git clone <repository-url>` this repository
 - `cd celebscan-api`
 - `dotnet restore`
 - `dotnet build -c Release`

## Running / Development
 - `cd src/Celebscan.Service`
 - `dotnet run`

### Testing
 - `cd test/Celebscan.Service.Tests`
 - `dotnet test`

### Building
 - `dotnet build` (development)
 - `dotnet build -c Release` (production)
