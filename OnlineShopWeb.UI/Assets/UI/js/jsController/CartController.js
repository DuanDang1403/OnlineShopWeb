﻿var cart = {
    init: function () {
        cart.regEvents();
        },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href='/';
        });
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = '/thanh-toan';
        });
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');
            var cartItem = [];
            $.each(listProduct, function (i, item) {
                cartItem.push({
                    Quantity: $(item).val(),
                    Product: {
                        ProductID: $(item).data('id')
                    }
                });
            });
            $.ajax({
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartItem) },
                dataType: 'Json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href='/gio-hang'
                    }
                }
            }) 


        });
        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: '/Cart/DeleteAll',               
                dataType: 'Json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang'
                    }
                }
            }) 
           
        });

        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault(),
                $.ajax({
                    url: '/Cart/Delete',
                    data: { id: $(this).data('id') },
                dataType: 'Json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = '/gio-hang'
                    }
                }
            })

        });
    }

}
cart.init()