# Garyon

A C# library aiming to offer multi-dimensional functionality.

## Pre-Release Notice

This project is not yet ready for an actual release. All the code is still in pre-release, thus it might contain bugs, some APIs might be
renamed, refactored, reimplemented, or even **removed entirely** in future releases. Please keep that in mind and always ensure that your
implementations are up-to-date after updating the library.

After the initial release, there will be **no such issues**. All the new APIs will be properly tested, constructed and organized before
making it to production.

## What areas does it cover?

From `System.Console` to `System.Reflection`, or even custom implementations of common assets, this library can expand virtually every
single area the standard library does not fully cover.

# Contribution

Attempt to abide to the code style that the project's code already follows, ensure all changes are properly tested, and include test cases. More information is specifically
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

This library aims to offer peak performance for its implementations. Some leftover APIs are not offering the best performance possible, and will be taken care of as the library is being maintained.
