using System;
using System.Collections.Generic;
using System.IO;

namespace linqPractice
{
    /// <summary>
    /// Demonstrates all key exception handling concepts:
    /// - try / catch / finally
    /// - multiple catch blocks
    /// - throwing and rethrowing exceptions
    /// - custom exceptions
    /// - nested try-catch
    /// - practical business logic error handling
    /// </summary>
    public static class ExceptionHandlingDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== ⚠️ EXCEPTION HANDLING DEMO =====\n");

            // 1️⃣ Basic Try-Catch Example
            BasicTryCatch();

            // 2️⃣ Multiple Catch Blocks Example
            MultipleCatchExample();

            // 3️⃣ Finally Block Example
            FinallyBlockExample();

            // 4️⃣ Custom Exception Example
            CustomExceptionExample();

            // 5️⃣ Nested Try-Catch Example
            NestedTryCatchExample();

            // 6️⃣ Rethrowing Exceptions Example
            RethrowExample();

            // 7️⃣ Practical Example: Validating Student Marks
            PracticalValidationExample();

            Console.WriteLine("\n===== END OF EXCEPTION HANDLING DEMO =====");
        }

        // ======================================================
        // 1️⃣ BASIC TRY-CATCH
        // ======================================================
        private static void BasicTryCatch()
        {
            Console.WriteLine("\n=== 1️⃣ Basic Try-Catch ===");

            try
            {
                int[] numbers = { 10, 20, 30 };
                Console.WriteLine(numbers[5]); // ❌ Index out of range
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
            }

            Console.WriteLine("✅ Program continued after handling exception.");
        }

        // ======================================================
        // 2️⃣ MULTIPLE CATCH BLOCKS
        // ======================================================
        private static void MultipleCatchExample()
        {
            Console.WriteLine("\n=== 2️⃣ Multiple Catch Blocks ===");

            try
            {
                int a = 10, b = 0;
                int result = a / b; // ❌ Divide by zero
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"❌ Math Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                // General exception (always last)
                Console.WriteLine($"⚠️ General Error: {ex.Message}");
            }
        }

        // ======================================================
        // 3️⃣ FINALLY BLOCK
        // ======================================================
        private static void FinallyBlockExample()
        {
            Console.WriteLine("\n=== 3️⃣ Finally Block ===");

            StreamReader reader = null;
            try
            {
                reader = new StreamReader("nonexistent.txt"); // ❌ File doesn't exist
                Console.WriteLine(reader.ReadToEnd());
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"❌ File not found: {ex.Message}");
            }
            finally
            {
                // Always runs, even if exception occurs
                if (reader != null)
                    reader.Close();

                Console.WriteLine("✅ 'finally' executed: resources cleaned up.");
            }
        }

        // ======================================================
        // 4️⃣ CUSTOM EXCEPTION
        // ======================================================
        private static void CustomExceptionExample()
        {
            Console.WriteLine("\n=== 4️⃣ Custom Exception ===");

            try
            {
                StudentRecord student = new StudentRecord { Name = "Alice", Marks = 120 };
                ValidateMarks(student.Marks);
                Console.WriteLine("✅ Student marks valid.");
            }
            catch (InvalidMarksException ex)
            {
                Console.WriteLine($"❌ Custom Exception: {ex.Message}");
            }
        }

        // Helper Method for Custom Exception
        private static void ValidateMarks(double marks)
        {
            if (marks < 0 || marks > 100)
                throw new InvalidMarksException("Marks must be between 0 and 100.");
        }

        // ======================================================
        // 5️⃣ NESTED TRY-CATCH
        // ======================================================
        private static void NestedTryCatchExample()
        {
            Console.WriteLine("\n=== 5️⃣ Nested Try-Catch ===");

            try
            {
                try
                {
                    int[] nums = new int[3];
                    nums[5] = 100; // Inner exception
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine($"Inner Catch: {ex.Message}");
                    throw; // Re-throw to outer catch
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Outer Catch: Exception bubbled up: {ex.Message}");
            }
        }

        // ======================================================
        // 6️⃣ RETHROWING EXCEPTIONS
        // ======================================================
        private static void RethrowExample()
        {
            Console.WriteLine("\n=== 6️⃣ Rethrowing Exceptions ===");

            try
            {
                ProcessFile("does_not_exist.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Exception caught in caller: {ex.Message}");
            }
        }

        private static void ProcessFile(string fileName)
        {
            try
            {
                File.ReadAllText(fileName);
            }
            catch (Exception)
            {
                Console.WriteLine("⚠️ Error in ProcessFile. Rethrowing...");
                throw; // Re-throws exception to caller
            }
        }

        // ======================================================
        // 7️⃣ PRACTICAL EXAMPLE - VALIDATION SYSTEM
        // ======================================================
        private static void PracticalValidationExample()
        {
            Console.WriteLine("\n=== 7️⃣ Practical Example: Validating Students ===");

            List<StudentRecord> students = new List<StudentRecord>
            {
                new StudentRecord { Name = "Bob", Marks = 85 },
                new StudentRecord { Name = "Charlie", Marks = -10 }, // ❌ invalid
                new StudentRecord { Name = "Eve", Marks = 92 }
            };

            foreach (var student in students)
            {
                try
                {
                    ValidateMarks(student.Marks);
                    Console.WriteLine($"✅ {student.Name}'s marks are valid: {student.Marks}");
                }
                catch (InvalidMarksException ex)
                {
                    Console.WriteLine($"❌ Error for {student.Name}: {ex.Message}");
                }
            }
        }
    }

    // ======================================================
    // 🎯 CUSTOM EXCEPTION CLASS
    // ======================================================
    public class InvalidMarksException : Exception
    {
        public InvalidMarksException() { }

        public InvalidMarksException(string message)
            : base(message)
        { }

        public InvalidMarksException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    // ======================================================
    // 🧩 HELPER CLASS
    // ======================================================
    public class StudentRecord
    {
        public string Name { get; set; }
        public double Marks { get; set; }
    }
}
