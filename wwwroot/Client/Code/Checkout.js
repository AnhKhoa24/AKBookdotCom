document.addEventListener("DOMContentLoaded", function () {
    $("#div-footer").removeClass("d-none");
});

function submitOrder() {
    // Lấy input radio được chọn
    const selectedPayment = document.querySelector('input[name="payment"]:checked');

    // Kiểm tra xem có lựa chọn nào không
    if (selectedPayment) {
        const checkoutType = selectedPayment.id;

        // Tạo một form ảo để gửi dữ liệu
        const form = document.createElement('form');
        form.method = 'POST';
        form.action = '/Home/Order';

        // Tạo input cho checkoutType
        const checkoutTypeInput = document.createElement('input');
        checkoutTypeInput.type = 'hidden';
        checkoutTypeInput.name = 'checkoutType';
        checkoutTypeInput.value = checkoutType;

        // Thêm input vào form
        form.appendChild(checkoutTypeInput);

        // Thêm form vào body và submit
        document.body.appendChild(form);
        form.submit();
    } else {
        alert("Vui lòng chọn một phương thức thanh toán.");
    }
}