var folderUtilities = {};

folderUtilities.getId = function (folder) {
    return folder.id
}

folderUtilities.getName = function (folder) {
    if (![null, undefined].includes(folder.name)) {
        return folder.name
    }
    else {
        return "(لا يوجد)";
    }
}

folderUtilities.getDescription = function (folder) {
    if (![null, undefined].includes(folder.description)) {
        return folder.description;
    }
    else {
        return "(لا يوجد)";
    }
}