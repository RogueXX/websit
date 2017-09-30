

$(document).ready(function(){
    $('.dbl_tms_module_wrap .list li').hover(function() {
        $(this).find('p.text').stop().animate({paddingBottom: '10%'}, 500);
    }, function() {
        $(this).find('p.text').stop().animate({paddingBottom: 0}, 500);
    });

    // $(window).scrollTop() > 0 ? $('#header').find('.module-wrap').removeClass("bg-transparent") : $('#header').find('.module-wrap').addClass("bg-transparent");

    // $('#switch-btn').click(function(event) {
    //     $(this).toggleClass('.open');
    //     // if($(this).hasClass('open')){

    //     // }
    //      $('#theme-list').slideToggle(100);
        
    // });

    $('#detail1101').show();
    //议程
    $('.day-choice ul').on('click', 'li', function() {
        $(this).addClass('cur').siblings().removeClass('cur');
        var ty = $(this).attr('data-day')
        if(ty =='day1'){
            $('#meeting').find('[data-id="first"]').show();
            $('#meeting').find('[data-id="first"]').find('.detail').eq(0).show();
            $('#meeting').find('[data-id="next"]').hide();
        }else if(ty =='day2'){
            $('#meeting').find('[data-id="next"]').show();
            $('#meeting').find('[data-id="first"]').hide();
            $('#meeting').find('[data-id="next"]').find('.detail').eq(0).show();
        }else{
            $('#meeting').find('[data-id="first"]').show();
            $('#meeting').find('[data-id="first"]').find('.detail').eq(0).show();
            $('#meeting').find('[data-id="next"]').hide();
        }
    });
    $('.meeting-list .list').on('click', 'p', function() {
        $(this).addClass('cur').siblings().removeClass('cur');
        var current = $(this).attr('data-meeting');
        var selitem = $('.meeting-detail').find('.detail');
        for(var i=0;i<selitem.length;i++){
            if(current == $(selitem[i]).attr('id')){
                $(selitem[i]).show();
            }else{
                $(selitem[i]).hide();
            }

        }
        

    });
});