using System;
using System.Collections.Generic;

namespace linqPractice
{
    // ===================== ⚙️ GENERIC CONSTRAINTS DEMO ===================== //
    public static class GenericConstraintsDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== ⚙️ GENERIC CONSTRAINTS DEMO =====\n");

            // 1️⃣ BASIC GENERIC CLASS EXAMPLE
            Console.WriteLine("=== 1️⃣ Basic Generic Class Example ===");
            GenericBox<int> intBox = new GenericBox<int>(42);
            GenericBox<string> strBox = new GenericBox<string>("Hello Generics");
            intBox.Show();
            strBox.Show();

            // 2️⃣ GENERIC WITH CONSTRAINT: REFERENCE TYPE
            Console.WriteLine("\n=== 2️⃣ 'where T : class' Example ===");
            Repository<Student> studentRepo = new Repository<Student>();
            studentRepo.Add(new Student { Name = "Alice", Age = 22 });
            studentRepo.Add(new Student { Name = "Bob", Age = 23 });
            studentRepo.DisplayAll();

            // 3️⃣ VALUE TYPE CONSTRAINT
            Console.WriteLine("\n=== 3️⃣ 'where T : struct' Example ===");
            Calculator<int> calc = new Calculator<int>(5, 10);
            calc.ShowSum();

            // 4️⃣ DEFAULT CONSTRUCTOR CONSTRAINT
            Console.WriteLine("\n=== 4️⃣ 'where T : new()' Example ===");
            Factory<SimplePerson> personFactory = new Factory<SimplePerson>();
            SimplePerson newPerson = personFactory.Create();
            newPerson.Name = "Charlie";
            newPerson.Introduce();

            // 5️⃣ INHERITANCE & INTERFACE CONSTRAINTS
            Console.WriteLine("\n=== 5️⃣ 'where T : BaseClass, IInterface' Example ===");
            Processor<SpecialStudent> processor = new Processor<SpecialStudent>();
            processor.RunProcess(new SpecialStudent { Name = "David", Grade = "A+" });

            // 6️⃣ MULTIPLE CONSTRAINTS
            Console.WriteLine("\n=== 6️⃣ Multiple Constraints Example ===");
            MultiConstraintHandler<SpecialStudent> multiHandler = new MultiConstraintHandler<SpecialStudent>();
            multiHandler.Handle(new SpecialStudent { Name = "Eve", Grade = "A" });

            Console.WriteLine("\n===== ✅ END OF GENERIC CONSTRAINTS DEMO =====");
        }
    }

    // 🧱 1️⃣ Basic Generic Class
    public class GenericBox<T>
    {
        private T _item;

        public GenericBox(T item)
        {
            _item = item;
        }

        public void Show()
        {
            Console.WriteLine($"Box contains: {_item}");
        }
    }

    // 🧩 2️⃣ Reference Type Constraint
    public class Repository<T> where T : class
    {
        private List<T> _items = new List<T>();

        public void Add(T item)
        {
            _items.Add(item);
        }

        public void DisplayAll()
        {
            foreach (var item in _items)
            {
                Console.WriteLine(item);
            }
        }
    }

    // 🧮 3️⃣ Value Type Constraint
    public class Calculator<T> where T : struct
    {
        private T _a;
        private T _b;

        public Calculator(T a, T b)
        {
            _a = a;
            _b = b;
        }

        public void ShowSum()
        {
            dynamic x = _a;
            dynamic y = _b;
            Console.WriteLine($"Sum = {x + y}");
        }
    }

    // 🏭 4️⃣ Default Constructor Constraint
    public class Factory<T> where T : new()
    {
        public T Create()
        {
            return new T(); // requires a public parameterless constructor
        }
    }

    public class SimplePerson
    {
        public string Name { get; set; }

        public void Introduce()
        {
            Console.WriteLine($"Hi, I'm {Name}.");
        }
    }

    // 🧠 5️⃣ Inheritance + Interface Constraint
    public interface IStudent
    {
        void Study();
    }

    public class BaseStudent
    {
        public string Name { get; set; }

    }

    public class SpecialStudent : BaseStudent, IStudent
    {
        public string Grade { get; set; }

        public void Study()
        {
            Console.WriteLine($"{Name} is studying hard!");
        }

        public override string ToString()
        {
            return $"Name: {Name}, Grade: {Grade}";
        }
    }

    public class Processor<T> where T : BaseStudent, IStudent
    {
        public void RunProcess(T student)
        {
            Console.WriteLine($"Processing student: {student.Name}");
            student.Study();
        }
    }

    // 🧩 6️⃣ Multiple Constraints Example
    public class MultiConstraintHandler<T> where T : BaseStudent, IStudent, new()
    {
        public void Handle(T obj)
        {
            Console.WriteLine("Handling multiple constraints object...");
            obj.Study();
            Console.WriteLine($"Created new instance of {typeof(T).Name} successfully!");
        }
    }
}
