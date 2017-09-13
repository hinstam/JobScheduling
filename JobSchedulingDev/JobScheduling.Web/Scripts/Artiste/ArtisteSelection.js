window.onresize = function () {
    //setArtistChoosePancel();
}

$(document).ready(function () {
    setArtistChoosePancel();
});


/***********************************双列 start*************************************/
//保存当前选中的
function submitArtise(name) {    if ($(".div_artiselist.columns li").length > 0) {
        var strArtiseList = "";
        $('#' + name).val(strArtiseList);
        $("input[type=checkbox]:checked").each(function () {
            strArtiseList += $(this).val() + ";\r\n";
        });
        $('#' + name).val(strArtiseList);
        hideMaskLayout(name);
        strArtiseList = "";
    }
    else {
        hideMaskLayout(name);
    }
}

//显示遮罩并给list传入初始参数 ["" 空字符串]
function showMaskLayout(name) {
    $("#div_mask_layout").addClass("show");
    $("#div_artise_choose_pancel").fadeIn();
    $("#SelectedArtist").val("");
    Load_SelectedArtist2(name);
}

//隐藏遮罩并给list传入设定参数 ["" 空字符串]
function hideMaskLayout(name) {
    $("#div_mask_layout").removeClass("show");
    $("#div_artise_choose_pancel").fadeOut();

    $("#SelectedArtist").val("");
    Load_SelectedArtist2(name);
}

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
            var currentinput = $(this).val().toLocaleLowerCase();
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
            var currentinput = $(this).val().toLocaleLowerCase();
            if (!(currentinput.indexOf(searchData) > -1)) {
                currentLi.hide();
            }
        });
    });
}


function submitArtise2(name) {
    if ($(".SelectedArtistList li").length > 0) {
        var strArtiseList = "";
        $('#' + name).val(strArtiseList);
        $(".SelectedArtistList input[type=checkbox]").each(function () {
            strArtiseList += $(this).val() + ";\r\n";
        });
        $('#' + name).val(strArtiseList);
        hideMaskLayout(name);
        strArtiseList = "";
    }
    else {
        hideMaskLayout(name);
    }
}

function Load_SelectedArtist2(name) {
    var strhtml = new StringBuilder();
    var strhtmlright = new StringBuilder();
    var tmpdata = "";
    $.post("/Home/GetSearchArtists", { SearchData: "" }, function (data) {
        if (data.IsSuccess) {
            strhtml.Append("<li class='even'><input type=\"checkbox\" id=\"input_checkAll\" onclick=\"javascript:onChangeChecked(this);\"/><a href=\"#\" style=\"font-weight:Bold;\">Check All</a></li>");
            $.each(data.EntityList, function (n, m) {
                tmpdata = (m.Artist_Code + "-" + m.EnglishName + "(" + m.ChineseName + ")");
                if ($('#' + name).val().indexOf(tmpdata) > -1) {
                    if (n % 2 == 0) {
                        strhtmlright.Append("<li class='odd'>");
                    } else {
                        strhtmlright.Append("<li class='even'>");
                    }
                    strhtmlright.AppendFormat("<input type=\"checkbox\"  value=\"{0}\" />", tmpdata);
                    strhtmlright.AppendFormat("<a href=\"#\">{0}</a>", tmpdata);
                    strhtmlright.Append("</li>");
                } else {
                    if (n % 2 == 0) {
                        strhtml.Append("<li class='odd'>");
                    } else {
                        strhtml.Append("<li class='even'>");
                    }
                    strhtml.AppendFormat("<input type=\"checkbox\" value=\"{0}\" />", tmpdata);
                    strhtml.AppendFormat("<a href=\"#\">{0}</a>", tmpdata);
                    strhtml.Append("</li>");
                }
            });
            $(".NotbeSelectedArtistList").html(strhtml.ToString());
            $(".SelectedArtistList").html(strhtmlright.ToString());
            $(".SearchNotbeSelectedArtist[name=\"SelectArtist\"]").val("");
            $(".SearchArtist[name=\"SelectArtist\"]").val("");
        } else {
            alert(data.Exception);
        }
    });

}

function SelectedArtisToNotSelected() {
    $(".SelectedArtistList li").each(function () {
        var currentLi = $(this);
        currentLi.find("input[type=checkbox]:checked").each(function () {
            //$(".NotbeSelectedArtistList").appendTo(currentLi);
            $(".NotbeSelectedArtistList").append(currentLi[0].outerHTML.replace("checked", ""))
            currentLi.remove();
        });
    });
}

function UnSelectedArtistToSelected() {
    $(".NotbeSelectedArtistList li").each(function () {
        var currentLi = $(this);
        currentLi.find("input[type=checkbox]:checked").each(function () {
            //$(".NotbeSelectedArtistList").appendTo(currentLi);
            if (!(currentLi.find("input[type=checkbox]:checked").is("#input_checkAll"))) {
                $(".SelectedArtistList").append(currentLi[0].outerHTML.replace("checked", ""));
                currentLi.remove();
            }
        });
    });
}
/***********************************双列 end*************************************/

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


/***********************************单列 start*************************************/
function Load_SelectedArtist1(name) {
    var strhtml = new StringBuilder();
    var tmpdata = "";
    $.post("/Home/GetSearchArtists", { SearchData: $("#SelectedArtist").val() }, function (data) {
        if (data.IsSuccess) {
            //strhtml.Append("<li class='even'><input type=\"checkbox\" id=\"input_checkAll\" onclick=\"javascript:onChangeChecked(this);\"/><a href=\"#\" style=\"font-weight:Bold;\">Check All</a></li>");
            $.each(data.EntityList, function (n, m) {
                if (n % 2 == 0) {
                    strhtml.Append("<li class='odd'>");
                } else {
                    strhtml.Append("<li class='even'>");
                }
                tmpdata = (m.Artist_Code + "-" + m.EnglishName + "(" + m.ChineseName + ")");
                if ($('#' + name).val().indexOf(tmpdata) > -1) {
                    strhtml.AppendFormat("<input type=\"radio\" name=\"artist\" checked value=\"{0}\" />", tmpdata);
                    strhtml.AppendFormat("<a href=\"#\">{0}</a>", tmpdata);
                    strhtml.Append("</li>");
                } else {
                    strhtml.AppendFormat("<input type=\"radio\" name=\"artist\" value=\"{0}\" />", tmpdata);
                    strhtml.AppendFormat("<a href=\"#\">{0}</a>", tmpdata);
                    strhtml.Append("</li>");
                }
            });
            $("#SelectedArtistList").html(strhtml.ToString());
        } else {
            alert(data.Exception);
        }
    });

}

function submitArtise1(name) {
    if ($(".div_artiselist li").length > 0) {
        var strArtiseList = "";
        $('#' + name).val(strArtiseList);
        $("input[type=radio]:checked").each(function () {
            strArtiseList += $(this).val() + ";\r\n";
        });
        $('#' + name).val(strArtiseList);
        hideMaskLayout1(name);
        strArtiseList = "";
    }
    else {
        hideMaskLayout1(name);
    }
}

function showMaskLayout1(name) {
    $("#div_mask_layout").addClass("show");
    $("#div_artise_choose_pancel1").fadeIn();
    $("#SelectedArtist").val("");
    Load_SelectedArtist1(name);
}

function hideMaskLayout1(name) {
    $("#div_mask_layout").removeClass("show");
    $("#div_artise_choose_pancel1").fadeOut();
    $("#SelectedArtist").val("");
    Load_SelectedArtist1(name);
}

/***********************************单列 end*************************************/