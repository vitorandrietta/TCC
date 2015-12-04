
function encontrarDivs() {
    cont = 1;
    elem = $("#" + cont.toString());
    while (elem != null && elem.length != 0) {
        stringUtil = $(elem[0]).attr('name');
        montarGrafico(stringUtil, cont.toString());
        cont++;
        elem = $("#" + cont.toString());
    }
}

//"(Math.floor(Math.random()*10)+1)+"-"+errorCount+tentativeTimes
function montarGrafico(str, id) {
    strUsavel = str.split("-");
    quantos = parseInt(strUsavel[1]);
    contIn = 1;

    content = [];

    while (contIn <= quantos) {
        intervaloTempo = parseInt(strUsavel[contIn + 1]);
        tri =  contIn.toString();
        content[contIn - 1] = { tentativa: tri, intervalo: intervaloTempo };
        contIn++;
    }



    new Morris.Line({
        element: id,
        data: content,
        xkey: 'tentativa',
        parseTime: false,
        // A list of names of data record attributes that contain y-values.
        ykeys: ['intervalo'],
        // Labels for the ykeys -- will be displayed when you hover over the
        // chart.
        labels: ['Intervalo']
    })

 
}

$("document").ready(function () {

    encontrarDivs();

})