function buildFoldersMenu(data, container, nodeClass) {
    $(`#${container} .${nodeClass}`).remove();
    if (data.length == 0) {
        $(`#${container} .submenu`).append(`
            <li class="submenu-action">
                <a href="#">
                    <span class="menu-text">Aucun dossier trouvé.</span>
                </a>
            </li>
        `);
    }
    else {
        for (let unit of data) {
            $(`#${container} .submenu`).append(`
                <li class="submenu-action ${nodeClass}" data-folder-id="${folderUtilities.getId(unit)}" data-folder-name="${folderUtilities.getName(unit)}">
                    <a href="/Fr/Folders/${folderUtilities.getId(unit)}">
                        <i class="submenu-action-icon folder-orange fas fa-folder"></i>
                        <span class="menu-text">${folderUtilities.getName(unit)}</span>
                    </a>
                </li>
            `);
        }
    }

    if (currentPage === pages.folderShow) {
        $(`.${nodeClass}[data-folder-id=${folderUtilities.getId(shownFolder)}]`).addClass("active");
    }
}

function loadFolders(container, nodeClass) {
    return $.ajax({
        url: `/API/Folders/All`,
        method: "GET",
        dataType: "json",
    }).then(function (response) {
        buildFoldersMenu(response, container, nodeClass);
        sidebarFoldersArray = response;
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('Erreur', "Erreur lors de chargement des dossiers.", 3000);
        console.log(response);
    }).always(function () {
        $(`#${container} .processing-icon`).addClass("hidden");
    });
}

function bindFolderLoading(container, nodeClass) {
    $(`#${container}`).on("touchstart", "a.dropdown-toggle", function () {
        if ($(`#${container}`).hasClass("hover-shown")) {
            return;
        }
        if ($(`#${container}`).hasClass("open")) {
            return;
        }
        $(`#${container} .processing-icon`).removeClass("hidden");
        loadFolders(container, nodeClass);
    });

    $(`#${container}`).on("mouseover", "a.dropdown-toggle", function () {
        if (!$("#sidebar").hasClass("menu-min")) {
            return;
        }
        if ($(`#${container}`).hasClass("hover-shown")) {
            return;
        }
        $(`#${container} .processing-icon`).removeClass("hidden");
        loadFolders(container, nodeClass);
    });

    $(`#${container}`).on("click", "a.dropdown-toggle", function () {
        if ($("#sidebar").hasClass("menu-min")) {
            return;
        }
        if ($(`#${container}`).hasClass("open")) {
            return;
        }
        $(`#${container} .processing-icon`).removeClass("hidden");
        loadFolders(container, nodeClass);
    });
}

function bindFolderSearching(input, nodeClass) {
    $(`#${input}`).on("keyup", function () {
        let inputValue = $(this).val();

        if (inputValue === "") {
            $(`.${nodeClass}`).each(function () {
                $(this).removeClass("hidden");
            });
            return;
        }

        $(`.${nodeClass}`).each(function () {
            if ($(this).attr("data-folder-name").toLowerCase().includes(inputValue.toLowerCase())) {
                $(this).removeClass("hidden");
            }
            else {
                $(this).addClass("hidden");
            }
        });
    });
}


