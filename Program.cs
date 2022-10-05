using System;
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
        int Count();
        int Create(Product p);
        int Update(Product p);
        int Delete(int productId);
    }

    public class MySQLProductDal : IProductDal
    {
        private MySqlConnection GetMySqlConnection()
        {
            string  connectionString = @"server=localhost;port=3306;database=northwind;user=root;password=mysql123;    ";
            // DRIVER PROVIDER
            return new MySqlConnection(connectionString); 
        }

        public int Create(Product p)
        {
            throw new NotImplementedException();
        }

        public int Delete(int productId)
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

        public int Update(Product p)
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

        public int Count()
        {
            int count = 0;

            using (var connection = GetMySqlConnection())
            {
                try
                {
                    connection.Open();

                    // sql injection
                    
                    string sql = "select count(*) from products";

                    MySqlCommand command = new MySqlCommand(sql,connection);
                    object result =  command.ExecuteScalar();

                    if (result!=null)
                    {
                        count = Convert.ToInt32(result);    
                    }
                    
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
            return count;

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

        public int Create(Product p)
        {
            int result = 0;
            using (var connection = GetMsSqlConnection())
            {
                try
                {
                    connection.Open();
                    // sql injection
                    
                    string sql = "INSERT INTO Products(ProductName,UnitPrice, Discontinued) VALUES (@productname,@unitprice,@discontinued) ";

                    SqlCommand command = new SqlCommand(sql,connection);

                    command.Parameters.AddWithValue("@productname", p.Name);
                    command.Parameters.AddWithValue("@unitprice", p.Price);
                    command.Parameters.AddWithValue("@discontinued", 1);

                    result = command.ExecuteNonQuery();

                    Console.WriteLine($"{result} adet kayıt eklendi");

                    
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);                    
                }
                finally
                {
                    connection.Close();
                }
                return result;
            }
            
        }

        public int Delete(int productId)
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
            Product product = null;

            using (var connection = GetMsSqlConnection())
            {
                try
                {
                    connection.Open();

                    // sql injection
                    
                    string sql = "select * from Products where ProductId=@productid ";

                    SqlCommand command = new SqlCommand(sql,connection);
                    command.Parameters.AddWithValue("@productid",id); 

                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        product = new Product()
                        {
                            ProductId = int.Parse(reader["ProductId"].ToString()),
                            Name = reader["ProductName"].ToString(),
                            Price = double.Parse(reader["UnitPrice"].ToString())
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

        public int Update(Product p)
        {
            int result = 0;
            using (var connection = GetMsSqlConnection())
            {
                try
                {
                    connection.Open();
                    // sql injection
                    
                    string sql = "UPDATE Products SET ProductName=@productname, UnitPrice=@unitprice WHERE ProductId=@productid ";

                    SqlCommand command = new SqlCommand(sql,connection);

                    command.Parameters.AddWithValue("@productname", p.Name);
                    command.Parameters.AddWithValue("@unitprice", p.Price);
                    command.Parameters.AddWithValue("@productid", p.ProductId);

                    result = command.ExecuteNonQuery();

                    Console.WriteLine($"{result} adet kayıt yeniden düzenlendi.");

                    
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.Message);                    
                }
                finally
                {
                    connection.Close();
                }
                return result;
            }
        }

        public List<Product> Find(string ProductName)
        {
            List<Product> products = null;

            using (var connection = GetMsSqlConnection())
            {
                try
                {
                    connection.Open();

                    // sql injection
                    
                    string sql = "select * from Products where ProductName like @productName";

                    SqlCommand command = new SqlCommand(sql,connection);
                    command.Parameters.AddWithValue("@ProductName" ,"%"+ProductName+"%"); 

                    SqlDataReader reader = command.ExecuteReader();

                    products = new List<Product>();

                    while (reader.Read())
                    {
                        products.Add(
                            new Product
                            {
                                ProductId = int.Parse(reader["ProductID"].ToString()),
                                Name = reader["ProductName"].ToString(),
                                Price = double.Parse(reader["UnitPrice"]?.ToString())
                            }
                        );
                        Console.WriteLine($"name: {reader[1]} price:  {reader[5]} ");
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

        public int Count()
        {
            int count = 0;

            using (var connection = GetMsSqlConnection())
            {
                try
                {
                    connection.Open();

                    // sql injection
                    
                    string sql = "select count(*) from products";

                    SqlCommand command = new SqlCommand(sql,connection);
                    object result =  command.ExecuteScalar();

                    if (result!=null)
                    {
                        count = Convert.ToInt32(result);    
                    }
                    
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
            return count;
        }
    }
    
    public class ProductManager : IProductDal
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal){
            _productDal = productDal;
        }

        public int Count()
        {
            return _productDal.Count();
        }

        public int Create(Product p)
        {
            return _productDal.Create(p);
        }

        public int Delete(int productId)
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

        public int Update(Product p)
        {
            return _productDal.Update(p);
        }
    }
    
    public class Program
    {
        static void Main(string[] args)
        {

            // Ürün Ekleme
            var productDal = new ProductManager(new MsSQLProductDal());  //injection işlemi
            var p = new Product();
            Console.WriteLine("Ürün Ekleme İşlemine Hoşgeldiniz.");
            Console.WriteLine("Ürün Adı giriniz.");
            p.Name = Console.ReadLine();
            Console.WriteLine("Ürün Fiyatı giriniz.");
            p.Price = Convert.ToDouble(Console.ReadLine());

            int count = productDal.Create(p);
            Console.WriteLine($"Total products: {count}");

            //Ürün Güncelleme
            var p2 = productDal.GetProductById(79);
            Console.WriteLine("Ürün Güncelleme İşlemine Hoşgeldiniz.");
            Console.WriteLine("79. nolu ürünün Adını değiştirin.");
            p2.Name = Console.ReadLine();
            Console.WriteLine("79. nolu ürünün Fiyatını değiştirin.");
            p2.Price = Convert.ToDouble(Console.ReadLine());

            int count2 = productDal.Update(p2);
            Console.WriteLine($"Total updated products : {count2}");




 
        }

    }
}