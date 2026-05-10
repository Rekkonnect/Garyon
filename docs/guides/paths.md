# Typed Paths (FilePath & DirectoryPath)

`FilePath` and `DirectoryPath` are lightweight wrappers around a path `string`.

They are designed to be a **non-intrusive** way to work with paths:

- They **do not** check whether the file/directory exists.
- They **do not** touch the file system unless you explicitly call APIs that do.
- Their helpers use path-string manipulation (for example `System.IO.Path`).

This makes them a good fit for APIs that need to *accept and pass around* paths while keeping the IO boundary explicit.

## Why Not Just Use `string`?

Using dedicated types can help:

- Avoid mixing file paths and directory paths by accident.
- Improve readability of method signatures.
- Centralize common path operations (get directory, change extension, combine, etc.).

## FilePath

```csharp
using Garyon.Objects.IO;

FilePath file = @"C:\logs\app.txt";

// Path-only operations:
var dir = file.Directory;                 // DirectoryPath
var name = file.FileName;                 // "app.txt"
var stem = file.ExtensionlessFileName;    // "app"
var ext = file.Extension;                 // ".txt"

var json = file.WithExtension(".json");   // "C:\logs\app.json"
var renamed = file.WithFileName("x.txt"); // "C:\logs\x.txt"
```

## DirectoryPath

```csharp
using Garyon.Objects.IO;

DirectoryPath dir = @"C:\logs";

var parent = dir.Parent;          // DirectoryPath (or default if none)
var file = dir.File("app.txt");   // FilePath "C:\logs\app.txt"
```

## FileInfo / DirectoryInfo Wrappers

Both types expose `FileInfo` / `DirectoryInfo` wrappers:

```csharp
FileInfo info = file.FileInfo;
DirectoryInfo dirInfo = dir.DirectoryInfo;
```

Creating these wrappers is still a *path-only* operation. However, calling members like `Exists`, `Length`, `EnumerateFiles()`, etc. will involve the file system.

## API Reference

- [FilePath](../api/Garyon.Objects.IO.FilePath.yml)
- [DirectoryPath](../api/Garyon.Objects.IO.DirectoryPath.yml)

