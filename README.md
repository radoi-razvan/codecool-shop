<div id="top"></div>

# Codecool Shop

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#main-features">Main Features</a></li>
        <li><a href="#integrated-services">Integrated Services</a></li>
        <li><a href="#built-with">Built With</a></li>
        <li><a href="#visuals">Visuals</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#development-team">Development Team</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

![main_page.jpg][main-page]

Codecool shop is an online shop where you can buy different types of products. Used technologies: ASP.NET Core, C#, SQL Server, HTML, CSS, Bootstrap, JavaScript. 

- The user can sort the products by Category or Supplier
- The user can register, login, add products to his cart (clear the cart, delete items from cart, increase or decrease the quantity of the item)
- The user can place an order, by using a credit card
- If the payment succeeded he will get a success response and a mail with the order details
- The user cand see his order history, paid orders but also orders that he checked out and he didn't pay, if he wants to pay them now he can

<p align="right">(<a href="#top">back to top</a>)</p>


### Main Features

- Sort Products by Category and Supplier
- Register
- Login/Logout
- Add to Cart
- Cart Preview
- Edit cart items quantity from the Cart Preview (Increase, Decrease, Remove Item, Clear Cart)
- Place order on the checkout page
- Pay for items on the payment page
- Receive an email with the order details
- Check his orders history and pay for the orders he checked out but didn't paid yet

<p align="right">(<a href="#top">back to top</a>)</p>

### Built With

Back End:
* [ASP .NET Core][asp-net-core]
* [Entity Framework Core][ef-core]
* [C#][c#]

Front End:
* [HTML][html]
* [CSS][css]
* [JavaScript][js]
* [React.js][react]
* [Bootstrap][bootstrap]

Database Management:
* [Microsoft SQL Server][msql-server]
* [Microsoft SQL Server Management Studio][ssms]

IDE:
* [Microsoft Visual Studio][visual-studio]

<p align="right">(<a href="#top">back to top</a>)</p>


### Integrated Services

Email:
* [Sendgrid][sendgrid]

Payment processing:
* [Stripe][stripe]

<p align="right">(<a href="#top">back to top</a>)</p>

### Visuals

Register Page:

![register.jpg][register]

Login Page:

![login.jpg][login]

Sort by Category:

![sort_by_category.jpg][sort-by-category]

Sort by Supplier:

![sort_by_supplier.jpg][sort-by-supplier]

Cart:

![cart.jpg][cart]

Checkout:

![checkout.jpg][checkout]

Payment Page:

![payment_page.jpg][payment-page]

Payment Form:

![payment_form.jpg][payment-form]

Payment Succeeded:

![payment_succeeded.jpg][payment-succeeded]

Order Confirmation Mail:

![order_confirmation_mail.jpg][order-confirmation-mail]

Orders History:

![orders_history.jpg][orders-history]

<p align="right">(<a href="#top">back to top</a>)</p>


<!-- GETTING STARTED -->
## Getting Started

### Installation

- Create a database
- Fill in Stripe - SecretKey here in the environment variables:
![set_sendgrid_API_key.jpg][set-sendgrid-API-key]
- Fill in the Sendgrid - ApiKey and ConnectionStrings in appsettings.js `WARNING this is bad practice because if you commit your code on github the keys will be exposed, the better way is to set them in environment variables like the stripe key`:
    
    ```json
    {
      "ConnectionStrings": {
        "ShopConnection": "<your-database-connection-string-comes-here>",
      },
      "Sendgrid": {
        "SecretKey": "<your-sendgrid-secret-api-key-comes-here>",
        "PublishableKey": "<your-sendgrid-publishable-api-key-comes-here>"
      }
    }
    ```
    
- Fill in other details related to Sendgrid and Stripe according to your data
- Update the database from the Package Manager Console

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- USAGE EXAMPLES -->
## Usage

Run the project with IIS Express.

<p align="right">(<a href="#top">back to top</a>)</p>


## Development Team

* [Radoi Razvan's GitHub][radoi-razvan]
* [Alex Moraru's GitHub][AlexMoraru97]

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->
[contributors-shield]: https://img.shields.io/github/contributors/othneildrew/Best-README-Template.svg?style=for-the-badge
[contributors-url]: https://github.com/mihaibuga/online-shop/graphs/contributors
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/mihai-buga

[asp-net-core]: https://dotnet.microsoft.com/en-us/learn/aspnet/what-is-aspnet-core
[ef-core]: https://docs.microsoft.com/en-us/ef/core/
[c#]: https://docs.microsoft.com/en-us/dotnet/csharp/
[html]: https://html.com/
[css]: https://www.w3.org/Style/CSS/Overview.en.html
[js]: https://www.javascript.com/
[react]: https://reactjs.org/
[bootstrap]: https://getbootstrap.com
[msql-server]: https://www.microsoft.com/en-us/sql-server/sql-server-2019
[ssms]: https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15
[visual-studio]: https://visualstudio.microsoft.com/

[radoi-razvan]: https://github.com/radoi-razvan
[AlexMoraru97]: https://github.com/AlexMoraru97

[sendgrid]: https://sendgrid.com/
[stripe]: https://stripe.com/

[main-page]: assets/main_page.jpg
[register]: assets/register.jpg
[login]: assets/login.jpg
[sort-by-category]: assets/sort_by_category.jpg
[sort-by-supplier]: assets/sort_by_supplier.jpg
[cart]: assets/cart.jpg
[checkout]: assets/checkout.jpg
[payment-page]: assets/payment_page.jpg
[payment-form]: assets/payment_form.jpg
[payment-succeeded]: assets/payment_succeeded.jpg
[order-confirmation-mail]: assets/order_confirmation_mail.jpg
[orders-history]: assets/orders_history.jpg
[set-sendgrid-API-key]: assets/set_sendgrid_API_key.jpg

