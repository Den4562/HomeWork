using System.Numerics;
using System.Threading.Tasks;
using System.Windows;

namespace task2_16._09._23
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

        private async void Start_click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(max_tb.Text, out int limit) && limit >= 0)
            {
                await counting_Fibonacci_numbers(limit);
            }
            else
            {
                count_tb.Text = "Error";
            }
        }
        private async Task counting_Fibonacci_numbers(int limit)
        {
            await Task.Run(() =>
            {
                BigInteger first = 0;
                BigInteger second = 1;
                BigInteger sum = 0;
                for (int i = 0; i < limit; i++)
                {
                    sum += first;
                    BigInteger next = first + second;
                    second = first;
                    first = next;
                }
                Application.Current.Dispatcher.Invoke(() => count_tb.Text = (sum + " "));
            });
        }
    }
}
