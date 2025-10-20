using System.Windows;
using WpfApp3.Data;
using WpfApp3.Models;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        private readonly ApplicationDbContext _context;

        public Window4()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            LoadWorkshops();
        }

        private void LoadWorkshops()
        {
            WorkshopList.ItemsSource = _context.Workshops.ToList();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();
        }

        private void AddWorkShops_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new WorkshopDialog();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var newWorkshop = new Workshops
                    {
                        Name = dialog.WorkshopName,
                        StuffCount = dialog.StuffCount,
                        WorkshopType = dialog.SelectedType
                    };
                    _context.Workshops.Add(newWorkshop);
                    _context.SaveChanges();
                    LoadWorkshops();
                    MessageBox.Show("Цех успешно добавлен!", "Успех");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении: {ex.Message}");
                }
            }
        }

        private void EditWorkshops_Click(object sender, RoutedEventArgs e)
        {
            if (WorkshopList.SelectedItem is not Workshops selectedWorkshop)
            {
                MessageBox.Show("Выберите цех для редактирования.");
                return;
            }

            var dialog = new WorkshopDialog(selectedWorkshop);
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    selectedWorkshop.Name = dialog.WorkshopName;
                    selectedWorkshop.StuffCount = dialog.StuffCount;
                    selectedWorkshop.WorkshopType = dialog.SelectedType;

                    _context.Workshops.Update(selectedWorkshop);
                    _context.SaveChanges();
                    LoadWorkshops();
                    MessageBox.Show("Цех успешно обновлен!", "Успех");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при редактировании: {ex.Message}");
                }
            }
        }

        private void DeleteWorkshops_Click(object sender, RoutedEventArgs e)
        {
            if (WorkshopList.SelectedItem is not Workshops selectedWorkshop)
            {
                MessageBox.Show("Пожалуйста, выберите цех для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                _context.Workshops.Remove(selectedWorkshop);
                _context.SaveChanges();
                LoadWorkshops(); // обновить список
                MessageBox.Show("Цех успешно удален!", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}");
            }
        }
    }
}
