
function btnCancel_onclick() {
    window.location.href = "/AccessRight/index/" + $("#selectedGroupID").val();
}

function btnUpdate_onclick() {
    var str = "";
    $.each($("#AccessRightForm input[itemid]:checked"), function (n, m) {
        str += $(m).attr("itemid") + ",";
    });
    $("#ModuleActions").val(str);
   
    $.post("/AccessRight/Index", $("#AccessRightForm").serializeArray(), function (data) {
        if (data.IsSuccess) {
            alert("Update Success! The accessright will take effect after re-login ");
            window.location.href = "/AccessRight/index/" + $("#selectedGroupID").val();
        } else {
            alert(data.Exception);
        }
    });
}

function Load_Group() {
    var strhtml = new StringBuilder();
    $.post("/Group/GetAllGroup", { GroupName: $("#GroupName").val() }, function (data) {
        if (data.IsSuccess) {
            $.each(data.EntityList, function (n, m) {
                if (n % 2 == 0) {

                    strhtml.Append("<li class='odd'>");
                } else {
                    strhtml.Append("<li class='even'>");
                }
                strhtml.AppendFormat("<a href=\"/accessright/index/{0}\">{1}</a>",m.GroupID, m.GroupName);
                strhtml.Append("</li>");
            })
            $("#Grouplist").html(strhtml.ToString());
        } else {
            alert(data.Exception);
        }
    });
}

function CheckAll(moduleID) {

    if ($("#chk_" + moduleID).attr("checked") == "checked") {
        $("#AccessRightForm input[itemid^='" + moduleID + "']").attr("checked", true);
    }
    else {
        $("#AccessRightForm input[itemid^='" + moduleID + "']").attr("checked", false);
    }

}
