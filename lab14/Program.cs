class Program
{
    static void Main()
    {
        // Задание 1: Информация о процессах
        ProcessInfo.DisplayProcessInfo();

        // Задание 2: Домены приложений
        DomainOperations.DemonstrateAppDomains();

        // Задание 3: Простые числа в потоке
        PrimeNumberCalculator.CalculatePrimes(50);

        // Задание 4: Четные и нечетные числа
        EvenOddNumbers.RunEvenOddThreads();
        EvenOddNumbers.RunSyncEvenOdd();

        // Задание 5: Timer
        TimerTask.DemonstrateTimer();

        Console.ReadLine();
    }
}
