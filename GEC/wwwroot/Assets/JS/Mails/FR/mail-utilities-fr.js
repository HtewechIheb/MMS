var mailUtilities = {};
mailUtilities.getId = function (mail) {
    return mail.id;
}

mailUtilities.getMailType = function (mail) {
    switch (mail.mailType) {
        case 0: return "Entrant";
            break;
        case 1: return "Sortant";
            break;
        case 2: return "Interne";
            break;
        default: return "(Aucun)";
    }
}

mailUtilities.getChannel = function (mail) {
    switch (mail.channel) {
        case 0: return "Dur";
            break;
        case 1: return "Email";
            break;
        case 2: return "Colis Postal";
            break;
        default: return "(Aucun)";
    }
}

mailUtilities.getRegistrationNumber = function (mail) {
    if (![null, undefined].includes(mail.registrationNumber)) {
        return mail.registrationNumber;
    }
    else {
        return "(Aucun)";
    }
}

mailUtilities.getObject = function (mail) {
    if (![null, undefined].includes(mail.object)) {
        return mail.object;
    }
    else {
        return "(Aucun)";
    }
}

mailUtilities.getSenderRegistrationNumber = function (mail) {
    if (![null, undefined].includes(mail.senderRegistrationNumber)) {
        return mail.senderRegistrationNumber;
    }
    else {
        return "(Aucun)";
    }
}

mailUtilities.getKeyWords = function (mail) {
    if (![null, undefined].includes(mail.keyWords)) {
        return mail.keyWords;
    }
    else {
        return "(Aucun)";
    }
}

mailUtilities.getObservations = function (mail) {
    if (![null, undefined].includes(mail.observations)) {
        return mail.observations;
    }
    else {
        return "(Aucun)";
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
        case 0: return "(Aucun)";
            break;
        case 1: return "Normal";
            break;
        case 2: return "Confidentiel";
            break;
        default: return "(Aucun)";
    }
}

mailUtilities.getLanguage = function (mail) {
    switch (mail.language) {
        case 0: return "(Aucun)";
            break;
        case 1: return "Arabe";
            break;
        case 2: return "Français";
            break;
        case 3: return "Englais";
            break;
        case 4: return "Allemand";
            break;
        case 5: return "Italien";
            break;
        case 6: return "Espagñol";
            break;
        default: return "(Aucun)";
    }
}

mailUtilities.getProcessingTimeFrame = function (mail) {
    switch (mail.processingTimeFrame) {
        case 0: return "(Aucun)";
            break;
        case 1: return "Normal";
            break;
        case 2: return "Urgent";
            break;
        default: return "(Aucun)";
    }
}

mailUtilities.getBoolean = function (value) {
    if (![null, undefined].includes(value)) {
        switch (value.toString().toLowerCase()) {
            case "true": return "Oui";
                break;
            case "false": return "Non";
                break;
            default: "(Aucun)";
        }
    }
    return "(Aucun)";
}

mailUtilities.getDate = function (date) {
    if (![null, undefined].includes(date)) {
        return moment(date).format("DD/MM/YYYY");
    }
    return "(Aucun)";
}