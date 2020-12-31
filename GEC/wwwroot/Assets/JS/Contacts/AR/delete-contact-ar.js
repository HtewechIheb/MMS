$("#contact-dynamic-table, #contact-list-view").on("click", ".contact-delete-button", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    Swal.fire({
        title: 'هل أنت متأكد ؟',
        text: "هذا الإجراء لا رجعة فيه.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'تأكيد',
        cancelButtonText: 'إلغاء',
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
                successNotification('نجاح', "تم محو المراسل بنجاح.", 3000);
                console.log(response);
                return response;
            }).fail(function (response) {
                errorNotification('خطأ', "وقع خطأ أثناء محو المراسل.", 3000);
                console.log(response);
            });
        }
    })
});