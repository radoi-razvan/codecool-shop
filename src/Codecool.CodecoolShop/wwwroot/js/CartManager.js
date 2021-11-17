import { dataHandler } from "./DataHandler.js";
import { domManager } from "./DomManager.js";
import { htmlFactory } from "./HtmlFactory.js";

export let cartManager = {
  loadCart: async function () {
    domManager.removeChildren("#cart-container");
    const products = await dataHandler.getCartProducts();
    let productsSum = 0;
    for (let product of products) {
      productsSum +=
        parseFloat(product.DefaultPrice).toFixed(2) *
        product.CartProduct.Quantity;
      const productCard = htmlFactory.cartItemBuilder(product);
      domManager.addChild("#cart-container", productCard);
    }
    const sumCard = htmlFactory.cartTotalBuilder(productsSum);
    domManager.addChild("#cart-container", sumCard);
    addEventsToCartItems();
    displayCartCount(products);
  },

  addProduct: async function (productId) {
    await dataHandler.addProductToCart(productId);
    this.loadCart();
  },

  increaseProduct: async function (productId) {
    await dataHandler.increaseProductQuantity(productId);
    this.loadCart();
  },

  deleteProduct: async function (productId) {
    await dataHandler.deleteProductFromCart(productId);
    this.loadCart();
  },

  removeProduct: async function (productId) {
    await dataHandler.removeProductFromCart(productId);
    this.loadCart();
  },

  clearCart: async function () {
    await dataHandler.clearCart();
    this.loadCart();
  },
};

function addEventsToCartItems() {
  const increaseBtns = document.querySelectorAll(".product-increase");
  const decreaseBtns = document.querySelectorAll(".product-remove");
  const deleteBtns = document.querySelectorAll(".product-delete");
  const cartClearBtn = document.getElementById("cart-clear");

  for (let iBtn of increaseBtns) {
    iBtn.addEventListener("click", (event) => {
      event.preventDefault();
      cartManager.increaseProduct(event.target.dataset.increase.split("-")[1]);
    });
  }

  for (let dBtn of decreaseBtns) {
    dBtn.addEventListener("click", (event) => {
      event.preventDefault();
      cartManager.removeProduct(event.target.dataset.remove.split("-")[1]);
    });
  }

  for (let deBtn of deleteBtns) {
    deBtn.addEventListener("click", (event) => {
      event.preventDefault();
      cartManager.deleteProduct(event.target.dataset.delete.split("-")[1]);
    });
  }

  cartClearBtn.addEventListener("click", (event) => {
    event.preventDefault();
    cartManager.clearCart();
  });
}

function displayCartCount(products) {
  const cartCountDisplay = document.getElementById("cart-quantity");
  let count = 0;
  for (let product of products) {
    count += product.CartProduct.Quantity;
  }
  cartCountDisplay.innerHTML = count;
}
