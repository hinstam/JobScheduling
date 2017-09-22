"use strict";
//var today = moment();

//var andTwoHours = moment().add(2, "hours");

//var date = new Date(today.valueOf()).toLocaleDateString();

//console.log(today.valueOf())

//var today_friendly = "/Date(" + today.valueOf() + ")/";
//var next_friendly = "/Date(" + andTwoHours.valueOf() + ")/";

var source = [
{
    id: 1,
    name: "Pol1",
    desc: "A",
    cssClass: "ganttRed",
    values: [
        {
            from: "/Date(2017-9-18 00:00:00)/",
            to: "/Date(2017-9-18 10:00:00)/",
            label: "A",
            desc: "try to do this",
            //customClass: "ganttGreen",
            //dataObj: null
        }
    //    ,
    //    {
    //    from: "/Date(2017-9-19 00:00:00)/",
    //    to: "/Date(2017-9-19 00:00:00)/",
    //    label: "A",
    //    desc: "try to do this",
    //    customClass: "ganttGreen",
    //    dataObj: null
    //}
    //, {
    //    from: "/Date(2017-9-19 13:00:00)/",
    //    to: "/Date(2017-9-19 16:00:00)/",
    //    label: "A",
    //    desc: "try to do this",
    //    customClass: "ganttGreen",
    //    dataObj: null
    //}
    ]
}
    //, {
    //    name: "",
    //    desc: "B",
    //    values: [{
    //        from: "/Date(2017-9-18 01:00:00)/",
    //        to: "/Date(2017-9-18 03:00:00)/",
    //        label: "B",
    //        customClass: "ganttRed"
    //    },
    //    {
    //        from: "/Date(2017-9-18 11:00:00)/",
    //        to: "/Date(2017-9-18 12:00:00)/",
    //        label: "B",
    //        customClass: "ganttRed"
    //    }]
    //}, {
    //    name: "",
    //    desc: "C",
    //    values: [{
    //        from: "/Date(2017-9-18 04:00:00)/",
    //        to: "/Date(2017-9-18 08:00:00)/",
    //        label: "C",
    //        customClass: "ganttGreen"
    //    }, {
    //        from: "/Date(2017-9-18 17:00:00)/",
    //        to: "/Date(2017-9-18 20:00:00)/",
    //        label: "C",
    //        customClass: "ganttRed"
    //    }]
    //}, {
    //    name: "",
    //    desc: "D",
    //    values: [{
    //        from: "/Date(2017-9-18 21:00:00)/",
    //        to: "/Date(2017-9-18 23:00:00)/",
    //        label: "C",
    //        customClass: "ganttBlue"
    //    }]
    //}, {
    //    name: "测试",
    //    desc: "你好",
    //    values: [{
    //        from: today_friendly,
    //        to: next_friendly,
    //        label: "测试",
    //        customClass: "ganttBlue"
    //    }]
    //}, {
    //    name: "测试",
    //    desc: "你好",
    //    values: [{
    //        from: today_friendly,
    //        to: next_friendly,
    //        label: "测试",
    //        customClass: "ganttGreen"
    //    }]
    //}, {
    //    name: "测试",
    //    desc: "你好",
    //    values: [{
    //        from: today_friendly,
    //        to: next_friendly,
    //        label: "测试",
    //        customClass: "ganttGreen"
    //    }]
    //}, {
    //    name: "测试",
    //    desc: "你好",
    //    values: [{
    //        from: today_friendly,
    //        to: next_friendly,
    //        label: "测试",
    //        customClass: "ganttGreen"
    //    }]
    //}, {
    //    name: "测试",
    //    desc: "你好",
    //    values: [{
    //        from: today_friendly,
    //        to: next_friendly,
    //        label: "测试",
    //        customClass: "ganttGreen"
    //    }]
    //}, {
    //    name: "测试",
    //    desc: "你好",
    //    values: [{
    //        from: today_friendly,
    //        to: next_friendly,
    //        label: "测试",
    //        customClass: "ganttGreen"
    //    }]
    //}, {
    //    name: "测试",
    //    desc: "你好",
    //    values: [{
    //        from: today_friendly,
    //        to: next_friendly,
    //        label: "测试",
    //        customClass: "ganttGreen"
    //    }]
    //}
];


//$.post('/Home/GetGanttSource', function (data) {
//    console.log(data);

//    $(".gantt").gantt({
//        source: data,
//        scale: "hours",
//        minScale: "hours",
//        maxScale: "hours",
//        navigate: "scroll",
//        itemsPerPage: 100,
//        months: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
//        dow: ["日", "一", "二", "三", "四", "五", "六"],
//        holidays: ["2017-9-18"],
//        onItemClick: function (data) {
//            console.log(data.name);
//        },
//        onAddClick: function (dt, rowId) {
//            console.log(dt + " " + rowId);
//        },
//        onRender: function () {
//            console.log("Gantt has rendered");
//        }
//    });
//});

$(".gantt").gantt({
    source: source,
    scale: "hours",
    minScale: "hours",
    //maxScale: "hours",
    navigate: "scroll",
    itemsPerPage: 100,
    //months: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"],
    //dow: ["日", "一", "二", "三", "四", "五", "六"],
    //holidays: ["2017-9-18"],
    //onItemClick: function (data) {
    //    console.log(data.name);
    //},
    //onAddClick: function (dt, rowId) {
    //    console.log(dt + " " + rowId);
    //},
    onRender: function () {
        console.log("Gantt has rendered");
    }
});

$.post("/ProductScheduling/ExportProductScheduling", function (data) {
    if (data.IsSuccess) {
        window.location = "/File/Download?fileName=" + data.FileName + "&type=1";
    }
    else {
        alert(data.Message);
    }
});