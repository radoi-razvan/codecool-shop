using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Utils;

namespace Codecool.CodecoolShop.Controllers
{
    public class CartController : Controller
    {
        public Cart shoppingCart;
        public CartController()
        {
            shoppingCart = new Cart();
        }
        public IActionResult AddToCart(int productId)
        {
            var products = HttpContext.Session.GetObject<List<Product>>("ProductList");
            shoppingCart.Add(products[productId-1]);
            ViewBag.ShoppingCart = shoppingCart;
            return View("\\Views\\Product\\Index.cshtml", products);
        }
    }
}
