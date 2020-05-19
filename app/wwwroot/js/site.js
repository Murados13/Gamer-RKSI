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