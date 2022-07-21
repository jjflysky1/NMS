//$(function () {
//    $(".dropdown").hover(
//        function () {
//            $('.dropdown-menu', this).stop(true, true).fadeIn("fast");
//            $(this).toggleClass('open');

//        },
//        function () {
//            $('.dropdown-menu', this).stop(true, true).fadeOut("fast");
//            $(this).toggleClass('open');

//        });
//});

$(document).ready(function () {
    // add button style 
    $("[name='poll_bar'").addClass("btn btn-default");
    // Add button style with alignment to left with margin.
    $("[name='poll_bar'").css({ "text-align": "left", "margin": "5px" });

    // add button style 
    $("[name='poll_bar1'").addClass("btn btn-default");
    // Add button style with alignment to left with margin.
    $("[name='poll_bar1'").css({ "text-align": "left", "margin": "5px, 5px, 5px" });

    //loop 
    $("[name='poll_bar'").each(
        function (i) {
            //get poll value 	
            var bar_width = (parseFloat($("[name='poll_val'").eq(i).text()) / 2).toString();
            bar_width = bar_width + "%"; //add percentage sign.										
            //set bar button width as per poll value mention in span.
            $("[name='poll_bar'").eq(i).width(bar_width);

            //Define rules.
            var bar_width_rule = parseFloat($("[name='poll_val'").eq(i).text());
            if (bar_width_rule >= 90) { $("[name='poll_bar'").eq(i).addClass("btn btn-sm btn-danger") }
            if (bar_width_rule > 70) { $("[name='poll_bar'").eq(i).addClass("btn btn-sm btn-warning") }
            if (bar_width_rule <= 70) { $("[name='poll_bar'").eq(i).addClass("btn btn-sm btn-success") }

            //Hide dril down divs
            //$("#" + $("[name='poll_bar'").eq(i).text()).hide
        });

    $("[name='poll_bar1'").each(
        function (i) {
            //get poll value 	
            var bar_width1 = (parseFloat($("[name='poll_val1'").eq(i).text()) / 100).toString();
            bar_width1 = bar_width1 + "%"; //add percentage sign.										
            //set bar button width as per poll value mention in span.
            $("[name='poll_bar1'").eq(i).width(bar_width1);

            //Define rules.
            var bar_width_rule1 = parseFloat($("[name='poll_val1'").eq(i).text());
            if (bar_width_rule1 >= 10000) { $("[name='poll_bar1'").eq(i).addClass("btn btn-sm btn-danger") }
            if (bar_width_rule1 > 5000) { $("[name='poll_bar1'").eq(i).addClass("btn btn-sm btn-warning") }
            if (bar_width_rule1 <= 5000) { $("[name='poll_bar1'").eq(i).addClass("btn btn-sm btn-success") }

            //Hide dril down divs
            //$("#" + $("[name='poll_bar1'").eq(i).text()).hide


        });
});


//; (function ($) {
//    $.fn.datepicker.dates['kr'] = {
//        days: ["일요일", "월요일", "화요일", "수요일", "목요일", "금요일", "토요일", "일요일"],
//        daysShort: ["일", "월", "화", "수", "목", "금", "토", "일"],
//        daysMin: ["일", "월", "화", "수", "목", "금", "토", "일"],
//        months: ["1월", "2월", "3월", "4월", "5월", "6월", "7월", "8월", "9월", "10월", "11월", "12월"],
//        monthsShort: ["1월", "2월", "3월", "4월", "5월", "6월", "7월", "8월", "9월", "10월", "11월", "12월"]
//    };
//}(jQuery));


