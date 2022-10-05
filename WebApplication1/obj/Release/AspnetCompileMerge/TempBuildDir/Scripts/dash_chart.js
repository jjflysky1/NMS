///네트워크 보안 차트
$(function () {
    $.ajax({
        type: "POST",
        url: "/Chart/Chart.aspx/chart3",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess3,
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });
    $.ajax({
        type: "POST",
        url: "/Chart/Chart.aspx/chart2",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess2,
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });


    $.ajax({
        type: "POST",
        url: "/Chart/Chart.aspx/chart1",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });
});

setInterval(function () {
    $.ajax({
        type: "POST",
        url: "/Chart/Chart.aspx/chart3",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess3,
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });
    $.ajax({
        type: "POST",
        url: "/Chart/Chart.aspx/chart2",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess2,
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });
    $.ajax({
        type: "POST",
        url: "/Chart/Chart.aspx/chart1",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,

        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });
}, 1 * 10 * 1000);



var temp_traffic3 = "";
var serverip3 = "";
var temp_time3 = "";
var trafficArray3 = "";
var timeArray3 = "";
let data3 = "";
function OnSuccess3(response) {
    var customers3 = response.d;
    temp_traffic3 = "";
    temp_time3 = "";
    $(customers3).each(function () {
        serverip3 = this.serverip3;
        temp_traffic3 += this.traffic3 + ",";
        temp_time3 += this.time3 + ",";
    });

    temp_traffic3 = temp_traffic3.slice(0, -1);
    temp_time3 = temp_time3.slice(0, -1);
    trafficArray3 = temp_traffic3.split(',');
    timeArray3 = temp_time3.split(',');
    trafficArray3.reverse();
    timeArray3.reverse();
    data3 = trafficArray3;

}



var temp_traffic2 = "";
var serverip2 = "";
var temp_time2 = "";
var trafficArray2 = "";
var timeArray2 = "";
let data2 = "";
function OnSuccess2(response) {
    var customers2 = response.d;
    temp_traffic2 = "";
    temp_time2 = "";
    $(customers2).each(function () {
        serverip2 = this.serverip2;
        temp_traffic2 += this.traffic2 + ",";
        temp_time2 += this.time2 + ",";

    });
    temp_traffic2 = temp_traffic2.slice(0, -1);
    temp_time2 = temp_time2.slice(0, -1);
    trafficArray2 = temp_traffic2.split(',');
    timeArray2 = temp_time2.split(',');
    trafficArray2.reverse();
    timeArray2.reverse();
    data2 = trafficArray2;
}


function OnSuccess(response) {
    ////alert(String(response.d).length);
    //alert(String(timeArray3).length);
    //alert(String(temp_time2).length);
    //alert(String(temp_traffic2).length);
    var customers = response.d;
    var temp_traffic = "";
    var serverip = "";
    var temp_time = "";
    $(customers).each(function () {
        serverip = this.serverip;
        temp_traffic += this.traffic + ",";
        temp_time += this.time + ",";

    });
    temp_traffic = temp_traffic.slice(0, -1);
    temp_time = temp_time.slice(0, -1);
    var trafficArray = temp_traffic.split(',');
    var timeArray = temp_time.split(',');
    trafficArray.reverse();
    timeArray.reverse();
    let data = trafficArray;
    //alert(data);



    //function addData(chart, data) {
    //  chart.data.datasets.forEach(dataset => {
    //    let data = dataset.data;
    //    const first = data.shift();
    //    data.push(first);
    //    dataset.data = data;
    //  });
    //    chart.update();

    //}

    var ctx = "";
    var myChart = "";
    $('#div1').html(''); //remove canvas from container
    $('#div1').html('<canvas id="myChart" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>'); //add it back to the container
    ctx = document.getElementById("myChart").getContext("2d");
    myChart = new Chart(ctx, {
        type: "line",
        data: {
            labels: timeArray,
            datasets: [
                {
                    label: serverip,
                    data: trafficArray,
                    backgroundColor: ["rgba(113, 88, 203, .15)"],
                    borderColor: ["rgba(113, 88, 203, 1)"],
                    borderWidth: 1,
                    pointRadius: 1,
                    pointHoverRadius: 1,
                    fill: "start"
                }
                , {
                    label: serverip2,
                    data: trafficArray2,
                    backgroundColor: ["rgba(161, 201, 249, .15)"],
                    borderColor: ["rgba(161, 201, 249, 1)"],
                    borderWidth: 1,
                    pointRadius: 1,
                    pointHoverRadius: 1,
                    fill: "start"
                }
                , {
                    label: serverip3,
                    data: trafficArray3,
                    backgroundColor: ["rgba(192, 161, 249, .15)"],
                    borderColor: ["rgba(192, 161, 249, 1)"],
                    borderWidth: 1,
                    pointRadius: 1,
                    pointHoverRadius: 1,
                    fill: "start"
                }
            ]
        },
        options: {
            animation: {
                duration: 0
            },
            tooltips: {
                intersect: false,
                backgroundColor: "rgba(113, 88, 203, 1)",
                titleFontSize: 16,
                titleFontStyle: "400",
                titleSpacing: 4,
                titleMarginBottom: 8,
                bodyFontSize: 12,
                bodyFontStyle: '400',
                bodySpacing: 4,
                xPadding: 8,
                yPadding: 8,
                cornerRadius: 4,
                displayColors: false,

                callbacks: {
                    title: function (t, d) {
                        const o = d.datasets.map((ds) => ds.data[t[0].index] + " mbps")

                        return o.join(', ');
                    },
                    label: function (t, d) {
                        return d.labels[t.index];
                    }
                }
            },
            title: {
                text: "Public Bandwidth",
                display: false
            },
            maintainAspectRatio: true,
            spanGaps: false,
            elements: {
                line: {
                    tension: 0.3
                }
            },
            plugins: {
                filler: {
                    propagate: false
                }
            },
            legend: {
                labels: {
                    fontColor: "#d4d4d4",
                    fontSize: 11
                }
            },
            scales: {
                xAxes: [
                    {
                        ticks: {
                            fontColor: "#d4d4d4",
                            autoSkip: true,
                            maxTicksLimit: 5,
                            display: true,
                            maxRotation: 0,
                            minRotation: 0
                        },
                        gridLines: {
                            display: true,
                            drawOnChartArea: false,
                        }
                    }
                ],
                yAxes: [
                    {
                        ticks: {
                            fontColor: "#d4d4d4",
                            //reverse: true //리버스

                        }
                    }
                ],
                y2: {
                    reverse: true
                }

            }
        }
    });



};


///서버차트
$(function () {
    $.ajax({
        type: "POST",
        url: "/Chart/Chart3.aspx/chart3",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess6,
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });
    $.ajax({
        type: "POST",
        url: "/Chart/Chart3.aspx/chart2",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess5,
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });


    $.ajax({
        type: "POST",
        url: "/Chart/Chart3.aspx/chart1",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: OnSuccess4,
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });
});

setInterval(function () {
    $.ajax({
        type: "POST",
        url: "/Chart/Chart3.aspx/chart3",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess6,
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });
    $.ajax({
        type: "POST",
        url: "/Chart/Chart3.aspx/chart2",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess5,
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });
    $.ajax({
        type: "POST",
        url: "/Chart/Chart3.aspx/chart1",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess4,
        failure: function (response) {
            //alert(response.d);
        },
        error: function (response) {
            //alert(response.d);
        }
    });
}, 1 * 10 * 1000);


var temp_traffic6 = "";
var serverip6 = "";
var temp_time6 = "";
var trafficArray6 = "";
var timeArray6 = "";
let data6 = "";
function OnSuccess6(response) {
    var customers6 = response.d;
    temp_time6 = "";
    temp_traffic6 = "";
    $(customers6).each(function () {
        serverip6 = this.serverip3;
        temp_traffic6 += this.traffic3 + ",";
        temp_time6 += this.time3 + ",";
    });

    temp_traffic6 = temp_traffic6.slice(0, -1);
    temp_time6 = temp_time6.slice(0, -1);
    trafficArray6 = temp_traffic6.split(',');
    timeArray6 = temp_time6.split(',');
    trafficArray6.reverse();
    timeArray6.reverse();
    data6 = trafficArray6;
}


var temp_traffic5 = "";
var serverip5 = "";
var temp_time5 = "";
var trafficArray5 = "";
var timeArray5 = "";
let data5 = "";
function OnSuccess5(response) {
    var customers5 = response.d;
    temp_time5 = "";
    temp_traffic5 = "";
    $(customers5).each(function () {
        serverip5 = this.serverip2;
        temp_traffic5 += this.traffic2 + ",";
        temp_time5 += this.time2 + ",";

    });

    temp_traffic5 = temp_traffic5.slice(0, -1);
    temp_time5 = temp_time5.slice(0, -1);
    trafficArray5 = temp_traffic5.split(',');
    timeArray5 = temp_time5.split(',');
    trafficArray5.reverse();
    timeArray5.reverse();
    data5 = trafficArray5;
}


function OnSuccess4(response) {
    var customers4 = response.d;

    var temp_traffic4 = "";
    var serverip4 = "";
    var temp_time4 = "";
    var count4 = 0;
    $(customers4).each(function () {
        serverip4 = this.serverip;
        temp_traffic4 += this.traffic + ",";
        temp_time4 += this.time + ",";

    });
    temp_traffic4 = temp_traffic4.slice(0, -1);
    temp_time4 = temp_time4.slice(0, -1);
    var trafficArray4 = temp_traffic4.split(',');
    var timeArray4 = temp_time4.split(',');
    trafficArray4.reverse();
    timeArray4.reverse();
    let data4 = trafficArray4;
    //alert(data2);


    //function addData(chart, data) {
    //  chart.data.datasets.forEach(dataset => {
    //    let data = dataset.data;
    //    const first = data.shift();
    //    data.push(first);
    //    dataset.data = data;
    //  });

    //  chart.update();
    // }


    var ctx2 = "";
    var myChart2 = "";
    $('#div2').html(''); //remove canvas from container
    $('#div2').html('<canvas id="myChart2" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>'); //add it back to the container
    ctx2 = document.getElementById("myChart2").getContext("2d");
    myChart2 = new Chart(ctx2, {
        type: "line",
        data: {
            labels: timeArray4,
            datasets: [
                {
                    label: serverip4,
                    data: trafficArray4,
                    backgroundColor: ["rgba(113, 88, 203, .15)"],
                    borderColor: ["rgba(113, 88, 203, 1)"],
                    borderWidth: 1,
                    pointRadius: 1,
                    pointHoverRadius: 1,
                    fill: "start"
                }
                , {
                    label: serverip5,
                    data: trafficArray5,
                    backgroundColor: ["rgba(161, 201, 249, .15)"],
                    borderColor: ["rgba(161, 201, 249, 1)"],
                    borderWidth: 1,
                    pointRadius: 1,
                    pointHoverRadius: 1,
                    fill: "start"
                }
                , {
                    label: serverip6,
                    data: trafficArray6,
                    backgroundColor: ["rgba(192, 161, 249, .15)"],
                    borderColor: ["rgba(192, 161, 249, 1)"],
                    borderWidth: 1,
                    pointRadius: 1,
                    pointHoverRadius: 1,
                    fill: "start"
                }
            ]
        },
        options: {
            animation: {
                duration: 0
            },
            tooltips: {
                intersect: false,
                backgroundColor: "rgba(113, 88, 203, 1)",
                titleFontSize: 16,
                titleFontStyle: "400",
                titleSpacing: 4,
                titleMarginBottom: 8,
                bodyFontSize: 12,
                bodyFontStyle: '400',
                bodySpacing: 4,
                xPadding: 8,
                yPadding: 8,
                cornerRadius: 4,
                displayColors: false,

                callbacks: {
                    title: function (t, d) {
                        const o = d.datasets.map((ds) => ds.data[t[0].index] + " mbps")

                        return o.join(', ');
                    },
                    label: function (t, d) {
                        return d.labels[t.index];
                    }
                }
            },
            title: {
                text: "Public Bandwidth",
                display: false
            },
            maintainAspectRatio: true,
            spanGaps: false,
            elements: {
                line: {
                    tension: 0.3
                }
            },
            plugins: {
                filler: {
                    propagate: false
                }
            },
            legend: {
                labels: {
                    fontColor: "#d4d4d4",
                    fontSize: 11
                }
            },
            scales: {
                xAxes: [
                    {
                        ticks: {
                            fontColor: "#d4d4d4",
                            autoSkip: true,
                            maxTicksLimit: 5,
                            display: true,
                            maxRotation: 0,
                            minRotation: 0

                        },
                        gridLines: {
                            display: true,
                            drawOnChartArea: false,
                        }
                    }
                ],
                yAxes: [
                    {
                        ticks: {
                            fontColor: "#d4d4d4"

                        }
                    }
                ]
            }
        }
    });
};