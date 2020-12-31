var renderedGroupsListItems;
var renderedGroupsDataTableItems;


//DataTables
$.fn.dataTable.moment('DD/MM/YYYY');

var groupsTable =
    $('#group-dynamic-table')
        .DataTable({
            "dom": "<'row'<'col-xs-6 col-sm-4'l><'hidden-xs col-sm-4 filer_options'><'col-xs-6 col-sm-4'f>>" +
                "tr" +
                "<'row'<'col-12'p>>",
            language: {
                processing: "جاري المعالجة...",
                search: "بحث:",
                lengthMenu: "العدد المعروض: _MENU_",
                info: "",
                infoEmpty: "",
                infoFiltered: "",
                infoPostFix: "",
                loadingRecords: "جاري التحميل...",
                zeroRecords: "لا توجد مجموعات للعرض.",
                emptyTable: "لا توجد أي مجموعات.",
                paginate: {
                    first: "الأول",
                    previous: "السابق",
                    next: "الموالي",
                    last: "الأخير"
                },
                aria: {
                    sortAscending: "إضغط لترتيب الخانة تصاعديا:",
                    sortDescending: "إضغط لترتيب الخانة تنازليا:"
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

groupsTable.on('select', function (e, dt, type, index) {
    if (type === 'row') {
        $(groupsTable.row(index).node()).find('input:checkbox').prop('checked', true);
    }
});
groupsTable.on('deselect', function (e, dt, type, index) {
    if (type === 'row') {
        $(groupsTable.row(index).node()).find('input:checkbox').prop('checked', false);
    }
});

//Select/deselect all rows according to table header checkbox
$('#group-dynamic-table > thead > tr > th input[type=checkbox], #group-dynamic-table_wrapper input[type=checkbox]').eq(0).on('click', function () {
    var th_checked = this.checked;//checkbox inside "TH" table header

    $('#group-dynamic-table').find('tbody > tr').each(function () {
        var row = this;
        if (th_checked) groupsTable.row(row).select();
        else groupsTable.row(row).deselect();
    });
});

//Select/deselect a row when the checkbox is checked/unchecked
$('#group-dynamic-table').on('click', 'td input[type=checkbox]', function () {
    var row = $(this).closest('tr').get(0);
    if (this.checked) groupsTable.row(row).deselect();
    else groupsTable.row(row).select();
});

$(document).on('click', '#group-dynamic-table .dropdown-toggle', function (e) {
    e.stopImmediatePropagation();
    e.stopPropagation();
    e.preventDefault();
});

$("#group-dynamic-table_filter input").on("keyup", function () {
    $("#group-thumbnail-view-search-input").val($(this).val());
    renderedGroupsDataTableItems = groupsTable.rows({ search: 'applied' }).data().toArray();
    synchronizeGroupsDataTableAndList();
});

$("#group-dynamic-table_length select").on("change", function () {
    $("#group-thumbnail-view-search-length").val($(this).val());
});


//List View
var groupsWrapper = $("#group-thumbnail-view .groups-content");

function synchronizeGroupsDataTableAndList() {
    let searchResultData = groupsTable.rows({ search: 'applied' }).data().toArray();
    let searchResultGroups = [];
    for (let data of searchResultData) {
        let resultGroup = groupsArray.find(function (element) {
            return element.id == data[1];
        });
        searchResultGroups.push(resultGroup);
    }
    renderGroupsList(searchResultGroups);
}

$("#group-thumbnail-view-search-input").on("keyup", function () {
    $("#group-dynamic-table_filter input").val($(this).val());
    groupsTable.search($(this).val());
    synchronizeGroupsDataTableAndList();
});

$("#group-thumbnail-view-search-length").on("change", function () {
    renderGroupsList(renderedGroupsListItems);
    $("#group-dynamic-table_length select").val($(this).val());
    $("#group-dynamic-table_length select").trigger("change");
});


//View Switch
$("#group-table-view-button, #group-table-view-button-576").on("click", function () {
    $("#group-thumbnail-view").addClass("hidden");
    $("#group-table-view").removeClass("hidden");
    groupsTable.draw();
});

$("#group-thumbnail-view-button, #group-thumbnail-view-button-576").on("click", function () {
    $("#group-table-view").addClass("hidden");
    $("#group-thumbnail-view").removeClass("hidden");
});


//Render DataTable
function renderGroupsDataTable(groups) {
    groupsTable.clear();
    for (let group of groups) {
        let tableRow = $(`
                <tr data-group-id=${groupUtilities.getId(group)}>
                    <td class="group-table-checkbox center">
                        <label class="pos-rel">
                            <input type="checkbox" class="ace" />
                            <span class="lbl"></span>
                        </label>
                    </td>
                    <td class="group-table-id hidden">${groupUtilities.getId(group)}</td>
                    <td class="group-table-first">${groupUtilities.getName(group)}</td>
                    <td class="group-table-actions">
                        <div class="hidden-sm hidden-xs action-buttons">
                            <a class="blue group-details-button" data-group-id="${groupUtilities.getId(group)}" href="/Ar/Groups/${groupUtilities.getId(group)}">
                                <i class="ace-icon fas fa-search-plus bigger-120"></i>
                            </a>

                            <a class="blue manage-group-members-button" data-group-id="${groupUtilities.getId(group)}" href="#">
                                <i class="ace-icon fas fa-users-cog bigger-120"></i>
                            </a>

                            <a class="orange group-edit-button" data-group-id="${groupUtilities.getId(group)}" href="#">
                                <i class="ace-icon fas fa-edit bigger-120"></i>
                            </a>

                            <a class="red group-delete-button" data-group-id="${groupUtilities.getId(group)}" href="#">
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
                                        <a href="/Ar/Groups/${groupUtilities.getId(group)}" class="tooltip-info group-details-button" data-group-id="${groupUtilities.getId(group)}" data-rel="tooltip" title="الأعضاء">
                                            <span class="blue">
                                                <i class="ace-icon fas fa-search-plus bigger-120"></i>
                                            </span>
                                        </a>
                                    </li>    

                                    <li>
                                        <a href="#" class="tooltip-info manage-group-members-button" data-group-id="${groupUtilities.getId(group)}" data-rel="tooltip" title="تنظيم الأعضاء">
                                            <span class="blue">
                                                <i class="ace-icon fas fa-users-cog bigger-120"></i>
                                            </span>
                                        </a>
                                    </li>

                                    <li>
                                        <a href="#" class="tooltip-warning group-edit-button" data-group-id="${groupUtilities.getId(group)}" data-rel="tooltip" title="تعديل">
                                            <span class="red">
                                                <i class="ace-icon fas fa-edit bigger-120"></i>
                                            </span>
                                        </a>
                                    </li>

                                    <li>
                                        <a href="#" class="tooltip-error group-delete-button" data-group-id="${groupUtilities.getId(group)}" data-rel="tooltip" title="محو">
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
        groupsTable.rows.add($(tableRow));
    }
    groupsTable.draw();
    renderedGroupsDataTableItems = groups;
}

//Render Thumbnail View
function groupThumbnailItemTemplate(groups) {
    var item = "";
    if (groups.length == 0) {
        item = `<p>Aucun groupe n'est trouvé.</p>`;
    }
    else {
        for (let group of groups) {
            item += `
                    <div class="col-xs-12 col-sm-4 col-md-3">
                        <div class="thumbnail search-thumbnail group-entry">
                            <img class="media-object group-entry-image" alt="Group Image" src="/Assets/Images/placeholder/groupDefault.png" />
                            <div class="caption group-caption">
                                <h2 class="group-header">${groupUtilities.getName(group)}</h2>
                                <div class="thumbnail-buttons">
                                    <a class="btn btn-white btn-primary btn-bold manage-group-members-button" data-group-id="${groupUtilities.getId(group)}" href="/Ar/Groups/${groupUtilities.getId(group)}">
                                        <i class="ace-icon fas fa-search-plus"></i>
                                        الأعضاء
                                    </a>
                                    <a class="btn btn-white btn-primary btn-bold manage-group-members-button" data-group-id="${groupUtilities.getId(group)}" href="#">
                                        <i class="ace-icon fas fa-users-cog"></i>
                                        تنظيم الأعضاء
                                    </a>
                                    <a class="btn btn-white btn-warning btn-bold group-edit-button" data-group-id="${groupUtilities.getId(group)}" href="#">
                                        <i class="ace-icon fas fa-edit"></i>
                                        تعديل
                                    </a>
                                    <a class="btn btn-white btn-danger btn-bold group-delete-button" data-group-id="${groupUtilities.getId(group)}" href="#">
                                        <i class="ace-icon fas fa-trash-alt"></i>
                                        محو
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
function renderGroupsList(groups, pageSize = null) {
    groupsWrapper.empty();
    var paginationOptions = {
        dataSource: groups,
        ulClassName: "pagination",
        prevText: "السابق",
        nextText: "الموالي",
        pageSize: Number($("#group-thumbnail-view-search-length").val()),
        callback: function (data, pagination) {
            var item = groupThumbnailItemTemplate(data);
            groupsWrapper.html(item);
        }
    }
    $("#groups-thumbnail-pagination").pagination(paginationOptions);
    renderedGroupsListItems = groups;
}

//Render Groups
function renderGroups(groups) {
    renderGroupsDataTable(groups);
    renderGroupsList(groups);
}
renderGroups(groupsArray);


//Get Mails With AJAX Ander Render Them
function getAndRenderGroups() {
    $.ajax({
        url: `/API/Groups/All`,
        method: "GET",
        dataType: "json",
    }).then(function (response) {
        groupsArray = response;
        renderGroups(groupsArray);
        return response;
    }).fail(function (response) {
        errorNotification('خطأ', "وقع خطأ أثناء تحميل المجموعات.", 3000);
        console.log(response);
    });
}