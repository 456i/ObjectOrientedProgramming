using System.Diagnostics;

public static class ProcessInfo
{
    public static void DisplayProcessInfo()
    {
        Console.WriteLine("=== ЗАДАНИЕ 1: ИНФОРМАЦИЯ О ПРОЦЕССАХ ===");

        // Получаем все запущенные процессы
        Process[] processes = Process.GetProcesses();

        // Записываем в файл
        using (StreamWriter writer = new StreamWriter("processes.txt"))
        {
            writer.WriteLine(
                $"{"ID", -10} {"Имя", -25} {"Приоритет", -12} {"Время запуска", -20} {"Состояние", -15} {"Время CPU", -15}"
            );
            writer.WriteLine(new string('-', 100));

            foreach (var process in processes)
            {
                try
                {
                    string processInfo =
                        $"{process.Id, -10} {process.ProcessName, -25} "
                        + $"{process.BasePriority, -12} {process.StartTime, -20:yyyy-MM-dd HH:mm:ss} "
                        + $"{GetProcessState(process), -15} {process.TotalProcessorTime, -15}";

                    Console.WriteLine(processInfo);
                    writer.WriteLine(processInfo);
                }
                catch (Exception ex)
                {
                    // Некоторые процессы могут быть недоступны
                    Console.WriteLine(
                        $"Ошибка доступа к процессу {process.ProcessName}: {ex.Message}"
                    );
                }
            }
        }

        Console.WriteLine(
            $"\nИнформация о {processes.Length} процессах сохранена в файл processes.txt"
        );
    }

    // Добавленный метод
    private static string GetProcessState(Process process)
    {
        try
        {
            return process.Responding ? "Running" : "Not Responding";
        }
        catch
        {
            return "Unknown";
        }
    }
}
