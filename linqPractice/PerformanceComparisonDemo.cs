using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace linqPractice
{
    /// <summary>
    /// ============================
    /// 📊 PERFORMANCE COMPARISON DEMO
    /// ============================
    /// This example compares:
    /// 1️⃣ Sequential file I/O (one file at a time)
    /// 2️⃣ Asynchronous I/O (await each file)
    /// 3️⃣ Parallel I/O (multiple threads using Parallel.ForEach)
    ///
    /// GOAL:
    /// - Understand how concurrency and asynchrony affect I/O-bound performance.
    /// - Learn to measure performance using Stopwatch.
    /// </summary>
    public static class PerformanceComparisonDemo
    {
        public static async Task Run()
        {
            Console.WriteLine("===== ⚡ PERFORMANCE COMPARISON DEMO =====\n");

            string basePath = Path.Combine(Environment.CurrentDirectory, "PerfFiles");
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            // Prepare 10 demo files with random text
            await GenerateTestFiles(basePath, 10, 30000);

            // Run comparisons
            await RunSequentialRead(basePath);
            await RunAsyncRead(basePath);
            RunParallelRead(basePath);

            Console.WriteLine("\n===== ✅ END OF PERFORMANCE DEMO =====\n");
        }

        // ==========================================================
        // 🧱 STEP 1: Generate test files (simulate real-world data)
        // ==========================================================
        private static async Task GenerateTestFiles(string path, int count, int linesPerFile)
        {
            Console.WriteLine("Creating sample files...");

            Random rnd = new Random();

            for (int i = 1; i <= count; i++)
            {
                string filePath = Path.Combine(path, $"file{i}.txt");
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    for (int j = 0; j < linesPerFile; j++)
                    {
                        await writer.WriteLineAsync($"Line {j + 1}: Random number {rnd.Next(0, 10000)}");
                    }
                }
            }

            Console.WriteLine($"✅ {count} files created, each with {linesPerFile} lines.\n");
        }

        // ==========================================================
        // 🕐 STEP 2: Sequential Read (synchronous)
        // ==========================================================
        private static async Task RunSequentialRead(string path)
        {
            Console.WriteLine("=== 1️⃣ Sequential File Reading ===");

            var sw = Stopwatch.StartNew();
            string[] files = Directory.GetFiles(path, "*.txt");

            int totalLines = 0;

            foreach (string file in files)
            {
                // Blocking I/O — waits for each file to finish
                using (StreamReader reader = new StreamReader(file))
                {
                    while (await reader.ReadLineAsync() is string line)
                    {
                        totalLines++;
                    }
                }
            }

            sw.Stop();
            Console.WriteLine($"✅ Sequential read completed. Total lines: {totalLines:N0}");
            Console.WriteLine($"⏱️ Time Taken: {sw.ElapsedMilliseconds} ms\n");
        }

        // ==========================================================
        // ⚙️ STEP 3: Asynchronous Read (awaiting multiple Tasks)
        // ==========================================================
        private static async Task RunAsyncRead(string path)
        {
            Console.WriteLine("=== 2️⃣ Asynchronous File Reading ===");

            var sw = Stopwatch.StartNew();
            string[] files = Directory.GetFiles(path, "*.txt");

            // Create a list of asynchronous read tasks
            List<Task<int>> tasks = files.Select(ReadFileAsync).ToList();

            int[] lineCounts = await Task.WhenAll(tasks);
            int totalLines = lineCounts.Sum();

            sw.Stop();
            Console.WriteLine($"✅ Async read completed. Total lines: {totalLines:N0}");
            Console.WriteLine($"⏱️ Time Taken: {sw.ElapsedMilliseconds} ms\n");
        }

        private static async Task<int> ReadFileAsync(string filePath)
        {
            int count = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                while (await reader.ReadLineAsync() is string line)
                {
                    count++;
                }
            }
            return count;
        }

        // ==========================================================
        // 🚀 STEP 4: Parallel Read (multi-threaded)
        // ==========================================================
        private static void RunParallelRead(string path)
        {
            Console.WriteLine("=== 3️⃣ Parallel File Reading ===");

            var sw = Stopwatch.StartNew();
            string[] files = Directory.GetFiles(path, "*.txt");

            int totalLines = 0;
            object lockObj = new object(); // to protect shared counter

            Parallel.ForEach(files, file =>
            {
                int localCount = 0;
                using (StreamReader reader = new StreamReader(file))
                {
                    while (reader.ReadLine() != null)
                    {
                        localCount++;
                    }
                }

                // Update shared variable safely
                lock (lockObj)
                {
                    totalLines += localCount;
                }
            });

            sw.Stop();
            Console.WriteLine($"✅ Parallel read completed. Total lines: {totalLines:N0}");
            Console.WriteLine($"⏱️ Time Taken: {sw.ElapsedMilliseconds} ms\n");
        }
    }
}
