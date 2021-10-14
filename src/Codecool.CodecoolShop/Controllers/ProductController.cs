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
            ViewBag.ShoppingCartTotal = ProductService.GetCart().Products.Keys.Sum(p => p.DefaultPrice);
        }

        public IActionResult Index()
        {
            var products = ProductService.GetAllProducts().ToList();
            HttpContext.Session.Set("ProductList", products);
            GetViewData();
            return View(products);
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

        public IActionResult AddToCart(int productId)
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Add(products[productId - 1]);
            GetViewData();
            
            return View("\\Views\\Product\\Index.cshtml", products);
        }
        public IActionResult RemoveFromCart(int productId)
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Remove(productId);
            GetViewData();

            return View("\\Views\\Product\\Index.cshtml", products);
        }
        public IActionResult DeleteFromCart(int productId)
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Delete(productId);
            GetViewData();

            return View("\\Views\\Product\\Index.cshtml", products);
        }

        public IActionResult ClearCart()
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            ProductService.GetCart().Clear();
            GetViewData();

            return View("\\Views\\Product\\Index.cshtml", products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
