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
        destravarTelaRecarregar
        $.blockUI({
            message: msg
        })
    }

    function TestaCPF(strCPF) {
        var Soma;
        var Resto;
        Soma = 0;
        if (strCPF == "00000000000")
            return false;
        for (i = 1; i <= 9; i++)
            Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
        Resto = (Soma * 10) % 11; if ((Resto == 10) || (Resto == 11)) Resto = 0; if (Resto != parseInt(strCPF.substring(9, 10))) return false; Soma = 0; for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i); Resto = (Soma * 10) % 11; if ((Resto == 10) || (Resto == 11)) Resto = 0; if (Resto != parseInt(strCPF.substring(10, 11))) return false; return true;
    }

    function validarCampos(rg, cpf, nome, tipo) {
        rg = rg.trim();

        if (tipo.trim() == null || tipo.trim() == "") {
            return false;
        }

        if (nome.trim() == null || nome.trim() == "") {
            return false;
        }

        if (rg.indexOf("-") > -1 && rg.indexOf(".") > -1) {
            if (rg.length != 12) {
                return false;
            }
        }
        else {
            if (rg.length != 9)
                return;
        }

        while (rg.indexOf("-") > 0 || rg.indexOf(".") > 0) {
            rg = rg.replace("-", "");
            rg = rg.replace(".", "");
        }

        if (isNaN(rg)) {
            return false;
        }

        return true;
    }



    $("#txteditarcpf").prop('disabled', true);
    $(".cpf").mask("000.000.000-00");
    $(".rg").mask("00.000.000-0");


    $(".limpar").on("click", function (e) {
        e.preventDefault();
        $("#txtcpf").val("");
        $("#txtnome").val("");
        $("#txtrg").val("");//voce parou aqui
        $("#combotipoCliente")[0].selectedIndex = -1;
    });

    $(".limparModificar").on("click", function (e) {
        e.preventDefault();
        $("#txteditarnome").val("");
        $("#txteditarrg").val("");
        $("#editarcombotipoCliente")[0].selectedIndex = -1;
    });


    $(".salvar").on("click", function (e) {
        e.preventDefault();

        if (!validarCampos($("#txtrg").val(), $("#txtcpf").val(), $("#txtnome").val(), $("#combotipoCliente").val())) {
            exibirMensagem('<h3>Campos inválidos<br><br>\
            <button onclick="destravarTela()">OK</button></h3>');

            return;
        }

        var fullName = $("#txtnome").val();
        var firstName = fullName.substr(0, fullName.indexOf(" "));
        var lastName = fullName.substr(fullName.indexOf(" ") + 1, fullName.length);

        var client = {
            CPF: $("#txtcpf").val(), FirstName: firstName, LastName: lastName
            , RG: $("#txtrg").val(), ClientTypeID: $("#combotipoCliente").val()
        };

        $.ajax({
            type: "POST",
            url: "EfetivarCadastro",
            data: client,
            success: function (response) {
                if (response.exceptionText == null) {

                    exibirMensagem('<h3>O Cliente foi Incluído com êxito<br><br>\
                              <button onclick="destravarTela()">OK</button></h3>');
                }
                else {
                    exibirMensagem('<h3>Ocorreu um erro na Inclusao do cliente:'
                               + response.exceptionText + '<br><br>\
                                  <button onclick="destravarTela()">OK</button></h3>');
                }
            },
            error: function () {
                message: '<h3>Erro na comunicação com o servidor <br>\
                          <button onclick="destravarTela()">OK</button></h3>'
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
        $("#txteditarcpf").val($(colunas[2]).html());

        $("#editarcombotipoCliente option").each(function () {
            if ($(this).text() == $(colunas[4]).html()) {
                $(this).attr('selected', true);
                return;
            }
        });

        $("#txteditarrg").val($(colunas[3]).html());
        $("#txteditarnome").val($(colunas[0]).html() + " " + $(colunas[1]).html());
    });

    $(".pesquisar").on("click", function (e) {
        e.preventDefault();
        var fullName = $("#txtnome").val();
        var firstName = fullName.substr(0, fullName.indexOf(" "));
        var lastName = fullName.substr(fullName.indexOf(" ") + 1, fullName.length);



        var client = {
            CPF: $("#txtcpf").val(), FirstName: firstName, LastName: lastName
          , RG: $("#txtrg").val(), ClientTypeID: $("#combotipoCliente").val()
        };

        $.ajax({
            type: "POST",
            url: "Pesquisar",
            data: client,
            success: function (grid) {
                $(".gridConteudo").html(grid);
                $(".webGrid").find("tr").on("click", function () {

                    if (selectedRow != undefined) {
                        $(selectedRow).removeClass("selected");
                    }

                    $(this).addClass("selected");
                    selectedRow = $(this);

                    var colunas = $(this).find("td");
                    $("#txteditarcpf").val($(colunas[2]).html());
                    $("#editarcombotipoCliente option").each(function () {
                        if ($(this).text() == $(colunas[4]).html()) {
                            $(this).attr('selected', true);
                            return;
                        }
                    });
                    $("#txteditarrg").val($(colunas[3]).html());
                    $("#txteditarnome").val($(colunas[0]).html() + " " + $(colunas[1]).html());
                });

            },
            dataType: "html"
        });
    });

    $(".alterar").on("click", function (e) {

        e.preventDefault();

        if (!validarCampos($("#txteditarrg").val(), $("#txteditarcpf").val(), $("#txteditarnome").val(), $("#editarcombotipoCliente").val())) {
            exibirMensagem('<h3>Campos inválidos para edição<br><br>\
            <button onclick="destravarTela()">OK</button></h3>');
            return;
        }

        exibirMensagem('<h3>Deseja Mesmo efetuar a alteração ?<br><br>\
        <button class="confirmUpdate">Sim</button>&nbsp<button onclick="destravarTela()">Não</button></h3>');


        $(".confirmUpdate").on("click", function () {

            destravarTela();

            var fullName = $("#txteditarnome").val();
            var firstName = fullName.substr(0, fullName.indexOf(" "));
            var lastName = fullName.substr(fullName.indexOf(" ") + 1, fullName.length);

            var client = {
                CPF: $("#txteditarcpf").val(), FirstName: firstName, LastName: lastName
              , RG: $("#txteditarrg").val(), ClientTypeID: $("#editarcombotipoCliente").val()
            };


            $.ajax({
                type: "POST",
                url: "ConfirmarAlteracao",
                data: client,
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

        if ($("#txteditarcpf").val().trim() == "") {
            exibirMensagem('<h3>O CPF do client nao pode estar nulo para efetuar a alteração, por favor selecione uma linha válida<br><br>\
            <button onclick="destravarTela()">OK</button></h3>');
            return;
        }

        exibirMensagem('<h3>Deseja Mesmo efetuar a exclusao ?<br><br>\
        <button class="confirmDelete">Sim</button>&nbsp<button onclick="destravarTela()">Não</button></h3>');


        $(".confirmDelete").on("click", function () {

            destravarTela();

            e.preventDefault();
            var fullName = $("#txteditarnome").val();
            var firstName = fullName.substr(0, fullName.indexOf(" "));
            var lastName = fullName.substr(fullName.indexOf(" ") + 1, fullName.length);

            var client = {
                CPF: $("#txteditarcpf").val(), FirstName: firstName, LastName: lastName
              , RG: $("#txteditarrg").val(), ClientTypeID: $("#editarcombotipoCliente").val()
            };

            $.ajax({
                type: "POST",
                url: "ConfirmarExclusao",
                data: client,
                success: function (msg) {
                    if (msg.success) {
                        exibirMensagem('<h3>Exclusao efetuada com êxito<br><br>\
                    <button onclick="destravarTelaRecarregar()">OK</button></h3>');
                    }

                    else {
                        exibirMensagem('<h3> Ocorreu um erro durante a Exclusão:' + msg.exceptionText + '<br>\
                    <button onclick="destravarTela()">OK</button></h3>');

                    }
                },
                error: function () {
                    exibirMensagem('<h3> Erro na comunicação com o servidor<br>\
                 <button onclick="destravarTela()">OK</button></h3>');
                },
                dataType: "json"
            });
        });
    });


})