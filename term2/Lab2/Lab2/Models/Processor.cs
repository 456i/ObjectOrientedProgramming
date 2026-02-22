using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2.Models
{
    public class Processor
    {
        public string Manufacturer { get; set; }
        public string Series { get; set; }
        public string Model { get; set; }
        public int Cores { get; set; }
        public double BaseFrequency { get; set; }
        public double MaxFrequency { get; set; }
        public int ArchitectureBitness { get; set; }
        public int CacheL1 { get; set; }
        public int CacheL2 { get; set; }
        public int CacheL3 { get; set; }
    }
}
