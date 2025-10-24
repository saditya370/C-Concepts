using System;
using System.Threading.Tasks;

namespace linqPractice
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("====================================");
            Console.WriteLine("   Welcome to the C# Practice Hub   ");
            Console.WriteLine("====================================");
            Console.WriteLine("1️ LINQ Demo");
            Console.WriteLine("2  Collections & Generics Demo");
            Console.WriteLine("3️ OOP Demo");
            Console.WriteLine("4️  Delegates & Events Demo");
            Console.WriteLine("5️  File I/O Demo");
            Console.WriteLine("6️  Exception Handling Demo ");
            Console.WriteLine("7️ Async File I/O Demo ");
            Console.WriteLine("8️  Parallel File I/O Demo");
            Console.WriteLine("9️ Performance Comparison Demo");
            Console.WriteLine("10  Collections Advanced Demo");
            Console.WriteLine("11  Design Patterns Demo");
            Console.WriteLine("12  Properties and Indexers Demo");
            Console.WriteLine("13  Extension Methods Demo");
            Console.WriteLine("14  Nullable Types Demo");



            Console.WriteLine("------------------------------------");
            Console.Write("👉 Enter your choice: ");
            var choice = Console.ReadLine();

            Console.WriteLine("\n");

            switch (choice)
            {
                case "1":
                    LinqDemo.Run();
                    break;

                case "2":
                    CollectionsAndGenericsDemo.Run();
                    break;
                case "3":
                    OOPDemo.Run();
                    break;
                case "4":
                    DelegatesAndEventsDemo.Run();
                    break;
                case "5":
                    FileIODemo.Run();
                    break;
                case "6":
                    ExceptionHandlingDemo.Run();
                    break;
                case "7": await AsyncFileIODemo.Run(); break;
                case "8":
                    await ParallelFileIODemo.Run();
                    break;

                case "9":
                    await PerformanceComparisonDemo.Run();
                    break;

                case "10":   CollectionsAdvancedDemo.Run(); break;

                case "11":   DesignPatternsDemo.Run(); break;

                case "12": PropertiesAndIndexersDemo.Run(); break;

                case "13": ExtensionMethodsDemo.Run(); break;

                case "14": NullableTypesDemo.Run(); break;






                default:
                    Console.WriteLine("❌ Invalid choice! Please enter 1 or 2.");
                    break;
            }

            Console.WriteLine("\n------------------------------------");
            Console.WriteLine("✅ Demo Complete. Press any key to exit...");
            Console.ReadKey();
        }
    }
}
