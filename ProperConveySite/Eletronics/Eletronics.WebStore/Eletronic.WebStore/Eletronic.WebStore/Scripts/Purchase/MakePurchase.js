function destravarTela() {
    $.unblockUI();
}

$(function () {

    function exibirMensagem(msg) {
        $.blockUI({
            message: msg
        })
    }

    $(".comprar").on("click", function (e) {
        e.preventDefault();



        var form = $(this).parent();
        var productID = $(this).attr("productid");
        var productQuantity = $($($(form).find("div[name='quantity']")[0]).find("input[name='product_quantity']")[0]).val().trim();


        if (productQuantity == "" || isNaN(productQuantity)) {
            exibirMensagem('<h3> Quantidade inválida de produto <br><br>\
            <button onclick="destravarTela()">OK</button></h3>');
            return;
        }

        var purchase = {
            ProductID: productID,
            PurchaseStatus: 1,
            ProductQuantity: productQuantity
        };

        exibirMensagem('<h3>Confirmar compra<br><br>\
         <button class="confirmPurchase">Sim</button>&nbsp<button onclick="destravarTela()">Nao</button></h3>');

        $(".confirmPurchase").on("click", function () {

            $.ajax({
                type: "POST",
                url: "MakePurchase",
                data: purchase,
                success: function (msg) {

                    if (msg.sucessText) {
                        exibirMensagem('<h3> Compra efeutada com êxito <br><br>\
                    <button onclick="destravarTela()">OK</button></h3>');
                        location.reload();
                    }

                    else {

                        exibirMensagem('<h3> Erro ao efetuar a compra <br><br>\
                                <button onclick="destravarTela()">OK</button></h3>');
                    }

                },

                error: function () {
                    exibirMensagem('<h3> Erro na comunicação do servidor <br><br>\
                                <button onclick="destravarTela()">OK</button></h3>');
                },

                dataType: "json"

            });
        });
    });

});