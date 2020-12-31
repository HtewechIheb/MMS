$(document).on("click", "button, i", function () {
    $(this.blur());
});

//Sidemenu Icons Tooltips
$('[data-toggle=tooltip]').tooltip();

//Application Pages
var pages = {
    home: "Home",
    mails: "Mails",
    mailsIngoing: "Mails Ingoing",
    mailsOutgoing: "Mails Outgoing",
    mailsInternal: "Mails Internal",
    mailShow: "Mail Show",
    folders: "Folders",
    folderShow: "Folder Show",
    contacts: "Contacts",
    contactShow: "Contact Show",
    groups: "Groups",
    groupShow: "Group Show"
}

var popups = {
    newMail: "New Mail",
    editMail: "Edit Mail",
    newFolder: "New Folder",
    newContact: "New Contact",
    editContact: "Edit Contact",
    newGroup: "New Group",
    editGroup: "Edit Group",
    manageGroupMembers: "ManageGroupMembers"
}

//Global Variables
var currentPage;
var currentPopups = [];
const mailsPerYearDigit = 4;

//Logout Button
$("#logout-button").on("click", function (event) {
    event.preventDefault();
    $("#logout-form").submit();
})

//Anchor Elements Like Buttons And Icons
$(".anchor-button").on("click", function (event) {
    event.stopImmediatePropagation();
    event.preventDefault();
    let URL = $(this).attr("data-url");
    window.location.assign(`${URL}`);
});

//Notifications
function successNotification(title, msg, time) {
    $.gritter.add({
        title: title,
        text: msg,
        class_name: 'gritter-success',
        time: time
    });
}

function errorNotification(title, msg, time) {
    $.gritter.add({
        title: title,
        text: msg,
        class_name: 'gritter-error',
        time: time
    });
}

//Search
$("#nav-search-input").popover({
    html: true,
    trigger: "manual"
});

function searchSubmit() {
    const form = $("#nav-search-form");
    const input = $("#nav-search-input");
    const inputValue = input.val();
    if (inputValue != "") {
        if (moment(inputValue, "DD/MM/YYYY").isValid()) {
            form[0].dayString.value = moment(inputValue, "DD/MM/YYYY").format("DD");
            form[0].monthString.value = moment(inputValue, "DD/MM/YYYY").format("MM");
            form[0].yearString.value = moment(inputValue, "DD/MM/YYYY").format("YYYY");
        }
        else if (moment(inputValue, "MM/YYYY").isValid()) {
            form[0].monthString.value = moment(inputValue, "MM/YYYY").format("MM");
            form[0].yearString.value = moment(inputValue, "MM/YYYY").format("YYYY");
        }
        else if (moment(inputValue, "YYYY").isValid()) {
            form[0].yearString.value = moment(inputValue, "YYYY").format("YYYY");
        }
        form[0].submit();
    }
    else {
        input.focus();
        input.popover("show");
        input.on("blur", function () {
            $(this).popover("hide");
        });
        setTimeout(function () {
            $("#nav-search-input").popover("hide");
        }, 3500);
    }
}

$("#nav-search-input").on("keydown", function (event) {
    if (event.key === "Enter") {
        event.preventDefault();
        searchSubmit();
    }
});

$("#nav-search-button").on("click", searchSubmit);