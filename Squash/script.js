$('.menu li').on('click', function (a,b,c) {
   // var name = this.innerText;
   // var currentpg = window.location.href.split("output")[0] + "output/";
   // var file = currentpg + name + ".html";

    var loc = $(this).attr('loc');

    window.location = loc;
});