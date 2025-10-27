using System;
using System.Collections.Generic;

namespace linqPractice
{
    /// <summary>
    /// ===============================================
    /// 🔑 PROPERTIES & INDEXERS DEMO
    /// ===============================================
    /// This demo covers:
    /// 1️⃣ Auto-implemented properties
    /// 2️⃣ Properties with backing fields
    /// 3️⃣ Computed properties (get-only)
    /// 4️⃣ Property accessors with different access levels
    /// 5️⃣ Init-only properties (C# 9.0+)
    /// 6️⃣ Indexers - custom [] operator implementation
    /// 7️⃣ Multi-parameter indexers
    /// </summary>
    public static class PropertiesAndIndexersDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== 🔑 PROPERTIES & INDEXERS DEMO =====\n");

            AutoPropertiesExample();
            BackingFieldExample();
            ComputedPropertiesExample();
            PropertyAccessLevelsExample();
            IndexerExample();
            MultiParameterIndexerExample();

            Console.WriteLine("\n===== ✅ END OF PROPERTIES & INDEXERS DEMO =====");
        }

        // ============================================================
        // 1️⃣ AUTO-IMPLEMENTED PROPERTIES
        // ============================================================
        private static void AutoPropertiesExample()
        {
            Console.WriteLine("=== 1️⃣ Auto-Implemented Properties ===");

            Person person = new Person
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 30
            };

            Console.WriteLine($"Name: {person.FirstName} {person.LastName}");
            Console.WriteLine($"Age: {person.Age}");
            Console.WriteLine($"ID (read-only): {person.Id}");
            Console.WriteLine();
        }

        // ============================================================
        // 2️⃣ PROPERTIES WITH BACKING FIELDS (Validation)
        // ============================================================
        private static void BackingFieldExample()
        {
            Console.WriteLine("=== 2️⃣ Properties with Backing Fields ===");

            BankAccount account = new BankAccount();
            account.AccountHolder = "Alice";
            account.Balance = 1000;

            Console.WriteLine($"Account: {account.AccountHolder}");
            Console.WriteLine($"Balance: {account.Balance:C}");

            try
            {
                account.Balance = -500; // ❌ Will throw exception
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }

            Console.WriteLine();
        }

        // ============================================================
        // 3️⃣ COMPUTED PROPERTIES (Get-Only)
        // ============================================================
        private static void ComputedPropertiesExample()
        {
            Console.WriteLine("=== 3️⃣ Computed Properties ===");

            Rectangle rect = new Rectangle { Width = 10, Height = 5 };

            Console.WriteLine($"Width: {rect.Width}, Height: {rect.Height}");
            Console.WriteLine($"Area: {rect.Area}"); // Computed on-the-fly
            Console.WriteLine($"Perimeter: {rect.Perimeter}");

            rect.Width = 20;
            Console.WriteLine($"New Area: {rect.Area}"); // Automatically updated
            Console.WriteLine();
        }

        // ============================================================
        // 4️⃣ PROPERTY ACCESS LEVELS (Public get, Private set)
        // ============================================================
        private static void PropertyAccessLevelsExample()
        {
            Console.WriteLine("=== 4️⃣ Property Access Levels ===");

            User user = new User("Alice", "alice@example.com");

            Console.WriteLine($"Username: {user.Username}"); // ✅ Public get
            Console.WriteLine($"Email: {user.Email}");
            Console.WriteLine($"Created: {user.CreatedDate}"); // ✅ Public get, private set

            // user.Username = "Bob"; // ❌ Error: set accessor is private
            // user.CreatedDate = DateTime.Now; // ❌ Error: set accessor is private

            user.UpdateEmail("newemail@example.com"); // Must use method
            Console.WriteLine($"Updated Email: {user.Email}");
            Console.WriteLine();
        }

        // ============================================================
        // 5️⃣ INDEXERS - Custom [] Operator
        // ============================================================
        private static void IndexerExample()
        {
            Console.WriteLine("=== 5️⃣ Indexers (Custom [] Access) ===");

            Classroom classroom = new Classroom();

            // Using indexer to add students
            classroom[0] = "Alice";
            classroom[1] = "Bob";
            classroom[2] = "Charlie";

            // Using indexer to retrieve students
            Console.WriteLine($"Student at index 0: {classroom[0]}");
            Console.WriteLine($"Student at index 1: {classroom[1]}");
            Console.WriteLine($"Student at index 2: {classroom[2]}");

            Console.WriteLine("\nAll Students:");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"  [{i}] {classroom[i]}");
            }

            Console.WriteLine();
        }

        // ============================================================
        // 6️⃣ MULTI-PARAMETER INDEXERS
        // ============================================================
        private static void MultiParameterIndexerExample()
        {
            Console.WriteLine("=== 6️⃣ Multi-Parameter Indexers ===");

            Grid grid = new Grid(3, 3);

            // Set values using [row, col] indexer
            grid[0, 0] = 1;
            grid[0, 1] = 2;
            grid[0, 2] = 3;
            grid[1, 0] = 4;
            grid[1, 1] = 5;
            grid[1, 2] = 6;

            Console.WriteLine("Grid Values:");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{grid[i, j]} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }

    // ============================================================
    // SUPPORTING CLASSES
    // ============================================================

    /// <summary>
    /// Example 1: Auto-implemented properties
    /// Compiler automatically generates backing fields
    /// </summary>
    public class Person
    {
        // Auto-implemented properties - simplest form
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        // Read-only auto-property (can only be set in constructor or initializer)
        public string Id { get; } = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Example 2: Properties with backing fields and validation
    /// </summary>
    public class BankAccount
    {
        private string _accountHolder;
        private double _balance;

        public string AccountHolder
        {
            get { return _accountHolder; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Account holder cannot be empty");
                _accountHolder = value;
            }
        }

        public double Balance
        {
            get { return _balance; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Balance cannot be negative");
                _balance = value;
            }
        }
    }

    /// <summary>
    /// Example 3: Computed properties (no backing field, calculated on access)
    /// </summary>
    public class Rectangle
    {
        public double Width { get; set; }
        public double Height { get; set; }

        // Computed property - calculated every time it's accessed
        public double Area => Width * Height;

        // Another computed property
        public double Perimeter => 2 * (Width + Height);

        // Expression-bodied property (same as above but different syntax)
        public bool IsSquare => Width == Height;
    }

    /// <summary>
    /// Example 4: Properties with different access levels
    /// </summary>
    public class User
    {
        // Public get, private set - can only be set within the class
        public string Username { get; private set; }
        public string Email { get; private set; }

        // Read-only property (no set accessor at all)
        public DateTime CreatedDate { get; private set; }

        public User(string username, string email)
        {
            Username = username;
            Email = email;
            CreatedDate = DateTime.Now;
        }

        // Method to update email (controlled access)
        public void UpdateEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email cannot be empty");

            Email = newEmail;
        }
    }

    /// <summary>
    /// Example 5: Indexer - allows object to be indexed like an array
    /// </summary>
    public class Classroom
    {
        private string[] students = new string[10];

        // Indexer: this[int index]
        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= students.Length)
                    throw new IndexOutOfRangeException($"Index {index} is out of range");
                return students[index];
            }
            set
            {
                if (index < 0 || index >= students.Length)
                    throw new IndexOutOfRangeException($"Index {index} is out of range");
                students[index] = value;
            }
        }

        public int Capacity => students.Length;
    }

    /// <summary>
    /// Example 6: Multi-parameter indexer (like a 2D array)
    /// </summary>
    public class Grid
    {
        private int[,] data;

        public Grid(int rows, int cols)
        {
            data = new int[rows, cols];
        }

        // Multi-parameter indexer: this[int row, int col]
        public int this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= data.GetLength(0) || col < 0 || col >= data.GetLength(1))
                    throw new IndexOutOfRangeException("Grid index out of range");
                return data[row, col];
            }
            set
            {
                if (row < 0 || row >= data.GetLength(0) || col < 0 || col >= data.GetLength(1))
                    throw new IndexOutOfRangeException("Grid index out of range");
                data[row, col] = value;
            }
        }
    }
}