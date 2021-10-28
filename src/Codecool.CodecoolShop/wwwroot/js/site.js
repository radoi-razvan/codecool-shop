// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
import { productsManager } from "./ProductsManager.js";
import { layoutManager } from "./LayoutManager.js";

const init = () => {

    layoutManager.loadLayoutElements();
    productsManager.loadProducts();

}

init();

//const addCartButtonsList = document.querySelectorAll('.add-cart-btn');
//addEventOnClick(addCartButtonsList);

    //const addCartButtonsList = document.querySelectorAll('.add-cart-btn');
    //for (let element of addCartButtonsList) {
    //    element.addEventListener('click', (event) => {
    //        const currentElement = event.currentTarget;
    //        const currentId = currentElement.dataset.id;
    //        const cartContainer = document.getElementById('cart-container');
    //        const customRoute = currentElement.getAttribute("href");
    //        getCart(currentId, cartContainer);
    //        event.preventDefault();
    //    });
    //}

//}

//const getCart = async function (cartContainer, customRoute) {
//    const response = await apiGet(customRoute);
//    /*const response = await apiGet(`Cart/Add/${currentId}`);*/
//    cartContainer.innerHTML = '';

//    for (let item of response) {
//        if ("TotalQuantity" in item) {
//            const cartQuantityContainer = document.getElementById('cart-quantity');
//            cartQuantityContainer.innerHTML = item.TotalQuantity;
//        }
//        else {
//            cartContainer.insertAdjacentHTML('beforeend', `
//            <div class="Cart-Items-cart">
//                    <div class="image-box-cart">
//                        <img src="https://localhost:44368/img/${item.Name}.jpg" class="cart-img" />
//                    </div>
//                    <div class="about-cart centered">
//                        <h1 class="title-cart text-md-center">${item.Name}</h1>
//                    </div>
//                    <div class="counter-cart">
//                        <a class="cart-plus-btn-int" href="Cart/Add/${item.Id}">
//                            <i class="bi bi-plus-circle text-dark" style="font-size: 20px"></i>
//                        </a>
//                        <div class="count-cart">${item.Quantity}</div>
//                        <a class="cart-minus-btn" href="Cart/Delete/${item.Id}">
//                            <i class="bi bi-dash-circle text-dark" style="font-size: 20px"></i>
//                        </a>
//                    </div>
//                    <div class="prices-cart">
//                        <div class="amount-cart">$${item.Price}</div>
//                        <div class="remove-cart">
//                            <a class="cart-remove-btn" href="Cart/Remove/${item.Id}">
//                                <i class="bi bi-x-circle text-danger"></i>
//                            </a>
//                        </div>
//                    </div>
//                </div>
//                <hr class="bg-dark" />
//        `);
//            if (item == response[Object.keys(response).length - 1]) {
//                const cartContainer = document.getElementById('cart-container');
//                cartContainer.insertAdjacentHTML('beforeend', `
//                    <div class="checkout-cart">
//                        <div class="total-cart">
//                            <div>
//                                <div class="Subtotal-cart">Sub-Total</div>
//                            </div>
//                            <div class="total-amount-cart me-3">$${response[0].TotalPrice}</div>
//                        </div>
//                        <a class="btn btn-warning" style="font-weight: bold" asp-area="" asp-controller="Product" asp-action="Checkout">Checkout</a>
//                    </div>
//                    `);
//            }

//            const cartClearBtn = document.querySelectorAll('.cart-clear');
//            const addCartButtonsList = document.querySelectorAll('.add-cart-btn-int');
//            const cartPlusBtns = document.querySelectorAll('.cart-plus-btn');
//            const cartMinusBtns = document.querySelectorAll('.cart-minus-btn');
//            const removeCartBtns = document.querySelectorAll('.cart-remove-btn');

//            addEventOnClick(displayCartBtn);
//            addEventOnClick(cartClearBtn);
//            addEventOnClick(addCartButtonsList);
//            addEventOnClick(cartPlusBtns);
//            addEventOnClick(cartMinusBtns);
//            addEventOnClick(removeCartBtns);
//        }
//    }
//};



//function addEventOnClick(btnsList) {
//    for (let element of btnsList) {
//        element.addEventListener('click', (event) => {
//            const cartContainer = document.getElementById('cart-container');
//            const currentElement = event.currentTarget;
//            const customRoute = currentElement.getAttribute("href");
//            getCart(cartContainer, customRoute);
//            event.preventDefault();
//        });
//    }
//}