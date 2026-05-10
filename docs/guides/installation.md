# Installation Guide

## Prerequisites

- Any of the following frameworks:
  - .NET Standard 2.0 or higher
  - .NET Core 3.1 or higher
  - .NET 5.0 or higher

By supporting .NET Standard 2.0, the library is also available for projects
targeting .NET Framework 4.6.1 and higher, or .NET Core 2.0 and higher
([source](https://learn.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-0#select-net-standard-version)).

## NuGet Package

Install Garyon via the NuGet Package Manager:

### Package Manager Console
```powershell
Install-Package Garyon
```

### .NET CLI
```bash
dotnet add package Garyon
```

### PackageReference
Add the following to your `.csproj` file:

```xml
<PackageReference Include="Garyon" Version="0.5.0" />
```

## Verify Installation

After installation, verify Garyon is available by adding a using directive:

```csharp
using Garyon;
using Garyon.Extensions;
using Garyon.Objects;
```

Try using a simple extension:

```csharp
using Garyon.Extensions;

var numbers = new[] { 1, 2, 3, 4, 5 };
var min = numbers.MinMax(); // Returns (1, 5)
```

## Next Steps

- [Quick Start Guide](quick-start.md)
- [API Reference](../api/toc.yml)
