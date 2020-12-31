var folderUtilities = {};

folderUtilities.getId = function (folder) {
    return folder.id
}

folderUtilities.getName = function (folder) {
    if (![null, undefined].includes(folder.name)) {
        return folder.name
    }
    else {
        return "(Aucun)";
    }
}

folderUtilities.getDescription = function (folder) {
    if (![null, undefined].includes(folder.description)) {
        return folder.description;
    }
    else {
        return "(Aucun)";
    }
}