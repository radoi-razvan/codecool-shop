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
        public ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger, IConfiguration config)
        {
            _logger = logger;
            configuration = config;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance(),
                CartDaoMemory.GetInstance());
        }

        private void GetViewData()
        {
            ViewBag.Categories = ProductService.GetAllCategories().ToList();
            ViewBag.Suppliers = ProductService.GetAllSuppliers().ToList();
            ViewBag.ShoppingCart = ProductService.GetCart().Products;
            ViewBag.ShoppingCartTotal = 0;
            ViewBag.ItemsNumber = ProductService.GetCart().Products.Values.Sum();
        }

        [Route("/get-products")]
        public string GetAllProducts()
        {
            DataManager dm = new DataManager(configuration);
            var products = dm.GetAllProducts();
            return JsonSerializer.Serialize(products);
        }

        public IActionResult Index()
        {
            GetViewData();
            return View("Index");
        }

        public IActionResult Checkout()
        {
            GetViewData();
            return View("Checkout");
        }

        public IActionResult Payment()
        {
            GetViewData();
            return View("Payment");
        }

        [Route("/get-products/{categoryIndex}")]
        public string IndexByCategory(int categoryIndex)
        {
            var products = ProductService.GetProductsForCategory(categoryIndex).ToList();
            HttpContext.Session.Set("ProductList", products);
            GetViewData();
            
            return JsonSerializer.Serialize(products);
        }

        [Route("/get-products/{supplierIndex}")]
        public IActionResult IndexBySupplier(int supplierIndex)
        {
            var products = ProductService.GetProductsForSupplier(supplierIndex).ToList();
            HttpContext.Session.Set("ProductList", products);
            GetViewData();
            return View("Index", products);
        }

        [Route("Cart/Add/{productId}")]
        public string AddToCart(int productId)
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Add(products[productId - 1]);
            GetViewData();

            return GenerateCartAsJasonObject();
        }

        [Route("Cart/Remove/{productId}")]
        public string RemoveFromCartCustomRoute(int productId)
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Remove(productId);
            GetViewData();

            return GenerateCartAsJasonObject();
        }

        [Route("Cart/Delete/{productId}")]
        public string DeleteFromCartCustomRoute(int productId)
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Delete(productId);
            GetViewData();

            return GenerateCartAsJasonObject();
        }

        [Route("Cart/Clear")]
        public string ClearCartCustomRoute()
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Clear();
            GetViewData();

            return GenerateCartAsJasonObject();
        }

        public IActionResult Register()
        {
            GetViewData();
            return View("Register");
        }

        public IActionResult Login()
        {
            GetViewData();
            return View("Login");
        }

        public IActionResult Privacy()
        {
            GetViewData();
            return View();
        }

        public string GenerateCartAsJasonObject()
        {

            var cartProducts = ProductService.GetCart().Products;
            var productsList = new List<Dictionary<string, dynamic>>();
            var cartDetailsDict = new Dictionary<string, dynamic>();
            // cartDetailsDict.Add("TotalPrice", GetCartTotalPrice(cartProducts));
            cartDetailsDict.Add("TotalQuantity", cartProducts.Values.Sum());
            productsList.Add(cartDetailsDict);

            foreach (KeyValuePair<Product, int> keyValuePair in cartProducts)
            {
                Dictionary<string, dynamic> productDict = new Dictionary<string, dynamic>();
                productDict.Add("Name", $"{keyValuePair.Key.Name}");
                productDict.Add("Id", keyValuePair.Key.Id);
                productDict.Add("Quantity", keyValuePair.Value);
                productDict.Add("Price", keyValuePair.Key.DefaultPrice);
                productsList.Add(productDict);
            }

            return JsonSerializer.Serialize(productsList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
