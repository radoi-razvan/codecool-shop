using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;

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
                SupplierDaoMemory.GetInstance());
        }

        private void GetCategoriesAndSuppliers()
        {
            ViewBag.Categories = ProductService.GetAllCategories().ToList();
            ViewBag.Suppliers = ProductService.GetAllSuppliers().ToList();
        }

        public IActionResult Index()
        {
            // var session = HttpContext.Session;
            // Session["myKey"]   
            var products = ProductService.GetAllProducts().ToList();
            GetCategoriesAndSuppliers();
            return View(products);
        }
        public IActionResult IndexByCategory(int categoryIndex)
            {
                var products = ProductService.GetProductsForCategory(categoryIndex).ToList();
                GetCategoriesAndSuppliers();
                return View("Index", products);
            }        
        public IActionResult IndexBySupplier(int supplierIndex)
        {
            var products = ProductService.GetProductsForSupplier(supplierIndex).ToList();
            GetCategoriesAndSuppliers();
            return View("Index", products);
        }

        public IActionResult Privacy()
        {
            GetCategoriesAndSuppliers();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
