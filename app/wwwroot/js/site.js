function login(btn) {
    if ($(btn).data("isclicked")) return true;
    $(btn).data("isclicked", true);
    $("#sntp").show();
    e = $("#iema").val();
    p = $("#ipas").val();


    $.post("/User/enter", { email: e, password: p }, function (data) {
        var x = JSON.parse(data);
        if (x.code == "200") {
            window.location.replace("/Home/Index");
        }
        else {
            $("#sntp").hide();
            var $post = $("#frame");
            $post.addClass("shake");
            setTimeout(function () {
                $post.removeClass("shake");
                $(btn).data("isclicked", false);

            }, 1600);
        }
    });
    return false;
}

function processOrder(id, event) {
    event.preventDefault();
    try {
        $.get('/api/v1/order/process/<ID>'.replace('<ID>', id), function (data, textStatus, jqXHR) {
            if (jqXHR.status == 200) {
                $('td[data-order-id="' + id + '"]').html("<span>Заявка обработана</span>");
            }
        });
    } catch (e) {
        alert('Произошла ошибка!')
        console.info(e)
    }
}

//$(document).ready(function () {
document.addEventListener("DOMContentLoaded", function () {
    $("#iema, #ipas").on('keypress', function (e) {
        if (typeof e.keyCode !== "undefined" && e.keyCode == 13) {
            login($('#frame button'));
            return false;
        }
    });
});

// Shopping cart

const goodsDictionary = {
    '1': {
        name: 'Кепка',
        description: 'Белая кепка с фирменным логотипом компании',
        image_url: '/img/1 (1).jpg',
        price: 500
    },
    '2': {
        name: 'Футболка',
        description: 'Белая футболка с фирменным логотипом компании',
        image_url: '/img/2.jpg',
        price: 800,
    },
    '3': {
        name: 'Кружка',
        description: 'Белая кружка с фирменным логотипом компании',
        image_url: '/img/3.jpg',
        price: 350,
    }
};

function buildCartContents() {
    let cart = {};
    try {
        cart = JSON.parse(window.localStorage.getItem('shoppingCart'));
        if (cart === null)
            throw "Cart object is null!";
    } catch (e) {
        cart = {};
    }

    if (Object.keys(cart).length > 0) {
        let itemsHtmlTemplate = `
        <tr>
            <td>
                <div class="row">
                    <div class="col-sm-2"><img src="<IMAGE_URL>" alt="..." class="img-fluid img-thumbnail" /></div>
                    <div class="col-sm-10">
                        <h4 class="nomargin"><NAME></h4>
                        <p><DESCRIPTION></p>
                    </div>
                </div>
            </td >
            <td><PRICE></td>
            <td>
                <input data-product-id="<ID>" onchange="cartItemQtyChange(this)" type="number" class="form-control text-center" value="<QTY>">
            </td>
            <td class="text-center"><SUBTOTAL></td>
        </tr>`;
        let cartHtml = '';
        let total = 0;
        $.each(cart, function (key, value) {
            cartHtml += itemsHtmlTemplate
                .replace('<NAME>', goodsDictionary[key]['name'])
                .replace('<DESCRIPTION>', goodsDictionary[key]['description'])
                .replace('<IMAGE_URL>', goodsDictionary[key]['image_url'])
                .replace('<PRICE>', goodsDictionary[key]['price'] + '₽')
                .replace('<ID>', key)
                .replace('<QTY>', value)
                .replace('<SUBTOTAL>', (goodsDictionary[key]['price'] * value) + '₽');
            total += goodsDictionary[key]['price'] * value;
        });

        $('#cart tbody').html(cartHtml);
        $('#cart_total').html('Итог: ' + total + '₽');
    } else {
        $('#cart tbody').html(`
        <tr>
            <td colspan="4" class="text-center">
                Пока нет товаров
            </td>
        </tr>`);
        $('#cart_total').html('Итог: 0₽');
    }
}

function cartItemQtyChange(element) {
    let cart = JSON.parse(window.localStorage.getItem('shoppingCart'));
    cart[$(element).data('productId')] = $(element).val();
    if ($(element).val() < 1) {
        delete cart[$(element).data('productId')];
    }

    window.localStorage.setItem('shoppingCart', JSON.stringify(cart));
    buildCartContents();
}

function addCartItem(element) {
    let cart = {};
    try {
        cart = JSON.parse(window.localStorage.getItem('shoppingCart'));
        if (cart === null)
            throw "Cart object is null!";
    } catch (e) {
        cart = {};
    }

    let productId = $(element).data('productId');
    
    if(typeof cart[productId] === 'number') {
        cart[productId] += 1;
    } else {
        cart[productId] = 1;
    }
    
    window.localStorage.setItem('shoppingCart', JSON.stringify(cart));
    
    $(element).toggleClass("btn-primary btn-success").html('Добавлено!').prop('disabled', true);
    
    setTimeout(function () {
        $(element).toggleClass("btn-success btn-primary").html('В корзину').prop('disabled', false);
    }, 2000);
}


document.addEventListener("DOMContentLoaded", function () {
    buildCartContents();
});