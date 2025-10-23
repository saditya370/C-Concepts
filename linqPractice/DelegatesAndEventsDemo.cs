using System;

namespace linqPractice
{
    // ==================== DELEGATES & EVENTS DEMO ==================== //
    public static class DelegatesAndEventsDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== ⚡ Delegates & Events Demo =====\n");

            // 1️⃣ BASIC DELEGATE DEMO
            Console.WriteLine("=== 1️⃣ Basic Delegate Example ===");
            MathOperation op1 = Add;          // point to Add method
            MathOperation op2 = Subtract;     // point to Subtract method

            Console.WriteLine($"Add(10,5) = {op1(10, 5)}");
            Console.WriteLine($"Subtract(10,5) = {op2(10, 5)}");

            // 2️⃣ MULTICAST DELEGATE
            Console.WriteLine("\n=== 2️⃣ Multicast Delegate Example ===");
            MathOperation multiOp = Add;
            multiOp += Multiply; // add another method to the delegate
            multiOp(3, 4);       // both Add and Multiply execute

            // 3️⃣ ANONYMOUS METHOD / LAMBDA
            Console.WriteLine("\n=== 3️⃣ Anonymous Method / Lambda Example ===");
            MathOperation divide = (x, y) => y != 0 ? x / y : 0;
            Console.WriteLine($"Divide(20,5) = {divide(20, 5)}");

            // 4️⃣ EVENT DEMO - Temperature Monitor
            Console.WriteLine("\n=== 4️⃣ Event Example: Temperature Monitor ===");
            TemperatureMonitor monitor = new TemperatureMonitor();

            // Subscribe event handlers
            monitor.OnThresholdReached += AlertEmail;
            monitor.OnThresholdReached += AlertSMS;
            monitor.OnThresholdReached += (temp) => Console.WriteLine($"[Lambda Handler] Logging Temp: {temp}°C");

            // Simulate readings
            monitor.CheckTemperature(30);
            monitor.CheckTemperature(55);
            monitor.CheckTemperature(70);

            // 5️⃣ EVENT EXAMPLE 2 - Bank Account
            Console.WriteLine("\n=== 5️⃣ Event Example: Bank Account ===");
            BankAccount account = new BankAccount(500);
            account.BalanceChanged += balance => Console.WriteLine($"[Notifier] New Balance: {balance:C}");

            account.Deposit(200);
            account.Withdraw(150);
            account.Withdraw(700); // triggers insufficient balance event

            Console.WriteLine("\n===== END OF DELEGATES & EVENTS DEMO =====");
        }

        // =====================  DELEGATE DEFINITIONS  =====================
        // Delegate declaration (like defining a function signature)
        public delegate double MathOperation(double x, double y);

        // Normal methods that match delegate signature
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

        // =====================  EVENT EXAMPLE 1 =====================
        public class TemperatureMonitor
        {
            // Declare an event using a delegate
            public delegate void ThresholdReachedHandler(double temperature);
            public event ThresholdReachedHandler OnThresholdReached;

            private const double Threshold = 50.0;

            public void CheckTemperature(double temp)
            {
                Console.WriteLine($"[Monitor] Temperature: {temp}°C");
                if (temp > Threshold)
                {
                    Console.WriteLine("⚠️  Threshold exceeded!");
                    OnThresholdReached?.Invoke(temp); // Safe event call
                }
            }
        }

        // Event handlers (subscribers)
        public static void AlertEmail(double temp)
        {
            Console.WriteLine($"[Email Alert] Temperature is {temp}°C! Sending email...");
        }

        public static void AlertSMS(double temp)
        {
            Console.WriteLine($"[SMS Alert] Temperature is {temp}°C! Sending text message...");
        }

        // =====================  EVENT EXAMPLE 2 =====================
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
                BalanceChanged?.Invoke(_balance);
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
                BalanceChanged?.Invoke(_balance);
            }
        }
    }
}
