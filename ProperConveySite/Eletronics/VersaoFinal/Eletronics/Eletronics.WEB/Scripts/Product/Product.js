function destravarTela() {
    $.unblockUI();
}

function destravarTelaRecarregar() {

    destravarTela();
    location.reload();
}

$(function () {

    var selectedRow;

    function exibirMensagem(msg) {
        $.blockUI({
            message: msg
        })
    }


    function validarCampos(productName, productDescription, supplier, quantidade) {

        if (quantidade == null || quantidade.trim() == "") {
            return false;
        }

        if (productName.trim() == null || productName.trim() == "") {
            return false;
        }

        if (productDescription.trim() == null || productDescription.trim() == "") {
            return false;
        }

        if (supplier.trim() == null || supplier.trim() == "") {
            return false;
        }

        return true;
    }


    $("#editarproductid").prop('disabled', true);


    $(".limpar").on("click", function (e) {

        e.preventDefault();
        $("#txtproductname").val("");
        $("#productid").val(0); // pode bugar
        $("#quantityavaiable").val(0);
        $("#txtproductdescription").val("");
        $("#combofornecedor")[0].selectedIndex = -1;
    });

    $(".limparModificar").on("click", function (e) {
        e.preventDefault();
        $("#txteditarproductname").val("");
        $("#editarproductid").val(0); // pode bugar
        $("#editarquantityavaiable").val(0);
        $("#txteditarproductdescription").val("");
        $("#editarcombofornecedor")[0].selectedIndex = -1;
    });


    $(".salvar").on("click", function (e) {
        e.preventDefault();
        //productName, productDescription, supplier
        if (!validarCampos($("#txtproductname").val(), $("#txtproductdescription").val(), $("#combofornecedor").val(), $("#quantityavaiable").val())) {
            exibirMensagem('<h3>Campos inválidos<br><br>\
            <button onclick="destravarTela()">OK</button></h3>');
            return;
        }



        var product = {
            ProductName: $("#txtproductname").val(),
            ProductDescription: $("#txtproductdescription").val(),
            AvaiableQuantity: $("#quantityavaiable").val(),
            SupplierId: $("#combofornecedor").val()
        };

        $.ajax({
            type: "POST",
            url: "EfetivarCadastro",
            data: product,
            success: function (response) {
                if (response.exceptionText == null) {

                    exibirMensagem('<h3>O Produto foi Incluído com êxito<br><br>\
                                   <button onclick="destravarTela()">OK</button></h3>');
                }
                else {
                    exibirMensagem('<h3>Ocorreu um erro na Inclusao do Produto:'
                    + response.exceptionText + '<br><br>\
                    <button onclick="destravarTela()">OK</button></h3>');
                }
            },
            error: function () {
                exibirMensagem('<h3>Erro na comunicação com o servidor <br><br>\
                          <button onclick="destravarTela()">OK</button></h3>');
            },
            dataType: "json"
        });
    });




    $(".webGrid").find("tr").on("click", function () {

        if (selectedRow != undefined) {

            $(selectedRow).removeClass("selected");

        }

        $(this).addClass("selected");
        selectedRow = $(this);

        var colunas = $(this).find("td");

        $("#editarproductid").val($(colunas[0]).html().trim())//CERTEZA DESSE PARSEINT
        $("#txteditarproductname").val($(colunas[1]).html())
        $("#txteditarproductdescription").val($(colunas[2]).html());
        $("#editarquantityavaiable").val($(colunas[3]).html().trim())//CERTEZA DESSE PARSEINT
        $("#editarcombofornecedor option").each(function () {
            if ($(this).text() == $(colunas[5]).html()) {
                $(this).attr('selected', true);
                return;
            }
        });

    });

    $(".pesquisar").on("click", function (e) {
        e.preventDefault();

        var avaiableQuantity;

        if ($("#quantityavaiable").val() == "" || $("#quantityavaiable").val() == null) {

            avaiableQuantity = -1
        }

        else {
            avaiableQuantity = $("#quantityavaiable").val();
        }

        var product = {
            ProductName: $("#txtproductname").val(),
            ProductDescription: $("#txtproductdescription").val(),
            AvaiableQuantity: avaiableQuantity,
            SupplierId: $("#combofornecedor").val(),
            ProductID: $("#productid").val()

        };



        $.ajax({
            type: "POST",
            url: "Pesquisar",
            data: product,
            success: function (grid) {
                $(".gridConteudo").html(grid);
                $(".webGrid").find("tr").on("click", function () {

                    if (selectedRow != undefined) {

                        $(selectedRow).removeClass("selected");

                    }

                    $(this).addClass("selected");
                    selectedRow = $(this);

                    var colunas = $(this).find("td");

                    $("#editarproductid").val($(colunas[0]).html().trim())//CERTEZA DESSE PARSEINT
                    $("#txteditarproductname").val($(colunas[1]).html())
                    $("#txteditarproductdescription").val($(colunas[2]).html());
                    $("#editarquantityavaiable").val($(colunas[3]).html().trim())//CERTEZA DESSE PARSEINT
                    $("#editarcombofornecedor option").each(function () {
                        if ($(this).text() == $(colunas[5]).html()) {
                            $(this).attr('selected', true);
                            return;
                        }
                    });

                });
            },
            dataType: "html"
        });
    });

    $(".alterar").on("click", function (e) {

        e.preventDefault();
        if (!validarCampos($("#txteditarproductname").val(), $("#txteditarproductdescription").val(), $("#editarcombofornecedor").val(), $("#editarquantityavaiable").val())) {
            exibirMensagem('<h3>Campos inválidos<br><br>\
            <button onclick="destravarTela()">OK</button></h3>');
            return;
        }

        if ($("#editarproductid").val().trim() == "" || $("#editarproductid").val().trim() == null) {
            exibirMensagem('<h3>Campos inválidos<br><br>\
            <button onclick="destravarTela()">OK</button></h3>');
            return;
        }

        exibirMensagem('<h3>Deseja Mesmo efetuar a alteração ?<br><br>\
        <button class="confirmUpdate">Sim</button>&nbsp<button onclick="destravarTela()">Não</button></h3>');

        $(".confirmUpdate").on("click", function () {

            destravarTela();

            var product = {
                ProductName: $("#txteditarproductname").val(),
                ProductDescription: $("#txteditarproductdescription").val(),
                AvaiableQuantity: $("#editarquantityavaiable").val(),
                SupplierId: $("#editarcombofornecedor").val(),
                ProductID: $("#editarproductid").val()
            };


            $.ajax({
                type: "POST",
                url: "ConfirmarAlteracao",
                data: product,
                success: function (msg) {

                    if (msg.success) {
                        exibirMensagem('<h3>Alteração efetuada com êxito<br><br>\
                    <button onclick="destravarTelaRecarregar()">OK</button></h3>');
                    }

                    else {
                        exibirMensagem('<h3> Ocorreu um erro durante a alteração:' + msg.exceptionText + '<br><br>\
                    <button onclick="destravarTela()">OK</button></h3>');

                    }
                },
                error: function () {
                    exibirMensagem('<h3> Erro na comunicação com o servidor<br><br>\
                 <button onclick="destravarTela()">OK</button></h3>');
                },
                dataType: "json"
            });

        });
    });

    $(".excluir").on("click", function (e) {
        e.preventDefault();

        if ($("#editarproductid").val().trim() == "" || $("#editarproductid").val().trim() == null) {
            exibirMensagem('<h3>Campos inválidos<br><br>\
            <button onclick="destravarTela()">OK</button></h3>');
            return;
        }

        exibirMensagem('<h3>Deseja Mesmo efetuar a exclusão ?<br><br>\
        <button class="confirmDelete">Sim</button>&nbsp<button onclick="destravarTela()">Não</button></h3>');

        $(".confirmDelete").on("click", function () {

            destravarTela();

            var product = {
                ProductName: $("#txteditarproductname").val(),
                ProductDescription: $("#txteditarproductdescription").val(),
                AvaiableQuantity: $("#editarquantityavaiable").val(),
                SupplierId: $("#editarcombofornecedor").val(),
                ProductID: $("#editarproductid").val()
            };


            $.ajax({
                type: "POST",
                url: "ConfirmarExclusao",
                data: product,
                success: function (msg) {
                    if (msg.success) {
                        exibirMensagem('<h3>Exclusao efetuada com êxito<br><br>\
                    <button onclick="destravarTelaRecarregar()">OK</button></h3>');
                    }

                    else {
                        exibirMensagem('<h3> Ocorreu um erro durante a Exclusão:' + msg.exceptionText + '<br><br>\
                    <button onclick="destravarTela()">OK</button></h3>');

                    }
                },
                error: function () {
                    exibirMensagem('<h3> Erro na comunicação com o servidor<br><br>\
                 <button onclick="destravarTela()">OK</button></h3>');
                },
                dataType: "json"
            });

        });
    });


})