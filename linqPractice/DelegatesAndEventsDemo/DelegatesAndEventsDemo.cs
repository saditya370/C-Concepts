using System;

namespace linqPractice
{
    // ==================== ⚡ DELEGATES & EVENTS DEMO ==================== //
    // This demo covers:
    // 1. Basic delegate usage
    // 2. Multicast delegates
    // 3. Anonymous methods / Lambdas
    // 4. Events and event-driven programming
    // 5. Practical event scenarios
    public static class DelegatesAndEventsDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== ⚡ Delegates & Events Demo =====\n");

            // 1️⃣ BASIC DELEGATE DEMO
            Console.WriteLine("=== 1️⃣ Basic Delegate Example ===");
            MathOperation op1 = Add;       // points to Add method
            MathOperation op2 = Subtract;  // points to Subtract method

            Console.WriteLine($"Add(10,5) = {op1(10, 5)}");
            Console.WriteLine($"Subtract(10,5) = {op2(10, 5)}");

            // Why use delegates?
            // → They allow you to treat methods as variables — pass them as parameters or call them dynamically.


            // 2️⃣ MULTICAST DELEGATE
            Console.WriteLine("\n=== 2️⃣ Multicast Delegate Example ===");
            MathOperation multiOp = Add;
            multiOp += Multiply;  // Now both Add and Multiply methods are attached

            Console.WriteLine("Calling multicast delegate (3,4):");
            multiOp(3, 4);  // Invokes Add first, then Multiply

            // ⚠️ Note: If the delegate returns a value, only the *last method’s* return value is received.


            // 3️⃣ ANONYMOUS METHOD / LAMBDA
            Console.WriteLine("\n=== 3️⃣ Anonymous Method / Lambda Example ===");

            // Define a delegate instance using a lambda expression
            MathOperation divide = (x, y) =>
            {
                if (y == 0)
                {
                    Console.WriteLine("Cannot divide by zero!");
                    return 0;
                }
                return x / y;
            };

            Console.WriteLine($"Divide(20,5) = {divide(20, 5)}");

            // 💡 Lambdas make delegate usage concise — no need for named methods.


            // 4️⃣ EVENT DEMO - Temperature Monitor
            Console.WriteLine("\n=== 4️⃣ Event Example: Temperature Monitor ===");

            TemperatureMonitor monitor = new TemperatureMonitor();

            // Subscribe multiple handlers to the event
            monitor.OnThresholdReached += AlertEmail;
            monitor.OnThresholdReached += AlertSMS;
            monitor.OnThresholdReached += delegate (double temp)
            {
                Console.WriteLine($"[Anonymous Handler] Logging Temp: {temp}°C");
            };

            monitor.CheckTemperature(30);
            monitor.CheckTemperature(55);
            monitor.CheckTemperature(70);

            // 💡 Events use delegates to notify multiple subscribers safely.
            // They are the backbone of event-driven systems (e.g., button clicks, data updates).


            // 5️⃣ EVENT EXAMPLE 2 - Bank Account
            Console.WriteLine("\n=== 5️⃣ Event Example: Bank Account ===");

            BankAccount account = new BankAccount(500);
            account.BalanceChanged += delegate (double balance)
            {
                Console.WriteLine($"[Notifier] New Balance: {balance:C}");
            };

            account.Deposit(200);
            account.Withdraw(150);
            account.Withdraw(700); // triggers insufficient balance message

            Console.WriteLine("\n===== END OF DELEGATES & EVENTS DEMO =====");
        }

        // ===================== 🔹 DELEGATE DEFINITIONS =====================
        // Delegates define a *method signature* — any method with same signature can be assigned.
        public delegate double MathOperation(double x, double y);

        // Regular methods matching the delegate signature
        public static double Add(double a, double b)
        {
            double result = a + b;
            Console.WriteLine($"Add: {a} + {b} = {result}");
            return result;
        }

        public static double Subtract(double a, double b)
        {
            double result = a - b;
            Console.WriteLine($"Subtract: {a} - {b} = {result}");
            return result;
        }

        public static double Multiply(double a, double b)
        {
            double result = a * b;
            Console.WriteLine($"Multiply: {a} × {b} = {result}");
            return result;
        }


        // ===================== 🌡️ EVENT EXAMPLE 1 =====================
        // Demonstrates using events for a temperature threshold system.
        public class TemperatureMonitor
        {
            // Define delegate type for event
            public delegate void ThresholdReachedHandler(double temperature);

            // Declare event (based on the delegate)
            public event ThresholdReachedHandler OnThresholdReached;

            private const double Threshold = 50.0;

            public void CheckTemperature(double temp)
            {
                Console.WriteLine($"[Monitor] Temperature: {temp}°C");
                if (temp > Threshold)
                {
                    Console.WriteLine("⚠️  Threshold exceeded!");
                    if (OnThresholdReached != null)
                        OnThresholdReached(temp); // Trigger event
                }
            }
        }

        // Event Handlers (Subscribers)
        public static void AlertEmail(double temp)
        {
            Console.WriteLine($"[Email Alert] Temperature {temp}°C — Sending email notification...");
        }

        public static void AlertSMS(double temp)
        {
            Console.WriteLine($"[SMS Alert] Temperature {temp}°C — Sending text message...");
        }


        // ===================== 💰 EVENT EXAMPLE 2 =====================
        // Demonstrates using events for account balance updates.
        public class BankAccount
        {
            public delegate void BalanceChangedHandler(double newBalance);
            public event BalanceChangedHandler BalanceChanged;

            private double _balance;

            public BankAccount(double initial)
            {
                _balance = initial;
            }

            public void Deposit(double amount)
            {
                _balance += amount;
                Console.WriteLine($"Deposited: {amount:C}");
                if (BalanceChanged != null)
                    BalanceChanged(_balance);
            }

            public void Withdraw(double amount)
            {
                if (amount > _balance)
                {
                    Console.WriteLine($"❌ Insufficient balance to withdraw {amount:C}");
                    return;
                }

                _balance -= amount;
                Console.WriteLine($"Withdrawn: {amount:C}");
                if (BalanceChanged != null)
                    BalanceChanged(_balance);
            }
        }
    }
}
