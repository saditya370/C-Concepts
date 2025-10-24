# 📘 OOP Concepts in C# (.NET Framework 4.8)

Welcome to the **C# OOP Learning Playground** — a hands-on, Feynman-style project to master the four pillars of Object-Oriented Programming (OOP) in C#, plus interfaces as a key related concept.

---

## 🌟 Overview

This demo explores the core principles of OOP in C#, providing practical examples and exercises to deepen your understanding of **Encapsulation**, **Inheritance**, **Polymorphism**, **Abstraction**, and **Interfaces**.

| Concept       | Description                                   | Example                     |
|---------------|-----------------------------------------------|-----------------------------|
| Encapsulation | Hiding data and exposing controlled access    | Private fields + public properties |
| Inheritance   | Reusing base class behavior                   | `Manager` inherits `Employee` |
| Polymorphism  | Same interface, different implementation      | `CalculatePay()`            |
| Abstraction   | Showing what to do, not how                   | Abstract methods            |
| Interface     | Defining contracts for unrelated classes      | `IWork`                     |

---

## 🧱 Core OOP Concepts

### 1️⃣ Encapsulation
**Goal**: Protect internal data and expose only what’s needed.

**Code Example**:
```csharp
private int _id;
public int Id 
{ 
    get { return _id; } 
    set { if (value > 0) _id = value; } 
}
```

**Benefits**:
- Prevents direct manipulation of internal data.
- Ensures data consistency.
- Provides a single point of control via getters/setters.

---

### 2️⃣ Inheritance
**Goal**: Reuse and extend functionality from a base class.

**Code Example**:
```csharp
public abstract class Employee { ... }
public class FullTimeEmployee : Employee { ... }
public class Manager : FullTimeEmployee { ... }
```

**Benefits**:
- Promotes code reuse.
- Establishes a logical hierarchy (is-a relationship).
- Simplifies maintenance and extension.

---

### 3️⃣ Polymorphism
**Goal**: Enable one interface with multiple implementations.

**Code Example**:
```csharp
Employee emp = new PartTimeEmployee(...);
emp.CalculatePay(); // Calls derived version dynamically
```

**Benefits**:
- Enhances code flexibility.
- Simplifies extension.
- Encourages abstraction and modular design.

---

### 4️⃣ Abstraction
**Goal**: Expose what an object does, not how it does it.

**Code Example**:
```csharp
public abstract class Employee
{
    public abstract void CalculatePay();
}
```

**Benefits**:
- Simplifies usage for consumers.
- Reduces complexity.
- Hides implementation details.

---

### 5️⃣ Interface
**Goal**: Define contracts that multiple classes can implement.

**Code Example**:
```csharp
public interface IWork
{
    void DoWork();
}
```

**Benefits**:
- Enables multiple inheritance of behavior.
- Promotes flexibility and decoupling.

---

## 🧠 Feynman-Style Q&A

| Question | Answer |
|----------|--------|
| ❓ What’s the difference between abstraction and encapsulation? | Encapsulation hides data; abstraction hides implementation details. |
| ❓ Why use abstract classes over interfaces? | Abstract classes can contain shared logic; interfaces only define contracts. |
| ❓ What’s method overriding vs. method hiding? | Overriding replaces base behavior; hiding redefines a method with `new`. |
| ❓ Why is polymorphism powerful? | It allows code to work with general types (`Employee`) while executing specific behavior. |
| ❓ Can a class inherit multiple interfaces? | Yes, but it can only inherit from one base class. |

---

## 🧪 Suggested Exercises

1. Create an `Intern : Employee` class with a stipend-based `CalculatePay()` method.
2. Modify the `Manager` class to include a `TeamSize` property and adjust pay calculation.
3. Add an `ISpeak` interface with a `Speak()` method, and make employees introduce themselves.
4. Create an array of `Employee` objects and loop through it to observe polymorphism in action.

---

## 🧭 Summary

| Concept       | Keyword             | Purpose                       |
|---------------|---------------------|-------------------------------|
| Encapsulation | `private`, `get/set`| Protect data                  |
| Inheritance   | `:`                 | Reuse and extend behavior     |
| Polymorphism  | `virtual`, `override`| Flexible, dynamic behavior   |
| Abstraction   | `abstract`          | Simplify design               |
| Interface     | `interface`         | Define contracts              |

**Core Idea**: *In OOP, you design code like the real world — defining roles, relationships, and responsibilities.* 🚀