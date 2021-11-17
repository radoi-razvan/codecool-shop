using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using Codecool.CodecoolShop.Models;
using System;

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
                                INNER JOIN supplier s on s.id = p.supplier_id";
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

        public List<Product> GetProductsBySupplier(int supplierId)
        {
            List<Product> productList = new List<Product>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT p.id, p.name, p.defaultprice, p.description, p.currency,
                                c.id c_id, c.name c_name, c.department c_department, c.description c_description,
                                s.id s_id, s.name s_name, s.description s_description
                                FROM product p 
                                INNER JOIN category c on c.id = p.category_id
                                INNER JOIN supplier s on s.id = p.supplier_id
                                WHERE s.id = @supplierId";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@supplierId", supplierId);
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

        public List<Product> GetProductsByCategory(int categoryId)
        {
            List<Product> productList = new List<Product>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT p.id, p.name, p.defaultprice, p.description, p.currency,
                                c.id c_id, c.name c_name, c.department c_department, c.description c_description,
                                s.id s_id, s.name s_name, s.description s_description
                                FROM product p 
                                INNER JOIN category c on c.id = p.category_id
                                INNER JOIN supplier s on s.id = p.supplier_id
                                WHERE c.id = @categoryId";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@categoryId", categoryId);
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

        public List<Product> GetProductsInCart(int cartId)
        {
            List<Product> productList = new List<Product>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT p.id, p.name, p.defaultprice, p.description, p.currency,
                                c.id c_id, c.name c_name, c.department c_department, c.description c_description,
                                s.id s_id, s.name s_name, s.description s_description, cp.quantity
                                FROM product p 
                                INNER JOIN category c on c.id = p.category_id
                                INNER JOIN supplier s on s.id = p.supplier_id
                                INNER JOIN cart_product cp on cp.product_id = p.id
                                WHERE cp.cart_id = @cartId";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@cartId", cartId);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newProduct = new Product();
                    var newCategory = new ProductCategory();
                    var newSupplier = new Supplier();
                    var newCartProduct = new CartProduct();

                    newCategory.Id = (int)sqlDataReader["c_id"];
                    newCategory.Name = (string)sqlDataReader["c_name"];
                    newCategory.Description = (string)sqlDataReader["c_description"];
                    newCategory.Department = (string)sqlDataReader["c_department"];

                    newSupplier.Id = (int)sqlDataReader["s_id"];
                    newSupplier.Name = (string)sqlDataReader["s_name"];
                    newSupplier.Description = (string)sqlDataReader["s_description"];

                    newCartProduct.Quantity = (int)sqlDataReader["quantity"];

                    newProduct.Id = (int)sqlDataReader["id"];
                    newProduct.Name = (string)sqlDataReader["name"];
                    newProduct.Description = (string)sqlDataReader["description"];
                    newProduct.DefaultPrice = (decimal)sqlDataReader["defaultprice"];
                    newProduct.Currency = (string)sqlDataReader["currency"];
                    newProduct.ProductCategory = newCategory;
                    newProduct.Supplier = newSupplier;
                    newProduct.CartProduct = newCartProduct;

                    productList.Add(newProduct);
                }
            }

            connection.Close();
            return productList;
        }

        public void AddProductToCart(int productId, int cartId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQueryCheck = @"SELECT cart_id, product_id, quantity FROM cart_product WHERE cart_id = @cartId AND product_id = @productId";
            SqlCommand command1 = new SqlCommand(sqlQueryCheck, connection);
            command1.Parameters.AddWithValue("@cartId", cartId);
            command1.Parameters.AddWithValue("@productId", productId);
            connection.Open();
            SqlDataReader sqlDataReader = command1.ExecuteReader();
            bool productInCart = sqlDataReader.HasRows;
            connection.Close();

            if(productInCart)
            {
                string sqlQueryUpdate= @"UPDATE cart_product SET quantity = quantity + 1 WHERE cart_id = @cartId AND product_id = @productId";
                SqlCommand command2 = new SqlCommand(sqlQueryUpdate, connection);
                command2.Parameters.AddWithValue("@cartId", cartId);
                command2.Parameters.AddWithValue("@productId", productId);
                connection.Open();
                command2.ExecuteNonQuery();
                connection.Close();
            } 
            else
            {
                string sqlQueryInsert = @"INSERT INTO cart_product(cart_id, product_id, quantity) VALUES (@cartId, @productId, 1)";
                SqlCommand command3 = new SqlCommand(sqlQueryInsert, connection);
                command3.Parameters.AddWithValue("@cartId", cartId);
                command3.Parameters.AddWithValue("@productId", productId);
                connection.Open();
                command3.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void IncreaseProductQuantity(int productId, int cartId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"UPDATE cart_product SET quantity = quantity + 1 WHERE cart_id = @cartId AND product_id = @productId";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@cartId", cartId);
            command.Parameters.AddWithValue("@productId", productId);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void RemoveProductFromCart(int productId, int cartId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"UPDATE cart_product SET quantity = quantity - 1 WHERE cart_id = @cartId AND product_id = @productId";
            SqlCommand command1 = new SqlCommand(sqlQuery, connection);
            command1.Parameters.AddWithValue("@cartId", cartId);
            command1.Parameters.AddWithValue("@productId", productId);
            connection.Open();
            command1.ExecuteNonQuery();
            connection.Close();

            int currentQuantity = 1;
            string sqlQueryCheck = @"SELECT quantity FROM cart_product WHERE cart_id = @cartId AND product_id = @productId";
            SqlCommand command2 = new SqlCommand(sqlQueryCheck, connection);
            command2.Parameters.AddWithValue("@cartId", cartId);
            command2.Parameters.AddWithValue("@productId", productId);
            connection.Open();
            SqlDataReader sqlDataReader = command2.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    currentQuantity = (int)sqlDataReader["quantity"];
                }
            }
            connection.Close();

            if(currentQuantity <= 0)
            {
                DeleteProductFromCart(productId, cartId);
            }
        }

        public void DeleteProductFromCart(int productId, int cartId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"DELETE FROM cart_product WHERE @productId = product_id AND @cartId = cart_id";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@cartId", cartId);
            command.Parameters.AddWithValue("@productId", productId);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void ClearCart(int cartId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"DELETE FROM cart_product WHERE @cartId = cart_id";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@cartId", cartId);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }


        public List<ProductCategory> GetAllCategories()
        {
            List<ProductCategory> categories = new List<ProductCategory>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT * FROM category";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newCategory = new ProductCategory();

                    newCategory.Id = (int)sqlDataReader["id"];
                    newCategory.Name = (string)sqlDataReader["name"];
                    newCategory.Description = (string)sqlDataReader["description"];
                    newCategory.Department = (string)sqlDataReader["department"];

                    categories.Add(newCategory);
                }
            }

            connection.Close();
            return categories;
        }

        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>() { };
            SqlConnection connection = new SqlConnection(ConnectionString);
            string sqlQuery = @"SELECT * FROM supplier";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            connection.Open();
            SqlDataReader sqlDataReader = command.ExecuteReader();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    var newSupplier = new Supplier();

                    newSupplier.Id = (int)sqlDataReader["id"];
                    newSupplier.Name = (string)sqlDataReader["name"];
                    newSupplier.Description = (string)sqlDataReader["description"];

                    suppliers.Add(newSupplier);
                }
            }

            connection.Close();
            return suppliers;
        }
    }
}
