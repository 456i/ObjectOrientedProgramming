using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lab2
{
    public partial class MainForm : Form
    {
        // ─── Список заказов ───────────────────────────────────────
        private readonly List<Computer> _orders = new();

        // ─── Цены видеокарт по модели (ГБ памяти, базовая цена) ──
        private static readonly Dictionary<string, (int memGB, decimal basePrice)> GpuSpec = new()
        {
            { "RTX 4050", (8,  35_000) }, { "RTX 4070", (12, 60_000) },
            { "RTX 4080", (16, 90_000) }, { "RTX 4090", (24,130_000) },
            { "RTX 5050", (8,  45_000) }, { "RTX 5070", (12, 75_000) },
            { "RTX 5080", (16,110_000) }, { "RTX 5090", (24,160_000) },
            { "RX 9060",  (8,  28_000) }, { "RX 9070",  (16, 55_000) },
        };

        // ─── Конструктор ──────────────────────────────────────────
        public MainForm()
        {
            InitializeComponent();

            // Колонки ListView — добавляем здесь, т.к. Designer их не сохраняет
            lvOrders.Columns.Add("№", 42);
            lvOrders.Columns.Add("Тип", 115);
            lvOrders.Columns.Add("CPU", 175);
            lvOrders.Columns.Add("GPU", 135);
            lvOrders.Columns.Add("ОЗУ", 85);
            lvOrders.Columns.Add("Диск", 65);
            lvOrders.Columns.Add("Дата", 92);
            lvOrders.Columns.Add("Цена ₽", 115);

            RecalcCost();
        }

        // ═══════════════════════════════════════════════════════════
        //  СБОРКА ОБЪЕКТА Computer ИЗ ПОЛЕЙ ФОРМЫ
        // ═══════════════════════════════════════════════════════════
        private Computer CollectComputer(bool validate = true)
        {
            errorProvider.Clear();
            bool valid = true;



            // Проверяем ComboBox — должен быть выбран пункт
            void Check(ComboBox cb, string name)
            {
                if (cb.SelectedIndex < 0)
                {
                    errorProvider.SetError(cb, $"Выберите «{name}»");
                    valid = false;
                }
                else
                {
                    errorProvider.SetError(cb, "");
                }
            }

            if (validate)
            {
                Check(cbComputerType, "Тип компьютера");
                Check(cbCPUMaker, "Производитель CPU");
                Check(cbCPUCore, "Количество ядер");
                Check(cbModelCPU, "Поколение CPU");
                Check(cbRamSize, "Объём ОЗУ");
                Check(cbRamType, "Тип ОЗУ");
                Check(cbGPU, "Видеокарта");
            }

            if (validate && !valid)
                throw new InvalidOperationException(
                    "Заполните все поля, отмеченные значком ⚠");

            // ── Процессор ─────────────────────────────────────────
            string maker = cbCPUMaker.Text;
            int cores = int.Parse(cbCPUCore.Text);
            int gen = int.Parse(cbModelCPU.Text);
            // Строим читаемую модель: "Intel Core i7" или "AMD Ryzen 7"
            string cpuModel = maker == "Intel"
                ? $"Core i{gen} ({cores}C)"
                : $"Ryzen {gen} ({cores}C)";

            float cpuModelPrice = (cpuModel == "Intel") ? 1.7f : 2.3f;
            var cpu = new Processor
            {
                Manufacturer = maker,
                Model = cpuModel,
                Cores = cores,
                MaxFrequency = 3.5 + cores * 0.5 * gen * 0.3 * cpuModelPrice, // условная зависимость от ядер
                ArchBits = 64,
                CacheL3MB = cores * 2// условный кэш
            };

            // ── Видеокарта ────────────────────────────────────────
            string gpuKey = cbGPU.Text;
            var spec = GpuSpec[gpuKey];

            var gpu = new GPU
            {
                Manufacturer = gpuKey.StartsWith("RX") ? "AMD" : "NVIDIA",
                Model = gpuKey,
                MemoryGB = spec.memGB,
                Frequency = 1800
            };

            // ── ОЗУ и диск ───────────────────────────────────────
            int ramGB = cbRamSize.SelectedIndex >= 0 ? int.Parse(cbRamSize.Text) : 16;

            return new Computer
            {
                Type = cbComputerType.Text,
                CPU = cpu,
                VideoCard = gpu,
                RAMsizeGB = int.Parse(cbRamSize.Text),
                RAMtype = cbRamType.Text,
                HDDsizeGB = 1000,
                HDDtype = rbSSD.Checked ? "SSD" : "HDD",
                PurchaseDate = calPurchase.SelectionStart,
                HasMonitor = chkMonitor.Checked,
                HasKeyboard = chkKeyboard.Checked,
                HasMouse = chkMouse.Checked
            };
        }

        // ═══════════════════════════════════════════════════════════
        //  ПЕРЕСЧЁТ СТОИМОСТИ В РЕАЛЬНОМ ВРЕМЕНИ
        // ═══════════════════════════════════════════════════════════
        private void RecalcCost()
        {
            try
            {
                var c = CollectComputer(validate: false);
                decimal p = PriceCalculator.GetComputerPrice(c);

                lblCostValue.Text = $"{p:N0} ₽";
                pbCost.Value = (int)Math.Min(p, pbCost.Maximum);
            }
            catch { /* поля ещё не заполнены — молча пропускаем */ }
        }

        // ═══════════════════════════════════════════════════════════
        //  ОБРАБОТЧИКИ СОБЫТИЙ — требуются Designer'ом
        // ═══════════════════════════════════════════════════════════

        // Один общий — для контролов с AnyControl_Changed
        private void AnyControl_Changed(object sender, EventArgs e) => RecalcCost();

        // Отдельные имена, которые Designer назначил своим ComboBox'ам
        private void cbCoreMaker_SelectedIndexChanged(object sender, EventArgs e) => RecalcCost();
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) => RecalcCost();
        private void cbRamSize_SelectedIndexChanged(object sender, EventArgs e) => RecalcCost();
        private void cbRamType_SelectedIndexChanged(object sender, EventArgs e) => RecalcCost();
        private void cbGPU_SelectedIndexChanged(object sender, EventArgs e) => RecalcCost();

        // Пустые заглушки — Designer их прописал, они должны существовать
        private void gbMain_Enter(object sender, EventArgs e) { }
        private void lblComputerType_Click(object sender, EventArgs e) { }
        private void lblCPUMaker_Click(object sender, EventArgs e) { }
        private void lblCPUCores_Click(object sender, EventArgs e) { }
        private void lblGPUMaker_Click(object sender, EventArgs e) { }

        // ═══════════════════════════════════════════════════════════
        //  КНОПКА: Добавить заказ
        // ═══════════════════════════════════════════════════════════
        private void BtnAddOrder_Click(object sender, EventArgs e)
        {
            try
            {
                var c = CollectComputer(validate: true);
                _orders.Add(c);
                RefreshOrdersList();
                TabCtrl.SelectedIndex = 1; // переключить на «Мои заказы»
                MessageBox.Show(
                    $"Заказ #{_orders.Count} добавлен!\n" +
                    $"Стоимость: {c.Price():N0} ₽\n" +
                    $"Итого по лаборатории: {PriceCalculator.GetLaboratoryTotal(_orders):N0} ₽",
                    "Заказ добавлен",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ═══════════════════════════════════════════════════════════
        //  ОБНОВЛЕНИЕ ТАБЛИЦЫ ЗАКАЗОВ
        // ═══════════════════════════════════════════════════════════
        private void RefreshOrdersList()
        {
            lvOrders.Items.Clear();

            for (int i = 0; i < _orders.Count; i++)
            {
                var c = _orders[i];
                decimal price = PriceCalculator.GetComputerPrice(c);

                var item = new ListViewItem((i + 1).ToString());
                item.SubItems.Add(c.Type);
                item.SubItems.Add($"{c.CPU.Manufacturer} {c.CPU.Model}");
                item.SubItems.Add(c.VideoCard.Model);
                item.SubItems.Add($"{c.RAMsizeGB} ГБ {c.RAMtype}");
                item.SubItems.Add(c.HDDtype);
                item.SubItems.Add(c.PurchaseDate.ToString("dd.MM.yy"));
                item.SubItems.Add($"{price:N0} ₽");

                item.BackColor = i % 2 == 0 ? Color.White : Color.AliceBlue;
                lvOrders.Items.Add(item);
            }

            // Обновляем итог по лаборатории в заголовке кнопки Удалить
            decimal total = PriceCalculator.GetLaboratoryTotal(_orders);
            lblOrders.Text = _orders.Count == 0
                ? "Список заказов:"
                : $"Список заказов: {_orders.Count} шт.  |  Итого: {total:N0} ₽";
        }

        private void LvOrders_SelectedIndexChanged(object sender, EventArgs e) { }

        // ═══════════════════════════════════════════════════════════
        //  КНОПКА: Удалить заказ
        // ═══════════════════════════════════════════════════════════
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lvOrders.SelectedIndices.Count == 0)
            {
                MessageBox.Show("Выберите строку заказа для удаления.",
                    "Ничего не выбрано", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int idx = lvOrders.SelectedIndices[0];
            var confirm = MessageBox.Show(
                $"Удалить заказ #{idx + 1}?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                _orders.RemoveAt(idx);
                RefreshOrdersList();
            }
        }

        // ═══════════════════════════════════════════════════════════
        //  КНОПКА: Сохранить в файл
        // ═══════════════════════════════════════════════════════════
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (_orders.Count == 0)
            {
                MessageBox.Show("Нет заказов для сохранения.",
                    "Пусто", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var dlg = new SaveFileDialog
            {
                Title = "Сохранить заказы",
                Filter = "JSON файл (*.json)|*.json",
                FileName = "lab_orders"
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                FileManager.Save(_orders, dlg.FileName);
                MessageBox.Show($"Сохранено {_orders.Count} заказов.",
                    "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения:\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═══════════════════════════════════════════════════════════
        //  КНОПКА: Загрузить из файла
        // ═══════════════════════════════════════════════════════════
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog
            {
                Title = "Загрузить заказы",
                Filter = "JSON файл (*.json)|*.json"
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                var loaded = FileManager.Load(dlg.FileName);

                if (loaded.Count == 0)
                {
                    MessageBox.Show("Файл пуст или не содержит заказов.",
                        "Нет данных", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Спросить: добавить или заменить
                if (_orders.Count > 0)
                {
                    var answer = MessageBox.Show(
                        $"Добавить {loaded.Count} загруженных заказов к существующим {_orders.Count}?\n" +
                        "«Нет» — заменить текущий список.",
                        "Способ загрузки",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (answer == DialogResult.Cancel) return;
                    if (answer == DialogResult.No) _orders.Clear();
                }

                _orders.AddRange(loaded);
                RefreshOrdersList();
                TabCtrl.SelectedIndex = 1;
                MessageBox.Show($"Загружено заказов: {loaded.Count}",
                    "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки:\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gbGPU_Enter(object sender, EventArgs e)
        {

        }
    }
}