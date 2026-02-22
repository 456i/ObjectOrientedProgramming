using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2.Models
{
    public class Computer
    {
        public string Type { get; set; }
        public string RAMType { get; set; }
        public int RAMSize { get; set; }
        public string DiskType { get; set; }
        public int DiskSize { get; set; }
        public DateTime PurchaseDate { get; set; }

        public Processor Processor { get; set; }
        public VideoCard VideoCard { get; set; }

        public double Price { get; set; }
    }
}
