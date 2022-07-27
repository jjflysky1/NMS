<%@ Page MaintainScrollPositionOnPostback="true" EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="main7.aspx.cs" Inherits="WebApplication1.main7" %>

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



        function go(No) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;

       <%= Page.GetPostBackEventReference(Button2) %>

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





    //$(function () {
    //    $('.left').mouseenter(function () {
    //        $('#inner1').css({ visibility: 'hidden' })

    //    })
    //    $('.left').mouseleave(function () {
    //        $('.item').css({ visibility: 'hidden' }).fadeOut(100)

    //    })
    //})


    </script>
    <style>
        .progress {
            height: 12px;
            background: #ebedef;
            border-radius: 32px;
            box-shadow: none
        }

        .progress-bar {
            line-height: 12px;
            background: #1abc9c;
            box-shadow: none
        }

        .progress-bar-success {
            background-color: #2ecc71
        }

        .progress-bar-warning {
            background-color: #f1c40f
        }

        .progress-bar-danger {
            background-color: #e74c3c
        }

        .progress-bar-info {
            background-color: #3498db
        }
    </style>
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

        body {
            background-image: url("img/enterprise.jpg");
            /*background-color:#1e1b1e;*/
            /*            background: linear-gradient(to top, #2f323a 0%, #2f323a 100%);*/
            background-repeat: no-repeat;
            background-size: 100% 100%;
            /* opacity: 0.5;*/
        }

        html {
            height: 100%
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="height: 100%;">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="test">
            <%--  <uc1:uc_menu ID="uc_menu" runat="server" />--%>


            <div>

                <%--     <section class="content-header">
              <h1>
                Dashboard
                <small>Version 2.0</small>
              </h1>
              <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                <li class="active"><a href="#">Dashboard</a></li>
              </ol>
            </section>--%>


                <section class="content">
                    <div class="row">
                        <table style="width: 100%; margin-top: 10px;">
                            <tr>
                                <td>
                                    <div class="box-header " style="width: 100%; margin-left: 10px; margin-bottom: -50px; margin-top: 20px; z-index: 99;">

                                        <div style="float: right; margin-top: 0px; margin-right: 30px; margin-bottom: -20px;">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Timer ID="Timer1" runat="server" Interval="5700"></asp:Timer>
                                                    <%--<span style=" color:white; " class="glyphicon glyphicon-time" aria-hidden="true"></span>   <font size="3"> <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></font>--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                                    </div>

                                    <div style="margin-left: 30px; margin-bottom: 0px; margin-top: 0px; width: 97.5%; height: 200px;">
                                        <div style="margin-left: 1%">
                                            <h4>
                                                <span style="color: #d4d4d4;" class="glyphicon glyphicon-hdd" aria-hidden="true"></span><<font color="#d4d4d4"> 장비 수량</font>
                                            </h4>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <asp:Table ID="TBLLIST0" BackColor="Transparent" runat="server" CssClass="table no-border" Width="100%"></asp:Table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                    <%--<div id="allchart" runat="server" style="margin-left:30px; margin-top:30px; width:98.5%; margin-right:0; vertical-align:middle; "></div>--%>
                                </td>
                            </tr>
                        </table>
                        <%--  </div>
      </div>--%>
                        <div style="width: 100%; height: 100%; margin-top: -20px;">
                            <div style="float: left; width: 20%; margin-left: 0px; height: 100%; vertical-align: middle;">
                                <div class="card card0 border" style="margin-left: 30px; height: 260px; margin-left: 3%; background-color: rgba(0, 0, 0, 0.3);">
                                    <div style="margin-left: 2%">
                                        <h4>
                                            <span style="color: #d4d4d4;" class="glyphicon glyphicon-tasks" aria-hidden="true"></span><font color="#d4d4d4">네트워크/보안 자원 List</font>
                                        </h4>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Table ID="TBLLIST3_1" runat="server" CssClass="table no-border" Width="100%"></asp:Table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <%--    <div class="" style="width:100%; text-align:center; margin-top:-30px; margin-left:20px; margin-bottom:-40px;">
                      <section class="content-header">
                      <font color="white">
                          <h3>
                      Secure State
                      </h3>
                          </font>
                     </section>
                </div>
                <br /><br />
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                          <div class="cssload-loader"  runat="server" id="inner1" style="  margin-left:20px;">
	<div class="cssload-inner cssload-one" runat="server" id="inner1_1"></div>
	<div class="cssload-inner cssload-two" runat="server" id="inner1_2"></div>
	<div class="cssload-inner cssload-three" runat="server" id="inner1_3"></div>
</div>
                            <div class="cssload-loader1"  runat="server" id="Div3" style=" margin-top:-62px; margin-left:20px;">
	<div class="cssload-inner1 cssload-one" runat="server" id="Div4"></div>
	<div class="cssload-inner1 cssload-two" runat="server" id="Div5"></div>
	<div class="cssload-inner1 cssload-three" runat="server" id="Div6"></div>
</div>
                    </ContentTemplate>
                </asp:UpdatePanel>--%>



                                <div class="card card0 border" style="margin-left: 3%; margin-top: 5px; background-color: rgba(0, 0, 0, 0.3);">
                                    <div style="margin-left: 2%">
                                        <h4>
                                            <span style="color: #d4d4d4;" class="glyphicon glyphicon-stats" aria-hidden="true"></span><font color="#d4d4d4">네트워크/보안 차트</font>
                                        </h4>
                                    </div>
                                    <div id="div1" style="width: 100%; height: 210px; margin-top: 0px;">
                                        <canvas id="myChart" style="z-index: 200; width: 100%; height: 100%; color: #d4d4d4;"></canvas>
                                    </div>
                                </div>
                                <%--<script src='../Scripts/chart.min.js'></script>--%>
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
                                                            const o = d.datasets.map((ds) => ds.data[t[0].index] + " MB/s")

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


                                </script>


                                <div class="card card0 border" style="margin-left: 3%; margin-top: 5px; background-color: rgba(0, 0, 0, 0.3);">
                                    <div style="margin-left: 2%;">
                                        <h4>
                                            <span style="transform: rotate(180deg); color: #d4d4d4;" class="glyphicon glyphicon-tag" aria-hidden="true"></span><font color="#d4d4d4">네트워크/보안 다운 장비</font>
                                        </h4>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Table ID="TBLLIST2" runat="server" CssClass="table no-border" Width="100%"></asp:Table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>


                            </div>
                            </div>


                            <style>
                                #line {
                                    position: absolute;
                                    width: 2px;
                                    margin-top: -1px;
                                    background-color: red;
                                }
                            </style>
                            <script>
                                function adjustLine(from, to, line) {

                                    var fT = from.offsetTop + from.offsetHeight / 2;
                                    var tT = to.offsetTop + to.offsetHeight / 2;
                                    var fL = from.offsetLeft + from.offsetWidth / 2;
                                    var tL = to.offsetLeft + to.offsetWidth / 2;

                                    var CA = Math.abs(tT - fT);
                                    var CO = Math.abs(tL - fL);
                                    var H = Math.sqrt(CA * CA + CO * CO);
                                    var ANG = 180 / Math.PI * Math.acos(CA / H);

                                    if (tT > fT) {
                                        var top = (tT - fT) / 2 + fT;
                                    } else {
                                        var top = (fT - tT) / 2 + tT;
                                    }
                                    if (tL > fL) {
                                        var left = (tL - fL) / 2 + fL;
                                    } else {
                                        var left = (fL - tL) / 2 + tL;
                                    }

                                    if ((fT < tT && fL < tL) || (tT < fT && tL < fL) || (fT > tT && fL > tL) || (tT > fT && tL > fL)) {
                                        ANG *= -1;
                                    }
                                    top -= H / 2;

                                    line.style["-webkit-transform"] = 'rotate(' + ANG + 'deg)';
                                    line.style["-moz-transform"] = 'rotate(' + ANG + 'deg)';
                                    line.style["-ms-transform"] = 'rotate(' + ANG + 'deg)';
                                    line.style["-o-transform"] = 'rotate(' + ANG + 'deg)';
                                    line.style["-transform"] = 'rotate(' + ANG + 'deg)';
                                    line.style.top = top + 'px';
                                    line.style.left = left + 'px';
                                    line.style.height = H + 'px';
                                }
                                adjustLine(
                                    document.getElementById('div100'),
                                    document.getElementById('div101'),
                                    document.getElementById('line')
                                );

                            </script>



                            <%--   <div id="div100" style="position:absolute; margin-left:47%; ">
                <img src="Img/browser-g33732ce31_1280.png" width="100px" />
            </div>
            <div div="div101" style="position:absolute; margin-left:47%; margin-top:20%; ">
                <img src="switch.png" width="100px" />
            </div>--%>

                            <%--  <div id="div200" runat="server" class="card card0 border" style=" height:625px; width:59%; position:absolute; margin-left:20.8%; padding-left:20px; margin-top:auto;  z-index:99; background-color:rgba(0, 0, 0, 0.3); ">
             <div>
                      <h4>
                        <span style=" color:#d4d4d4;" class="glyphicon glyphicon-tasks" aria-hidden="true"></span><font color="#d4d4d4"> 메인 장비</font>
                      </h4>
                </div>
                </div>
                            --%>


                            <div style="width: 59%; height: 820px; position: absolute; margin-left: 20.8%; padding-left: 0px; background-color: rgba(0, 0, 0, 0.3);">
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                <div class="" id="div3" runat="server" style="float:left; width: 100%;  margin-top: auto;  margin-left: 3%; z-index: 99;">
                                 
                                    <div class="box-header" style="margin-left: 0%;">
                                        <i style="color: #d4d4d4;" class="glyphicon glyphicon-tasks"></i>
                                        <h4 class="box-title"><font color="#d4d4d4">메인 장비</font></h4>
                                    </div>
                                </div>
                                         </ContentTemplate>
                                    </asp:UpdatePanel>
                            </div>


                            <div style="padding-left: 30px; float: right; width: 20%; height: auto; margin-right: 1%; margin-bottom: 40px; vertical-align: middle;">
                                <div class="card card0 border" style="height: 260px; background-color: rgba(0, 0, 0, 0.3);">
                                    <div style="margin-left: 2%">
                                        <h4>
                                            <span style="color: #d4d4d4;" class="glyphicon glyphicon-tasks" aria-hidden="true"></span><font color="#d4d4d4">서버 자원 List </font>
                                        </h4>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <asp:Table ID="TBLLIST3" runat="server" CssClass="table no-border " Width="100%"></asp:Table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                
                                <%--<div class="" style="width:100%; text-align:center; margin-top:-30px;  margin-bottom:-40px; ">
                      <section class="content-header">
                      <font color="white">
                          <h3>
                      Server State
                      </h3>
                          </font>
                     </section>
                </div>
                <br /><br />
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                            <div class="cssload-loader"  runat="server" id="inner2" style=" ">
	<div class="cssload-inner cssload-one" runat="server" id="inner2_1"></div>
	<div class="cssload-inner cssload-two" runat="server" id="inner2_2"></div>
	<div class="cssload-inner cssload-three" runat="server" id="inner2_3"></div>
                                </div>
                                     <div class="cssload-loader1"  runat="server" id="Div7" style="  margin-top:-62px;">
	<div class="cssload-inner1 cssload-one" runat="server" id="Div8"></div>
	<div class="cssload-inner1 cssload-two" runat="server" id="Div9"></div>
	<div class="cssload-inner1 cssload-three" runat="server" id="Div10"></div>
                                         </div>
                    </ContentTemplate>
                </asp:UpdatePanel>--%>





                                <%--  <div class="cssload-thecube" runat="server" id="cube2" style="float:left;  margin-left:50%;">
	<div class="cssload-cube cssload-c1"></div>
	<div class="cssload-cube cssload-c2"></div>
	<div class="cssload-cube cssload-c4"></div>
	<div class="cssload-cube cssload-c3"></div>
</div>--%>


                                <div class="card card0 border" style="margin-top: 5px; background-color: rgba(0, 0, 0, 0.3);">
                                    <div style="margin-left: 2%">
                                        <h4>
                                            <span style="color: #d4d4d4;" class="glyphicon glyphicon-stats" aria-hidden="true"></span><font color="#d4d4d4">서버 차트 </font>
                                        </h4>
                                    </div>

                                    <div id="div2" style="width: 100%; height: 210px; margin-top: 0px;">
                                        <canvas id="myChart2" style="width: 100%;"></canvas>
                                    </div>
                                </div>
                                <script>
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
                                                            const o = d.datasets.map((ds) => ds.data[t[0].index] + " MB/s")

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

                                </script>
                                        <div class="card card0 border" style="margin-top: 5px; background-color: rgba(0, 0, 0, 0.3);">
                                            <div style="margin-left: 2%;">
                                                <h4>
                                                    <span style="transform: rotate(180deg); color: #d4d4d4;" class="glyphicon glyphicon-tag" aria-hidden="true"></span><font color="#d4d4d4">서버 다운 장비</font>
                                                </h4>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:Table ID="TBLLIST" runat="server" CssClass="table no-border " Width="100%"></asp:Table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>

                            </div >
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div id="div4" class="card card0 border" runat="server" style="float: left; width: 98.3%; margin-left: 0.7%; z-index: 99; margin-top: 9%; background-color: rgba(0, 0, 0, 0.3);">
                                                    <div style="margin-left: 0.5%;">
                                                        <h4>
                                                            <span style="transform: rotate(180deg); color: #d4d4d4;" class="glyphicon glyphicon-tag" aria-hidden="true"></span><font color="#d4d4d4">이벤트 로그</font>
                                                        </h4>
                                                        <asp:Table ID="TBLLIST1" runat="server" CssClass="table no-border " Width="100%"></asp:Table>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>


                        </div >

                    </div >
                </section >
            </div >

            <%-- <uc2:uc_bottom ID="uc_bottom" runat="server" />     --%>
        </div >
            

        <style>
            .grad {
                /*background-image:linear-gradient(356deg, rgba(2,0,36,1) 0%, rgba(9,9,121,1) 55%, rgba(0,212,255,1) 100%)}*/
                /*background: linear-gradient(to right, #0f2027, #203a43, #2c5364);*/
                background: rgb(25,34,55);
            }

            .grad2 {
                /*background: radial-gradient(circle, rgba(40,47,77,1) 0%, rgba(31,55,86,1) 100%);*/
            }

            .card {
                position: relative;
                display: -ms-flexbox;
                display: flex;
                -ms-flex-direction: column;
                flex-direction: column;
                min-width: 0;
                word-wrap: break-word;
                background-color: #fff;
                background-clip: border-box;
                border: 1px solid rgba(0,0,0,.125);
                border-radius: .25rem
            }

                .card > hr {
                    margin-right: 0;
                    margin-left: 0
                }

                .card > .list-group:first-child .list-group-item:first-child {
                    border-top-left-radius: .25rem;
                    border-top-right-radius: .25rem
                }

                .card > .list-group:last-child .list-group-item:last-child {
                    border-bottom-right-radius: .25rem;
                    border-bottom-left-radius: .25rem
                }

            .card-body {
                -ms-flex: 1 1 auto;
                flex: 1 1 auto;
                padding: 1.25rem
            }

            .card-title {
                margin-bottom: .75rem
            }

            .card-subtitle {
                margin-top: -.375rem;
                margin-bottom: 0
            }

            .card-text:last-child {
                margin-bottom: 0
            }

            .card-link:hover {
                text-decoration: none
            }

            .card-link + .card-link {
                margin-left: 1.25rem
            }

            .card-header {
                padding: .75rem 1.25rem;
                margin-bottom: 0;
                background-color: rgba(0,0,0,.03);
                border-bottom: 1px solid rgba(0,0,0,.125)
            }

                .card-header:first-child {
                    border-radius: calc(.25rem - 1px) calc(.25rem - 1px) 0 0
                }

                .card-header + .list-group .list-group-item:first-child {
                    border-top: 0
                }

            .card-footer {
                padding: .75rem 1.25rem;
                background-color: rgba(0,0,0,.03);
                border-top: 1px solid rgba(0,0,0,.125)
            }

                .card-footer:last-child {
                    border-radius: 0 0 calc(.25rem - 1px) calc(.25rem - 1px)
                }

            .card-header-tabs {
                margin-right: -.625rem;
                margin-bottom: -.75rem;
                margin-left: -.625rem;
                border-bottom: 0
            }

            .card-header-pills {
                margin-right: -.625rem;
                margin-left: -.625rem
            }

            .card-img-overlay {
                position: absolute;
                top: 0;
                right: 0;
                bottom: 0;
                left: 0;
                padding: 1.25rem
            }

            .card-img {
                width: 100%;
                border-radius: calc(.25rem - 1px)
            }

            .card-img-top {
                width: 100%;
                border-top-left-radius: calc(.25rem - 1px);
                border-top-right-radius: calc(.25rem - 1px)
            }

            .card-img-bottom {
                width: 100%;
                border-bottom-right-radius: calc(.25rem - 1px);
                border-bottom-left-radius: calc(.25rem - 1px)
            }

            .card-deck {
                display: -ms-flexbox;
                display: flex;
                -ms-flex-direction: column;
                flex-direction: column
            }

                .card-deck .card {
                    margin-bottom: 15px
                }

            @media (min-width:576px) {
                .card-deck {
                    -ms-flex-flow: row wrap;
                    flex-flow: row wrap;
                    margin-right: -15px;
                    margin-left: -15px
                }

                    .card-deck .card {
                        display: -ms-flexbox;
                        display: flex;
                        -ms-flex: 1 0 0%;
                        flex: 1 0 0%;
                        -ms-flex-direction: column;
                        flex-direction: column;
                        margin-right: 15px;
                        margin-bottom: 0;
                        margin-left: 15px
                    }
            }

            .card-group {
                display: -ms-flexbox;
                display: flex;
                -ms-flex-direction: column;
                flex-direction: column
            }

                .card-group > .card {
                    margin-bottom: 15px
                }

            @media (min-width:576px) {
                .card-group {
                    -ms-flex-flow: row wrap;
                    flex-flow: row wrap
                }

                    .card-group > .card {
                        -ms-flex: 1 0 0%;
                        flex: 1 0 0%;
                        margin-bottom: 0
                    }

                        .card-group > .card + .card {
                            margin-left: 0;
                            border-left: 0
                        }

                        .card-group > .card:not(:last-child) {
                            border-top-right-radius: 0;
                            border-bottom-right-radius: 0
                        }

                            .card-group > .card:not(:last-child) .card-header, .card-group > .card:not(:last-child) .card-img-top {
                                border-top-right-radius: 0
                            }

                            .card-group > .card:not(:last-child) .card-footer, .card-group > .card:not(:last-child) .card-img-bottom {
                                border-bottom-right-radius: 0
                            }

                        .card-group > .card:not(:first-child) {
                            border-top-left-radius: 0;
                            border-bottom-left-radius: 0
                        }

                            .card-group > .card:not(:first-child) .card-header, .card-group > .card:not(:first-child) .card-img-top {
                                border-top-left-radius: 0
                            }

                            .card-group > .card:not(:first-child) .card-footer, .card-group > .card:not(:first-child) .card-img-bottom {
                                border-bottom-left-radius: 0
                            }
            }

            .card-columns .card {
                margin-bottom: .75rem
            }

            @media (min-width:576px) {
                .card-columns {
                    -webkit-column-count: 3;
                    -moz-column-count: 3;
                    column-count: 3;
                    -webkit-column-gap: 1.25rem;
                    -moz-column-gap: 1.25rem;
                    column-gap: 1.25rem;
                    orphans: 1;
                    widows: 1
                }

                    .card-columns .card {
                        display: inline-block;
                        width: 100%
                    }
            }

            .accordion > .card {
                overflow: hidden
            }

                .accordion > .card:not(:first-of-type) .card-header:first-child {
                    border-radius: 0
                }

                .accordion > .card:not(:first-of-type):not(:last-of-type) {
                    border-bottom: 0;
                    border-radius: 0
                }

                .accordion > .card:first-of-type {
                    border-bottom: 0;
                    border-bottom-right-radius: 0;
                    border-bottom-left-radius: 0
                }

                .accordion > .card:last-of-type {
                    border-top-left-radius: 0;
                    border-top-right-radius: 0
                }

                .accordion > .card .card-header {
                    margin-bottom: -1px
                }
        </style>

        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
        <asp:Button ID="Button2" runat="server" Text="Button" Visible="false" OnClick="Button2_Click" />
        <asp:Button ID="Button4" runat="server" Text="Button" Visible="false" OnClick="Button4_Click" />
        <asp:Button ID="Button5" runat="server" Text="Button" Visible="false" OnClick="Button5_Click" />
        <asp:Button ID="Button3" runat="server" Text="Button" Visible="false" OnClick="Button3_Click" />

        <asp:Button ID="Button1" Visible="false" runat="server" Text="Button" OnClick="Button1_Click" />

        <asp:Label ID="Label3" runat="server"></asp:Label>
        <asp:HiddenField ID="TextBox5" runat="server" />
    </form >
                                        <script src="Scripts/jquery.min.js"></script>
                                <script src="Scripts/adminlte.min.js"></script>
</body>
</html>
