# ⚙️ Generic Constraints Demo (Beginner → Intermediate → Advanced)

Welcome to the **C# Generic Constraints Learning Playground** — a hands-on, Feynman-style demo that teaches you how to use **generic constraints** to make your generic code safer, clearer, and more powerful. Think of constraints as guardrails on a highway: they keep your generic types on track, ensuring only the right types are used! 🚗 This demo takes you from beginner basics to advanced multi-constraint scenarios.

---

## 🧩 Why Do We Need Constraints?

Generics let you write flexible, reusable code, but too much freedom can lead to chaos. Without constraints, a generic type `T` could be *anything* — `string`, `int`, `Dog`, or `Car` — which might not make sense for your logic.

**Example of the Problem**:
```csharp
public class Box<T>
{
    public void Print()
    {
        // ❌ Compiler error: T could be anything, so .Name isn’t guaranteed
        // Console.WriteLine(T.Name);
    }
}
```

**With a Constraint**:
```csharp
public class Box<T> where T : Person
{
    public void Print(T obj)
    {
        // ✅ Safe: T is guaranteed to have a Name property
        Console.WriteLine(obj.Name);
    }
}
```

**Why Constraints Matter**:
- Ensure **type safety** by restricting allowed types.
- Guarantee access to specific members (e.g., methods, properties).
- Make code **self-documenting** and easier to understand.

---

## 🧱 Core Concepts

### 1️⃣ Basic — Generic Class with No Constraints
**Concept**: A simple generic class that accepts any type.

**Code Example**:
```csharp
public class GenericBox<T>
{
    private T _item;
    public GenericBox(T item) { _item = item; }
    public void Show() { Console.WriteLine($"Box contains: {_item}"); }
}
```

**Usage**:
```csharp
var intBox = new GenericBox<int>(42);
var strBox = new GenericBox<string>("Hello Generics");
intBox.Show(); // Box contains: 42
strBox.Show(); // Box contains: Hello Generics
```

**Key Notes**:
- `T` is a type placeholder for *any* type.
- Type-safe alternative to `object`, eliminating casting.
- No constraints = maximum flexibility, but limited member access.

---

### 2️⃣ Intermediate — Reference Type Constraint (`where T : class`)
**Concept**: Restrict `T` to reference types (e.g., classes, interfaces).

**Code Example**:
```csharp
public class Repository<T> where T : class
{
    private List<T> _items = new List<T>();
    public void Add(T item) { _items.Add(item); }
    public void DisplayAll() { foreach (var i in _items) Console.WriteLine(i); }
}
```

**Usage**:
```csharp
Repository<Student> repo = new Repository<Student>();
repo.Add(new Student { Name = "Alice", Age = 22 });
repo.Add(new Student { Name = "Bob", Age = 23 });
repo.DisplayAll();
```

**Why `class`?**:
- Prevents value types (e.g., `int`, `double`) from being used.
- Common in repositories, caches, or service layers.

---

### 3️⃣ Intermediate — Value Type Constraint (`where T : struct`)
**Concept**: Restrict `T` to value types (e.g., `int`, `double`, `DateTime`).

**Code Example**:
```csharp
public class Calculator<T> where T : struct
{
    private T _a, _b;
    public Calculator(T a, T b) { _a = a; _b = b; }
    public void ShowSum() { dynamic x = _a, y = _b; Console.WriteLine($"Sum = {x + y}"); }
}
```

**Usage**:
```csharp
Calculator<int> calc = new Calculator<int>(5, 10);
calc.ShowSum(); // Sum = 15
```

**Why `struct`?**:
- Ensures only value types are used.
- Useful for numeric or struct-like operations.
- `dynamic` is used here for addition, as `T` doesn’t guarantee an `+` operator.

---

### 4️⃣ Intermediate — Default Constructor Constraint (`where T : new()`)
**Concept**: Require `T` to have a public parameterless constructor.

**Code Example**:
```csharp
public class Factory<T> where T : new()
{
    public T Create()
    {
        return new T(); // ✅ Safe because T has a parameterless constructor
    }
}
```

**Usage**:
```csharp
public class SimplePerson { public string Name { get; set; } public void Introduce() { Console.WriteLine($"Hi, I'm {Name}!"); } }
Factory<SimplePerson> factory = new Factory<SimplePerson>();
SimplePerson p = factory.Create();
p.Name = "Charlie";
p.Introduce(); // Hi, I'm Charlie!
```

**Why `new()`?**:
- Allows safe instantiation of `T`.
- Common in factories, object pools, or dependency injection.

---

### 5️⃣ Advanced — Inheritance & Interface Constraints
**Concept**: Restrict `T` to types that inherit a base class and/or implement an interface.

**Code Example**:
```csharp
public interface IStudent { void Study(); }
public class BaseStudent { public string Name { get; set; } }
public class SpecialStudent : BaseStudent, IStudent
{
    public string Grade { get; set; }
    public void Study() { Console.WriteLine($"{Name} is studying hard!"); }
}

public class Processor<T> where T : BaseStudent, IStudent
{
    public void RunProcess(T student)
    {
        Console.WriteLine($"Processing {student.Name}");
        student.Study();
    }
}
```

**Usage**:
```csharp
Processor<SpecialStudent> processor = new Processor<SpecialStudent>();
processor.RunProcess(new SpecialStudent { Name = "David", Grade = "A+" });
// Processing David
// David is studying hard!
```

**Why multiple constraints?**:
- Ensures `T` has specific base class behavior and interface methods.
- Order doesn’t matter, but `class` or `struct` must come first if used.

---

### 6️⃣ Advanced — Multiple Constraints in One
**Concept**: Combine inheritance, interface, and constructor constraints for maximum control.

**Code Example**:
```csharp
public class MultiConstraintHandler<T> where T : BaseStudent, IStudent, new()
{
    public void Handle(T obj)
    {
        Console.WriteLine("Handling multiple constraint object...");
        obj.Study();
        Console.WriteLine($"Created new instance of {typeof(T).Name} successfully!");
    }
}
```

**Usage**:
```csharp
MultiConstraintHandler<SpecialStudent> handler = new MultiConstraintHandler<SpecialStudent>();
handler.Handle(new SpecialStudent { Name = "Eve", Grade = "A" });
// Handling multiple constraint object...
// Eve is studying hard!
// Created new instance of SpecialStudent successfully!
```

**Why combine them?**:
- Ensures `T` inherits from a base class, implements an interface, and can be instantiated.
- Ideal for complex scenarios requiring strict type guarantees.

---

## 🧩 Summary Table

| Constraint Type | Syntax | Purpose | Example Type |
|----------------|--------|---------|--------------|
| **Reference Type** | `where T : class` | Only reference types allowed | `Repository<Student>` |
| **Value Type** | `where T : struct` | Only value types allowed | `Calculator<int>` |
| **Default Constructor** | `where T : new()` | Must have a public parameterless constructor | `Factory<Person>` |
| **Inheritance** | `where T : BaseClass` | Must inherit from a base class | `Processor<Student>` |
| **Interface** | `where T : IInterface` | Must implement a specific interface | `Processor<IWorker>` |
| **Multiple** | `where T : BaseClass, IInterface, new()` | Combines rules for safety | `MultiHandler<Worker>` |

---

## 🧠 Common Interview Q&A

| Question | Answer |
|----------|--------|
| ❓ **Can I use multiple constraints?** | Yes, separate them with commas, e.g., `where T : BaseClass, IInterface, new()`. |
| ❓ **Why not just use `object` for everything?** | Using `object` loses type safety, requiring casts and risking runtime errors. Generics with constraints maintain compile-time safety. |
| ❓ **What’s the difference between `where T : class` and `where T : BaseClass`?** | `class` allows any reference type; `BaseClass` restricts to types inheriting from `BaseClass`. |
| ❓ **Can I use constraints on methods too?** | Yes, e.g., `public void Print<T>(T item) where T : IFormattable`. |
| ❓ **Why use `where T : new()`?** | It ensures `T` can be instantiated with a parameterless constructor, useful for factories or object creation. |
| ❓ **Can constraints be used with interfaces?** | Yes, `where T : IInterface` ensures `T` implements the specified interface. |
| ❓ **What happens if a type doesn’t meet the constraint?** | The compiler throws an error, preventing invalid types at compile time. |
| ❓ **Can I constrain `T` to sealed types?** | Yes, as long as the sealed type meets the constraint (e.g., `string` for `class` or `IComparable`). |

---

## 🧪 Practice Challenges

1. Create a generic `Printer<T>` class with a `where T : IPrintable` constraint, where `IPrintable` has a `Print()` method.
2. Modify `Calculator<T>` to work only with `IComparable` types and sort two values.
3. Create a generic `Pool<T>` class with `where T : new()` to manage a reusable object pool.
4. Combine constraints to make a `Manager<T>` class that requires `T` to inherit from `Employee` and implement `IWorkable`.

---

## 🧱 File Overview

| File | Purpose |
|------|---------|
| `GenericConstraintsDemo.cs` | Defines and demonstrates generic constraints from basic to advanced |
| `Program.cs` | Calls `GenericConstraintsDemo.Run()` |
| `linqPractice` namespace | Organizes demos in a consistent structure |

---

## ✅ Key Takeaways

- **Generic constraints** add guardrails to generics, ensuring type safety and member access.
- **From basic to advanced**: Use `class`, `struct`, `new()`, base classes, or interfaces to control `T`.
- **Real-world power**: Constraints make code self-documenting and robust for repositories, factories, and processors.
- **LINQ connection**: Many LINQ methods use constraints like `IComparable` or `IEnumerable<T>`.

**Core Idea**: *Generic constraints turn flexible generics into safe, powerful tools for building robust C# applications!* 🚀
