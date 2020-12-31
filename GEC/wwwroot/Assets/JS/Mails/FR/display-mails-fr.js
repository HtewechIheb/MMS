var renderedMailsListItems;
var renderedMailsDataTableItems;


//DataTables
$.fn.dataTable.moment('DD/MM/YYYY');

var mailsTable =
    $('#mail-dynamic-table')
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
                emptyTable: "Aucun mail n'est trouvé.",
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
                null, null, null,
                { "bSortable": false }
            ],
            "aaSorting": [],
            select: {
                style: 'multi',
                selector: 'td:not(:last-child)'
            }
        });

$.fn.dataTable.Buttons.defaults.dom.container.className = 'dt-buttons btn-overlap btn-group btn-overlap';

mailsTable.on('select', function (e, dt, type, index) {
    if (type === 'row') {
        $(mailsTable.row(index).node()).find('input:checkbox').prop('checked', true);
    }
});
mailsTable.on('deselect', function (e, dt, type, index) {
    if (type === 'row') {
        $(mailsTable.row(index).node()).find('input:checkbox').prop('checked', false);
    }
});

//Table checkboxes
$('th input[type=checkbox], td input[type=checkbox]').prop('checked', false);

//Select/deselect all rows according to table header checkbox
$('#mail-dynamic-table > thead > tr > th input[type=checkbox], #mail-dynamic-table_wrapper input[type=checkbox]').eq(0).on('click', function () {
    var th_checked = this.checked;//checkbox inside "TH" table header

    $('#mail-dynamic-table').find('tbody > tr').each(function () {
        var row = this;
        if (th_checked) mailsTable.row(row).select();
        else mailsTable.row(row).deselect();
    });
});

//Select/deselect a row when the checkbox is checked/unchecked
$('#mail-dynamic-table').on('click', 'td input[type=checkbox]', function () {
    var row = $(this).closest('tr').get(0);
    if (this.checked) mailsTable.row(row).deselect();
    else mailsTable.row(row).select();
});

//Add tooltip for small view action buttons in dropdown menu
$('[data-rel="tooltip"]').tooltip({ placement: tooltip_placement });

//Tooltip placement on right or left
function tooltip_placement(context, source) {
    var $source = $(source);
    var $parent = $source.closest('table')
    var off1 = $parent.offset();
    var w1 = $parent.width();

    var off2 = $source.offset();
    //var w2 = $source.width();

    if (parseInt(off2.left) < parseInt(off1.left) + parseInt(w1 / 2)) return 'right';
    return 'left';
}

$(document).on('click', '#mail-dynamic-table .dropdown-toggle', function (e) {
    e.stopImmediatePropagation();
    e.stopPropagation();
    e.preventDefault();
});

$("#mail-dynamic-table_filter input").on("keyup", function () {
    $("#mail-list-view-search-input").val($(this).val());
    renderedMailsDataTableItems = mailsTable.rows({ search: 'applied' }).data().toArray();
    synchronizeMailsDataTableAndList();
});

$("#mail-dynamic-table_length select").on("change", function () {
    $("#mail-list-view-search-length").val($(this).val());
});


//List View
var mailsWrapper = $("#mail-list-view .mails-content");

function synchronizeMailsDataTableAndList() {
    let searchResultData = mailsTable.rows({ search: 'applied' }).data().toArray();
    let searchResultMails = [];
    console.log(searchResultData);
    for (let data of searchResultData) {
        let resultMail = mailsArray.find(function (element) {
            return element.id == data[1];
        });
        searchResultMails.push(resultMail);
    }
    renderMailsList(searchResultMails);
}

$("#mail-list-view-search-input").on("keyup", function () {
    $("#mail-dynamic-table_filter input").val($(this).val());
    mailsTable.search($(this).val());
    synchronizeMailsDataTableAndList();
});

$("#mail-list-view-search-length").on("change", function () {
    renderMailsList(renderedMailsListItems);
    $("#mail-dynamic-table_length select").val($(this).val());
    $("#mail-dynamic-table_length select").trigger("change");
});


//View Switch
$("#mail-table-view-button, #mail-table-view-button-576").on("click", function () {
    $("#mail-list-view").addClass("hidden");
    $("#mail-table-view").removeClass("hidden");
    mailsTable.draw();
});
$("#mail-list-view-button, #mail-list-view-button-576").on("click", function () {
    $("#mail-table-view").addClass("hidden");
    $("#mail-list-view").removeClass("hidden");
});


//Render DataTable
function renderMailsDataTable(mails) {
    mailsTable.clear();
    for (let mail of mails) {
        let tableRow = $(`
                <tr data-mail-id=${mailUtilities.getId(mail)}>
                    <td class="mail-table-checkbox center">
                        <label class="pos-rel">
                            <input type="checkbox" class="ace" />
                            <span class="lbl"></span>
                        </label>
                    </td>
                    <td class="mail-table-id hidden">${mailUtilities.getId(mail)}</td>
                    <td class="mail-table-third hidden-768">${mailUtilities.getDate(mail.registrationDate)}</td>
                    <td class="mail-table-second hidden-480">${mailUtilities.getSender(mail)}</td>
                    <td class="mail-table-first">${mailUtilities.getObject(mail)}</td>
                    <td class="mail-table-actions">
                        <div class="hidden-sm hidden-xs action-buttons">
                            <a class="blue mail-details-button" data-mail-id="${mailUtilities.getId(mail)}" href="/Fr/Mails/${mailUtilities.getId(mail)}">
                                <i class="ace-icon fas fa-search-plus bigger-120"></i>
                            </a>

                            <a class="orange mail-edit-button" data-mail-id="${mailUtilities.getId(mail)}" href="#">
                                <i class="ace-icon fas fa-edit bigger-120"></i>
                            </a>

                            <a class="red mail-delete-button" data-mail-id="${mailUtilities.getId(mail)}" href="#">
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
                                        <a href="/Fr/Mails/${mailUtilities.getId(mail)}" class="tooltip-info mail-details-button" data-mail-id="${mailUtilities.getId(mail)}" data-rel="tooltip" title="Détails">
                                            <span class="blue">
                                                <i class="ace-icon fas fa-search-plus bigger-120"></i>
                                            </span>
                                        </a>
                                    </li>

                                    <li>
                                        <a href="#" class="tooltip-warning mail-edit-button" data-mail-id="${mailUtilities.getId(mail)}" data-rel="tooltip" title="Modifier">
                                            <span class="orange">
                                                <i class="ace-icon fas fa-edit bigger-120"></i>
                                            </span>
                                        </a>
                                    </li>

                                    <li>
                                        <a href="#" class="tooltip-error mail-delete-button" data-mail-id="${mailUtilities.getId(mail)}" data-rel="tooltip" title="Supprimer">
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
        mailsTable.rows.add($(tableRow));
    }
    mailsTable.draw();
    renderedMailsDataTableItems = mails;
}

//Render List View
function mailsListItemTemplate(mails) {
    var item = "";
    if (mails.length == 0) {
        item = `<p>Aucun mail n'est trouvé.</p>`;
    }
    else {
        for (let mail of mails) {
            item += `
                    <div class="row mail-entry">
                        <div class="col-xs-12 col-sm-3 text-center">
                            <img src="/Assets/Images/placeholder/mailDefault.png" alt="Mail Image" class="mail-entry-image" />
                        </div>

                        <div class="col-xs-12 col-sm-9">
                            <div class="mail-content mail-content-hidden-576">
                            <h2 class="mail-header">${mailUtilities.getObject(mail)}</h2>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <p><strong>Expéditeur:&nbsp;</strong>${mailUtilities.getSender(mail)}</p>
                                    </div>
                                    <div class="col-xs-6">
                                        <p><strong>Type:&nbsp;</strong>${mailUtilities.getMailType(mail)}</p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <p><strong>Destinataire:&nbsp;</strong>${mailUtilities.getRecipient(mail)}</p>
                                    </div>
                                    <div class="col-xs-6">
                                        <p><strong>Canal:&nbsp;</strong>${mailUtilities.getChannel(mail)}</p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <p><strong>Copie Physique:&nbsp;</strong>${mailUtilities.getBoolean(mail.hasHardCopy)}</p>
                                    </div>
                                    <div class="col-xs-6">
                                        <p><strong>Date d'enregistrement:&nbsp;</strong>${mailUtilities.getDate(mail.registrationDate)}</p>
                                    </div>
                                </div>
                            </div>
                            <div class="mail-action-buttons-hidden-576 center-buttons">
                                <a class="btn btn-white btn-primary btn-bold mail-details-button" data-mail-id="${mailUtilities.getId(mail)}" href="/Fr/Mails/${mailUtilities.getId(mail)}">
                                    <i class="ace-icon fas fa-search-plus"></i>
                                    Détails
                                </a>
                                <a class="btn btn-white btn-success btn-bold mail-edit-button" data-mail-id="${mailUtilities.getId(mail)}" href="#">
                                    <i class="ace-icon fas fa-edit"></i>
                                    Modifier
                                </a>
                                <a class="btn btn-white btn-danger btn-bold mail-delete-button" data-mail-id="${mailUtilities.getId(mail)}" href="#">
                                    <i class="ace-icon fas fa-trash-alt"></i>
                                    Supprimer
                                </a>
                            </div>

                            <div class="search-content mail-content-shown-576">
                                <h2 class="mail-header">${mailUtilities.getObject(mail)}</h2>
                                <p><strong>Expéditeur:&nbsp;</strong>${mailUtilities.getSender(mail)}</p>
                                <p><strong>Destinataire:&nbsp;</strong>${mailUtilities.getRecipient(mail)}</p>
                                <p><strong>Copie Physique:&nbsp;</strong>${mailUtilities.getBoolean(mail.hasHardCopy)}</p>
                                <p><strong>Type:&nbsp;</strong>${mailUtilities.getMailType(mail)}</p>
                                <p><strong>Canal:&nbsp;</strong>${mailUtilities.getChannel(mail)}</p>
                                <p><strong>Date de réception:&nbsp;</strong>${mailUtilities.getDate(mail.registrationDate)}</p>
                            </div>
                            <div class="mail-action-buttons-shown-576 center-buttons">
                                <a class="btn btn-white btn-primary btn-bold mail-details-button" data-mail-id="${mailUtilities.getId(mail)}" href="/Fr/Mails/${mailUtilities.getId(mail)}">
                                    <i class="ace-icon fas fa-search-plus"></i>
                                    Détails
                                </a>
                                <a class="btn btn-white btn-success btn-bold mail-edit-button" data-mail-id="${mailUtilities.getId(mail)}" href="#"> 
                                    <i class="ace-icon fas fa-edit"></i>
                                    Modifier
                                </a>
                                <a class="btn btn-white btn-danger btn-bold mail-delete-button" data-mail-id="${mailUtilities.getId(mail)}" href="#">
                                    <i class="ace-icon fas fa-trash-alt"></i>
                                    Supprimer
                                </a>
                            </div>
                        </div>
                    </div>
                    `;
        }
    }
    return item;
}
function renderMailsList(mails, pageSize = null) {
    mailsWrapper.empty();
    var paginationOptions = {
        dataSource: mails,
        ulClassName: "pagination",
        prevText: "Précédent",
        nextText: "Suivant",
        pageSize: Number($("#mail-list-view-search-length").val()),
        callback: function (data, pagination) {
            var item = mailsListItemTemplate(data);
            mailsWrapper.html(item);
        }
    }
    $("#mails-list-pagination").pagination(paginationOptions);
    renderedMailsListItems = mails;
}

//Render Mails
function renderMails(mails) {
    renderMailsDataTable(mails);
    renderMailsList(mails);
}
renderMails(mailsArray);


//Get Mails With AJAX And Render Them
function getAndRenderMails() {
    let URL;
    if (currentPage === pages.mails) {
        URL = `/API/Mails/All`;
    }
    else if (currentPage === pages.folderShow) {
        URL = `/API/Folders/${folderUtilities.getId(shownFolder)}/Mails`;
    }
    else if (currentPage === pages.mailsIngoing) {
        URL = `/API/Mails/Ingoing`;
    }
    else if (currentPage === pages.mailsOutgoing) {
        URL = `/API/Mails/Outgoing`;
    }
    else if (currentPage === pages.mailsInternal) {
        URL = `/API/Mails/Internal`;
    }

    $.ajax({
        url: URL,
        method: "GET",
        dataType: "json",
    }).then(function (response) {
        mailsArray = response;
        renderMails(mailsArray);
        return response;
    }).fail(function (response) {
        errorNotification('Erreur', "Erreur lord du chargement des courriers.", 3000);
        console.log(response);
    });
}


//Filtering
var currentFiteringOptions = {
    type: null,
    channel: null,
    confidentiality: null,
};
filterMails(currentFiteringOptions);

function filterMails(filteringOptions) {
    let filteredMails = [];
    for (let mail of mailsArray) {
        let matchingFilter = true;
        if ((filteringOptions.type != null && mail.mailType != filteringOptions.type) ||
            (filteringOptions.channel != null && mail.channel != filteringOptions.channel) ||
            (filteringOptions.confidentiality != null && mail.confidentiality != filteringOptions.confidentiality)) {
            matchingFilter = false;
        }
        if (matchingFilter) {
            filteredMails.push(mail);
        }
    }
    renderMails(filteredMails);
}

$("#filtering-type").on("change", function () {
    if ($(this).val() == "all") {
        currentFiteringOptions.type = null;
        filterMails(currentFiteringOptions);
    }
    else {
        currentFiteringOptions.type = $(this).val();
        filterMails(currentFiteringOptions);
    }
});
$("#filtering-channel").on("change", function () {
    if ($(this).val() == "all") {
        currentFiteringOptions.channel = null;
        filterMails(currentFiteringOptions);
    }
    else {
        currentFiteringOptions.channel = $(this).val();
        filterMails(currentFiteringOptions);
    }
});
$("#filtering-confidentiality").on("change", function () {
    if ($(this).val() == "all") {
        currentFiteringOptions.confidentiality = null;
        filterMails(currentFiteringOptions);
    }
    else {
        currentFiteringOptions.confidentiality = $(this).val();
        filterMails(currentFiteringOptions);
    }
});