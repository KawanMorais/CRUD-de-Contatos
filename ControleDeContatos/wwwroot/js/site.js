


$(document).ready(function () {
    getDatatable('#table-contatos', "Nenhum contato encontrado no sistema.");

    getDatatable('#table-usuarios', "Nenhum usuário foi encontrado.");
});

function getDatatable(id, mensagemVazio) {
    let table = new DataTable(id, {
        ordering: true,
        paging: true,
        searching: true,
        language: {
            emptyTable: mensagemVazio,
            info: "Mostrar _START_ até _END_ de _TOTAL_ registros",
            infoEmpty: "Mostrar 0 até 0 de 0 registros",
            infoFiltered: "(Filtrar de _MAX_ total registros)",
            thousands: ".",
            lengthMenu: "Mostrar _MENU_ registros por página",
            loadingRecords: "Carregando...",
            processing: "Processando...",
            zeroRecords: "Nenhum registro encontrado",
            search: "Pesquisar",
            paginate: {
                next: "Próximo",
                previous: "Anterior",
                first: "Primeiro",
                last: "Último"
            },
            aria: {
                sortAscending: ": Ordenar colunas de forma ascendente",
                sortDescending: ": Ordenar colunas de forma descendente"
            }
        }
    });
}



$('.close-alert').click(function () {
    $('.alert').hide('hide');
});


