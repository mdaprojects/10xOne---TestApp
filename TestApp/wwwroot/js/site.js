function confirmDelete(isDeleteClicked) {
    if (isDeleteClicked) {
        $("#deleteSpan").hide();
        $("#confirmDeleteSpan").show();
    } else {
        $("#deleteSpan").show();
        $("#confirmDeleteSpan").hide();
    }
}
