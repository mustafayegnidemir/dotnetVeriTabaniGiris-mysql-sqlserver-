﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace dotnetVeritabani
{
    public interface IProductDal
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        List<Product> Find(string productName);
        void Create(Product p);
        void Update(Product p);
        void Delete(int productId);
    }

    public class MySQLProductDal : IProductDal
    {
        private MySqlConnection GetMySqlConnection()
        {
            string  connectionString = @"server=localhost;port=3306;database=northwind;user=root;password=mysql123;    ";
            // DRIVER PROVIDER
            return new MySqlConnection(connectionString); 
        }

        public void Create(Product p)
        {
            throw new NotImplementedException();
        }

        public void Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products =   null;

            using (var connection = GetMySqlConnection() )
            {
                try
                {
                    connection.Open();
                    string sql = "select * from Products";

                    MySqlCommand command = new MySqlCommand(sql,connection);

                    MySqlDataReader reader =  command.ExecuteReader();

                    products = new List<Product>();

                    while (reader.Read())
                    {
                        products.Add(
                            new Product
                            {
                                ProductId = int.Parse(reader[1].ToString()),
                                Name = reader["product_name"].ToString(),
                                Price = double.Parse(reader["list_price"].ToString())
                            }
                        );
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

            return products;     
        }

        public Product GetProductById(int id)
        {
            Product product = null;

            using (var connection = GetMySqlConnection())
            {
                try
                {
                    connection.Open();

                    // sql injection
                    
                    string sql = "select * from Products where id=@productId ";

                    MySqlCommand command = new MySqlCommand(sql,connection);
                    command.Parameters.Add("@productId",MySqlDbType.Int32).Value = id; 

                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        product = new Product()
                        {
                            ProductId = int.Parse(reader["id"].ToString()),
                            Name = reader["product_name"].ToString(),
                            Price = double.Parse(reader["list_price"].ToString())
                        };
                           
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
            return product;

        }

        public void Update(Product p)
        {
            throw new NotImplementedException();
        }

        public List<Product> Find(string productName)
        {
            List<Product> products = null;

            using (var connection = GetMySqlConnection())
            {
                try
                {
                    connection.Open();

                    // sql injection
                    
                    string sql = "select * from Products where product_name like @productName";

                    MySqlCommand command = new MySqlCommand(sql,connection);
                    command.Parameters.Add("@productName",MySqlDbType.String).Value = "%"+productName+"%"; 

                    MySqlDataReader reader = command.ExecuteReader();

                    products = new List<Product>();

                    while (reader.Read())
                    {
                        products.Add(
                            new Product
                            {
                                ProductId = int.Parse(reader["id"].ToString()),
                                Name = reader["product_name"].ToString(),
                                Price = double.Parse(reader["list_price"]?.ToString())
                            }
                        );
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
            return products;

        }
    }

    public class MsSQLProductDal : IProductDal
    {
        private SqlConnection GetMsSqlConnection()
        {
            string  connectionString =  @"Data Source = .\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=SSPI;";
            // DRIVER PROVIDER
            return new SqlConnection(connectionString); 
        }

        public void Create(Product p)
        {
            throw new NotImplementedException();
        }

        public void Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = null;

            using (var connection = GetMsSqlConnection() )
            {
                try
                {
                    connection.Open();
                    string sql = "select * from Products";

                    SqlCommand command = new SqlCommand(sql,connection);

                    SqlDataReader reader =  command.ExecuteReader();

                    products = new List<Product>();

                    while (reader.Read())
                    {
                        products.Add(
                            new Product
                            {
                                ProductId = int.Parse(reader["ProductId"].ToString()),
                                Name = reader["ProductName"].ToString(),
                                Price = double.Parse(reader["UnitPrice"]?.ToString())
                            }
                        );
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

            return products;     
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product p)
        {
            throw new NotImplementedException();
        }

        public List<Product> Find(string productName)
        {
            throw new NotImplementedException();
        }
    }
    
    public class ProductManager : IProductDal
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal){
            _productDal = productDal;
        }
        public void Create(Product p)
        {
            throw new NotImplementedException();
        }

        public void Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public List<Product> Find(string productName)
        {
            return _productDal.Find(productName);
        }

        public List<Product> GetAllProducts()
        {
            return _productDal.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return _productDal.GetProductById(id);
        }

        public void Update(Product p)
        {
            throw new NotImplementedException();
        }
    }
    
    public class Program
    {
        static void Main(string[] args)
        {
            // var productDal = new MySQLProductDal();
            //  var productDal2 = new MsSQLProductDal();

            // var products = productDal.GetAllProducts();
            // var products = productDal2.GetAllProducts();

            var productDal = new ProductManager(new MySQLProductDal());  //injection işlemi
            // var products = productDal.GetAllProducts();

            // var product = productDal.GetProductById(4);
            var products = productDal.Find("Northwind");

            // Console.WriteLine($"{product.Name}");

            foreach (var pr in products)
            {
                Console.WriteLine($"Id: {pr.ProductId} name: {pr.Name} price: {pr.Price}");
            }
        }

    }
}