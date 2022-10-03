using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetVeritabani
{
    public class Program
    {
        static void Main(string[] args){
            GetSqlConnection();
            GetMySqlConnection();
        }

        static void GetMySqlConnection(){
            string connectionString = @"Data Source = .\SQLEXPRESS; Initial Catalog= Northwind2;Integrated Security=SSPI;";

            string  connectionString2 = @"server=localhost;port=3306;database="

            // DRIVER PROVIDER
            using (var connection = new SqlConnection(connectionString) )
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Bağlantı Sağlandı");

                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

        }


        
    }
}