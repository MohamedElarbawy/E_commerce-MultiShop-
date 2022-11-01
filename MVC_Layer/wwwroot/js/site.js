// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    getNumberOfProductsInWishList();
    getNumberOfProductsIncart();

   
});





//start shopping cart----------------------------------------------------------
var cartProduct;
var cartProducts = [];
var allIdsINcart = [];
function addToCart(product,chosenColor,chosenSize) {
    cartProduct = {

        id: product.getAttribute("data-product-id"),
        name: product.getAttribute("data-product-name"),
        img: product.getAttribute("data-product-img"),
        price: product.getAttribute("data-product-price"),
        color: chosenColor,
        size: chosenSize,
        count: 1,
        totalPrice: product.getAttribute("data-product-price")

    }
    cartProducts.push(cartProduct);

    if (sessionStorage.getItem("cartProducts") === null || sessionStorage.getItem("cartProducts").length === 0) {
        sessionStorage.setItem("cartProducts", JSON.stringify(cartProducts));
        getNumberOfProductsIncart();

    } else {//check if the item already exists and add it if not exists
        cartProducts = JSON.parse(sessionStorage.getItem("cartProducts"));
        for (let i = 0; i < cartProducts.length; i++) {
            allIdsINcart.push(cartProducts[i].id);
        }
        if (allIdsINcart.indexOf(cartProduct.id) === -1) {
            cartProducts.push(cartProduct);
            sessionStorage.setItem("cartProducts", JSON.stringify(cartProducts));
            getNumberOfProductsIncart();

        } else {
            //if item exists increase count if click add to cart
            //add colors and size if not chosen at first time
            var selectedProduct = Array.from(cartProducts).filter(x => x.id == cartProduct.id)[0];
            selectedProduct.count += 1;
            selectedProduct.color = cartProduct.color;
            selectedProduct.size = cartProduct.size;
            selectedProduct.totalPrice = parseInt(selectedProduct.price) * selectedProduct.count;

            console.log(cartProducts);
            sessionStorage.setItem("cartProducts", JSON.stringify(cartProducts));
        }

        
    }
    
   alertify.notify('Successfully added to cart!', 'success', 3);
   
   
}


function addToCartWithColorNSize(product){

  var color= product.parentElement.previousElementSibling.querySelector(":checked").value
    var size = product.parentElement.previousElementSibling.previousElementSibling.querySelector(":checked").value

    addToCart(product, color, size);
    location.href="/cart/cart"

}



function getNumberOfProductsIncart() {
    let counterElement = document.getElementById("shoppingCart-counter");
    let counterElement2 = document.getElementById("cart-counter");

    var productsIncart = JSON.parse(sessionStorage.getItem("cartProducts"));

    if (productsIncart == null) {
        counterElement.innerHTML = 0;
        counterElement2.innerHTML = 0;
    } else {
        counterElement.innerHTML = productsIncart.length;
        counterElement2.innerHTML = productsIncart.length;
    }
}




function appendProductsToCartBody() {

    var sessionStorageProducts = JSON.parse(sessionStorage.getItem("cartProducts"));
    console.log(sessionStorageProducts);

    for (var i = 0; i < sessionStorageProducts.length; i++) {
        var product = sessionStorageProducts[i];
        document.getElementById("cartBody").innerHTML += `
                     <tr ${product.color !== undefined ?'style="border-left:solid;border-width:thick;border-left-color:'+product.color:''}">
                        <td class="align-middle"><img src="/img/${product.img}" alt="" style="width: 50px;"></td>
                            <td class="align-middle"><a href="/Home/ProductDetails/${product.id}">${product.name}</a></td>
                            <td class="align-middle">${product.price}</td>
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
    var sessionStorageProducts = JSON.parse(sessionStorage.getItem("cartProducts"));
    //remove from ui
    product.parentElement.parentElement.remove();
    //remove from sessionStorage
    var newsessionStorageProducts = sessionStorageProducts.filter(x => x.id != product.getAttribute("data-product-id"));
    sessionStorage.setItem("cartProducts", JSON.stringify(newsessionStorageProducts));
    getNumberOfProductsIncart();
    getTotalPrice()
};





function changeQuantity(product) {
    var sessionStorageProducts = JSON.parse(sessionStorage.getItem("cartProducts"));

    var selectedProduct = Array.from(sessionStorageProducts).filter(x => x.id == product.getAttribute("data-product-id"))[0];

    if (product.classList.contains("btn-plus")) {
        selectedProduct.count += 1;
    } else {
        if (selectedProduct.count > 1) {
            selectedProduct.count -= 1;
        } else {
            selectedProduct.count = 1;
        }
    }
    selectedProduct.totalPrice = parseInt(selectedProduct.price) * selectedProduct.count;
    product.parentElement.parentElement.querySelector("input").value = selectedProduct.count;
    product.parentElement.parentElement.parentElement.parentElement.querySelector(".totalPrice").innerHTML = selectedProduct.totalPrice;


    var newsessionStorageProducts = sessionStorageProducts.filter(x => x.id != product.getAttribute("data-product-id"));
    newsessionStorageProducts.push(selectedProduct);
    sessionStorage.setItem("cartProducts", JSON.stringify(newsessionStorageProducts));
    console.log(sessionStorageProducts);
    getDiscount();
};




function getTotalPrice(discount) {
    var items = sessionStorage.getItem("cartProducts");
    $.ajax({
        method: "GET",
        url: "/cart/getTotalPrice",
        data: { sessionStorageItems: items },
        success: function (result) {

            document.getElementById("subtotal").innerHTML = `$${parseFloat(result).toFixed(2)}`
            if (discount != null) {
                discount /= 100;
                totalPriceAfterDiscount = parseFloat(result - (result * discount)).toFixed(2);
                console.log(totalPriceAfterDiscount);
                document.getElementById("totalPrice-discount").innerHTML = `$${totalPriceAfterDiscount}`;
            } else
                document.getElementById("totalPrice-discount").innerHTML = `$${parseFloat(result).toFixed(2)}`;


        }, error: function (xhr, status) {
            console.log(xhr);
            console.log(status);

        }
    })
};



function getDiscount(code) {
   // let discountCode = button.parentElement.parentElement.querySelector("input").value;
    let discountCode = "";
    if (code != null)
        discountCode = code;
    else discountCode = document.getElementById("coupon-code").value;
    let discountCodesList =[];
    discountCodesList.push(discountCode);

    $.ajax({
        method: "GET",
        url: "/cart/getDiscount",
        data: { code: discountCode },
        success: function (result) {
            if (code != null)
                return result;
            else if(result > 0 && result < 100) {
                document.getElementById("dicount").innerHTML = `${result}%`;
                getTotalPrice(result);
                sessionStorage.setItem("discountCodes", discountCodesList);
            } else {
                document.getElementById("dicount").innerHTML = `$00.0`;
                getTotalPrice();
            }
        },
        error: function (xhr, status) {
            console.log(xhr);
            console.log(status);

        }
    })
};


//end shopping cart----------------------------------------------------------
//start checkout page--------------------------------------------------------

function sendItems() {
    var x = sessionStorage.getItem("cartProducts");
    document.getElementById('addcartitem').value = x;
    console.log("item sent");
}

function appendProductsToCheckoutBody() {

    let parent = document.getElementById("order-products")
    let products = JSON.parse(sessionStorage.getItem("cartProducts"));
    let items = ``;
    products.forEach(function (product, i) {
        items += `<div class="d-flex justify-content-between">
        <p>${product.name}</p>
        <p>$${product.totalPrice}</p>
    </div>`
    });
    parent.innerHTML = ` <h6 class="mb-3">Products</h6>
${items}
`
   
}
function addTotalPriceToCheckoutPage() {
    let subtotalElement = document.getElementById("subtotal-chckout")
    let products = JSON.parse(sessionStorage.getItem("cartProducts"));
    let subtotalPrice=0
    products.forEach(function (product, i) {
        subtotalPrice += parseInt(product.totalPrice);
    })
    subtotalElement.innerHTML = `$${subtotalPrice}`
  let dicountCode=  sessionStorage.getItem("discountCodes");
    let discount = getDiscount(dicountCode);
    if (discount > 0)
        document.getElementById("discount-checkout").innerHTML=`$${discount}%`

}






//end checkout page----------------------------------------------------------
//Start wishList ------------------------------------------------------------


var wishListProduct;
var wishListProducts = [];
var allIds = [];

function addToWishLIst(product) {
    let id = product.getAttribute("data-product-id-wishlist");

    wishListProduct = {
        productId: id,
    }
    wishListProducts.push(wishListProduct);

    if (sessionStorage.getItem("wishListProducts") === null) {
        sessionStorage.setItem("wishListProducts", JSON.stringify(wishListProducts));
        getNumberOfProductsInWishList();

    } else {
        wishListProducts = JSON.parse(sessionStorage.getItem("wishListProducts"));
        for (let i = 0; i < wishListProducts.length; i++) {
            allIds.push(wishListProducts[i].productId);
        }
        if (allIds.indexOf(wishListProduct.productId) === -1) {
            wishListProducts.push(wishListProduct);
            sessionStorage.setItem("wishListProducts", JSON.stringify(wishListProducts));
            getNumberOfProductsInWishList();

        }


    }
    alertify.notify('Successfully added to WishList!', 'success', 3);

}



function getNumberOfProductsInWishList() {
    let counterElement = document.getElementById("wishlist-counter");
    let counterElement2 = document.getElementById("wishlist_counter");
    var productsInWishList = JSON.parse(sessionStorage.getItem("wishListProducts"));
    if (productsInWishList == null) {
        counterElement.innerHTML = 0;
        counterElement2.innerHTML = 0;
    } else {
        counterElement.innerHTML = productsInWishList.length;
        counterElement2.innerHTML = productsInWishList.length;
    }
}
//End wishList  --------------------------------------------------------------------------
//start alert in admin page when delete ---------------------------------------------------

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



//end alert in admin page when delete ---------------------------------------------
//start searching for products  ajax  ---------------------------------------------






$("#search").keyup(function () {

    let x = $(this).val();

    $.ajax({
        method: "GET",
        url: "/Search/Search",
        data: {s:x },
        success: function (result) {
            console.log(result);
            let items = '';
            $.each(result, function (i, product) {

                items += `<div class="col-lg-4 col-md-6 col-sm-6 pb-1 " id="div1">
                       <div class="product-item bg-light mb-4">
                            <div class="product-img position-relative overflow-hidden">
                                <img class="img-fluid w-100" src="/img/${product.imgName}" alt="">
                                <div class="product-action">
                                    <a class="btn btn-outline-dark btn-square" title="Add To Cart"
                                   data-product-id="${product.id}"
                                   data-product-name="${product.productName}"
                                   data-product-img="${product.imgName}"
                                   data-product-price="${product.productPrice}"
                                   onclick="addToCart(this)"><i class="fa fa-shopping-cart"></i></a>
                                    <a class="btn btn-outline-dark btn-square" data-product-id-wishlist="${product.id}" onclick="addToWishLIst(this)"><i class="far fa-heart"></i></a>

                                </div>
                            </div>
                            <div class="text-center py-4">
                                <a class="h6 text-decoration-none text-truncate" href="/Home/ProductDetails/${product.id}">${product.productName}</a>
                                <div class="d-flex align-items-center justify-content-center mt-2">
                                    <h5>$${product.productPrice}</h5>
                                </div>

                            </div>
                        </div>
                        </div>`
            });
            $("#renderSearch").html(items);
        },
        error: function (xhr, status) {
            console.log(xhr);
            console.log(status);

        }
    });


});



//end searching for products  ajax  ---------------------------------------------
