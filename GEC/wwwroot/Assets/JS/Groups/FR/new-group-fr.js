$("#new-group-button").on("click", function () {
    currentPopups.push(popups.newGroup);
    $(".new-group-popup-wrapper").css("display", "block");
});

$(".new-group-popup .closeButton").on("click", function () {
    currentPopups.pop();
    $("#new-group-form")[0].reset();
    $(".new-group-popup-wrapper").css("display", "none");
});

$("#new-group-form").on("reset", function () {
    $("#new-group-form .field-validation-error").empty();
});

$("#new-group-form").on("submit", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    if (!$("#new-group-form").valid()) {
        return false;
    }
    $("#new-group-submit-button").attr("disabled", "disabled");
    $("#new-group-reset-button").attr("disabled", "disabled");
    $(".new-group-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".new-group-popup .processing-icon-inline-block-hidden-576").removeClass("hidden");

    var formData = new FormData();
    if (![null, undefined].includes($("#new-group-form #NewGroup_Name").val())) {
        formData.append("Name", $("#new-group-form #NewGroup_Name").val());
    }
    if (![null, undefined].includes($("#new-group-form #NewGroup_Description").val())) {
        formData.append("Description", $("#new-group-form #NewGroup_Description").val());
    }

    var createGroup_NewGroup = $.ajax({
        url: `/API/Groups`,
        method: "POST",
        data: formData,
        processData: false,
        contentType: false,
    }).then(function (response) {
        if (currentPage === pages.groups) {
            getAndRenderGroups();
        }
        currentPopups.pop();
        $("#new-group-form")[0].reset();
        $(".new-group-popup-wrapper").css("display", "none");
        successNotification('Succès', "Group ajouté avec succès.", 3000);
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('Erreur', "Erreur lors de l'ajout du group.", 3000);
        console.log(response);
    }).always(function () {
        $(".new-group-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".new-group-popup .processing-icon-inline-block-hidden-576").addClass("hidden");
        $("#new-group-submit-button").removeAttr("disabled");
        $("#new-group-reset-button").removeAttr("disabled");
    });
});