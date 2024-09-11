using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Repositories
{
    public class ProviderRepository : IHandleServiceProviders
    {
        public readonly string connectionString = string.Empty;
        public ProviderRepository(IConfiguration configuration) {

            connectionString = configuration.GetConnectionString("ConnectionDBSQL");

        }


        public void BalanceServiceProviders(BalanceServiceProviders request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("spi_BalanceServiceProviders", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@i_bspCode", SqlDbType.VarChar)).Value = request.BspCode;
                    command.Parameters.Add(new SqlParameter("@i_bspName", SqlDbType.VarChar)).Value = request.BspName;
                    command.Parameters.Add(new SqlParameter("@i_businessId", SqlDbType.VarChar)).Value = request.BusinessId;
                    command.Parameters.Add(new SqlParameter("@i_codingScheme", SqlDbType.VarChar)).Value = request.CodingScheme;
                    command.Parameters.Add(new SqlParameter("@i_country", SqlDbType.VarChar)).Value = request.Country;  
                    
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

            }
            catch (Exception)
            {

                throw new Exception("Communication with the database could not be established");
            }
            
        }



        public BalanceServiceProviders BalanceServiceProvidersById(Guid request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("spr_BalanceServiceProviders", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@i_idBalanceServiceProvider", SqlDbType.UniqueIdentifier)).Value = request;

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    BalanceServiceProviders provider = new BalanceServiceProviders();

                    while (reader.Read())
                    {
                        provider.BspCode = reader["bspCode"].ToString();
                        provider.BspName = reader["bspName"].ToString();
                        provider.BusinessId = reader["businessId"].ToString();
                        provider.CodingScheme = reader["codingScheme"].ToString();
                        provider.Country = reader["country"].ToString();
                    }

                    connection.Close();

                    return provider;
                }

            }
            catch (Exception)
            {

                throw new Exception("Communication with the database could not be established");
            }

        }

        public void DeleteBalanceServiceProvidersById(Guid request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("spd_BalanceServiceProviders", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@i_idBalanceServiceProvider", SqlDbType.UniqueIdentifier)).Value = request;

                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }

            }
            catch (Exception)
            {

                throw new Exception("Communication with the database could not be established");
            }

        }

        public void UpdateBalanceServiceProvidersById(BalanceServiceProviders request)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("spu_BalanceServiceProviders", connection);

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@i_idBalanceServiceProvider", SqlDbType.UniqueIdentifier)).Value = request.Id;
                    command.Parameters.Add(new SqlParameter("@i_bspCode", SqlDbType.VarChar)).Value = request.BspCode;
                    command.Parameters.Add(new SqlParameter("@i_bspName", SqlDbType.VarChar)).Value = request.BspName;
                    command.Parameters.Add(new SqlParameter("@i_businessId", SqlDbType.VarChar)).Value = request.BusinessId;
                    command.Parameters.Add(new SqlParameter("@i_codingScheme", SqlDbType.VarChar)).Value = request.CodingScheme;
                    command.Parameters.Add(new SqlParameter("@i_country", SqlDbType.VarChar)).Value = request.Country;

                    connection.Open();

                    command.ExecuteNonQuery();

                    connection.Close();
                }

            }
            catch (Exception)
            {

                throw new Exception("Communication with the database could not be established");
            }

        }

    }


    
}


