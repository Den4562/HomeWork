using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace WpаFindWord
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

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string wordToSearch = WordTextBox.Text;
            string filePath = FilePathTextBox.Text;

            if (string.IsNullOrWhiteSpace(wordToSearch) || string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Ошибка: Заполните поля для поиска и нахождение слова.");
                return;
            }

            try
            {
                int wordCount = await SearchWordAsync(filePath, wordToSearch);
                ResultLabel.Content = $"Слово '{wordToSearch}' упоминаеться в количестве {wordCount}.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private async Task<int> SearchWordAsync(string filePath, string wordToSearch)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string fileContent = await reader.ReadToEndAsync();
                string[] words = fileContent.Split(new char[] { ' ', '\t', '\n', '\r', '.', ',', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                int wordCount = words.Count(word => word.Equals(wordToSearch, StringComparison.OrdinalIgnoreCase));
                return wordCount;
            }
        }
    }
}
