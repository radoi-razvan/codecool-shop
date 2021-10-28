import { dataHandler } from "./DataHandler.js";
import { domManager } from "./DomManager.js";
import { htmlFactory } from "./HtmlFactory.js";

export let cartManager = {
    loadCart: async function () {
        domManager.removeChildren('#cart-container');
        const products = await dataHandler.getCartProducts();
        for (let product of products) {
            const productCard = htmlFactory.cartItemBuilder(product);
            domManager.addChild("#cart-container", productCard);
            let item = document.getElementById("");

        }
    },

    addProduct: async function () {
        const cartProducts = await dataHandler.getCartProducts();
    },

    removeProduct: async function () {

    },

    removeProduct: async function () {

    },

    clearCart: async function () {

    }
}

function addEventsToCartItem(cartItem) {

}
