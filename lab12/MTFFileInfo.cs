public class MTFFileInfo
{
    public void ShowFileInfo(string filePath)
    {
        try
        {
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Exists)
            {
                Console.WriteLine("Файл не найден.");
                return;
            }

            Console.WriteLine($"Полный путь: {fi.FullName}");
            Console.WriteLine(
                $"Имя: {fi.Name}, Расширение: {fi.Extension}, Размер: {fi.Length} байт"
            );
            Console.WriteLine($"Создан: {fi.CreationTime}, Изменён: {fi.LastWriteTime}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении информации о файле: {ex.Message}");
        }
    }
}
