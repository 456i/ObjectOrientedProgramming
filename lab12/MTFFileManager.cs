using System.IO.Compression;

public class MTFFileManager
{
    public void CreateDir(string path)
    {
        try
        {
            Directory.CreateDirectory(path);
            Console.WriteLine($"Директория создана: {path}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании директории: {ex.Message}");
        }
    }

    public void CopyFile(string source, string dest)
    {
        try
        {
            File.Copy(source, dest, true);
            Console.WriteLine($"Файл скопирован: {source} → {dest}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при копировании файла: {ex.Message}");
        }
    }

    public void MoveFile(string source, string dest)
    {
        try
        {
            File.Move(source, dest, true);
            Console.WriteLine($"Файл перемещён: {source} → {dest}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при перемещении файла: {ex.Message}");
        }
    }

    public void CreateZip(string sourceDir, string zipPath)
    {
        try
        {
            if (File.Exists(zipPath))
                File.Delete(zipPath);

            ZipFile.CreateFromDirectory(sourceDir, zipPath);
            Console.WriteLine($"Архив создан: {zipPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании архива: {ex.Message}");
        }
    }

    public void ExtractZip(string zipPath, string extractDir)
    {
        try
        {
            ZipFile.ExtractToDirectory(zipPath, extractDir);
            Console.WriteLine($"Архив разархивирован в: {extractDir}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при разархивировании: {ex.Message}");
        }
    }
}
