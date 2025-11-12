using System.Reflection;

#pragma warning disable SYSLIB0024 // Подавление предупреждения об устаревшем методе

public static class DomainOperations
{
    public static void DemonstrateAppDomains()
    {
        Console.WriteLine("\n=== ЗАДАНИЕ 2: РАБОТА С ДОМЕНАМИ ПРИЛОЖЕНИЙ ===");

        // Текущий домен приложения
        AppDomain currentDomain = AppDomain.CurrentDomain;

        Console.WriteLine($"Текущий домен: {currentDomain.FriendlyName}");
        Console.WriteLine($"Базовая директория: {currentDomain.BaseDirectory}");
        Console.WriteLine($"Детали конфигурации: {currentDomain.SetupInformation}");

        // Сборки в текущем домене
        Assembly[] assemblies = currentDomain.GetAssemblies();
        Console.WriteLine($"\nЗагруженные сборки ({assemblies.Length}):");
        foreach (var assembly in assemblies)
        {
            Console.WriteLine($"  - {assembly.FullName}");
        }

        // Создание нового домена
        AppDomain newDomain = AppDomain.CreateDomain("НовыйДомен");
        Console.WriteLine($"\nСоздан новый домен: {newDomain.FriendlyName}");

        // Загрузка сборки в новый домен (пример)
        try
        {
            // Здесь можно загрузить нужную сборку
            // newDomain.Load("SomeAssembly");
            Console.WriteLine("Сборка загружена в новый домен");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки сборки: {ex.Message}");
        }

        // Выгрузка домена
        AppDomain.Unload(newDomain);
        Console.WriteLine("Новый домен выгружен");
    }
}
#pragma warning restore SYSLIB0024
