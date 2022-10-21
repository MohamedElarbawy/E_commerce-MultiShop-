// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    getNumberOfProductsInWishList();
    getNumberOfProductsIncart();
    appendProductsToCartBody();
    getTotalPrice();
});


//localStorage.clear();


var cartProduct;
var cartProducts = [];
var allIdsINcart = [];
function addToCart(product) {
    cartProduct = {

        id: product.getAttribute("data-product-id"),
        name: product.getAttribute("data-product-name"),
        img: product.getAttribute("data-product-img"),
        price: product.getAttribute("data-product-price"),
        color: product.getAttribute("data-product-color"),
        colorId: product.getAttribute("data-product-colorId"),
        count: 1,
        totalPrice: product.getAttribute("data-product-price")
        
    }
    cartProducts.push(cartProduct);

    if (localStorage.getItem("cartProducts") === null || localStorage.getItem("cartProducts").length === 0) {
        localStorage.setItem("cartProducts", JSON.stringify(cartProducts));
        getNumberOfProductsIncart();

    } else {
        cartProducts = JSON.parse(localStorage.getItem("cartProducts"));
        for (let i = 0; i < cartProducts.length; i++) {
            allIdsINcart.push(cartProducts[i].id);
        }
        if (allIdsINcart.indexOf(cartProduct.id) === -1) {
            cartProducts.push(cartProduct);
            localStorage.setItem("cartProducts", JSON.stringify(cartProducts));
            getNumberOfProductsIncart();

        } else {

            var selectedProduct = Array.from(cartProducts).filter(x => x.id == cartProduct.id)[0];
            selectedProduct.count += 1;
           selectedProduct.totalPrice = parseInt(selectedProduct.price) * selectedProduct.count;

            console.log(cartProducts);
            localStorage.setItem("cartProducts", JSON.stringify(cartProducts));
        }

    }
    //console.log(JSON.parse(localStorage.getItem("cartProducts")))


}


function getNumberOfProductsIncart() {
    let counterElement = document.getElementById("shoppingCart-counter");
    let counterElement2 = document.getElementById("cart-counter");

    var productsIncart = JSON.parse(localStorage.getItem("cartProducts"));

    if (productsIncart == null) {
        counterElement.innerHTML = 0;
        counterElement2.innerHTML = 0;
    } else {
        counterElement.innerHTML = productsIncart.length;
        counterElement2.innerHTML = productsIncart.length;
    }
}




function appendProductsToCartBody() {
  
    var localStorageProducts = JSON.parse(localStorage.getItem("cartProducts"));
    console.log(localStorageProducts);
   
    for (var i = 0; i < localStorageProducts.length; i++) {
        var product = localStorageProducts[i];
        document.getElementById("cartBody").innerHTML += `
                     <tr>
                        <td class="align-middle"><img src="/img/${product.img}" alt="" style="width: 50px;"> ${product.name}</td>
                            <td class="align-middle">$${product.price}</td>
                            <td class="align-middle">
                                <div class="input-group quantity mx-auto" style="width: 100px;">
                                    <div class="input-group-btn">
                                        <button class="btn btn-sm btn-primary btn-minus" data-product-id="${product.id}" onclick="changeQuantity(this)">
                                        <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                    <input type="text" class="form-control form-control-sm bg-secondary border-0 text-center " value="${product.count}">
                                    <div class="input-group-btn">
                                        <button class="btn btn-sm btn-primary btn-plus" data-product-id="${product.id}" onclick="changeQuantity(this)">
                                            <i class="fa fa-plus"></i>
                                        </button>
                                    </div>
                                </div>
                            </td>
                            <td class="align-middle totalPrice">$${product.totalPrice}</td>
                            <td class="align-middle"><button data-product-id="${product.id}" onclick="deleteProductFromCart(this)" class="removeBtn btn btn-sm btn-danger"><i class="fa fa-times"></i></button></td>
                        </tr>`
    }
}

function deleteProductFromCart(product) {
    var localStorageProducts = JSON.parse(localStorage.getItem("cartProducts"));
    //remove from ui
    product.parentElement.parentElement.remove();
    //remove from localStorage
    var newLocalStorageProducts = localStorageProducts.filter(x => x.id != product.getAttribute("data-product-id"));
    localStorage.setItem("cartProducts", JSON.stringify(newLocalStorageProducts));
    getNumberOfProductsIncart();
    getTotalPrice()
};





function changeQuantity(product) {
      //  var oldValue = product.parentElement.parentElement.querySelector("input").value;
    var localStorageProducts = JSON.parse(localStorage.getItem("cartProducts"));

    var selectedProduct = Array.from(localStorageProducts).filter(x => x.id == product.getAttribute("data-product-id"))[0];
    
    if (product.classList.contains("btn-plus")) {
       // var newVal = parseInt(oldValue) + 1;
        selectedProduct.count += 1;
    } else {
        if (selectedProduct.count > 1) {
          //  newVal = parseInt(oldValue) - 1;
            selectedProduct.count -= 1;
        } else {
           // newVal = 1;
            selectedProduct.count = 1;
        }
    }
    selectedProduct.totalPrice = parseInt(selectedProduct.price) * selectedProduct.count;
    product.parentElement.parentElement.querySelector("input").value = selectedProduct.count;
    product.parentElement.parentElement.parentElement.parentElement.querySelector(".totalPrice").innerHTML = selectedProduct.totalPrice;
    

    var newLocalStorageProducts = localStorageProducts.filter(x => x.id != product.getAttribute("data-product-id"));
    newLocalStorageProducts.push(selectedProduct);
    localStorage.setItem("cartProducts", JSON.stringify(newLocalStorageProducts));
    console.log(localStorageProducts);
    getTotalPrice()
};





//end shopping cart--------------------------------------------------------

//wishList Start ----------------------------------------------------------


var wishListProduct;
var wishListProducts = [];
var allIds = [];

function addToWishLIst(product) {
    let id = product.getAttribute("data-product-id-wishlist");

    wishListProduct = {
        productId: id,
    }
    wishListProducts.push(wishListProduct);

    if (localStorage.getItem("wishListProducts") === null) {
        localStorage.setItem("wishListProducts", JSON.stringify(wishListProducts));
        getNumberOfProductsInWishList();

    } else {
        wishListProducts = JSON.parse(localStorage.getItem("wishListProducts"));
        for (let i = 0; i < wishListProducts.length; i++) {
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
    let counterElement = document.getElementById("wishlist-counter");
    let counterElement2 = document.getElementById("wishlist_counter");
    var productsInWishList = JSON.parse(localStorage.getItem("wishListProducts"));
    if (productsInWishList == null) {
        counterElement.innerHTML = 0;
        counterElement2.innerHTML = 0;
    } else {
        counterElement.innerHTML = productsInWishList.length;
        counterElement2.innerHTML = productsInWishList.length;
    }
}
//wishList End -----------------------------------------------------------

//alert in admin page when delete start---------------------------------------------------

function alert(id) {
    swal({
        title: "Are you sure?",
        text: "This Product Will Be Deleted permanently From Database",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                confirmDelete(id);
            }

        });
}

function confirmDelete(productId) {
    $.ajax({
        type: "post",
        url: "/Admin/delete",
        data: { id: productId },
        success: function (result) {
            location.reload();
        }

    });

};



//alert in admin page when delete end---------------------------------------------



function getTotalPrice() {
    var items = localStorage.getItem("cartProducts");
    $.ajax({
        method: "GET",
        url: "/cart/getTotalPrice",
        data: { localStorageItems: items },
        success: function (result) {
            console.log(result);
            document.getElementById("subtotal").innerHTML =`$${result}`
            $(".subtotal").html = result;
            console.log(document.getElementById("subtotal"))
        },  error: function (xhr, status) {
            console.log(xhr);
            console.log(status);

        }
    })
};