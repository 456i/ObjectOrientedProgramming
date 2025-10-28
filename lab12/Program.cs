class Program
{
    static void Main()
    {
        Console.WriteLine("=== Демонстрация работы классов MTF ===\n");

        // 1️ Работа с логами
        var log = new MTFLog();
        log.WriteLog("Запуск программы", "Программа Lab12 стартовала");
        log.WriteLog(
            "Демонстрация",
            "Проверка работы MTFDiskInfo, MTFFileInfo, MTFDirInfo, MTFFileManager"
        );

        Console.WriteLine("=== Чтение логов ===");
        log.ReadLog();
        Console.WriteLine();

        // 2️ Работа с дисками
        var diskInfo = new MTFDiskInfo();
        Console.WriteLine("=== Информация о свободном месте на дисках ===");
        diskInfo.ShowFreeSpace();
        Console.WriteLine("\n=== Файловые системы дисков ===");
        diskInfo.ShowFileSystem();
        Console.WriteLine("\n=== Полная информация по дискам ===");
        diskInfo.ShowAllDisksInfo();
        Console.WriteLine();

        // 3️ Работа с файлами
        var fileInfo = new MTFFileInfo();
        string testFile = "test.txt";
        System.IO.File.WriteAllText(testFile, "Тестовая запись в файл");
        Console.WriteLine($"=== Информация о файле {testFile} ===");
        fileInfo.ShowFileInfo(testFile);
        Console.WriteLine();

        // 4️ Работа с директориями
        var dirInfo = new MTFDirInfo();
        string testDir = Environment.CurrentDirectory; // текущая директория
        Console.WriteLine($"=== Информация о директории {testDir} ===");
        dirInfo.ShowDirInfo(testDir);
        Console.WriteLine();

        // 5️ Работа с файловым менеджером
        var fileManager = new MTFFileManager();
        string inspectDir = "MTFInspect";
        string filesDir = "MTFFiles";

        // Создание директории
        fileManager.CreateDir(inspectDir);
        fileManager.CreateDir(filesDir);

        // Копирование и перемещение тестового файла
        string copyFile = System.IO.Path.Combine(inspectDir, "test_copy.txt");
        fileManager.CopyFile(testFile, copyFile);
        string movedFile = System.IO.Path.Combine(inspectDir, "test_moved.txt");
        fileManager.MoveFile(copyFile, movedFile);

        // Архивирование и разархивирование
        string zipPath = "MTFFiles.zip";
        fileManager.CreateZip(filesDir, zipPath);
        string extractDir = "MTFExtracted";
        fileManager.CreateDir(extractDir);
        fileManager.ExtractZip(zipPath, extractDir);

        Console.WriteLine("\n=== Демонстрация завершена ===");
    }
}
