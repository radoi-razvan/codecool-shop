// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
import { productsManager } from "./ProductsManager.js";
import { layoutManager } from "./LayoutManager.js";
import { cartManager } from "./CartManager.js";

const init = () => {
  layoutManager.loadLayoutElements();
  productsManager.loadProducts();
  cartManager.loadCart();
  cartManager.addClearCartEvent();
};

init();
