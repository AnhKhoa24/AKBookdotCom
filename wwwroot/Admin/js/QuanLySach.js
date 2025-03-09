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
                            <td>${giaban} đ</td>
                            <td>${element.mota}</td>
                            <td>
                            <img src="/Images/${element.anhbia}" style="height: 50px; width:auto; border-radius: 4px;"/>
                            </td>
                           <td class="justify-content-start align-items-center">
    <button onclick="GetDataUpdateSach(${element.masach})" class="btn btn-light">
        <img src="/Images/icons8-edit-20.png" class="rounded" alt="..." />
    </button>
    <button onclick="Delete(${element.masach})" class="btn btn-light ml-2">
        <img src="/Images/icons8-delete-20.png" class="rounded" alt="..." />
    </button>
</td>

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

function Delete(id) {
    swal({
        title: "Xóa sách?",
        text: "Bạn có chắc muốn xóa sách này không!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                swal("Poof! Your imaginary file has been deleted!", {
                    icon: "success",
                });
            } else {
                return;
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


function addBook() {
    var formData = new FormData();
    formData.append('TenSach', $('#tensach').val());
    formData.append('MaSach', $('#masach').val());
    formData.append('GiaBan', $('#giaban').val());
    formData.append('SLTon', $('#slton').val());
    formData.append('MaNXB', $('#nhaxuatban').val()[0]);
    formData.append('MaCD', $('#chude').val()[0]);

    var tacGias = $('#tacgias').val();
    tacGias.forEach(function (value) {
        formData.append('TacGias', value);
    });

    formData.append('MoTa', getDataFromEditor());
    var imageFile = $('#imageFileInput')[0].files[0];
    if (imageFile) {
        formData.append('file', imageFile);
    }
    $.ajax({
        url: '/Admin/Home/AddSach',
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            swal("Thành công! " + response.message, {
                icon: "success",
            });
            resetFormAndCloseModal();
            let tranghientai = $("#phantrang .page-item.active a").text();
            getSach(tranghientai)
        },
        error: function (xhr, status, error) {
            alert('Lỗi khi gửi dữ liệu!');
            console.log(error);
        }
    });
}

function resetFormAndCloseModal() {
    $('#tensach').val('');
    $('#giaban').val('');
    $('#slton').val('');
    $('#nhaxuatban').val(null).trigger('change');
    $('#chude').val(null).trigger('change');
    $('#tacgias').val(null).trigger('change');
    CKEDITOR.instances.editor.setData('');
    $('#imageFileInput').val('');
    $('#previewImage').hide();
    $('#clearImageButton').hide();
    $('#exampleModal').modal('hide');
}

function GetDataUpdateSach(id) {
    resetFormAndCloseModal();
    $.ajax({
        url: '/Admin/Home/getDataEdit',
        type: 'POST',
        data: {
            idsach: id
        },
        success: function (response) {

            CKEDITOR.instances.editor.setData(response.mota);
            $('#masach').val(id);
            $('#tensach').val(response.tensach);
            $('#giaban').val(response.giaban);
            $('#slton').val(response.soluongton);
            $('#chude').append(new Option(response.tenChuDe, response.maCd, true, true)).trigger('change');
            $('#nhaxuatban').append(new Option(response.tenNxb, response.maNxb, true, true)).trigger('change');
            var options = [];
            response.dstacgia.forEach(function (item) {
                options.push(new Option(item.tenTg, item.maTg, true, true));
            });
            $('#tacgias').append(options).trigger('change');


            var defaultImageUrl = '/Images/' + response.anhbia;
            $('#previewImage').attr('src', defaultImageUrl).show();
            $('#clearImageButton').show();
            $('#exampleModal').modal('show');
        },

        error: function (xhr, status, error) {
            alert('Lỗi khi gửi dữ liệu!');
            console.log(error);
        }
    });
}
function setThem() {
    $('#masach').val(0);
}