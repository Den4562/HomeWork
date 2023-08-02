using MaterialDesignThemes.Wpf;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        string connectionString = @"Data Source=DESKTOP-N5K3CGS\SQLEXPRESS01;Initial Catalog=products;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        SqlConnection connection;
        ClientsViewModel clientsViewModel;
        public MainWindow()
        {
            connection = new SqlConnection(connectionString);
            clientsViewModel = new ClientsViewModel();
            InitializeComponent();
        }

        private async void MainLoad(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.OpenAsync();
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    MessageBox.Show("Connection is open", "Connection", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Connection is closed", "Connection", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private async void CloseConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.CloseAsync();
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    MessageBox.Show("Connection is closed", "Connection", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void TakeInfo_Click(object sender, RoutedEventArgs e)
        {
            string content = "";
            content += "Connection string: " + connection.ConnectionString + "\n";
            content += "Database: " + connection.Database + "\n";
            content += "DataSource: " + connection.DataSource + "\n";
            content += "ServerVersion: " + connection.ServerVersion + "\n";
            content += "State: " + connection.State + "\n";
            content += "WorkstationId: " + connection.WorkstationId + "\n";
            try
            {
                MessageBox.Show(content, "Connection Informayion", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private async void Run_Click(object sender, RoutedEventArgs e)
        {
            string query = QueryText.Text;

            if (string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Please enter a valid SQL query.", "Query Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }



            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = query;
            sqlCommand.Connection = connection;
        
            try
            {
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                clientsViewModel.ClearClients();
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
                            
                            clientsViewModel.AddClient(new Client(id, name, type, "", 0));
                        }
                        else if (sqlDataReader.FieldCount == 2 && sqlDataReader.GetName(0) == "Id"  &&  sqlDataReader.GetName(1) == "Color")
                        {
                            int id = Convert.ToInt32(sqlDataReader.GetValue(0));
                            string color = sqlDataReader.GetString(1);
                            clientsViewModel.AddClient(new Client(id, "", "", color, 0));
                        }

                        else if (sqlDataReader.FieldCount ==5 && sqlDataReader.GetName(0) == "Id" && sqlDataReader.GetName(1) == "Name" && sqlDataReader.GetName(2) == "Type" && sqlDataReader.GetName(3) == "Color" && sqlDataReader.GetName(4) == "Callory" )
                        {
                            int id = Convert.ToInt32(sqlDataReader.GetValue(0));
                            string name = sqlDataReader.GetValue(1).ToString();
                            string type = sqlDataReader.GetValue(2).ToString();
                            string color = sqlDataReader.GetValue(3).ToString();
                            int callory = Convert.ToInt32(sqlDataReader.GetValue(4));
                            clientsViewModel.AddClient(new Client(id, name, type, color, callory));
                        }

                        else if (sqlDataReader.FieldCount ==3 &&  sqlDataReader.GetName(0) == "Id" && sqlDataReader.GetName(1) == "Name" && sqlDataReader.GetName(2) == "Callory")
                        {
                            int id = Convert.ToInt32(sqlDataReader.GetValue(0));
                            string name = sqlDataReader.GetString(1);
                            int callory = Convert.ToInt32(sqlDataReader.GetValue(2));

                            clientsViewModel.AddClient(new Client(id, name, "", "", callory));
                        }

                        else if (sqlDataReader.FieldCount == 1 && sqlDataReader.GetName(0) == "Type")
                        {
                            string type = sqlDataReader.GetString(0); 
                            clientsViewModel.AddClient(new Client(0, "", type, "", 0));
                        }

                        else if (sqlDataReader.FieldCount == 1)
                        {
                           
                            int averageCallory = Convert.ToInt32(sqlDataReader.GetValue(0));
                            clientsViewModel.AddClient(new Client(0, "", "", "", averageCallory));
                        }

                        else if (sqlDataReader.FieldCount == 3 && sqlDataReader.GetName(0) == "Color" && sqlDataReader.GetName(1) == "Type")
                        {
                      
                            string color = sqlDataReader.GetString(0);
                            string type = sqlDataReader.GetString(1);
                            int count = Convert.ToInt32(sqlDataReader.GetValue(2));
                            clientsViewModel.AddClient(new Client(0, "", type, color, count));
                        }

                        else
                        {
                            int id = Convert.ToInt32(sqlDataReader.GetValue(0));
                            string name = sqlDataReader.GetValue(1).ToString();
                            string type = sqlDataReader.GetValue(2).ToString();
                            string color = sqlDataReader.GetValue(3).ToString();
                            int callory = Convert.ToInt32(sqlDataReader.GetValue(4));

                            clientsViewModel.AddClient(new Client(id, name, type, color, callory));
                        }
                    }

                   

                    sqlDataReader.Close(); 
                    

                    DataTable.ItemsSource = clientsViewModel.Clients;
                }
                else
                {
                    MessageBox.Show("No rows", "Data", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
