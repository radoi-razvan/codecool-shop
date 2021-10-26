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
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
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
            ViewBag.ShoppingCartTotal = GetCartTotalPrice(ViewBag.ShoppingCart);
            ViewBag.ItemsNumber = ProductService.GetCart().Products.Values.Sum();
        }

        public IActionResult Index()
        {
            var products = ProductService.GetAllProducts().ToList();
            HttpContext.Session.Set("ProductList", products);
            GetViewData();
            return View(products);
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

        public IActionResult IndexByCategory(int categoryIndex)
        {
            var products = ProductService.GetProductsForCategory(categoryIndex).ToList();
            HttpContext.Session.Set("ProductList", products);
            GetViewData();
            return View("Index", products);
        }        
        public IActionResult IndexBySupplier(int supplierIndex)
        {
            var products = ProductService.GetProductsForSupplier(supplierIndex).ToList();
            HttpContext.Session.Set("ProductList", products);
            GetViewData();
            return View("Index", products);
        }

        //public async Task<IActionResult> Index()
        //{
        //    var products = await ProductService.GetAllProducts().ToList();
        //    HttpContext.Session.Set("ProductList", products);
        //    GetViewData();
        //    return View(products);
        //}

        public IActionResult AddToCart(int productId)
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Add(products[productId - 1]);
            GetViewData();
            
            return View("\\Views\\Product\\Index.cshtml", products);
        }

        [Route("Cart/Add/{productId}")]
        public string AddToCartCustomRoute(int productId)
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Add(products[productId - 1]);
            GetViewData();

            return GenerateCartAsJasonObject();
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Remove(productId);
            GetViewData();

            return View("\\Views\\Product\\Index.cshtml", products);
        }

        [Route("Cart/Remove/{productId}")]
        public string RemoveFromCartCustomRoute(int productId)
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Remove(productId);
            GetViewData();

            return GenerateCartAsJasonObject();
        }

        public IActionResult DeleteFromCart(int productId)
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Delete(productId);
            GetViewData();

            return View("\\Views\\Product\\Index.cshtml", products);
        }

        [Route("Cart/Delete/{productId}")]
        public string DeleteFromCartCustomRoute(int productId)
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Delete(productId);
            GetViewData();

            return GenerateCartAsJasonObject();
        }

        public IActionResult ClearCart()
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Clear();
            GetViewData();

            return View("\\Views\\Product\\Index.cshtml", products);
        }

        [Route("Cart/Clear")]
        public string ClearCartCustomRoute()
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Clear();
            GetViewData();

            return GenerateCartAsJasonObject();
        }

        public IActionResult Privacy()
        {
            GetViewData();
            return View();
        }

        public decimal GetCartTotalPrice(Dictionary<Product,int> cart)
        {
            decimal result = 0;
            foreach(var kv in cart)
            {
                result += kv.Key.DefaultPrice * kv.Value;
            }
            return result;
        }

        public string GenerateCartAsJasonObject() {

            var cartProducts = ProductService.GetCart().Products;
            var productsList = new List<Dictionary<string, dynamic>>();
            var cartDetailsDict = new Dictionary<string, dynamic>();
            cartDetailsDict.Add("TotalPrice", GetCartTotalPrice(cartProducts));
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
