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
    function validarCNPJ(cnpj) {



        cnpj = cnpj.replace(/[^\d]+/g, '');

        if (cnpj == '') return false;

        if (cnpj.length != 14)
            return false;


        if (cnpj == "00000000000000" ||
            cnpj == "11111111111111" ||
            cnpj == "22222222222222" ||
            cnpj == "33333333333333" ||
            cnpj == "44444444444444" ||
            cnpj == "55555555555555" ||
            cnpj == "66666666666666" ||
            cnpj == "77777777777777" ||
            cnpj == "88888888888888" ||
            cnpj == "99999999999999")
            return false;

        // Valida DVs
        tamanho = cnpj.length - 2
        numeros = cnpj.substring(0, tamanho);
        digitos = cnpj.substring(tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0))
            return false;

        tamanho = tamanho + 1;
        numeros = cnpj.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2)
                pos = 9;
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1))
            return false;

        return true;

    }

    function validarCampos(cnpj, corporateName, tipo) {


        if (tipo.trim() == null || tipo.trim() == "") {
            return false;
        }

        if (corporateName.trim() == null || corporateName.trim() == "") {
            return false;
        }

        if (!validarCNPJ(cnpj)) {
            return false;
        }

        return true;
    }



    $("#txteditarcnpj").prop('disabled', true);
    $(".cnpj").mask("00.000.000/0000-00");


    $(".limpar").on("click", function (e) {
        e.preventDefault();
        $("#txtcnpj").val("");
        $("#txtcorporationname").val("");
        $("#combotipofornecedor")[0].selectedIndex = -1;
    });

    $(".limparModificar").on("click", function (e) {
        e.preventDefault();
        $("#txteditarcnpj").val("");
        $("#txteditarcorporationname").val("");
        $("#editarcombotipofornecedor")[0].selectedIndex = -1;
    });


    $(".salvar").on("click", function (e) {
        e.preventDefault();

        if (!validarCampos($("#txtcnpj").val(), $("#txtcorporationname").val(), $("#combotipofornecedor").val())) {
            exibirMensagem('<h3>Campos inválidos<br><br>\
            <button onclick="destravarTela()">OK</button></h3>');
            return;
        }



        var supplier = {
            CNPJ: $("#txtcnpj").val().trim(), SupplierTypeID: $("#combotipofornecedor").val(),
            CorporateName: $("#txtcorporationname").val().trim()
        };

        $.ajax({
            type: "POST",
            url: "EfetivarCadastro",
            data: supplier,
            success: function (response) {
                if (response.exceptionText == null) {

                    exibirMensagem('<h3>O Fornecedor foi Incluído com êxito<br><br>\
                                   <button onclick="destravarTela()">OK</button></h3>');
                }
                else {
                    exibirMensagem('<h3>Ocorreu um erro na Inclusao do fornecedor:'
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
        $("#editarcombotipofornecedor option").each(function () {
            if ($(this).text() == $(colunas[2]).html()) {
                $(this).attr('selected', true);
                return;
            }
        });

        $("#txteditarcorporationname").val($(colunas[0]).html());
        $("#txteditarcnpj").val($(colunas[1]).html());
    });

    $(".pesquisar").on("click", function (e) {
        e.preventDefault();

        var supplier = {
            CNPJ: $("#txtcnpj").val().trim(), SupplierTypeID: $("#combotipofornecedor").val(),
            CorporateName: $("#txtcorporationname").val().trim()
        };

        $.ajax({
            type: "POST",
            url: "Pesquisar",
            data: supplier,
            success: function (grid) {
                $(".gridConteudo").html(grid);
                $(".webGrid").find("tr").on("click", function () {

                    if (selectedRow != undefined) {
                        $(selectedRow).removeClass("selected");
                    }

                    $(this).addClass("selected");
                    selectedRow = $(this);

                    var colunas = $(this).find("td");
                    $("#editarcombotipofornecedor option").each(function () {
                        if ($(this).text() == $(colunas[2]).html()) {
                            $(this).attr('selected', true);
                            return;
                        }
                    });

                    $("#txteditarcorporationname").val($(colunas[0]).html());
                    $("#txteditarcnpj").val($(colunas[1]).html());
                });
            },
            dataType: "html"
        });
    });

    $(".alterar").on("click", function (e) {

        e.preventDefault();


        if (!validarCampos($("#txteditarcnpj").val(), $("#txteditarcorporationname").val(), $("#editarcombotipofornecedor").val())) {
            exibirMensagem('<h3>Campos inválidos para edição<br><br>\
            <button onclick="destravarTela()">OK</button></h3>');
            return;
        }


        exibirMensagem('<h3>Deseja Mesmo efetuar a alteração ?<br><br>\
        <button class="confirmUpdate">Sim</button>&nbsp<button onclick="destravarTela()">Não</button></h3>');



        $(".confirmUpdate").on("click", function () {

        destravarTela();

        var supplier = {
            CNPJ: $("#txteditarcnpj").val().trim(), SupplierTypeID: $("#editarcombotipofornecedor").val(),
            CorporateName: $("#txteditarcorporationname").val().trim()
        };


        $.ajax({
            type: "POST",
            url: "ConfirmarAlteracao",
            data: supplier,
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
                exibirMensagem('<h3> Erro na comunicação co o servidor<br><br>\
                 <button onclick="destravarTela()">OK</button></h3>');
            },
            dataType: "json"
        });

        });
    });

    $(".excluir").on("click", function (e) {
        e.preventDefault();


        if (!validarCNPJ($("#txteditarcnpj").val())) {
            exibirMensagem('<h3>Campos inválidos para alteracao<br><br>\
            <button onclick="destravarTela()">OK</button></h3>');
            return;
        }

        exibirMensagem('<h3>Deseja Mesmo efetuar a exclusão ?<br><br>\
        <button class="confirmRemove">Sim</button>&nbsp<button onclick="destravarTela()">Não</button></h3>');

        $(".confirmRemove").on("click", function () {

            destravarTela();

            var supplier = {
                CNPJ: $("#txteditarcnpj").val().trim(), SupplierTypeID: $("#editarcombotipofornecedor").val(),
                CorporateName: $("#txteditarcorporationname").val().trim()
            };


            $.ajax({
                type: "POST",
                url: "ConfirmarExclusao",
                data: supplier,
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
                    exibirMensagem('<h3> Erro na comunicação co o servidor<br><br>\
                 <button onclick="destravarTela()">OK</button></h3>');
                },
                dataType: "json"
            });

        });
    });

})