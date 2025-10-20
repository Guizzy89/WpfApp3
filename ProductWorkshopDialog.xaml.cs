using System;
using System.Linq;
using System.Windows;
using WpfApp3.Data;
using WpfApp3.Models;

namespace WpfApp3
{
    public partial class ProductWorkshopDialog : Window
    {
        private readonly ApplicationDbContext _context;

        public string ProductWorkshopName { get; set; }
        public int WorkshopId { get; set; }
        public decimal ManufacturingInHours { get; set; }

        public ProductWorkshopDialog()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            LoadWorkshops();
        }

        public ProductWorkshopDialog(ProductWorkshops pw) : this()
        {
            if (pw != null)
            {
                NameBox.Text = pw.Name;
                WorkshopBox.SelectedValue = pw.WorkshopId;
                HoursBox.Text = pw.ManufacturingInHours.ToString();
            }
        }

        private void LoadWorkshops()
        {
            WorkshopBox.ItemsSource = _context.Workshops.ToList();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите название.");
                return;
            }

            if (WorkshopBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите цех.");
                return;
            }

            if (!decimal.TryParse(HoursBox.Text, out var hours))
            {
                MessageBox.Show("Введите корректное значение времени (в часах).");
                return;
            }

            ProductWorkshopName = NameBox.Text.Trim();
            WorkshopId = (int)WorkshopBox.SelectedValue;
            ManufacturingInHours = hours;

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
