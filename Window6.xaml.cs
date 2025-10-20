using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;
using WpfApp3.Data;
using WpfApp3.Models;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Window6.xaml
    /// </summary>
    public partial class Window6 : Window
    {
        private readonly ApplicationDbContext _context;

        public Window6()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            LoadProductWorkshops();
        }

        public void LoadProductWorkshops()
        {
            ProductWorkshopsList.ItemsSource = _context.ProductWorkshops
                .Include(pw => pw.Workshop)
                .ToList();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();
        }

        private void AddProductWorkshops_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ProductWorkshopDialog();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var newProductWorkshop = new ProductWorkshops
                    {
                        Name = dialog.ProductWorkshopName,
                        WorkshopId = dialog.WorkshopId,
                        ManufacturingInHours = dialog.ManufacturingInHours
                    };
                    _context.ProductWorkshops.Add(newProductWorkshop);
                    _context.SaveChanges();
                    LoadProductWorkshops();
                    MessageBox.Show("Связь продукта и цеха успешно добавлена!", "Успех");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении: {ex.Message}");
                }
            }
        }

        private void EditProductWorkshops_Click(object sender, RoutedEventArgs e)
        {
            if (ProductWorkshopsList.SelectedItem is not ProductWorkshops selectedProductWorkshop)
            {
                MessageBox.Show("Выберите запись для редактирования.");
                return;
            }
            var dialog = new ProductWorkshopDialog(selectedProductWorkshop);
            if (dialog.ShowDialog() == true) {
                try
                {
                    selectedProductWorkshop.Name = dialog.ProductWorkshopName;
                    selectedProductWorkshop.WorkshopId = dialog.WorkshopId;
                    selectedProductWorkshop.ManufacturingInHours = dialog.ManufacturingInHours;
                    _context.SaveChanges();
                    LoadProductWorkshops();
                    MessageBox.Show("Связь продукта и цеха успешно обновлена!", "Успех");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при обновлении: {ex.Message}");
                }
            }
        }

        private void DeleteProductWorkshops_Click(object sender, RoutedEventArgs e)
        {
            if (ProductWorkshopsList.SelectedItem is not ProductWorkshops selectedPW)
            {
                MessageBox.Show("Выберите запись для удаления.");
                return;
            }

            try
            {
                _context.ProductWorkshops.Remove(selectedPW);
                _context.SaveChanges();
                LoadProductWorkshops();
                MessageBox.Show("Запись успешно удалена!", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _context.Dispose();
            base.OnClosed(e);
        }
    }    
}

