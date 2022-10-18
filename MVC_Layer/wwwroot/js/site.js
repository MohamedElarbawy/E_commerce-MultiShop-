// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
getNumberOfProductsInWishList();
getNumberOfProductsInShoppingCart();


//localStorage.clear();


var ShoppingCartProduct;
var ShoppingCartProducts = [];
var allIdsINShoppingCart = [];
function addToCart(product) {
    ShoppingCartProduct = {

     id : product.getAttribute("data-product-id"),
     name : product.getAttribute("data-product-name"),
     img   : product.getAttribute("data-product-img"),
     price : product.getAttribute("data-product-price"),
     color : product.getAttribute("data-product-color"),
     colorId : product.getAttribute("data-product-colorId"),
     count:1
    }
    ShoppingCartProducts.push(ShoppingCartProduct);

    if (localStorage.getItem("shoppingCartProducts") === null || localStorage.getItem("shoppingCartProducts").length === 0) {
        localStorage.setItem("shoppingCartProducts", JSON.stringify(ShoppingCartProducts));
        getNumberOfProductsInShoppingCart();

    } else {
        ShoppingCartProducts = JSON.parse(localStorage.getItem("shoppingCartProducts"));
        for (let i = 0; i < ShoppingCartProducts.length; i++) {
            allIdsINShoppingCart.push(ShoppingCartProducts[i].id);
        }
        if (allIdsINShoppingCart.indexOf(ShoppingCartProduct.id) === -1) {
            ShoppingCartProducts.push(ShoppingCartProduct);
            localStorage.setItem("shoppingCartProducts", JSON.stringify(ShoppingCartProducts));
            getNumberOfProductsInShoppingCart();

        } else {
            console.log(ShoppingCartProducts);
            Array.from(ShoppingCartProducts).filter(x=>x.id == ShoppingCartProduct.id)[0].count += 1;
            console.log(ShoppingCartProducts);
            localStorage.setItem("shoppingCartProducts", JSON.stringify(ShoppingCartProducts));
        }

    }
    //console.log(JSON.parse(localStorage.getItem("shoppingCartProducts")))

   
}


function getNumberOfProductsInShoppingCart() {
    let counterElement = document.getElementById("shoppingCart-counter");
    var productsInshoppingCart = JSON.parse(localStorage.getItem("shoppingCartProducts"));
    if (productsInshoppingCart == null) {
        counterElement.innerHTML = 0;
    } else {
        counterElement.innerHTML = productsInshoppingCart.length;
    }
}




function appendProductsToCartBody() {
    document.getElementById("cartBody").innerHTML ="<h3>Your selected products will appear here</h3>";
 var   localStorageProducts = JSON.parse(localStorage.getItem("shoppingCartProducts"));
    console.log(localStorageProducts);
    console.log(typeof (localStorageProducts));
    for (var i = 0; i < localStorageProducts.length; i++) {
        var product = localStorageProducts[i];
        document.getElementById("cartBody").innerHTML += `
 <tr>
                            <td class="align-middle"><img src="/img/${product.img}" alt="" style="width: 50px;"> Product Name</td>
                            <td class="align-middle">$150</td>
                            <td class="align-middle">
                                <div class="input-group quantity mx-auto" style="width: 100px;">
                                    <div class="input-group-btn">
                                        <button class="btn btn-sm btn-primary btn-minus">
                                        <i class="fa fa-minus"></i>
                                        </button>
                                    </div>
                                    <input type="text" class="form-control form-control-sm bg-secondary border-0 text-center" value="1">
                                    <div class="input-group-btn">
                                        <button class="btn btn-sm btn-primary btn-plus">
                                            <i class="fa fa-plus"></i>
                                        </button>
                                    </div>
                                </div>
                            </td>
                            <td class="align-middle">$150</td>
                            <td class="align-middle"><button data-product-id="${product.id}"  class="removeBtn btn btn-sm btn-danger"><i class="fa fa-times"></i></button></td>
                        </tr>

`
    }
}
appendProductsToCartBody();

var localStorageProducts = JSON.parse(localStorage.getItem("shoppingCartProducts"));
Array.from(document.querySelectorAll(".removeBtn")).forEach(x => x.onclick = function() {
    //remove from ui
    this.parentElement.parentElement.remove();
    //remove from ls
    var newLocalStorageProducts = localStorageProducts.filter(x => x.id != this.getAttribute("data-product-id"));
    localStorage.setItem("shoppingCartProducts", JSON.stringify(newLocalStorageProducts));
})


//wishList Start


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
    var productsInWishList = JSON.parse(localStorage.getItem("wishListProducts"));
    if (productsInWishList == null) {
        counterElement.innerHTML = 0;
    } else {
        counterElement.innerHTML = productsInWishList.length;
    }
}


//wishList End
function SendLocalStorage() {

    let cartObjects = localStorage.getItem("shoppingCartProducts");

    $.ajax({
        type: "POST",
        url: "/Cart/GetLocalStorage",
        data: {items:cartObjects},
        success: function (result) {
            console.log(result)
        }

    });

};

//alert in admin page when delete

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

function confirmDelete (productId){
    $.ajax({
        type: "post",
        url: "/Admin/delete",
        data: { id: productId },
        success: function (result) {
            location.reload();
        }

    });

};

//document.getElementsByClassName("fa-plus").onclick = function () {




//}
(function ($) {
$('.quantity button').on('click', function () {
    var button = $(this);
    var oldValue = button.parent().parent().find('input').val();
    if (button.hasClass('btn-plus')) {
        var newVal = parseFloat(oldValue) + 1;
    } else {
        if (oldValue > 1) {
            var newVal = parseFloat(oldValue) - 1;
        } else {
            newVal = 1;
        }
    }
    button.parent().parent().find('input').val(newVal);
});
    
});





//$(document).ready(function () {
//    $("button").click(function () {
//        $("#div1").load("/Some/ShopAjax", { query: "300-700" });
//    });
//});