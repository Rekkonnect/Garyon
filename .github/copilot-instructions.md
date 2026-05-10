# Copilot Instructions

## Project Guidelines
- When asked to refactor tests, continue through the full requested split instead of stopping after an initial partial pass.
- In tests, materialize spans and other ref structs into non-ref structs before any await/yield boundary.
- Prefer collection expressions instead of `new[] { ... }` and `Array.Empty<T>()`.