# 🧩 C# Generics Learning Playground

Welcome to the **C# Generics Practice Hub** — a hands-on, Feynman-style learning project designed to help you deeply understand C# generics rather than just memorizing syntax.

---

## 📘 Overview

This project explores **C# Generics**, a powerful feature for creating type-safe, reusable, and performant code. Generics allow you to write flexible classes, methods, and collections that work with any data type while maintaining compile-time safety.

---

## 🧩 Key Concepts

### 1️⃣ Non-Generic Collections
**Examples**: `ArrayList`, `Hashtable`

**Code Example**:
```csharp
ArrayList arr = new ArrayList { 10, "Hello", true };
Hashtable ht = new Hashtable { ["Id"] = 1, ["Name"] = "Alice" };
```

**Downsides**:
- No compile-time type checking.
- Requires boxing/unboxing (performance overhead).
- Slower and more error-prone.

**Recommendation**: Use only for legacy code or mixed-type scenarios.

---

### 2️⃣ Generic Collections
**Examples**: `List<T>`, `Dictionary<K,V>`, `Queue<T>`, `Stack<T>`, `HashSet<T>`

**Code Example**:
```csharp
List<int> numbers = new() { 1, 2, 3 };
Dictionary<int, string> dict = new() { [1] = "Alice" };
```

**Benefits**:
- Compile-time type safety.
- Better performance (no boxing/unboxing).
- Cleaner and more maintainable code.

**💡 Tip**: Prefer generics in all new code for safety and speed.

---

### 3️⃣ Custom Generic Class
**Concept**: Define your own generic class to handle any type dynamically.

**Code Example**:
```csharp
public class Box<T>
{
    public T Value { get; set; }
    public void Display() => Console.WriteLine($"Box<{typeof(T).Name}>: {Value}");
}
```

**Usage**:
```csharp
var intBox = new Box<int>(123);
var strBox = new Box<string>("Hello");
```

**Why it matters**: Promotes code reuse — the same class works for any type.

---

### 4️⃣ Generic Method
**Concept**: Write type-agnostic methods that adapt to any type.

**Code Example**:
```csharp
public static void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}
```

**Key Takeaway**: Generic methods enable reusable algorithms without code duplication.

---

## 💬 Feynman-Style Q&A

| # | Question | Answer |
|---|----------|--------|
| 1 | Why are non-generic collections slower? | They store values as `object`, causing boxing/unboxing overhead. |
| 2 | Why are generics type-safe? | The compiler enforces type consistency, preventing invalid casts. |
| 3 | What’s the purpose of `<T>` in generics? | `<T>` acts as a type placeholder, replaced with a real type at runtime. |
| 4 | What’s the difference between a generic method and a generic class? | A generic class works for one type per instance; a generic method can vary per call. |
| 5 | Why might `HashSet<T>` ignore duplicates? | It uses hashing to enforce uniqueness, like mathematical sets. |

---

## 🧪 Suggested Exercises

1. Create `Box<double>` and `Box<bool>`.  
   *What happens when you call `Display()`?*

2. Add more elements to a `HashSet<int>` and observe how duplicates behave.

3. Implement a `Pair<T1, T2>` class that holds two generic values.

4. Modify the `Swap<T>` method to print the type name of `T` during swapping.

---

## 🧩 Summary

| Concept | Key Idea |
|---------|----------|
| `ArrayList` / `Hashtable` | Unchecked, flexible, but unsafe and slow. |
| `List<T>` / `Dictionary<K,V>` | Type-safe, modern, and performant collections. |
| `Box<T>` | Custom generic container for reusability. |
| `Swap<T>` | Generic method for type-agnostic utility. |

**Core Idea**: *“Generics let you write once and reuse forever — safely.”* 🚀