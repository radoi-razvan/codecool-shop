export const htmlFactory = {
    cartItemBuilder: function (product) {
        return `<div id="cartitem${product.Id}" class="Cart-Items-cart">
                <div class="image-box-cart">
                    <img src="https://localhost:44368/img/@(keyValuePair.Key.Name).jpg" class="cart-img" />
                </div>
                <div class="about-cart centered">
                    <h1 class="title-cart text-md-center">${product.Name}</h1>
                </div>
                <div class="counter-cart">
                    <a asp-area="" asp-controller="Product" asp-action="AddToCart" asp-route-productId="${product.Id}">
                        <i class="bi bi-plus-circle text-dark" style="font-size: 20px"></i>
                    </a>
                    <div class="count-cart">${product}</div>
                    <a asp-area="" asp-controller="Product" asp-action="DeleteFromCart" asp-route-productId="${product.Id}">
                        <i class="bi bi-dash-circle text-dark" style="font-size: 20px"></i>
                    </a>
                </div>
                <div class="prices-cart">
                    <div class="amount-cart">${product}</div>
                    <div class="remove-cart">
                        <a asp-area="" asp-controller="Product" asp-action="RemoveFromCart" asp-route-productId="${product.Id}">
                            <i class="bi bi-x-circle text-danger"></i>
                        </a>
                    </div>
                </div>
            </div>
            <hr class="bg-dark" />`;
    },

    cardBuilder: function (product) {
        return `<div id="product${product.Id}" class="col-lg-3 col-lg-3 card-container" style="max-width: 350px;">
                <div class="card">
                    <img src="https://localhost:44368/img/${product.Name}.jpg" style="height: 50%; width: 50%; align-self: center; padding-top: 10px">
                    <div class="card-body">
                        <h5 class="card-title">${product.Name}</h5>
                        <p class="card-text">${product.Description}.</p>
                        <p class="card-text">Category: ${product.ProductCategory.Department}</p>
                        <p class="card-text">Supplier: ${product.Supplier.Name}</p>
                        <p class="card-text text-center"><strong>Price: $${parseFloat(product.DefaultPrice).toFixed(2)}</strong></p>
                        <a type="button" class="btn btn-warning add-cart-btn" data-product="${product}" style="font-weight: bold" href="Cart/Add/${product.Id}">
                            <i class="bi bi-cart-plus" style="font-size: 20px"></i> Add To Cart
                        </a>
                    </div>
                </div>
            </div>`;
    },

    dropDownItemBuilder: function (element, section) {
        if (section === "category") {
            return `<a id="${section}${element.Id}" class="dropdown-item" data-id="${element.Id}" href="/get-products/${section}/${element.Id}"> ${element.Name}s </a>`
        } else {
            return `<a id="${section}${element.Id}" class="dropdown-item" data-id="${element.Id}" href="/get-products/${section}/${element.Id}"> ${element.Name} </a>`
        }
    },

}




