using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace linqPractice
{
    /// <summary>
    /// ====================== ⚡ PARALLEL & ASYNC FILE I/O DEMO ======================
    /// Teaches:
    /// 1️⃣ Sequential async file I/O
    /// 2️⃣ Parallel async operations using Task.WhenAll()
    /// 3️⃣ Parallel.ForEach for CPU-bound concurrency
    /// 4️⃣ Hybrid (Async + Parallel) logging safely using lock()
    /// </summary>
    public static class ParallelFileIODemo
    {
        public static async Task Run()
        {
            Console.WriteLine("===== ⚙️ PARALLEL & ASYNC FILE I/O DEMO =====\n");

            string basePath = Path.Combine(Environment.CurrentDirectory, "ParallelFiles");
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
                Console.WriteLine($"📁 Created directory: {basePath}");
            }

            // Prepare a list of file paths to work on
            List<string> filePaths = new List<string>();
            for (int i = 1; i <= 5; i++)
                filePaths.Add(Path.Combine(basePath, $"data_file_{i}.txt"));

            // ------------------------------------------------------------
            // EASY → Sequential Async Writes
            // ------------------------------------------------------------
            Console.WriteLine("\n=== 🧩 1️⃣ Sequential Async File Writes ===");
            Stopwatch sw = Stopwatch.StartNew();

            foreach (var file in filePaths)
            {
                await WriteSampleDataAsync(file, 100);
            }

            sw.Stop();
            Console.WriteLine($"⏱ Sequential async writing took: {sw.ElapsedMilliseconds} ms");

            // ------------------------------------------------------------
            // INTERMEDIATE → Parallel Async Writes using Task.WhenAll
            // ------------------------------------------------------------
            Console.WriteLine("\n=== ⚡ 2️⃣ Parallel Async File Writes using Task.WhenAll ===");
            sw.Restart();

            var writeTasks = new List<Task>();
            foreach (var file in filePaths)
            {
                writeTasks.Add(WriteSampleDataAsync(file, 100));
            }

            await Task.WhenAll(writeTasks); // Run all writes concurrently

            sw.Stop();
            Console.WriteLine($"⏱ Parallel async writing took: {sw.ElapsedMilliseconds} ms");

            // ------------------------------------------------------------
            // ADVANCED → Parallel.ForEach for concurrent reading
            // ------------------------------------------------------------
            Console.WriteLine("\n=== 🧠 3️⃣ Parallel.ForEach for Concurrent File Reading ===");
            Stopwatch readSw = Stopwatch.StartNew();

            Parallel.ForEach(filePaths, (file) =>
            {
                int count = CountLines(file);
                Console.WriteLine($"📖 {Path.GetFileName(file)} has {count} lines. (Thread: {Thread.CurrentThread.ManagedThreadId})");
            });

            readSw.Stop();
            Console.WriteLine($"⏱ Parallel reading completed in: {readSw.ElapsedMilliseconds} ms");

            // ------------------------------------------------------------
            // EXPERT → Hybrid Async + Parallel Logging
            // ------------------------------------------------------------
            Console.WriteLine("\n=== 🚀 4️⃣ Hybrid Async + Parallel Logging ===");

            string logFile = Path.Combine(basePath, "log.txt");
            object logLock = new object(); // ensures thread-safe writes

            Parallel.ForEach(filePaths, async (file) =>
            {
                // Use StreamReader instead of File.ReadAllTextAsync for compatibility
                string content = await ReadAllTextAsyncCompatible(file);
                int wordCount = content.Split(' ').Length;

                string message = $"{DateTime.Now:G} → {Path.GetFileName(file)} processed by Thread {Thread.CurrentThread.ManagedThreadId} ({wordCount} words)";

                lock (logLock)
                {
                    File.AppendAllText(logFile, message + Environment.NewLine);
                }

                Console.WriteLine($"✅ Processed {Path.GetFileName(file)} (Thread {Thread.CurrentThread.ManagedThreadId})");
            });

            Console.WriteLine("\n📜 Hybrid processing started — waiting for async tasks to complete...");
            await Task.Delay(1000); // small pause for console clarity

            Console.WriteLine("\n=== ✅ DEMO COMPLETE ===");
            Console.WriteLine($"🗂 Log file created at: {logFile}");
        }

        // ============================================================
        // 🧱 Helper: Write random sample data asynchronously
        // ============================================================
        private static async Task WriteSampleDataAsync(string path, int lines)
        {
            using (var writer = new StreamWriter(path, false))
            {
                for (int i = 1; i <= lines; i++)
                {
                    await writer.WriteLineAsync($"Line {i}: Random text {Guid.NewGuid()}");
                }
            }
            Console.WriteLine($"📝 File written: {Path.GetFileName(path)}");
        }

        // ============================================================
        // 📖 Helper: Count number of lines in a file (CPU-bound)
        // ============================================================
        private static int CountLines(string path)
        {
            int count = 0;
            using (var reader = new StreamReader(path))
            {
                while (reader.ReadLine() != null)
                    count++;
            }
            return count;
        }

        // ============================================================
        // 🧩 Helper: Replacement for File.ReadAllTextAsync (works everywhere)
        // ============================================================
        private static async Task<string> ReadAllTextAsyncCompatible(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
