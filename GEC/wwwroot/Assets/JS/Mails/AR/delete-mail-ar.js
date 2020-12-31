$("#mail-dynamic-table, #mail-list-view").on("click", ".mail-delete-button", function (event) {
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
            var mailId = $(this).attr("data-mail-id");
            $.ajax({
                url: `/API/Mails/${mailId}`,
                method: "DELETE",
                dataType: "json",
            }).then(function (response) {
                getAndRenderMails();
                successNotification('نجاح', "تم محو المراسلة بنجاح.", 3000);
                console.log(response);
                return response;
            }).fail(function (response) {
                errorNotification('خطأ', "وقع خطأ أثناء محو المراسلة.", 3000);
                console.log(response);
            });
        }
    })
});