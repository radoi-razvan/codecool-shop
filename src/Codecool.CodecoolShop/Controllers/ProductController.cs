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
            return JsonSerializer.Serialize(dataManager.GetProductsInCart(0));
        }

        [Route("cart/add/{productId}")]
        public void AddToCart(int productId)
        {
            dataManager.AddProductToCart(productId, 0);
        }

        [Route("cart/remove/{productId}")]
        public void RemoveFromCart(int productId)
        {
            dataManager.RemoveProductFromCart(productId, 0);
        }

        [Route("cart/delete/{productId}")]
        public void DeleteFromCart(int productId)
        {
            dataManager.DeleteProductFromCart(productId, 0);
        }

        [Route("cart/clear")]
        public void ClearCart()
        {
            dataManager.ClearCart(0);
        }

        public IActionResult Privacy()
        {
            return View("Privacy");
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
