class EvenOddNumbers
{
    private static readonly object fileLock = new object();
    private static int currentNumber = 1;
    private static readonly int maxNumber = 20;

    public static void RunEvenOddThreads()
    {
        Console.WriteLine("\n=== ЗАДАНИЕ 4: ЧЕТНЫЕ И НЕЧЕТНЫЕ ЧИСЛА ===");

        // Очистка файла
        File.WriteAllText("numbers.txt", "");

        // Поток для четных чисел
        Thread evenThread = new Thread(PrintEvenNumbers)
        {
            Name = "EvenThread",
            Priority = ThreadPriority.BelowNormal,
        };

        // Поток для нечетных чисел
        Thread oddThread = new Thread(PrintOddNumbers)
        {
            Name = "OddThread",
            Priority = ThreadPriority.Normal,
        };

        // Запуск потоков
        evenThread.Start();
        oddThread.Start();

        // Ожидание завершения
        evenThread.Join();
        oddThread.Join();

        Console.WriteLine("Оба потока завершили работу");
        Console.WriteLine("Результат сохранен в файл numbers.txt");
    }

    // Вариант A: Сначала четные, потом нечетные
    static void PrintEvenNumbers()
    {
        for (int i = 2; i <= maxNumber; i += 2)
        {
            string result = $"Четное: {i}";
            Console.WriteLine(result);

            lock (fileLock)
            {
                File.AppendAllText("numbers.txt", result + Environment.NewLine);
            }

            Thread.Sleep(100); // Медленнее
        }
    }

    static void PrintOddNumbers()
    {
        for (int i = 1; i <= maxNumber; i += 2)
        {
            string result = $"Нечетное: {i}";
            Console.WriteLine(result);

            lock (fileLock)
            {
                File.AppendAllText("numbers.txt", result + Environment.NewLine);
            }

            Thread.Sleep(50); // Быстрее
        }
    }

    // Вариант B: Чередование четных и нечетных (синхронизированно)
    public static void RunSyncEvenOdd()
    {
        Console.WriteLine("\n=== ЧЕРЕДОВАНИЕ ЧЕТНЫХ И НЕЧЕТНЫХ ===");

        File.WriteAllText("sync_numbers.txt", "");
        currentNumber = 1;

        Thread syncEvenThread = new Thread(PrintEvenSync);
        Thread syncOddThread = new Thread(PrintOddSync);

        syncEvenThread.Start();
        syncOddThread.Start();

        syncEvenThread.Join();
        syncOddThread.Join();
    }

    static void PrintEvenSync()
    {
        while (currentNumber <= maxNumber)
        {
            lock (fileLock)
            {
                if (currentNumber % 2 == 0 && currentNumber <= maxNumber)
                {
                    string result = $"Четное (sync): {currentNumber}";
                    Console.WriteLine(result);
                    File.AppendAllText("sync_numbers.txt", result + Environment.NewLine);
                    currentNumber++;
                }
            }
            Thread.Sleep(10);
        }
    }

    static void PrintOddSync()
    {
        while (currentNumber <= maxNumber)
        {
            lock (fileLock)
            {
                if (currentNumber % 2 == 1 && currentNumber <= maxNumber)
                {
                    string result = $"Нечетное (sync): {currentNumber}";
                    Console.WriteLine(result);
                    File.AppendAllText("sync_numbers.txt", result + Environment.NewLine);
                    currentNumber++;
                }
            }
            Thread.Sleep(10);
        }
    }
}
