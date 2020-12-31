$("#new-contact-button, .new-contact-button").on("click", function () {
    currentPopups.push(popups.newContact);
    $(".new-contact-popup-wrapper").css("display", "block");
});

$(".new-contact-popup .closeButton").on("click", function () {
    currentPopups.pop();
    $("#new-contact-form")[0].reset();
    $(".new-contact-popup-wrapper").css("display", "none");
});

$("#new-contact-form").on("reset", function () {
    $("#new-contact-form .field-validation-error").empty();
});

$("#new-contact-form").on("submit", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    if (!$("#new-contact-form").valid()) {
        return false;
    }
    $("#new-contact-submit-button").attr("disabled", "disabled");
    $("#new-contact-reset-button").attr("disabled", "disabled");
    $("#new-contact-upload-button").attr("disabled", "disabled");
    $(".new-contact-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".new-contact-popup .processing-icon-inline-block-hidden-576").removeClass("hidden");

    var formData = new FormData();
    if (![null, undefined].includes($("#new-contact-form #NewContact_Nature").val())) {
        formData.append("Nature", $("#new-contact-form #NewContact_Nature").val());
    }
    if (![null, undefined].includes($("#new-contact-form #NewContact_ContactType").val())) {
        formData.append("ContactType", $("#new-contact-form #NewContact_ContactType").val());
    }
    if (![null, undefined].includes($("#new-contact-form #NewContact_Name").val())) {
        formData.append("Name", $("#new-contact-form #NewContact_Name").val());
    }
    if (![null, undefined].includes($("#new-contact-form #NewContact_Email1").val())) {
        formData.append("Email1", $("#new-contact-form #NewContact_Email1").val());
    }
    if (![null, undefined].includes($("#new-contact-form #NewContact_Email2").val())) {
        formData.append("Email2", $("#new-contact-form #NewContact_Email2").val());
    }
    if (![null, undefined].includes($("#new-contact-form #NewContact_Telephone1").val())) {
        formData.append("Telephone1", $("#new-contact-form #NewContact_Telephone1").val());
    }
    if (![null, undefined].includes($("#new-contact-form #NewContact_Telephone2").val())) {
        formData.append("Telephone2", $("#new-contact-form #NewContact_Telephone2").val());
    }
    if (![null, undefined].includes($("#new-contact-form #NewContact_Fax").val())) {
        formData.append("Fax", $("#new-contact-form #NewContact_Fax").val());
    }
    if (![null, undefined].includes($("#new-contact-form #NewContact_Address").val())) {
        formData.append("Address", $("#new-contact-form #NewContact_Address").val());
    }

    var createContact_NewContact = $.ajax({
        url: `/API/Contacts`,
        method: "POST",
        data: formData,
        processData: false,
        contentType: false,
    }).then(function (response) {
        if (currentPage === pages.contacts || currentPage === pages.groupShow) {
            getAndRenderContacts();
        }
        currentPopups.pop();
        $("#new-contact-form")[0].reset();
        $(".new-contact-popup-wrapper").css("display", "none");
        successNotification('نجاح', "تم إضافة المراسل بنجاح.", 3000);
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('خطأ', "وقع خطأ أثناء إضافة المراسل.", 3000);
        console.log(response);
    }).always(function () {
        $(".new-contact-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".new-contact-popup .processing-icon-inline-block-hidden-576").addClass("hidden");
        $("#new-contact-submit-button").removeAttr("disabled");
        $("#new-contact-reset-button").removeAttr("disabled");
        $("#new-contact-upload-button").removeAttr("disabled");
    });

    if (currentPopups.includes(popups.newMail)) {
        var getContacts_NewContact = createContact_NewContact.then(function (response) {
            return $.ajax({
                url: `/API/Contacts/All`,
                method: "GET",
                dataType: "json",
            }).then(function (response) {
                $("#new-mail-form #NewMail_IdSender").empty();
                $("#new-mail-form #NewMail_IdRecipient").empty();
                for (let contact of response) {
                    $("#new-mail-form #NewMail_IdSender").append(`<option value="${contactUtilities.getId(contact)}">${contactUtilities.getName(contact)}</option>`);
                    $("#new-mail-form #NewMail_IdRecipient").append(`<option value="${contactUtilities.getId(contact)}">${contactUtilities.getName(contact)}</option>`);
                }
                console.log(response);
                return response;
            }).fail(function (response) {
                errorNotification('خطأ', "وقع خطأ أثناء تحميل المراسلين .", 3000);
                console.log(response);
            });
        });
    }
    else if (currentPopups.includes(popups.editMail)) {
        var getContacts_NewContact = createContact_NewContact.then(function (response) {
            return $.ajax({
                url: `/API/Contacts/All`,
                method: "GET",
                dataType: "json",
            }).then(function (response) {
                $("#new-mail-form #EditMail_IdSender").empty();
                $("#new-mail-form #EditMail_IdRecipient").empty();
                for (let contact of response) {
                    $("#new-mail-form #EditMail_IdSender").append(`<option value="${contactUtilities.getId(contact)}">${contactUtilities.getName(contact)}</option>`);
                    $("#new-mail-form #EditMail_IdRecipient").append(`<option value="${contactUtilities.getId(contact)}">${contactUtilities.getName(contact)}</option>`);
                }
                console.log(response);
                return response;
            }).fail(function (response) {
                errorNotification('خطأ', "وقع خطأ أثناء تحميل المراسلين .", 3000);
                console.log(response);
            });
        });
    }
});