using System;
using System.Collections.Generic;

namespace linqPractice
{
    /// <summary>
    /// ======================================
    /// 🧩 DESIGN PATTERNS DEMO
    /// ======================================
    /// Each section covers a core OOP design pattern with comments
    /// and real-world examples.
    /// </summary>
    public static class DesignPatternsDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== 🧩 DESIGN PATTERNS DEMO =====\n");

            SingletonDemo();
            FactoryDemo();
            StrategyDemo();
            ObserverDemo();
            RepositoryDemo();

            Console.WriteLine("\n===== ✅ END OF DESIGN PATTERNS DEMO =====");
        }

        // ============================================================
        // 1️⃣ SINGLETON PATTERN — One global instance
        // ============================================================
        private static void SingletonDemo()
        {
            Console.WriteLine("\n=== 1️⃣ Singleton Pattern ===");

            Logger logger1 = Logger.Instance;
            Logger logger2 = Logger.Instance;

            logger1.Log("App started");
            logger2.Log("Singleton confirmed — both loggers are same instance");

            Console.WriteLine($"logger1 == logger2? {ReferenceEquals(logger1, logger2)}\n");
        }

        // ============================================================
        // 2️⃣ FACTORY PATTERN — Centralized object creation
        // ============================================================
        private static void FactoryDemo()
        {
            Console.WriteLine("=== 2️⃣ Factory Pattern ===");

            var car = VehicleFactory.CreateVehicle("car");
            var bike = VehicleFactory.CreateVehicle("bike");

            car.Drive();
            bike.Drive();

            Console.WriteLine();
        }

        // ============================================================
        // 3️⃣ STRATEGY PATTERN — Runtime behavior swapping
        // ============================================================
        private static void StrategyDemo()
        {
            Console.WriteLine("=== 3️⃣ Strategy Pattern ===");

            ShoppingCart cart = new ShoppingCart();

            // Apply percentage discount
            cart.SetDiscountStrategy(new PercentageDiscount(0.10));
            Console.WriteLine($"Total with 10% discount: {cart.CalculateTotal(1000)}");

            // Swap strategy at runtime
            cart.SetDiscountStrategy(new FlatDiscount(200));
            Console.WriteLine($"Total with flat ₹200 discount: {cart.CalculateTotal(1000)}");

            Console.WriteLine();
        }

        // ============================================================
        // 4️⃣ OBSERVER PATTERN — Event notification
        // ============================================================
        private static void ObserverDemo()
        {
            Console.WriteLine("=== 4️⃣ Observer Pattern ===");

            Stock stock = new Stock("AAPL", 150);
            stock.Attach(new Investor("Alice"));
            stock.Attach(new Investor("Bob"));

            stock.Price = 155; // triggers notification
            stock.Price = 160;

            Console.WriteLine();
        }

        // ============================================================
        // 5️⃣ REPOSITORY PATTERN (Generic)
        // ============================================================
        private static void RepositoryDemo()
        {
            Console.WriteLine("=== 5️⃣ Generic Repository Pattern ===");

            IRepository<Product> productRepo = new InMemoryRepository<Product>();

            productRepo.Add(new Product { Id = 1, Name = "Laptop" });
            productRepo.Add(new Product { Id = 2, Name = "Mouse" });

            foreach (var product in productRepo.GetAll())
                Console.WriteLine($"Product: {product.Name}");

            Console.WriteLine();
        }
    }

    // ============================================================
    // 🔸 SINGLETON IMPLEMENTATION
    // ============================================================
    public sealed class Logger
    {
        private static readonly Logger _instance = new Logger();
        public static Logger Instance => _instance;

        private Logger() { } // private constructor prevents new Logger()

        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {DateTime.Now:T} - {message}");
        }
    }

    // ============================================================
    // 🔸 FACTORY IMPLEMENTATION
    // ============================================================
    public interface IVehicle
    {
        void Drive();
    }

    public class Car : IVehicle
    {
        public void Drive() => Console.WriteLine("🚗 Driving a car...");
    }

    public class Bike : IVehicle
    {
        public void Drive() => Console.WriteLine("🏍️ Riding a bike...");
    }

    public static class VehicleFactory
    {
        public static IVehicle CreateVehicle(string type)
        {
            switch (type.ToLower())
            {
                case "car": return new Car();
                case "bike": return new Bike();
                default: throw new ArgumentException("Unknown vehicle type");
            }
        }
    }

    // ============================================================
    // 🔸 STRATEGY IMPLEMENTATION
    // ============================================================
    public interface IDiscountStrategy
    {
        double ApplyDiscount(double amount);
    }

    public class PercentageDiscount : IDiscountStrategy
    {
        private readonly double _percent;
        public PercentageDiscount(double percent) => _percent = percent;
        public double ApplyDiscount(double amount) => amount - (amount * _percent);
    }

    public class FlatDiscount : IDiscountStrategy
    {
        private readonly double _flat;
        public FlatDiscount(double flat) => _flat = flat;
        public double ApplyDiscount(double amount) => amount - _flat;
    }

    public class ShoppingCart
    {
        private IDiscountStrategy _discountStrategy;

        public void SetDiscountStrategy(IDiscountStrategy strategy)
        {
            _discountStrategy = strategy;
        }

        public double CalculateTotal(double amount)
        {
            return _discountStrategy?.ApplyDiscount(amount) ?? amount;
        }
    }

    // ============================================================
    // 🔸 OBSERVER IMPLEMENTATION
    // ============================================================
    public interface IInvestor
    {
        void Update(string stockName, double newPrice);
    }

    public class Investor : IInvestor
    {
        private readonly string _name;
        public Investor(string name) => _name = name;

        public void Update(string stockName, double newPrice)
        {
            Console.WriteLine($"📈 {_name} notified: {stockName} is now {newPrice:C}");
        }
    }

    public class Stock
    {
        private double _price;
        private readonly List<IInvestor> _investors = new List<IInvestor>();
        public string Symbol { get; }

        public Stock(string symbol, double price)
        {
            Symbol = symbol;
            _price = price;
        }

        public double Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    _price = value;
                    Notify();
                }
            }
        }

        public void Attach(IInvestor investor) => _investors.Add(investor);
        public void Detach(IInvestor investor) => _investors.Remove(investor);

        private void Notify()
        {
            foreach (var investor in _investors)
                investor.Update(Symbol, _price);
        }
    }

    // ============================================================
    // 🔸 REPOSITORY IMPLEMENTATION
    // ============================================================
    public interface IRepository<T>
    {
        void Add(T item);
        IEnumerable<T> GetAll();
    }

    public class InMemoryRepository<T> : IRepository<T>
    {
        private readonly List<T> _data = new List<T>();
        public void Add(T item) => _data.Add(item);
        public IEnumerable<T> GetAll() => _data;
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
