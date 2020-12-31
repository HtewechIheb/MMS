$("#ShowContact_Nature").text(contactUtilities.getNature(shownContact));
$("#ShowContact_ContactType").text(contactUtilities.getContactType(shownContact));
$("#ShowContact_Name").text(contactUtilities.getName(shownContact));
$("#ShowContact_Header").text(contactUtilities.getName(shownContact));
$("#ShowContact_Email1").text(contactUtilities.getEmail1(shownContact));
$("#ShowContact_Email2").text(contactUtilities.getEmail2(shownContact));
$("#ShowContact_Address").text(contactUtilities.getAddress(shownContact));
$("#ShowContact_Telephone1").text(contactUtilities.getTelephone1(shownContact));
$("#ShowContact_Telephone2").text(contactUtilities.getTelephone2(shownContact));
$("#ShowContact_Fax").text(contactUtilities.getFax(shownContact));

if (shownContactGroups.length === 0) {
    $("#ShowContact_ContactGroups").append(`
        <li class="list-group-item">${contactUtilities.getName(shownContact)} n'est membre d'aucun groupe.</li>
    `);
}
else {
    $.each(shownContactGroups, function (index, group) {
        $("#ShowContact_ContactGroups").append(`
            <li class="list-group-item">${groupUtilities.getName(group)}</li>
        `);
    });
}
