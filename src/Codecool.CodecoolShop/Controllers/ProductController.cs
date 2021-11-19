using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;
using Microsoft.AspNetCore.Http;
using Codecool.CodecoolShop.Utils;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IConfiguration configuration;
        private DataManager dataManager;

        public ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger, IConfiguration config)
        {
            _logger = logger;
            configuration = config;
            dataManager = new DataManager(configuration);

            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance(),
                CartDaoMemory.GetInstance());
        }

        [Route("/get-products")]
        public string GetAllProducts()
        {
            return JsonSerializer.Serialize(dataManager.GetAllProducts());
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Checkout()
        {
            return View("Checkout");
        }

        public IActionResult Payment()
        {
            return View("Payment");
        }

        public IActionResult Order()
        {   
            var orders = dataManager.GetOrders(1);
            return View("Order", orders);
        }

        public void OnPost(Order order)
        {
            Order currentOrder = order;
            string firstName = currentOrder.FirstName;
            string lastName = currentOrder.LastName;
            string clientEmail = currentOrder.ClientEmail;
            string clientAddress = currentOrder.ClientAddress;
            string phoneNumber = currentOrder.PhoneNumber;
            string country = currentOrder.Country;
            string city = currentOrder.City;
            string zipCode = currentOrder.ZipCode;           
            
            dataManager.CreateOrder(1, firstName, lastName, clientEmail,
                clientAddress, phoneNumber, country, city, zipCode);
            Response.Redirect("Payment");
        }

        [Route("api/get-categories")]
        public string GetCategories()
        {
            return JsonSerializer.Serialize(dataManager.GetAllCategories());
        }

        [Route("api/get-suppliers")]
        public string GetSuppliers()
        {
            return JsonSerializer.Serialize(dataManager.GetAllSuppliers());
        }

        [Route("/get-products/category/{categoryIndex}")]
        public string IndexByCategory(int categoryIndex)
        {
            return JsonSerializer.Serialize(dataManager.GetProductsByCategory(categoryIndex));
        }

        [Route("/get-products/supplier/{supplierIndex}")]
        public string IndexBySupplier(int supplierIndex)
        {
            return JsonSerializer.Serialize(dataManager.GetProductsBySupplier(supplierIndex));
        }

        [Route("api/get-cart-products")]
        public string GetCartProducts()
        {
            // TODO
            // and related FK
            // change database user.Id if you used automted Identity to PK, nvarchar(450) not null
            // and GetProductsInCart(string userId)
            return JsonSerializer.Serialize(dataManager.GetProductsInCart(1));
        }

        [Route("cart/add/{productId}")]
        public void AddToCart(int productId)
        {
            // TODO
            // change database user.Id if you used automted Identity to PK, nvarchar(450) not null
            // and related FK
            // and AddProductToCart(productId, string userId)
            dataManager.AddProductToCart(productId, 1);
        }

        [Route("cart/increase/{productId}")]
        public void IncreaseProductQuantityInCart(int productId)
        {
            // TODO
            // change database user.Id if you used automted Identity to PK, nvarchar(450) not null
            // and related FK
            // and IncreaseProductQuantity(productId, string userId)
            dataManager.IncreaseProductQuantity(productId, 1);
        }

        [Route("cart/remove/{productId}")]
        public void RemoveFromCart(int productId)
        {
            // TODO
            // change database user.Id if you used automted Identity to PK, nvarchar(450) not null
            // and related FK
            // and RemoveProductFromCart(productId, string userId)
            dataManager.RemoveProductFromCart(productId, 1);
        }

        [Route("cart/delete/{productId}")]
        public void DeleteFromCart(int productId)
        {
            // TODO
            // change database user.Id if you used automted Identity to PK, nvarchar(450) not null
            // and related FK
            // and DeleteProductFromCart(productId, string userId)
            dataManager.DeleteProductFromCart(productId, 1);
        }

        [Route("cart/clear")]
        public void ClearCart()
        {
            // TODO
            // change database user.Id if you used automted Identity to PK, nvarchar(450) not null
            // and related FK
            // and ClearCart(string userId)
            dataManager.ClearCart(1);
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        //public string GenerateCartAsJasonObject() {

        //    var cartProducts = ProductService.GetCart().Products;
        //    var productsList = new List<Dictionary<string, dynamic>>();
        //    var cartDetailsDict = new Dictionary<string, dynamic>();
        //    cartDetailsDict.Add("TotalPrice", GetCartTotalPrice(cartProducts));
        //    cartDetailsDict.Add("TotalQuantity", cartProducts.Values.Sum());
        //    productsList.Add(cartDetailsDict);

        //    foreach (KeyValuePair<Product, int> keyValuePair in cartProducts)
        //    {
        //        Dictionary<string, dynamic> productDict = new Dictionary<string, dynamic>();
        //        productDict.Add("Name", $"{keyValuePair.Key.Name}");
        //        productDict.Add("Id", keyValuePair.Key.Id);
        //        productDict.Add("Quantity", keyValuePair.Value);
        //        productDict.Add("Price", keyValuePair.Key.DefaultPrice);
        //        productsList.Add(productDict);
        //    }

        //    return JsonSerializer.Serialize(productsList);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
