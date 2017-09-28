

$(document).ready(function(){
    $('.dbl_tms_module_wrap .list li').hover(function() {
        $(this).find('p.text').stop().animate({paddingBottom: '10%'}, 500);
    }, function() {
        $(this).find('p.text').stop().animate({paddingBottom: 0}, 500);
    });

    // $(window).scrollTop() > 0 ? $('#header').find('.module-wrap').removeClass("bg-transparent") : $('#header').find('.module-wrap').addClass("bg-transparent");

    $('#switch-btn').click(function(event) {
        $(this).toggleClass('.open');
        // if($(this).hasClass('open')){

        // }
         $('#theme-list').slideToggle(100);
        
    });
       
});