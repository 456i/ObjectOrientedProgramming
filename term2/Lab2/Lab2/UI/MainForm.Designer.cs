namespace Lab2
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            gbMain = new GroupBox();
            cbModelCPU = new ComboBox();
            cbCPUCore = new ComboBox();
            lblHardDrive = new Label();
            lblOrderMaker = new Label();
            lblComputerType = new Label();
            cbComputerType = new ComboBox();
            rbSSD = new RadioButton();
            lblCPUMaker = new Label();
            rbHDD = new RadioButton();
            cbCPUMaker = new ComboBox();
            lblCPUModel = new Label();
            lblCPUCores = new Label();
            calPurchase = new MonthCalendar();
            gbGPU = new GroupBox();
            cbGPU = new ComboBox();
            lblGPUMaker = new Label();
            chkMonitor = new CheckBox();
            chkKeyboard = new CheckBox();
            chkMouse = new CheckBox();
            gbRAM = new GroupBox();
            cbRamType = new ComboBox();
            cbRamSize = new ComboBox();
            lblRAMSize = new Label();
            lblRAMType = new Label();
            gbCost = new GroupBox();
            pbCost = new ProgressBar();
            lblCostValue = new Label();
            TabCtrl = new TabControl();
            CatalogTab = new TabPage();
            btnLoad = new Button();
            btnAddOrder = new Button();
            btnSave = new Button();
            OrderTab = new TabPage();
            lblOrders = new Label();
            lvOrders = new ListView();
            btnDelete = new Button();
            errorProvider = new ErrorProvider(components);
            gbMain.SuspendLayout();
            gbGPU.SuspendLayout();
            gbRAM.SuspendLayout();
            gbCost.SuspendLayout();
            TabCtrl.SuspendLayout();
            CatalogTab.SuspendLayout();
            OrderTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // gbMain
            // 
            gbMain.Controls.Add(cbModelCPU);
            gbMain.Controls.Add(cbCPUCore);
            gbMain.Controls.Add(lblHardDrive);
            gbMain.Controls.Add(lblOrderMaker);
            gbMain.Controls.Add(lblComputerType);
            gbMain.Controls.Add(cbComputerType);
            gbMain.Controls.Add(rbSSD);
            gbMain.Controls.Add(lblCPUMaker);
            gbMain.Controls.Add(rbHDD);
            gbMain.Controls.Add(cbCPUMaker);
            gbMain.Controls.Add(lblCPUModel);
            gbMain.Controls.Add(lblCPUCores);
            gbMain.Controls.Add(calPurchase);
            gbMain.Font = new Font("Segoe UI Semibold", 14F);
            gbMain.Location = new Point(5, 4);
            gbMain.Name = "gbMain";
            gbMain.Size = new Size(885, 248);
            gbMain.TabIndex = 0;
            gbMain.TabStop = false;
            gbMain.Text = "Основные компоненты";
            gbMain.Enter += gbMain_Enter;
            // 
            // cbModelCPU
            // 
            cbModelCPU.Cursor = Cursors.Hand;
            cbModelCPU.DropDownStyle = ComboBoxStyle.DropDownList;
            cbModelCPU.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbModelCPU.Items.AddRange(new object[] { "4", "5", "6", "7", "8", "9", "10" });
            cbModelCPU.Location = new Point(272, 163);
            cbModelCPU.Name = "cbModelCPU";
            cbModelCPU.Size = new Size(206, 29);
            cbModelCPU.TabIndex = 7;
            cbModelCPU.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // cbCPUCore
            // 
            cbCPUCore.Cursor = Cursors.Hand;
            cbCPUCore.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCPUCore.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbCPUCore.Items.AddRange(new object[] { "6", "8", "10", "12", "14", "16" });
            cbCPUCore.Location = new Point(272, 119);
            cbCPUCore.Name = "cbCPUCore";
            cbCPUCore.Size = new Size(206, 29);
            cbCPUCore.TabIndex = 13;
            cbCPUCore.SelectedIndexChanged += cbCoreMaker_SelectedIndexChanged;
            // 
            // lblHardDrive
            // 
            lblHardDrive.AutoSize = true;
            lblHardDrive.Font = new Font("Segoe UI", 12F);
            lblHardDrive.Location = new Point(23, 207);
            lblHardDrive.Name = "lblHardDrive";
            lblHardDrive.Size = new Size(112, 21);
            lblHardDrive.TabIndex = 12;
            lblHardDrive.Text = "Жесткий диск:";
            // 
            // lblOrderMaker
            // 
            lblOrderMaker.AutoSize = true;
            lblOrderMaker.Font = new Font("Segoe UI", 12F);
            lblOrderMaker.Location = new Point(566, 43);
            lblOrderMaker.Name = "lblOrderMaker";
            lblOrderMaker.Size = new Size(192, 21);
            lblOrderMaker.TabIndex = 11;
            lblOrderMaker.Text = "Дата Оформления заказа";
            // 
            // lblComputerType
            // 
            lblComputerType.AutoSize = true;
            lblComputerType.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblComputerType.Location = new Point(23, 43);
            lblComputerType.Margin = new Padding(20);
            lblComputerType.Name = "lblComputerType";
            lblComputerType.Size = new Size(133, 21);
            lblComputerType.TabIndex = 0;
            lblComputerType.Text = "Тип компьютера:";
            lblComputerType.Click += lblComputerType_Click;
            // 
            // cbComputerType
            // 
            cbComputerType.Cursor = Cursors.Hand;
            cbComputerType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbComputerType.Font = new Font("Segoe UI", 12F);
            cbComputerType.Items.AddRange(new object[] { "Рабочая станция", "Сервер", "Ноутбук", "Моноблок", "Мини-ПК" });
            cbComputerType.Location = new Point(272, 40);
            cbComputerType.Margin = new Padding(20);
            cbComputerType.Name = "cbComputerType";
            cbComputerType.Size = new Size(206, 29);
            cbComputerType.TabIndex = 1;
            cbComputerType.SelectedIndexChanged += AnyControl_Changed;
            // 
            // rbSSD
            // 
            rbSSD.AutoSize = true;
            rbSSD.Checked = true;
            rbSSD.Font = new Font("Segoe UI", 12F);
            rbSSD.Location = new Point(365, 205);
            rbSSD.Name = "rbSSD";
            rbSSD.Size = new Size(57, 25);
            rbSSD.TabIndex = 8;
            rbSSD.TabStop = true;
            rbSSD.Text = "SSD";
            rbSSD.CheckedChanged += AnyControl_Changed;
            // 
            // lblCPUMaker
            // 
            lblCPUMaker.AutoSize = true;
            lblCPUMaker.Font = new Font("Segoe UI", 12F);
            lblCPUMaker.Location = new Point(23, 84);
            lblCPUMaker.Margin = new Padding(20);
            lblCPUMaker.Name = "lblCPUMaker";
            lblCPUMaker.Size = new Size(158, 21);
            lblCPUMaker.TabIndex = 2;
            lblCPUMaker.Text = "Производитель CPU:";
            lblCPUMaker.Click += lblCPUMaker_Click;
            // 
            // rbHDD
            // 
            rbHDD.AutoSize = true;
            rbHDD.Font = new Font("Segoe UI", 12F);
            rbHDD.Location = new Point(272, 205);
            rbHDD.Name = "rbHDD";
            rbHDD.Size = new Size(61, 25);
            rbHDD.TabIndex = 9;
            rbHDD.Text = "HDD";
            rbHDD.CheckedChanged += AnyControl_Changed;
            // 
            // cbCPUMaker
            // 
            cbCPUMaker.Cursor = Cursors.Hand;
            cbCPUMaker.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCPUMaker.Font = new Font("Segoe UI", 12F);
            cbCPUMaker.Items.AddRange(new object[] { "Intel", "AMD" });
            cbCPUMaker.Location = new Point(272, 81);
            cbCPUMaker.Margin = new Padding(20);
            cbCPUMaker.Name = "cbCPUMaker";
            cbCPUMaker.Size = new Size(206, 29);
            cbCPUMaker.TabIndex = 3;
            cbCPUMaker.SelectedIndexChanged += AnyControl_Changed;
            // 
            // lblCPUModel
            // 
            lblCPUModel.AutoSize = true;
            lblCPUModel.Font = new Font("Segoe UI", 12F);
            lblCPUModel.Location = new Point(23, 166);
            lblCPUModel.Margin = new Padding(20);
            lblCPUModel.Name = "lblCPUModel";
            lblCPUModel.Size = new Size(103, 21);
            lblCPUModel.TabIndex = 4;
            lblCPUModel.Text = "Модель CPU:";
            // 
            // lblCPUCores
            // 
            lblCPUCores.AutoSize = true;
            lblCPUCores.Font = new Font("Segoe UI", 12F);
            lblCPUCores.Location = new Point(23, 125);
            lblCPUCores.Name = "lblCPUCores";
            lblCPUCores.Size = new Size(82, 21);
            lblCPUCores.TabIndex = 6;
            lblCPUCores.Text = "Ядер CPU:";
            lblCPUCores.Click += lblCPUCores_Click;
            // 
            // calPurchase
            // 
            calPurchase.Location = new Point(566, 73);
            calPurchase.MaxSelectionCount = 1;
            calPurchase.Name = "calPurchase";
            calPurchase.TabIndex = 10;
            // 
            // gbGPU
            // 
            gbGPU.Controls.Add(cbGPU);
            gbGPU.Controls.Add(lblGPUMaker);
            gbGPU.Controls.Add(chkMonitor);
            gbGPU.Controls.Add(chkKeyboard);
            gbGPU.Controls.Add(chkMouse);
            gbGPU.Font = new Font("Segoe UI Semibold", 14F);
            gbGPU.Location = new Point(5, 396);
            gbGPU.Name = "gbGPU";
            gbGPU.Size = new Size(885, 213);
            gbGPU.TabIndex = 1;
            gbGPU.TabStop = false;
            gbGPU.Text = "Видеокарта и периферия";
            gbGPU.Enter += gbGPU_Enter;
            // 
            // cbGPU
            // 
            cbGPU.Cursor = Cursors.Hand;
            cbGPU.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGPU.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbGPU.Items.AddRange(new object[] { "RTX 4050", "RTX 4070", "RTX 4080", "RTX 4090", "RTX 5050", "RTX 5050", "RTX 5070", "RTX 5080", "RTX 5090", "RX 9070", "RX 9070", "RX 9060 ", "RX 9060 " });
            cbGPU.Location = new Point(272, 48);
            cbGPU.Name = "cbGPU";
            cbGPU.Size = new Size(206, 29);
            cbGPU.TabIndex = 5;
            cbGPU.SelectedIndexChanged += cbGPU_SelectedIndexChanged;
            // 
            // lblGPUMaker
            // 
            lblGPUMaker.AutoSize = true;
            lblGPUMaker.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblGPUMaker.Location = new Point(23, 48);
            lblGPUMaker.Margin = new Padding(20);
            lblGPUMaker.Name = "lblGPUMaker";
            lblGPUMaker.Size = new Size(44, 21);
            lblGPUMaker.TabIndex = 0;
            lblGPUMaker.Text = "GPU:";
            lblGPUMaker.Click += lblGPUMaker_Click;
            // 
            // chkMonitor
            // 
            chkMonitor.AutoSize = true;
            chkMonitor.Font = new Font("Segoe UI", 12F);
            chkMonitor.Location = new Point(23, 88);
            chkMonitor.Margin = new Padding(20);
            chkMonitor.Name = "chkMonitor";
            chkMonitor.Size = new Size(169, 25);
            chkMonitor.TabIndex = 5;
            chkMonitor.Text = "Монитор (+9 000₽)";
            chkMonitor.CheckedChanged += AnyControl_Changed;
            // 
            // chkKeyboard
            // 
            chkKeyboard.AutoSize = true;
            chkKeyboard.Font = new Font("Segoe UI", 12F);
            chkKeyboard.Location = new Point(23, 129);
            chkKeyboard.Margin = new Padding(20);
            chkKeyboard.Name = "chkKeyboard";
            chkKeyboard.Size = new Size(185, 25);
            chkKeyboard.TabIndex = 6;
            chkKeyboard.Text = "Клавиатура (+1 500₽)";
            chkKeyboard.CheckedChanged += AnyControl_Changed;
            // 
            // chkMouse
            // 
            chkMouse.AutoSize = true;
            chkMouse.Font = new Font("Segoe UI", 12F);
            chkMouse.Location = new Point(23, 169);
            chkMouse.Margin = new Padding(20);
            chkMouse.Name = "chkMouse";
            chkMouse.Size = new Size(149, 25);
            chkMouse.TabIndex = 7;
            chkMouse.Text = "Мышь (+1 000₽)";
            chkMouse.CheckedChanged += AnyControl_Changed;
            // 
            // gbRAM
            // 
            gbRAM.Controls.Add(cbRamType);
            gbRAM.Controls.Add(cbRamSize);
            gbRAM.Controls.Add(lblRAMSize);
            gbRAM.Controls.Add(lblRAMType);
            gbRAM.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbRAM.Location = new Point(5, 258);
            gbRAM.Name = "gbRAM";
            gbRAM.Size = new Size(885, 132);
            gbRAM.TabIndex = 2;
            gbRAM.TabStop = false;
            gbRAM.Text = "Оперативная память";
            // 
            // cbRamType
            // 
            cbRamType.Cursor = Cursors.Hand;
            cbRamType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRamType.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbRamType.Items.AddRange(new object[] { "DDR3", "DDR4", "DDR5" });
            cbRamType.Location = new Point(272, 71);
            cbRamType.Name = "cbRamType";
            cbRamType.Size = new Size(206, 29);
            cbRamType.TabIndex = 4;
            cbRamType.SelectedIndexChanged += cbRamType_SelectedIndexChanged;
            // 
            // cbRamSize
            // 
            cbRamSize.Cursor = Cursors.Hand;
            cbRamSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRamSize.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cbRamSize.Items.AddRange(new object[] { "8", "16", "32", "64" });
            cbRamSize.Location = new Point(272, 36);
            cbRamSize.Name = "cbRamSize";
            cbRamSize.Size = new Size(206, 29);
            cbRamSize.TabIndex = 3;
            cbRamSize.SelectedIndexChanged += cbRamSize_SelectedIndexChanged;
            // 
            // lblRAMSize
            // 
            lblRAMSize.AutoSize = true;
            lblRAMSize.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRAMSize.Location = new Point(23, 39);
            lblRAMSize.Margin = new Padding(20);
            lblRAMSize.Name = "lblRAMSize";
            lblRAMSize.Size = new Size(93, 21);
            lblRAMSize.TabIndex = 0;
            lblRAMSize.Text = "Объём (ГБ):";
            // 
            // lblRAMType
            // 
            lblRAMType.AutoSize = true;
            lblRAMType.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRAMType.Location = new Point(23, 74);
            lblRAMType.Margin = new Padding(20);
            lblRAMType.Name = "lblRAMType";
            lblRAMType.Size = new Size(39, 21);
            lblRAMType.TabIndex = 2;
            lblRAMType.Text = "Тип:";
            // 
            // gbCost
            // 
            gbCost.Controls.Add(pbCost);
            gbCost.Controls.Add(lblCostValue);
            gbCost.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbCost.Location = new Point(5, 615);
            gbCost.Name = "gbCost";
            gbCost.Size = new Size(885, 89);
            gbCost.TabIndex = 3;
            gbCost.TabStop = false;
            gbCost.Text = "Расчёт стоимости";
            // 
            // pbCost
            // 
            pbCost.Location = new Point(10, 28);
            pbCost.Maximum = 300000;
            pbCost.Name = "pbCost";
            pbCost.Size = new Size(468, 22);
            pbCost.TabIndex = 0;
            // 
            // lblCostValue
            // 
            lblCostValue.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCostValue.ForeColor = Color.DarkGreen;
            lblCostValue.Location = new Point(492, 28);
            lblCostValue.Name = "lblCostValue";
            lblCostValue.Size = new Size(200, 22);
            lblCostValue.TabIndex = 1;
            lblCostValue.Text = "0 ₽";
            // 
            // TabCtrl
            // 
            TabCtrl.Controls.Add(CatalogTab);
            TabCtrl.Controls.Add(OrderTab);
            TabCtrl.Font = new Font("Segoe UI Semibold", 14F);
            TabCtrl.Location = new Point(12, 12);
            TabCtrl.Name = "TabCtrl";
            TabCtrl.SelectedIndex = 0;
            TabCtrl.Size = new Size(907, 799);
            TabCtrl.TabIndex = 0;
            // 
            // CatalogTab
            // 
            CatalogTab.AutoScroll = true;
            CatalogTab.Controls.Add(gbMain);
            CatalogTab.Controls.Add(gbCost);
            CatalogTab.Controls.Add(btnLoad);
            CatalogTab.Controls.Add(btnAddOrder);
            CatalogTab.Controls.Add(gbGPU);
            CatalogTab.Controls.Add(gbRAM);
            CatalogTab.Controls.Add(btnSave);
            CatalogTab.Location = new Point(4, 34);
            CatalogTab.Name = "CatalogTab";
            CatalogTab.Size = new Size(899, 761);
            CatalogTab.TabIndex = 0;
            CatalogTab.Text = "📋 Каталог";
            // 
            // btnLoad
            // 
            btnLoad.BackColor = Color.FromArgb(150, 80, 200);
            btnLoad.FlatStyle = FlatStyle.Flat;
            btnLoad.Font = new Font("Segoe UI Semibold", 14F);
            btnLoad.ForeColor = Color.White;
            btnLoad.Location = new Point(750, 719);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(140, 36);
            btnLoad.TabIndex = 6;
            btnLoad.Text = "📂 Загрузить";
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += BtnLoad_Click;
            // 
            // btnAddOrder
            // 
            btnAddOrder.BackColor = Color.FromArgb(0, 120, 215);
            btnAddOrder.FlatStyle = FlatStyle.Flat;
            btnAddOrder.Font = new Font("Segoe UI Semibold", 14F);
            btnAddOrder.ForeColor = Color.White;
            btnAddOrder.Location = new Point(370, 719);
            btnAddOrder.Name = "btnAddOrder";
            btnAddOrder.Size = new Size(192, 36);
            btnAddOrder.TabIndex = 4;
            btnAddOrder.Text = "✅ Добавить заказ";
            btnAddOrder.UseVisualStyleBackColor = false;
            btnAddOrder.Click += BtnAddOrder_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(30, 160, 100);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI Semibold", 14F);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(15, 719);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(167, 36);
            btnSave.TabIndex = 5;
            btnSave.Text = "💾 Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += BtnSave_Click;
            // 
            // OrderTab
            // 
            OrderTab.Controls.Add(lblOrders);
            OrderTab.Controls.Add(lvOrders);
            OrderTab.Controls.Add(btnDelete);
            OrderTab.Location = new Point(4, 34);
            OrderTab.Name = "OrderTab";
            OrderTab.Size = new Size(899, 761);
            OrderTab.TabIndex = 1;
            OrderTab.Text = "📦 Мои заказы";
            // 
            // lblOrders
            // 
            lblOrders.AutoSize = true;
            lblOrders.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOrders.Location = new Point(5, 8);
            lblOrders.Name = "lblOrders";
            lblOrders.Size = new Size(123, 19);
            lblOrders.TabIndex = 0;
            lblOrders.Text = "Список заказов:";
            // 
            // lvOrders
            // 
            lvOrders.FullRowSelect = true;
            lvOrders.GridLines = true;
            lvOrders.Location = new Point(5, 30);
            lvOrders.Name = "lvOrders";
            lvOrders.Size = new Size(891, 456);
            lvOrders.TabIndex = 1;
            lvOrders.UseCompatibleStateImageBehavior = false;
            lvOrders.View = View.Details;
            lvOrders.SelectedIndexChanged += LvOrders_SelectedIndexChanged;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(200, 50, 50);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(712, 508);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(164, 47);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "🗑 Удалить";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += BtnDelete_Click;
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 245, 255);
            ClientSize = new Size(929, 823);
            Controls.Add(TabCtrl);
            Name = "MainForm";
            Text = "IT Лаборатория";
            gbMain.ResumeLayout(false);
            gbMain.PerformLayout();
            gbGPU.ResumeLayout(false);
            gbGPU.PerformLayout();
            gbRAM.ResumeLayout(false);
            gbRAM.PerformLayout();
            gbCost.ResumeLayout(false);
            TabCtrl.ResumeLayout(false);
            CatalogTab.ResumeLayout(false);
            OrderTab.ResumeLayout(false);
            OrderTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        // ═══════════════════════════════════════════════════
        //  Объявление полей (Designer создаёт их здесь)
        // ═══════════════════════════════════════════════════

        // 1. GroupBox
        private GroupBox gbMain;
        private GroupBox gbGPU;
        private GroupBox gbRAM;
        private GroupBox gbCost;

        // 2. TabControl
        private TabControl TabCtrl;
        private TabPage CatalogTab;
        private TabPage OrderTab;

        // 3. ComboBox
        private ComboBox cbComputerType;
        private ComboBox cbCPUMaker;

        // 4. Label
        private Label lblComputerType;
        private Label lblCPUMaker;
        private Label lblCPUModel;
        private Label lblCPUCores;
        private Label lblRAMSize;
        private Label lblRAMType;
        private Label lblGPUMaker;
        private Label lblCostValue;
        private Label lblOrders;

        // 5. ProgressBar
        private ProgressBar pbCost;

        // 6. ErrorProvider
        private ErrorProvider errorProvider;

        // 7. CheckBox
        private CheckBox chkMonitor;
        private CheckBox chkKeyboard;
        private CheckBox chkMouse;

        // 8. RadioButton
        private RadioButton rbSSD;
        private RadioButton rbHDD;

        // 9. Button
        private Button btnAddOrder;
        private Button btnSave;
        private Button btnLoad;
        private Button btnDelete;

        // Дополнительно
        private MonthCalendar calPurchase;
        private ListView lvOrders;
        private Label lblOrderMaker;
        private Label lblHardDrive;
        private ComboBox cbModelCPU;
        private ComboBox cbCPUCore;
        private ComboBox cbRamType;
        private ComboBox cbRamSize;
        private ComboBox cbGPU;
    }
}