using System;
using System.Linq;
using System.Windows;
using WpfApp3.Data;
using WpfApp3.Models;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private readonly ApplicationDbContext _context;

        public Window2()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            LoadMaterialTypes();
        }

        private void LoadMaterialTypes()
        {
            MaterialList.ItemsSource = _context.MaterialTypes.ToList();
        }

        private void AddMaterialTypes_Click(object sender, RoutedEventArgs e)
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Введите название материала:", "Добавить тип материала", "");
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Название материала не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string percentStr = Microsoft.VisualBasic.Interaction.InputBox("Введите процент потерь:", "Новый материал", "0");
            if (!decimal.TryParse(percentStr, out decimal losePercent))
            {
                MessageBox.Show("Введите корректное число.", "Ошибка");
                return;
            }

            try
            {
                var newMaterial = new MaterialTypes
                {
                    Name = name,
                    LosePercent = losePercent
                };

                _context.MaterialTypes.Add(newMaterial);
                _context.SaveChanges();

                LoadMaterialTypes(); // обновить список
                MessageBox.Show("Материал успешно добавлен!", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}");
            }
        }

        private void EditMaterialTypes_Click(object sender, RoutedEventArgs e)
        {
            if (MaterialList.SelectedItem is not MaterialTypes selected)
            {
                MessageBox.Show("Выберите материал для редактирования.", "Внимание");
                return;
            }

            string newName = Microsoft.VisualBasic.Interaction.InputBox("Введите новое название:", "Редактирование", selected.Name);
            if (string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Название не может быть пустым.", "Ошибка");
                return;
            }

            string newPercentStr = Microsoft.VisualBasic.Interaction.InputBox("Введите новый процент потерь:", "Редактирование", selected.LosePercent.ToString());
            if (!decimal.TryParse(newPercentStr, out decimal newLosePercent))
            {
                MessageBox.Show("Введите корректное число.", "Ошибка");
                return;
            }

            try
            {
                selected.Name = newName;
                selected.LosePercent = newLosePercent;

                _context.MaterialTypes.Update(selected);
                _context.SaveChanges();

                LoadMaterialTypes();
                MessageBox.Show("Материал успешно обновлён!", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании: {ex.Message}");
            }
        }

        private void DeleteMaterialTypes_Click(object sender, RoutedEventArgs e)
        {
            if (MaterialList.SelectedItem is not MaterialTypes selected)
            {
                MessageBox.Show("Выберите материал для удаления.", "Внимание");
                return;
            }

            var confirm = MessageBox.Show($"Удалить материал «{selected.Name}»?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm != MessageBoxResult.Yes)
                return;

            try
            {
                _context.MaterialTypes.Remove(selected);
                _context.SaveChanges();

                LoadMaterialTypes();
                MessageBox.Show("Материал удалён.", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();
        }
    }
}
