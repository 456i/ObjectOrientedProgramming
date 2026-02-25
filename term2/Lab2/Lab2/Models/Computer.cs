using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Lab2
{
    public class Computer
    {
        public string Type { get; set; } = "";
        public Processor CPU { get; set; } = new();
        public GPU VideoCard { get; set; } = new();
        public int RAMsizeGB { get; set; }
        public string RAMtype { get; set; } = "";
        public int HDDsizeGB { get; set; }
        public string HDDtype { get; set; } = "";
        public DateTime PurchaseDate { get; set; } = DateTime.Today;
        public bool HasMonitor { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasMouse { get; set; }

        /// <summary>
        /// Итоговая стоимость = (CPU + GPU + ОЗУ + Диск + Периферия) × коэффициент типа
        /// </summary>
        public decimal Price()
        {
            decimal p = CPU.Price() / 10 + VideoCard.Price();

            decimal RAMTypePrice = RAMtype switch
            {
                "DDR3" => 1.1m,
                "DDR4" => 1.7m,
                "DDR5" => 2.8m,
                _ => 1m
            };

            p += RAMsizeGB * RAMTypePrice * 10;
            p += HDDtype == "SSD" ? HDDsizeGB * 6m : HDDsizeGB * 1m;

            if (HasMonitor) p += 9_000m;
            if (HasKeyboard) p += 1_500m;
            if (HasMouse) p += 1_000m;

            p *= Type switch
            {
                "Рабочая станция" => 1.3m,
                "Сервер" => 1.6m,
                "Ноутбук" => 1.4m,
                "Моноблок" => 1.2m,
                "МИНИ-ПК" => 1.1m,
                _ => 1.0m
            };

            p /= 3;
            return Math.Round(p, 2);
        }

        public override string ToString() =>
            $"[{Type}] {CPU.Model} | {VideoCard.Model} | {RAMsizeGB}ГБ {RAMtype} | {HDDsizeGB}ГБ {HDDtype}";
    }
}