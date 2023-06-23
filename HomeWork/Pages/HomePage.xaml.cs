using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.Providers;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        GoodsCollection goodsCollection = new GoodsCollection();
        public HomePage()
        {
            InitializeComponent();
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
            if (count == "")
            {
                throw new FormatException("Введіть кількість товару");
            }
            return true;
        }
        private void AddGood_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Check(tbName.Text, tbPrice.Text, tbCount.Text))
                {
                    goodsCollection.AddGood(tbName.Text, Convert.ToInt32(tbPrice.Text), Convert.ToInt32(tbCount.Text), "Delete");
                    goodsGrid.ItemsSource = goodsCollection.Goods;
                    goodsGrid.Items.Refresh();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteGood_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Good good = goodsGrid.SelectedItem as Good;
                goodsCollection.DeleteGood(tbName.Text);
                goodsGrid.ItemsSource = goodsCollection.Goods;
                goodsGrid.Items.Refresh();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void MainWindowLoad(object sender, RoutedEventArgs e)
        {
           
        }


        private void EditGood_Click(object sender, RoutedEventArgs e)
        {
            var selectedGood = goodsGrid.SelectedItem as Good;
            if (selectedGood != null)
            {
                try
                {
                    string name = tbName.Text;
                    int price = int.Parse(tbPrice.Text);
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

        private void DublicateAddGood_Click(object sender, RoutedEventArgs e)
        {
            Good good = goodsGrid.SelectedItem as Good;
            goodsCollection.AddGood(good.Name, good.Price, good.Count, good.Button);
            goodsGrid.ItemsSource = goodsCollection.Goods;
            goodsGrid.Items.Refresh();
        }
    }
}
