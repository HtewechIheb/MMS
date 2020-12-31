$("#ShowMail_MailType").text(mailUtilities.getMailType(shownMail));
$("#ShowMail_Channel").text(mailUtilities.getChannel(shownMail));
$("#ShowMail_RegistrationNumber").text(mailUtilities.getRegistrationNumber(shownMail));
$("#ShowMail_RegistrationDate").text(mailUtilities.getDate(shownMail.registrationDate));
$("#ShowMail_IdSender").text(contactUtilities.getName(shownMailSender));
$("#ShowMail_IdRecipient").text(contactUtilities.getName(shownMailRecipient));
$("#ShowMail_Object").text(mailUtilities.getObject(shownMail));
$("#ShowMail_Confidentiality").text(mailUtilities.getBoolean(shownMail.confidentiality));
$("#ShowMail_HasHardCopy").text(mailUtilities.getBoolean(shownMail.hasHardCopy));
$("#ShowMail_SendingDate").text(mailUtilities.getDate(shownMail.sendingDate));
$("#ShowMail_SenderRegistrationNumber").text(mailUtilities.getSenderRegistrationNumber(shownMail));
$("#ShowMail_ProcessingTimeFrame").text(mailUtilities.getProcessingTimeFrame(shownMail));
$("#ShowMail_Language").text(mailUtilities.getLanguage(shownMail));
$("#ShowMail_KeyWords").text(mailUtilities.getKeyWords(shownMail));
if (![null, undefined].includes(shownMailFolder)) {
    $("#ShowMail_IdFolder").text(folderUtilities.getName(shownMailFolder));
}
else {
    $("#ShowMail_IdFolder").text("(Aucun)");
}
$("#ShowMail_Observations").text(mailUtilities.getObservations(shownMail));
