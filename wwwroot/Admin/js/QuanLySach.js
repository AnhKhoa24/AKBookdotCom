document.addEventListener("DOMContentLoaded", function () {
    getSach(1)

    $("#tacgia").select2();

    $('#phantrang').on("click", ".page-item:not(.disabled)", function (event) {
        event.preventDefault();
        var pageNum;
        if ($(this).hasClass('previous')) {
            var currentPage = $('#phantrang .page-item.active').find('.page-link').text();
            pageNum = parseInt(currentPage) - 1;
        } else if ($(this).hasClass('next')) {
            var currentPage = $('#phantrang .page-item.active').find('.page-link').text();
            pageNum = parseInt(currentPage) + 1;
        } else {
            pageNum = $(this).find('.page-link').text();
        }
        if (!$(this).hasClass('active')) {
            //$('#phantrang .page-item').removeClass('active');
            //$(this).addClass('active');
            getSach(pageNum);
        }
    });
});

function getSach(page) {
    $.ajax({
        url: '/Admin/Home/Sach',
        type: 'POST',
        dataType: 'json',
        data: {
            trang: page,
            pagesize: 10,
            search: ''
        },
        success: function (response) {
            $("#dataSach").empty();
            var i = 1;
            response.data.forEach(function (element) {
                var giaban = new Intl.NumberFormat('en-US').format(element.giaban);

                var item = `<tr>
                            <td>${i}</td>
                            <td>${element.tensach}</td>
                            <td>${giaban}</td>
                            <td>${element.mota}</td>
                            <td>${element.anhbia}</td>
                            <td>Sửa</td>
                        </tr>
`;
                $("#dataSach").append(item);
                i++;
            });
            GenPagnation(response.trang, response.tongtrang);
        },
        error: function (xhr, status, error) {
            alert("Sai mật khẩu")
        }
    });
}


function GenPagnation(trang, sotrang) {
    var htmltrang = "";
    if (trang == 1) {
        htmltrang += `<li class="page-item disabled previous">
                        <a class="page-link" href="#" tabindex="-1" aria-disabled="true">Previous</a>
                    </li>`;
    } else {
        htmltrang += `<li class="page-item previous">
                        <a class="page-link" href="#">Previous</a>
                    </li>`;
    }

    for (let i = 1; i <= sotrang; i++) {
        if (i == trang) {
            htmltrang += `<li class="page-item active"><a class="page-link" href="#">${i}</a></li>`;
        } else {
            htmltrang += `<li class="page-item"><a class="page-link" href="#">${i}</a></li>`;
        }
    }

    if (trang == sotrang) {
        htmltrang += `<li class="page-item disabled next">
                        <a class="page-link" href="#">Next</a>
                    </li>`;
    } else {
        htmltrang += `<li class="page-item next">
                        <a class="page-link" href="#">Next</a>
                    </li>`;
    }

    $("#phantrang").empty();
    $("#phantrang").append(htmltrang);
}
