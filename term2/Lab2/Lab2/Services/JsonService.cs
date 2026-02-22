using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;

namespace Lab2.Services
{
    public static class JsonService
    {
        public static void Save(List<Models.Computer> computers, string path)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(computers, options);
            File.WriteAllText(path, json);
        }

        public static List<Models.Computer> Load(string path)
        {
            if (!File.Exists(path))
                return new List<Models.Computer>();

            string json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<Models.Computer>>(json)
                   ?? new List<Models.Computer>();
        }

    }
}
