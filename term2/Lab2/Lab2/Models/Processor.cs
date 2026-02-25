namespace Lab2
{
    public class Processor
    {
        public string Manufacturer { get; set; } = "";
        public string Series { get; set; } = "";
        public string Model { get; set; } = "";
        public int Cores { get; set; }
        public double Frequency { get; set; }
        public double MaxFrequency { get; set; }
        public int ArchBits { get; set; } = 64;
        public int CacheL1KB { get; set; }
        public int CacheL2KB { get; set; }
        public int CacheL3MB { get; set; }

        /// <summary>
        /// Стоимость CPU:
        /// База 4000 + ядра×2500 + макс.частота×1500 + L3×200 + 500 (за 64бит)
        /// </summary>
        public decimal Price()
        {
            decimal p = 4_000m;
            p += (decimal)(MaxFrequency * 1_500);
            p += CacheL3MB * 200m;
            p += ArchBits == 64 ? 500m : 0m;
            return p;
        }

        public override string ToString() =>
            $"{Manufacturer} {Series} {Model} ({Cores}C / {MaxFrequency:F1}GHz)";
    }
}