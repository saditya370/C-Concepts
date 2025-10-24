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

            // ✅ Ensure directory exists
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
                Console.WriteLine($"📁 Created directory: {basePath}");
            }

            // 1️⃣ WRITE FILE ASYNC
            Console.WriteLine("\n=== 1️⃣ Writing File Asynchronously ===");
            await WriteStudentsAsync(filePath);

            // 2️⃣ READ FILE ASYNC
            Console.WriteLine("\n=== 2️⃣ Reading File Asynchronously ===");
            await ReadStudentsAsync(filePath);

            // 3️⃣ APPEND FILE ASYNC
            Console.WriteLine("\n=== 3️⃣ Appending Data Asynchronously ===");
            await AppendStudentAsync(filePath, "Eve,23,Physics");
            await ReadStudentsAsync(filePath);

            // 4️⃣ STREAM DEMO
            Console.WriteLine("\n=== 4️⃣ StreamWriter & StreamReader (Async) ===");
            await UseAsyncStreamsExample(filePath);

            // 5️⃣ LOGGING DEMO
            Console.WriteLine("\n=== 5️⃣ Asynchronous Logging Example ===");
            await LogActionAsync(logPath, "Student data asynchronously updated.");
            await LogActionAsync(logPath, "New student added asynchronously.");
            await DisplayLogAsync(logPath);

            // 6️⃣ EXCEPTION DEMO
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
        /// <summary>
        /// Writes student records asynchronously using StreamWriter.
        /// </summary>
        private static async Task WriteStudentsAsync(string path)
        {
            string[] students =
            {
                "Alice,20,Mathematics",
                "Bob,21,Computer Science",
                "Charlie,22,Engineering",
                "David,19,Chemistry"
            };

            try
            {
                using (var writer = new StreamWriter(path, false))
                {
                    foreach (var student in students)
                    {
                        await writer.WriteLineAsync(student).ConfigureAwait(false);
                    }
                    await writer.FlushAsync().ConfigureAwait(false);
                }
                Console.WriteLine($"✅ File written asynchronously at: {path}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"❌ I/O Error while writing: {ex.Message}");
            }
        }

        // ============================================================
        /// <summary>
        /// Reads student records asynchronously from file with validation and error handling.
        /// </summary>
        private static async Task ReadStudentsAsync(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("⚠️ File not found!");
                return;
            }

            try
            {
                using (var reader = new StreamReader(path))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
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
                Console.WriteLine("✅ Async file read completed successfully.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"❌ I/O Error while reading: {ex.Message}");
            }
        }

        // ============================================================
        /// <summary>
        /// Appends a new student record asynchronously to the file.
        /// </summary>
        private static async Task AppendStudentAsync(string path, string newStudent)
        {
            try
            {
                using (var writer = new StreamWriter(path, true))
                {
                    await writer.WriteLineAsync(newStudent).ConfigureAwait(false);
                    await writer.FlushAsync().ConfigureAwait(false);
                }
                Console.WriteLine($"✅ Added asynchronously: {newStudent}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"❌ I/O Error while appending: {ex.Message}");
            }
        }

        // ============================================================
        /// <summary>
        /// Demonstrates async writing and reading using StreamWriter and StreamReader.
        /// </summary>
        private static async Task UseAsyncStreamsExample(string path)
        {
            string asyncStreamFile = Path.Combine(Path.GetDirectoryName(path), "async_stream_demo.txt");

            try
            {
                using (var writer = new StreamWriter(asyncStreamFile))
                {
                    await writer.WriteLineAsync("Hello from Async StreamWriter!").ConfigureAwait(false);
                    await writer.WriteLineAsync("This file is written asynchronously.").ConfigureAwait(false);
                    await writer.WriteLineAsync("Non-blocking I/O improves performance.").ConfigureAwait(false);
                    await writer.FlushAsync().ConfigureAwait(false);
                }

                Console.WriteLine("✅ Data written asynchronously with StreamWriter.");

                using (var reader = new StreamReader(asyncStreamFile))
                {
                    Console.WriteLine("\n📖 Reading asynchronously with StreamReader:");
                    string line;
                    while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"❌ Error during stream demo: {ex.Message}");
            }
        }

        // ============================================================
        /// <summary>
        /// Logs an event asynchronously to a log file.
        /// </summary>
        private static async Task LogActionAsync(string logPath, string message)
        {
            try
            {
                using (var writer = new StreamWriter(logPath, true))
                {
                    await writer.WriteLineAsync($"{DateTime.Now:G} - {message}").ConfigureAwait(false);
                    await writer.FlushAsync().ConfigureAwait(false);
                }
                Console.WriteLine($"📄 Log written asynchronously: {message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"❌ Failed to write log: {ex.Message}");
            }
        }

        // ============================================================
        /// <summary>
        /// Reads and displays log file asynchronously.
        /// </summary>
        private static async Task DisplayLogAsync(string logPath)
        {
            Console.WriteLine("\n📜 Async Log File Content:");

            if (!File.Exists(logPath))
            {
                Console.WriteLine("⚠️ Log file not found.");
                return;
            }

            try
            {
                using (var reader = new StreamReader(logPath))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"❌ Failed to read log: {ex.Message}");
            }
        }

        // ============================================================
        /// <summary>
        /// Reads a file asynchronously and throws if missing (for exception demo).
        /// </summary>
        private static async Task<string> ReadAllTextFallbackAsync(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return await reader.ReadToEndAsync().ConfigureAwait(false);
            }
        }
    }
}
