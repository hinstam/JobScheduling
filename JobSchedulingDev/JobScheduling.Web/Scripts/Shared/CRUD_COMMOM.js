
//del
function btn_Del(url, param, func) {
    var index = $(".content_gridview .Selected");
    var _id = index.find("li").eq(1);
    var message = "";

    if (param) {
        var _cn = index.find("li[columnName='" + param + "']");
        message = "Are you sure to delete it 【" + _cn.attr("columnName") + "=" + _cn.text() + "】?";
    } else {
        message = "Are you sure to delete it ?";
    }

    if (index.length > 0) {
        if (confirm(message)) {
            $.post(url, $.trim(_id.attr("columnName")) + "=" + $.trim(_id.text()), function (data) {
                if (data.IsSuccess) {
                    alert("Success!");

                    (func) && (func(url, data));

                    index.remove();

                } else {
                    alert(data.Exception);
                }
            });
        }
    } else {
        alert("Please select list!");
    }
}
 
//edit
function btn_Edit(url, param) {
    var index = $(".content_gridview .Selected");
    var param = param || index.find("li").eq(1).attr("columnName");
    var _id = $.trim( index.find("li").eq(1).text());
    if (_id.length > 0){
        window.location = url + "?" +  $.trim(param) + "=" + _id;
    } else {
        alert("Please select list!");
    }
};

//detail
function btn_Detail(url, param) {
    var index = $(".content_gridview .Selected");
    var param = param || index.find("li").eq(1).attr("columnName");
    var _id = $.trim(index.find("li").eq(1).text());
    if (index.length > 0) {
        window.location = url + "?" + $.trim(param) + "=" + _id;
    } else {
        alert("Please select list!");
    }
}

//print
function btn_Print(url, param) {
    var index = $(".content_gridview .Selected");
    var param = param || index.find("li").eq(1).attr("columnName");
    var _id = $.trim(index.find("li").eq(1).text());
    if (index.length > 0) {
        var targetURL = url + "?" + $.trim(param) + "=" + _id;
        window.open(targetURL, "_blank");
    } else {
        alert("Please select list!");
    }
}


//redirect 
function Redirect(url, param) {
    var index = $(".content_gridview .Selected");
    var param = param || index.find("li").eq(1).attr("columnName");
    var _id = $.trim(index.find("li").eq(1).text());
    if (index.length > 0) {
        window.location = url + "?" + $.trim(param) + "=" + _id;
    } else {
        alert("Please select list!");
    }
}

//function btn_Add(url)
//{
//    var index = $(".content_gridview .Selected");
//    var param = param || index.find("li").eq(0).attr("columnName");

//    window.location = url + "?" + param.trim() + "=" + _id;
//}



/**************** StringBuilder start  jason 2013-10-25************************/

function StringBuilder() {
    this._buffers = [];
    this._length = 0;
    this._splitChar = arguments.length > 0 ? arguments[arguments.length - 1] : '';

    if (arguments.length > 0) {
        for (var i = 0, iLen = arguments.length - 1; i < iLen; i++) {
            this.Append(arguments[i]);
        }
    }
}

StringBuilder.prototype.Append = function (str) {
    this._length += str.length;
    this._buffers[this._buffers.length] = str;
}
StringBuilder.prototype.Add = StringBuilder.prototype.append;

StringBuilder.prototype.AppendFormat = function () {
    if (arguments.length > 1) {
        var TString = arguments[0];
        if (arguments[1] instanceof Array) {
            for (var i = 0, iLen = arguments[1].length; i < iLen; i++) {
                var jIndex = i;
                var re = eval("/\\{" + jIndex + "\\}/g;");
                TString = TString.replace(re, arguments[1][i]);
            }
        }
        else {
            for (var i = 1, iLen = arguments.length; i < iLen; i++) {
                var jIndex = i - 1;
                var re = eval("/\\{" + jIndex + "\\}/g;");
                TString = TString.replace(re, arguments[i]);
            }
        }
        this.Append(TString);
    }
    else if (arguments.length == 1) {
        this.Append(arguments[0]);
    }
}

StringBuilder.prototype.Length = function () {
    if (this._splitChar.length > 0 && (!this.IsEmpty())) {
        return this._length + (this._splitChar.length * (this._buffers.length - 1));
    }
    else {
        return this._length;
    }
}

StringBuilder.prototype.IsEmpty = function () {
    return this._buffers.length <= 0;
}

StringBuilder.prototype.Clear = function () {
    this._buffers = [];
    this._length = 0;
}

StringBuilder.prototype.ToString = function () {
    if (arguments.length == 1) {
        return this._buffers.join(arguments[1]);
    }
    else {
        return this._buffers.join(this._splitChar);
    }
}


/**************** StringBuilder end ************************/


