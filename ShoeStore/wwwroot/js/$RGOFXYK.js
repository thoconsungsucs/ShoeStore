let dataTable;
$(document).ready(function () {
    LoadDataTable();
});
function LoadDataTable() {
    dataTable = $('#tbldata').DataTable({
        "ajax": '/shoe/getall',
        "columns": [
            { data: 'shoeName' },
            { data: 'price', "width": '25%' },
            { data: 'categoryName', "width": '25%' },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="btn-group" style="min-width: 150px;">
                        <a href="/SpecificShoe/Upsert/${data}" class="btn btn-secondary" >
                            <i class="bi bi-pencil"></i>
                            Add Specific Shoe
                        </a>
                        <a href="/Shoe/Upsert/${data}" class="btn btn-success" >
                            <i class="bi bi-pencil"></i>
                            Edit
                        </a>
                        <a href="/Shoe/Delete/${data}" class="btn btn-danger" >
                            <i class="bi bi-trash"></i>
                                Delete
                        </a>
                    </div>`
                },
                "width": '25%'
            }
        ]
    },
    );
}