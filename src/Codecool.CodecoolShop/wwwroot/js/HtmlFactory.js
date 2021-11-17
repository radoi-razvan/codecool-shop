export const htmlFactory = {
  cartItemBuilder: function (product) {
    return `<div id="cartitem${product.Id}" class="Cart-Items-cart">
                <div class="image-box-cart">
                    <img src="https://localhost:44368/img/${
                      product.Name
                    }.jpg" class="cart-img" />
                </div>
                <div class="about-cart centered">
                    <h1 class="title-cart text-md-center">${product.Name}</h1>
                </div>
                <div class="counter-cart">
                        <i class="bi bi-plus-circle text-dark product-increase" data-increase="increase-${
                          product.Id
                        }" style="font-size: 20px; cursor: pointer;"></i>
                    <div class="count-cart">${
                      product.CartProduct.Quantity
                    }</div>
                        <i class="bi bi-dash-circle text-dark product-remove" data-remove="remove-${
                          product.Id
                        }" style="font-size: 20px; cursor: pointer;"></i>
                </div>
                <div class="prices-cart">
                    <div class="amount-cart">$${parseFloat(
                      product.DefaultPrice
                    ).toFixed(2)}</div>
                    <div class="remove-cart">
                            <i class="bi bi-x-circle text-danger product-delete"  data-delete="delete-${
                              product.Id
                            }"></i>
                    </div>
                </div>
            </div>
            <hr class="bg-dark" />`;
  },

  cartTotalBuilder: function (productSum) {
    return `<div class="checkout-cart">
                <div class="total-cart">
                    <div>
                        <div class="Subtotal-cart">Sub-Total</div>
                    </div>
                    <div class="total-amount-cart me-3" id="cart-total-container">$${productSum}</div>
                </div>
                <a class="btn btn-warning" style="font-weight: bold" href="/Product/Checkout">Checkout</a>
            </div>`;
  },

  cardBuilder: function (product) {
    return `<div id="product${
      product.Id
    }" class="col-lg-3 col-lg-3 card-container" style="max-width: 350px;">
                <div class="card">
                    <img src="https://localhost:44368/img/${
                      product.Name
                    }.jpg" style="height: 50%; width: 50%; align-self: center; padding-top: 10px">
                    <div class="card-body">
                        <h5 class="card-title">${product.Name}</h5>
                        <p class="card-text card-description">${product.Description}.</p>
                        <p class="card-text">Category: ${
                          product.ProductCategory.Department
                        }</p>
                        <p class="card-text">Supplier: ${
                          product.Supplier.Name
                        }</p>
                        <p class="card-text text-center"><strong>Price: $${parseFloat(
                          product.DefaultPrice
                        ).toFixed(2)}</strong></p>
                        <a type="button" class="btn btn-warning add-cart-btn" data-product="dataProduct-${
                          product.Id
                        }" style="font-weight: bold">
                            <i class="bi bi-cart-plus" style="font-size: 20px"></i> Add To Cart
                        </a>
                    </div>
                </div>
            </div>`;
  },

  dropDownItemBuilder: function (element, section) {
    if (section === "category") {
      return `<a id="${section}${element.Id}" class="dropdown-item" data-id="${element.Id}" href="/get-products/${section}/${element.Id}"> ${element.Name}s </a>`;
    } else {
      return `<a id="${section}${element.Id}" class="dropdown-item" data-id="${element.Id}" href="/get-products/${section}/${element.Id}"> ${element.Name} </a>`;
    }
  },
};
