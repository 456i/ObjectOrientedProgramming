using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void calculateBTN_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox1.Text, out double length) ||
                !double.TryParse(textBox2.Text, out double width) ||
                !double.TryParse(textBox3.Text, out double height))
            {
                MessageBox.Show("Введите корректные числовые значения.");
                return;
            }

            if (length <= 0 || width <= 0 || height <= 0)
            {
                MessageBox.Show("Размеры должны быть больше нуля.");
                return;
            }

            double floorArea = length * width;
            double wallArea = 2 * (length + width) * height;

            double floorFeet = floorArea * 10.7639;
            double wallFeet = wallArea * 10.7639;

            double laminate = floorArea;
            double tile = floorArea + wallArea;
            double wallpaper = wallArea;
            // Материалы (ft²)
            double laminateFeet = laminate * 10.7639;
            double tileFeet = tile * 10.7639;
            double wallpaperFeet = wallpaper * 10.7639;

            MessageBox.Show(
                $"=== ПЛОЩАДИ ===\n\n" +
                $"Пол: {floorArea:F2} м² ({floorFeet:F2} ft²)\n" +
                $"Стены: {wallArea:F2} м² ({wallFeet:F2} ft²)\n\n" +

                $"=== РЕКОМЕНДАЦИИ ПО МАТЕРИАЛАМ ===\n\n" +

                $"Ламинат (только пол):\n" +
                $"{laminate:F2} м² ({laminateFeet:F2} ft²)\n\n" +

                $"Плитка (пол + стены):\n" +
                $"{tile:F2} м² ({tileFeet:F2} ft²)\n\n" +

                $"Обои (только стены):\n" +
                $"{wallpaper:F2} м² ({wallpaperFeet:F2} ft²)"
            );


        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
