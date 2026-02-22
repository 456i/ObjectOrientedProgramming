namespace Lab2
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gbMain = new GroupBox();
            gbCPU = new GroupBox();
            gbGPU = new GroupBox();
            comboBox1 = new ComboBox();
            label1 = new Label();
            gbMain.SuspendLayout();
            SuspendLayout();
            // 
            // gbMain
            // 
            gbMain.Controls.Add(label1);
            gbMain.Controls.Add(comboBox1);
            gbMain.Location = new Point(95, 76);
            gbMain.Name = "gbMain";
            gbMain.Size = new Size(1424, 308);
            gbMain.TabIndex = 0;
            gbMain.TabStop = false;
            gbMain.Text = "Основные";
            gbMain.Enter += groupBox1_Enter;
            // 
            // gbCPU
            // 
            gbCPU.Location = new Point(95, 444);
            gbCPU.Name = "gbCPU";
            gbCPU.Size = new Size(1424, 356);
            gbCPU.TabIndex = 1;
            gbCPU.TabStop = false;
            gbCPU.Text = "Процессор";
            gbCPU.Enter += gbCPU_Enter;
            // 
            // gbGPU
            // 
            gbGPU.Location = new Point(95, 872);
            gbGPU.Name = "gbGPU";
            gbGPU.Size = new Size(610, 336);
            gbGPU.TabIndex = 2;
            gbGPU.TabStop = false;
            gbGPU.Text = "Видео-карта";
            gbGPU.Enter += gbGPU_Enter;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Server", "Laptop", "WorkStation" });
            comboBox1.Location = new Point(438, 132);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(242, 40);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 54);
            label1.Name = "label1";
            label1.Size = new Size(78, 32);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1918, 1562);
            Controls.Add(gbGPU);
            Controls.Add(gbCPU);
            Controls.Add(gbMain);
            Name = "MainForm";
            Text = "Form1";
            gbMain.ResumeLayout(false);
            gbMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbMain;
        private GroupBox gbCPU;
        private ComboBox comboBox1;
        private GroupBox gbGPU;
        private Label label1;
    }
}
