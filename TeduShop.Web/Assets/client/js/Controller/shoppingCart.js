var cart = {
    init: function () {
        cart.loadData();
        cart.registerEvent();
    },
    registerEvent: function () {
        //(cart.getTotalOrder() == null ? '0' : $('#lblTotalOrder').text(cart.getTotalOrder()));

        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.addItem(productId);
        });
        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            //var quantity = parseInt($(this).data('quantity'));
            //var price = parseFloat($(this).data('price'));

            //$('#lblTotalOrder').text(cart.getTotalOrder() - price * quantity);
            cart.deleteItem(productId);
        });
        $('.txtQuantity').off('keyup').on('keyup', function () {//keypress: bam'+enter, keydown: bam' xuong', keyup:bam'
            var productId = parseInt($(this).data('id'));
            var quantity = parseInt($(this).val());
            var price = parseFloat($(this).data('price'));
            if (isNaN(quantity) == false) { //phai la so'
                var amount = quantity * price;
                $('#amount_' + productId).text(numeral(amount).format('0,0'));
            }
            else {
                $('#amount_' + productId).text(0);
            }
            $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));

            cart.updateAll();//moi lan keyup-->cap nhat vo session.
        });

        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });

        $('#btnCheckOut').off('click').on('click', function () {
            $('#divCheckout').show();
        });

        $('#btnDeleteAll').off('click').on('click', function () {
            cart.deleteAll();
        });

        $('#chkUserLoginInfo').off('click').on('click', function () {
            if ($('#chkUserLoginInfo').prop('checked'))
                cart.getLoginUser();
            else {
                $('#txtName').val('');
                $('#txtAddress').val('');
                $('#txtEmail').val('');
                $('#txtPhone').val('');
            }
        });

        $('#btnCreateOrder').off('click').on('click', function () {
            cart.createOrder();
        });
    },

    getLoginUser: function (id) {
        $.ajax({
            url: '/ShoppingCart/GetUser',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    var user = response.data;

                    $('#txtName').val(user.FullName);
                    $('#txtAddress').val(user.Address);
                    $('#txtEmail').val(user.Email);
                    $('#txtPhone').val(user.PhoneNumber);
                }
            }
        });
    },

    createOrder: function (id) {
        var order = {
            CustomerName: $('#txtName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMobile: $('#txtPhone').val(),
            CustomerMessage: $('#txtMessage').val(),
            PaymentMethod: "Thanh toán tiền mặt",
            Status: false
        }
        $.ajax({
            url: '/ShoppingCart/CreateOrder',
            type: 'POST',
            data: {
                orderViewModel: JSON.stringify(order)
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    $('#divCheckOut').hide();
                    cart.deleteAll();
                    setTimeout(function () {
                        $('#cartContent').html('Cảm ơn bạn đã đặt hàng. chúng tôi sẽ liên hệ lại sớm nhất có thể.');
                    }, 2000);

                }
            }
        });
    },

    getTotalOrder: function () {
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        });
        return total;
    },

    addItem: function (id) {
        $.ajax({
            url: '/ShoppingCart/Add',
            data: {
                productId: id
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status)
                    alert('Thêm sản phẩm thành công.');
            }
        });
    },
    deleteAll: function () {
        $.ajax({
            url: '/ShoppingCart/DeleteAll',
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        });
    },

    updateAll: function () {
        var cartList = [];
        $.each($('.txtQuantity'), function (i, item) {
            cartList.push({
                ProductId: $(item).data('id'),
                Quantity: $(item).val()
            });
        });
        $.ajax({
            url: '/ShoppingCart/Update',
            type: 'POST',
            data: {
                cartData: JSON.stringify(cartList)
            },
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    alert('Cập nhật giỏ hàng thành công.');
                    cart.loadData();
                }
            }
        });
    },

    deleteItem: function (id) {
        $.ajax({
            url: '/ShoppingCart/DeleteItem',
            data: {
                productId: id
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status)
                    cart.loadData();
            }
        });
    },

    loadData: function () {
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var template = $('#tblCart').html();
                    var html = '';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ProductId: item.ProductId,
                            ProductName: item.Product.Name,
                            Image: item.Product.Image,
                            Price: item.Product.Price,
                            PriceF: numeral(item.Product.Price).format('0,0'),
                            Quantity: item.Quantity,
                            Amount: numeral(item.Quantity * item.Product.Price).format('0,0')
                        });
                    });
                    $('#cartBody').html(html);
                    if (html == '') {
                        $('#cartContent').html('Không có sản phẩm nào trong giỏ hàng.');
                    }
                    $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
                    cart.registerEvent();
                }
            }
        });
    }
}
cart.init();