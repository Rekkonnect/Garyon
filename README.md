# Garyon

A C# library aiming to offer multi-dimensional functionality.

## Why is it here?

Many parts of the standard library are either incomplete or not as well-designed. This library offers further helpers and overall
functionality with its code, preventing the user from needing to write commonly found boilerplate code for frequent tasks. Many projects
include some utility code that could serve general purpose, and is being reimplemented on a per-project basis, which should not happen.
Furthermore, a lot of the boilerplate code is not even nearly as optimized as it could be, while in some projects that need performance,
optimizations are either mediocre, or just too in-depth, consuming useful time from actually developing the intended project.

## What areas does it cover?

From `System.Console` to `System.Reflection`, or even custom implementations of common assets, this library can expand virtually every
single area the standard library does not fully cover.

# Contribution

Abide to the [code style](CodeStyle.md), ensure all additions are properly tested, and include test cases. More information is specifically
given under the Quality Control section:

# Quality Control

This library aims to offer high-performance utility code, thus everything needs to be properly tested and benchmarked to ensure that it
does have a significant improvement over other solutions. This is why the solution contains the
```
Garyon.QualityControl
Garyon.Benchmarks
Garyon.Tests
```
projects.

- `Garyon.QualityControl` is the base project, which the other two depend on. It is meant to contain shared assets between tests and
benchmarks. 
- `Garyon.Benchmarks` contains the benchmarks that are being run in order to test performance of the functionality that is being provided.
- `Garyon.Tests` contains test cases for each single non-obviously correct function to ensure the utilities are correctly implemented in
every case. Every single test must cover all edge cases, to never come across potential regressions.

# Performance

To ensure performance is peak, SIMD must be taken into consideration. There are numerous instructions that fall under that category, and
offer great performance improvements when used in an impactful amount. Repetitive calls to small functions should be inlined, whereas
much less frequently used functions need not be inlined, neither optimized, for long as they do not completely ruin performance.
