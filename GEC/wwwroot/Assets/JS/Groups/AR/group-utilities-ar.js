var groupUtilities = {};
groupUtilities.getId = function (group) {
    return group.id
}

groupUtilities.getName = function (group) {
    if (![null, undefined].includes(group.name)) {
        return group.name;
    }
    else {
        return "(لا يوجد)";
    }
}

groupUtilities.getDescription = function (group) {
    if (![null, undefined].includes(group.description)) {
        return group.description;
    }
    else {
        return "(لا يوجد)";
    }
}