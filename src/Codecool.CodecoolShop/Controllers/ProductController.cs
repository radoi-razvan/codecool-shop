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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Stripe;
using System;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IConfiguration configuration;
        private DataManager dataManager;
        private readonly UserManager<IdentityUser> _userManager;

        public Services.ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger, IConfiguration config, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            configuration = config;
            dataManager = new DataManager(configuration);

            ProductService = new Services.ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance(),
                CartDaoMemory.GetInstance());

            _userManager = userManager;
        }

        [AllowAnonymous]
        [Route("/get-products")]
        public string GetAllProducts()
        {
            return JsonSerializer.Serialize(dataManager.GetAllProducts());
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View("Index");
        }

        [Authorize]
        public IActionResult Checkout()
        {
            return View("Checkout");
        }

        [Authorize]
        [HttpPost]
        public async void SendgridEmailSubmit(Models.Order order)
        {
            ViewData["Message"] = "Email Sent!!!...";
            Emailmodel emailmodel = new Emailmodel();
            //string userId = User.FindFirstValue(ClaimTypes.Email);
            emailmodel.From = "codecoolshopofficial@gmail.com";
            emailmodel.To = User.FindFirstValue(ClaimTypes.Email);
            emailmodel.Subject = "Order Confirmation";
            emailmodel.Body = $"Greetings {User.FindFirstValue(ClaimTypes.Name)},\n" +
                $"we are happy to announce that we received your payment for the following products:\n" +
                $"{order.OrderProducts} \n";
            MailConfirmationManager emailexample = new MailConfirmationManager();
            await emailexample.Execute(emailmodel.From, emailmodel.To, emailmodel.Subject, emailmodel.Body
                , emailmodel.Body);
        }

        [Authorize]
        public IActionResult Payment()
        {
            // TODO payment logic and links, stripe for Payment and SendGrid for email sending
            // identity user db + check if logged in and get his id
            return View("Payment");
        }

        [Authorize]
        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = dataManager.GetClientOrderDetails(userId);
            var customers = new CustomerService();
            var charges = new ChargeService();
            var customer = customers.Create(new CustomerCreateOptions { 
                Email = stripeEmail,
                Source = stripeToken
            });
            var charge = charges.Create(new ChargeCreateOptions { 
                Amount = Convert.ToInt64(dataManager.GetOrderTotalByOrderId(order.Id)),
                Currency = "usd",
                Customer = customer.Id,
            });

            string message;
            if (charge.Status == "succeeded")
            {
                dataManager.SetOrderStatus(order.Id, "paid");
                string BalanceTransactionId = charge.BalanceTransactionId;
                message = "Payment Succeeded";
                SendgridEmailSubmit(order);
                return View("PaymentResponse", message);
            }
            message = "Payment Failed";
            return View("PaymentResponse", message);
        }

        [Authorize]
        public IActionResult Order()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = dataManager.GetOrders(userId);
            return View("Order", orders);
        }

        [Authorize]
        public void OnPost(Models.Order order)
        {
            Models.Order currentOrder = order;
            string firstName = currentOrder.FirstName;
            string lastName = currentOrder.LastName;
            string clientEmail = currentOrder.ClientEmail;
            string clientAddress = currentOrder.ClientAddress;
            string phoneNumber = currentOrder.PhoneNumber;
            string country = currentOrder.Country;
            string city = currentOrder.City;
            string zipCode = currentOrder.ZipCode;

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dataManager.CreateOrder(userId, firstName, lastName, clientEmail,
                clientAddress, phoneNumber, country, city, zipCode);
            Response.Redirect("Payment");
        }

        [AllowAnonymous]
        [Route("api/get-categories")]
        public string GetCategories()
        {
            return JsonSerializer.Serialize(dataManager.GetAllCategories());
        }

        [AllowAnonymous]
        [Route("api/get-suppliers")]
        public string GetSuppliers()
        {
            return JsonSerializer.Serialize(dataManager.GetAllSuppliers());
        }

        [AllowAnonymous]
        [Route("/get-products/category/{categoryIndex}")]
        public string IndexByCategory(int categoryIndex)
        {
            return JsonSerializer.Serialize(dataManager.GetProductsByCategory(categoryIndex));
        }

        [AllowAnonymous]
        [Route("/get-products/supplier/{supplierIndex}")]
        public string IndexBySupplier(int supplierIndex)
        {
            return JsonSerializer.Serialize(dataManager.GetProductsBySupplier(supplierIndex));
        }

        [Authorize]
        [Route("api/get-cart-products")]
        public string GetCartProducts()
        {
            // TODO
            // and related FK
            // change database user.Id if you used automted Identity to PK, nvarchar(450) not null
            // and GetProductsInCart(string userId)
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // dataManager.RegisterUserCart(userId);
            return JsonSerializer.Serialize(dataManager.GetProductsInCart(userId));
        }

        [Authorize]
        [Route("cart/add/{productId}")]
        public void AddToCart(int productId)
        {
            // TODO
            // change database user.Id if you used automted Identity to PK, nvarchar(450) not null
            // and related FK
            // and AddProductToCart(productId, string userId)
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dataManager.AddProductToCart(productId, userId);
        }

        [Authorize]
        [Route("cart/increase/{productId}")]
        public void IncreaseProductQuantityInCart(int productId)
        {
            // TODO
            // change database user.Id if you used automted Identity to PK, nvarchar(450) not null
            // and related FK
            // and IncreaseProductQuantity(productId, string userId)
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dataManager.IncreaseProductQuantity(productId, userId);
        }

        [Authorize]
        [Route("cart/remove/{productId}")]
        public void RemoveFromCart(int productId)
        {
            // TODO
            // change database user.Id if you used automted Identity to PK, nvarchar(450) not null
            // and related FK
            // and RemoveProductFromCart(productId, string userId)
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dataManager.RemoveProductFromCart(productId, userId);
        }

        [Authorize]
        [Route("cart/delete/{productId}")]
        public void DeleteFromCart(int productId)
        {
            // TODO
            // change database user.Id if you used automted Identity to PK, nvarchar(450) not null
            // and related FK
            // and DeleteProductFromCart(productId, string userId)
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dataManager.DeleteProductFromCart(productId, userId);
        }

        [Authorize]
        [Route("cart/clear")]
        public void ClearCart()
        {
            // TODO
            // change database user.Id if you used automted Identity to PK, nvarchar(450) not null
            // and related FK
            // and ClearCart(string userId)
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dataManager.ClearCart(userId);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
