//Sending Date Client Side Validation
$.validator.unobtrusive.adapters.add("editmailsendingdatevalidation", [], function (options) {
    options.rules.editmailsendingdatevalidation = {};
    options.messages["editmailsendingdatevalidation"] = options.message;
});

$.validator.addMethod('editmailsendingdatevalidation', function (value, element, params) {
    if (!value) {
        return true;
    }
    if ($(element.form).attr("id") == "edit-mail-form") {
        return Date.parse(value) <= Date.parse($("#edit-mail-form #EditMail_RegistrationDate").val());
    }
});

$(".edit-mail-popup .closeButton").on("click", function () {
    currentPopups.pop();
    $("#edit-mail-form")[0].reset();
    $(".edit-mail-popup-wrapper").css("display", "none");
});

$("#edit-mail-file").on("change", function () {
    $("#edit-mail-file-viewer").attr("src", URL.createObjectURL(this.files[0]));
});

$("#edit-mail-form").on("reset", function () {
    $(".field-validation-error").empty();
    $("#edit-mail-file-viewer").attr("src", "/Document Placeholders/FR/NoDocument_Fr.html");
});

function setRegistrationNumber_EditMail() {
    let mailType = $("#edit-mail-form #EditMail_MailType").val();
    let currentYear = moment().format("YY");
    let registrationNumber;
    let registrationNumberSubstring = $("#edit-mail-form #EditMail_RegistrationNumber").val().slice(1);
    switch (mailType) {
        case 0: registrationNumber = `E${registrationNumberSubstring}`;
            break;
        case 1: registrationNumber = `S${registrationNumberSubstring}`;
            break;
        case 2: registrationNumber = `I${registrationNumberSubstring}`;
            break;
    }
    $("#edit-mail-form #EditMail_RegistrationNumber").val(registrationNumber);
}

$("#edit-mail-form #EditMail_MailType").on("change", function () {
    setRegistrationNumber_EditMail();
});

$("#mail-dynamic-table, #mail-list-view").on("click", ".mail-edit-button", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    currentPopups.push(popups.editMail);
    $("#edit-mail-submit-button").attr("disabled", "disabled");
    $("#edit-mail-reset-button").attr("disabled", "disabled");
    $("#edit-mail-upload-button").attr("disabled", "disabled");
    $(".edit-mail-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".edit-mail-popup .processing-icon-inline-block-hidden-576").removeClass("hidden");
    $(".edit-mail-popup-wrapper").css("display", "block");
    $("#edit-mail-form #EditMail_Id").val($(this).attr("data-mail-id"));
    var mailId = $(this).attr("data-mail-id");

    var getMailFolders_EditMail = $.ajax({
        url: `/API/Folders/All`,
        method: "GET",
        dataType: "json",
    }).then(function (response) {
        $("#edit-mail-form #EditMail_IdFolder").empty();
        $("#edit-mail-form #EditMail_IdFolder").append(`<option value="">Aucun</option>`);
        for (let folder of response) {
            $("#edit-mail-form #EditMail_IdFolder").append(`<option value="${folderUtilities.getId(folder)}">${folderUtilities.getName(folder)}</option>`);
        }
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('Erreur', "Erreur lors du chargement des dossiers.", 3000);
        console.log(response);
    });

    var getMailContacts_EditMail = $.ajax({
        url: `/API/Contacts/All`,
        method: "GET",
        dataType: "json",
    }).then(function (response) {
        $("#edit-mail-form #EditMail_IdSender").empty();
        $("#edit-mail-form #EditMail_IdRecipient").empty();
        for (let contact of response) {
            $("#edit-mail-form #EditMail_IdSender").append(`<option value="${contactUtilities.getId(contact)}">${contactUtilities.getName(contact)}</option>`);
            $("#edit-mail-form #EditMail_IdRecipient").append(`<option value="${contactUtilities.getId(contact)}">${contactUtilities.getName(contact)}</option>`);
        }
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('Erreur', "Erreur lors du chargement des contacts.", 3000);
        console.log(response);
    });

    var getMailDetails_EditMail = $.ajax({
        url: `/API/Mails/${mailId}`,
        method: "GET",
        dataType: "json",
    }).then(function (response) {
        if (response.mailType != null) {
            $("#edit-mail-form #EditMail_MailType").val(response.mailType);
        }
        if (response.channel != null) {
            $("#edit-mail-form #EditMail_Channel").val(response.channel);
        }
        if (response.registrationNumber != null) {
            $("#edit-mail-form #EditMail_RegistrationNumber").val(response.registrationNumber);
        }
        if (response.registrationDate != null) {
            $("#edit-mail-form #EditMail_RegistrationDate").val(moment(response.registrationDate).format("YYYY-MM-DD"));
        }
        if (response.idSender != null) {
            $("#edit-mail-form #EditMail_IdSender").val(response.idSender);
        }
        if (response.idRecipient != null) {
            $("#edit-mail-form #EditMail_IdRecipient").val(response.idRecipient);
        }
        if (response.object != null) {
            $("#edit-mail-form #EditMail_Object").val(response.object);
        }
        if (response.confidentiality != null) {
            var confidentiality = $("#edit-mail-form input[name='EditMail.Confidentiality']");
            confidentiality.each(function (input) {
                if (confidentiality[input].value == response.confidentiality.toString()) {
                    $(confidentiality[input]).attr("checked", "checked");
                }
            });
        }
        if (response.hasHardCopy != null) {
            var hasHardCopy = $("#edit-mail-form input[name='EditMail.HasHardCopy']");
            hasHardCopy.each(function (input) {
                if (hasHardCopy[input].value == response.hasHardCopy.toString()) {
                    $(hasHardCopy[input]).attr("checked", "checked");
                }
            });
        }
        if (response.sendingDate != null) {
            $("#edit-mail-form #EditMail_SendingDate").val(moment(response.sendingDate).format("YYYY-MM-DD"));
        }
        if (response.senderRegistrationNumber != null) {
            $("#edit-mail-form #EditMail_SenderRegistrationNumber").val(response.senderRegistrationNumber);
        }
        if (response.processingTimeFrame != null) {
            $("#edit-mail-form #EditMail_ProcessingTimeFrame").val(response.processingTimeFrame);
        }
        else {
            $("#edit-mail-form #EditMail_ProcessingTimeFrame").val('0');
        }
        if (response.language != null) {
            $("#edit-mail-form #EditMail_Language").val(response.language);
        }
        else {
            $("#edit-mail-form #EditMail_Language").val('0');
        }
        if (response.keyWords != null) {
            $("#edit-mail-form #EditMail_KeyWords").val(response.keyWords);
        }
        if (response.idFolder != null) {
            $("#edit-mail-form #EditMail_IdFolder").val(response.idFolder);
        }
        else {
            $("#edit-mail-form #EditMail_IdFolder").val("");
        }
        if (response.observations != null) {
            $("#edit-mail-form #EditMail_Observations").val(response.observations);
        }
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('Erreur', "Erreur lors du chargement des détails du courrier.", 3000);
        console.log(response);
    });

    var getMailFile_EditMail = getMailDetails_EditMail.then(function (response) {
        if (![null, undefined].includes(response.digitizedFile)) {
            return $.ajax({
                url: `/API/Mails/Download/${mailId}`,
                method: "GET",
                contentType: 'application/json; charset=utf-8',
                datatype: 'json',
            }).then(function (response) {
                $("#edit-mail-file-viewer").attr("src", `/API/Mails/Download/${mailId}`);
                console.log(response);
                return response;
            }).fail(function (response) {
                errorNotification('Erreur', "Erreur lors du chargement du document du courrier.", 3000);
                console.log(response);
            });
        }
        else {
            return true;
        }
    });

    $.when(getMailFolders_EditMail, getMailContacts_EditMail, getMailDetails_EditMail, getMailFile_EditMail).done(function () {
        $("#edit-mail-submit-button").removeAttr("disabled");
        $("#edit-mail-reset-button").removeAttr("disabled");
        $("#edit-mail-upload-button").removeAttr("disabled");
    }).fail(function () {
        currentPopups.pop();
        $("#edit-mail-form")[0].reset();
        $(".edit-mail-popup-wrapper").css("display", "none");
    }).always(function () {
        $(".edit-mail-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".edit-mail-popup .processing-icon-inline-block-hidden-576").addClass("hidden");
    });
});

$("#edit-mail-form").on("submit", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    if (!$("#edit-mail-form").valid()) {
        return false;
    }
    $("#edit-mail-submit-button").attr("disabled", "disabled");
    $("#edit-mail-reset-button").attr("disabled", "disabled");
    $("#edit-mail-upload-button").attr("disabled", "disabled");
    $(".edit-mail-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".edit-mail-popup .processing-icon-inline-block-hidden-576").removeClass("hidden");
    var mailId = $("#edit-mail-form #EditMail_Id").val();

    var formData = new FormData();
    if (![null, undefined].includes($("#edit-mail-form #EditMail_Id").val())) {
        formData.append("Id", $("#edit-mail-form #EditMail_Id").val());
    }
    if (![null, undefined].includes($("#edit-mail-form #EditMail_MailType").val())) {
        formData.append("MailType", $("#edit-mail-form #EditMail_MailType").val());
    }
    if (![null, undefined].includes($("#edit-mail-form #EditMail_Channel").val())) {
        formData.append("Channel", $("#edit-mail-form #EditMail_Channel").val());
    }
    if (![null, undefined].includes($("#edit-mail-form #EditMail_RegistrationNumber").val())) {
        formData.append("RegistrationNumber", $("#edit-mail-form #EditMail_RegistrationNumber").val());
    }
    if (![null, undefined].includes($("#edit-mail-form #EditMail_RegistrationDate").val())) {
        formData.append("RegistrationDate", $("#edit-mail-form #EditMail_RegistrationDate").val());
    }
    if (![null, undefined].includes($("#edit-mail-form #EditMail_IdSender").val())) {
        formData.append("IdSender", $("#edit-mail-form #EditMail_IdSender").val());
    }
    if (![null, undefined].includes($("#edit-mail-form #EditMail_IdRecipient").val())) {
        formData.append("IdRecipient", $("#edit-mail-form #EditMail_IdRecipient").val());
    }
    if (![null, undefined].includes($("#edit-mail-form #EditMail_Object").val())) {
        formData.append("Object", $("#edit-mail-form #EditMail_Object").val());
    }
    if (![null, undefined].includes($("#edit-mail-form input[name='EditMail.Confidentiality']:checked").val())) {
        formData.append("Confidentiality", $("#edit-mail-form input[name='EditMail.Confidentiality']:checked").val());
    }
    if (![null, undefined].includes($("#edit-mail-form input[name='EditMail.HasHardCopy']:checked").val())) {
        formData.append("HasHardCopy", $("#edit-mail-form input[name='EditMail.HasHardCopy']:checked").val());
    }
    if (![null, undefined].includes($("#edit-mail-form #EditMail_SendingDate").val())) {
        formData.append("SendingDate", $("#edit-mail-form #EditMail_SendingDate").val());
    }
    if (![null, undefined, '0'].includes($("#edit-mail-form #EditMail_ProcessingTimeFrame").val())) {
        formData.append("ProcessingTimeFrame", $("#edit-mail-form #EditMail_ProcessingTimeFrame").val());
    }
    if (![null, undefined, '0'].includes($("#edit-mail-form #EditMail_Language").val())) {
        formData.append("Language", $("#edit-mail-form #EditMail_Language").val());
    }
    if (![null, undefined].includes($("#edit-mail-form #EditMail_Keywords").val())) {
        formData.append("KeyWords", $("#edit-mail-form #EditMail_Keywords").val());
    }
    if (![null, undefined].includes($("#edit-mail-form #EditMail_IdFolder").val())) {
        formData.append("IdFolder", $("#edit-mail-form #EditMail_IdFolder").val());
    }
    if (![null, undefined].includes($("#edit-mail-form #EditMail_Observations").val())) {
        formData.append("Observations", $("#edit-mail-form #EditMail_Observations").val());
    }
    if (![null, undefined].includes($("#edit-mail-form #edit-mail-file")[0].files[0])) {
        formData.append("DigitizedFile", $("#edit-mail-form #edit-mail-file")[0].files[0]);
    }

    var updateMail_EditMail = $.ajax({
        url: `/API/Mails/${mailId}`,
        method: "PUT",
        data: formData,
        processData: false,
        contentType: false,
    }).then(function (response) {
        getAndRenderMails();
        currentPopups.pop();
        $("#edit-mail-form")[0].reset();
        $(".edit-mail-popup-wrapper").css("display", "none");
        successNotification('Succès', "Courrier modifié avec succès.", 3000);
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('Erreur', "Erreur lors de la modification du courrier.", 3000);
        console.log(response);
    }).always(function () {
        $(".edit-mail-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".edit-mail-popup .processing-icon-inline-block-hidden-576").addClass("hidden");
        $("#edit-mail-submit-button").removeAttr("disabled");
        $("#edit-mail-reset-button").removeAttr("disabled");
        $("#edit-mail-upload-button").removeAttr("disabled");
    });
});