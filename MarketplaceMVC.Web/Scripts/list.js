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