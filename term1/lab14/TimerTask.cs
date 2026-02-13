public static class TimerTask
{
    private static Timer? timer; // Добавлен nullable
    private static int executionCount = 0;
    private static readonly int maxExecutions = 5;

    public static void DemonstrateTimer()
    {
        Console.WriteLine("\n=== ЗАДАНИЕ 5: ПОВТОРЯЮЩАЯСЯ ЗАДАЧА С TIMER ===");

        File.WriteAllText("timer_log.txt", "=== Лог таймера ===\n");

        timer = new Timer(TimerCallback, null, 1000, 2000);

        Console.WriteLine("Таймер запущен. Ожидание выполнения...");

        Thread.Sleep(11000);

        timer?.Dispose(); // Добавлена проверка на null
        Console.WriteLine("Таймер остановлен");
        Console.WriteLine("Лог сохранен в timer_log.txt");
    }

    static void TimerCallback(object? state) // Добавлен nullable параметр
    {
        executionCount++;
        string message = $"[{DateTime.Now:HH:mm:ss}] Выполнение #{executionCount}";

        Console.WriteLine(message);
        File.AppendAllText("timer_log.txt", message + Environment.NewLine);

        if (executionCount >= maxExecutions)
        {
            timer?.Change(Timeout.Infinite, Timeout.Infinite); // Добавлена проверка на null
        }
    }
}
