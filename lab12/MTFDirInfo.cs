public class MTFDirInfo
{
    public void ShowDirInfo(string dirPath)
    {
        try
        {
            DirectoryInfo di = new DirectoryInfo(dirPath);
            if (!di.Exists)
            {
                Console.WriteLine("Директория не найдена.");
                return;
            }

            Console.WriteLine($"Директория: {di.FullName}");
            Console.WriteLine($"Создана: {di.CreationTime}");
            Console.WriteLine($"Количество файлов: {di.GetFiles().Length}");
            Console.WriteLine($"Количество поддиректорий: {di.GetDirectories().Length}");

            Console.WriteLine("Родительские директории:");
            DirectoryInfo parent = di.Parent;
            while (parent != null)
            {
                Console.WriteLine(parent.FullName);
                parent = parent.Parent;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении информации о директории: {ex.Message}");
        }
    }
}
