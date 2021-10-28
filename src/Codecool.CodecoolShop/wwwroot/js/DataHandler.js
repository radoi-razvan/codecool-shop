export const dataHandler = {
    getProducts: async function () {
        let response = await apiGet('/get-products');
        return response
    },

    getProductsByCategory: async function (supplierId) {
        let response = await apiGet(`/get-products/${supplierId}`);
        return response
    },

    getProductsBySupplier: async function (categoryId) {
        let response = await apiGet(`/get-products/${categoryId}`);
        return response
    },

    getCartProducts: async function () {
        let response = await apiGet('/get-cart-products')
        return response
    },

    addProductToCart: async function (productId) {
        let response = await apiPut(`Cart/Add/${productId}`)
        return response
    }
}


    async function apiGet (url) {
        const response = await fetch(url)
        if (response.ok) {
            const data = response.json()   
            return data
        }
    }

    async function apiPost(url, payload) {
        const response = await fetch(url, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload),
        })
        if (response.ok) {
            const data = response.json();
            return data;
        }
    }

    async function apiDelete(url) {
        const response = await fetch(url, {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json' }
        })
        if (response.ok) {
            const data = response.json()
            return data
        }
    }

    async function apiPut(url) {
        const response = await fetch(url, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' }
        })
        if (response.ok) {
            const data = response.json()
            return data
        }
    }
