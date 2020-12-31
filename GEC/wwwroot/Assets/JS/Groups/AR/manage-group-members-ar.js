var contactsDualList = $('#ManageGroupMembers_IdContact').bootstrapDualListbox({
    moveOnSelect: false,
    moveSelectedLabel: 'إضافة المختارين',
    moveAllLabel: 'إضافة الكل',
    removeSelectedLabel: 'إلغاء المختارين',
    removeAllLabel: 'إلغاء الكل',
    selectedListLabel: '<strong>المراسلون داخل المجموعة</strong>',
    nonSelectedListLabel: '<strong>المراسلون خارج المجموعة</strong>',
    selectorMinimalHeight: 250,
    filterPlaceHolder: '',
    infoTextFiltered: '<span class="label label-purple label-lg">Filtered</span>', 
    infoText: false,
});
var contactsDualListContainer = contactsDualList.bootstrapDualListbox('getContainer');
contactsDualListContainer.find('.btn').addClass('btn-white btn-info btn-bold');
//contactsDualListContainer.find('.btn').attr('data-toggle') = "tooltip";
//contactsDualListContainer.find('.btn').attr('data-placement') = "bottom";

function resetContactsDualList() {
    $("#manage-group-members-form #ManageGroupMembers_IdContact").empty();
    contactsDualList.bootstrapDualListbox('refresh');
}

function getAndRenderContactsAndGroupMembers_ManageGroupMembers(groupId) {
    $("#manage-group-members-submit-button").attr("disabled", "disabled");
    $("#manage-group-members-reset-button").attr("disabled", "disabled");
    $(".manage-group-members-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".manage-group-members-popup .processing-icon-block-hidden-576").removeClass("hidden");

    var getGroupMembers_ManageGroupMembers = $.ajax({
        url: `/API/Groups/${groupId}/Members`,
        method: "GET",
        dataType: "json"
    }).then(function (response) {
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('خطأ', "وقع خطأ أثناء تحميل أعضاء المجموعة.", 3000);
        console.log(response);
    });

    var getContacts_ManageGroupMembers = $.ajax({
        url: `/API/Contacts/All`,
        method: "GET",
        dataType: "json",
    }).then(function (response) {
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('خطأ', "وقع خطأ أثناء تحميل المراسلين.", 3000);
        console.log(response);
    });

    $.when(getContacts_ManageGroupMembers, getGroupMembers_ManageGroupMembers).done(function (contacts, groupMembers) {
        $("#manage-group-members-form #ManageGroupMembers_IdContact").empty();
        for (let contact of contacts) {
            if (groupMembers.find(member => member.id === contact.id)) {
                $("#manage-group-members-form #ManageGroupMembers_IdContact").append(`<option value="${contactUtilities.getId(contact)}" selected>${contactUtilities.getName(contact)}</option >`);
            }
            else {
                $("#manage-group-members-form #ManageGroupMembers_IdContact").append(`<option value="${contactUtilities.getId(contact)}">${contactUtilities.getName(contact)}</option>`);
            }
        }
        contactsDualList.bootstrapDualListbox('refresh');

        $("#manage-group-members-submit-button").removeAttr("disabled");
        $("#manage-group-members-reset-button").removeAttr("disabled");
    }).fail(function () {
        currentPopups.pop();
        resetContactsDualList();
        $(".manage-group-members-popup-wrapper").css("display", "none");
    }).always(function () {
        $(".manage-group-members-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".manage-group-members-popup .processing-icon-block-hidden-576").addClass("hidden");
    });
}

$("#group-dynamic-table, #group-thumbnail-view").on("click", ".manage-group-members-button", function () {
    event.stopImmediatePropagation();
    event.preventDefault();
    currentPopups.push(popups.manageGroupMembers);
    $(".manage-group-members-popup-wrapper").css("display", "block");
    $("#manage-group-members-form #ManageGroupMembers_IdGroup").val($(this).attr("data-group-id"));
    var groupId = $("#manage-group-members-form #ManageGroupMembers_IdGroup").val();
    getAndRenderContactsAndGroupMembers_ManageGroupMembers(groupId);
    
});

$(".manage-group-members-popup .closeButton").on("click", function () {
    currentPopups.pop();
    resetContactsDualList();
    $(".manage-group-members-popup-wrapper").css("display", "none");
});

$("#manage-group-members-form").on("reset", function (event) {
    event.preventDefault();
    var groupId = $("#manage-group-members-form #ManageGroupMembers_IdGroup").val();
    getAndRenderContactsAndGroupMembers_ManageGroupMembers(groupId);
});

$("#manage-group-members-form").on("submit", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    if (!$("#manage-group-members-form").valid()) {
        return false;
    }
    $("#manage-group-members-submit-button").attr("disabled", "disabled");
    $("#manage-group-members-reset-button").attr("disabled", "disabled");
    $(".manage-group-members-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".manage-group-members-popup .processing-icon-inline-block-hidden-576").removeClass("hidden");
    var groupId = $("#manage-group-members-form #ManageGroupMembers_IdGroup").val();

    var formData = new FormData();
    if (![null, undefined].includes($("#manage-group-members-form #ManageGroupMembers_IdGroup").val())) {
        formData.append("IdGroup", $("#manage-group-members-form #ManageGroupMembers_IdGroup").val());
    }
    if (![null, undefined].includes($("#manage-group-members-form #ManageGroupMembers_IdContact").val())) {
        for (contact of $("#manage-group-members-form #ManageGroupMembers_IdContact").val()) {
            formData.append("IdContact", contact);
        }
    }

    var manageGroupMembers_ManageGroupMembers = $.ajax({
        url: `/API/Groups/${groupId}`,
        method: "POST",
        data: formData,
        processData: false,
        contentType: false,
    }).then(function (response) {
        currentPopups.pop();
        resetContactsDualList();
        $(".manage-group-members-popup-wrapper").css("display", "none");
        successNotification('نجاح', "تم تنظيم أعضاء المجموعة بنجاح.", 3000);
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('خطأ', "وقع خطأ أثناء تنظيم أعضاء المجموعة.", 3000);
        console.log(response);
    }).always(function () {
        $(".manage-group-members-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".manage-group-members-popup .processing-icon-inline-block-hidden-576").addClass("hidden");
        $("#manage-group-members-submit-button").removeAttr("disabled");
        $("#manage-group-members-reset-button").removeAttr("disabled");
    });
});