using System;
using System.Threading.Tasks;

namespace linqPractice
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool exitRequested = false;

            while (!exitRequested)
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("   Welcome to the C# Practice Hub   ");
                Console.WriteLine("====================================");

                Console.WriteLine("1️  Data Types and Memory Demo");
                Console.WriteLine("2️  Value vs Reference Types Demo");
                Console.WriteLine("3️  Memory Allocation Demo");
                Console.WriteLine("4️  Nullable Types Demo");
                Console.WriteLine("5️  OOP Demo");
                Console.WriteLine("6️  Properties and Indexers Demo");
                Console.WriteLine("7️  Delegates & Events Demo");
                Console.WriteLine("8️  Collections & Generics Demo");
                Console.WriteLine("9️  Generic Constraints Demo");
                Console.WriteLine("10  LINQ Demo");
                Console.WriteLine("11  IEnumerable vs IQueryable Demo");
                Console.WriteLine("12  Extension Methods Demo");
                Console.WriteLine("13  Collections Advanced Demo");
                Console.WriteLine("14  Exception Handling Demo");
                Console.WriteLine("15  File I/O Demo");
                Console.WriteLine("16  Async File I/O Demo");
                Console.WriteLine("17  Parallel File I/O Demo");
                Console.WriteLine("18  Performance Comparison Demo");
                Console.WriteLine("19  Design Patterns Demo");
                Console.WriteLine("20  Dependency Injection (DI) Demo");

                Console.WriteLine("------------------------------------");
                Console.Write("👉 Enter your choice (or Q / Esc to quit): ");
                var choice = Console.ReadLine()?.Trim().ToLower();

                Console.WriteLine();

                if (string.IsNullOrEmpty(choice)) continue;
                if (choice == "q")
                {
                    exitRequested = true;
                    break;
                }

                switch (choice)
                {
                    case "1": DataTypesAndMemoryDemo.Run(); break;
                    case "2": ValueVsReferenceDemo.Run(); break;
                    case "3": MemoryAllocationDemo.Run(); break;
                    case "4": NullableTypesDemo.Run(); break;
                    case "5": OOPDemo.Run(); break;
                    case "6": PropertiesAndIndexersDemo.Run(); break;
                    case "7": DelegatesAndEventsDemo.Run(); break;
                    case "8": CollectionsAndGenericsDemo.Run(); break;
                    case "9": GenericConstraintsDemo.Run(); break;
                    case "10": LinqDemo.Run(); break;
                    case "11": IEnumerableVsIQueryableDemo.Run(); break;
                    case "12": ExtensionMethodsDemo.Run(); break;
                    case "13": CollectionsAdvancedDemo.Run(); break;
                    case "14": ExceptionHandlingDemo.Run(); break;
                    case "15": FileIODemo.Run(); break;
                    case "16": await AsyncFileIODemo.Run(); break;
                    case "17": await ParallelFileIODemo.Run(); break;
                    case "18": await PerformanceComparisonDemo.Run(); break;
                    case "19": DesignPatternsDemo.Run(); break;
                    case "20": DependencyInjectionDemo.Run(); break;

                    default:
                        Console.WriteLine("❌ Invalid choice! Please enter a number between 1 and 20.");
                        break;
                }

                Console.WriteLine("\n------------------------------------");
                Console.WriteLine("✅ Demo Complete.");
                Console.WriteLine("Press [Enter] to go back to the menu, or [Esc] to exit...");

                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Escape)
                    exitRequested = true;
            }

            Console.Clear();
            Console.WriteLine("👋 Exiting... Thank you for using the C# Practice Hub!");
            await Task.Delay(500);
        }
    }
}
