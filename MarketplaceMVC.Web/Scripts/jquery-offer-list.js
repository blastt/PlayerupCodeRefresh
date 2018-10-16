function SelectPage(page) {

    $('#page').val(page);
    SearchOffersPage();
}

function SearchOffers() {

    var game = $('#game').val();
    var page = 1;
    var sort = $('#sort').val();
    var onlineOnly = $('#online-only').is(':checked');
    var searchInDiscription = $('#serch-in-description').is(':checked');
    var searchString = $('#search-string').val();
    var priceFrom = $('#price-from').val();
    var priceTo = $('#price-to').val();
    var personalAccount = $('#personal-account').val();
    var isBanned = $('#is-banned').val();



    var message = {
        "page": page,
        "sort": sort,
        "isOnline": isOnline,
        "searchInDiscription": searchInDiscription,
        "searchString": searchString,
        "jsonFilters": filters,
        "game": game,
        "priceFrom": priceFrom,
        "priceTo": priceTo
    };

    $.ajax({
        url: '/Offer/OfferSearch',
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ search: message }),
        dataType: "html",
        success: function (response) {
            var s = $('#sort').val();
            $('#list').html(response);

        }
    });
}