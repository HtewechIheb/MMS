var sidebarFoldersArray;

//Bind Sidebar Loading Event
bindFolderLoading("folders-menu-item", "folder-node");

//Bind Sidebar Searching Event
bindFolderSearching("submenu-folder-search", "folder-node");

//Build Sibar Menu
buildFoldersMenu(foldersArray, "folders-menu-item", "folder-node");