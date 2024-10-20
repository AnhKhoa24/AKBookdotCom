document.addEventListener("DOMContentLoaded", function ()
{

    $("#nxb-parent").on("click", function () {
        if ($(this).hasClass('active')) {
            $("#nxb-child").removeClass("show");
            $(this).removeClass("active")
        } else {
            $("#nxb-child").addClass("show")
            $(this).addClass("active")
        }
    });

});
function showLoadingSpinner() {
    var spinner = document.createElement('div');
    spinner.className = 'spinner';

    swal({
        title: "Loading...",
        text: "Please wait while we process your request.",
        icon: "info",
        buttons: false,
        content: {
            element: "div",
            attributes: {
                class: "spinner",
            },
        },
        className: "custom-swal"
    });
}
function addToCart(idsach)
{
    showLoadingSpinner()
    $.ajax({
        url: '/Home/addToCart',
        type: 'POST',
        data: {
            idsach: idsach,
            sl: 1
        },
        success: function (response) {
            swal.close();
            if (response.status == 200) {
                swal("Thành công! Bạn đã thêm vào giỏ hàng!", {
                    icon: "success",
                });
            }
            else if (response.status == 500) {
                swal({
                    title: "Bạn cần đăng nhập",
                    text: "Đi đến trang đăng nhập để tiếp tục",
                    icon: "warning",
                    buttons: true,
                    dangerMode: true,
                })
                    .then((willDelete) => {
                        if (willDelete) {
                            window.location.href = "/Auth/Login";
                        } else {
                            return;
                        }
                    });
            }
        },
        error: function (xhr, status, error) {
            console.error('Lỗi:', status, error);
        }
    });
}