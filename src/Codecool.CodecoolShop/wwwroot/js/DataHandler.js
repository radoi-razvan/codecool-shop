export const dataHandler = {
  getProducts: async function () {
    let response = await apiGet("/get-products");
    return response;
  },

  getCategories: async function () {
    let response = await apiGet("api/get-categories");
    return response;
  },

  getSuppliers: async function () {
    let response = await apiGet("api/get-suppliers");
    return response;
  },

  getProductsByCategory: async function (categoryId) {
    let response = await apiGet(`/get-products/category/${categoryId}`);
    return response;
  },

  getProductsBySupplier: async function (supplierId) {
    let response = await apiGet(`/get-products/supplier/${supplierId}`);
    return response;
  },

  getCartProducts: async function () {
    let response = await apiGet("api/get-cart-products");
    return response;
  },

  addProductToCart: async function (productId) {
    let response = await apiPost(`cart/add/${productId}`);
    return response;
  },

  increaseProductQuantity: async function (productId) {
    let response = await apiPut(`cart/increase/${productId}`);
    return response;
  },

  removeProductFromCart: async function (productId) {
    let response = await apiPut(`cart/remove/${productId}`);
    return response;
  },

  deleteProductFromCart: async function (productId) {
    let response = await apiDelete(`cart/delete/${productId}`);
    return response;
  },

  clearCart: async function () {
    let response = await apiDelete(`cart/clear`);
    return response;
  },
};

async function apiGet(url) {
  const response = await fetch(url);
  if (response.ok) {
    const data = response.json();
    return data;
  }
}

async function apiPost(url) {
  const response = await fetch(url, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
  });
  if (response.ok) {
    console.log("POST request succeeded");
  }
}

async function apiDelete(url) {
  const response = await fetch(url, {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
  });
  if (response.ok) {
    console.log("DELETE request succeeded");
  }
}

async function apiPut(url) {
  const response = await fetch(url, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
  });
  if (response.ok) {
    console.log("Put request succeeded");
  }
}
