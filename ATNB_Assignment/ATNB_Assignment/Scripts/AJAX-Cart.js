/*!
 * Written by TanPTN
 * Do not copy this shiet
 */

$(window).on('load', function () {
    $.ajax({
        type: "GET",
        url: '/Store/GetAmountItems',
        context: this,
        success: function (data) {
            $("#amountItems").html(data);
            if (data == 0) {
                $("#tableItems").hide();
                $("#btn_customerInfo").hide();
                $("#noItems").show();
            } else {
                $("#noItems").hide();
                $("#tableItems").show();
                $("#btn_customerInfo").show();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('The following error occured: ' + textStatus, errorThrown);
        }
    })
});

$(document).ready(function () {
    $("#AddCart_btn").click(function (e) {
        e.preventDefault();
        var id = $('#BookId').val();
        var quantity = $('#quantity').val();
        console.log(id);
        console.log(quantity);
        $.ajax({
            type: "POST",
            url: "/Store/AddCustomerCart",
            data: { id, quantity },
            success: function (response) {
                alert('Add to cart Success :D');
                location.reload();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('The following error occured: ' + textStatus, errorThrown);
            }
        });
    });
});

$(document).ready(function () {
    $("#btn_info").on("click", function () {
        alert('Order Success');
    });

});
