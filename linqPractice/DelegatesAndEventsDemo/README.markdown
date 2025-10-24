# 🎯 Delegates & Events in C#

Welcome to the **C# Delegates & Events Learning Playground** — a hands-on, Feynman-style project designed to help you master delegates and events for dynamic, event-driven programming in C#.

---

## 🌟 Overview

This demo explores **delegates** and **events**, key C# features that enable flexible, type-safe method invocation and publisher-subscriber patterns. Learn how to create dynamic behavior and build responsive applications.

**What You'll Learn**:
- Delegates as type-safe method pointers
- Multicast delegates for invoking multiple methods
- Anonymous methods and lambdas for concise code
- Events as a publisher-subscriber mechanism
- Real-world scenarios (e.g., Temperature Monitor, Bank Account)

---

## 🧩 Core Concepts

### 1️⃣ What is a Delegate?
**Definition**: A delegate is a type-safe reference to a method, acting like a variable that holds a method's address.

**Code Example**:
```csharp
public delegate double MathOperation(double x, double y);

MathOperation op = Add;
Console.WriteLine(op(10, 5)); // Calls Add method
```

**Why it matters**: Delegates treat methods as first-class citizens, allowing dynamic invocation, passing, or combining methods.

---

### 2️⃣ Multicast Delegates
**Concept**: A delegate can point to multiple methods, invoking them sequentially.

**Code Example**:
```csharp
MathOperation multiOp = Add;
multiOp += Multiply;
multiOp(3, 4); // Calls Add, then Multiply
```

**Key Notes**:
- Only the last method’s return value is received.
- Ideal for notifying multiple subscribers (e.g., logging, UI updates).

---

### 3️⃣ Anonymous Methods & Lambdas
**Concept**: Define delegate logic inline without naming separate methods.

**Code Example**:
```csharp
MathOperation divide = (x, y) =>
{
    if (y == 0) return 0;
    return x / y;
};
```

**Why use them**: Lambdas and anonymous methods make code concise, especially for short logic blocks.

---

### 4️⃣ Events — The “Publish/Subscribe” Mechanism
**Concept**: Events notify subscribers when something happens, implementing the publisher-subscriber pattern.

**Example**: Temperature Monitor
```csharp
public class TemperatureMonitor
{
    public delegate void ThresholdReachedHandler(double temperature);
    public event ThresholdReachedHandler OnThresholdReached;

    private const double Threshold = 50.0;

    public void CheckTemperature(double temp)
    {
        Console.WriteLine($"Temp: {temp}°C");
        if (temp > Threshold)
        {
            Console.WriteLine("⚠️ Threshold exceeded!");
            OnThresholdReached?.Invoke(temp);
        }
    }
}
```

**Subscribers**:
```csharp
monitor.OnThresholdReached += AlertEmail;
monitor.OnThresholdReached += AlertSMS;
monitor.OnThresholdReached += (temp) => Console.WriteLine($"Logging {temp}°C");
```

**Real-world use**: Powers UI button clicks, timers, and data change notifications in .NET.

---

### 5️⃣ Real-World Example — Bank Account
**Concept**: Events allow objects to broadcast changes to subscribers safely.

**Code Example**:
```csharp
public class BankAccount
{
    public delegate void BalanceChangedHandler(double newBalance);
    public event BalanceChangedHandler BalanceChanged;

    private double _balance;

    public BankAccount(double initial) { _balance = initial; }

    public void Deposit(double amount)
    {
        _balance += amount;
        Console.WriteLine($"Deposited: {amount:C}");
        BalanceChanged?.Invoke(_balance);
    }

    public void Withdraw(double amount)
    {
        if (amount > _balance)
        {
            Console.WriteLine("❌ Insufficient balance!");
            return;
        }
        _balance -= amount;
        Console.WriteLine($"Withdrawn: {amount:C}");
        BalanceChanged?.Invoke(_balance);
    }
}
```

**Why it matters**: Events notify all subscribers (e.g., UI, logs) whenever the balance changes.

---

## 🧩 Concept Summary

| Concept                | Description                              | Real-World Analogy                 |
|------------------------|------------------------------------------|------------------------------------|
| Delegate               | Type-safe reference to a method          | A “remote control” for a method     |
| Multicast Delegate     | Invokes multiple methods                 | One button turning on multiple lights |
| Lambda / Anonymous Method | Inline delegate without name          | Quick one-off behavior             |
| Event                  | Notification system for subscribers      | “Observer pattern” or “doorbell”   |

---

## 🧠 Feynman-Style Q&A

| Question | Answer |
|----------|--------|
| ❓ Why use delegates instead of just calling a method? | Delegates enable flexible, runtime-decided method invocation. |
| ❓ What makes an event different from a delegate? | Events restrict access — subscribers can only add/remove handlers, not invoke directly. |
| ❓ Can multiple subscribers listen to one event? | Yes, all attached handlers are invoked when the event fires. |
| ❓ What happens if no one subscribes? | The event is null, so check with `?.Invoke` before firing. |
| ❓ How do lambdas simplify delegate usage? | They eliminate the need for separate named methods for short actions. |
| ❓ Why use events in GUI or enterprise apps? | They decouple components — the publisher doesn’t need to know who’s listening. |

---

## 🧪 Suggested Exercises

1. Create a new delegate and assign multiple methods to it. Observe the order of invocation.
2. Add a new event to `TemperatureMonitor` for low temperatures (e.g., below 0°C).
3. Modify the `BankAccount` class to include an event for insufficient funds during withdrawal.
4. Replace a named method in the `TemperatureMonitor` example with a lambda expression.

---

## 🧩 Related Concepts to Learn Next
- `Func<>` and `Action<>` built-in generic delegates
- `EventHandler` and `EventArgs` standard pattern
- Multithreaded event handling
- Using delegates with LINQ (`Func<T>` in queries)

**Core Idea**: *Delegates and events make your code dynamic, decoupled, and ready for real-world applications.* 🚀