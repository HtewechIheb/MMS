$(".new-folder-button").on("click", function () {
    currentPopups.push(popups.newFolder);
    $(".new-folder-popup-wrapper").css("display", "block");
});

$(".new-folder-popup .closeButton").on("click", function () {
    currentPopups.pop();
    $("#new-folder-form")[0].reset();
    $(".new-folder-popup-wrapper").css("display", "none");
});

$("#new-folder-form").on("reset", function () {
    $("#new-folder-form .field-validation-error").empty();
});

$("#new-folder-form").on("submit", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    if (!$("#new-folder-form").valid()) {
        return false;
    }
    $("#new-folder-submit-button").attr("disabled", "disabled");
    $("#new-folder-reset-button").attr("disabled", "disabled");
    $(".new-folder-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".new-folder-popup .processing-icon-inline-block-hidden-576").removeClass("hidden");

    var formData = new FormData();
    if (![null, undefined].includes($("#new-folder-form #NewFolder_Name").val())) {
        formData.append("Name", $("#new-folder-form #NewFolder_Name").val());
    }
    if (![null, undefined].includes($("#new-folder-form #NewFolder_Description").val())) {
        formData.append("Description", $("#new-folder-form #NewFolder_Description").val());
    }

    var createFolder_NewFolder = $.ajax({
        url: `/API/Folders`,
        method: "POST",
        data: formData,
        processData: false,
        contentType: false,
    }).then(function (response) {
        if (currentPage === pages.folders) {
            getAndRenderFolders();
        }
        currentPopups.pop();
        $("#new-folder-form")[0].reset();
        $(".new-folder-popup-wrapper").css("display", "none");
        successNotification('Succès', "Dossier ajouté avec succès.", 3000);
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('Erreur', "Erreur lors du l'ajout du dossier.", 3000);
        console.log(response);
    }).always(function () {
        $(".new-folder-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".new-folder-popup .processing-icon-inline-block-hidden-576").addClass("hidden");
        $("#new-folder-submit-button").removeAttr("disabled");
        $("#new-folder-reset-button").removeAttr("disabled");
    });

    if (currentPopups.includes(popups.newMail)) {
        var getFolders_NewFolder = createFolder_NewFolder.then(function (response) {
            return $.ajax({
                url: `/API/Folders/All`,
                method: "GET",
                dataType: "json",
            });
        }).then(function (response) {
            $("#new-mail-form #NewMail_IdFolder").empty();
            $("#new-mail-form #NewMail_IdFolder").append(`<option value="">Aucun</option>`);
            for (let folder of response) {
                $("#new-mail-form #NewMail_IdFolder").append(`<option value="${folderUtilities.getId(folder)}">${folderUtilities.getName(folder)}</option>`);
            }
            console.log(response);
            return response;
        }).fail(function (response) {
            errorNotification('Erreur', "Erreur lors du chargement des dossiers.", 3000);
            console.log(response);
        });
    }
    else if (currentPopups.includes(popups.editMail)) {
        var getFolders_NewFolder = createFolder_NewFolder.then(function (response) {
            return $.ajax({
                url: `/API/Folders/All`,
                method: "GET",
                dataType: "json",
            });
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
    }
});