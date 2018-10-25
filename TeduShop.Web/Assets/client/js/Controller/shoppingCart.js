var cart = {
    init: function () {
        
        cart.loadData();
        cart.registerEvent();
    },
    registerEvent: function () {
        $('#btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            
            var productId = parseInt($(this).data('id'));
            alert(productId);
            cart.addItem(productId);
        });
        $('.btnDeleteItem').off('click').on('click', function () {

            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);
        });
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
                            Quantity: item.Quantity,
                            Amount: item.Quantity * item.Product.Price
                        });
                        
                    });
                    $('#cartBody').html(html);

                    cart.registerEvent();
                }
            }
        });
    }
}
cart.init();