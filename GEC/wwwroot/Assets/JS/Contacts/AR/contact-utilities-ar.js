var contactUtilities = {};

contactUtilities.getId = function (contact) {
    return contact.id;
}

contactUtilities.getNature = function (contact) {
    switch (contact.nature) {
        case 0: return "شخص";
            break;
        case 1: return "مؤسسة";
            break
        default: return "(لا يوجد)";
    }
}

contactUtilities.getContactType = function (contact) {
    switch (contact.contactType) {
        case 0: return "داخلي";
            break;
        case 1: return "خارجي";
            break;
        default: return "(لا يوجد)";
    }
}

contactUtilities.getName = function (contact) {
    if (![null, undefined].includes(contact.name)) {
        return contact.name;
    }
    else {
        return "(لا يوجد)";
    }
}

contactUtilities.getEmail1 = function (contact) {
    if (![null, undefined].includes(contact.email1)) {
        return contact.email1;
    }
    else {
        return "(لا يوجد)";
    }
}

contactUtilities.getEmail2 = function (contact) {
    if (![null, undefined].includes(contact.email2)) {
        return contact.email2;
    }
    else {
        return "(لا يوجد)";
    }
}

contactUtilities.getAddress = function (contact) {
    if (![null, undefined].includes(contact.address)) {
        return contact.address;
    }
    else {
        return "(لا يوجد)";
    }
}

contactUtilities.getTelephone1 = function (contact) {
    if (![null, undefined].includes(contact.telephone1)) {
        return contact.telephone1;
    }
    else {
        return "(لا يوجد)";
    }
}

contactUtilities.getTelephone2 = function (contact) {
    if (![null, undefined].includes(contact.telephone2)) {
        return contact.telephone2;
    }
    else {
        return "(لا يوجد)";
    }
}

contactUtilities.getFax = function (contact) {
    if (![null, undefined].includes(contact.fax)) {
        return contact.fax;
    }
    else {
        return "(لا يوجد)";
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
        return "(لا يوجد)";
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
        return "(لا يوجد)";
    }
}