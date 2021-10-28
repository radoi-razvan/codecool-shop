using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class DataManager
    {
        private readonly IConfiguration configuration;
        private string ConnectionString => configuration.GetConnectionString("ShopConnection");
        public DataManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT p.id, p.name, p.defaultprice, p.description, p.currency,
                                c.id c_id, c.name c_name, c.department c_department, c.description c_description,
                                s.id s_id, s.name s_name, s.description s_description
                                FROM product p 
                                INNER JOIN category c on c.id = p.category_id
                                INNER JOIN supplier s on s.id = p.category_id";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newProduct = new Product();
                    var newCategory = new ProductCategory();
                    var newSupplier = new Supplier();

                    newCategory.Id = (int)sqlDataReader["c_id"];
                    newCategory.Name = (string)sqlDataReader["c_name"];
                    newCategory.Description = (string)sqlDataReader["c_description"];
                    newCategory.Department = (string)sqlDataReader["c_department"];

                    newSupplier.Id = (int)sqlDataReader["s_id"];
                    newSupplier.Name = (string)sqlDataReader["s_name"];
                    newSupplier.Description = (string)sqlDataReader["s_description"];
                    
                    newProduct.Id = (int)sqlDataReader["id"];
                    newProduct.Name = (string)sqlDataReader["name"];
                    newProduct.Description = (string)sqlDataReader["description"];
                    newProduct.DefaultPrice = (decimal)sqlDataReader["defaultprice"];
                    newProduct.Currency = (string)sqlDataReader["currency"];
                    newProduct.ProductCategory = newCategory;
                    newProduct.Supplier = newSupplier;

                    productList.Add(newProduct);
                }
            }

            connection.Close();
            return productList;
        }
    }
}
