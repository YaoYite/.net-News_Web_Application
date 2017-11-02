//$(document).ready(function () {
//    $('#data').DataTable(
//    );
//});

//$(document).ready(function () {
//    // Setup - add a text input to each footer cell
//    $('#data tfoot th').each(function () {
//        var title = $(this).text();
//        $(this).html('<input type="text" placeholder="Search ' + title + '" />');
//    });

//    // DataTable
//    var table = $('#data').DataTable();

//    // Apply the search
//    table.columns().every(function () {
//        var that = this;

//        $('input', this.footer()).on('keyup change', function () {
//            if (that.search() !== this.value) {
//                that
//                .search(this.value)
//                .draw();
//            }
//        });
//    });
//});
//$(document).ready(function () {
//    var table = $('#data').DataTable();

//    $('#data tbody').on('click', 'tr', function () {
//        $(this).toggleClass('selected');
//    });

//    $('#button').click(function () {
//        alert(table.rows('.selected').data().length + ' row(s) selected');
//    });
//});

function filterGlobal() {
    $('#data').DataTable().search(
    $('#global_filter').val()
    //,
    //$('#global_regex').prop('checked'),
    //$('#global_smart').prop('checked')
    ).draw();
}

function filterColumn(i) {
    $('#data').DataTable().column(i).search(
    $('#col' + i + '_filter').val()
    //,
    //$('#col' + i + '_regex').prop('checked'),
    //$('#col' + i + '_smart').prop('checked')
    ).draw();
}

$(document).ready(function () {
    $('#data').dataTable({
        "oLanguage": {
            "sSearch": "<span>Quick Search</span> _INPUT_" //search
        }
    }
    );

    $('input.global_filter').on('keyup click', function () {
        filterGlobal();
    });

    $('input.column_filter').on('keyup click', function () {
        filterColumn($(this).parents('tr').attr('data-column'));
    });
});

$(document).ready(function () {
    $('#search').hide();
    $('#searchLink').click(function () {
        $('#search').toggle();
    });
})

//function toggleTable() {
//    var lTable = document.getElementById("search");
//    lTable.style.display = (lTable.style.display == "table") ? "none" : "table";
//}