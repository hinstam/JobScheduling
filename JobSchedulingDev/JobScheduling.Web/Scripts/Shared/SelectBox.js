/*************************  根据ArtisteSelection.js将弹出选择控件做成公用***********************/
/****************************** jasonyiu 2013-11-14 start *********************************/
/******************************** 选择公共 start *************************************/

window.onresize = function () {
    setArtistChoosePancel();
}

$(document).ready(function () {
    setArtistChoosePancel();
});


function setArtistChoosePancel() {
    var height = ($(window).height() - $("#div_artise_choose_pancel").height()) / 2;
    if (height < 0) {
        height = 20;
    }
    $("#div_artise_choose_pancel,").css({
        position: 'absolute',
        left: ($(window).width() - $("#div_artise_choose_pancel").width()) / 2,
        top: height
    });

    $("#div_artise_choose_pancel1").css({
        position: 'absolute',
        left: ($(window).width() - $("#div_artise_choose_pancel1").width()) / 2,
        top: height
    });
}


function DivClose() {
    $("#div_mask_layout").removeClass("show");
    $("#div_artise_choose_pancel").fadeOut();
    $("#div_artise_choose_pancel1").fadeOut();
    $("#OKButton").unbind("click");
    $("#OKButton2").unbind("click");
}
/************************  选择公共 end **********************************/

/***********************************复选 start *************************************/

//全选 checkbox
function onChangeChecked(checkbox) {
    var ele_checkbox = $("#" + checkbox.id);
    if (ele_checkbox.is(":checked")) {
        $(".NotbeSelectedArtistList input[type=checkbox]").attr("checked", ele_checkbox.attr("checked"));
    } else {
        $(".NotbeSelectedArtistList input[type=checkbox]").removeAttr("checked");
    }
}

//搜索没有勾选的artist
function search_Not_be_Selected() {
    $(".NotbeSelectedArtistList li").show();
    var searchData = $(".SearchNotbeSelectedArtist[name=\"SelectArtist\"]").val().toLocaleLowerCase();

    if (searchData.length == 0) {
        $(".NotbeSelectedArtistList li").show();
        return false;
    }

    $(".NotbeSelectedArtistList li").each(function () {
        var currentLi = $(this);
        currentLi.find("input[type=checkbox]").each(function () {
            var currentinput = $(this).next().text().toLocaleLowerCase();
            if (!(currentinput.indexOf(searchData) > -1)) {
                currentLi.hide();
            }
        });
    });
}

//搜索勾选的artist
function search_Selected() {
    $(".SelectedArtistList li").show();
    var searchData = $(".SearchArtist[name=\"SelectArtist\"]").val().toLocaleLowerCase();
    if (searchData.length == 0) {
        $(".SelectedArtistList li").show();
        return false;
    }
    $(".SelectedArtistList li").each(function () {
        var currentLi = $(this);
        currentLi.find("input[type=checkbox]").each(function () {
            var currentinput = $(this).next().text().toLocaleLowerCase();
            if (!(currentinput.indexOf(searchData) > -1)) {
                currentLi.hide();
            }
        });
    });
}

function SelectedArtisToNotSelected() {
    $(".SelectedArtistList li").each(function () {
        var currentLi = $(this);
        currentLi.find("input[type=checkbox]:checked:visible").each(function () {
            //$(".NotbeSelectedArtistList").appendTo(currentLi);
            $(".NotbeSelectedArtistList").append(currentLi[0].outerHTML.replace("checked", ""))
            currentLi.remove();
        });
    });
}

function UnSelectedArtistToSelected() {
    $(".NotbeSelectedArtistList li").each(function () {
        var currentLi = $(this);
        currentLi.find("input[type=checkbox]:checked:visible").each(function () {
            //$(".NotbeSelectedArtistList").appendTo(currentLi);
            if (!(currentLi.find("input[type=checkbox]:checked").is("#input_checkAll"))) {
                $(".SelectedArtistList").append(currentLi[0].outerHTML.replace("checked", ""));
                currentLi.remove();
            }
        });
    });
}

function DivOpen(name, url, func) {
    $("#div_mask_layout").addClass("show");
    $("#div_artise_choose_pancel").fadeIn();
    Load_Selected(name, url);
    $("#OKButton").click(function () {
        var strhtml = new StringBuilder();
        var slist = $(".NotbeSelectedArtistList input[type=checkbox]:checked");
        slist.each(function (n, m) {
            if (slist.length == n + 1) {
                strhtml.AppendFormat("<b code='{0}'>{1}</b> ", $(this).val(), $(this).next().text());
            } else {
                strhtml.AppendFormat("<b code='{0}'>{1},</b> ", $(this).val(), $(this).next().text());
            }
        });
        $("#" + name).html(strhtml.ToString());
        DivClose();
        $(this).unbind("click");
        (func) && (func());
    });
}

function Load_Selected(name, url) {
    var selected = "";
    $.each($("#" + name + " b[code]"), function (n, m) {
        selected += $(m).attr("code") + ",";
    });

    var strhtml = new StringBuilder();
    var strhtmlright = new StringBuilder();

    $.post(url, { SearchData: "" }, function (data) {
        if (data.IsSuccess) {
            //strhtml.Append("<li class='even'><input type=\"checkbox\" id=\"input_checkAll\" onclick=\"javascript:onChangeChecked(this);\"/><a href=\"#\" style=\"font-weight:Bold;\">Check All</a></li>");
            $.each(data.EntityList, function (n, m) {
                //if (selected.indexOf(m.Value) > -1) {
                if (n % 2 == 0) {
                    strhtmlright.Append("<li class='odd'>");
                } else {
                    strhtmlright.Append("<li class='even'>");
                }
                if (selected.indexOf(m.Value) > -1) {
                    strhtmlright.AppendFormat("<input type=\"checkbox\"  value=\"{0}\"  checked />", m.Value);
                } else {
                    strhtmlright.AppendFormat("<input type=\"checkbox\"  value=\"{0}\" />", m.Value);
                }
                strhtmlright.AppendFormat("<a href=\"#\">{0}</a>", m.Text);
                strhtmlright.Append("</li>");
                //} else {
                //if (n % 2 == 0) {
                //    strhtml.Append("<li class='odd'>");
                //} else {
                //    strhtml.Append("<li class='even'>");
                //}
                //strhtml.AppendFormat("<input type=\"checkbox\" value=\"{0}\" />", m.Value);
                //strhtml.AppendFormat("<a href=\"#\">{0}</a>", m.Text);
                //strhtml.Append("</li>");
                //}
            });
            //$(".NotbeSelectedArtistList").html(strhtml.ToString());
            $(".NotbeSelectedArtistList").html(strhtmlright.ToString());
            $(".SearchNotbeSelectedArtist[name=\"SelectArtist\"]").val("");
            //$(".SearchArtist[name=\"SelectArtist\"]").val("");
        } else {
            alert(data.Exception);
        }
    });
}

function setArtistChoosePancel() {
    var divHeight = $("#div_artise_choose_pancel").height();
    if (divHeight == null)
        divHeight = $("#div_artise_choose_pancel1").height();

    var height = ($(window).height() - divHeight) / 2;
    if (height < 0) {
        height = 20;
    }
    $("#div_artise_choose_pancel,").css({
        position: 'absolute',
        left: ($(window).width() - $("#div_artise_choose_pancel").width()) / 2,
        top: height
    });

    $("#div_artise_choose_pancel1").css({
        position: 'absolute',
        left: ($(window).width() - $("#div_artise_choose_pancel1").width()) / 2,
        top: height
    });
}


/****************  复选 end ************************/

/*******************  单选 start *******************************/

function DivOpen2(name, url, func) {
    $("#div_mask_layout").addClass("show");
    $("#div_artise_choose_pancel1").fadeIn();
    $("#SelectedArtist").val("");
    Load_Selected2(name, url);

    $("#OKButton2").click(function () {
        var strhtml = new StringBuilder();
        $(".SelectedArtistList:visible input[type=radio]:checked").each(function () {
            strhtml.AppendFormat("<b code='{0}'>{1}</b> ", $(this).val(), $(this).next().text());
        });
        $("#" + name).html(strhtml.ToString());
        DivClose();
        $(this).unbind("click");
        (func) && (func());
    });
}


function Load_Selected2(name, url) {

    var selected = "";
    $.each($("#" + name + " b[code]"), function (n, m) {
        selected += $(m).attr("code") + ",";
    });

    var strhtml = new StringBuilder();
    $.post(url, { SearchData: $("#SelectedArtist").val() }, function (data) {
        if (data.IsSuccess) {
            $.each(data.EntityList, function (n, m) {
                if (n % 2 == 0) {
                    strhtml.Append("<li class='odd'>");
                } else {
                    strhtml.Append("<li class='even'>");
                }
                if (selected.indexOf(m.Value) > -1) {
                    strhtml.AppendFormat("<input type=\"radio\" name=\"artist\" checked value=\"{0}\" />", m.Value);
                    strhtml.AppendFormat("<a href=\"#\">{0}</a>", m.Text);
                    strhtml.Append("</li>");
                } else {
                    strhtml.AppendFormat("<input type=\"radio\" name=\"artist\" value=\"{0}\" />", m.Value);
                    strhtml.AppendFormat("<a href=\"#\">{0}</a>", m.Text);
                    strhtml.Append("</li>");
                }
            });
            $(".SelectedArtistList").html(strhtml.ToString());
        } else {
            alert(data.Exception);
        }
    });
}


//搜索勾选的artist
function search_Selected2() {
    $(".SelectedArtistList li").show();
    var searchData = $("#SelectedArtist").val().toLocaleLowerCase();

    if (searchData.length == 0) {
        $(".SelectedArtistList li").show();
        return false;
    }
    $(".SelectedArtistList li").each(function () {
        var currentLi = $(this);
        currentLi.find("input[type=radio]").each(function () {
            var currentinput = $(this).next().text().toLocaleLowerCase();
            if (!(currentinput.indexOf(searchData) > -1)) {
                currentLi.hide();
            }
        });
    });
}

/************************ 单选  end *******************************/
/************************ jasonyiu end *****************************************/


/******************* 剪断select中的Option jasonyiu  2013-11-29 start *****************/

$(function () {

    $(".cutOption").each(function () {
        cutOption(this, 30);
    });
});

function cutOption(selectObj, length) {
    $(selectObj).find("option").each(function (n, m) {
        var text = $(m).text();
        //$(m).attr("title", text);
        m.setAttribute("title", text);//for ie7  modified by mark 2013-12-19
        if (text.length > length) {
            $(m).text(text.substring(0, length) + "...");
        }

        
    });

    var browser = navigator.appName;

    var b_version = navigator.appVersion;

    var version = b_version.split(";");

    var trim_Version = version[1].replace(/[ ]/g, "");

    if (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE8.0") {

        //alert("IE 7.0");
        //$("div", $(ele)).css({ "width": wrapEleWidth, "margin": 0, "padding": 0 });
        $(selectObj).width("200px");

    }
}

/************************  剪断select中的Option  end **********************************/




/************************** 弹出层 自适应 start Jasonyiu 2013-12-9 ****************************/


function PanelOpen(url, val) {
    $("#div_mask_layout").addClass("show");

    $.get(url, val, function myfunction(data) {
        $(".content_panel").html(data);
        $("#wrapper_panel").fadeIn();
    });
}


function PanelClose() {
    $("#div_mask_layout").removeClass("show");
    $("#wrapper_panel").fadeOut();
}

function DataSuccess(xhr) {
    if (xhr.indexOf("runing<script") > -1) {
        $("body").append(xhr);
        return false;
    }
}

function DataComplete(xhr) {
    if (xhr.responseText.indexOf("<form") > -1) {
        $(".content_panel").html(xhr.responseText);
        //$.validator.unobtrusive.parse("form");
        return false;
    }
}



/************************** 弹出层 自适应 end ****************************/


/********************** 部分视图变换 start Jasonyiu 2013-12-17 *************************/


function SkipPanel(url, val, panel) {
    $.get(url, val, function (data) {
        $("#" + panel).html(data);
        return data;
    });
}


function SubPanel(url, val, panel) {
    $.post(url, val, function (data) {
        $("#" + panel).html(data);
        return data;
    });
}



/************************** 部分视图变换 end ****************************/


