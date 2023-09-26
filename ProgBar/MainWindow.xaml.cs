using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProgBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int NumProgressBars = 5;
        private Random random = new Random();
        private bool isPaused = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartAnimation_Click(object sender, RoutedEventArgs e)
        {
            // Видалення існуючих прогрес-барів
            progressBarsPanel.Children.Clear();

            // Створення нових прогрес-барів та запуск анімації
            for (int i = 0; i < NumProgressBars; i++)
            {
                ProgressBar progressBar = new ProgressBar();
                progressBar.Width = 300;
                progressBar.Height = 20;
                progressBar.Maximum = 100;

                // Створення і запуск потоку для анімації прогрес-бара
                Thread thread = new Thread(() => AnimateProgressBar(progressBar));
                thread.Start();

                progressBarsPanel.Children.Add(progressBar);
            }
        }
        private void AnimateProgressBar(ProgressBar progressBar)
        {
            while (true)
            {
                // Згенерувати випадковий величину та колір для прогрес-бара
                double value = random.Next(0, 101);
                Color color = Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256));

                // Перевірка стану паузи
                while (isPaused)
                {
                    Thread.Sleep(100); // Зачекайте і спробуйте знову через 100 мс
                }

                // Оновити інтерфейс у потоці UI
                Dispatcher.Invoke(() =>
                {
                    progressBar.Value = value;
                    progressBar.Foreground = new SolidColorBrush(color);
                });

                // Почекати трохи перед наступною ітерацією
                Thread.Sleep(1000);
            }
        }

        private void PauseAnimation_Click(object sender, RoutedEventArgs e)
        {
            isPaused = !isPaused; // Зміна стану паузи
            if (isPaused)
            {
                Pause.Content = "Возобновить";
            }
            else
            {
                Pause.Content = "Пауза";
            }
        }




    }
}
