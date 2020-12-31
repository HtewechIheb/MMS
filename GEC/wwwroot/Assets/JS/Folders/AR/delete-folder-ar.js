$("#folder-dynamic-table, #folder-thumbnail-view").on("click", ".folder-delete-button", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    Swal.fire({
        title: 'Êtes-vous sûrs ?',
        text: "Cette action est irréversible.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Confirmer',
        cancelButtonText: 'Annuler',
        focusConfirm: false,
    }).then((result) => {
        if (result.value) {
            var folderId = $(this).attr("data-folder-id");
            $.ajax({
                url: `/API/Folders/${folderId}`,
                method: "DELETE",
                dataType: "json",
            }).then(function (response) {
                getAndRenderFolders();
                successNotification('نجاح', "تم محو الملف بنجاح.", 3000);
                console.log(response);
                return response;
            }).fail(function (response) {
                errorNotification('خطأ', "وقع خطأ أثناء محو الملف.", 3000);
                console.log(response);
            });
        }
    })
});