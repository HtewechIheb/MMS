var contactUtilities = {};

contactUtilities.getId = function (contact) {
    return contact.id;
}

contactUtilities.getNature = function (contact) {
    switch (contact.nature) {
        case 0: return "Naturelle";
            break;
        case 1: return "Legalle";
            break
        default: return "(Aucun)";
    }
}

contactUtilities.getContactType = function (contact) {
    switch (contact.contactType) {
        case 0: return "Interne";
            break;
        case 1: return "Externe";
            break;
        default: return "(Aucun)";
    }
}

contactUtilities.getName = function (contact) {
    if (![null, undefined].includes(contact.name)) {
        return contact.name;
    }
    else {
        return "(Aucun)";
    }
}

contactUtilities.getEmail1 = function (contact) {
    if (![null, undefined].includes(contact.email1)) {
        return contact.email1;
    }
    else {
        return "(Aucun)";
    }
}

contactUtilities.getEmail2 = function (contact) {
    if (![null, undefined].includes(contact.email2)) {
        return contact.email2;
    }
    else {
        return "(Aucun)";
    }
}

contactUtilities.getAddress = function (contact) {
    if (![null, undefined].includes(contact.address)) {
        return contact.address;
    }
    else {
        return "(Aucun)";
    }
}

contactUtilities.getTelephone1 = function (contact) {
    if (![null, undefined].includes(contact.telephone1)) {
        return contact.telephone1;
    }
    else {
        return "(Aucun)";
    }
}

contactUtilities.getTelephone2 = function (contact) {
    if (![null, undefined].includes(contact.telephone2)) {
        return contact.telephone2;
    }
    else {
        return "(Aucun)";
    }
}

contactUtilities.getFax = function (contact) {
    if (![null, undefined].includes(contact.fax)) {
        return contact.fax;
    }
    else {
        return "(Aucun)";
    }
}

contactUtilities.getEmail = function (contact) {
    if (![null, undefined].includes(contact.email1)) {
        return contact.email1;
    }
    else if (![null, undefined].includes(contact.email2)) {
        return contact.email2;
    }
    else {
        return "(Aucun)";
    }
}

contactUtilities.getTelephone = function (contact) {
    if (![null, undefined].includes(contact.telephone1)) {
        return contact.telephone1;
    }
    else if (![null, undefined].includes(contact.telephone2)) {
        return contact.telephone2;
    }
    else {
        return "(Aucun)";
    }
}