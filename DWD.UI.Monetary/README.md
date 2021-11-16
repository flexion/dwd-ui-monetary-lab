# Monetary Service
## Introduction
A preliminary service that exposes some public domain calculations related to DWD monetaries.  While this service is functional, and allows clients to submit requests and receive results, we expect this service to be refactored in the near future.  For example, it is possible that many of the endpoints will be removed and instead will be used internally by more coarse grained monetary operations such as CheckEligibility, EstablishMonetary, and RecalculateMonetary.  No final decision has been made, but any consumer of this service should anticipate it could change without notice.

## Solution Structure
* /DWD.UI.Monetary.Domain/ - C# .net class library containing monetary business logic in the form of business entities and use cases.
* /DWD.UI.Monetary.Service/ - C# .net asp.net core web api containing the web api layer in the form of controllers and mappers.
* /DWD.UI.Monetary.Tests/ - C# xunit test library containing unit tests.

## Dependencies
* .net 5 (base framework)
* asp.net core 5 (rest api)
* coverlet 3.0.2 (code coverage)
* swashbuckle 5.6.3 (swagger)
* xunit 2.4.1 (unit testing)

## Building and Testing
### Install .net 5 sdk
Install the [.net 5 sdk](https://dotnet.microsoft.com/download/dotnet/5.0).
### Build on command line
Please run the following command, from /DWD.UI.Monetary/, to build the software:
```
dotnet build
```
### Test on command line
Please run the following command, from /DWD.UI.Monetary/, to test the software:
```
dotnet test
```
## Executing
Please run the following command, from /DWD.UI.Monetary/DWD.UI.Monetary.Service/, to run the software in a local web server:
```
dotnet run
```
