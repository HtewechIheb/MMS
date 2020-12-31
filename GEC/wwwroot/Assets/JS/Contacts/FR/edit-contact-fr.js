$(".edit-contact-popup .closeButton").on("click", function () {
    currentPopups.pop();
    $("#edit-contact-form")[0].reset();
    $(".edit-contact-popup-wrapper").css("display", "none");
});

$("#edit-contact-form").on("reset", function () {
    $("#edit-contact-form .field-validation-error").empty();
});

$("#contact-dynamic-table, #contact-list-view").on("click", ".contact-edit-button", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    currentPopups.push(popups.editContact);
    $("#edit-contact-submit-button").attr("disabled", "disabled");
    $("#edit-contact-reset-button").attr("disabled", "disabled");
    $("#edit-contact-upload-button").attr("disabled", "disabled");
    $(".edit-contact-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".edit-contact-popup .processing-icon-inline-block-hidden-576").removeClass("hidden");
    $(".edit-contact-popup-wrapper").css("display", "block");
    $("#edit-contact-form #EditContact_Id").val($(this).attr("data-contact-id"));
    var contactId = $(this).attr("data-contact-id");

    var getContactDetails_EditContact =  $.ajax({
        url: `/API/Contacts/${contactId}`,
        method: "GET",
        dataType: "json",
    }).then(function (response) {
        if (response.nature != null) {
            $("#edit-contact-form #EditContact_Nature").val(response.nature);
        }
        if (response.contactType != null) {
            $("#edit-contact-form #EditContact_ContactType").val(response.contactType);
        }
        if (response.name != null) {
            $("#edit-contact-form #EditContact_Name").val(response.name);
        }
        if (response.email1 != null) {
            $("#edit-contact-form #EditContact_Email1").val(response.email1);
        }
        if (response.email2 != null) {
            $("#edit-contact-form #EditContact_Email2").val(response.email2);
        }
        if (response.telephone1 != null) {
            $("#edit-contact-form #EditContact_Telephone1").val(response.telephone1);
        }
        if (response.telephone2 != null) {
            $("#edit-contact-form #EditContact_Telephone2").val(response.telephone2);
        }
        if (response.fax != null) {
            $("#edit-contact-form #EditContact_Fax").val(response.fax);
        }
        if (response.address != null) {
            $("#edit-contact-form #EditContact_Address").val(response.address);
        }
        console.log(response);
        return response;
    }).fail(function (response) {
        currentPopups.pop();
        $("#edit-contact-form")[0].reset();
        $(".edit-contact-popup-wrapper").css("display", "none");
        errorNotification('Erreur', "Erreur lors du chargement du contact.", 3000);
        console.log(response);
    }).always(function () {
        $(".edit-contact-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".edit-contact-popup .processing-icon-inline-block-hidden-576").addClass("hidden");
        $("#edit-contact-submit-button").removeAttr("disabled");
        $("#edit-contact-reset-button").removeAttr("disabled");
        $("#edit-contact-upload-button").removeAttr("disabled");
    });
});

$("#edit-contact-form").on("submit", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    if (!$("#edit-contact-form").valid()) {
        return false;
    }
    $("#edit-contact-submit-button").attr("disabled", "disabled");
    $("#edit-contact-reset-button").attr("disabled", "disabled");
    $("#edit-contact-upload-button").attr("disabled", "disabled");
    $(".edit-contact-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".edit-contact-popup .processing-icon-inline-block-hidden-576").removeClass("hidden");
    var contactId = $("#edit-contact-form #EditContact_Id").val();

    var formData = new FormData();
    if (![null, undefined].includes($("#edit-contact-form #EditContact_Id").val())) {
        formData.append("Id", $("#edit-contact-form #EditContact_Id").val());
    }
    if (![null, undefined].includes($("#edit-contact-form #EditContact_Nature").val())) {
        formData.append("Nature", $("#edit-contact-form #EditContact_Nature").val());
    }
    if (![null, undefined].includes($("#edit-contact-form #EditContact_ContactType").val())) {
        formData.append("ContactType", $("#edit-contact-form #EditContact_ContactType").val());
    }
    if (![null, undefined].includes($("#edit-contact-form #EditContact_Name").val())) {
        formData.append("Name", $("#edit-contact-form #EditContact_Name").val());
    }
    if (![null, undefined].includes($("#edit-contact-form #EditContact_Email1").val())) {
        formData.append("Email1", $("#edit-contact-form #EditContact_Email1").val());
    }
    if (![null, undefined].includes($("#edit-contact-form #EditContact_Email2").val())) {
        formData.append("Email2", $("#edit-contact-form #EditContact_Email2").val());
    }
    if (![null, undefined].includes($("#edit-contact-form #EditContact_Telephone1").val())) {
        formData.append("Telephone1", $("#edit-contact-form #EditContact_Telephone1").val());
    }
    if (![null, undefined].includes($("#edit-contact-form #EditContact_Telephone2").val())) {
        formData.append("Telephone2", $("#edit-contact-form #EditContact_Telephone2").val());
    }
    if (![null, undefined].includes($("#edit-contact-form #EditContact_Fax").val())) {
        formData.append("Fax", $("#edit-contact-form #EditContact_Fax").val());
    }
    if (![null, undefined].includes($("#edit-contact-form #EditContact_Address").val())) {
        formData.append("Address", $("#edit-contact-form #EditContact_Address").val());
    }

    var updateContact_EditContact =  $.ajax({
        url: `/API/Contacts/${contactId}`,
        method: "PUT",
        data: formData,
        processData: false,
        contentType: false,
    }).then(function (response) {
        getAndRenderContacts();
        currentPopups.pop();
        $("#edit-contact-form")[0].reset();
        $(".edit-contact-popup-wrapper").css("display", "none");
        successNotification('Succès', "Contact modifié avec succès.", 3000);
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('Erreur', "Erreur lors de la modification du contact.", 3000);
        console.log(response);
    }).always(function () {
        $(".edit-contact-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".edit-contact-popup .processing-icon-inline-block-hidden-576").addClass("hidden");
        $("#edit-contact-submit-button").removeAttr("disabled");
        $("#edit-contact-reset-button").removeAttr("disabled");
        $("#edit-contact-upload-button").removeAttr("disabled");
    });
});