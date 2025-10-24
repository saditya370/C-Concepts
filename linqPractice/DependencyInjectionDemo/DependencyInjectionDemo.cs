using System;

namespace linqPractice
{
    // ===================== DEPENDENCY INJECTION DEMO ===================== //
    public static class DependencyInjectionDemo
    {
        public static void Run()
        {
            Console.WriteLine("===== ⚙️ DEPENDENCY INJECTION DEMO =====\n");

            // 1️⃣ Without Dependency Injection (Tight Coupling)
            Console.WriteLine("=== 1️⃣ Without Dependency Injection ===");
            var notification1 = new NotificationServiceWithoutDI();
            notification1.Send();

            // 2️⃣ With Manual Dependency Injection (Loose Coupling)
            Console.WriteLine("\n=== 2️⃣ With Manual Dependency Injection ===");
            INotifier emailNotifier = new EmailNotifier();
            var notification2 = new NotificationService(emailNotifier);
            notification2.Send("Hello via Email!");

            // 3️⃣ Swap the dependency easily
            Console.WriteLine("\n=== 3️⃣ Swapping Dependencies Dynamically ===");
            INotifier smsNotifier = new SmsNotifier();
            var notification3 = new NotificationService(smsNotifier);
            notification3.Send("Hello via SMS!");

            // 4️⃣ Injecting Mock dependency (useful for testing)
            Console.WriteLine("\n=== 4️⃣ Mock Dependency (Testing Example) ===");
            INotifier mockNotifier = new MockNotifier();
            var testService = new NotificationService(mockNotifier);
            testService.Send("Test Message");

            Console.WriteLine("\n===== ✅ END OF DEPENDENCY INJECTION DEMO =====");
        }
    }

    // ===================== 1️⃣ Tight Coupling Example ===================== //
    public class NotificationServiceWithoutDI
    {
        // Directly depends on a concrete class
        private EmailNotifier _notifier = new EmailNotifier();

        public void Send()
        {
            _notifier.Send("Hello from tightly coupled code!");
        }
    }

    // ===================== 2️⃣ Define an Abstraction (Interface) ===================== //
    public interface INotifier
    {
        void Send(string message);
    }

    // ===================== 3️⃣ Implement Multiple Notifiers ===================== //
    public class EmailNotifier : INotifier
    {
        public void Send(string message)
        {
            Console.WriteLine($"📧 Sending Email: {message}");
        }
    }

    public class SmsNotifier : INotifier
    {
        public void Send(string message)
        {
            Console.WriteLine($"📱 Sending SMS: {message}");
        }
    }

    // ===================== 4️⃣ Mock Notifier for Testing ===================== //
    public class MockNotifier : INotifier
    {
        public void Send(string message)
        {
            Console.WriteLine($"🧪 [Mock] Would send: {message}");
        }
    }

    // ===================== 5️⃣ Service Using Dependency Injection ===================== //
    public class NotificationService
    {
        private readonly INotifier _notifier; // Abstraction instead of concrete class

        // Dependency is injected via constructor
        public NotificationService(INotifier notifier)
        {
            _notifier = notifier;
        }

        public void Send(string message)
        {
            _notifier.Send(message);
        }
    }
}
