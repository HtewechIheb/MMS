$(".edit-group-popup .closeButton").on("click", function () {
    currentPopups.pop();
    $("#edit-group-form")[0].reset();
    $(".edit-group-popup-wrapper").css("display", "none");
});

$("#edit-group-form").on("reset", function () {
    $("#edit-group-form .field-validation-error").empty();
});

$("#group-dynamic-table, #group-thumbnail-view").on("click", ".group-edit-button", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    currentPopups.push(popups.editGroup);
    $("#edit-group-submit-button").attr("disabled", "disabled");
    $("#edit-group-reset-button").attr("disabled", "disabled");
    $(".edit-group-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".edit-group-popup .processing-icon-inline-block-hidden-576").removeClass("hidden");
    $(".edit-group-popup-wrapper").css("display", "block");
    $("#edit-group-form #EditGroup_Id").val($(this).attr("data-group-id"));
    var groupId = $(this).attr("data-group-id");

    var getGroupDetails_EditGroup = $.ajax({
        url: `/API/Groups/${groupId}`,
        method: "GET",
        dataType: "json",
    }).then(function (response) {
        if (response.name != null) {
            $("#edit-group-form #EditGroup_Name").val(groupUtilities.getName(response));
        }
        if (response.description != null) {
            $("#edit-group-form #EditGroup_Description").val(groupUtilities.getDescription(response));
        }
        console.log(response);
        return response;
    }).fail(function (response) {
        currentPopups.pop();
        $("#edit-group-form")[0].reset();
        $(".edit-group-popup-wrapper").css("display", "none");
        errorNotification('Erreur', "Erreur lors du chargement du groupe.", 3000);
        console.log(response);
    }).always(function () {
        $(".edit-group-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".edit-group-popup .processing-icon-inline-block-hidden-576").addClass("hidden");
        $("#edit-group-submit-button").removeAttr("disabled");
        $("#edit-group-reset-button").removeAttr("disabled");
    });
});

$("#edit-group-form").on("submit", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    if (!$("#edit-group-form").valid()) {
        return false;
    }
    $("#edit-group-submit-button").attr("disabled", "disabled");
    $("#edit-group-reset-button").attr("disabled", "disabled");
    $(".edit-group-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".edit-group-popup .processing-icon-inline-block-hidden-576").removeClass("hidden");
    var groupId = $("#edit-group-form #EditGroup_Id").val();

    var formData = new FormData();
    if (![null, undefined].includes($("#edit-group-form #EditGroup_Id").val())) {
        formData.append("Id", $("#edit-group-form #EditGroup_Id").val());
    }
    if (![null, undefined].includes($("#edit-group-form #EditGroup_Name").val())) {
        formData.append("Name", $("#edit-group-form #EditGroup_Name").val());
    }
    if (![null, undefined].includes($("#edit-group-form #EditGroup_Description").val())) {
        formData.append("Description", $("#edit-group-form #EditGroup_Description").val());
    }

    var updateGroup_EditGroup = $.ajax({
        url: `/API/Groups/${groupId}`,
        method: "PUT",
        data: formData,
        processData: false,
        contentType: false,
    }).then(function (response) {
        getAndRenderGroups();
        currentPopups.pop();
        $("#edit-group-form")[0].reset();
        $(".edit-group-popup-wrapper").css("display", "none");
        successNotification('Succès', "Groupe modifié avec succès.", 3000);
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('Erreur', "Erreur lors de la modification du groupe.", 3000);
        console.log(response);
    }).always(function () {
        $(".edit-group-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".edit-group-popup .processing-icon-inline-block-hidden-576").addClass("hidden");
        $("#edit-group-submit-button").removeAttr("disabled");
        $("#edit-group-reset-button").removeAttr("disabled");
    });
});