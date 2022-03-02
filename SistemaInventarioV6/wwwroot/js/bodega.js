﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $("#tblDatos").DataTable({
        "ajax": {
            "url": "/Admin/Bodega/ObtenerTodos"
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
                            <a href="/Admin/Bodega/Upsert/${data}" class="btn bg-gradient btn-primary text-white"><i class="fas fa-edit"></i></a>
                            <a href="Admin/Bodega/Delete/${data}" class="btn bg-gradient btn-danger text-white"><i class="fas fa-trash-alt"></i></a>
                        </div>
                    `;
                }, "width": "20%"
            }
        ]
    });
}