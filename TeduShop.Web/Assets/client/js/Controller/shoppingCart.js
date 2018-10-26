﻿var cart = {
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
            cart.getLoginUser();
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
                    alert('XÓa giỏ hàng thành công.');
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