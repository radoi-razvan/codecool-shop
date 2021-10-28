import { dataHandler } from "./DataHandler.js";
import { domManager } from "./DomManager.js";
import { htmlFactory } from "./HtmlFactory.js";

export let cartManager = {
    loadCart: async function () {
        domManager.removeChildren('#productList');
        const products = await dataHandler.getCartProducts();
        for (let product of products) {
            const productCard = htmlFactory.cartItemBuilder(product);
            domManager.addChild("#productList", productCard);
            domManager.addEventListener(".add-cart-btn", "click", addToCart)
        }
    },

    addProduct: async function () {
        const cartProducts = await dataHandler.getCartProducts();
    }
}
