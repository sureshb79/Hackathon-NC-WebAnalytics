$(function () {
    GetBarData();
    GetDoughNutData();
    GetLineData();
    
    //Morris.Area({
    //    element: 'morris-area-chart',
    //    data: [{
    //        period: '2010 Q1',
    //        iphone: 2666,
    //        ipad: null,
    //        itouch: 2647
    //    }, {
    //        period: '2010 Q2',
    //        iphone: 2778,
    //        ipad: 2294,
    //        itouch: 2441
    //    }, {
    //        period: '2010 Q3',
    //        iphone: 4912,
    //        ipad: 1969,
    //        itouch: 2501
    //    }, {
    //        period: '2010 Q4',
    //        iphone: 3767,
    //        ipad: 3597,
    //        itouch: 5689
    //    }, {
    //        period: '2011 Q1',
    //        iphone: 6810,
    //        ipad: 1914,
    //        itouch: 2293
    //    }, {
    //        period: '2011 Q2',
    //        iphone: 5670,
    //        ipad: 4293,
    //        itouch: 1881
    //    }, {
    //        period: '2011 Q3',
    //        iphone: 4820,
    //        ipad: 3795,
    //        itouch: 1588
    //    }, {
    //        period: '2011 Q4',
    //        iphone: 15073,
    //        ipad: 5967,
    //        itouch: 5175
    //    }, {
    //        period: '2012 Q1',
    //        iphone: 10687,
    //        ipad: 4460,
    //        itouch: 2028
    //    }, {
    //        period: '2012 Q2',
    //        iphone: 8432,
    //        ipad: 5713,
    //        itouch: 1791
    //    }],
    //    xkey: 'period',
    //    ykeys: ['iphone', 'ipad', 'itouch'],
    //    labels: ['iPhone', 'iPad', 'iPod Touch'],
    //    pointSize: 2,
    //    hideHover: 'auto',
    //    resize: true
    //});



});


function GetBarData() {
    $.ajax({
        type: 'POST',
        url: '../AjaxCall.aspx/GetBarData',
        data: '{}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            var JSONdata = JSON.parse(data.d);
            ParseJsonBar(JSONdata.Table);
        }
    });


}
function ParseJsonBar(JSONdata) {
    var App1 = JSONdata[0].App1;
    var App2 = JSONdata[0].App2;
    Morris.Bar({
        element: 'morris-bar-chart',
        data: JSONdata,
        xkey: 'timeline',
        ykeys: ['compare1', 'compare2'],
        labels: [App1, App2],
        hideHover: 'auto',
        resize: true
    });

}
function BindDoughNut(JSONdata) {
    Morris.Donut({
        element: 'morris-donut-chart',
        data: JSONdata,
        resize: true
    });

}

function GetDoughNutData() {
    $.ajax({
        type: 'POST',
        url: '../AjaxCall.aspx/GetDoughNutData',
        data: '{}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            var JSONdata = JSON.parse(data.d);
            BindDoughNut(JSONdata.Table);
        }
    });
}

function GetLineData() {

    $.ajax({
        type: 'POST',
        url: '../AjaxCall.aspx/GetLineData',
        data: '{}',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (data) {
            var JSONdata = JSON.parse(data.d);
            BindLine(JSONdata.Table);
        }
    });


}
function BindLine(JSONdata) {
    var App1 = JSONdata[0].App1;
    var App2 = JSONdata[0].App2;
    Morris.Area({
        element: 'morris-area-chart',
        data: JSONdata,
        xkey: 'timeline',
        ykeys: ['compare1', 'compare2'],
        labels: [App1, App2],
        pointSize: 2,
        hideHover: 'auto',
        resize: true
    });

}

