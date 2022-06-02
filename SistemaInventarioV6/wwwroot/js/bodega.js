var dataTable;
$(document).ready(function () {
    loadDataTable();
});

$("#btnNew").click(function () {
    $("#ventana_modal_body").load("/Admin/Bodega/Upsert", function () {
        $("#ventana_modal").modal("show");
        $("#ventana_modal_title").text("Bodega");
    });
});

function Edit(urlUpsert) {
    $("#ventana_modal_body").load(urlUpsert, function () {
        $("#ventana_modal").modal("show");
        $("#ventana_modal_title").text("Editar Bodega");
    });
}

function loadDataTable() {
    dataTable = $("#tblDatos").DataTable({
        "ajax": {
            "url": "/Admin/Bodega/ObtenerTodos"
        },
        "language": {
            "url": "/lib/AdminLTE-3.2.0/plugins/datatables/es_es.json"
        },
        "columns": [
            { "data": "nombre", "width": "20%" },
            { "data": "descripcion", "width": "40%" },
            {
                "data": "estado",
                "render": function (data) {
                    let estado = (data == true) ? "Activo" : "Inactivo";
                    return `${estado}`;
                },
                "width": "20%``"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="text-center">
                            <a href="#" onclick=Edit("/Admin/Bodega/Upsert/${data}") class="btn bg-gradient btn-primary text-white"><i class="fas fa-edit"></i></a>
                            <a href="#" onclick=Delete("/Admin/Bodega/Delete/${data}") class="btn bg-gradient btn-danger text-white"><i class="fas fa-trash-alt"></i></a>
                        </div>
                    `;
                }, "width": "20%"
            }
        ]
    });
}

function Delete(url) {
    new swal({
        title: "¿Desea eliminar la bodega?",
        text: "Este registro no se podrá recuperar",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: 'Borrar',
        cancelButtonText: 'Cancelar',
        dangermode: true
    }).then((borrar) => {
        if (borrar.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}