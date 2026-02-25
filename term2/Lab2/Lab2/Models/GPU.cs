namespace Lab2
{
    public class GPU
    {
        public string Manufacturer { get; set; } = "";
        public string Series { get; set; } = "";
        public string Model { get; set; } = "";
        public double Frequency { get; set; }
        public int MemoryGB { get; set; }

        /// <summary>
        /// Стоимость GPU:
        /// База 5000 + память×1500 + частота×200
        /// </summary>
        public decimal Price()
        {
            decimal p = 5_000m;
            p += MemoryGB * 1_500m;
            p += (decimal)(Frequency * 200);
            return p;
        }

        public override string ToString() =>
            $"{Manufacturer} {Series} {Model} ({MemoryGB}ГБ / {Frequency}МГц)";
    }
}