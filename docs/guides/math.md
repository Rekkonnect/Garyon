# Math Utilities

Garyon provides a small `Garyon.Mathematics` namespace with math-focused helpers and extensions.

## Overview

This guide covers the following types:

- `GeneralMath` (power, factorials, Min/Max)
- `SequencesMath` (integer sequence sums)
- `MathConstants` (common constants)
- `Overflowing` (overflow/underflow checks)
- `MathNumberExtensions` (numeric extension methods like `.Square()`, `.Sqrt()`, `.Log()`)
- `MathExtensions` (adds methods onto `System.Math` via C# 14 extension members)
- `BinaryExtensions` (flag helpers via C# 14 extension members; only in builds with generic math enabled)

## GeneralMath

### Integer Power

`GeneralMath.Power` computes integer powers using integer arithmetic.

```csharp
using Garyon.Mathematics;

int a = GeneralMath.Power(2, 10);     // 1024
long b = GeneralMath.Power(2L, 30L);  // 1073741824
```

Notes (as implemented):

- Negative exponents return `0`.
- `0^0` throws `InvalidOperationException`.

### Factorials

Factorials are available as `long`, `double`, and `BigInteger`:

```csharp
using Garyon.Mathematics;
using System.Numerics;

long small = GeneralMath.Factorial(12);                 // 479001600
double rounded = GeneralMath.Factorial(5.2);            // rounds via Math.Round(...)
BigInteger big = GeneralMath.FactorialBigInteger(50);   // 50!
```

### Min/Max Helpers

`GeneralMath.Min(...)` / `GeneralMath.Max(...)` provide overloads for many numeric types (and, in some builds, generic math overloads).

```csharp
using Garyon.Mathematics;

int min = GeneralMath.Min(5, 2, 9); // 2
int max = GeneralMath.Max(5, 2, 9); // 9
```

Note (as implemented): when the enumerable overloads receive an empty sequence, they return the type sentinel (`MinValue` for `Min`, `MaxValue` for `Max`).

## SequencesMath

`SequencesMath` currently provides arithmetic series sums:

```csharp
using Garyon.Mathematics;

int sum1To100 = SequencesMath.Sum(100);       // 1 + 2 + ... + 100
int sum10To20 = SequencesMath.Sum(10, 20);    // 10 + 11 + ... + 20
```

In builds with generic math enabled, `SequencesMath.Sum<T>(...)` is also available for `IBinaryInteger<T>` inputs.

## MathConstants

`MathConstants` provides a handful of common constants:

```csharp
using Garyon.Mathematics;

double circle = MathConstants.TwoPi;
double sin45 = MathConstants.Sin45;
double tan30 = MathConstants.Tan30;
```

## Overflowing

`Overflowing` helps predict whether an integer addition or multiplication would overflow/underflow.

```csharp
using Garyon.Mathematics;

bool addOverflows = Overflowing.CheckIfAdditionOverflows(int.MaxValue, 1); // true
bool mulOverflows = Overflowing.CheckIfMultiplicationOverflows(50_000, 50_000);
```

## MathNumberExtensions

These are standard extension methods on numeric types:

```csharp
using Garyon.Mathematics;

int squaredInt = 12.Square();
double squaredDouble = 12.5.Square();

double pow = 2.0.Pow(10);      // Math.Pow shorthand
double root = 9.0.Sqrt();      // Math.Sqrt wrapper
double ln = 10.0.Log();        // Math.Log wrapper
double log10 = 100.0.Log10();  // Math.Log10 wrapper
```

Some methods are available only on certain targets (for example `Log2`, `ILogB`, `Sqrt(float)`, and generic-math-only helpers like `Halve<T>`).

## MathExtensions (C# 14)

> [!VERSION]
> **C# 14 required:** `MathExtensions` uses C# 14 extension members (`extension(Math)`), enabling `Math.Square(...)`.

`MathExtensions` adds a `Square(...)` method onto `System.Math` via C# 14 extension members:

```csharp
using System;
using Garyon.Mathematics;

int squared = Math.Square(12);
```

## BinaryExtensions (C# 14; generic math builds)

> [!VERSION]
> **C# 14 required:** `BinaryExtensions` uses C# 14 extension members (`extension<T>(...)`). Availability also depends on generic math support.

`BinaryExtensions` adds flag helpers onto bitwise-capable numeric types (only in builds where generic math is enabled):

```csharp
using Garyon.Mathematics;

int value = 0b0110;
bool has2 = value.HasFlag(0b0010);
int without2 = value.RemoveFlag(0b0010);
```

## API Reference

- [Garyon.Mathematics](../api/Garyon.Mathematics.yml)
- [GeneralMath](../api/Garyon.Mathematics.GeneralMath.yml)
- [SequencesMath](../api/Garyon.Mathematics.SequencesMath.yml)
- [MathConstants](../api/Garyon.Mathematics.MathConstants.yml)
- [Overflowing](../api/Garyon.Mathematics.Overflowing.yml)
- [MathNumberExtensions](../api/Garyon.Mathematics.MathNumberExtensions.yml)
- [MathExtensions](../api/Garyon.Mathematics.MathExtensions.yml)
- [BinaryExtensions](../api/Garyon.Mathematics.BinaryExtensions.yml)
