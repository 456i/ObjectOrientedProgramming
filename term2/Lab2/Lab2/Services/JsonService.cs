using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Lab2;

namespace Lab2
{
    /// <summary>
    /// Дополнительный сервис — сохранение и загрузка через JSON
    /// (альтернатива FileManager с форматом .itlab).
    /// </summary>
    public static class JsonService
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true
        };

        /// <summary>Сериализует список компьютеров в JSON-файл.</summary>
        public static void SaveJson(string filePath, List<Computer> computers)
        {
            var json = JsonSerializer.Serialize(computers, _options);
            File.WriteAllText(filePath, json);
        }

        /// <summary>Десериализует список компьютеров из JSON-файла.</summary>
        public static List<Computer> LoadJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Computer>>(json) ?? new List<Computer>();
        }
    }
}