function login(btn) {
    if ($(btn).data("isclicked")) return true;
    $(btn).data("isclicked", true);
    $("#sntp").show();
    e =$("#iema").val();
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