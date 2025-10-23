using System;

namespace linqPractice
{
    // ==================== OOP DEMO ==================== //
    public static class OOPDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== 🧠 OOP Concepts Demo =====\n");

            // Create employees
            FullTimeEmployee emp1 = new FullTimeEmployee(1, "Alice", 60000);
            PartTimeEmployee emp2 = new PartTimeEmployee(2, "Bob", 25, 120); // hourly rate and hours
            Manager mgr = new Manager(3, "Charlie", 90000, "Engineering");

            // ========== 1️⃣ ENCAPSULATION ==========
            Console.WriteLine("=== Encapsulation Example ===");
            emp1.SetBonus(5000); // controlled access via method, not direct field
            Console.WriteLine($"{emp1.Name}'s Total Salary (with bonus): {emp1.GetTotalSalary()}");

            // ========== 2️⃣ INHERITANCE ==========
            Console.WriteLine("\n=== Inheritance Example ===");
            mgr.DisplayInfo(); // Inherited from Employee
            emp2.DisplayInfo();

            // ========== 3️⃣ POLYMORPHISM ==========
            Console.WriteLine("\n=== Polymorphism Example ===");
            Employee baseRef;

            baseRef = emp1; // Parent reference, child object
            baseRef.CalculatePay();

            baseRef = emp2;
            baseRef.CalculatePay();

            baseRef = mgr;
            baseRef.CalculatePay(); // overridden in Manager class

            // ========== 4️⃣ ABSTRACTION ==========
            Console.WriteLine("\n=== Abstraction Example ===");
            Console.WriteLine("All employees implement an abstract method CalculatePay() differently.");
            Console.WriteLine("This hides implementation details and shows only high-level behavior.\n");

            // ========== INTERFACE EXAMPLE ==========
            Console.WriteLine("=== Interface Example ===");
            IWork worker1 = emp1;
            IWork worker2 = mgr;
            worker1.DoWork();
            worker2.DoWork();

            Console.WriteLine("\n===== END OF OOP DEMO =====");
        }
    }

    // ==================================================
    // 🧱 BASE ABSTRACT CLASS → ABSTRACTION + INHERITANCE
    // ==================================================
    public abstract class Employee
    {
        // ENCAPSULATION: private field with public property
        private int _id;
        private string _name;

        public int Id
        {
            get { return _id; }
            set { if (value > 0) _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { if (!string.IsNullOrEmpty(value)) _name = value; }
        }

        // Constructor (shared by all child classes)
        public Employee(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // ABSTRACTION: forces subclasses to implement this
        public abstract void CalculatePay();

        // Common behavior shared by all employees
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id}, Name: {Name}");
        }
    }

    // ==================================================
    // 🎯 INTERFACE → Defines "work" behavior contract
    // ==================================================
    public interface IWork
    {
        void DoWork();
    }

    // ==================================================
    // 👩‍💼 FULL-TIME EMPLOYEE → Inherits & Implements
    // ==================================================
    public class FullTimeEmployee : Employee, IWork
    {
        private double _salary;
        private double _bonus;

        public FullTimeEmployee(int id, string name, double salary)
            : base(id, name)
        {
            _salary = salary;
        }

        public void SetBonus(double bonus)
        {
            _bonus = bonus;
        }

        public double GetTotalSalary()
        {
            return _salary + _bonus;
        }

        public override void CalculatePay()
        {
            Console.WriteLine($"{Name} (Full-Time): Annual Salary = {_salary:C}");
        }

        public void DoWork()
        {
            Console.WriteLine($"{Name} is working full-time on assigned projects.");
        }
    }

    // ==================================================
    // 🧑‍🔧 PART-TIME EMPLOYEE → Different Calculation Logic
    // ==================================================
    public class PartTimeEmployee : Employee, IWork
    {
        private double _hourlyRate;
        private int _hoursWorked;

        public PartTimeEmployee(int id, string name, double hourlyRate, int hoursWorked)
            : base(id, name)
        {
            _hourlyRate = hourlyRate;
            _hoursWorked = hoursWorked;
        }

        public override void CalculatePay()
        {
            double pay = _hourlyRate * _hoursWorked;
            Console.WriteLine($"{Name} (Part-Time): Pay = {pay:C}");
        }

        public void DoWork()
        {
            Console.WriteLine($"{Name} is working part-time on flexible hours.");
        }
    }

    // ==================================================
    // 👨‍💼 MANAGER → EXTENDS FULL-TIME EMPLOYEE
    // ==================================================
    public class Manager : FullTimeEmployee
    {
        public string Department { get; set; }

        public Manager(int id, string name, double salary, string dept)
            : base(id, name, salary)
        {
            Department = dept;
        }

        public override void CalculatePay()
        {
            Console.WriteLine($"{Name} (Manager of {Department}): Salary includes leadership bonus.");
        }

        // Method hiding (extra info)
        public new void DisplayInfo()
        {
            Console.WriteLine($"Manager: {Name} - Department: {Department}");
        }

        public new void DoWork()
        {
            Console.WriteLine($"{Name} is managing the {Department} team.");
        }
    }
}
