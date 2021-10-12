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

        //public IActionResult Index()
        //{
        //    var products= ProductService.GetAllProducts();
        //    return View(products.ToList());
        //}
        public IActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.Products = ProductService.GetAllProducts().ToList();
            ViewBag.Categories = ProductService.GetAllCategories().ToList();
            ViewBag.Suppliers = ProductService.GetAllSuppliers().ToList();
            return View();
        }
        public IActionResult IndexByCategory(int categoryIndex)
            {
                ViewBag.Products= ProductService.GetProductsForCategory(categoryIndex).ToList();
                ViewBag.Categories = ProductService.GetAllCategories().ToList();
                ViewBag.Suppliers = ProductService.GetAllSuppliers().ToList();
                return View("Index");
            }        
        public IActionResult IndexBySupplier(int supplierIndex)
        {
            ViewBag.Products = ProductService.GetProductsForSupplier(supplierIndex).ToList();
            ViewBag.Categories = ProductService.GetAllCategories().ToList();
            ViewBag.Suppliers = ProductService.GetAllSuppliers().ToList();
            return View("Index");
        }

        public IActionResult Privacy()
        {
            ViewBag.Categories = ProductService.GetAllCategories().ToList();
            ViewBag.Suppliers = ProductService.GetAllSuppliers().ToList();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
