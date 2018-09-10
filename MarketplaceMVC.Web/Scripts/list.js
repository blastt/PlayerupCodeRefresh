function submitForm(v) {
    $('#sort').val(v.value);
    $('form#search-panel').submit();
    return false;
}