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
            //console.log(data);
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
                //console.log(response);
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

//var cartItems = function () {
//    $.ajax({
//        type: "GET",
//        dataType: "json",
//        url: 'http://localhost:53281/Cart.json',
//        context: this,
//        data: data = JSON.stringify(),
//        success: function (response) {
//            var rsdt = response.data;
//            //var sell_list = rsdt.filter(data => data.type == 'sell');
//            drawTable(rsdt);
//            $("#cart-table").DataTable(rsdt);

//        },
//        error: function (jqXHR, textStatus, errorThrown) {
//            console.log('The following error occured: ' + textStatus, errorThrown);
//            alert('Cant get sell list');
//        }
//    })
//}

//function drawTable(data) {
//    for (var i = 0; i < data.length; i++) {
//        drawSellRow(data[i]);
//    }
//}

//function drawRow(data) {
//    var row = $("<tr />");
//    $("#sell_table tbody").append(row);
//    row.append($("<td style = text-align:center >" + $("#sell_table tbody tr").length + "</td>"));
//    row.append($("<td style = text-align:center >" + data.title + "</td>"));
//    row.append($("<td style = text-align:center >" + data.quantity + "</td>"));
//    row.append($("<td style = text-align:center >" + data.price + "</td>"));
//    row.append($("<td style = text-align:center > </td>"));
//    row.append($("<td style = text-align:center >" + "<div class=\"col-sm-12 hidden-xs\"" + ">"
//        + "<button class=\"btn waves-effect waves-light btn-success logged-in sell-btn\"" + " data-toggle=\"modal\" data-target=\"#detail\" onclick=\"showSellDetail(" + data.id + ");\">"
//        + "Buy" + "</button>" + "</div>" + "</td>"));
//}