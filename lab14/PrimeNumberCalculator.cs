public static class PrimeNumberCalculator
{
    private static Thread? calculationThread; // Добавлен nullable
    private static bool isPaused = false;
    private static bool shouldStop = false;

    public static void CalculatePrimes(int n)
    {
        Console.WriteLine("\n=== ЗАДАНИЕ 3: ПОТОК ДЛЯ РАСЧЕТА ПРОСТЫХ ЧИСЕЛ ===");

        using (StreamWriter writer = new StreamWriter("primes.txt", append: true))
        {
            calculationThread = new Thread(() =>
            {
                Console.WriteLine($"Поиск простых чисел от 1 до {n}");
                writer.WriteLine($"=== Простые числа от 1 до {n} ===");

                for (int i = 2; i <= n && !shouldStop; i++)
                {
                    // Пауза
                    while (isPaused && !shouldStop)
                    {
                        Thread.Sleep(100);
                    }

                    if (IsPrime(i))
                    {
                        string result = $"Простое число: {i}";
                        Console.WriteLine(result);
                        writer.WriteLine(result);
                    }

                    Thread.Sleep(50); // Имитация сложных вычислений
                }

                if (!shouldStop)
                {
                    Console.WriteLine("Расчет завершен!");
                    writer.WriteLine("=== Расчет завершен ===");
                }
            });

            // Настройка потока
            calculationThread.Name = "PrimeCalculator";
            calculationThread.Priority = ThreadPriority.Normal;

            // Запуск потока
            calculationThread.Start();
            PrintThreadInfo(calculationThread);

            // Управление потоком
            Thread.Sleep(1000);
            PauseCalculation();
            Console.WriteLine("Поток приостановлен");
            Thread.Sleep(2000);
            ResumeCalculation();
            Console.WriteLine("Поток возобновлен");
            Thread.Sleep(1000);
            StopCalculation();

            // Ожидание завершения
            calculationThread.Join();
            Console.WriteLine("Поток завершил работу");
        }
    }

    static bool IsPrime(int number)
    {
        if (number <= 1)
            return false;
        if (number == 2)
            return true;
        if (number % 2 == 0)
            return false;

        for (int i = 3; i <= Math.Sqrt(number); i += 2)
        {
            if (number % i == 0)
                return false;
        }
        return true;
    }

    public static void PauseCalculation() => isPaused = true;

    public static void ResumeCalculation() => isPaused = false;

    public static void StopCalculation() => shouldStop = true;

    static void PrintThreadInfo(Thread thread)
    {
        Console.WriteLine($"\nИнформация о потоке:");
        Console.WriteLine($"  Имя: {thread.Name}");
        Console.WriteLine($"  ID: {thread.ManagedThreadId}");
        Console.WriteLine($"  Приоритет: {thread.Priority}");
        Console.WriteLine($"  Состояние: {thread.ThreadState}");
        Console.WriteLine($"  Фонный: {thread.IsBackground}");
    }
}
