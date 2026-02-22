using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lab2.Services
{
    public static class PriceCalculator
    {
        public static double Calculate(Computer computer)
        {
            double cpuPrice =
                computer.Processor.Cores * 800 +
                computer.Processor.BaseFrequency * 1000;

            double ramPrice = computer.RAMSize * 250;
            double diskPrice = computer.DiskSize * 5;

            double gpuPrice = 0;
            if (computer.VideoCard != null)
                gpuPrice = computer.VideoCard.MemorySize * 300;

            double total = cpuPrice + ramPrice + diskPrice + gpuPrice;

            switch (computer.Type)
            {
                case "Server":
                    total *= 1.5;
                    break;
                case "Laptop":
                    total *= 1.2;
                    break;
                case "Workstation":
                    total *= 1.3;
                    break;
            }
            return total;
        }

        public static double CalculateLabTotal(List<Computer> computers) => computers.Sum(c => c.Price);
    }
}
