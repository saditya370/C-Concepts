using System;
using System.Collections.Generic;
using System.Linq;

namespace linqPractice
{
    // ===================== ✨ EXTENSION METHODS DEMO ===================== //
    public static class ExtensionMethodsDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== ✨ EXTENSION METHODS DEMO =====\n");

            // 1️⃣ BASIC EXTENSION METHODS
            string name = "Alice";
            name.SayHello();
            Console.WriteLine("I love learning C#!".Shout());

            int num = 5;
            Console.WriteLine($"Square of {num} = {num.Square()}");

            DateTime today = DateTime.Today;
            Console.WriteLine($"Tomorrow is: {today.Tomorrow():D}");

            // 2️⃣ ADVANCED EXAMPLES — LIKE LINQ STYLE
            Console.WriteLine("\n=== 🧠 Advanced Extension Methods (LINQ-like) ===");

            List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
            List<int> evenSquares = numbers
                .FilterEven()
                .SelectSquare()
                .ToList();

            Console.WriteLine("Even Squares: " + string.Join(", ", evenSquares));

            string[] fruits = { "apple", "banana", "pear", "mango" };
            var longNames = fruits.FilterByLength(5);
            Console.WriteLine("Fruits with more than 5 letters: " + string.Join(", ", longNames));

            Console.WriteLine("\n===== ✅ END OF EXTENSION METHODS DEMO =====");
        }
    }

    // ===================== 🧩 BASIC EXTENSION METHODS ===================== //
    public static class BasicExtensions
    {
        public static void SayHello(this string name)
        {
            Console.WriteLine($"👋 Hello, {name}! Nice to meet you!");
        }

        public static string Shout(this string text)
        {
            return text.ToUpper() + "!!!";
        }

        public static int Square(this int value)
        {
            return value * value;
        }

        public static DateTime Tomorrow(this DateTime date)
        {
            return date.AddDays(1);
        }
    }

    // ===================== 🧠 ADVANCED (LINQ-LIKE) EXTENSIONS ===================== //
    public static class AdvancedExtensions
    {
        // Filters even numbers from a list
        public static IEnumerable<int> FilterEven(this IEnumerable<int> source)
        {
            foreach (var n in source)
            {
                if (n % 2 == 0)
                    yield return n;
            }
        }

        // Squares each number (like Select in LINQ)
        public static IEnumerable<int> SelectSquare(this IEnumerable<int> source)
        {
            foreach (var n in source)
                yield return n * n;
        }

        // Filters strings longer than 'minLength'
        public static IEnumerable<string> FilterByLength(this IEnumerable<string> source, int minLength)
        {
            foreach (var s in source)
            {
                if (s.Length > minLength)
                    yield return s;
            }
        }
    }
}
