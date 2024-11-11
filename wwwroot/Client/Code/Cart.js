document.addEventListener("DOMContentLoaded", function () {
    $("#div-footer").removeClass("d-none");
    getCart();
});

function getCart() {
    $.ajax({
        url: "/Home/RenderCart",
        type: "POST",
        success: function (data) {
            $("#giohang-render").empty();
            $("#giohang-render").append(data);
        }
    });
}

function deleteCart(idSach) {
    $.ajax({
        url: "/Home/DeleteCart",
        type: "POST",
        data: {
            idsach: idSach
        },
        success: function (data) {
            getCart();
            getCountCart();
        },
        error: function (e) {
        }
    });
}
function addQuantity(idSach) {
    var sl = parseInt($("#input-sl-" + idSach).val(), 10) || 0; 
    $("#input-sl-" + idSach).val(sl + 1);
    getSl(idSach);
}
function minusQuantity(idSach) {
    var sl = parseInt($("#input-sl-" + idSach).val(), 10) || 0;
    if (sl > 1) {
        $("#input-sl-" + idSach).val(sl - 1);
    }
    getSl(idSach);
}

function getSl(idSach)
{
    $.ajax({
        url: "/Home/ChangeSL",
        type: "POST",
        data: {
            idsach: idSach,
            sl: $("#input-sl-" + idSach).val()
        },
        success: function (data) {
            getCart();
        }
    });
}

//function Order() {
//    showLoadingSpinner();
//    $.ajax({
//        url: "/Home/Order",
//        type: "POST",
//        success: function (response) {
//            swal.close();
//            swal("Thành công! " + response.message, {
//                icon: "success",
//            });
//            $("#giohang-render").empty();
//            getCountCart();
//        }
//    });
//}