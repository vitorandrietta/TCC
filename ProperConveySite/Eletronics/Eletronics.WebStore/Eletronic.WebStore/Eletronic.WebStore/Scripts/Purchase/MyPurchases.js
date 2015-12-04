function destravarTela() {
    $.unblockUI();
}

$(function () {

    var currentLine;

    $(".delete").on("click", function () {


        if (currentLine == null) {
            exibirMensagem('<h3>Você deve selecionar um produto primeiramente antes deletá-lo<br><br>\
            <button onclick="destravarTela()">OK</button></h3>');
            return;
        }




        try {
            var purchase = { PurchaseID: $($(currentLine).find("td")[5]).html().trim() };
        }
        finally {
            if (purchase == null || purchase.PurchaseID == null || isNaN(purchase.PurchaseID) || purchase.PurchaseID == "") {
                exibirMensagem('<h3>Você deve selecionar um produto primeiramente antes deletá-lo<br><br>\
            <button onclick="destravarTela()">OK</button></h3>');
                return;
            }
        }



        exibirMensagem('<h3>Deseja cacelar a compra selecionada ?<br><br>\
        <button class="confirmDelete">Sim</button>&nbsp<button onclick="destravarTela()">Nao</button></h3>');

        $(".confirmDelete").on("click", function () {

            destravarTela();

            $.ajax({
                type: "POST",
                url: "CancelPurchase",
                data: purchase,
                success: function (msg) {

                    if (msg.sucessText) {
                        exibirMensagem('<h3> Compra cancelada com êxito <br><br>\
                        <button onclick="destravarTela()">OK</button></h3>');
                        location.reload();
                    }

                    else {
                        exibirMensagem('<h3> Erro ao efetuar o cancelamento da compra <br><br>\
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

    function exibirMensagem(msg) {
        $.blockUI({
            message: msg
        })
    }


    $("#gridcontent").on("click", "tr", function (e) {
        e.preventDefault();
        if (currentLine != null) {
            $(currentLine).css("background-color", "#ffffff");
        }
        currentLine = $(this);
        $(this).css("background-color", "#ADD8E6");
    })

});