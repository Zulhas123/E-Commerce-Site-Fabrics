// Toaster Message Function
function showToasterMessage(message, type) {
    toastr.options = {
        closeButton: true,
        progressBar: true,
        positionClass: "toast-top-right",
        timeOut: 8000
    };
    toastr[type](message);
}

// CSRF Token
if (typeof csrfToken === 'undefined') {
    const csrfToken = $('meta[name="csrf-token"]').attr('content');
}


// AJAX Form Submission - Checkout
$("#checkout-form").on("submit", function (e) {
    e.preventDefault();
    const form = $(this);
    const url = form.attr("action");

    $.ajax({
        url,
        type: "POST",
        data: form.serialize(),
        success: function (response) {
            if (response.status) {
                showToasterMessage(response.msg, "success");
                window.location.href = response.url;
            } else {
                showToasterMessage(response.msg, "error");
            }
        },
        error: function () {
            showToasterMessage("An error occurred. Please try again later.", "error");
        }
    });
});

// Remove Item from Cart
$(document).on('click', '.remove-item', function (e) {
    e.preventDefault();
    const id = $(this).data('id');
    const url = `https://www.mapkathi.com/cart/${id}`;

    $.ajax({
        type: 'GET',
        url,
        success: function (res) {
            if (res.status) {
                toastr.success(res.msg);
                window.location.reload();
            } else {
                toastr.error(res.msg);
            }
        },
        error: function () {
            toastr.error('An error occurred while processing your request.');
        }
    });
});

// Quantity Increment/Decrement & Update Cart
$(document).ready(function () {
    function updateSubtotal(id, quantity) {
        const price = parseFloat($(`#subtotal-${id}`).data('price'));
        const subtotal = price * quantity;
        $(`#subtotal-${id}`).text(subtotal.toFixed(2));
        updateCart(id, quantity);
    }

    function updateCart(id, quantity) {
        $.ajax({
            type: 'POST',
            url: `https://www.mapkathi.com/cart/update/${id}`,
            data: {
                _token: csrfToken,
                quantity
            }
        });
    }

    $(document).on('click', '.inc', function (e) {
        e.preventDefault();
        const id = $(this).data('id');
        const input = $(`.quantity-value[data-id='${id}']`);
        const newQuantity = parseInt(input.val()) + 1;
        input.val(newQuantity);
        updateSubtotal(id, newQuantity);
    });

    $(document).on('click', '.dec', function (e) {
        e.preventDefault();
        const id = $(this).data('id');
        const input = $(`.quantity-value[data-id='${id}']`);
        const newQuantity = parseInt(input.val()) - 1;
        if (newQuantity >= 1) {
            input.val(newQuantity);
            updateSubtotal(id, newQuantity);
        }
    });

    $(document).on('click', '.update-cart', function (e) {
        e.preventDefault();
        const id = $(this).data('id');
        const quantity = parseInt($(`.quantity-value[data-id='${id}']`).val());

        $.ajax({
            type: 'POST',
            url: `https://www.mapkathi.com/cart/update/${id}`,
            data: {
                _token: csrfToken,
                quantity
            },
            success: function (response) {
                const subtotal = response.totalAmount.toFixed(2);
                $(`#subtotal-${id}`).text(subtotal);
                $('#total-amount').text(subtotal);
            }
        });
    });
});

// Buy Now Button
$(document).ready(function () {
    $('.buy-now').on('click', function (e) {
        e.preventDefault();
        const productId = $(this).attr('href').split('/').pop();
        const addToCartUrl = $(this).data('url');

        $.ajaxSetup({ headers: { 'X-CSRF-TOKEN': csrfToken } });

        $.post(addToCartUrl, { id: productId, quantity: 1 }, function (response) {
            if (response.status) {
                toastr.success(response.msg);
                window.location.href = "https://www.mapkathi.com/checkout";
            } else {
                toastr.error(response.msg);
            }
        });
    });
});

// Cart Add via Button
$(function () {
    function setCookie(name, value, minutes) {
        let expires = "";
        if (minutes) {
            const date = new Date();
            date.setTime(date.getTime() + (minutes * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    }

    function getCookie(name) {
        const nameEQ = name + "=";
        const ca = document.cookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) === ' ') c = c.substring(1);
            if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length);
        }
        return null;
    }

    // Close Offer Popup
    $(".modal-overlay, .offerBox .content .close").click(function () {
        $('.offerBox').hide();
        setCookie('offerBoxHidden', 'true', 5);
    });

    if (getCookie('offerBoxHidden') === 'true') {
        $('.offerBox').hide();
    }

    // Add to Cart
    $(document).on('click', '.add-to-cart', function () {
        const id = $(this).data('id');
        const url = $(this).data('url');
        addToCart(url, id);
    });

    function addToCart(url, id, variation = "", quantity = 1) {
        $.ajax({
            type: "POST",
            url,
            headers: { 'X-CSRF-TOKEN': csrfToken },
            data: { id, quantity, variation },
            success: function (res) {
                if (res.status) {
                    window.location.reload();
                } else {
                    toastr.error(res.msg);
                }
            },
            error: function () {
                toastr.error('An error occurred while processing your request.');
            }
        });
    }
});

// Owl Carousel Init
$(document).ready(function () {
    $('.owl-carousel').show();

    $('.product_slider_sell').owlCarousel({
        loop: true,
        autoplay: true,
        autoplayTimeout: 3000,
        autoplayHoverPause: true,
        rewind: false,
        responsive: {
            0: { items: 1 },
            340: { items: 2 },
            500: { items: 2 },
            600: { items: 3 },
            870: { items: 4 },
            1070: { items: 5 },
            1200: { items: 5 },
            1300: { items: 5 },
            1400: { items: 6 }
        }
    });

    $('.slider_product').owlCarousel({
        loop: true,
        rewind: false,
        responsive: {
            0: { items: 1 },
            340: { items: 2 },
            500: { items: 2 },
            600: { items: 3 },
            870: { items: 4 },
            1070: { items: 5 },
            1200: { items: 5 },
            1300: { items: 5 },
            1400: { items: 6 }
        }
    });
});

// Show popup after 5 minutes
document.addEventListener("DOMContentLoaded", function () {
    const popUpForm = document.getElementById("popUpForm");
    const shouldShowPopup = localStorage.getItem("showPopup");
    const lastCloseTime = localStorage.getItem("lastCloseTime");

    if (!shouldShowPopup || (shouldShowPopup && lastCloseTime && Date.now() - lastCloseTime >= 5 * 60 * 1000)) {
        popUpForm.style.display = "block";
    }



    document.querySelector('.popupGrid').addEventListener('click', function (event) {
        if (event.target.classList.contains('popupGrid')) {
            popUpForm.style.display = "none";
            localStorage.setItem("showPopup", false);
            localStorage.setItem("lastCloseTime", Date.now());
        }
    });

    document.getElementById("close").addEventListener("click", function () {
        popUpForm.style.display = "none";
        localStorage.setItem("showPopup", false);
        localStorage.setItem("lastCloseTime", Date.now());
    });
});
$(document).ready(function () {
    if ($('.brand-slider').length > 0) {
        $('.brand-slider').slick({
            slidesToShow: 5,
            slidesToScroll: 1,
            autoplay: true,
            autoplaySpeed: 2000,
            arrows: false,
            dots: false,
            responsive: [
                {
                    breakpoint: 768,
                    settings: {
                        slidesToShow: 2
                    }
                },
                {
                    breakpoint: 480,
                    settings: {
                        slidesToShow: 1
                    }
                }
            ]
        });
    }
});


// Initialize Select2
$(document).ready(function () {
    $('.select2').select2({ closeOnSelect: true });
});
