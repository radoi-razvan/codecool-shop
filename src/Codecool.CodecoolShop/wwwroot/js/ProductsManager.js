import { dataHandler } from "./DataHandler.js";
import { domManager } from "./DomManager.js";
import { htmlFactory } from "./HtmlFactory.js";
import { cartManager } from "./CartManager.js";

export let productsManager = {
    loadProducts: async function () {
        domManager.removeChildren('#productList');
        const products = await dataHandler.getProducts();
        for (let product of products) {
            const productCard = htmlFactory.cardBuilder(product);
            domManager.addChild("#productList", productCard);
            domManager.addEventListener(".add-cart-btn", "click", addToCart)
        }
    }
}

function addToCart(clickEvent) {
    clickEvent.preventDefault();
    const target = clickEvent.target;
    cartManager.addProduct(target.dataset.product);
}
