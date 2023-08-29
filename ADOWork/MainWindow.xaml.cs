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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataRow selectedRow;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-N5K3CGS\\SQLEXPRESS01;Initial Catalog=KantstovaryDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string queryString = "SELECT * FROM Product";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                    DataSet ds = new DataSet();
                   
                    adapter.Fill(ds);
               
                    DGMain.ItemsSource = ds.Tables[0].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-N5K3CGS\\SQLEXPRESS01;Initial Catalog=KantstovaryDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string queryString = "SELECT * FROM Product";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.NewRow();
                    dr["Name"] = txtName.Text;
                    dr["Type"] = txtType.Text;
                    dr["Amount"] = Convert.ToInt32(txtAmount.Text);
                    dr["Manager"] = txtManager.Text;
                    dr["Sobivartist"] = Convert.ToInt32(txtSobivartist.Text);

                    dt.Rows.Add(dr);
                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                    adapter.Update(dt);
                    ds.Clear();
                    adapter.Fill(ds);
                    DGMain.ItemsSource = ds.Tables[0].DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void DGMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGMain.SelectedItem != null && DGMain.SelectedItem is DataRowView)
            {
                selectedRow = ((DataRowView)DGMain.SelectedItem).Row;
                Console.WriteLine("Selected Row ID: " + selectedRow["id"]); 
            }
        }

        private void DGMain_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
               
                DataRowView editedItem = e.Row.Item as DataRowView;
                if (editedItem != null)
                {
                    UpdateData(editedItem.Row);
                }
            }
        }

        private async Task UpdateData(DataRow rowToUpdate)
        {
            string connectionString = "Data Source=DESKTOP-N5K3CGS\\SQLEXPRESS01;Initial Catalog=KantstovaryDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            string updateQuery = "UPDATE Product SET Name = @Name, Type = @Type, Amount = @Amount, Manager = @Manager, Sobivartist = @Sobivartist WHERE id = @ProductID";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand(updateQuery, connection);
                    cmd.Parameters.AddWithValue("@Name", rowToUpdate["Name"]);
                    cmd.Parameters.AddWithValue("@Type", rowToUpdate["Type"]);
                    cmd.Parameters.AddWithValue("@Amount", rowToUpdate["Amount"]);
                    cmd.Parameters.AddWithValue("@Manager", rowToUpdate["Manager"]);
                    cmd.Parameters.AddWithValue("@Sobivartist", rowToUpdate["Sobivartist"]);
                    cmd.Parameters.AddWithValue("@ProductID", rowToUpdate["id"]);
                    int rowsAffected = await cmd.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Запись успешно обновлена.");
                    }
                    else
                    {
                        MessageBox.Show("Не удалось обновить запись.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
           
                DataTable dt = ((DataView)DGMain.ItemsSource).Table;

    
                string connectionString = "Data Source=DESKTOP-N5K3CGS\\SQLEXPRESS01;Initial Catalog=KantstovaryDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                string selectQuery = "SELECT * FROM Product"; 
                SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connectionString);

        
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

             
                adapter.Update(dt);

                MessageBox.Show("Изменения сохранены в базе данных.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении изменений: " + ex.Message);
            }
        }


        private async void Delete_click(object sender, RoutedEventArgs e)
        {
            if (selectedRow != null)
            {
                string connectionString = "Data Source=DESKTOP-N5K3CGS\\SQLEXPRESS01;Initial Catalog=KantstovaryDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
                string deleteQuery = "DELETE FROM Product WHERE id = @ProductID"; 

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        await connection.OpenAsync();
                        SqlCommand cmd = new SqlCommand(deleteQuery, connection);
                        cmd.Parameters.AddWithValue("@ProductID", selectedRow["id"]); 
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Запись успешно удалена.");
                            selectedRow = null;
                            // Загрузите данные снова и обновите DataGrid
                            string reloadQuery = "SELECT * FROM Product";
                            SqlDataAdapter adapter = new SqlDataAdapter(reloadQuery, connection);
                            DataSet ds = new DataSet();
                            adapter.Fill(ds);
                            DGMain.ItemsSource = ds.Tables[0].DefaultView;
                        }
                        else
                        {
                            MessageBox.Show("Не удалось удалить запись.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления.");
            }
        }

    }
}
