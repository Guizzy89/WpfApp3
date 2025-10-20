using System;
using System.Windows;
using WpfApp3.Models.Enums;
using WpfApp3.Models;

namespace WpfApp3
{
    public partial class WorkshopDialog : Window
    {
        public string WorkshopName { get; private set; }
        public int StuffCount { get; private set; }
        public WorkshopTypes SelectedType { get; private set; }

        public WorkshopDialog(Workshops workshop = null)
        {
            InitializeComponent();
            TypeBox.ItemsSource = Enum.GetValues(typeof(WorkshopTypes));

            if (workshop != null)
            {
                Title = "Редактировать цех";
                OkButton.Content = "Сохранить";

                NameBox.Text = workshop.Name;
                StuffBox.Text = workshop.StuffCount.ToString();
                TypeBox.SelectedItem = workshop.WorkshopType;
            }
            else
            {
                Title = "Добавить цех";
                OkButton.Content = "Добавить";
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Название не может быть пустым.");
                return;
            }

            if (!int.TryParse(StuffBox.Text, out int stuff) || stuff < 0)
            {
                MessageBox.Show("Введите корректное количество сотрудников.");
                return;
            }

            if (TypeBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип цеха.");
                return;
            }

            WorkshopName = NameBox.Text;
            StuffCount = stuff;
            SelectedType = (WorkshopTypes)TypeBox.SelectedItem;

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
