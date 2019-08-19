using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tests
{
    public class DatabaseUtils
    {
        // This function takes a list of ids and returns the matching records from the users table
        public static IList<Data> getUsers(IList<int> idList)
        {
            // Assert the ids are not null and atleast 1
            Assert.NotNull(idList);
            Assert.That(idList.Count > 0);
            IList<Data> dataList = new List<Data>();

            // Prepare the SQL query needed for querying the matching users
            StringBuilder selectQuery = new StringBuilder();
            selectQuery.Append("select * from model.dbo.users where id in (");
            for (int i = 0; i < idList.Count; i++)
            {
                selectQuery.Append(idList[i]);
                Console.WriteLine(selectQuery.ToString());
                if (i != idList.Count - 1)
                {
                    selectQuery.Append(",");
                    Console.WriteLine(selectQuery.ToString());
                }
            }
            selectQuery.Append(")");
            Console.WriteLine("Query to the db:" + selectQuery.ToString());

            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "localhost";
                builder.UserID = "****";
                builder.Password = "***";
                builder.InitialCatalog = "****";

                // Connect to SQL
                Console.Write("Connecting to SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connected to the database......");

                    using (SqlCommand command = new SqlCommand(selectQuery.ToString(), connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Data data = new Data();
                                data.id = reader.GetInt32(0);
                                data.email = reader.GetString(1);
                                data.first_name = reader.GetString(2);
                                data.last_name = reader.GetString(3);
                                data.avatar = reader.GetString(4);
                                dataList.Add(data);
                            }

                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            return dataList;
        }
    }
}