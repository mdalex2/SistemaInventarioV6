var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblDatos").DataTable({
        "ajax": {
            "url": "/Admin/Categoria/ObtenerTodos"
        },
        "language": {
            "url": "/lib/AdminLTE-3.2.0/plugins/datatables/es_es.json"
        },
        "columns": [
            { "data": "nombre", "width": "20%" },
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
                            <a href="/Admin/Categoria/Upsert/${data}" class="btn bg-gradient btn-primary text-white"><i class="fas fa-edit"></i></a>
                            <a href="#" onclick=Delete("/Admin/Categoria/Delete/${data}") class="btn bg-gradient btn-danger text-white"><i class="fas fa-trash-alt"></i></a>
                        </div>
                    `;
                }, "width": "20%"
            }
        ]
    });
}

function Delete(url) {
    new swal({
        title: "¿Desea eliminar la categoria?",
        text: "Este registro no se podrá recuperar",
        icon: "question",
        showCancelButton: true,
        confirmButtonText: 'Borrar',
        cancelButtonText: 'Cancelar',
        dangermode: true
    }).then((borrar) => {
        if (borrar) {
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