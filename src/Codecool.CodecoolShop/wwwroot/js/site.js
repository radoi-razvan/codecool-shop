// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const init = () => {
    const toggleCartBtn = document.getElementById("cartBtn");
    toggleCartBtn.addEventListener("mouseover", (event) => {
        const cartContainer = document.getElementById("container-cart");
        cartContainer.style.display = "flex";
        event.preventDefault();
    });

    const cartContent = document.getElementById("container-cart").childNodes[1];
    cartContent.addEventListener("mouseleave", (event) => {
        const cartContainer = document.getElementById("container-cart");
        cartContainer.style.display = "none";
    });
}

init();
