
$(function () {

  
    $('#btnIngresar').hover(function () {
    
        $('.button ').addClass('animacionVer');
    })

    $('#contenido').mouseleave(function () {
        $('.button').removeClass('animacionVer');
    })


});
