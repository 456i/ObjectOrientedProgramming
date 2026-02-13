public class MTFDiskInfo
{
    public void ShowFreeSpace()
    {
        try
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                    Console.WriteLine(
                        $"Диск {drive.Name}: свободно {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} GB"
                    );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении информации о диске: {ex.Message}");
        }
    }

    public void ShowFileSystem()
    {
        try
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                    Console.WriteLine($"Диск {drive.Name}: файловая система {drive.DriveFormat}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении файловой системы: {ex.Message}");
        }
    }

    public void ShowAllDisksInfo()
    {
        try
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                    Console.WriteLine(
                        $"Диск {drive.Name}, Объем: {drive.TotalSize / (1024 * 1024 * 1024)} GB, "
                            + $"Свободно: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} GB, Метка: {drive.VolumeLabel}"
                    );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при выводе информации о дисках: {ex.Message}");
        }
    }
}
