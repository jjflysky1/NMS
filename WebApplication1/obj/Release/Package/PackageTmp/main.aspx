<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="WebApplication1.main" %>

<%@ Register Src="~/Common/leftmenu.ascx" TagName="uc_menu" TagPrefix="uc1" %>
<%@ Register Src="~/Common/bottom.ascx" TagName="uc_bottom" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel='stylesheet' href="Content/bootstrap.min.css" />
    <link rel="stylesheet" href="Content/AdminLTE.min.css" />
    <link rel="stylesheet" href="Content/font-awesome.min.css" />

    <link rel="stylesheet" href="Content/_all-skins.min.css" />
    <link rel="stylesheet" href="Content/ionicons.min.css" />
    <link rel="stylesheet" href="Content/bootstrap.min2.css" />
    <%--<link rel="stylesheet" href="Content/style.css" />--%>
    <script src="Scripts/jquery-3.2.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Content/JavaScript.js"></script>
    <script src="Scripts/AdminLTE.css"></script>
    <script src="Scripts/highcharts.js"></script>
    <script src="Scripts/highcharts-3d.js"></script>
    <script src="Scripts/exporting.js"></script>
    <script src="Scripts/export-data.js"></script>
    <script src="Scripts/jschart.js"></script>
    <link href="Scripts/Allcss.css" rel="stylesheet" />
    <title></title>
    <script type="text/javascript">




        function go(No, category) {

            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;
        var obj = document.getElementById("<%=HiddenField2.ClientID %>");
        if (obj)
            obj.value = category;


       <%--     <%= Page.GetPostBackEventReference(Button2) %>--%>

        }
        function go3(No) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;

       <%= Page.GetPostBackEventReference(Button5) %>

        }
        function go2(No, category) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;
        var obj = document.getElementById("<%=HiddenField2.ClientID %>");
        if (obj)
            obj.value = category;

       <%= Page.GetPostBackEventReference(Button4) %>

        }



        function getUserIP(onNewIP) { //  onNewIp - your listener function for new IPs
            //compatibility for firefox and chrome
            var myPeerConnection = window.RTCPeerConnection || window.mozRTCPeerConnection || window.webkitRTCPeerConnection;
            var pc = new myPeerConnection({
                iceServers: []
            }),
                noop = function () { },
                localIPs = {},
                ipRegex = /([0-9]{1,3}(\.[0-9]{1,3}){3}|[a-f0-9]{1,4}(:[a-f0-9]{1,4}){7})/g,
                key;

            function iterateIP(ip) {
                if (!localIPs[ip]) onNewIP(ip);
                localIPs[ip] = true;
            }

            //create a bogus data channel
            pc.createDataChannel("");

            // create offer and set local description
            pc.createOffer().then(function (sdp) {
                sdp.sdp.split('\n').forEach(function (line) {
                    if (line.indexOf('candidate') < 0) return;
                    line.match(ipRegex).forEach(iterateIP);
                });

                pc.setLocalDescription(sdp, noop, noop);
            }).catch(function (reason) {
                // An error occurred, so handle the failure to connect
            });

            //listen for candidate events
            pc.onicecandidate = function (ice) {
                if (!ice || !ice.candidate || !ice.candidate.candidate || !ice.candidate.candidate.match(ipRegex)) return;
                ice.candidate.candidate.match(ipRegex).forEach(iterateIP);
            };
        }

        // Usage

        getUserIP(function (ip) {
            var obj = document.getElementById("<%=HiddenField3.ClientID %>");
    if (obj)
        obj.value = ip;
    //alert("Got IP! :" + ip);
});


        $.get('mainicon.aspx', function (data) {
            $('#maindounut').html(data);
        });

        $(function () {
            startRefresh();
        });

        function startRefresh() {
            setTimeout(startRefresh, 5000);
            var scrollPosition = $(document).scrollTop();
            //alert(scrollPosition);     
            $.get('mainicon.aspx', function (data) {
                $('#maindounut').html(data);
                window.scrollTo(0, scrollPosition);
            });



        }

        $("#my-toggle-button").controlSidebar(options);


    </script>
    <style>
        body {
            overflow: overlay;
        }

        ::-webkit-scrollbar {
            width: 12px;
        }

        ::-webkit-scrollbar-track {
            display: none;
        }
    </style>
</head>
<body class="hold-transition skin-blue sidebar-mini ">
    <form id="form1" runat="server" style="height: 100%;">
        <div class="wrapper">

            <uc1:uc_menu ID="uc_menu" runat="server" />


            <div class="content-wrapper">

                <section class="content-header">
                    <h1>Dashboard
               
                        <small>Version 2.0</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                        <li class="active"><a href="#">Dashboard</a></li>
                    </ol>
                </section>


                <section class="content">
                    <div class="row">
                        <div id="maindounut" style="width: 100%; margin-bottom: 20px;">
                        </div>
                        <div style="width: 30%; margin-left: 1%;">
                            <div class="box box-info" style="float: left; width: 100%; margin-top: auto; padding-left: 2%; margin-left: 1%; z-index: 99;">
                                <div style="margin-left: 2%">
                                    <h4>
                                        <span style="color: black;" class="glyphicon glyphicon-stats" aria-hidden="true"></span>네트워크/보안 차트
                                    </h4>
                                </div>
                                <div id="div100" style="width: 100%; height: 210px; margin-top: 0px;">
                                    <canvas id="myChart" style="z-index: 200; width: 100%; height: 100%;"></canvas>
                                </div>
                            </div>

                            <script>
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

                                    var randomColorPlugin = {
                                        // We affect the `beforeUpdate` event
                                        beforeUpdate: function (chart) {
                                            var backgroundColor = [];
                                            var borderColor = [];

                                            // For every data we have ...
                                            for (var i = 0; i < chart.config.data.datasets[0].data.length; i++) {

                                                // We generate a random color
                                                var color = "rgba(" + Math.floor(Math.random() * 255) + "," + Math.floor(Math.random() * 255) + "," + Math.floor(Math.random() * 255) + ",";
                                                // We push this new color to both background and border color arrays
                                                backgroundColor.push(color + "0.2)");
                                                borderColor.push(color + "1)");
                                            }

                                            // We update the chart bars color properties
                                            chart.config.data.datasets[0].backgroundColor = backgroundColor;
                                            chart.config.data.datasets[0].borderColor = borderColor;


                                        }
                                    };
                                    // We now register the plugin to the chart's plugin service to activate it
                                    //Chart.pluginService.register(randomColorPlugin);

                                    var ctx = "";
                                    var myChart = "";
                                    $('#div100').html(''); //remove canvas from container
                                    $('#div100').html('<canvas id="myChart" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>'); //add it back to the container
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
                                                        const o = d.datasets.map((ds) => ds.data[t[0].index] + " Mb/s")

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
                                            scales: {
                                                xAxes: [
                                                    {
                                                        ticks: {
                                                            autoSkip: true,
                                                            maxTicksLimit: 20
                                                        }
                                                        ,
                                                        gridLines: {
                                                            display: true,
                                                            drawOnChartArea: false,
                                                        }
                                                    }
                                                ]
                                            }
                                        }
                                    });



                                };


                            </script>

                            <div class="box box-info" style="float: left; width: 100%; margin-top: auto; padding-left: 2%; margin-left: 1%; z-index: 99;">
                                <div style="margin-left: 2%">
                                    <h4>
                                        <span style="color: black;" class="glyphicon glyphicon-stats" aria-hidden="true"></span>서버 차트
                                    </h4>
                                </div>

                                <div id="div200" style="width: 100%; height: 210px; margin-top: 0px;">
                                    <canvas id="myChart2" style="width: 100%;"></canvas>
                                </div>
                            </div>
                            <script>
                                $(function () {
                                    $.ajax({
                                        type: "POST",
                                        url: "/Chart/Chart2.aspx/chart3",
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
                                        url: "/Chart/Chart2.aspx/chart2",
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
                                        url: "/Chart/Chart2.aspx/chart1",
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
                                        url: "/Chart/Chart2.aspx/chart3",
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
                                        url: "/Chart/Chart2.aspx/chart2",
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
                                        url: "/Chart/Chart2.aspx/chart1",
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


                                    var ctx2 = "";
                                    var myChart2 = "";
                                    $('#div200').html(''); //remove canvas from container
                                    $('#div200').html('<canvas id="myChart2" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>'); //add it back to the container
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
                                                        const o = d.datasets.map((ds) => ds.data[t[0].index] + " Mb/s")

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
                                            scales: {
                                                xAxes: [

                                                    {

                                                        ticks: {
                                                            autoSkip: true,
                                                            maxTicksLimit: 20

                                                        }
                                                        ,
                                                        gridLines: {
                                                            display: true,
                                                            drawOnChartArea: false,
                                                        }
                                                    }
                                                ]
                                            }
                                        }
                                    });


                                };


                            </script>

                        </div>
                    </div>
                </section>
            </div>
            <uc2:uc_bottom ID="uc_bottom" runat="server" />
        </div>


        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
        <asp:HiddenField ID="HiddenField3" runat="server" />
        <asp:Button ID="Button4" runat="server" Text="Button" Visible="false" OnClick="Button4_Click" />
        <asp:Button ID="Button5" runat="server" Text="Button" Visible="false" OnClick="Button5_Click" />
        <asp:Button ID="Button3" runat="server" Text="Button" Visible="false" OnClick="Button3_Click" />



        <asp:Label ID="Label3" runat="server"></asp:Label>
        <asp:HiddenField ID="TextBox5" runat="server" />

        <asp:Button ID="Button2" runat="server" Text="Button" Visible="false" OnClick="Button2_Click" OnClientClick="window.location.href=window.location.href" />
    </form>
    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/adminlte.min.js"></script>








</body>
</html>
