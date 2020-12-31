//Sending Date Client Side Validation
$.validator.unobtrusive.adapters.add("newmailsendingdatevalidation", [], function (options) {
    options.rules.newmailsendingdatevalidation = {};
    options.messages["newmailsendingdatevalidation"] = options.message;
});

$.validator.addMethod('newmailsendingdatevalidation', function (value, element, params) {
    if (!value) {
        return true;
    }
    if ($(element.form).attr("id") == "new-mail-form") {
        return Date.parse(value) <= Date.parse($("#new-mail-form #NewMail_RegistrationDate").val());

    }
});

function setRegistrationNumber_NewMail() {
    return $.ajax({
        url: `/API/Mails/Generator/RegistrationNumber`,
        method: "GET",
        data: {
            mailType: $("#new-mail-form #NewMail_MailType").val()
        }
    }).then(function (response) {
        $("#new-mail-form #NewMail_RegistrationNumber").val(response);
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('خطأ', "وقع خطأ أثناء إحداث رقم التسجيل .", 3000);
        console.log(response);
    });
}

$("#new-mail-form #NewMail_MailType").on("change", function () {
    setRegistrationNumber_NewMail();
});

$("#new-mail-button").on("click", function () {
    currentPopups.push(popups.newMail);
    $("#new-mail-submit-button").attr("disabled", "disabled");
    $("#new-mail-reset-button").attr("disabled", "disabled");
    $("#new-mail-upload-button").attr("disabled", "disabled");
    $(".new-mail-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".new-mail-popup .processing-icon-block-hidden-576").removeClass("hidden");
    $(".new-mail-popup-wrapper").css("display", "block");

    var getMailFolders_NewMail = $.ajax({
        url: `/API/Folders/All`,
        method: "GET",
        dataType: "json",
    }).then(function (response) {
        $("#new-mail-form #NewMail_IdFolder").empty();
        $("#new-mail-form #NewMail_IdFolder").append(`<option value="">بدون</option>`);
        for (let folder of response) {
            $("#new-mail-form #NewMail_IdFolder").append(`<option value="${folderUtilities.getId(folder)}">${folderUtilities.getName(folder)}</option>`);
        }
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('خطأ', "وقع خطأ أثناء تحميل الملفات .", 3000);
        console.log(response);
    });

    var getContacts_NewMail = $.ajax({
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

    $.when(setRegistrationNumber_NewMail(), getContacts_NewMail, getMailFolders_NewMail).done(function () {
        $("#new-mail-submit-button").removeAttr("disabled");
        $("#new-mail-reset-button").removeAttr("disabled");
        $("#new-mail-upload-button").removeAttr("disabled");
    }).fail(function () {
        currentPopups.pop();
        $("#new-mail-form")[0].reset();
        $(".new-mail-popup-wrapper").css("display", "none");
    }).always(function () {
        $(".new-mail-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".new-mail-popup .processing-icon-block-hidden-576").addClass("hidden");
    });
});

$(".new-mail-popup .closeButton").on("click", function () {
    currentPopups.pop();
    $("#new-mail-form")[0].reset();
    $(".new-mail-popup-wrapper").css("display", "none");
});

$("#new-mail-file").on("change", function () {
    $("#new-mail-file-viewer").attr("src", URL.createObjectURL(this.files[0]));
});

$("#new-mail-form").on("reset", function () {
    $("#new-mail-form .field-validation-error").empty();
    $("#new-mail-file-viewer").attr("src", "/Document Placeholders/AR/SelectDocument_Ar.html");
});

$("#new-mail-form").on("submit", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    if (!$("#new-mail-form").valid()) {
        return false;
    }
    $("#new-mail-submit-button").attr("disabled", "disabled");
    $("#new-mail-reset-button").attr("disabled", "disabled");
    $("#new-mail-upload-button").attr("disabled", "disabled");
    $(".new-mail-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".new-mail-popup .processing-icon-inline-block-hidden-576").removeClass("hidden");

    var formData = new FormData();
    if (![null, undefined].includes($("#new-mail-form #NewMail_MailType").val())) {
        formData.append("MailType", $("#new-mail-form #NewMail_MailType").val());
    }
    if (![null, undefined].includes($("#new-mail-form #NewMail_Channel").val())) {
        formData.append("Channel", $("#new-mail-form #NewMail_Channel").val());
    }
    if (![null, undefined].includes($("#new-mail-form #NewMail_RegistrationNumber").val())) {
        formData.append("RegistrationNumber", $("#new-mail-form #NewMail_RegistrationNumber").val());
    }
    if (![null, undefined].includes($("#new-mail-form #NewMail_RegistrationDate").val())) {
        formData.append("RegistrationDate", $("#new-mail-form #NewMail_RegistrationDate").val());
    }
    if (![null, undefined].includes($("#new-mail-form #NewMail_IdSender").val())) {
        formData.append("IdSender", $("#new-mail-form #NewMail_IdSender").val());
    }
    if (![null, undefined].includes($("#new-mail-form #NewMail_IdRecipient").val())) {
        formData.append("IdRecipient", $("#new-mail-form #NewMail_IdRecipient").val());
    }
    if (![null, undefined].includes($("#new-mail-form #NewMail_Object").val())) {
        formData.append("Object", $("#new-mail-form #NewMail_Object").val());
    }
    if (![null, undefined].includes($("#new-mail-form input[name='NewMail.Confidentiality']:checked").val())) {
        formData.append("Confidentiality", $("#new-mail-form input[name='NewMail.Confidentiality']:checked").val());
    }
    if (![null, undefined].includes($("#new-mail-form input[name='NewMail.HasHardCopy']:checked").val())) {
        formData.append("HasHardCopy", $("#new-mail-form input[name='NewMail.HasHardCopy']:checked").val());
    }
    if (![null, undefined].includes($("#new-mail-form #NewMail_SendingDate").val())) {
        formData.append("SendingDate", $("#new-mail-form #NewMail_SendingDate").val());
    }
    if (![null, undefined, '0'].includes($("#new-mail-form #NewMail_ProcessingTimeFrame").val())) {
        formData.append("ProcessingTimeFrame", $("#new-mail-form #NewMail_ProcessingTimeFrame").val());
    }
    if (![null, undefined, '0'].includes($("#new-mail-form #NewMail_Language").val())) {
        formData.append("Language", $("#new-mail-form #NewMail_Language").val());
    }
    if (![null, undefined].includes($("#new-mail-form #NewMail_KeyWords").val())) {
        formData.append("KeyWords", $("#new-mail-form #NewMail_KeyWords").val());
    }
    if (![null, undefined].includes($("#new-mail-form #NewMail_IdFolder").val())) {
        formData.append("IdFolder", $("#new-mail-form #NewMail_IdFolder").val());
    }
    if (![null, undefined].includes($("#new-mail-form #NewMail_Observations").val())) {
        formData.append("Observations", $("#new-mail-form #NewMail_Observations").val());
    }
    if (![null, undefined].includes($("#new-mail-form #new-mail-file")[0].files[0])) {
        formData.append("DigitizedFile", $("#new-mail-form #new-mail-file")[0].files[0]);
    }

    var createMail_NewMail = $.ajax({
        url: `/API/Mails`,
        method: "POST",
        data: formData,
        processData: false,
        contentType: false,
    }).then(function (response) {
        if (currentPage === pages.mails
            || currentPage === pages.folderShow
            || currentPage === pages.mailsIngoing
            || currentPage === pages.mailsOutgoing
            || currentPage === pages.mailsInternal) {
            getAndRenderMails();
        }
        currentPopups.pop();
        $("#new-mail-form")[0].reset();
        $(".new-mail-popup-wrapper").css("display", "none");
        successNotification('نجاح', "تم إضافة المراسلة بنجاح.", 3000);
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('خطأ', "وقع خطأ أثناء إضافة المراسلة .", 3000);
        console.log(response);
    }).always(function () {
        $(".new-mail-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".new-mail-popup .processing-icon-inline-block-hidden-576").addClass("hidden");
        $("#new-mail-submit-button").removeAttr("disabled");
        $("#new-mail-reset-button").removeAttr("disabled");
        $("#new-mail-upload-button").removeAttr("disabled");
    });
});