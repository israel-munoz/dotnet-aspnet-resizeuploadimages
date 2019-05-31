using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DbManager
{
    public class DbService
    {
        public static int InsertImage(byte[] content, string fileName)
        {
            int id;
            string connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("InsertImage", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@fileName", fileName);
                command.Parameters.AddWithValue("@content", content);
                SqlParameter outputId = command.Parameters.Add(
                    new SqlParameter("@id", SqlDbType.Int) { Direction = ParameterDirection.Output });

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    id = (int)outputId.Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }

            return id;
        }
    }
}
