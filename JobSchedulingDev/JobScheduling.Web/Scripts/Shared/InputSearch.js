function LoadInputSearchData(name, url, func) {
    $.post(url, name + "=" + $("#" + name).val(), function (data) {
        if (data.IsSuccess) {
            $("#" + name).autocomplete({ source: data.EntityList });
            $("#" + name).blur(func);
        } else {
            alert(data.Exception);
        }
    });
}