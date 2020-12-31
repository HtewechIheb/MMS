$(".edit-folder-popup .closeButton").on("click", function () {
    currentPopups.pop();
    $("#edit-folder-form")[0].reset();
    $(".edit-folder-popup-wrapper").css("display", "none");
});

$("#edit-folder-form").on("reset", function () {
    $("#edit-folder-form .field-validation-error").empty();
});

$("#folder-dynamic-table, #folder-thumbnail-view").on("click", ".folder-edit-button", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    currentPopups.push(popups.editFolder);
    $("#edit-folder-submit-button").attr("disabled", "disabled");
    $("#edit-folder-reset-button").attr("disabled", "disabled");
    $("#edit-folder-upload-button").attr("disabled", "disabled");
    $(".edit-folder-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".edit-folder-popup .processing-icon-inline-block-hidden-576").removeClass("hidden");
    $(".edit-folder-popup-wrapper").css("display", "block");
    $("#edit-folder-form #EditFolder_Id").val($(this).attr("data-folder-id"));
    var folderId = $(this).attr("data-folder-id");

    var getFolderDetails_EditFolder =  $.ajax({
        url: `/API/Folders/${folderId}`,
        method: "GET",
        dataType: "json",
    }).then(function (response) {
        if (response.name != null) {
            $("#edit-folder-form #EditFolder_Name").val(response.name);
        }
        if (response.description != null) {
            $("#edit-folder-form #EditFolder_Description").val(response.description);
        }
        console.log(response);
        return response;
    }).fail(function (response) {
        currentPopups.pop();
        $("#edit-folder-form")[0].reset();
        $(".edit-folder-popup-wrapper").css("display", "none");
        errorNotification('خطأ', "وقع خطأ أثناء تحميل الملف.", 3000);
        console.log(response);
    }).always(function () {
        $(".edit-folder-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".edit-folder-popup .processing-icon-inline-block-hidden-576").addClass("hidden");
        $("#edit-folder-submit-button").removeAttr("disabled");
        $("#edit-folder-reset-button").removeAttr("disabled");
        $("#edit-folder-upload-button").removeAttr("disabled");
    });
});

$("#edit-folder-form").on("submit", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    if (!$("#edit-folder-form").valid()) {
        return false;
    }
    $("#edit-folder-submit-button").attr("disabled", "disabled");
    $("#edit-folder-reset-button").attr("disabled", "disabled");
    $("#edit-folder-upload-button").attr("disabled", "disabled");
    $(".edit-folder-popup .processing-icon-block-shown-576").removeClass("hidden");
    $(".edit-folder-popup .processing-icon-inline-block-hidden-576").removeClass("hidden");
    var folderId = $("#edit-folder-form #EditFolder_Id").val();

    var formData = new FormData();
    if (![null, undefined].includes($("#edit-folder-form #EditFolder_Id").val())) {
        formData.append("Id", $("#edit-folder-form #EditFolder_Id").val());
    }
    if (![null, undefined].includes($("#edit-folder-form #EditFolder_Name").val())) {
        formData.append("Name", $("#edit-folder-form #EditFolder_Name").val());
    }
    if (![null, undefined].includes($("#edit-folder-form #EditFolder_Description").val())) {
        formData.append("Description", $("#edit-folder-form #EditFolder_Description").val());
    }

    var updateFolder_EditFolder =  $.ajax({
        url: `/API/Folders/${folderId}`,
        method: "PUT",
        data: formData,
        processData: false,
        contentType: false,
    }).then(function (response) {
        getAndRenderFolders();
        currentPopups.pop();
        $("#edit-folder-form")[0].reset();
        $(".edit-folder-popup-wrapper").css("display", "none");
        successNotification('نجاح', "تم تعديل الملف بنجاح.", 3000);
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('خطأ', "وقع خطأ أثناء تعديل الملف.", 3000);
        console.log(response);
    }).always(function () {
        $(".edit-folder-popup .processing-icon-block-shown-576").addClass("hidden");
        $(".edit-folder-popup .processing-icon-inline-block-hidden-576").addClass("hidden");
        $("#edit-folder-submit-button").removeAttr("disabled");
        $("#edit-folder-reset-button").removeAttr("disabled");
        $("#edit-folder-upload-button").removeAttr("disabled");
    });
});