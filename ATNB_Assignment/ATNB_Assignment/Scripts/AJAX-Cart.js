/*!
 * Written by TanPTN
 * Do not copy this shiet
 */

$(document).ready(function () {
    $("#AddCart_btn").click(function (e) {
        e.preventDefault();
        var data = {
            "title": $('#title').val(),
            "price": $('#span-price').val(),
            "quantity": $('#quantity').val(),
            "payment_method": $('#method_payment_sell').val()
        }
        console.log(data);
        $.ajax({
            type: "POST",
            url: "http://localhost:53281/Cart.json",
            data: data,
            success: function (result) {
                console.log(result);
                alert('Add to cart Success :D');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('The following error occured: ' + textStatus, errorThrown);
            }
        });
        location.reload(true);
    });
});

var cartItems = function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: 'http://localhost:53281/Cart.json',
        context: this,
        data: data = JSON.stringify(),
        success: function (response) {
            var rsdt = response.data;
            var sell_list = rsdt.filter(data => data.type == 'sell');
                drawTable(sell_list);
                $("#sell_table").DataTable(sell_list);

        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log('The following error occured: ' + textStatus, errorThrown);
            alert('Cant get sell list');
        }
    })
}

function drawTable(data) {
    for (var i = 0; i < data.length; i++) {
        drawSellRow(data[i]);
    }
}

function drawRow(data) {
    var row = $("<tr />");
    $("#sell_table tbody").append(row);
    row.append($("<td style = text-align:center >" + $("#sell_table tbody tr").length + "</td>"));
    row.append($("<td style = text-align:center >" + data.title + "</td>"));
    row.append($("<td style = text-align:center >" + data.quantity + "</td>"));
    row.append($("<td style = text-align:center >" + data.price + "</td>"));
    row.append($("<td style = text-align:center > </td>"));
    row.append($("<td style = text-align:center >" + "<div class=\"col-sm-12 hidden-xs\"" + ">"
        + "<button class=\"btn waves-effect waves-light btn-success logged-in sell-btn\"" + " data-toggle=\"modal\" data-target=\"#detail\" onclick=\"showSellDetail(" + data.id + ");\">"
        + "Buy" + "</button>" + "</div>" + "</td>"));
}