using System;
using System.Linq;
using System.Windows;
using WpfApp3.Data;
using WpfApp3.Models;

namespace WpfApp3
{
    public partial class ProductDialog : Window
    {
        private readonly ApplicationDbContext _context;

        // Публичные свойства, которыми будет оперировать Window5
        public int Article { get; set; }
        public string ProductName { get; set; }
        public int ProductTypeId { get; set; }
        public int MaterialTypeId { get; set; }
        public decimal MinimalCost { get; set; }

        // Пустой конструктор — режим "Добавить"
        public ProductDialog()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            LoadLookups();
        }

        // Конструктор для редактирования (можно передать выбранный продукт)
        public ProductDialog(Products product) : this()
        {
            if (product == null) return;

            // заполняем поля
            ArticleBox.Text = product.Article.ToString();
            ArticleBox.IsEnabled = false; // артикул первичный ключ — менять нельзя
            NameBox.Text = product.Name;
            CostBox.Text = product.MinimalCost.ToString();

            // устанавливаем выбранные значения ComboBox'ов по Id
            ProductTypeBox.SelectedValue = product.ProductTypeId;
            MaterialTypeBox.SelectedValue = product.MaterialTypeId;
        }

        private void LoadLookups()
        {
            // Подгружаем справочники — ProductTypes и MaterialTypes
            ProductTypeBox.ItemsSource = _context.ProductTypes.ToList();
            MaterialTypeBox.ItemsSource = _context.MaterialTypes.ToList();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (!int.TryParse(ArticleBox.Text, out int articleVal))
            {
                MessageBox.Show("Артикул должен быть целым числом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите название продукта.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ProductTypeBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите тип продукта.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MaterialTypeBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите тип материала.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(CostBox.Text, out decimal cost) || cost < 0)
            {
                MessageBox.Show("Введите корректную минимальную стоимость.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // сохраняем в публичные свойства
            Article = articleVal;
            ProductName = NameBox.Text.Trim();
            ProductTypeId = (int)ProductTypeBox.SelectedValue;
            MaterialTypeId = (int)MaterialTypeBox.SelectedValue;
            MinimalCost = cost;

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        protected override void OnClosed(EventArgs e)
        {
            _context.Dispose();
            base.OnClosed(e);
        }
    }
}
