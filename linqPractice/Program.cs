using System;

namespace linqPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("====================================");
            Console.WriteLine("   Welcome to the C# Practice Hub   ");
            Console.WriteLine("====================================");
            Console.WriteLine("1️⃣  LINQ Demo");
            Console.WriteLine("2️⃣  Collections & Generics Demo");
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
