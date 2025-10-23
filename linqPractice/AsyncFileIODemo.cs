using System;
using System.IO;
using System.Threading.Tasks;

namespace linqPractice
{
    // ===================== ⚡ ASYNC FILE I/O DEMO ===================== //
    public static class AsyncFileIODemo
    {
        public static async Task Run()
        {
            Console.WriteLine("===== ⚡ ASYNC FILE HANDLING DEMO =====\n");

            string basePath = Path.Combine(Environment.CurrentDirectory, "AsyncFiles");
            string filePath = Path.Combine(basePath, "async_students.txt");
            string logPath = Path.Combine(basePath, "async_log.txt");

            // ✅ Create directory if it doesn't exist
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
                Console.WriteLine($"📁 Created directory: {basePath}");
            }

            // 1️⃣ WRITE DATA ASYNCHRONOUSLY
            Console.WriteLine("\n=== 1️⃣ Writing File Asynchronously ===");
            await WriteStudentsAsync(filePath);

            // 2️⃣ READ DATA ASYNCHRONOUSLY
            Console.WriteLine("\n=== 2️⃣ Reading File Asynchronously ===");
            await ReadStudentsAsync(filePath);

            // 3️⃣ APPEND DATA ASYNCHRONOUSLY
            Console.WriteLine("\n=== 3️⃣ Appending to File Asynchronously ===");
            await AppendStudentAsync(filePath, "Eve,23,Physics");
            await ReadStudentsAsync(filePath);

            // 4️⃣ STREAM WRITER / READER ASYNC DEMO
            Console.WriteLine("\n=== 4️⃣ StreamWriter & StreamReader (Async) ===");
            await UseAsyncStreamsExample(filePath);

            // 5️⃣ ASYNC LOGGING DEMO
            Console.WriteLine("\n=== 5️⃣ Asynchronous Logging Example ===");
            await LogActionAsync(logPath, "Student data asynchronously updated.");
            await LogActionAsync(logPath, "New student added asynchronously.");
            await DisplayLogAsync(logPath);

            // 6️⃣ ASYNC EXCEPTION HANDLING
            Console.WriteLine("\n=== 6️⃣ Exception Handling with Async ===");
            try
            {
                string missingFile = Path.Combine(basePath, "missing_file.txt");
                await ReadAllTextFallbackAsync(missingFile);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"❌ File Not Found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Unexpected Error: {ex.Message}");
            }

            Console.WriteLine("\n===== ✅ END OF ASYNC FILE I/O DEMO =====");
        }

        // ============================================================
        // 🧱 1️⃣ Write Async (Manual implementation for old .NET)
        // ============================================================
        private static async Task WriteStudentsAsync(string path)
        {
            string[] students =
            {
                "Alice,20,Mathematics",
                "Bob,21,Computer Science",
                "Charlie,22,Engineering",
                "David,19,Chemistry"
            };

            using (var writer = new StreamWriter(path, false))
            {
                foreach (var student in students)
                {
                    await writer.WriteLineAsync(student);
                }
            }
            Console.WriteLine($"✅ File written asynchronously at: {path}");
        }

        // ============================================================
        // 📖 2️⃣ Read Async (Manual read implementation)
        // ============================================================
        private static async Task ReadStudentsAsync(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("⚠️ File not found!");
                return;
            }

            using (var reader = new StreamReader(path))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    var data = line.Split(',');
                    if (data.Length < 3)
                    {
                        Console.WriteLine($"⚠️ Skipping invalid line: {line}");
                        continue;
                    }
                    Console.WriteLine($"Name: {data[0]}, Age: {data[1]}, Course: {data[2]}");
                }
            }
        }

        // ============================================================
        // ➕ 3️⃣ Append Async
        // ============================================================
        private static async Task AppendStudentAsync(string path, string newStudent)
        {
            using (var writer = new StreamWriter(path, true))
            {
                await writer.WriteLineAsync(newStudent);
            }
            Console.WriteLine($"✅ Added asynchronously: {newStudent}");
        }

        // ============================================================
        // 💡 4️⃣ StreamReader & StreamWriter Async Example
        // ============================================================
        private static async Task UseAsyncStreamsExample(string path)
        {
            string asyncStreamFile = Path.Combine(Path.GetDirectoryName(path), "async_stream_demo.txt");

            using (var writer = new StreamWriter(asyncStreamFile))
            {
                await writer.WriteLineAsync("Hello from Async StreamWriter!");
                await writer.WriteLineAsync("This file is written asynchronously.");
                await writer.WriteLineAsync("Non-blocking I/O improves performance.");
            }

            Console.WriteLine("✅ Data written asynchronously with StreamWriter.");

            using (var reader = new StreamReader(asyncStreamFile))
            {
                Console.WriteLine("\n📖 Reading asynchronously with StreamReader:");
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }

        // ============================================================
        // 🧾 5️⃣ Async Logging Example
        // ============================================================
        private static async Task LogActionAsync(string logPath, string message)
        {
            using (var writer = new StreamWriter(logPath, true))
            {
                await writer.WriteLineAsync($"{DateTime.Now:G} - {message}");
            }
            Console.WriteLine($"📄 Log written asynchronously: {message}");
        }

        private static async Task DisplayLogAsync(string logPath)
        {
            Console.WriteLine("\n📜 Async Log File Content:");

            if (!File.Exists(logPath))
            {
                Console.WriteLine("⚠️ Log file not found.");
                return;
            }

            using (var reader = new StreamReader(logPath))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }

        // ============================================================
        // 🧩 6️⃣ Manual Async Read for Exceptions
        // ============================================================
        private static async Task<string> ReadAllTextFallbackAsync(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
