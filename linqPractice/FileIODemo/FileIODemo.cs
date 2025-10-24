using System;
using System.IO;
using System.Text;

namespace linqPractice
{
    // ===================== FILE I/O DEMO ===================== //
    public static class FileIODemo
    {
        public static void Run()
        {
            Console.WriteLine("===== 📁 FILE HANDLING & STREAMS DEMO =====\n");

            string basePath = Path.Combine(Environment.CurrentDirectory, "DemoFiles");
            string filePath = Path.Combine(basePath, "students.txt");
            string logPath = Path.Combine(basePath, "log.txt");

            // ✅ Create directory if it doesn't exist
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
                Console.WriteLine($"📁 Created directory: {basePath}");
            }

            // 1️⃣ CREATE & WRITE FILE
            Console.WriteLine("\n=== 1️⃣ Writing to a File ===");
            WriteStudentsToFile(filePath);

            // 2️⃣ READ FILE
            Console.WriteLine("\n=== 2️⃣ Reading from a File ===");
            ReadStudentsFromFile(filePath);

            // 3️⃣ APPEND TO FILE
            Console.WriteLine("\n=== 3️⃣ Appending Data to a File ===");
            AppendStudent(filePath, "Eve,23,Physics");
            ReadStudentsFromFile(filePath);

            // 4️⃣ STREAM WRITER / READER DEMO
            Console.WriteLine("\n=== 4️⃣ Using StreamWriter and StreamReader ===");
            UseStreamsExample(filePath);

            // 5️⃣ FILE INFO AND OPERATIONS
            Console.WriteLine("\n=== 5️⃣ File Info and Operations ===");
            ShowFileDetails(filePath);

            // 6️⃣ LOGGING EXAMPLE
            Console.WriteLine("\n=== 6️⃣ Logging Example (Append Log File) ===");
            LogAction(logPath, "Student file updated.");
            LogAction(logPath, "New student added.");
            DisplayLog(logPath);

            // 7️⃣ EXCEPTION HANDLING DEMO
            Console.WriteLine("\n=== 7️⃣ Exception Handling Example ===");
            try
            {
                string missingFile = Path.Combine(basePath, "no_file.txt");
                Console.WriteLine(File.ReadAllText(missingFile)); // Will throw FileNotFoundException
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"❌ Error: File not found! {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Unexpected Error: {ex.Message}");
            }

            Console.WriteLine("\n===== END OF FILE I/O DEMO =====");
        }

        // ============================================================
        // 🧱 1️⃣ Write File (Create new file and write data)
        // ============================================================
        private static void WriteStudentsToFile(string path)
        {
            string[] students =
            {
                "Alice,20,Mathematics",
                "Bob,21,Computer Science",
                "Charlie,22,Engineering",
                "David,19,Chemistry"
            };

            // File.WriteAllLines automatically creates or overwrites file
            File.WriteAllLines(path, students);
            Console.WriteLine($"✅ File created and written at: {path}");
        }

        // ============================================================
        // 📖 2️⃣ Read File (Read all lines safely)
        // ============================================================
        private static void ReadStudentsFromFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("⚠️ File not found!");
                return;
            }

            string[] lines = File.ReadAllLines(path);

            if (lines.Length == 0)
            {
                Console.WriteLine("⚠️ File is empty.");
                return;
            }

            foreach (var line in lines)
            {
                // ✅ Skip empty lines or malformed data
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] data = line.Split(',');

                // ✅ Ensure there are 3 columns before accessing indexes
                if (data.Length < 3)
                {
                    Console.WriteLine($"⚠️ Skipping invalid line: {line}");
                    continue;
                }

                string name = data[0].Trim();
                string age = data[1].Trim();
                string course = data[2].Trim();

                Console.WriteLine($"Name: {name}, Age: {age}, Course: {course}");
            }
        }

        // ============================================================
        // ➕ 3️⃣ Append Data Safely
        // ============================================================
        private static void AppendStudent(string path, string newStudent)
        {
            try
            {
                File.AppendAllText(path, Environment.NewLine + newStudent);
                Console.WriteLine($"✅ Added: {newStudent}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to append student: {ex.Message}");
            }
        }

        // ============================================================
        // 💡 4️⃣ StreamReader & StreamWriter Example
        // ============================================================
        private static void UseStreamsExample(string path)
        {
            string streamFile = Path.Combine(Path.GetDirectoryName(path), "stream_demo.txt");

            // "using" ensures automatic cleanup
            using (StreamWriter writer = new StreamWriter(streamFile))
            {
                writer.WriteLine("Hello from StreamWriter!");
                writer.WriteLine("This text is being written line by line.");
                writer.WriteLine("Streams are efficient for large files.");
            }

            Console.WriteLine("✅ Data written using StreamWriter.");

            // Reading it back
            using (StreamReader reader = new StreamReader(streamFile))
            {
                Console.WriteLine("\n📖 Reading using StreamReader:");
                string content;
                while ((content = reader.ReadLine()) != null)
                {
                    Console.WriteLine(content);
                }
            }
        }

        // ============================================================
        // 🧾 5️⃣ File Info and Operations
        // ============================================================
        private static void ShowFileDetails(string path)
        {
            FileInfo info = new FileInfo(path);

            if (!info.Exists)
            {
                Console.WriteLine("⚠️ File not found!");
                return;
            }

            Console.WriteLine($"File Name: {info.Name}");
            Console.WriteLine($"Full Path: {info.FullName}");
            Console.WriteLine($"Size: {info.Length} bytes");
            Console.WriteLine($"Created On: {info.CreationTime}");
            Console.WriteLine($"Last Modified: {info.LastWriteTime}");
        }

        // ============================================================
        // 📝 6️⃣ Logging Example (Practical Real-World Use)
        // ============================================================
        private static void LogAction(string logPath, string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logPath, append: true))
                {
                    writer.WriteLine($"{DateTime.Now:G} - {message}");
                }
                Console.WriteLine($"📄 Log written: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to write log: {ex.Message}");
            }
        }

        private static void DisplayLog(string logPath)
        {
            Console.WriteLine("\n📜 Log File Content:");
            if (!File.Exists(logPath))
            {
                Console.WriteLine("⚠️ Log file not found.");
                return;
            }

            string[] logs = File.ReadAllLines(logPath);
            if (logs.Length == 0)
            {
                Console.WriteLine("⚠️ Log file is empty.");
                return;
            }

            foreach (var line in logs)
                Console.WriteLine(line);
        }
    }
}
