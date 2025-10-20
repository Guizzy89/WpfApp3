using System.Windows;
using WpfApp3.Data;
using WpfApp3.Models;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        private readonly ApplicationDbContext _context;

        public Window3()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            LoadProductTypes();
        }
                

        private void LoadProductTypes()
        {
            ProductList.ItemsSource = _context.ProductTypes.ToList();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();
        }

        private void AddProductTypes_Click(object sender, RoutedEventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Введите название продукта:", "Введите коэффициент типа продукции", "");
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Название продукта не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string coeffStr = Microsoft.VisualBasic.Interaction.InputBox("Введите коэффициент продукта:", "Новый продукт", "0");
            if (!decimal.TryParse(coeffStr, out decimal coeff))
            {
                MessageBox.Show("Введите корректное число.", "Ошибка");
                return;
            }

            try
            {

                var newProduct = new ProductTypes
                {
                    Name = name,
                    Coefficient = coeff
                };

                _context.ProductTypes.Add(newProduct);
                _context.SaveChanges();

                LoadProductTypes();
                MessageBox.Show("Продукт успешно добавлен!", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}");
            }
        }

            private void EditProductTypes_Click(object sender, RoutedEventArgs e)
            {
                if (ProductList.SelectedItem is ProductTypes selectedProduct)
                {
                    string newName = Microsoft.VisualBasic.Interaction.InputBox("Введите новое название продукта:", "Редактировать продукт", selectedProduct.Name);
                    if (string.IsNullOrWhiteSpace(newName))
                    {
                        MessageBox.Show("Название продукта не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    string coeffStr = Microsoft.VisualBasic.Interaction.InputBox("Введите новый коэффициент продукта:", "Редактировать продукт", selectedProduct.Coefficient.ToString());
                    if (!decimal.TryParse(coeffStr, out decimal newCoeff))
                    {
                        MessageBox.Show("Введите корректное число.", "Ошибка");
                        return;
                    }
                    try
                    {
                        selectedProduct.Name = newName;
                        selectedProduct.Coefficient = newCoeff;
                        _context.ProductTypes.Update(selectedProduct);
                        _context.SaveChanges();
                        LoadProductTypes(); 
                        MessageBox.Show("Продукт успешно обновлен!", "Успех");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при обновлении: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите продукт для редактирования.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

            private void DeleteProductTypes_Click(object sender, RoutedEventArgs e)
            {
                if (ProductList.SelectedItem is ProductTypes selectedProduct)
                {
                    var result = MessageBox.Show($"Вы уверены, что хотите удалить продукт '{selectedProduct.Name}'?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            _context.ProductTypes.Remove(selectedProduct);
                            _context.SaveChanges();
                            LoadProductTypes(); 
                            MessageBox.Show("Продукт успешно удален!", "Успех");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при удалении: {ex.Message}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите продукт для удаления.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
        }
    }
}
