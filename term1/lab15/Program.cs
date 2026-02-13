using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main() // для консольных приложений -> Task
    // static void Main()
    {
        // 1. Длительная задача с решетом Эратосфена
        Console.WriteLine("=== Задание 1: Длительная задача ===");

        Console.WriteLine("Последовательное вычисление");

        var num = 21474836;
        // //2147483647 Max Int
        var sw = Stopwatch.StartNew();
        var result1 = SieveOfEratosthenes(num);
        var result2 = SieveOfEratosthenes(num);
        var result3 = SieveOfEratosthenes(num);
        sw.Stop();
        Console.WriteLine($"Выполнено за {sw.ElapsedMilliseconds} ms\n");

        Console.WriteLine("Параллельное вычисление");
        sw.Restart();

        Task<List<int>> sieveTask1 = Task.Run(() => SieveOfEratosthenes(num));
        Task<List<int>> sieveTask2 = Task.Run(() => SieveOfEratosthenes(num));
        Task<List<int>> sieveTask3 = Task.Run(() => SieveOfEratosthenes(num));

        Console.WriteLine($"Id задачи: 1-{sieveTask1.Id} 2-{sieveTask2.Id} 3-{sieveTask3.Id}");
        Console.WriteLine(
            $"Статус: 1-{sieveTask1.Status} 2-{sieveTask2.Status} 3-{sieveTask3.Status}"
        );
        Console.WriteLine(
            $"IsCompleted: 1-{sieveTask1.IsCompleted} 2-{sieveTask2.IsCompleted} 3-{sieveTask3.IsCompleted}"
        );

        // Task.WaitAll(sieveTask1, sieveTask2, sieveTask3); можно убрать .Result вызывает неявное ожидание

        sw.Stop();

        Console.WriteLine(
            $"Найдено 1-{sieveTask1.Result.Count} 2-{sieveTask2.Result.Count} 3-{sieveTask3.Result.Count} простых чисел"
        );
        Console.WriteLine($"Выполнено за {sw.ElapsedMilliseconds} ms\n");

        // 2. Задача с отменой
        Console.WriteLine("=== Задание 2: Отмена задачи ===");

        sw.Restart();
        using var cts = new CancellationTokenSource();
        var cancelTask = Task.Run(() => SieveWithCancellation(num, cts.Token));

        var cancelTime = 10;
        cts.CancelAfter(cancelTime);
        Console.WriteLine($"Отмена запланирована через {cancelTime}ms");

        try
        {
            cancelTask.Wait();
            sw.Stop();

            if (cancelTask.IsCompletedSuccessfully)
            {
                Console.WriteLine($"Найдено {cancelTask.Result.Count} простых чисел");
            }
        }
        catch (AggregateException ex)
        {
            sw.Stop();
            if (ex.InnerException is OperationCanceledException)
            {
                Console.WriteLine($"Задача отменена! Выполнено за {sw.ElapsedMilliseconds}ms");
            }
            else
            {
                Console.WriteLine($"Ошибка: {ex.InnerException?.Message}");
            }
        }

        Console.WriteLine($"Финальный статус: {cancelTask.Status}");
        Console.WriteLine($"IsCanceled: {cancelTask.IsCanceled}");
        Console.WriteLine($"IsCompleted: {cancelTask.IsCompleted}\n");

        // 3. Задачи с возвратом результата
        Console.WriteLine("=== Задание 3: Задачи с возвратом результата ===");
        Task<int> task1 = Task.Run(() => CalculatePartialSum(1, 10));
        Task<int> task2 = Task.Run(() => CalculatePartialSum(10, 30));
        Task<int> task3 = Task.Run(() => CalculatePartialSum(30, 50));

        Task<double> finalTask = Task.Run(() =>
        {
            return (task1.Result + task2.Result + task3.Result) / 3.0;
        });
        // .Result автоматическая блокировка до выполнения
        Console.WriteLine($"Среднее значение: {finalTask.Result}\n");

        // 4. Задачи продолжения
        Console.WriteLine("=== Задание 4: Задачи продолжения ===");

        // Вариант 1: ContinueWith
        Task<int> baseTask = Task.Run(() => Enumerable.Range(1, 1000).Sum());
        Task continuation = baseTask.ContinueWith(t =>
            Console.WriteLine($"ContinueWith результат: {t.Result}")
        );
        continuation.Wait();

        // Вариант 2: GetAwaiter
        Task<int> baseTask2 = Task.Run(() => Enumerable.Range(1, 500).Sum());
        var awaiter = baseTask2.GetAwaiter();
        awaiter.OnCompleted(() =>
            Console.WriteLine($"GetAwaiter результат: {awaiter.GetResult()}\n")
        );
        Thread.Sleep(100);

        // 5. Parallel.For/ForEach
        Console.WriteLine("=== Задание 5: Parallel.For ===");
        int[] numbers = new int[100000000];

        sw.Restart();
        Parallel.For(0, numbers.Length, i => numbers[i] = i *  2);
        sw.Stop();
        Console.WriteLine($"Parallel.For: {sw.ElapsedMilliseconds} ms");

        sw.Restart();
        for (int i = 0; i < numbers.Length; i++)
            numbers[i] = i * 2;
        sw.Stop();
        Console.WriteLine($"Обычный цикл: {sw.ElapsedMilliseconds} ms\n");

        // 6. Parallel.Invoke
        Console.WriteLine("=== Задание 6: Parallel.Invoke ===");
        Parallel.Invoke(
            () => Console.WriteLine($"Выполнено в потоке {Thread.CurrentThread.ManagedThreadId}"),
            () => Console.WriteLine($"Выполнено в потоке {Thread.CurrentThread.ManagedThreadId}"),
            () => Console.WriteLine($"Выполнено в потоке {Thread.CurrentThread.ManagedThreadId}\n")
        );

        // 7. BlockingCollection
        Console.WriteLine("=== Задание 7: BlockingCollection ===");
        var warehouse = new BlockingCollection<string>();

        // Поставщики
        var suppliers = Task.Run(() =>
        {
            string[] products =
            {
                "Холодильник",
                "Телевизор",
                "Стиральная машина",
                "Пылесос",
                "Микроволновка",
            };
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(200);
                warehouse.Add(products[i]);
                Console.WriteLine(
                    $"Поставлен: {products[i]} | Товаров на складе: {warehouse.Count}"
                );
            }
            warehouse.CompleteAdding();
        });

        // Покупатели
        var consumers = Task.Run(() =>
        {
            Random rand = new Random();
            while (!warehouse.IsCompleted)
            {
                try
                {
                    string product = warehouse.Take(); // явная блокировка выполнения потока
                    Console.WriteLine($"Куплен: {product} | Осталось: {warehouse.Count}");
                    Thread.Sleep(350);
                }
                catch (InvalidOperationException) { }
            }
        });

        Task.WaitAll(suppliers, consumers);
        Console.WriteLine();

        // 8. Async/Await
        Console.WriteLine("=== Задание 8: Async/Await ===");
        // AsyncMethod().GetAwaiter().GetResult();
        // AsyncMethod().Wait();
        await AsyncMethod();
    }

    // Решето Эратосфена

    static List<int> SieveOfEratosthenes(int n)
    {
        var bitSet = new bool[n + 1];
        Array.Fill(bitSet, true);
        var result = new List<int>();
        bitSet[0] = bitSet[1] = false;

        for (int i = 2; i <= n; i++)
        {
            if (bitSet[i])
            {
                result.Add(i);
                for (int j = i * i; j <= n && j > 0; j += i)
                    bitSet[j] = false;
            }
        }
        return result;
    }

    static List<int> SieveWithCancellation(int n, CancellationToken token)
    {
        var bitSet = new bool[n + 1];
        Array.Fill(bitSet, true);
        var result = new List<int>();
        bitSet[0] = bitSet[1] = false;

        for (int i = 2; i <= n; i++)
        {
            if (bitSet[i])
            {
                token.ThrowIfCancellationRequested();
                result.Add(i);
                for (int j = i * i; j <= n && j > 0; j += i)
                    bitSet[j] = false;
            }
        }
        return result;
    }

    static int CalculatePartialSum(int start, int end)
    {
        return Enumerable.Range(start, end - start + 1).Sum();
    }

    static async Task AsyncMethod() // корутина
    {
        await Task.Delay(1000);
        Console.WriteLine("Асинхронный метод завершен!");
    }
}
