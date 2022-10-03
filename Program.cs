using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace dotnetVeritabani
{
    public class Program
    {
        static void Main(string[] args){
            GetAllProducts();
        }

        static void GetAllProducts(){

            using (var connection = GetMySqlConnection() )
            {
                try
                {
                    connection.Open();
                    string sql = "select * from Products";

                    MySqlCommand command = new MySqlCommand(sql,connection);

                    MySqlDataReader reader =  command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"name: {reader[3]} price:  {reader[6]} ");
                    }
                    reader.Close();
                    

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





        static MySqlConnection GetMySqlConnection(){

            string  connectionString = @"server=localhost;port=3306;database=northwind;user=root;password=mysql123;";
            // DRIVER PROVIDER
            return new MySqlConnection(connectionString); 
            



        }


        
    }
}