using System;

namespace linqPractice
{
    // ==================== OOP DEMO ==================== //
    public static class OOPDemo
    {
        public static void Run()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("🧠 C# OOP Concepts Demo (.NET Framework 4.8)");
            Console.WriteLine("=============================================\n");

            // Create sample objects
            FullTimeEmployee emp1 = new FullTimeEmployee(1, "Alice", 60000);
            PartTimeEmployee emp2 = new PartTimeEmployee(2, "Bob", 25, 120);
            Manager mgr = new Manager(3, "Charlie", 90000, "Engineering");

            // ==================================================
            // 1️⃣ ENCAPSULATION — Data Hiding with Controlled Access
            // ==================================================
            Console.WriteLine("=== 1️⃣ Encapsulation Example ===");
            emp1.SetBonus(5000); // controlled access via method (not direct field)
            Console.WriteLine(emp1.Name + "'s Total Salary (with bonus): " + emp1.GetTotalSalary());

            // ==================================================
            // 2️⃣ INHERITANCE — Reusing and Extending Base Class
            // ==================================================
            Console.WriteLine("\n=== 2️⃣ Inheritance Example ===");
            mgr.DisplayInfo(); // custom DisplayInfo (method hiding)
            emp2.DisplayInfo(); // inherited version from Employee

            // ==================================================
            // 3️⃣ POLYMORPHISM — One Interface, Many Implementations
            // ==================================================
            Console.WriteLine("\n=== 3️⃣ Polymorphism Example ===");
            Employee baseRef; // Base class reference

            baseRef = emp1;
            baseRef.CalculatePay(); // Calls FullTimeEmployee version

            baseRef = emp2;
            baseRef.CalculatePay(); // Calls PartTimeEmployee version

            baseRef = mgr;
            baseRef.CalculatePay(); // Calls Manager version (overridden)

            // ==================================================
            // 4️⃣ ABSTRACTION — Hiding Complexity, Exposing Essentials
            // ==================================================
            Console.WriteLine("\n=== 4️⃣ Abstraction Example ===");
            Console.WriteLine("All employees implement CalculatePay() differently,");
            Console.WriteLine("hiding how pay is computed but exposing the idea of 'payment'.\n");

            // ==================================================
            // 5️⃣ INTERFACE — Defining Contracts for Behavior
            // ==================================================
            Console.WriteLine("=== 5️⃣ Interface Example ===");
            IWork worker1 = emp1; // interface reference
            IWork worker2 = mgr;
            worker1.DoWork(); // calls FullTimeEmployee's version
            worker2.DoWork(); // calls Manager's version

            Console.WriteLine("\n✅ END OF OOP DEMO ✅");
        }
    }

    // ==================================================
    // 🧱 ABSTRACT BASE CLASS → Abstraction + Inheritance
    // ==================================================
    public abstract class Employee
    {
        // ENCAPSULATION: Private fields + controlled access through properties
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

        // Constructor (called by derived classes)
        public Employee(int id, string name)
        {
            Id = id;
            Name = name;
        }

        // ABSTRACT METHOD — must be implemented by subclasses
        public abstract void CalculatePay();

        // VIRTUAL METHOD — can be overridden by child classes
        public virtual void DisplayInfo()
        {
            Console.WriteLine("ID: " + Id + ", Name: " + Name);
        }
    }

    // ==================================================
    // 🎯 INTERFACE — Defines a contract for "work" behavior
    // ==================================================
    public interface IWork
    {
        void DoWork();
    }

    // ==================================================
    // 👩‍💼 FULL-TIME EMPLOYEE — Implements Interface + Extends Base
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

        // Overridden from abstract base class
        public override void CalculatePay()
        {
            Console.WriteLine(Name + " (Full-Time): Annual Salary = " + _salary.ToString("C"));
        }

        public void DoWork()
        {
            Console.WriteLine(Name + " is working full-time on assigned projects.");
        }
    }

    // ==================================================
    // 🧑‍🔧 PART-TIME EMPLOYEE — Different Pay Logic
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
            Console.WriteLine(Name + " (Part-Time): Pay = " + pay.ToString("C"));
        }

        public void DoWork()
        {
            Console.WriteLine(Name + " is working part-time with flexible hours.");
        }
    }

    // ==================================================
    // 👨‍💼 MANAGER — Extends FullTimeEmployee + Adds Department
    // ==================================================
    public class Manager : FullTimeEmployee
    {
        public string Department { get; set; }

        public Manager(int id, string name, double salary, string dept)
            : base(id, name, salary)
        {
            Department = dept;
        }

        // Overridden to add extra meaning for managers
        public override void CalculatePay()
        {
            Console.WriteLine(Name + " (Manager of " + Department + "): Salary includes leadership bonus.");
        }

        // METHOD HIDING — hides base class DisplayInfo()
        public new void DisplayInfo()
        {
            Console.WriteLine("Manager: " + Name + " - Department: " + Department);
        }

        public new void DoWork()
        {
            Console.WriteLine(Name + " is managing the " + Department + " department.");
        }
    }
}
