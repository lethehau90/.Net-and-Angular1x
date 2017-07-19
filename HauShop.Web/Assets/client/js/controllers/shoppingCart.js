/// <reference path="../jquery.min.js" />
var cart = {
    init: function () {
        cart.registerEvent();
        cart.loadData();
    },
    registerEvent: function () {

        $('#frmPayment').validate({
            rules: {
                name: 'required',
                address: 'required',
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    number: true
                }
            },
            messages: {
                name: 'Yêu cầu nhập tên',
                address: 'Yêu cầu nhập địa chỉ',
                email: {
                    required: "Bạn cần nhập email",
                    email: "định dạng  email chưa đúng"
                },
                phone: {
                    required: "Số điện thoại yêu cầu",
                    number: "Yêu cầu phải là số"
                }
            }
        });
       
        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var productId = parseInt($(this).data('id'));
            cart.deleteItem(productId);
        });
        $('.txtQuantity').off('keyup').on('keyup', function () {
            var quantity = parseInt($(this).val());
            var productid = parseInt($(this).data('id'));
            if (isNaN(quantity) == false) {
                var price = parseFloat($(this).data('price'));
                var amount = numeral(quantity * price).format('0,0');
                $('#amount_' + productid).text(amount)
                cart.updateAll();
            }
            else {
                $('#amount_' + productid).text(0)
            }

            $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format('0,0'));
        })

        $('#btnContinue').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/";
        });

        $('#btnDeleteAll').off('click').on('click', function (e) {
            e.preventDefault();
            cart.deleteAll();
        });

        $('#btnCheckout').off('click').on('click', function (e) {
            e.preventDefault();
            $('#divCheckout').show();
        });

        $('#btnCreateOrder').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid =  $('#frmPayment').valid();
            if (isValid) {
                cart.createOrder();
            }
        });

        $('#chkUserLoginInfo').off('click').on('click', function () {
            if($('#chkUserLoginInfo').prop('checked'))
            {
                cart.getLoginUser();
            }
            else {
                $('#txtName').val('')
                $('#txtAddress').val('')
                $('#txtEmail').val('')
                $('#txtPhone').val('')
                $('#xtxtName').val('')
            }
        });

        $('input[name = "paymentMethod"]').off('click').on('click', function () {
            if ($(this).val() == 'NL') {
                $('.boxContent').hide()
                $("#nganluongContent").show()
            }
            else if ($(this).val() == 'ATM_ONLINE') {
                $('.boxContent').hide()
                $("#bankContent").show()
            }
            else {
                $('.boxContent').hide()
            }
        })
    },

    getLoginUser: function(){
        $.ajax({
            url: '/ShoppingCart/GetUser',
            type: 'POST',
            dataType: 'Json',
            success: function (respone) {
                if (respone.status) {
                    var user = respone.data;
                    $('#txtName').val(user.FullName)
                    $('#txtAddress').val(user.Address)
                    $('#txtEmail').val(user.Email)
                    $('#txtPhone').val(user.FullName)
                    $('#xtxtName').val(user.PhoneNumber)
                }
            }
        })
    },

    createOrder: function () {
        var order = {
            CustomerName: $('#txtName').val(),
            CustomerAddress: $('#txtAddress').val(),
            CustomerEmail: $('#txtEmail').val(),
            CustomerMobile: $('#txtPhone').val(),
            CustomerMessage : $('#txtMessage').val(),
            PaymentMethod: $('input[name = "paymentMethod"]:checked').val(),
            BankCode: $('input[name = "bankcode"]:checked').prop('id'),
            Status : false
        }
        $.ajax({
            url: '/ShoppingCart/CreateOrder',
            type: 'POST',
            dataType: 'Json',
            data: {
                orderViewModel: JSON.stringify(order)
            },
            success: function (respone) {
                if (respone.status) {
                    if (respone.urlCheckout != undefined && respone.urlCheckout != '') {
                        window.location.href =  respone.urlCheckout
                    }
                    else {
                        $('#divCheckout').hide();
                        cart.deleteAll();
                        setTimeout(function () {
                            $('#cartContent').html('Cảm ơn bạn đã đặt hàng thành công. Chúng tôi sẽ liên hệ sớm nhất')
                        }, 2000)
                    }
                }
                else {
                    $('#divMessage').show()
                    $('#divMessage').text(respone.message)
                }
            }
    })
},

    getTotalOrder: function(){
        var listTextBox = $('.txtQuantity');
        var total = 0;
        $.each(listTextBox, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'))
        })
        return total;
    },

    deleteAll: function () {
        $.ajax({
            url: '/shoppingCart/DeleteAll',
            type: 'POST',
            dataType: 'JSON',
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        })
    },

    addItem: function (productId) {
        $.ajax({
            url: '/shoppingCart/Add',
            data: {
                productId: productId
            },
            type: 'POST',
            dataType: 'JSON',
            success: function (response) {
                if (response.status) {
                    alert('Thêm sản phẩm thành công')
                }
                else {
                    alert(response.message)
                }
            }
        })
    },

    deleteItem: function (productId) {
        $.ajax({
            url: '/shoppingCart/DeleteItem',
            data: {
                productId: productId
            },
            type: "POST",
            dataType: "JSON",
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                }
            }
        })
    },

    loadData: function () {
        $.ajax({
            url: '/shoppingcart/GetAll',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var template = $('#tplCart').html();
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
                        })
                    });
                    if (html == '') {
                        $('#cartContent').html('không có sản phẩm nào trong giỏ hàng')
                    }
                    $('#cartBody').html(html);
                    $('#lblTotalOrder').text(numeral(cart.getTotalOrder()).format(0, 0));
                    cart.registerEvent();
                }
            }
        })
    },

    updateAll: function () {
        var cartList = [];

        $.each($('.txtQuantity'), function (i, item) {
            cartList.push({
                ProductId: $(item).data('id'),
                Quantity: $(item).val()
            })
        })

        $.ajax({
            url: '/shoppingCart/Update',
            data: {
                cartData : JSON.stringify(cartList)
            },
            type: "POST",
            dataType: "JSON",
            success: function (response) {
                if (response.status) {
                    cart.loadData();
                    console.log('update ok')
                }
            }
        })
    }
}

cart.init()