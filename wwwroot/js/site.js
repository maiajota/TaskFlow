function getDataAtual() {
    var dataAtual = new Date;
    var ano = dataAtual.getFullYear();
    var mes = (dataAtual.getMonth()).toString().padStart(2, '0');
    var dia = dataAtual.getDate().toString().padStart(2, '0');

    return dataFormatada = new Date(ano, mes, dia);
}

function getDataElemento(textoElemento) {
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

function  atualizarTabelaTarefas() {
    $.ajax({
        url: '/Home/AtualizarTabelaTarefas',
        type: 'GET',
        success: function(html) {
            $("#tabela-tarefas").html(html)
        }
    })
}

$("#input-cadastro-titulo").on("keydown", function(e) {
    var regex = /^[a-zA-Z]+$/

    if(!regex.test(e.key)) {
        e.preventDefault();
    }    

})

$("#formulario-cadastro-tarefa").on("submit", function(e) {
    e.preventDefault();

    var formData = new FormData(this);
    var nomeTarefa = formData.get("nome");
    var dataVencimentoTarefa = formData.get("dataVencimento");
    var urgente = formData.get("urgente");

    if(urgente == null)
        urgente = false;
    else
        urgente = true;

    console.log(urgente);
        
    $.ajax({
        url: '/Home/CadastrarTarefa',
        type: 'POST',
        data: {
            nome: nomeTarefa,
            dataVencimento: dataVencimentoTarefa,
            urgente: urgente
        },
        success: function(response) {
            if(response.status == 0) {
                    UIkit.notification({
                        message: response.message,
                        status: 'success',
                        pos: 'bottom-center',
                        timeout: 4000
                    });
            }
            else {
                UIkit.notification({
                    message: response.message,
                    status: 'danger',
                    pos: 'bottom-center',
                    timeout: 4000
                });
            }
            atualizarTabelaTarefas();
        }
    })
})

$("tbody tr:visible").each(function(index) {
    $(this).css('background-color', '');
    if (index % 2 === 0) {
        $(this).addClass('uk-table-striped-row');
    } else {
        $(this).removeClass('uk-table-striped-row');
    }
});

function aplicarFiltros() {
    var textoFiltro = $("#input-filtro").val().toLowerCase();
    var statusSelecionado = $("#filtro-status").val().toLowerCase();

    $("tbody tr").each(function() {
        var nomeTarefa = $(this).children().first().text().toLowerCase();
        var statusTarefa = $(this).children().eq(2).text().toLowerCase().trim();
        
        var passouFiltroTexto = nomeTarefa.includes(textoFiltro);
        var passouFiltroStatus = statusSelecionado === "todos os status" || statusTarefa === statusSelecionado;

        if (passouFiltroTexto && passouFiltroStatus) 
            $(this).show();
        else
            $(this).hide();
    });

    $("tbody tr:visible").each(function(index) {
        $(this).css('background-color', '');
        if (index % 2 === 0)
            $(this).addClass('uk-table-striped-row');
        else
            $(this).removeClass('uk-table-striped-row');
    });
}

$("#input-filtro").on("input", aplicarFiltros);
$("#filtro-status").on("change", aplicarFiltros);

$(".botao-abrir-modal").each(function() {
    $(this).on("click", function() {
        var idTarefa = $(this).data("id-tarefa");
        var nomeTarefa = $(this).parents("tr").find(".nome-tarefa")[0].innerHTML;
        var dataVencimentoTarefa = $(this).parents("tr").find(".data-vencimento-tarefa")[0].innerHTML;

        var partesData = dataVencimentoTarefa.split("/");

        var dataVencimentoFormatada = partesData[2] + "-" + partesData[1] + "-" + partesData[0];
        
        $("#input-id-tarefa").attr("value", idTarefa);
        console.log(idTarefa);
        
        $("#input-editar-tarefa-titulo").val(nomeTarefa);
        $("#input-editar-tarefa-data").val(dataVencimentoFormatada);
    })
})

$("#formulario-editar-tarefa").on("submit", function(e) {
    e.preventDefault();

    var formData = new FormData(this);
    var id = formData.get("id")
    var nomeTarefa = formData.get("nome");
    var dataVencimentoTarefa = formData.get("dataVencimento");

    $.ajax({
        url: '/Home/EditarTarefa',
        type: 'POST',
        data: {
            id: id,
            nome: nomeTarefa,
            dataVencimento: dataVencimentoTarefa
        },
        success: function(response) {
            if(response.status == 0) {
                    UIkit.notification({
                        message: response.message,
                        status: 'success',
                        pos: 'bottom-center',
                        timeout: 4000
                    });
            }
            else {
                UIkit.notification({
                    message: response.message,
                    status: 'danger',
                    pos: 'bottom-center',
                    timeout: 4000
                });
            }
            atualizarTabelaTarefas();
        }
    })
})
