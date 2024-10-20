var _trang = 1;
var _maxtrang = 1;
document.addEventListener("DOMContentLoaded", function () {

    console.log("hello")
    getSach(1);

    window.addEventListener('scroll', () => {
        if (window.innerHeight + window.scrollY >= document.body.offsetHeight)
        {
            if (_trang < _maxtrang) {
                _trang++;
                getSach(_trang)
            } else {
                $("#div-footer").removeClass("d-none");
            }

        }
    });
    $('.chude').on('click', function (event) {
        event.preventDefault(); 
        if ($(this).hasClass('active')) {
            $('.chude').removeClass('active');
        } else {
            $('.chude').removeClass('active');
            $(this).addClass('active')
        }
        $('html, body').animate({ scrollTop: 0 }, 'slow');
        _trang = 1;
        _maxtrang = 1;
        $("#context-list").empty();
        getSach(1);
        $("#navbar-vertical").removeClass("show")
    });
    $('.nhaxuatban').on('click', function (event) {
        event.preventDefault();
        if ($(this).hasClass('active')) {
            $('.nhaxuatban').removeClass('active');
        } else {
            $('.nhaxuatban').removeClass('active');
            $(this).addClass('active')
        }
        $('html, body').animate({ scrollTop: 0 }, 'slow');
        _trang = 1;
        _maxtrang = 1;
        $("#context-list").empty();
        getSach(1);
        $("#navbar-vertical").removeClass("show");
    });
    $("#timkiem-sach").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Home/getSub",
                type: "POST",
                dataType: "json",
                data: {
                    search: request.term 
                },
                success: function (data) {
                    response(data);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            console.log("Selected: " + ui.item.value);
        }
    });


});
function getSach(trang) {
    $("#spinner").removeClass("d-none");
    $("#spinner").addClass("d-block");
    $.ajax({
        url: '/Home/GetSach',
        type: 'POST',
        data: {
            Search: "",
            MaCD: $(".chude.active").data("idcd") ?? 0,
            MaNXB: $(".nhaxuatban.active").data("idnxb") ?? 0,
            Trang: trang,
        },
        success: function (response) {
            $("#context-list").append(response);
            reSetMaxTrang();
            $("#spinner").removeClass("d-block");
            $("#spinner").addClass("d-none");
        },
        error: function (xhr, status, error) {
            console.error('Lỗi:', status, error);
        }
    });
}
function reSetMaxTrang() {
    $.ajax({
        url: '/Home/GetMaxTrang',
        type: 'POST',
        data: {
            Search: "",
            MaCD: $(".chude.active").data("idcd") ?? 0,
            MaNXB: $(".nhaxuatban.active").data("idnxb") ?? 0,
        },
        success: function (response) {
            _maxtrang = response.totalPages
            if (_trang < _maxtrang) {
                $("#div-footer").addClass("d-none");
            } else {
                $("#div-footer").removeClass("d-none");
            }
        },
        error: function (xhr, status, error) {
            console.error('Lỗi:', status, error);
        }
    });
}