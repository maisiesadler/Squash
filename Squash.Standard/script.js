$('.menu li').on('click', function (a,b,c) {
    var loc = $(this).attr('loc');
    window.location = loc;
});