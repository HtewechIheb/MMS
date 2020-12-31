var renderedFoldersListItems;
var renderedFoldersDataTableItems;


//DataTables
$.fn.dataTable.moment('DD/MM/YYYY');

var foldersTable =
    $('#folder-dynamic-table')
        .DataTable({
            "dom": "<'row'<'col-xs-6 col-sm-4'l><'hidden-xs col-sm-4 filer_options'><'col-xs-6 col-sm-4'f>>" +
                "tr" +
                "<'row'<'col-12'p>>",
            language: {
                processing: "Traitement en cours...",
                search: "Recherche:",
                lengthMenu: "Élements affichés: _MENU_",
                info: "",
                infoEmpty: "",
                infoFiltered: "",
                infoPostFix: "",
                loadingRecords: "Chargement en cours...",
                zeroRecords: "Aucun &eacute;l&eacute;ment &agrave; afficher",
                emptyTable: "Aucun dossier n'est trouvé.",
                paginate: {
                    first: "Premier",
                    previous: "Pr&eacute;c&eacute;dent",
                    next: "Suivant",
                    last: "Dernier"
                },
                aria: {
                    sortAscending: ": activer pour trier la colonne par ordre croissant",
                    sortDescending: ": activer pour trier la colonne par ordre décroissant"
                }
            },
            bAutoWidth: false,
            "aoColumns": [
                { "bSortable": false },
                { "bSortable": false },
                null,
                { "bSortable": false }
            ],
            "aaSorting": [],
            select: {
                style: 'multi',
                selector: 'td:not(:last-child)'
            }
        });

foldersTable.on('select', function (e, dt, type, index) {
    if (type === 'row') {
        $(foldersTable.row(index).node()).find('input:checkbox').prop('checked', true);
    }
});
foldersTable.on('deselect', function (e, dt, type, index) {
    if (type === 'row') {
        $(foldersTable.row(index).node()).find('input:checkbox').prop('checked', false);
    }
});

//Select/deselect all rows according to table header checkbox
$('#folder-dynamic-table > thead > tr > th input[type=checkbox], #folder-dynamic-table_wrapper input[type=checkbox]').eq(0).on('click', function () {
    var th_checked = this.checked;//checkbox inside "TH" table header

    $('#folder-dynamic-table').find('tbody > tr').each(function () {
        var row = this;
        if (th_checked) foldersTable.row(row).select();
        else foldersTable.row(row).deselect();
    });
});

//Select/deselect a row when the checkbox is checked/unchecked
$('#folder-dynamic-table').on('click', 'td input[type=checkbox]', function () {
    var row = $(this).closest('tr').get(0);
    if (this.checked) foldersTable.row(row).deselect();
    else foldersTable.row(row).select();
});

$(document).on('click', '#folder-dynamic-table .dropdown-toggle', function (e) {
    e.stopImmediatePropagation();
    e.stopPropagation();
    e.preventDefault();
});

$("#folder-dynamic-table_filter input").on("keyup", function () {
    $("#folder-thumbnail-view-search-input").val($(this).val());
    renderedFoldersDataTableItems = foldersTable.rows({ search: 'applied' }).data().toArray();
    synchronizeFoldersDataTableAndList();
});

$("#folder-dynamic-table_length select").on("change", function () {
    $("#folder-thumbnail-view-search-length").val($(this).val());
});


//List View
var foldersWrapper = $("#folder-thumbnail-view .folders-content");

function synchronizeFoldersDataTableAndList() {
    let searchResultData = foldersTable.rows({ search: 'applied' }).data().toArray();
    let searchResultFolders = [];
    for (let data of searchResultData) {
        let resultFolder = foldersArray.find(function (element) {
            return element.id == data[1];
        });
        searchResultFolders.push(resultFolder);
    }
    renderFoldersList(searchResultFolders);
}

$("#folder-thumbnail-view-search-input").on("keyup", function () {
    $("#folder-dynamic-table_filter input").val($(this).val());
    foldersTable.search($(this).val());
    synchronizeFoldersDataTableAndList();
});

$("#folder-thumbnail-view-search-length").on("change", function () {
    renderFoldersList(renderedFoldersListItems);
    $("#folder-dynamic-table_length select").val($(this).val());
    $("#folder-dynamic-table_length select").trigger("change");
});


//View Switch
$("#folder-table-view-button, #folder-table-view-button-576").on("click", function () {
    $("#folder-thumbnail-view").addClass("hidden");
    $("#folder-table-view").removeClass("hidden");
    foldersTable.draw();
});

$("#folder-thumbnail-view-button, #folder-thumbnail-view-button-576").on("click", function () {
    $("#folder-table-view").addClass("hidden");
    $("#folder-thumbnail-view").removeClass("hidden");
});


//Render DataTable
function renderFoldersDataTable(folders) {
    foldersTable.clear();
    for (let folder of folders) {
        let tableRow = $(`
                <tr data-folder-id=${folderUtilities.getId(folder)}>
                    <td class="folder-table-checkbox center">
                        <label class="pos-rel">
                            <input type="checkbox" class="ace" />
                            <span class="lbl"></span>
                        </label>
                    </td>
                    <td class="folder-table-id hidden">${folderUtilities.getId(folder)}</td>
                    <td class="folder-table-first">${folderUtilities.getName(folder)}</td>
                    <td class="folder-table-actions">
                        <div class="hidden-sm hidden-xs action-buttons">
                            <a class="blue folder-details-button" data-folder-id="${folderUtilities.getId(folder)}" href="/Folders/${folderUtilities.getId(folder)}">
                                <i class="ace-icon fas fa-search-plus bigger-120"></i>
                            </a>

                            <a class="orange folder-edit-button" data-folder-id="${folderUtilities.getId(folder)}" href="#">
                                <i class="ace-icon fas fa-edit bigger-120"></i>
                            </a>

                            <a class="red folder-delete-button" data-folder-id="${folderUtilities.getId(folder)}" href="#">
                                <i class="ace-icon fas fa-trash-alt bigger-120"></i>
                            </a>
                        </div>

                        <div class="hidden-md hidden-lg">
                            <div class="inline pos-rel">
                                <button class="btn btn-minier btn-yellow dropdown-toggle" data-toggle="dropdown" data-position="auto">
                                    <i class="ace-icon fa fa-caret-down icon-only bigger-120"></i>
                                </button>

                                <ul class="dropdown-menu dropdown-only-icon dropdown-yellow dropdown-menu-right dropdown-caret dropdown-close">
                                    <li>
                                        <a href="/Folders/${folderUtilities.getId(folder)}" class="tooltip-info folder-details-button" data-folder-id="${folderUtilities.getId(folder)}" data-rel="tooltip" title="Voir Courriers">
                                            <span class="blue">
                                                <i class="ace-icon fas fa-search-plus bigger-120"></i>
                                            </span>
                                        </a>
                                    </li>

                                    <li>
                                        <a href="#" class="tooltip-warning folder-edit-button" data-folder-id="${folderUtilities.getId(folder)}" data-rel="tooltip" title="Modifier">
                                            <span class="red">
                                                <i class="ace-icon fas fa-edit bigger-120"></i>
                                            </span>
                                        </a>
                                    </li>

                                    <li>
                                        <a href="#" class="tooltip-error folder-delete-button" data-folder-id="${folderUtilities.getId(folder)}" data-rel="tooltip" title="Supprimer">
                                            <span class="red">
                                                <i class="ace-icon fas fa-trash-alt bigger-120"></i>
                                            </span>
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </td>
                </tr>
                `);
        foldersTable.rows.add($(tableRow));
    }
    foldersTable.draw();
    renderedFoldersDataTableItems = folders;
}

//Render Thumbnail View
function folderThumbnailItemTemplate(folders) {
    var item = "";
    if (folders.length == 0) {
        item = `<p>Aucun dossier n'est trouvé.</p>`;
    }
    else {
        for (let folder of folders) {
            item += `
                    <div class="col-xs-12 col-sm-4 col-md-3">
                        <div class="thumbnail search-thumbnail folder-entry">
                            <img class="media-object folder-entry-image" alt="Folder Image" src="/Assets/Images/placeholder/folderDefault.png" />
                            <div class="caption folder-caption">
                                <h2 class="folder-header">${folderUtilities.getName(folder)}</h2>
                                <div class="thumbnail-buttons">
                                    <a class="btn btn-white btn-primary btn-bold folder-details-button" data-folder-id="${folderUtilities.getId(folder)}" href="/Folders/${folderUtilities.getId(folder)}">
                                        <i class="ace-icon fas fa-search-plus"></i>
                                        Voir Courriers
                                    </a>
                                    <a class="btn btn-white btn-warning btn-bold folder-edit-button" data-folder-id="${folderUtilities.getId(folder)}" href="#">
                                        <i class="ace-icon fas fa-edit"></i>
                                        Modifier
                                    </a>
                                    <a class="btn btn-white btn-danger btn-bold folder-delete-button" data-folder-id="${folderUtilities.getId(folder)}" href="#">
                                        <i class="ace-icon fas fa-trash-alt"></i>
                                        Supprmier
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                    `;
        }
    }
    return item;
}
function renderFoldersList(folders, pageSize = null) {
    foldersWrapper.empty();
    var paginationOptions = {
        dataSource: folders,
        ulClassName: "pagination",
        prevText: "Précédent",
        nextText: "Suivant",
        pageSize: Number($("#folder-thumbnail-view-search-length").val()),
        callback: function (data, pagination) {
            var item = folderThumbnailItemTemplate(data);
            foldersWrapper.html(item);
        }
    }
    $("#folders-thumbnail-pagination").pagination(paginationOptions);
    renderedFoldersListItems = folders;
}

//Render Folders
function renderFolders(folders) {
    renderFoldersDataTable(folders);
    renderFoldersList(folders);
}
renderFolders(foldersArray);


//Get Folders With AJAX Ander Render Them
function getAndRenderFolders() {
    $.ajax({
        url: `/API/Folders/All`,
        method: "GET",
        dataType: "json",
    }).then(function (response) {
        foldersArray = response;
        renderFolders(foldersArray);
        return response;
    }).fail(function (response) {
        errorNotification('Erreur', "Erreur lord du chargement des dossiers.", 3000);
        console.log(response);
    });
}