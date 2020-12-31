var renderedContactsListItems;
var renderedContactsDataTableItems;


//DataTables
$.fn.dataTable.moment('DD/MM/YYYY');

var contactsTable =
    $('#contact-dynamic-table')
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
                emptyTable: "Aucun contact n'est trouvé.",
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

contactsTable.on('select', function (e, dt, type, index) {
    if (type === 'row') {
        $(contactsTable.row(index).node()).find('input:checkbox').prop('checked', true);
    }
});
contactsTable.on('deselect', function (e, dt, type, index) {
    if (type === 'row') {
        $(contactsTable.row(index).node()).find('input:checkbox').prop('checked', false);
    }
});

//Table checkboxes
$('th input[type=checkbox], td input[type=checkbox]').prop('checked', false);

//Select/deselect all rows according to table header checkbox
$('#contact-dynamic-table > thead > tr > th input[type=checkbox], #contact-dynamic-table_wrapper input[type=checkbox]').eq(0).on('click', function () {
    var th_checked = this.checked;//checkbox inside "TH" table header

    $('#contact-dynamic-table').find('tbody > tr').each(function () {
        var row = this;
        if (th_checked) contactsTable.row(row).select();
        else contactsTable.row(row).deselect();
    });
});

//Select/deselect a row when the checkbox is checked/unchecked
$('#contact-dynamic-table').on('click', 'td input[type=checkbox]', function () {
    var row = $(this).closest('tr').get(0);
    if (this.checked) contactsTable.row(row).deselect();
    else contactsTable.row(row).select();
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

$(document).on('click', '#contact-dynamic-table .dropdown-toggle', function (e) {
    e.stopImmediatePropagation();
    e.stopPropagation();
    e.preventDefault();
});

$("#contact-dynamic-table_filter input").on("keyup", function () {
    $("#contact-list-view-search-input").val($(this).val());
    renderedContactsDataTableItems = contactsTable.rows({ search: 'applied' }).data().toArray();
    synchronizeContactsDataTableAndList();
});

$("#contact-dynamic-table_length select").on("change", function () {
    $("#contact-list-view-search-length").val($(this).val());
});


//List View
var contactsWrapper = $("#contact-list-view .contacts-content");

function synchronizeContactsDataTableAndList() {
    let searchResultData = contactsTable.rows({ search: 'applied' }).data().toArray();
    let searchResultContacts = [];
    for (let data of searchResultData) {
        let resultContact = contactsArray.find(function (element) {
            return element.id == data[1];
        });
        searchResultContacts.push(resultContact);
    }
    renderContactsList(searchResultContacts);
}

$("#contact-list-view-search-input").on("keyup", function () {
    $("#contact-dynamic-table_filter input").val($(this).val());
    contactsTable.search($(this).val());
    synchronizeContactsDataTableAndList();
});

$("#contact-list-view-search-length").on("change", function () {
    renderContactsList(renderedContactsListItems);
    $("#contact-dynamic-table_length select").val($(this).val());
    $("#contact-dynamic-table_length select").trigger("change");
});


//View Switch
$("#contact-table-view-button, #contact-table-view-button-576").on("click", function () {
    $("#contact-list-view").addClass("hidden");
    $("#contact-table-view").removeClass("hidden");
    contactsTable.draw();
});

$("#contact-list-view-button, #contact-list-view-button-576").on("click", function () {
    $("#contact-table-view").addClass("hidden");
    $("#contact-list-view").removeClass("hidden");
});


//Render DataTable
function renderContactsDataTable(contacts) {
    contactsTable.clear();
    for (let contact of contacts) {
        let tableRow = $(`
                <tr data-contact-id=${contactUtilities.getId(contact)}>
                    <td class="contact-table-checkbox center">
                        <label class="pos-rel">
                            <input type="checkbox" class="ace" />
                            <span class="lbl"></span>
                        </label>
                    </td>
                    <td class="mail-table-id hidden">${contactUtilities.getId(contact)}</td>
                    <td class="contact-table-first">${contactUtilities.getName(contact)}</td>
                    <td class="contact-table-second hidden-480">${contactUtilities.getNature(contact)}</td>
                    <td class="contact-table-third hidden-768">${contactUtilities.getContactType(contact)}</td>
                    <td class="contact-table-actions">
                        <div class="hidden-sm hidden-xs action-buttons">
                            <a class="blue contact-details-button" data-contact-id="${contactUtilities.getId(contact)}" href="/Contacts/${contactUtilities.getId(contact)}">
                                <i class="ace-icon fas fa-search-plus bigger-120"></i>
                            </a>

                            <a class="orange contact-edit-button" data-contact-id="${contactUtilities.getId(contact)}" href="#">
                                <i class="ace-icon fas fa-edit bigger-120"></i>
                            </a>

                            <a class="red contact-delete-button" data-contact-id="${contactUtilities.getId(contact)}" href="#">
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
                                        <a href="/Contacts/${contactUtilities.getId(contact)}" class="tooltip-info contact-details-button" data-contact-id="${contactUtilities.getId(contact)}" data-rel="tooltip" title="Détails">
                                            <span class="blue">
                                                <i class="ace-icon fas fa-search-plus bigger-120"></i>
                                            </span>
                                        </a>
                                    </li>

                                    <li>
                                        <a href="#" class="tooltip-warning contact-edit-button" data-contact-id="${contactUtilities.getId(contact)}" data-rel="tooltip" title="Modifier">
                                            <span class="orange">
                                                <i class="ace-icon fas fa-edit bigger-120"></i>
                                            </span>
                                        </a>
                                    </li>

                                    <li>
                                        <a href="#" class="tooltip-error contact-delete-button" data-contact-id="${contactUtilities.getId(contact)}" data-rel="tooltip" title="Supprimer">
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
        contactsTable.rows.add($(tableRow));
    }
    contactsTable.draw();
    renderedContactsDataTableItems = contacts;
}

//Render List View
function contactsListItemTemplate(contacts) {
    var item = "";
    if (contacts.length == 0) {
        item = `<p>Aucun contact n'est trouvé.</p>`;
    }
    else {
        for (let contact of contacts) {
            item += `
                    <div class="row contact-entry">
                        <div class="col-xs-12 col-sm-3 text-center">
                            <img src="/Assets/Images/placeholder/contactDefault.png" alt="Contact Image" class="contact-entry-image" />
                        </div>

                        <div class="col-xs-12 col-sm-9">
                            <div class="contact-content contact-content-hidden-576">
                                <h2 class="contact-header">${contactUtilities.getName(contact)}</h2>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <p><strong>Nature:&nbsp;</strong>${contactUtilities.getNature(contact)}</p>
                                    </div>
                                    <div class="col-xs-6">
                                        <p><strong>Type:&nbsp;</strong>${contactUtilities.getContactType(contact)}</p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <p><strong>Email:&nbsp;</strong>${contactUtilities.getEmail(contact)}</p>
                                    </div>
                                    <div class="col-xs-6">
                                        <p><strong>Téléphone:&nbsp;</strong>${contactUtilities.getTelephone(contact)}</p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6">
                                        <p><strong>Fax:&nbsp;</strong>${contactUtilities.getFax(contact)}</p>
                                    </div>
                                    <div class="col-xs-6">
                                        <p><strong>Adresse Physique:&nbsp;</strong>${contactUtilities.getAddress(contact)}</p>
                                    </div>
                                </div>
                            </div>
                            <div class="contact-action-buttons-hidden-576 text-center">
                                <a class="btn btn-white btn-primary btn-bold contact-details-button" data-contact-id="${contactUtilities.getId(contact)}" href="/Contacts/${contactUtilities.getId(contact)}">
                                    <i class="ace-icon fas fa-search-plus"></i>
                                    Détails
                                </a>
                                <a class="btn btn-white btn-warning btn-bold contact-edit-button" data-contact-id="${contactUtilities.getId(contact)}" href="#">
                                    <i class="ace-icon fas fa-edit"></i>
                                    Modifier
                                </a>
                                <a class="btn btn-white btn-danger btn-bold contact-delete-button" data-contact-id="${contactUtilities.getId(contact)}" href="#">
                                    <i class="ace-icon fas fa-trash-alt"></i>
                                    Supprimer
                                </a>
                            </div>


                            <div class="search-content contact-content-shown-576">
                                <h2 class="contact-header">${contactUtilities.getName(contact)}</h2>
                                <p><strong>Nature:&nbsp;</strong>${contactUtilities.getNature(contact)}</p>
                                <p><strong>Type:&nbsp;</strong>${contactUtilities.getContactType(contact)}</p>
                                <p><strong>Email:&nbsp;</strong>${contactUtilities.getEmail(contact)}</p>
                                <p><strong>Téléphone:&nbsp;</strong>${contactUtilities.getTelephone(contact)}</p>
                                <p><strong>Fax:&nbsp;</strong>${contactUtilities.getFax(contact)}</p>
                                <p><strong>Adresse Physique:&nbsp;</strong>${contactUtilities.getAddress(contact)}</p>
                            </div>
                            <div class="contact-action-buttons-shown-576 text-center">
                                <a class="btn btn-white btn-primary btn-bold contact-details-button" data-contact-id="${contactUtilities.getId(contact)}" href="/Contacts/${contactUtilities.getId(contact)}">
                                    <i class="ace-icon fas fa-search-plus"></i>
                                    Détails
                                </a>
                                <a class="btn btn-white btn-warning btn-bold contact-edit-button" data-contact-id="${contactUtilities.getId(contact)}" href="#">
                                    <i class="ace-icon fas fa-edit"></i>
                                    Modifier
                                </a>
                                <a class="btn btn-white btn-danger btn-bold contact-delete-button" data-contact-id="${contactUtilities.getId(contact)}" href="#">
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
function renderContactsList(contacts, pageSize = null) {
    contactsWrapper.empty();
    var paginationOptions = {
        dataSource: contacts,
        ulClassName: "pagination",
        prevText: "Précédent",
        nextText: "Suivant",
        pageSize: Number($("#contact-list-view-search-length").val()),
        callback: function (data, pagination) {
            var item = contactsListItemTemplate(data);
            contactsWrapper.html(item);
        }
    }
    $("#contacts-list-pagination").pagination(paginationOptions);
    renderedContactsListItems = contacts;
}

//Render Contacts
function renderContacts(contacts) {
    renderContactsDataTable(contacts);
    renderContactsList(contacts);
}
renderContacts(contactsArray);


//Get Mails With AJAX Ander Render Them
function getAndRenderContacts() {
    let URL;

    if (currentPage === pages.contacts) {
        URL = `/API/Contacts/All`;
    }
    else if (currentPage === pages.groupShow) {
        URL = `/API/Groups/${groupUtilities.getId(group)}/Members`;
    }

    $.ajax({
        url: URL,
        method: "GET",
        dataType: "json",
    }).then(function (response) {
        contactsArray = response;
        renderContacts(contactsArray);
        return response;
    }).fail(function (response) {
        errorNotification('Erreur', "Erreur lord du chargement des contacts.", 3000);
        console.log(response);
    });
}


//Filtering
var currentFiteringOptions = {
    nature: null,
    type: null
};
filterContacts(currentFiteringOptions);

function filterContacts(filteringOptions) {
    let filteredContacts = [];
    for (let contact of contactsArray) {
        let matchingFilter = true;
        if ((filteringOptions.nature != null && contact.nature != filteringOptions.nature) ||
            (filteringOptions.type != null && contact.contactType != filteringOptions.type)) {
            matchingFilter = false;
        }
        if (matchingFilter) {
            filteredContacts.push(contact);
        }
    }
    renderContacts(filteredContacts);
}

$("#filtering-nature").on("change", function () {
    if ($(this).val() == "all") {
        currentFiteringOptions.nature = null;
        filterContacts(currentFiteringOptions);
    }
    else {
        currentFiteringOptions.nature = $(this).val();
        filterContacts(currentFiteringOptions);
    }
});
$("#filtering-type").on("change", function () {
    if ($(this).val() == "all") {
        currentFiteringOptions.type = null;
        filterContacts(currentFiteringOptions);
    }
    else {
        currentFiteringOptions.type = $(this).val();
        filterContacts(currentFiteringOptions);
    }
});