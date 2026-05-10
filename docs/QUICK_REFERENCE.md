# Garyon Quick Reference

Use this page for the fast path. For broader context, see [Documentation](DOCUMENTATION.md) and the [User Guides](guides/index.md).

## Installation

```bash
dotnet add package Garyon
```

## Common Patterns

### Collections

```csharp
using Garyon.Extensions;

var (min, max) = numbers.MinMax();
bool hasExactly5 = items.CountExactly(5);
bool hasAtLeast3 = items.CountAtLeast(3);
bool hasAtMost10 = items.CountAtMost(10);

dict.IncrementOrAddKeyValue("key");
var value = dict.ValueOrDefault("key", defaultValue);

var flat = nested2D.Flatten();
```

### Async

```csharp
using Garyon.Extensions;

await task.NoContext;
var result = await GetDataAsync().NoContext;
```

### Comparison

```csharp
using Garyon.Extensions.Comparison;

public int CompareTo(Person other)
{
    return this.BeginCompare(other)
        .By(p => p.LastName)
        .ThenBy(p => p.FirstName)
        .ThenByDesc(p => p.Age)
        .Result;
}
```

## Build The Documentation

From the repository root:

```powershell
.\docs\build-docs.ps1 -Serve
```

Or:

```bash
chmod +x docs/build-docs.sh
./docs/build-docs.sh --serve
```

Then open `http://localhost:8080`.

To use a custom port, explicitly serve the /docs/_site folder with the port of your choice, such as 9301:

```sh
docfx serve /docs/_site -p 9301
```
