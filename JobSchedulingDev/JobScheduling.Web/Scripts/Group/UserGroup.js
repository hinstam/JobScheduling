$(document).ready(function () {
    Load_User();
    Load_SelectedUser();
});


function Load_Group() {
    var strhtml = new StringBuilder();
    $.post("/Group/GetAllGroup", { GroupName: $("#GroupName").val() }, function (data) {
        if (data.IsSuccess) {
            $.each(data.EntityList, function (n, m) {
                if (n % 2 == 0) {

                    strhtml.AppendFormat("<li class='odd' onclick=\"LiGroup_onclick('{0}','{1}')\" >", m.GroupName, m.GroupID);
                } else {
                    strhtml.AppendFormat("<li class='even' onclick=\"LiGroup_onclick('{0}','{1}')\" >", m.GroupName, m.GroupID);
                }
                strhtml.AppendFormat("<a href=\"#\">{0}</a>", m.GroupName);
                strhtml.Append("</li>");
            })
            $("#Grouplist").html(strhtml.ToString());
        } else {
            alert(data.Exception);
        }
    });
}


function Load_User() {
    var strhtml = new StringBuilder();
    $.post("/User/GetAllUser", { GroupID: $("#GroupID").val(), UserName: $("#UserName").val() }, function (data) {
        if (data.IsSuccess) {
            $.each(data.EntityList, function (n, m) {
                if (n % 2 == 0) {
                    strhtml.Append("<li class='odd'>");
                } else {
                    strhtml.Append("<li class='even'>");
                }
                strhtml.AppendFormat("<input type=\"checkbox\" uid=\"{0}\" />", m.UID);
                strhtml.AppendFormat("<a href=\"#\">{0}</a>", m.UserName);
                strhtml.Append("</li>");
            })
            $("#UserList").html(strhtml.ToString());
        } else {
            alert(data.Exception);
        }
    });
}


function Load_SelectedUser() {
    if ($("#GroupID").val().length == 0) {
        return false;
    }
    var strhtml = new StringBuilder();
    $.post("/User/GetSelectedUser", { GroupID: $("#GroupID").val(), UserName: $("#SelectedUser").val() }, function (data) {
        if (data.IsSuccess) {
            $.each(data.EntityList, function (n, m) {
                if (n % 2 == 0) {
                    strhtml.Append("<li class='odd'>");
                } else {
                    strhtml.Append("<li class='even'>");
                }
                strhtml.AppendFormat("<input type=\"checkbox\" uid=\"{0}\" />", m.UID);
                strhtml.AppendFormat("<a href=\"#\">{0}</a>", m.UserName);
                strhtml.Append("</li>");
            })
            $("#SelectedUserList").html(strhtml.ToString());
        } else {
            alert(data.Exception);
        }
    });
}


function LiGroup_onclick(name, num) {
    $("#GroupID").val(num);
    $(".title").html(name);
    $("#SelectedUser").val("");
    $("#UserName").val("")
    Load_SelectedUser();
    Load_User();
}


function AddUser() {
    var uids = "";
    $.each($("#UserList input:checked"), function (n, m) {
        var uid = $(m).attr("uid");
        uids += uid + ",";
    });
    if (uids.length == 0) {
        alert("Please select a user!");
    }
    $.post("/Group/AddUserGroup", { groupID: $("#GroupID").val(), userUIDs: uids }, function (data) {
        if (data.IsSuccess) {
            Load_SelectedUser();
            Load_User();
        } else {
            alert(data.Exception);
        }
    });
}


function DelUser() {
    var uids = "";
    $.each($("#SelectedUserList input:checked"), function (n, m) {
        var uid = $(m).attr("uid");
        uids += uid + ",";
    });
    if (uids.length == 0) {
        alert("Please select a user!");
    }
    $.post("/Group/DelUserGroup", { groupID: $("#GroupID").val(), userUIDs: uids }, function (data) {
        if (data.IsSuccess) {
            Load_SelectedUser();
            Load_User();
        } else {
            alert(data.Exception);
        }
    });
}
