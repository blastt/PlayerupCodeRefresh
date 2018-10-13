function submitForm(v) {
    $('#sort').val(v.value);
    $('form#search-panel').submit();
    return false;
}



function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}



//$(document).ready(function () {
//    $('#dropdownMenuButton').mouseenter(function () {
//        myFunction();
//    });
//    window.onclick = function (event) {
//        if (!event.target.matches('.dropbtn')) {

//            var dropdowns = document.getElementsByClassName("dropdown-content");
//            var i;
//            for (i = 0; i < dropdowns.length; i++) {
//                var openDropdown = dropdowns[i];
//                if (openDropdown.classList.contains('show')) {
//                    openDropdown.classList.remove('show');
//                }
//            }
//        }
//    }
//    $('#myDropdown').children().each(function () {
//        $(this).mouseout(function () {
//            $(this).find('.game-menu').removeClass("show");

//        });
//    });
//    $('#myDropdown').children().each(function () {
//        $(this).mouseenter(function () {
//            $(this).find('.game-menu').addClass("show");

//        });
//    });

    
//});

$(document).ready(function () {
    $("#nav li").hover(
        function () {
            $(this).children('ul').hide();
            $(this).children('ul').slideDown('fast');
        },
        function () {
            $('ul', this).slideUp('fast');
        });
});

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