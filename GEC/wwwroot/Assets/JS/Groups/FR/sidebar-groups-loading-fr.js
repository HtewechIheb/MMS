function buildGroupsMenu(data, container, nodeClass) {
    $(`#${container} .${nodeClass}`).remove();
    if (data.length == 0) {
        $(`#${container} .submenu`).append(`
            <li class="submenu-action">
                <a href="#">
                    <span class="menu-text">Aucun groupe trouvé.</span>
                </a>
            </li>
        `);
    }
    else {
        for (let unit of data) {
            $(`#${container} .submenu`).append(`
                <li class="submenu-action ${nodeClass}" data-group-id="${groupUtilities.getId(unit)}" data-group-name="${groupUtilities.getName(unit)}">
                    <a href="/Fr/Groups/${groupUtilities.getId(unit)}">
                        <i class="submenu-action-icon folder-orange fas fa-users"></i>
                        <span class="menu-text">${groupUtilities.getName(unit)}</span>
                    </a>
                </li>
            `);
        }
    }

    if (currentPage === pages.groupShow) {
        $(`.${nodeClass}[data-group-id=${groupUtilities.getId(shownGroup)}]`).addClass("active");
    }
}

function loadGroups(container, nodeClass) {
    return $.ajax({
        url: `/API/Groups/All`,
        method: "GET",
        dataType: "json",
    }).then(function (response) {
        buildGroupsMenu(response, container, nodeClass);
        sidebarGroupsArray = response;
        console.log(response);
        return response;
    }).fail(function (response) {
        errorNotification('Erreur', "Erreur lors de chargement des groupes.", 3000);
        console.log(response);
    }).always(function () {
        $(`#${container} .processing-icon`).addClass("hidden");
    });
}

function bindGroupsLoading(container, nodeClass) {
    $(`#${container}`).on("touchstart", "a.dropdown-toggle", function () {
        if ($(`#${container}`).hasClass("hover-shown")) {
            return;
        }
        if ($(`#${container}`).hasClass("open")) {
            return;
        }
        $(`#${container} .processing-icon`).removeClass("hidden");
        loadGroups(container, nodeClass);
    });

    $(`#${container}`).on("mouseover", "a.dropdown-toggle", function () {
        if (!$("#sidebar").hasClass("menu-min")) {
            return;
        }
        if ($(`#${container}`).hasClass("hover-shown")) {
            return;
        }
        $(`#${container} .processing-icon`).removeClass("hidden");
        loadGroups(container, nodeClass);
    });

    $(`#${container}`).on("click", "a.dropdown-toggle", function () {
        if ($("#sidebar").hasClass("menu-min")) {
            return;
        }
        if ($(`#${container}`).hasClass("open")) {
            return;
        }
        $(`#${container} .processing-icon`).removeClass("hidden");
        loadGroups(container, nodeClass);
    });
}

function bindGroupSearching(input, nodeClass) {
    $(`#${input}`).on("keyup", function () {
        let inputValue = $(this).val();

        if (inputValue === "") {
            $(`.${nodeClass}`).each(function () {
                $(this).removeClass("hidden");
            });
            return;
        }

        $(`.${nodeClass}`).each(function () {
            if ($(this).attr("data-group-name").toLowerCase().includes(inputValue.toLowerCase())) {
                $(this).removeClass("hidden");
            }
            else {
                $(this).addClass("hidden");
            }
        });
    });
}