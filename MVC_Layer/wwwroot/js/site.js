// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
getNumberOfProductsInWishList();
getNumberOfProductsInShoppingCart();

localStorage.clear();


var ShoppingCartProduct;
var ShoppingCartProducts = [];
var allIdsINShoppingCart = [];
function addToCart(product) {
    var productId = product.getAttribute("data-product-id");
    ShoppingCartProduct = {
        "productId": productId,
        "count":1
    }

    if (localStorage.getItem("shoppingCartProducts") === null) {
        localStorage.setItem("shoppingCartProducts", JSON.stringify(ShoppingCartProducts));
        getNumberOfProductsInShoppingCart();

    } else {
        ShoppingCartProducts = JSON.parse(localStorage.getItem("shoppingCartProducts"));
        for (var i = 0; i < ShoppingCartProducts.length; i++) {
            allIdsINShoppingCart.push(ShoppingCartProducts[i].productId);
        }
        if (allIds.indexOf(wishListProduct.productId) === -1) {
            ShoppingCartProducts.push(wishListProduct);
            localStorage.setItem("shoppingCartProducts", JSON.stringify(ShoppingCartProducts));
            getNumberOfProductsInShoppingCart();

        }


    }
    console.log(JSON.parse(localStorage.getItem("shoppingCartProducts")))



}


function getNumberOfProductsInShoppingCart() {
    var counterElement = document.getElementById("shoppingCart-counter");
    var productsInshoppingCart = JSON.parse(localStorage.getItem("shoppingCartProducts"));
    if (productsInshoppingCart == null) {
        counterElement.innerHTML = 0;
    } else {
        counterElement.innerHTML = productsInshoppingCart.length;
    }
}





















//wishList Start


var wishListProduct;
var wishListProducts = [];
var allIds = [];

function addToWishLIst(product) {
    var id = product.getAttribute("data-product-id-wishlist");
   
    wishListProduct = {
        "productId": id
    }

    if (localStorage.getItem("wishListProducts") === null) {
        localStorage.setItem("wishListProducts", JSON.stringify(wishListProducts));
        getNumberOfProductsInWishList();

    } else {
        wishListProducts = JSON.parse(localStorage.getItem("wishListProducts"));
        for (var i = 0; i < wishListProducts.length; i++) {
            allIds.push(wishListProducts[i].productId);
        }
        if (allIds.indexOf(wishListProduct.productId) === -1) {
            wishListProducts.push(wishListProduct);
            localStorage.setItem("wishListProducts", JSON.stringify(wishListProducts));
            getNumberOfProductsInWishList();

        }


    }
        console.log(JSON.parse(localStorage.getItem("wishListProducts")))
}



function getNumberOfProductsInWishList() {
    var counterElement = document.getElementById("wishlist-counter");
    var productsInWishList = JSON.parse(localStorage.getItem("wishListProducts"));
    if (productsInWishList == null) {
        counterElement.innerHTML = 0;
    } else {
        counterElement.innerHTML = productsInWishList.length;
    }
}


//wishList End
