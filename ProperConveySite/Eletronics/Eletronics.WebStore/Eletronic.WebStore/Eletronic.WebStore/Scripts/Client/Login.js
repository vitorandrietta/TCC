function destravarTela() {
    $.unblockUI();
}
function destravarTelaRedirecionar(url) {
    $.unblockUI();
    window.location.href = url;
}

$(function () {
    function exibirMensagem(msg) {
        $.blockUI({
            message: msg
        })
    }

    $(".login").on("click", function (e) {

        e.preventDefault();

        if ($("#username").val() == "" || $("#password").val()=="") {

            exibirMensagem('<h3>Preencha todos os campos!<br><br>\
                    <button onclick="destravarTela()">OK</button></h3>');
            return;
        }

        clientLogin = {
            Username: $("#username").val(),
            Password: $("#password").val()
        }

        $.ajax({
            type: "POST",
            url: "Login",
            data: clientLogin,
            success: function (msg) {
                if (msg.sucessText) {
                    exibirMensagem('<h3>' + msg.sucessText + '<br><br>\
                    <button onclick="destravarTelaRedirecionar('+"'/Purchase/Selector'"+')">OK</button></h3>');
                }

                else {
                    exibirMensagem('<h3> ' + msg.exceptionText + '<br><br>\
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