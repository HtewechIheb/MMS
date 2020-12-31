$("#mail-dynamic-table, #mail-list-view").on("click", ".mail-delete-button", function (event) {
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
            var mailId = $(this).attr("data-mail-id");
            $.ajax({
                url: `/API/Mails/${mailId}`,
                method: "DELETE",
                dataType: "json",
            }).then(function (response) {
                getAndRenderMails();
                successNotification('Succès', "Courrier supprimé avec succès.", 3000);
                console.log(response);
                return response;
            }).fail(function (response) {
                errorNotification('Erreur', "Erreur lors de la suppression du courrier.", 3000);
                console.log(response);
            });
        }
    })
});