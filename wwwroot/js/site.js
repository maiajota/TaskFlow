function GetDataAtual() {
    var dataAtual = new Date;
    var ano = dataAtual.getFullYear();
    var mes = (dataAtual.getMonth()).toString().padStart(2, '0');
    var dia = dataAtual.getDate().toString().padStart(2, '0');

    return dataFormatada = new Date(ano, mes, dia);
}

function GetDataElemento(textoElemento) {
    var partesTextoElemento;
    var dataFormatada;

    if(textoElemento.includes("-")) {
        partesTextoElemento = textoElemento.split("-");
        dataFormatada = new Date(partesTextoElemento[0], partesTextoElemento[1] - 1, partesTextoElemento[2]);
    }
    else {
        partesTextoElemento = textoElemento.split("/");
        dataFormatada = new Date(partesTextoElemento[2], partesTextoElemento[1] - 1, partesTextoElemento[0]);
    }

    return dataFormatada;
}

$("#formulario-cadastro-tarefa").on("submit", function(e) {
    var textoInputDataVencimento = $("#input-data-vencimento").val();

    if(GetDataElemento(textoInputDataVencimento) < GetDataAtual()) {
        e.preventDefault()
        UIkit.notification({
            message: "A data de vencimento não pode ser anterior à data atual.",
            status: 'warning',
            pos: 'bottom-right',
            timeout: 4000
        });
    }
    
})

$("#input-filtro").on("input", function() {
    var textoFiltro = $(this).val().toLowerCase();

    $("tbody tr").each(function() {
        var nomeTarefa = $(this).children().first().text().toLowerCase();
        $(this).toggle(nomeTarefa.includes(textoFiltro))
            
    })
})

$("#checkbox-urgente").on("change", function() {
    $("tbody tr").each(function() {
        var textoDataVencimento = $(this).children().eq(1)[0].innerText;
        var dataAtual = GetDataAtual();
        var dataUrgente = new Date(dataAtual.getUTCFullYear(), dataAtual.getUTCMonth(), dataAtual.getDate() + 2);

        if(GetDataElemento(textoDataVencimento) > dataUrgente) 
            $(this).toggle()
    })
})
