var mailUtilities = {};
mailUtilities.getId = function (mail) {
    return mail.id;
}

mailUtilities.getMailType = function (mail) {
    switch (mail.mailType) {
        case 0: return "واردة";
            break;
        case 1: return "صادرة";
            break;
        case 2: return "داخلية";
            break;
        default: return "(لا يوجد)";
    }
}

mailUtilities.getChannel = function (mail) {
    switch (mail.channel) {
        case 0: return "وثيقة";
            break;
        case 1: return "بريد إلكتروني";
            break;
        case 2: return "طرد بريدي";
            break;
        default: return "(لا يوجد)";
    }
}

mailUtilities.getRegistrationNumber = function (mail) {
    if (![null, undefined].includes(mail.registrationNumber)) {
        return mail.registrationNumber;
    }
    else {
        return "(لا يوجد)";
    }
}

mailUtilities.getObject = function (mail) {
    if (![null, undefined].includes(mail.object)) {
        return mail.object;
    }
    else {
        return "(لا يوجد)";
    }
}

mailUtilities.getSenderRegistrationNumber = function (mail) {
    if (![null, undefined].includes(mail.senderRegistrationNumber)) {
        return mail.senderRegistrationNumber;
    }
    else {
        return "(لا يوجد)";
    }
}

mailUtilities.getKeyWords = function (mail) {
    if (![null, undefined].includes(mail.keyWords)) {
        return mail.keyWords;
    }
    else {
        return "(لا يوجد)";
    }
}

mailUtilities.getObservations = function (mail) {
    if (![null, undefined].includes(mail.observations)) {
        return mail.observations;
    }
    else {
        return "(لا يوجد)";
    }
}

mailUtilities.getSender = function (mail) {
    return contactsArray.find(contact => contact.id == mail.idSender).name;
}

mailUtilities.getRecipient = function (mail) {
    return contactsArray.find(contact => contact.id == mail.idRecipient).name;
}

mailUtilities.getConfidentiality = function (mail) {
    switch (mail.confidentiality) {
        case 0: return "(لا يوجد)";
            break;
        case 1: return "عادي";
            break;
        case 2: return "سري";
            break;
        default: return "(لا يوجد)";
    }
}

mailUtilities.getLanguage = function (mail) {
    switch (mail.language) {
        case 0: return "(بدون)";
            break;
        case 1: return "العربية";
            break;
        case 2: return "الفرنسية";
            break;
        case 3: return "الإنجليزية";
            break;
        case 4: return "الألمانية";
            break;
        case 5: return "الإيطالية";
            break;
        case 6: return "الإسبانية";
            break;
        default: return "(لا يوجد)";
    }
}

mailUtilities.getProcessingTimeFrame = function (mail) {
    switch (mail.processingTimeFrame) {
        case 0: return "(لا يوجد)";
            break;
        case 1: return "عادي";
            break;
        case 2: return "عاجل";
            break;
        default: return "(لا يوجد)";
    }
}

mailUtilities.getBoolean = function (value) {
    if (![null, undefined].includes(value)) {
        switch (value.toString().toLowerCase()) {
            case "true": return "نعم";
                break;
            case "false": return "لا";
                break;
            default: "(لا يوجد)";
        }
    }
    return "(لا يوجد)";
}

mailUtilities.getDate = function (date) {
    if (![null, undefined].includes(date)) {
        return moment(date).format("DD/MM/YYYY");
    }
    return "(لا يوجد)";
}