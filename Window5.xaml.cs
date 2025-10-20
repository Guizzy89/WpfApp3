using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Windows;
using WpfApp3.Data;
using WpfApp3.Models;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        private readonly ApplicationDbContext _context;

        public Window5()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            LoadProducts();
        }

        private void LoadProducts()
        {
            ProductsList.ItemsSource = _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.MaterialType)
                .ToList();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();
        }

        private void AddProducts_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ProductDialog();
            dialog.Owner = this;

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    // проверка уникальности артикля
                    if (_context.Products.Any(p => p.Article == dialog.Article))
                    {
                        MessageBox.Show("Продукт с таким артикулом уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var newProduct = new Products
                    {
                        Article = dialog.Article,
                        Name = dialog.ProductName,
                        ProductTypeId = dialog.ProductTypeId,
                        MaterialTypeId = dialog.MaterialTypeId,
                        MinimalCost = dialog.MinimalCost
                    };

                    _context.Products.Add(newProduct);
                    _context.SaveChanges();
                    LoadProducts();
                    MessageBox.Show("Продукт успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void EditProducts_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsList.SelectedItem is not Products selectedProduct)
            {
                MessageBox.Show("Выберите продукт для редактирования.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Передаём выбранный продукт в диалог — он заполнит поля
            var dialog = new ProductDialog(selectedProduct) { Owner = this };

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    // Артикул не меняем (он заблокирован в диалоге)
                    selectedProduct.Name = dialog.ProductName;
                    selectedProduct.ProductTypeId = dialog.ProductTypeId;
                    selectedProduct.MaterialTypeId = dialog.MaterialTypeId;
                    selectedProduct.MinimalCost = dialog.MinimalCost;

                    _context.SaveChanges();
                    LoadProducts();
                    MessageBox.Show("Продукт успешно обновлён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void DeleteProducts_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsList.SelectedItem is not Products selectedProduct)
            {
                MessageBox.Show("Выберите продукт для удаления.");
                return;
            }
            try 
            {
                _context.Products.Remove(selectedProduct);
                _context.SaveChanges();
                LoadProducts();
                MessageBox.Show("Продукт успешно удален!", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }
    }
}
