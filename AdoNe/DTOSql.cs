using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdoNetApp_1
{
    public class DTOSql
    {
        readonly string connectionString;

        SqlConnection? connection;
        public DTOSql(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }


        public async Task<ClientsViewModel> LoadProdazhiFromDb(ClientsViewModel old_data)
        {
            ClientsViewModel clientsViewModel = new ClientsViewModel();
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.CommandText = "SELECT * FROM Prodazhi";
                        sqlCommand.Connection = connection;
                        SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                clientsViewModel.AddProdazhi(new Prodazhi(
                                    sqlDataReader.GetInt32(0),
                                    sqlDataReader.GetInt32(1),
                                    sqlDataReader.GetString(2),
                                    sqlDataReader.GetString(3),
                                    sqlDataReader.GetInt32(4),
                                    sqlDataReader.GetInt32(5),
                                    sqlDataReader.GetDateTime(6)));
                            }

                            return clientsViewModel;
                        }
                        else
                        {
                            MessageBox.Show("Нет данных", "Данные", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    return old_data;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"SqlException: {ex.Message}", "SQL ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return old_data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Системная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return old_data;
                }
            }
        }


        public async Task<ClientsViewModel> LoadClientsFromDb(ClientsViewModel old_data)
        {
            ClientsViewModel clientsViewModel = new ClientsViewModel();
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand sqlCommand = new SqlCommand();
                        sqlCommand.CommandText = "SELECT * FROM Product";
                        sqlCommand.Connection = connection;
                        SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
                        if (sqlDataReader.HasRows)
                        {
                            while (sqlDataReader.Read())
                            {
                                clientsViewModel.AddProduct(new Product(sqlDataReader.GetInt32(0), sqlDataReader.GetString(1), sqlDataReader.GetString(2), sqlDataReader.GetInt32(3), sqlDataReader.GetString(4), sqlDataReader.GetInt32(5)));
                            }

                            return clientsViewModel;
                        }
                        else
                        {
                            MessageBox.Show("No rows", "Data", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    return old_data;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"SqlException: {ex.Message}", "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return old_data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"System Exception: {ex.Message}", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return old_data;
                }
            }
        }

        public async Task DeleteClientFromDb(int id)
        {
            string sqlExpression = "DELETE FROM Product WHERE Id = @id";
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                        SqlParameter idParam = new SqlParameter("@id", id);
                        

                        sqlCommand.Parameters.Add(idParam);
                        var result = await sqlCommand.ExecuteNonQueryAsync();

                        await connection.CloseAsync();
                        if (result > 0)
                        {
                            MessageBox.Show("Data deleted", "Data", MessageBoxButton.OK, MessageBoxImage.Information);
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
        }


        public async Task DeleteProdazhiFromDb(int id)
        {
            string sqlExpression = "DELETE FROM Prodazhi WHERE Id = @id";
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                        SqlParameter idParam = new SqlParameter("@id", id);


                        sqlCommand.Parameters.Add(idParam);
                        var result = await sqlCommand.ExecuteNonQueryAsync();

                        await connection.CloseAsync();
                        if (result > 0)
                        {
                            MessageBox.Show("Data deleted", "Data", MessageBoxButton.OK, MessageBoxImage.Information);
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
        }



        public async Task UpdateClient(Product client)
        {
            string sqlExpression = "UPDATE Product SET Name = @Name, Type = @Type, Amount = @Amount, Manager = @Manager, Sobivartist = @Sobivartist WHERE Id = @id";
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                        SqlParameter idParam = new SqlParameter("@id", client.Id);
                        SqlParameter nameParam = new SqlParameter("@Name", client.Name);
                        SqlParameter TypeParam = new SqlParameter("@Type", client.Type);
                        SqlParameter AmountParam = new SqlParameter("@Amount", client.Amount);
                        SqlParameter ManagerParam = new SqlParameter("@Manager", client.Manager);
                        SqlParameter SobivartistParam = new SqlParameter("@Sobivartist", client.Sobivartist);
                        sqlCommand.Parameters.Add(idParam);
                        sqlCommand.Parameters.Add(nameParam);
                        sqlCommand.Parameters.Add(TypeParam);
                        sqlCommand.Parameters.Add(AmountParam);
                        sqlCommand.Parameters.Add(ManagerParam);
                        sqlCommand.Parameters.Add(SobivartistParam);
                        var result = await sqlCommand.ExecuteNonQueryAsync();

                        await connection.CloseAsync();
                        if (result > 0)
                        {
                            MessageBox.Show("Data was edit", "Data", MessageBoxButton.OK, MessageBoxImage.Information);
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
        }


        public async Task UpdateProdazhi(Prodazhi prodazhi)
        {
            string sqlExpression = "UPDATE Prodazhi SET ProductId = @ProductId, Name = @Name, Manager = @Manager, AmountSale = @AmountSale, CostOne = @CostOne, DataSell = @DataSell WHERE Id = @id";
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                        SqlParameter idParam = new SqlParameter("@id", prodazhi.Id);
                        SqlParameter productIdParam = new SqlParameter("@ProductId", prodazhi.ProductId);
                        SqlParameter nameParam = new SqlParameter("@Name", prodazhi.Name);
                        SqlParameter managerParam = new SqlParameter("@Manager", prodazhi.Manager);
                        SqlParameter amountSaleParam = new SqlParameter("@AmountSale", prodazhi.AmountSale);
                        SqlParameter costOneParam = new SqlParameter("@CostOne", prodazhi.CostOne);
                        SqlParameter dataSellParam = new SqlParameter("@DataSell", prodazhi.DataSell);

                        sqlCommand.Parameters.Add(idParam);
                        sqlCommand.Parameters.Add(productIdParam);
                        sqlCommand.Parameters.Add(nameParam);
                        sqlCommand.Parameters.Add(managerParam);
                        sqlCommand.Parameters.Add(amountSaleParam);
                        sqlCommand.Parameters.Add(costOneParam);
                        sqlCommand.Parameters.Add(dataSellParam);

                        var result = await sqlCommand.ExecuteNonQueryAsync();

                        await connection.CloseAsync();
                        if (result > 0)
                        {
                            MessageBox.Show("Данные были обновлены", "Данные", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Нет данных", "Данные", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"SqlException: {ex.Message}", "SQL ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Системная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        public async Task SaveProdazhiToDb(int productId, string name, string manager, int amountSale, int costOne, DateTime dataSell)
        {
            string sqlExpression = "INSERT INTO Prodazhi (ProductId, Name, Manager, AmountSale, CostOne, DataSell) VALUES (@ProductId, @Name, @Manager, @AmountSale, @CostOne, @DataSell)";
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                        SqlParameter productIdParam = new SqlParameter("@ProductId", productId);
                        SqlParameter nameParam = new SqlParameter("@Name", name);
                        SqlParameter managerParam = new SqlParameter("@Manager", manager);
                        SqlParameter amountSaleParam = new SqlParameter("@AmountSale", amountSale);
                        SqlParameter costOneParam = new SqlParameter("@CostOne", costOne);
                        SqlParameter dataSellParam = new SqlParameter("@DataSell", dataSell);

                        sqlCommand.Parameters.Add(productIdParam);
                        sqlCommand.Parameters.Add(nameParam);
                        sqlCommand.Parameters.Add(managerParam);
                        sqlCommand.Parameters.Add(amountSaleParam);
                        sqlCommand.Parameters.Add(costOneParam);
                        sqlCommand.Parameters.Add(dataSellParam);

                        var result = await sqlCommand.ExecuteNonQueryAsync();

                        await connection.CloseAsync();
                        if (result > 0)
                        {
                            MessageBox.Show("Data saved", "Data", MessageBoxButton.OK, MessageBoxImage.Information);
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
        }


        public async Task SaveClientToDb(string name, string type, int amount, string manager, int sobivartist)
        {
            string sqlExpression = "INSERT INTO Product VALUES (@Name, @Type, @Amount, @Manager, @Sobivartist)";
            using (connection = new SqlConnection(connectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                        SqlParameter nameParam = new SqlParameter("@Name", name);
                        SqlParameter TypeParam = new SqlParameter("@Type", type);
                        SqlParameter AmountParam = new SqlParameter("@Amount", amount);
                        SqlParameter ManagerParam = new SqlParameter("@Manager", manager);
                        SqlParameter SobivartistParam = new SqlParameter("@Sobivartist",sobivartist);
                        sqlCommand.Parameters.Add(nameParam);
                        sqlCommand.Parameters.Add(TypeParam);
                        sqlCommand.Parameters.Add(AmountParam);
                        sqlCommand.Parameters.Add(ManagerParam);
                        sqlCommand.Parameters.Add(SobivartistParam);

                        var result = await sqlCommand.ExecuteNonQueryAsync();

                        await connection.CloseAsync();
                        if (result > 0)
                        {
                            MessageBox.Show("Data saved", "Data", MessageBoxButton.OK, MessageBoxImage.Information);
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
        }
        public async void TakeInformationServer()
        {
            string content = "";
            try
            {
                using (this.connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {

                        content += "Connection string: " + connection.ConnectionString + "\n";
                        content += "Database: " + connection.Database + "\n";
                        content += "DataSource: " + connection.DataSource + "\n";
                        content += "ServerVersion: " + connection.ServerVersion + "\n";
                        content += "State: " + connection.State + "\n";
                        content += "WorkstationId: " + connection.WorkstationId + "\n";
                        MessageBox.Show(content, "Connection Informayion", MessageBoxButton.OK, MessageBoxImage.Information);
                        await connection.CloseAsync();
                    }
                    else
                    {
                        MessageBox.Show("Connection is closed", "Connection", MessageBoxButton.OK, MessageBoxImage.Error);
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
        public async void CloseConnection()
        {
            if (connection != null)
            {
                await connection.CloseAsync();
            }
        }
        public string GetConnectionString()
        {
            return connectionString;
        }
    }
}
