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
using System.Windows.Threading;

namespace WpfHM19
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly List<int> primeNumbers = new List<int>();
        private CancellationTokenSource simpleStop;
        private CancellationTokenSource fibonachiStop;
        private int currentFibonacciCount = 0;
        private int currentPrimeNumber = 2;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int start;
            int end;

            if (string.IsNullOrWhiteSpace(StartTextBox.Text))
            {
                start = 2; // За замовчуванням починаємо з 2, якщо не вказано нижню межу.
            }
            else
            {
                start = int.Parse(StartTextBox.Text);
            }

            if (string.IsNullOrWhiteSpace(EndTextBox.Text))
            {
                end = int.MaxValue; // Генерувати до завершення додатка, якщо не вказано верхню межу.
            }
            else
            {
                end = int.Parse(EndTextBox.Text);
            }

            primeNumbers.Clear();
            ResultListBox.Items.Clear();

            simpleStop = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(GeneratePrimeNumbers, new Tuple<int, int>(start, end));
        }

        private void GeneratePrimeNumbers(object state)
        {
            Tuple<int, int> range = (Tuple<int, int>)state;
            int start = range.Item1;
            int end = range.Item2;

            for (int number = start; number <= end;)
            {
                if (IsPrime(number))
                {
                    primeNumbers.Add(number);

                    Dispatcher.Invoke(() =>
                    {
                        ResultListBox.Items.Add(number);
                    }, DispatcherPriority.Background);

                    if (simpleStop.Token.IsCancellationRequested)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            MessageBox.Show("Генерація прервана.");
                        });
                        return;
                    }
                }

                // Увеличиваем число на +1 вне зависимости от того, простое оно или нет.
                number++;
            }
           

            Dispatcher.Invoke(() =>
            {
                MessageBox.Show("Генерація завершена.");
            });
        }

        private bool IsPrime(int number)
        {
            if (number <= 1)
                return false;
            if (number <= 3)
                return true;
            if (number % 2 == 0 || number % 3 == 0)
                return false;

            int i = 5;
            while (i * i <= number)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                    return false;
                i += 6; // Меняем шаг итерации на +6
            }

            return true;
        }

      
        private void ListBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Заборонити подальшу обробку події, щоб прокрутка колесом миші спрацьовувала належним чином.
            e.Handled = true;
        }

        private void GenerateFibonacciButton_Click(object sender, RoutedEventArgs e)
        {
            int count;

            if (string.IsNullOrWhiteSpace(Fib.Text))
            {
                MessageBox.Show("Введіть коректну верхню межу для генерації чисел Фібоначчі.");
                return;
            }

            if (!int.TryParse(Fib.Text, out count))
            {
                MessageBox.Show("Введіть коректну верхню межу для генерації чисел Фібоначчі.");
                return;
            }

            primeNumbers.Clear();
            ResultListBox.Items.Clear();

            fibonachiStop = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(GenerateFibonacci, count);
        }

        private void GenerateFibonacci(object state)
        {
            int count = (int)state;
            int a = 0, b = 1;

            for (int i = 0; i < count; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;

                Dispatcher.Invoke(() =>
                {
                    ResultListBox.Items.Add(a);
                }, DispatcherPriority.Background);

                if (fibonachiStop.Token.IsCancellationRequested)
                {
                    Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("Генерація чисел Фібоначчі прервана.");
                    });
                    return;
                }

                if (fibonachiStop.Token.IsCancellationRequested)
                {
                    Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("Генерація чисел Фібоначчі прервана.");
                    });
                    return;
                }
            }

            Dispatcher.Invoke(() =>
            {
                MessageBox.Show("Генерація чисел Фібоначчі завершена.");
            });


        }
        private void StopFibonacci(object sender, RoutedEventArgs e)
        {
    
            fibonachiStop?.Cancel();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
           
            simpleStop?.Cancel();
        }

      
    }
}
