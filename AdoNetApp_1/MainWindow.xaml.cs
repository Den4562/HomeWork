using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdoNetApp_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DTOSql sql;
        ClientsViewModel clientsViewModel;
        SqlConnection connection;
        public MainWindow()
        {
            sql = new DTOSql(@"Data Source=DESKTOP-N5K3CGS\SQLEXPRESS01;Initial Catalog=KantstovaryDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            clientsViewModel = new ClientsViewModel();
            connection = sql.GetSqlConnection(); // Получаем SqlConnection из DTOSql
            InitializeComponent();
        }

        private void CloseConnection_Click(object sender, RoutedEventArgs e)
        {
            sql.CloseConnection();
        }

        private void TakeInfo_Click(object sender, RoutedEventArgs e)
        {
            sql.TakeInformationServer();
        }


        private async void Run_Click(object sender, RoutedEventArgs e)
        {
            string query = QueryText.Text;

            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Please enter a valid SQL query.", "Query Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            

            try
            {
                using (SqlConnection connection = new SqlConnection(sql.GetConnectionString()))
                {
                    await connection.OpenAsync();
                    SqlCommand sqlCommand = new SqlCommand(query, connection);
                    SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
                    clientsViewModel.ClearProduct();
                    clientsViewModel.ClearProdazhis();
                    DataTable.ItemsSource = null;

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {

                         
                             if (sqlDataReader.FieldCount == 3 && sqlDataReader.GetName(0) == "Id" && sqlDataReader.GetName(1) == "Name" && sqlDataReader.GetName(2) == "Type")
                            {
                                int id = Convert.ToInt32(sqlDataReader.GetValue(0));
                                string name = sqlDataReader.GetString(1);
                                string type = sqlDataReader.GetString(2);

                                clientsViewModel.AddProduct(new Product(id, name, type, 0, "", 0));

                            }

                            else if (sqlDataReader.FieldCount == 2 && sqlDataReader.GetName(0) == "Id" && sqlDataReader.GetName(1) == "Manager")
                            {
                                int id = Convert.ToInt32(sqlDataReader.GetValue(0));
                                string manager = sqlDataReader.GetString(1);
                                clientsViewModel.AddProdazhi(new Prodazhi(id, 0, "", manager, 0, 0, DateTime.Now));
                            }


                            else if (sqlDataReader.FieldCount == 2 && sqlDataReader.GetName(0) == "Manager" && sqlDataReader.GetName(1) == "TotalAmount")
                            {
                                string manager = sqlDataReader.GetString(0);
                                int amount = Convert.ToInt32(sqlDataReader.GetValue(1));

                                clientsViewModel.AddProduct(new Product(0, "", "", 0, manager, amount));
                            }

                           else if (sqlDataReader.FieldCount == 2 && sqlDataReader.GetName(0) == "Type" && sqlDataReader.GetName(1) == "TotalAmount")
                            {
                                string type = sqlDataReader.GetString(0);
                                int totalAmount = Convert.ToInt32(sqlDataReader.GetValue(1));
                                clientsViewModel.AddProduct(new Product(0, "", type, totalAmount, "", 0));
                            }

                            else  if (sqlDataReader.FieldCount == 7) // Проверяем, что количество полей равно 7 (количеству полей в таблице Prodazhi)
                            {
                                int id = Convert.ToInt32(sqlDataReader.GetValue(0));
                                int productId = Convert.ToInt32(sqlDataReader.GetValue(1));
                                string name = sqlDataReader.GetString(2);
                                string manager = sqlDataReader.GetString(3);
                                int amountSale = Convert.ToInt32(sqlDataReader.GetValue(4));
                                int costOne = Convert.ToInt32(sqlDataReader.GetValue(5));
                                DateTime dataSell = sqlDataReader.GetDateTime(6);

                                clientsViewModel.AddProdazhi(new Prodazhi(id, productId, name, manager, amountSale, costOne, dataSell));
                                
                            }
                            else
                            {
                                int id = Convert.ToInt32(sqlDataReader.GetValue(0));
                                string name = sqlDataReader.GetValue(1).ToString();
                                string type = sqlDataReader.GetValue(2).ToString();
                                int amount = Convert.ToInt32(sqlDataReader.GetValue(3));
                                string manager = sqlDataReader.GetValue(4).ToString();
                                int sobivartist = Convert.ToInt32(sqlDataReader.GetValue(5));

                                clientsViewModel.AddProduct(new Product(id,name,type,amount, manager, sobivartist));
                            }
                        }
                      


                        if (query.Contains("Product"))
                        {
                            DataTable.ItemsSource = clientsViewModel.Products.Distinct().ToList();
                        }
                        else if (query.Contains("Prodazhi"))
                        {
                            DataTable.ItemsSource = clientsViewModel.Prodazhis.Distinct().ToList();
                        }

                    }
                    else
                    {
                        MessageBox.Show("No rows", "Data", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"SqlException: {ex.Message}", "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"System Exception: {ex.Message}", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void RunSave_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            string type = tbType.Text;
            int amount = Convert.ToInt32(tbAmount.Text);
            string manager =tbManager.Text;
            int sobivartist = Convert.ToInt32(tbSobivartist.Text);

            await sql.SaveClientToDb(name, type, amount, manager,sobivartist);
            clientsViewModel = await sql.LoadClientsFromDb(clientsViewModel);
            DataTable.ItemsSource = clientsViewModel.Products;
            DataTable.Items.Refresh();
            tbName.Text = "";
            tbType.Text = "";
            tbAmount.Text = "";
            tbManager.Text = "";
            tbSobivartist.Text = "";
        }

        private async void RunSaveProdazhi_Click(object sender, RoutedEventArgs e)
        {
           
                int productId = Convert.ToInt32(tbProdId.Text);
                string name = tbNameProdaxhi.Text;
                string manager = tbManagerProdazhi.Text;
                int amountSale = Convert.ToInt32(tbAmountSale.Text);
                int costOne = Convert.ToInt32(tbCostOne.Text);
                DateTime dataSell = DateTime.Now;

                await sql.SaveProdazhiToDb(productId, name, manager, amountSale, costOne, dataSell);
                clientsViewModel = await sql.LoadProdazhiFromDb(clientsViewModel);
                DataTable.ItemsSource = clientsViewModel.Prodazhis;
                DataTable.Items.Refresh();
                tbProdId.Text = "";
                tbNameProdaxhi.Text = "";
                tbManagerProdazhi.Text = "";
                tbAmountSale.Text = "";
                tbCostOne.Text = "";
                tbDataSell.Text = "";

        }

        private async void HomeLoad(object sender, RoutedEventArgs e)
        {
            clientsViewModel = await sql.LoadClientsFromDb(clientsViewModel);
            DataTable.ItemsSource = clientsViewModel.Products;
            DataTable.Items.Refresh();
        }

        private async void DeleteClient_Click(object sender, RoutedEventArgs e)
        {
            if(DataTable.SelectedItem != null)
            {
                Product client = (Product)DataTable.SelectedItem;
                await sql.DeleteClientFromDb(client.Id);
                clientsViewModel.Products.Remove(client);
                DataTable.ItemsSource = clientsViewModel.Products;
                DataTable.Items.Refresh();
            }
            else
            {
                MessageBox.Show("No client selected! Select client to remove.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteProdazhi_Click(object sender, RoutedEventArgs e)
        {
            if (DataTable.SelectedItem != null)
            {
                Prodazhi prodazhi = DataTable.SelectedItem as Prodazhi;
                await sql.DeleteProdazhiFromDb(prodazhi.Id); // Предположим, что у вас есть метод DeleteProdazhiFromDb для удаления Prodazhi
                clientsViewModel.Prodazhis.Remove(prodazhi);
                DataTable.ItemsSource = clientsViewModel.Prodazhis;
                DataTable.Items.Refresh();
            }
            else
            {
                MessageBox.Show("No prodazhi selected! Select prodazhi to remove.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Edit_Click(object sender, MouseButtonEventArgs e)
        {
            DataTable.IsReadOnly = false;
        }

        private async void ChangeData(object sender, EventArgs e)
        {
            if (DataTable.IsReadOnly == false)
            {
                if (DataTable.SelectedItem is Prodazhi prodazhi) // Проверка на тип Prodazhi
                {
                    await sql.UpdateProdazhi(prodazhi); // Предположим, что у вас есть метод UpdateProdazhi для обновления Prodazhi
                }

                DataTable.IsReadOnly = true;
            }
        }
    }
}
