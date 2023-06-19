using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.Providers;
using static WpfApp1.MainWindow;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GoodsCollection goodsCollection = new GoodsCollection();
        public MainWindow()
        {
            InitializeComponent();
            goodsGrid.ItemsSource = goodsCollection.GetGoods();
        }
        bool Check(string name, string price, string count)
        {
            if (name == "")
            {
                
               throw new FormatException("Введіть назву товару");
            }
            if (price == "")
            {
                throw new FormatException("Введіть ціну товару");

            }
            if (count== "")
            {
                throw new FormatException("Введіть кількість товару");
            }
            return true;
        }
        private void AddGood_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = tbName.Text;
                decimal price = decimal.Parse(tbPrice.Text);
                int count = int.Parse(tbCount.Text);

           
                Check(name, price.ToString(), count.ToString());

           
                goodsCollection.AddGood(name, price, count);

         
                goodsGrid.ItemsSource = goodsCollection.GetGoods();
               
                goodsGrid.Items.Refresh();
               
            
                tbName.Text = "";
                tbPrice.Text = "";
                tbCount.Text = "";
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DeleteGood_Click(object sender, RoutedEventArgs e)
        {
            var selectedGood = goodsGrid.SelectedItem as Good;
            if (selectedGood != null)
            {
                goodsCollection.RemoveGood(selectedGood);
                goodsGrid.Items.Refresh();
                tbName.Text = "";
                tbPrice.Text = "";
                tbCount.Text = "";
            }
        }

        private void EditGood_Click(object sender, RoutedEventArgs e)
        {
            var selectedGood = goodsGrid.SelectedItem as Good;
            if (selectedGood != null)
            {
                try
                {
                    string name = tbName.Text;
                    decimal price = decimal.Parse(tbPrice.Text);
                    int count = int.Parse(tbCount.Text);

                    
                    Check(name, price.ToString(), count.ToString());

                 
                    selectedGood.Name = name;
                    selectedGood.Price = price;
                    selectedGood.Count = count;

                    goodsGrid.Items.Refresh();

                   
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

      

        private void MainWindowLoad(object sender, RoutedEventArgs e)
        {
            Button button = new Button();


            goodsGrid.ItemsSource = goodsCollection.GetGoods();
        }

        private void SaveList_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var item in goodsGrid.Items)
                    {
                        if (item is Good good)
                        {
                            writer.WriteLine($"Назва: {good.Name}, Ціна: {good.Price}, Кількість: {good.Count}");
                        }
                    }
                }

                MessageBox.Show("Список сохранен в файл.");
            }
        }

        private void LoadList_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                goodsCollection.ClearGoods();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                      
                        string[] parts = line.Split(',');

                        if (parts.Length == 3)
                        {
                            string name = parts[0].Split(':')[1].Trim();
                            decimal price = decimal.Parse(parts[1].Split(':')[1].Trim());
                            int count = int.Parse(parts[2].Split(':')[1].Trim());

                          
                            goodsCollection.AddGood(name, price, count);
                        }
                    }
                }

              
                goodsGrid.Items.Refresh();

                MessageBox.Show("Список загружен из файла.");
            }
        }

        private void DataGridSelect(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            var item = goodsGrid.SelectedItem as Good;
            if (item != null)
            {
                tbName.Text = item.Name;
                tbPrice.Text = item.Price.ToString();
                tbCount.Text = item.Count.ToString();
            }
           
        }


        public class GoodsCollection
        {
            private List<Good> goods = new List<Good>();

            public IEnumerable<Good> GetGoods()
            {
                return goods;
            }
            public void RemoveGood(Good good)
            {
                goods.Remove(good);
               
            }

            public void ClearGoods()
            {
                goods.Clear();
            }

            public void AddGood(string name, decimal price, int count)
            {
                Good newGood = new Good(name, price, count);
                goods.Add(newGood);
            }
        }

        
        public class Good
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Count { get; set; }

            public Good(string name, decimal price, int count)
            {
                Name = name;
                Price = price;
                Count = count;
            }
        }
    }
   
}
