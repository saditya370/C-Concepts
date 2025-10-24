# 📁 File I/O and Streams in C#

Welcome to the **C# File I/O and Streams Learning Playground** — a hands-on, Feynman-style project to master file system operations in C#. Learn how to create, read, write, append, and log data safely and efficiently.

---

## 🎯 Purpose

This demo covers the essentials of file handling in C#, including:

- File creation and writing
- Reading text files
- Appending data safely
- Using `StreamReader` and `StreamWriter`
- Accessing file metadata with `FileInfo`
- Building a logging system
- Exception handling for robust file operations

---

## 🧩 Core Concepts

### 1️⃣ File Creation & Writing
**Question**: How do we create and write data to a file in C#?

**Code Example**:
```csharp
string[] students =
{
    "Alice,20,Mathematics",
    "Bob,21,Computer Science"
};
File.WriteAllLines(filePath, students);
Console.WriteLine("✅ File created and written!");
```

**Explanation**:
- `File.WriteAllLines()` creates a file if it doesn’t exist or overwrites it if it does.
- Ideal for quick, simple writes.

---

### 2️⃣ Reading Files Safely
**Question**: How do we read text files line by line?

**Code Example**:
```csharp
if (File.Exists(filePath))
{
    string[] lines = File.ReadAllLines(filePath);
    foreach (var line in lines)
        Console.WriteLine(line);
}
```

**Key Notes**:
- Always check `File.Exists()` to avoid `FileNotFoundException`.
- `File.ReadAllText()` is an alternative for reading entire files as a single string.

---

### 3️⃣ Appending Data
**Question**: How can we add new data without overwriting a file?

**Code Example**:
```csharp
File.AppendAllText(filePath, Environment.NewLine + "Eve,23,Physics");
```

**Key Notes**:
- Use `AppendAllText()` to add data to the end of a file.
- Use case: Adding log entries, new records, or incremental data.
- **Avoid** `WriteAllText()` unless you intend to overwrite.

---

### 4️⃣ StreamReader & StreamWriter
**Question**: Why use streams instead of direct `File` methods?

**Code Example**:
```csharp
using (StreamWriter writer = new StreamWriter(streamFile))
{
    writer.WriteLine("Hello from StreamWriter!");
}

using (StreamReader reader = new StreamReader(streamFile))
{
    string line;
    while ((line = reader.ReadLine()) != null)
        Console.WriteLine(line);
}
```

**Why it matters**:
- Streams are ideal for large files or fine-grained control.
- The `using` statement ensures files are closed properly, preventing locks or memory leaks.

---

### 5️⃣ File Info and Metadata
**Question**: How can we inspect details about a file?

**Code Example**:
```csharp
FileInfo info = new FileInfo(filePath);
Console.WriteLine($"Size: {info.Length} bytes");
Console.WriteLine($"Created: {info.CreationTime}");
```

**Use Case**: Monitor file changes, retrieve metadata, or generate reports.

---

### 6️⃣ Logging Example (Real-World Application)
**Question**: How do we build a simple log file system?

**Code Example**:
```csharp
using (StreamWriter writer = new StreamWriter(logPath, append: true))
{
    writer.WriteLine($"{DateTime.Now:G} - File updated.");
}
```

**What’s happening**:
- `append: true` preserves existing data.
- Timestamps are added for each entry.
- Perfect for debugging or tracking system activity.

---

### 7️⃣ Exception Handling
**Question**: What if we try to read a file that doesn’t exist?

**Code Example**:
```csharp
try
{
    string content = File.ReadAllText("no_file.txt");
}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"❌ File not found: {ex.Message}");
}
```

**Key Notes**:
- Always wrap file operations in `try/catch` for robustness.
- Common exceptions: `FileNotFoundException`, `UnauthorizedAccessException`, `IOException`.

---

## 🧠 Feynman-Style Q&A

| Question | Answer |
|----------|--------|
| ❓ What’s the difference between `File` and `StreamWriter`? | `File` is for quick, small operations; `StreamWriter` is for larger or continuous writes. |
| ❓ Why use `using` with streams? | It automatically disposes of the file handle, preventing memory leaks or file locks. |
| ❓ What happens if you forget `using`? | The file may remain open (locked) until garbage collection, risky in long-running apps. |
| ❓ Can we write binary data instead of text? | Yes, with `FileStream` and `BinaryWriter`. |
| ❓ How to check if a file exists before reading? | Use `File.Exists(path)` to prevent exceptions. |
| ❓ Why do we need exception handling here? | File operations are external and unpredictable (e.g., missing files, permissions). |
| ❓ What’s the difference between `WriteAllText` and `AppendAllText`? | `WriteAllText` overwrites; `AppendAllText` adds content at the end. |

---

## 🧩 Related Concepts to Learn Next
- `BinaryReader` / `BinaryWriter` for binary files
- `FileStream` for low-level byte access
- Async file I/O with `StreamReader.ReadToEndAsync()`
- `DirectoryInfo` / `Directory` for folder-level operations
- `Path` class for cross-platform path building

---

## 🧱 Summary

| Concept       | API / Keyword                     | Use Case                     |
|---------------|-----------------------------------|------------------------------|
| Create / Write| `File.WriteAllText`, `File.WriteAllLines` | Save data             |
| Read          | `File.ReadAllText`, `File.ReadAllLines` | Load data             |
| Append        | `File.AppendAllText`              | Add data without overwriting |
| Streams       | `StreamReader`, `StreamWriter`    | Large file efficiency        |
| File Info     | `FileInfo`                        | Metadata and properties      |
| Logging       | `StreamWriter` (append mode)      | System logs                  |
| Safety        | `try/catch`, `File.Exists`        | Robust handling              |

**Core Idea**: *C# file I/O and streams make data persistence reliable, efficient, and safe for real-world applications.* 🚀