$(function () {
    //initial Pagecss
    intialPageCss();

    $(".reset").click(btnReset_onclick);
    $(".close").click(function () {
        $(".div_closesearchbar").trigger("click");
    });

    $("#divSearchDetails .div_searchcontent,.div_closesearchbar").css("display", "none");

});

function btnSearch_onclick(pageIndex) {
    $("#PageSize").val($("#pSize option:selected").val());
    if (pageIndex == null) {
        $("#PageIndex").val(1);
    } else {
        $("#PageIndex").val(pageIndex);
    }
    var postData = $("form").eq(0).serialize();
    var url = $("form").eq(0).attr("action");
    $("#processingBackground").toggle();
    $("#processing").toggle();
    $.post(url, postData, function (data) {
        $(".content_gridview").html(data);
        $("#TotalPages").val($("#tPages").val());//reset totalpages
        initialPage();
        $("#processing").toggle();
        $("#processingBackground").toggle();
    });
}

function NextPage() {
    var pageNumber = Number($("#PageIndex").val());
    var TotalPages = Number($("#TotalPages").val());
    if ((pageNumber + 1) <= TotalPages) {
        btnSearch_onclick(pageNumber + 1);
    }
}


function BackPage() {
    var pageNumber = Number($("#PageIndex").val());
    var TotalPages = Number($("#TotalPages").val());
    if ((pageNumber - 1) >= 1) {
        btnSearch_onclick(pageNumber - 1);
    }
}


function GotoPage() {
    var pageNumber = Number($("#GotoPage").val());
    var TotalPages = Number($("#TotalPages").val());
    if (pageNumber >= 1 && pageNumber <= TotalPages) {
        btnSearch_onclick(pageNumber);
    }
    else {
        alert("Please input the page number between 1 and " + TotalPages.toString());
    }
}


function EnterPress(e) {
    var e = e || window.event;
    if (e.keyCode == 13) {
        GotoPage();
    }
}


function btnReset_onclick() {
    $(".div_searchcontent input").val("");
}

function Gridview_li_Css(index, width, textalign, minwidth,totalMinWidth) {
    $(".content_gridview_header").each(function () {
        $(this).find("ul li").eq(index).attr("style", "width:" + width + "%");
        if (minwidth) {
            $(this).find("ul li").eq(index).css("min-width", minwidth);
        }
    });
    //$(".content_gridview_header li").eq(index).attr("style", "width:" + width + "%");

    $.each($(".content_gridview_row"), function (n, m) {       
        $(m).find("ul li").eq(index).attr("style", "width:" + width + "%;text-align:" + textalign);
        if (minwidth) {
            $(m).find("ul li").eq(index).css("min-width", minwidth);
        }

        
    });

    if (totalMinWidth) {
        $(".content_gridview_row").attr("min", totalMinWidth);
        $(".content_gridview_row ul").attr("min", totalMinWidth);
        $(".content_gridview_header").attr("min", totalMinWidth);
        $(".content_gridview_header ul").attr("min", totalMinWidth);
    }
}

function Gridview_li_Css_Ex(index, width, textalign, minwidth, funcHeader, funcRow, wrapEleWidth) {

    $(".content_gridview_header").each(function () {
        $(this).find("ul li").eq(index).attr("style", "width:" + width + "%");
        if (minwidth) {
            $(this).find("ul li").eq(index).css("min-width", minwidth);
        }
        (funcHeader) && funcHeader($(this).find("ul li").eq(index)[0], wrapEleWidth);
    });
    //$(".content_gridview_header li").eq(index).attr("style", "width:" + width + "%");

    $.each($(".content_gridview_row"), function (n, m) {
        $(m).find("ul li").eq(index).attr("style", "width:" + width + "%;text-align:" + textalign);
        if (minwidth) {
            $(m).find("ul li").eq(index).css("min-width", minwidth);
        }
        (funcRow) && funcRow($(m).find("ul li").eq(index)[0]);
    });
}
/**********************JamseChen 2013/11/11***************************************************/
function totalcost_li_Css(index, width) {
    //$.each($(".totalcost_header"), function (n, m) {
    //    $(this).eq(index).attr("style", "width:" + width + "%");
    //});
    $(".totalcost_header").each(function () {
        $(this).find("ul li").eq(index).attr("style", "width:" + width + "%;line-height:30px;");
        
    })
    //$(".totalcost_header li").eq(index).attr("style", "width:" + width + "%");

    $.each($(".totalcost_content"), function (n, m) {
        $(m).find("ul li").eq(index).attr("style", "width:" + width + "%;");
        $(this).unbind();
    });
}
function totalcost_li_Css(index, width, textalign) {
    $(".totalcost_header").each(function () {
        if (index == 0) {
            $(this).find("ul li").eq(index).attr("style", "width:" + width + "%;display:none;line-height:30px;");
        }
        else {
            $(this).find("ul li").eq(index).attr("style", "width:" + width + "%;line-height:30px;");
        } 
    })
    //$(".totalcost_header li").eq(index).attr("style", "width:" + width + "%");

    $.each($(".totalcost_content"), function (n, m) {
        if (index == 0) {
            $(m).find("ul li").eq(index).attr("style", "width:" + width + "%;display:none;text-align:" + textalign);
        }
        else {
            $(m).find("ul li").eq(index).attr("style", "width:" + width + "%;text-align:" + textalign);
        }
        //$(m).find("ul li").eq(index).attr("style", "width:" + width + "%;text-align:" + textalign);
        $(this).unbind();
    });
}
/***********************JamseChen 2013/11/11********************************/
function ddlSize_onchange(id) {
    $("#PageIndex").val(1);
    btnSearch_onclick();
}

//initial page list
function initialPage() {
    //var width = ($(window).width() - 200);
    //////////////
    //intialPageCss();
    //$(".div_paging li").attr("style", "width:" + 100 / 8 + "%");
    
    //$(".content_gridview_header").attr("style", "width:" + width + "px;");
    //$(".content_gridview_row").attr("style", "width:" + width + "px;");
    //$(".div_paging").attr("style", "width:" + width + "px;");
    
    
    ///////////

    setLayout();
    intialPageCss();


    //selected to change css
    $(".content_gridview_row").click(function () {
        var isSelected = $(this).hasClass("Selected");
        if (isSelected) {
            $(this).removeClass("Selected");
            return false;
        }
        $(".content_gridview_row").each(function () {
            $(this).removeClass("Selected");
        });
        $(this).addClass("Selected")
    })

    //selected to save current row
    $(".content_gridview_row ul").click(function () {
        if (window.current == undefined || window.current.html() != $(this).html())
            window.current = $(this);
        else
            window.current = undefined;
    });
};



