using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)   // Обработчик события кнопки "Выход"
        {
            MessageBoxResult result = MessageBox.Show(
                "Вы точно хотите закрыть приложение?",
                "Подтвердите выход",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)         // Обработчик события изменения текста в TextBox
        {

        }

        private void Enter_Click(object sender, RoutedEventArgs e)                      // Обработчик события кнопки "Вход"
        {
            if (UserEnterTheName.Text.Length < 1)                                       // Проверка на ввод имени пользователя
            {
                MessageBox.Show("Введите имя пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else                                                                        // Если имя введено, открываем новое окно и закрываем текущее
            {
                Window1 window1 = new Window1();    // Создание нового окна
                window1.Show();                     // Открытие нового окна
                this.Close();                       // Закрытие текущего окна
            }
                            
        }
    }
}