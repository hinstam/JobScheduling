//Div auto height when windows resize
window.onresize = function () {
    $("#divMidLeftSide").height($(window).height());
    setLayout();
    var isIE7 = $.browser.version == '7.0';
    if (isIE7) {
        $("#divMidLeftSide").height(1000);
    }
}
$(function () {
 
    //$(".content_gridview_row ul").attr("style", "width:" + $(".content_gridview_header ul").width()+"px");
    /*function bar*/
    $("#divContentFunction li").mousemove(function () {
        $(this).addClass("over");
        $(this).find(".back").addClass("backover");
        $(this).find(".new").addClass("addover");
        $(this).find(".del").addClass("delover");
        $(this).find(".edit").addClass("editover");
        $(this).find(".detail").addClass("detailover");
        $(this).find(".search").addClass("searchover");
        $(this).find(".selectuser").addClass("selectuserover");
        $(this).find("a").addClass("dark_color_orange");
        $(this).find(".next").addClass("nextover");
        $(this).find(".previous").addClass("previousover");
        $(this).find(".amount").addClass("amountover");
        $(this).find(".print").addClass("printover");
    }).mouseout(function () {
        $(this).removeClass("over");
        $(this).find(".back").removeClass("backover");
        $(this).find(".new").removeClass("addover");
        $(this).find(".del").removeClass("delover");
        $(this).find(".edit").removeClass("editover");
        $(this).find(".detail").removeClass("detailover");
        $(this).find(".search").removeClass("searchover");
        $(this).find(".selectuser").removeClass("selectuserover");
        $(this).find("a").removeClass("dark_color_orange");
        $(this).find(".next").removeClass("nextover");
        $(this).find(".previous").removeClass("previousover");
        $(this).find(".amount").removeClass("amountover");
        $(this).find(".print").removeClass("printover");
    });

    $(".content_gridview_row").click(function () {
        var isSelected = $(this).hasClass("Selected");
        if (isSelected) {
            $(this).removeClass("Selected");
            return false;
        }
        $(".content_gridview_row").each(function () {
            $(this).removeClass("Selected");
        });
        $(this).addClass("Selected");
    });

    $(".div_btnbar a").mousemove(function () {
        $(this).addClass("over");
    }).mouseout(function () {
        $(this).removeClass("over");
    });

    $(".div_closesearchbar").click(function () {
        $(".div_searchcontent").stop().animate({ height: "0px" }, 200, function () {
            $(this).hide();
            
            if ($("#projectFlag").val()) {
                $("#divSearchDetails").attr("style", "padding: 0;");//add by mark 2013-11-27
            } else {
                $("#divSearchDetails").attr("style", "padding: 10px 0;");
            }
        });
        $(".div_closesearchbar").hide();
    });

    $(".searchTool").click(function () {
        $(".div_searchcontent").show();
        $(".div_closesearchbar").show();
        $("#divSearchDetails").attr("style", "");
        $(".div_searchcontent").stop().animate({ height: "70px" }, 100, function () { });

        //$(this).attr("style", "padding: 10px 0 10px 20px;");

    });



    $(".div_datestart").mouseover(function () {
        $(this).find(".dateicon").addClass("over");

    }).mouseout(function () {
        $(this).find(".dateicon").removeClass("over");
    });

    $(".div_dateend").mouseover(function () {
        $(this).find(".dateicon").addClass("over");
    }).mouseout(function () {
        $(this).find(".dateicon").removeClass("over");
    });

    $(".searchicon").mouseover(function () {
        $(this).addClass("over");
    }).mouseout(function () {
        $(this).removeClass("over");
    });

    $(".div_rightset_mid .div_choosebar a").mouseover(function () {
        $(this).addClass("over");

    }).mouseout(function () {
        $(this).removeClass("over");
    });

    $(".a_dropiconarea").click(function (e) {
        $("#operateMenu").slideToggle();
        e.stopPropagation();
    });
    $(document).click(function () {
        $("#operateMenu").css("display", "none");
    });

    /*******************datepicker start***********************/
    $(".text_BeginDate").datepicker({
        dateFormat: 'yy/mm/dd',
        showAnim: "show",
        changeMonth: true,
        changeYear: true,
        firstDay: "1",
        onSelect: function (dateText, inst) {
            $(".text_EndDate").datepicker('option', 'minDate', new Date(dateText.replace('-', ',')));
        }
    });
    $(".text_EndDate").datepicker({
        dateFormat: 'yy/mm/dd',
        showAnim: "show",
        changeMonth: true,
        changeYear: true,
        firstDay: "1",
        onSelect: function (dateText, inst) {
            $(".text_BeginDate").datepicker('option', 'maxDate', new Date(dateText.replace('-', ',')));
        }
    });
    /*******************datepicker end*************************/

    setLayout();

    /************check for space (ID,Name,Code)***********/
    check_trim('ID');
    check_trim('UserID');
    check_trim('UserName');
    check_trim('GroupName');
    check_trim('Name');
    check_trim('Code');

    /************select first input at form***********/
    var first_input = $("form input:enabled:visible:not(.text_BeginDate,.text_EndDate)[type!=submit][type!=button]")[0];
    if (first_input != null) {
        first_input.focus();
    }
    /************select first input at form***********/

    //right corner username start
    if ($(".a_username") != null && $(".a_username") != undefined) {
        if ($(".a_username").text().length > 35) {
            $(".a_username").text($(".a_username").text().trim().substr(0, 30) + "...");
        } 
    }
    //right corner username end

    //group asseet start
    var max_width = 0;
    $(".div_grouprightsetedlist .row").each(function () {
        var width_groupright = 0;
        var ul = $(this).find("ul");
        ul.find("li:last span").each(function (index) {
            if($(this).width()<600)
                width_groupright += ($(this).width()+20);
        });
        if (width_groupright > max_width) {
            max_width = width_groupright;
        }
        ul.find("li:last").width(max_width+80);
        ul.width((max_width + 317));
        width_groupright = 0;
    });
    if ($(".div_grouprightsetedlist .row").width() < (max_width + 317)) {
        $(".div_grouprightsetedlist .row").width((max_width + 317));
    } else {
        $(".div_grouprightsetedlist .row ul").width(700);
        $(".div_grouprightsetedlist .row").find("li:last").width(463);
        $(".div_grouprightsetedlist").attr("style", "overflow-x:hidden;overflow-y:hidden;");
    }
    //group asseet end
 
    //add by mark 2013-11-27
    if ($("#projectFlag").val()) {
        $("#divSearchDetails").css("padding","0");
    }
   
});

function setLayout() {
    var width = ($(window).width() - 200);
    var height = $(window).height();
    //width = screen.width-200;
    //height = screen.height;
    $("#divMidRightContainer").attr("style",(width - 200)+"px");
    $("#divMidLeftSide").height(height);
    $("#divContentFunction").width($("#divContentHeader").width());

    //$(".content_gridview_header li").attr("style", "width:" + 100 / 8 + "%");
    //$(".content_gridview_row li").attr("style", "width:" + 100 / 8 + "%");
    //intialPageCss();

    $(".div_paging li").attr("style", "width:" + 100 / 8 + "%");

    $("#divMidRightContainer").attr("style", "width:" + width + "px;");
    $("#divContentHeader").attr("style", "width:" + width + "px;");
    $(".div_allfunction").attr("style", "width:" + width + "px;");

    $("#divContentFunction").attr("style", "width:" + width + "px;");
    $("#div_allfunction").attr("style", "width:" + width + "px;");

    ///////////////
    var headMinlength = $(".content_gridview_header").eq(0).attr("min");
    if(headMinlength==null || headMinlength<(width - 30))
        $(".content_gridview_header").attr("style", "width:" + (width - 30) + "px;");
     
    var contentMinlength = $(".content_gridview_row").eq(0).attr("min");
    if (contentMinlength == null || headMinlength < (width - 30))
        $(".content_gridview_row").attr("style", "width:" + (width - 30) + "px;");
 
    $(".div_paging").attr("style", "width:" + (width - 30) + "px;");
    //////////////

    $(".Welcome").height(height - 90);

    $(".content_gridview.GroupRight").attr("style", "width:" + width + "px;");
    $(".div_grouprightsetedlist").attr("style", "max-height:" + (height - 200) + "px;min-height:" + (height - 400) + "px");
    $(".div_grouplist.left").attr("style", "max-height:" + (height - 200) + "px;min-height:" + (height - 400) + "px");
    $(".div_rightsetlist").attr("style", "max-height:" + (height - 250) + "px;min-height:" + (height - 400) + "px");
    $(".div_rightset_mid").attr("style", "max-height:" + (height - 250) + "px;min-height:" + (height - 400) + "px");
    if ($("#divSearchDetails").html().length <= 38) {
        $("#divSearchDetails").attr("style", "display:none;");
    }

    //$('.content_infoview textarea[class=textarea_detail]').attr('disabled', 'disabled');
    $('.content_infoview textarea[class=textarea_detail]').attr('readonly', 'true');
    $('.content_infoview textarea[class=textarea_detail]').focus(function () {
        $(this).blur();
    });


    if ($.browser.msie && ($.browser.version == "7.0")) {
        $("#divMidRightContainer").attr("style", ($("#divMid").width() - 200) + "px");
        $("#divMidLeftSide").attr("style", "height:" + $("#divMid").height() + "px;");

        $(".div_grouprightsetedlist").attr("style", "max-height:" + (height - 300) + "px;min-height:" + (height - 400) + "px");
        $(".div_grouplist.left").attr("style", "max-height:" + (height - 300) + "px;min-height:" + (height - 400) + "px");
        if ($("#divSearchDetails").html().length <= 0) {
            $("#divSearchDetails").attr("style", "display:none;");
        }
    }

    


}

function renderGridview() {
    $(".content_gridview_header li").attr("style", "width:" + 100 / 8 + "%");
    $(".content_gridview_row li").attr("style", "width:" + 100 / 8 + "%");
    $(".div_paging li").attr("style", "width:" + 100 / 8 + "%");
};


/********************Left Side Menu**********************/
function showOrHideMenu(menuCode) {
    $('#' + menuCode + '_ul').toggle();
}


/**************check for space**************/
function check_trim(param) {
    $("[name=" + param + "]").keydown(function (e) {
        if (e.keyCode == 32) {
            var reg = /(^\s\w*)|(\w*\s$)/;
            if ($("#warnning").length > 0) {
                return;
            }

            if (reg.test($(this).val())) {
                var ele = $("<span style='color:#5D1476' id='warnning'>Don't allow Spaces before and after<span>");

                ele.insertAfter($(this));

                setTimeout(function () {
                    ele.fadeOut(500, function () { ele.remove(); });
                }, 1000);
            }
        }
    }).blur(function () {
        var reg = /(^\s\w*)|(\w*\s$)/;
        if ($("#warnning").length > 0) {
            return;
        }

        if (reg.test($(this).val())) {
            var ele = $("<span style='color:#5D1476' id='warnning'>Don't allow Spaces before and after<span>");

            ele.insertAfter($(this));

            setTimeout(function () {
                ele.fadeOut(500, function () { ele.remove(); });
            }, 1000);
        }
    });
}

/********************m97 datepicker********************/
function focusBeginTime(idEndTime,fmt) {
    var endTime = $dp.$(idEndTime);
    WdatePicker({
        onpicked: function () {
            endTime.focus();
        },
        dateFmt: !fmt? 'yyyy/MM/dd':fmt,
        maxDate: "#F{$dp.$D(\'" + idEndTime + "\')}"
    })
}

function focusEndTime(idBeginTime,fmt) {
    WdatePicker({
        dateFmt: !fmt ? 'yyyy/MM/dd' : fmt,
        minDate: "#F{$dp.$D(\'" + idBeginTime + "\')}"
    })
}

function limitTextareaRows(id) {
    var obj = document.getElementById(id);
    var o = obj.createTextRange().getClientRects();
    while (o.length > 6) {
        $("#" + id).val($("#" + id).val().substr(0, $("#" + id).val().length - 2));
        o = obj.createTextRange().getClientRects();
    }
    return o.length;
}

