using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLBaiGiuXe.DAO
{
    public class DataProvider
    {
        private static readonly object lockObject = new object();
        private static volatile DataProvider instence;

        // Câu lệnh kết nối
        private string con = "Data Source=quanlibaixe.mssql.somee.com;Initial Catalog=quanlibaixe;User ID=quanlibaixe_SQLLogin_1;Password=hhu3w7qia2";

        // get set Instence
        public static DataProvider Instance
        {
            get
            {
                if (instence == null)
                {
                    lock (lockObject)
                    {
                        if (instence == null)
                        {
                            instence = new DataProvider();
                        }
                    }
                }
                return DataProvider.instence;
            }
            private set => instence = value;
        }

        
        private DataProvider() { }

        
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();

            }
            return data;
        }
        
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(con))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = command.ExecuteNonQuery();

                connection.Close();

            }
            return data;
        }

    }
}

