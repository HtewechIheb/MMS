$("#group-dynamic-table, #group-thumbnail-view").on("click", ".group-delete-button", function (event) {
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
            var groupId = $(this).attr("data-group-id");
            $.ajax({
                url: `/API/Groups/${groupId}`,
                method: "DELETE",
                dataType: "json",
            }).then(function (response) {
                getAndRenderGroups();
                successNotification('نجاح', "تم محو المجموعة بنجاح.", 3000);
                console.log(response);
                return response;
            }).fail(function (response) {
                errorNotification('خطأ', "وقع خطأ أثناء محو المجموعة.", 3000);
                console.log(response);
            });
        }
    })
});