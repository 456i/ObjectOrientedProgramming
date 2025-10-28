public class MTFLog
{
    private string logFile = "mtflogfile.txt";

    public void WriteLog(string action, string details)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(logFile, true))
            {
                sw.WriteLine($"{DateTime.Now}: {action} - {details}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка записи в лог: {ex.Message}");
        }
    }

    public void ReadLog()
    {
        try
        {
            using (StreamReader sr = new StreamReader(logFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    Console.WriteLine(line);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка чтения лога: {ex.Message}");
        }
    }
}
