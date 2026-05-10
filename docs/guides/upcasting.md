# Upcasting

Most generic collection interfaces in .NET are invariant (for example `IList<T>`, `ICollection<T>`, `IDictionary<TKey, TValue>`). Even if `Derived : Base`, an `IList<Base>` cannot be used as an `IList<Derived>`.

Garyon provides lightweight “upcasting” views for common collection shapes when you already know the runtime values are of a more specific type and want to work with that type without copying the collection.

## List Upcasting

```csharp
using Garyon.Extensions.Upcasting;

IList<BaseItem> items = [new DerivedItem("a"), new DerivedItem("b")];

IList<DerivedItem> derivedItems = items.UpcastList<BaseItem, DerivedItem>();
derivedItems.Add(new DerivedItem("c"));

Console.WriteLine(items.Count); // 3 (same backing list)
```

## Other Collection Shapes

`UpcastingCollectionExtensions` also supports:

- `ICollection<T>` / `IReadOnlyCollection<T>`
- `IDictionary<TKey, TValue>` / `IReadOnlyDictionary<TKey, TValue>` (values are cast; keys are unchanged)
- `ISet<T>` (and `IReadOnlySet<T>` on frameworks that provide it)

## Safety Notes

- These helpers do not validate that every element/value is of the requested type; mistyping elements may cause undefined behavior.
- The returned objects are views over the original collection: changes are reflected immediately.
- Upcasting is not the same as downcasting, where the opposite casting relationship would apply. Downcasting is not supported yet, but is planned for the future.

## API Reference

- [UpcastingCollectionExtensions](../api/Garyon.Extensions.Upcasting.UpcastingCollectionExtensions.yml)

