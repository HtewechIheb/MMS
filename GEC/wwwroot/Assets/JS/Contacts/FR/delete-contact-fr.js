$("#contact-dynamic-table, #contact-list-view").on("click", ".contact-delete-button", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    Swal.fire({
        title: 'Êtes-vous sûrs ?',
        text: "Tous les courriers associés seront supprimés, cette action est irréversible.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Confirmer',
        cancelButtonText: 'Annuler',
        focusConfirm: false,
    }).then((result) => {
        if (result.value) {
            var contactId = $(this).attr("data-contact-id");
            $.ajax({
                url: `/API/Contacts/${contactId}`,
                method: "DELETE",
                dataType: "json",
            }).then(function (response) {
                getAndRenderContacts();
                successNotification('Succès', "Contact supprimé avec succès.", 3000);
                console.log(response);
                return response;
            }).fail(function (response) {
                errorNotification('Erreur', "Erreur lors de la suppression du contact.", 3000);
                console.log(response);
            });
        }
    })
});