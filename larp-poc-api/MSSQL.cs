using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace larp_poc_api
{
    public class MSSQL
    {
        private static readonly string Settings = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

        public static List<Dictionary<string, string>> ExecuteMsSqlQuery(string queryString)
        {
            var resultingData = new List<Dictionary<string, string>>();

            try
            {
                using (var connection = new SqlConnection(Settings))
                {
                    connection.Open();

                    using (var command = new SqlCommand(queryString, connection))
                    {
                        var resultReader = command.ExecuteReader();

                        if (resultReader.HasRows)
                        {
                            while (resultReader.Read())
                            {
                                var row = new Dictionary<string, string>();

                                for (var i = 0; i < resultReader.FieldCount; i++)
                                {
                                    var fieldValue = resultReader[i].ToString();

                                    var columnName = resultReader.GetName(i);

                                    row.Add(columnName, fieldValue);
                                }

                                resultingData.Add(row);
                            }
                        }
                        else
                        {
                            resultReader.Read();

                            var emptyRow = new Dictionary<string, string>();

                            for (var i = 0; i < resultReader.FieldCount; i++)
                            {
                                var columnName = resultReader.GetName(i);
                                emptyRow.Add(columnName, null);
                            }

                            resultingData.Add(emptyRow);
                        }

                        return resultingData;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Sql query execution failed with following error: " + e);
            }
        }

        public static void ExecuteNonQuery(string queryString)
        {
            SqlConnection conn = new SqlConnection(Settings);
            conn.Open();
            SqlCommand cmd = new SqlCommand(queryString, conn);
            cmd.ExecuteReader();
            conn.Close();
            conn.Dispose();
        }

        public static void ExecuteNonQuery(string queryString, SqlParameter parameter)
        {
            SqlConnection conn = new SqlConnection(Settings);
            conn.Open();
            SqlCommand cmd = new SqlCommand(queryString, conn);
            cmd.Parameters.Add(parameter);
            cmd.ExecuteReader();
            conn.Close();
            conn.Dispose();
        }
    }
}