<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="main4.aspx.cs" Inherits="WebApplication1.main4" %>

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
    <%--<link rel="stylesheet" href="Content/bootstrap.min2.css" />--%>
    <link rel="stylesheet" href="Content/sb-admin-2.min.css?after" />



    <link rel="stylesheet" href="../Scripts/datepicker3.min.css" />


    <%--<link rel="stylesheet" href="Content/style.css" />--%>
    <script src="Scripts/jquery-3.2.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Content/JavaScript.js"></script>
    <script src="Scripts/AdminLTE.css"></script>

    <%--<script src="Scripts/highcharts.js"></script>
    <script src="Scripts/highcharts-3d.js"></script>
    <script src="Scripts/exporting.js"></script>
    <script src="Scripts/export-data.js"></script>--%>
    <script src="Scripts/jschart.js"></script>
    <%--<link href="Scripts/Allcss.css" rel="stylesheet" />--%>
    <script src="Scripts/html2canvas/html2canvas.js"></script>
    <script src="Scripts/html2canvas/html2canvas.min.js"></script>


    <title></title>
    <script type="text/javascript">




        function go(No, category) {

            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;
        var obj = document.getElementById("<%=HiddenField2.ClientID %>");
        if (obj)
            obj.value = category;


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


 



    //$.get('mainicon.aspx', function (data) {
    //    $('#maindounut').html(data);
    //});

    //$(function() {
    ////startRefresh();
    //});

    //function startRefresh() {
    //    setTimeout(startRefresh, 5000);  
    //var scrollPosition = $(document).scrollTop();
    ////alert(scrollPosition);     
    //    $.get('mainicon.aspx', function (data) {
    //        $('#maindounut').html(data);
    //        window.scrollTo(0, scrollPosition); 
    //});



    //    }

      //  $("#my-toggle-button").controlSidebar(options);

      
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
<body class="hold-transition skin-black sidebar-mini ">
    <form id="form1" runat="server" style="height: 100%; ">
        <div class="wrapper" >
            
            <uc1:uc_menu ID="uc_menu" runat="server" />
            <div class="content-wrapper" style="background:radial-gradient(circle, rgba(40,47,77,1) 0%, rgba(31,55,86,1) 100%);">
                <section class="content-header" style="margin-top: -15px;">
                    <font color="white"><h1>Dashboard
                        <small>Version 2.0</small>
                    </h1></font>
                    <asp:Button  ID="Button12" runat="server" Text="Putty 접속" CssClass="btn btn-link btn-sm navbar-left" OnClick="Button12_Click"  />
                    
                  <%--  <ol class="breadcrumb" >
                        <li><font color="white"><i class="fa fa-dashboard"></i>Home</li>
                        <li class="active">Dashboard</font></li>
                    </ol>
                        --%>
                </section>
                 
                <div style="float: right; margin-top: 0px; margin-right: 15px; margin-bottom: 0px;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Timer ID="Timer1" runat="server" Interval="5700"></asp:Timer>
                            <span style="color: white;" class="glyphicon glyphicon-time" aria-hidden="true"></span><font color="white" size="3"> <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></font>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <br />

                <section class="content">
                    <div class="row">
                        <%--<div id="maindounut" style="width:100%; margin-bottom:20px;  ">      
               
        </div>  --%>
                         <style>
                             .grad {
                                 /*background-image:linear-gradient(356deg, rgba(2,0,36,1) 0%, rgba(9,9,121,1) 55%, rgba(0,212,255,1) 100%)}*/
                                 //background: linear-gradient(to right, #0f2027, #203a43, #2c5364);
                                 background: rgb(25,34,55);
                             }
                             .grad2{
                                 //background: radial-gradient(circle, rgba(40,47,77,1) 0%, rgba(31,55,86,1) 100%);
                             }
                            </style>
                        <div style="width: 100%; margin-left: 1%; ">
                            <div id="div1"   class="card shadow  grad" runat="server" style=" float: left;  width: 63%; margin-left: 1%; margin-bottom: 1%; margin-top: 0px; z-index: 99;">
                                <div class="grad2">
                             <%--   <font color="white">
                                    <h5>&nbsp;   장비
                                    </h5>
                                </font>--%>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:Table ID="TBLLIST2" runat="server" Width="100%"></asp:Table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                    </div>
                            </div>
                           


                            <%--     <div class="card  shadow " style=" float:left;  width:31%;  height:100px; margin-left:1%; margin-bottom:1%;  z-index:99; ">
                         <div class="border-left-primary ">
                            <h5>
                              &nbsp;   <i class="glyphicon glyphicon-th"></i> 서비스 문제
                            </h5>
                            </div>
                                  <div class="h5 mb-0 font-weight-bold text-gray-800">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></div>
                    <div class="col-auto">
                      <i class="fas fa-dollar-sign fa-2x text-gray-300"></i>
                    </div>
              </div>--%>


                            <div id="div2" class="card shadow grad" style="float: left; width: 31%; margin-left: 1%; margin-bottom: 1%; z-index: 99;">
                              <div class="grad2">
                                <%--   <center><font color="white">
                                    <h5>&nbsp;   서비스 이상유무
                                    </h5>
                                </font></center>--%>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:Table ID="TBLLIST3" runat="server" Width="100%"></asp:Table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                </div>
                            </div>
                            <script>
                            $(document).ready(function(){
                               $("#btn1").click(function () {
                                    var x = $('.content').width()-30;
                                    $("#box1").animate({ height: "100%;", width:x});
                                    $("#box1").css('z-index', 100);
                                    $("#box1").css('position', "absolute");
                                    $("#btn100").css('visibility', "visible");
                                });
                                $("#btn1-1").click(function(){
                                    $("#box1").animate({ height: "100%;", width: "31%" });
                                    $("#box1").css('z-index', 99);
                                    $("#box1").css('position', "");
                                    $("#btn100").css('visibility', "hidden");
                                });
                                $("#btn2").click(function () {
                                     var x = $('.content').width()-30;
                                     $("#box2").animate({ height: "100%;", width: x });
                                     $("#box2").css('z-index', 100);
                                     $("#box2").css('position', "absolute");
                                     $("#btn101").css('visibility', "visible");
                                });
                                $("#btn2-1").click(function(){
                                    $("#box2").animate({ height: "100%;", width: "31%" });
                                    $("#box2").css('z-index', 99);
                                    $("#box2").css('position', "");
                                    $("#btn101").css('visibility', "hidden");
                                });
                                $("#btn3").click(function () {
                                     var x = $('.content').width()-30;
                                     $("#box3").animate({ height: "100%;", width: x });
                                     $("#box3").css('z-index', 100);
                                     $("#box3").css('position', "absolute");
                                     $("#btn102").css('visibility', "visible");
                                });
                                $("#btn3-1").click(function(){
                                    $("#box3").animate({ height: "100%;", width: "31%" });
                                    $("#box3").css('z-index', 99);
                                    $("#box3").css('position', "");
                                    $("#btn102").css('visibility', "hidden");
                                });

                                $("#btn100").click(function () {
                                    html2canvas(div100).then(function (canvas) {
                                        var myImage = canvas.toDataURL();
                                        downloadURI(myImage, "cap.png");
                                    });
                                });
                                $("#btn101").click(function () {
                                    html2canvas(div101).then(function (canvas) {
                                        var myImage = canvas.toDataURL();
                                        downloadURI(myImage, "cap.png");
                                    });
                                });
                                $("#btn102").click(function () {
                                    html2canvas(div102).then(function (canvas) {
                                        var myImage = canvas.toDataURL();
                                        downloadURI(myImage, "cap.png");
                                    });
                                });
                                $("#div100").click(function () {
                                    $("#box1").animate({ height: "100%;", width: "31%" });
                                    $("#box1").css('z-index', 99);
                                    $("#box1").css('position', "");
                                    $("#btn100").css('visibility', "hidden");
                                });
                                $("#div101").click(function () {
                                    $("#box2").animate({ height: "100%;", width: "31%" });
                                    $("#box2").css('z-index', 99);
                                    $("#box2").css('position', "");
                                    $("#btn101").css('visibility', "hidden");
                                });
                                $("#div102").click(function () {
                                    $("#box3").animate({ height: "100%;", width: "31%" });
                                    $("#box3").css('z-index', 99);
                                    $("#box3").css('position', "");
                                    $("#btn102").css('visibility', "hidden");
                                });
                                

                                    function downloadURI(uri, name){
                                        var link = document.createElement("a");
	                                    link.download = name;
	                                    link.href = uri;
	                                    document.body.appendChild(link);
	                                    link.click();
                                    }
                            });
                            </script>
                            <div class="card shadow grad" id="box1"  style="float:left; text-align:right;   width: 31%; margin-top: auto; margin-left: 1%; margin-bottom: 1%; z-index: 99;">
                                <div style="position:absolute; z-index:10000; float:right; width:100%;" >
                                <button type="button" id="btn100" class="btn btn- btn-xs"  style=" visibility:hidden; font-size:12px; background-color:transparent; color:white; z-index:10000; width:60px; height:20px; margin-top:5px; "><b>이미지다운</b></button>
                                <button type="button" id="btn1" class="btn btn- btn-xs"  style=" font-size:15px; background-color:transparent; color:white; z-index:10000; width:20px; height:20px;"><b>+</b></button>
                                <button type="button" id="btn1-1" class="btn btn- btn-xs"  style=" font-size:15px; background-color:transparent; color:white; z-index:10000; width:20px; height:20px;"><b>-</b></button>
                                </div>
                                <div class="grad2">
                                <div id="div100" style="width:100%;  margin-top: 0px;">
                                    <%--<canvas id="myChart" style="z-index:200;   width:100%;  height: 100%;  " ></canvas>--%>
                                </div>
                                    </div>
                            </div>


                            <div class="card shadow grad" id="box2" style="float: left; text-align:right; width: 31%; margin-top: auto; margin-left: 1%; margin-bottom: 1%; z-index: 99;">
                                <div style="position:absolute; z-index:10000; float:right; width:100%;" >
                                    <button type="button" id="btn101" class="btn btn- btn-xs"  style=" visibility:hidden; font-size:12px; background-color:transparent; color:white; z-index:10000; width:60px; height:20px; margin-top:5px;"><b>이미지다운</b></button>
                                <button type="button" id="btn2" class="btn btn- btn-xs"  style=" font-size:15px; background-color:transparent; color:white; z-index:10000; width:20px; height:20px;"><b>+</b></button>
                                <button type="button" id="btn2-1" class="btn btn- btn-xs"  style=" font-size:15px; background-color:transparent; color:white; z-index:10000; width:20px; height:20px;"><b>-</b></button>
                                </div>
                                <div class="grad2">
                                <div id="div101" style="width: 100%; margin-top: 0px;">
                                    <%--<canvas id="myChart2" style="z-index:200;   width:100%;  height: 100%;  " ></canvas>--%>
                                </div>
                                    </div>
                            </div>

                            <div class="card shadow grad" id="box3" style="float:left;  text-align:right; width: 31%; margin-top: auto; margin-left: 1%; margin-bottom: 1%; z-index: 99;">
                                <div style="position:absolute; z-index:10000; float:right; width:100%;" >
                                    <button type="button" id="btn102" class="btn btn- btn-xs"  style=" visibility:hidden; font-size:12px; background-color:transparent; color:white; z-index:10000; width:60px; height:20px; margin-top:5px;"><b>이미지다운</b></button>
                                <button type="button" id="btn3" class="btn btn- btn-xs"  style=" font-size:15px; background-color:transparent; color:white; z-index:10000; width:20px; height:20px;"><b>+</b></button>
                                <button type="button" id="btn3-1" class="btn btn- btn-xs"  style=" font-size:15px; background-color:transparent; color:white; z-index:10000; width:20px; height:20px;"><b>-</b></button>
                                </div>
                               <div class="grad2">
                                <div id="div102" style="width: 100%;  margin-top: 0px;">
                                    <%--<canvas id="myChart3" style="z-index:200;   width:100%;  height: 100%;  " ></canvas>--%>
                                </div>
                                   </div>
                            </div>

                            <script>
                                //        if (typeof jQuery != 'undefined') {  
                                //    // jQuery is loaded => print the version
                                //    alert(jQuery.fn.jquery);
                                //}
                                $(function () {
                                    $.ajax({
                                        type: "POST",
                                        url: "/Chart/Chart.aspx/chart3",
                                        data: '{}',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        async: true,
                                        cache: false,
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
                                        async: true,
                                        cache: false,
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
                                        async: true,
                                        cache: false,
                                        success: OnSuccess,
                                        failure: function (response) {
                                            //alert(response.d);
                                        },
                                        error: function (response) {
                                            //alert(response.d);
                                        }
                                    });
                                });


                                function init() {

                                    window.ref = window.setInterval(function () { rewrite(); }, 10000);
                                }
                                function rewrite() {
                                    clearInterval(window.ref);
                                    $.ajax({
                                        type: "POST",
                                        url: "/Chart/Chart.aspx/chart3",
                                        data: '{}',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        async: true,
                                        cache: false,
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
                                        async: true,
                                        cache: false,
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
                                        async: true,
                                        cache: false,
                                        failure: function (response) {
                                            //alert(response.d);
                                        },
                                        error: function (response) {
                                            //alert(response.d);
                                        }
                                    });

                                    init();
                                }
                                init();


                                //setInterval(function () {

                                //},1 * 10 * 1000);



                                var temp_traffic3 = "";
                                var serverip3 = "";
                                var temp_time3 = "";
                                var trafficArray3 = "";
                                var timeArray3 = "";
                                function OnSuccess3(response) {
                                    var customers3 = response.d;
                                    temp_traffic3 = "";
                                    temp_time3 = "";
                                    serverip3 = "";
                                    trafficArray3 = "";
                                    timeArray3 = "";
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

                                }



                                var temp_traffic2 = "";
                                var serverip2 = "";
                                var temp_time2 = "";
                                var trafficArray2 = "";
                                var timeArray2 = "";
                                function OnSuccess2(response) {
                                    var customers2 = response.d;
                                    temp_traffic2 = "";
                                    temp_time2 = "";
                                    serverip2 = "";
                                    trafficArray2 = "";
                                    timeArray2 = "";
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
                                }

                                function OnSuccess(response) {
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



                                    var list = document.getElementById("div100");   // Get the <ul> element with id="myList"
                                    list.removeChild(list.childNodes[0]);
                                    var list2 = document.getElementById("div101");   // Get the <ul> element with id="myList"
                                    list2.removeChild(list2.childNodes[0]);
                                    var list3 = document.getElementById("div102");   // Get the <ul> element with id="myList"
                                    list3.removeChild(list3.childNodes[0]);

                                    //네트워크 보안1
                                    var ctx = "";
                                    var myChart = "";
                                    $('#div100').html(''); //remove canvas from container
                                    $('#myChart').remove(); // this is my <canvas> element
                                    $('#div100').html('<canvas id="myChart" style="z-index:200; position:relative;  width:100%;    "></canvas>'); //add it back to the container
                                    ctx = document.getElementById("myChart").getContext("2d");
                                    var gradientFill = ctx.createLinearGradient(0, 0, 0, 450);
                                    gradientFill.addColorStop(0, 'rgba(22, 183, 250, 0.3)');
                                    gradientFill.addColorStop(0.5, 'rgba(22, 183, 250, 0.1)');
                                    gradientFill.addColorStop(1, 'rgba(22, 183, 250, 0)');
                                    myChart = new Chart(ctx, {
                                        type: "line",
                                        data: {
                                            labels: timeArray,
                                            datasets: [
                                                {
                                                    label: serverip,
                                                    data: trafficArray,
                                                    //backgroundColor: ["rgba(3, 169, 244, 0.4)"],
                                                    backgroundColor: gradientFill,
                                                    borderColor: ["rgba(3, 169, 244, 1)"],
                                                    borderWidth: 1,
                                                    pointRadius: 1,
                                                    pointHoverRadius: 1,
                                                    fill: "start"
                                                }
                                                //,{
                                                //  label: serverip2,
                                                //  data: trafficArray2,
                                                //  backgroundColor: ["rgba(161, 201, 249, .15)"],
                                                //  borderColor: ["rgba(161, 201, 249, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //  }
                                                //,{
                                                //  label: serverip3,
                                                //  data: trafficArray3,
                                                //  backgroundColor: ["rgba(192, 161, 249, .15)"],
                                                //  borderColor: ["rgba(192, 161, 249, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //}
                                            ]
                                        },
                                        options: {
                                            animation: {
                                                duration: 0
                                            },
                                            tooltips: {
                                                intersect: false,
                                                backgroundColor: "rgba(3, 169, 244, 1)",
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
                                                text: "네트워크/보안 차트1",
                                                display: false,
                                                fontColor: "white",
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
                                                    fontColor: "white",
                                                    fontSize: 11
                                                }
                                            },
                                            scales: {
                                                xAxes: [
                                                    {
                                                        ticks: {
                                                            fontColor: "white",
                                                            autoSkip: true,
                                                            maxTicksLimit: 20,
                                                            display: true
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
                                                            fontColor: "white"
                                                        }
                                                    }
                                                ]
                                            }
                                            
                                        }
                                    });




                                    //네트워크 보안2
                                    var ctx2 = "";
                                    var myChart2 = "";
                                    $('#div101').html(''); //remove canvas from container
                                    $('#myChart2').remove(); // this is my <canvas> element
                                    $('#div101').html('<canvas id="myChart2" style="z-index:200; position:relative;  width:100%;  "></canvas>'); //add it back to the container
                                    ctx2 = document.getElementById("myChart2").getContext("2d");
                                    gradientFill = ctx2.createLinearGradient(0, 0, 0, 450);
                                    gradientFill.addColorStop(0, 'rgba(241, 191, 51, 0.3)');
                                    gradientFill.addColorStop(0.5, 'rgba(241, 191, 51, 0.1)');
                                    gradientFill.addColorStop(1, 'rgba(241, 191, 51, 0)');
                                    myChart2 = new Chart(ctx2, {
                                        type: "line",
                                        data: {
                                            labels: timeArray2,
                                            datasets: [
                                                //{
                                                //      label: serverip,
                                                //      data: trafficArray,
                                                //  backgroundColor: ["rgba(113, 88, 203, .15)"],
                                                //  borderColor: ["rgba(113, 88, 203, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //},
                                                {
                                                    label: serverip2,
                                                    data: trafficArray2,
                                                    //backgroundColor: ["rgba(255, 152, 0, 0.4)"],
                                                    backgroundColor: gradientFill,
                                                    borderColor: ["rgba(255, 152, 0, 1)"],
                                                    borderWidth: 1,
                                                    pointRadius: 1,
                                                    pointHoverRadius: 1,
                                                    fill: "start"
                                                }
                                                //,{
                                                //  label: serverip3,
                                                //  data: trafficArray3,
                                                //  backgroundColor: ["rgba(192, 161, 249, .15)"],
                                                //  borderColor: ["rgba(192, 161, 249, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //}
                                            ]
                                        },
                                        options: {
                                            animation: {
                                                duration: 0
                                            },
                                            tooltips: {
                                                intersect: false,
                                                backgroundColor: "rgba(255, 152, 0, 1)",
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
                                                text: "네트워크/보안 차트2",
                                                display: false,
                                                fontColor: "white"
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
                                                    fontColor: "white",
                                                    fontSize: 11
                                                }
                                            },
                                            scales: {
                                                xAxes: [
                                                    {
                                                        ticks: {
                                                            fontColor: "white",
                                                            autoSkip: true,
                                                            maxTicksLimit: 20,
                                                            display: true
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
                                                            fontColor: "white"
                                                        }
                                                    }
                                                ]
                                            }
                                        }
                                    });



                                    //네트워크 보안3
                                    var ctx3 = "";
                                    var myChart3 = "";
                                    $('#div102').html(''); //remove canvas from container
                                    $('#myChart3').remove(); // this is my <canvas> element
                                    $('#div102').html('<canvas id="myChart3" style="z-index:200; position:relative;  width:100%;    "></canvas>'); //add it back to the container
                                    ctx3 = document.getElementById("myChart3").getContext("2d");
                                    gradientFill = ctx3.createLinearGradient(0, 0, 0, 450);
                                    gradientFill.addColorStop(0, 'rgba(61, 210, 48, 0.3)');
                                    gradientFill.addColorStop(0.5, 'rgba(61, 210, 48, 0.1)');
                                    gradientFill.addColorStop(1, 'rgba(61, 210, 48, 0)');
                                    myChart3 = new Chart(ctx3, {
                                        type: "line",
                                        data: {
                                            labels: timeArray3,
                                            datasets: [
                                                //{
                                                //      label: serverip,
                                                //      data: trafficArray,
                                                //  backgroundColor: ["rgba(113, 88, 203, .15)"],
                                                //  borderColor: ["rgba(113, 88, 203, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //}
                                                //,{
                                                //  label: serverip2,
                                                //  data: trafficArray2,
                                                //  backgroundColor: ["rgba(161, 201, 249, .15)"],
                                                //  borderColor: ["rgba(161, 201, 249, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //  }
                                                //,
                                                {
                                                    label: serverip3,
                                                    data: trafficArray3,
                                                    //backgroundColor: ["rgba(29, 233, 182, 0.4)"],
                                                    backgroundColor: gradientFill,
                                                    borderColor: ["rgba(29, 233, 182, 1)"],
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
                                                backgroundColor: "rgba(29, 233, 182, 1)",
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
                                                text: "네트워크/보안 차트3",
                                                display: false,
                                                fontColor: "white"
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
                                                    fontColor: "white",
                                                    fontSize: 11
                                                }
                                            },
                                            scales: {
                                                xAxes: [
                                                    {
                                                        ticks: {
                                                            fontColor: "white",
                                                            autoSkip: true,
                                                            maxTicksLimit: 20,
                                                            display: true
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
                                                            fontColor: "white"
                                                        }
                                                    }
                                                ]
                                            }
                                        }
                                    });



                                };


                            </script>
                
                            <script>
                            $(document).ready(function(){
                                 $("#btn4").click(function () {
                                    var x = $('.content').width()-30;
                                    $("#box4").animate({ height: "100%;", width:x });
                                    $("#box4").css('z-index', 100);
                                    $("#box4").css('position', "absolute");
                                    $("#btn200").css('visibility', "visible");
                                });
                                $("#btn4-1").click(function(){
                                    $("#box4").animate({ height: "100%;", width: "31%" });
                                    $("#box4").css('z-index', 99);
                                    $("#box4").css('position', "");
                                    $("#btn200").css('visibility', "hidden");
                                });
                                $("#btn5").click(function () {
                                     var x = $('.content').width()-30;
                                     $("#box5").animate({ height: "100%;", width: x });
                                     $("#box5").css('z-index', 100);
                                     $("#box5").css('position', "absolute");
                                     $("#btn201").css('visibility', "visible");
                                });
                                $("#btn5-1").click(function(){
                                    $("#box5").animate({ height: "100%;", width: "31%" });
                                    $("#box5").css('z-index', 99);
                                    $("#box5").css('position', "");
                                    $("#btn201").css('visibility', "hidden");
                                });
                                $("#btn6").click(function () {
                                     var x = $('.content').width()-30;
                                     $("#box6").animate({ height: "100%;", width: x });
                                     $("#box6").css('z-index', 100);
                                     $("#box6").css('position', "absolute");
                                     $("#btn202").css('visibility', "visible");
                                });
                                $("#btn6-1").click(function(){
                                    $("#box6").animate({ height: "100%;", width: "31%" });
                                    $("#box6").css('z-index', 99);
                                    $("#box6").css('position', "");
                                    $("#btn202").css('visibility', "hidden");
                                });

                                
                                $("#btn200").click(function () {
                                    html2canvas(div200).then(function (canvas) {
                                        var myImage = canvas.toDataURL();
                                        downloadURI(myImage, "cap.png");
                                    });
                                });
                                $("#btn201").click(function () {
                                    html2canvas(div201).then(function (canvas) {
                                        var myImage = canvas.toDataURL();
                                        downloadURI(myImage, "cap.png");
                                    });
                                });
                                $("#btn202").click(function () {
                                    html2canvas(div202).then(function (canvas) {
                                        var myImage = canvas.toDataURL();
                                        downloadURI(myImage, "cap.png");
                                    });
                                });
                                $("#div200").click(function () {
                                    $("#box4").animate({ height: "100%;", width: "31%" });
                                    $("#box4").css('z-index', 99);
                                    $("#box4").css('position', "");
                                    $("#btn200").css('visibility', "hidden");
                                });
                                $("#div201").click(function () {
                                    $("#box5").animate({ height: "100%;", width: "31%" });
                                    $("#box5").css('z-index', 99);
                                    $("#box5").css('position', "");
                                    $("#btn201").css('visibility', "hidden");
                                });
                                $("#div202").click(function () {
                                    $("#box6").animate({ height: "100%;", width: "31%" });
                                    $("#box6").css('z-index', 99);
                                    $("#box6").css('position', "");
                                    $("#btn202").css('visibility', "hidden");
                                });
                                
                            });
                            </script>    

                            <div class="card shadow grad" id="box4" style="float: left; width: 31%; text-align:right; margin-top: auto; margin-left: 1%; z-index: 99; margin-bottom: 1%;">
                              <div style="position:absolute; z-index:10000; float:right; width:100%;" >
                                <button type="button" id="btn200" class="btn btn- btn-xs"  style=" visibility:hidden; font-size:12px; background-color:transparent; color:white; z-index:10000; width:60px; height:20px; margin-top:5px;"><b>이미지다운</b></button>
                                <button type="button" id="btn4" class="btn btn- btn-xs"  style=" font-size:15px; background-color:transparent; color:white; z-index:10000; width:20px; height:20px;"><b>+</b></button>
                                <button type="button" id="btn4-1" class="btn btn- btn-xs"  style=" font-size:15px; background-color:transparent; color:white; z-index:10000; width:20px; height:20px;"><b>-</b></button>
                                </div>
                                <div class="grad2">
                                <div id="div200" style="width: 100%;  margin-top: 0px;">
                                    <%--<canvas id="myChart4" style="  width:100%; "></canvas>--%>
                                </div>
                               </div>
                            </div>

                            <div class="card shadow grad" id="box5" style="float: left; width: 31%;  text-align:right;margin-top: auto; margin-left: 1%; z-index: 99; margin-bottom: 1%;">
                            <div style="position:absolute; z-index:10000; float:right; width:100%;" >
                                <button type="button" id="btn201" class="btn btn- btn-xs"  style=" visibility:hidden; font-size:12px; background-color:transparent; color:white; z-index:10000; width:60px; height:20px; margin-top:5px;"><b>이미지다운</b></button>
                                <button type="button" id="btn5" class="btn btn- btn-xs"  style=" font-size:15px; background-color:transparent; color:white; z-index:10000; width:20px; height:20px;"><b>+</b></button>
                                <button type="button" id="btn5-1" class="btn btn- btn-xs"  style=" font-size:15px; background-color:transparent; color:white; z-index:10000; width:20px; height:20px;"><b>-</b></button>
                                </div>
                                <div class="grad2">
                                <div id="div201" style="width: 100%;  margin-top: 0px;">
                                    <%--<canvas id="myChart5" style="  width:100%; "></canvas>--%>
                                </div>
                             </div>
                            </div>

                            <div class="card shadow grad" id="box6" style="float: left; width: 31%; text-align:right; margin-top: auto; margin-left: 1%; z-index: 99; margin-bottom: 1%;">
                             <div style="position:absolute; z-index:10000; float:right; width:100%;" >
                                 <button type="button" id="btn202" class="btn btn- btn-xs"  style=" visibility:hidden; font-size:12px; background-color:transparent; color:white; z-index:10000; width:60px; height:20px; margin-top:5px;"><b>이미지다운</b></button>
                                <button type="button" id="btn6" class="btn btn- btn-xs"  style=" font-size:15px; background-color:transparent; color:white; z-index:10000; width:20px; height:20px;"><b>+</b></button>
                                <button type="button" id="btn6-1" class="btn btn- btn-xs"  style=" font-size:15px; background-color:transparent; color:white; z-index:10000; width:20px; height:20px;"><b>-</b></button>
                                </div>
                                <div class="grad2">
                                <div id="div202" style="width: 100%;  margin-top: 0px;">
                                    <%--<canvas id="myChart6" style="  width:100%; "></canvas>--%>
                                </div>
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
                                        async: true,
                                        cache: false,
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
                                        async: true,
                                        cache: false,
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
                                        //url: "/Chart/Chart2.aspx/chart1",
                                        url: "/Chart/Chart2.aspx/chart1",
                                        data: '{}',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        async: true,
                                        cache: false,
                                        success: OnSuccess4,
                                        failure: function (response) {
                                            //alert(response.d);
                                        },
                                        error: function (response) {
                                            //alert(response.d);
                                        }
                                    });
                                });

                                function init2() {

                                    window.ref = window.setInterval(function () { rewrite2(); }, 10000);
                                }
                                function rewrite2() {
                                    clearInterval(window.ref);
                                    $.ajax({
                                        type: "POST",
                                        url: "/Chart/Chart2.aspx/chart3",
                                        data: '{}',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        async: true,
                                        cache: false,
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
                                        async: true,
                                        cache: false,
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
                                        async: true,
                                        cache: false,
                                        success: OnSuccess4,
                                        failure: function (response) {
                                            //alert(response.d);
                                        },
                                        error: function (response) {
                                            //alert(response.d);
                                        }
                                    });

                                    init2();
                                }
                                init2();


                                //setInterval(function () {

                                //},1 * 10 * 1000);


                                var temp_traffic6 = "";
                                var serverip6 = "";
                                var temp_time6 = "";
                                var trafficArray6 = "";
                                var timeArray6 = "";

                                function OnSuccess6(response) {
                                    var customers6 = response.d;
                                    temp_time6 = "";
                                    temp_traffic6 = "";
                                    serverip6 = "";
                                    timeArray6 = "";
                                    trafficArray6 = "";
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

                                }


                                var temp_traffic5 = "";
                                var serverip5 = "";
                                var temp_time5 = "";
                                var trafficArray5 = "";
                                var timeArray5 = "";

                                function OnSuccess5(response) {
                                    var customers5 = response.d;
                                    temp_time5 = "";
                                    temp_traffic5 = "";
                                    serverip5 = "";
                                    timeArray5 = "";
                                    trafficArray5 = "";
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




                                    var list4 = document.getElementById("div200");   // Get the <ul> element with id="myList"
                                    list4.removeChild(list4.childNodes[0]);
                                    var list5 = document.getElementById("div201");   // Get the <ul> element with id="myList"
                                    list5.removeChild(list5.childNodes[0]);
                                    var list6 = document.getElementById("div202");   // Get the <ul> element with id="myList"
                                    list6.removeChild(list6.childNodes[0]);

                                    //서버차트 1
                                    var ctx4 = "";
                                    var myChart4 = "";
                                    $('#div200').html(''); //remove canvas from container
                                    $('#myChart4').remove(); // this is my <canvas> element
                                    $('#div200').html('<canvas id="myChart4" style="z-index:200; position:relative;  width:100%;   "></canvas>'); //add it back to the container
                                    ctx4 = document.getElementById("myChart4").getContext("2d");
                                    var gradientFill = ctx4.createLinearGradient(0, 0, 0, 450);
                                    gradientFill.addColorStop(0, 'rgba(22, 183, 250, 0.3)');
                                    gradientFill.addColorStop(0.5, 'rgba(22, 183, 250, 0.1)');
                                    gradientFill.addColorStop(1, 'rgba(22, 183, 250, 0)');
                                    myChart4 = new Chart(ctx4, {
                                        type: "line",
                                        data: {
                                            labels: timeArray4,
                                            datasets: [
                                                {
                                                    label: serverip4,
                                                    data: trafficArray4,
                                                    //backgroundColor: ["rgba(3, 169, 244, 0.4)"],
                                                    backgroundColor: gradientFill,
                                                    borderColor: ["rgba(3, 169, 244, 1)"],
                                                    borderWidth: 1,
                                                    pointRadius: 1,
                                                    fill: "start"
                                                }
                                                //,{
                                                //  label: serverip5,
                                                //  data: trafficArray5,
                                                //  backgroundColor: ["rgba(161, 201, 249, .15)"],
                                                //  borderColor: ["rgba(161, 201, 249, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //  }
                                                //,{
                                                //  label: serverip6,
                                                //  data: trafficArray6,
                                                //  backgroundColor: ["rgba(192, 161, 249, .15)"],
                                                //  borderColor: ["rgba(192, 161, 249, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //}
                                            ]
                                        },
                                        options: {
                                            animation: {
                                                duration: 0
                                            },
                                            tooltips: {
                                                intersect: false,
                                                backgroundColor: "rgba(3, 169, 244, 1)",
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
                                                text: "서버 차트1",
                                                display: false,
                                                fontColor: "white",
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
                                                    fontColor: "white",
                                                    fontSize: 11
                                                }
                                            },
                                            scales: {
                                                xAxes: [
                                                    {
                                                        ticks: {
                                                            fontColor: "white",
                                                            autoSkip: true,
                                                            maxTicksLimit: 20
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
                                                            fontColor: "white"
                                                        }
                                                    }
                                                ]
                                            }
                                        }
                                    });

                                    //서버차트 2
                                    var ctx5 = "";
                                    var myChart5 = "";
                                    $('#div201').html(''); //remove canvas from container
                                    $('#myChart5').remove(); // this is my <canvas> element
                                    $('#div201').html('<canvas id="myChart5" style="z-index:200; position:relative;  width:100%;   "></canvas>'); //add it back to the container
                                    ctx5 = document.getElementById("myChart5").getContext("2d");
                                    gradientFill = ctx5.createLinearGradient(0, 0, 0, 450);
                                    gradientFill.addColorStop(0, 'rgba(241, 191, 51, 0.3)');
                                    gradientFill.addColorStop(0.5, 'rgba(241, 191, 51, 0.1)');
                                    gradientFill.addColorStop(1, 'rgba(241, 191, 51, 0)');
                                    myChart5 = new Chart(ctx5, {
                                        type: "line",
                                        data: {
                                            labels: timeArray5,
                                            datasets: [
                                                //{
                                                //  label: serverip4,
                                                //  data: trafficArray4,
                                                //  backgroundColor: ["rgba(113, 88, 203, .15)"],
                                                //  borderColor: ["rgba(113, 88, 203, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //},
                                                {
                                                    label: serverip5,
                                                    data: trafficArray5,
                                                    //backgroundColor: ["rgba(255, 152, 0, 0.4)"],
                                                    backgroundColor: gradientFill,
                                                    borderColor: ["rgba(255, 152, 0, 1)"],
                                                    borderWidth: 1,
                                                    pointRadius: 1,
                                                    fill: "start"
                                                }
                                                //,{
                                                //  label: serverip6,
                                                //  data: trafficArray6,
                                                //  backgroundColor: ["rgba(192, 161, 249, .15)"],
                                                //  borderColor: ["rgba(192, 161, 249, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //}
                                            ]
                                        },
                                        options: {
                                            animation: {
                                                duration: 0
                                            },
                                            tooltips: {
                                                intersect: false,
                                                backgroundColor: "rgba(255, 152, 0, 1)",
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
                                                        const o = d.datasets.map((ds) => ds.data[t[0].index] + " Mb/s");

                                                        return o.join(', ');
                                                    },
                                                    label: function (t, d) {
                                                        return d.labels[t.index];
                                                    }
                                                }
                                            },
                                            title: {
                                                text: "서버 차트2",
                                                display: false,
                                                fontColor: "white",
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
                                                    fontColor: "white",
                                                    fontSize: 11
                                                }
                                            },
                                            scales: {
                                                xAxes: [
                                                    {
                                                        ticks: {
                                                            fontColor: "white",
                                                            autoSkip: true,
                                                            maxTicksLimit: 20
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
                                                            fontColor: "white"
                                                        }
                                                    }
                                                ]
                                            }
                                        }
                                    });

                                    //서버차트 3
                                    var ctx6 = "";
                                    var myChart6 = "";
                                    $('#div202').html(''); //remove canvas from container
                                    $('#myChart6').remove(); // this is my <canvas> element
                                    $('#div202').html('<canvas id="myChart6" style="z-index:200; position:relative;  width:100%;   "></canvas>'); //add it back to the container
                                    ctx6 = document.getElementById("myChart6").getContext("2d");
                                    gradientFill = ctx6.createLinearGradient(0, 0, 0, 450);
                                    gradientFill.addColorStop(0, 'rgba(61, 210, 48, 0.3)');
                                    gradientFill.addColorStop(0.5, 'rgba(61, 210, 48, 0.1)');
                                    gradientFill.addColorStop(1, 'rgba(61, 210, 48, 0)');
                                    myChart6 = new Chart(ctx6, {
                                        type: "line",
                                        data: {
                                            labels: timeArray6,
                                            datasets: [
                                                //{
                                                //  label: serverip4,
                                                //  data: trafficArray4,
                                                //  backgroundColor: ["rgba(113, 88, 203, .15)"],
                                                //  borderColor: ["rgba(113, 88, 203, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //}
                                                //,{
                                                //  label: serverip5,
                                                //  data: trafficArray5,
                                                //  backgroundColor: ["rgba(161, 201, 249, .15)"],
                                                //  borderColor: ["rgba(161, 201, 249, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //  },
                                                {
                                                    label: serverip6,
                                                    data: trafficArray6,
                                                    //backgroundColor: ["rgba(29, 233, 182, 0.4)"],
                                                    backgroundColor: gradientFill,
                                                    borderColor: ["rgba(29, 233, 182, 1)"],

                                                    borderWidth: 1,
                                                    pointRadius: 1,
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
                                                backgroundColor: "rgba(29, 233, 182, 1)",
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
                                                text: "서버 차트3",
                                                display: false,
                                                fontColor: "white",
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
                                                    fontColor: "white",
                                                    fontSize: 11
                                                }
                                            },
                                            scales: {
                                                xAxes: [
                                                    {
                                                        ticks: {
                                                            fontColor: "white",
                                                            autoSkip: true,
                                                            maxTicksLimit: 20
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
                                                            fontColor: "white"
                                                        }
                                                    }
                                                ]
                                            }
                                        }
                                    });


                                };


                            </script>

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div id="div4" class="card shadow grad" runat="server" style="float: left; width: 63%; margin-left: 1%; z-index: 99;">
                                        <div class="grad2">
                                        <p></p><center><font color="white">
                                            <h6>&nbsp; 이벤트 로그
                                            </h6>
                                        </font></center>
                                        <asp:Table ID="TBLLIST" runat="server" CssClass="table no-border " Width="100%"></asp:Table>
                                            </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <br />


                            <div class="card shadow grad" style="float: left; width: 31%; margin-top: auto; margin-left: 1%; z-index: 99; margin-bottom: 1%;">
                                <div class="grad2">
                                <div id="div300" style="width: 100%; height: 185px; margin-top: 0px;">
                              <%--      <canvas id="myChart7" style="  width:100%;  "></canvas>--%>
                                </div>
                                    </div>
                            </div>
                            <script>
                                $(function () {
                                    $.ajax({
                                        type: "POST",
                                        url: "/Chart/Chart2.aspx/chart4",
                                        data: '{}',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        async: true,
                                        cache: false,
                                        success: OnSuccess9,
                                        failure: function (response) {
                                            //alert(response.d);
                                        },
                                        error: function (response) {
                                            //alert(response.d);
                                        }
                                    });
                                });


                                function init3() {

                                    window.ref = window.setInterval(function () { rewrite3(); }, 10000);
                                }
                                function rewrite3() {
                                    clearInterval(window.ref);
                                    $.ajax({
                                        type: "POST",
                                        url: "/Chart/Chart2.aspx/chart4",
                                        data: '{}',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        async: true,
                                        cache: false,
                                        success: OnSuccess9,
                                        failure: function (response) {
                                            //alert(response.d);
                                        },
                                        error: function (response) {
                                            //alert(response.d);
                                        }
                                    });

                                    $.ajax({
                                        type: "POST",
                                        url: "/Chart/Chart2.aspx/chart5",
                                        data: '{}',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        async: true,
                                        cache: false,
                                        success: OnSuccess10,
                                        failure: function (response) {
                                            //alert(response.d);
                                        },
                                        error: function (jqXHR, exception) {
                                            if (jqXHR.status === 0) {
                                                alert('Not connect.\n Verify Network.');
                                            }
                                            else if (jqXHR.status === 400) {
                                                alert('Server understood the request, but request content was invalid. [400]');
                                            }
                                            else if (jqXHR.status === 401) {
                                                alert('Unauthorized access. [401]');
                                            }
                                            else if (jqXHR.status === 403) {
                                                alert('Forbidden resource can not be accessed. [403]');
                                            }
                                            else if (jqXHR.status === 404) {
                                                alert('Requested page not found. [404]');
                                            }
                                            else if (jqXHR.status === 500) {
                                                alert('Internal server error. [500]');
                                            }
                                            else if (jqXHR.status === 503) {
                                                alert('Service unavailable. [503]');
                                            }
                                            else if (exception === 'parsererror') {
                                                alert('Requested JSON parse failed. [Failed]');
                                            }
                                            else if (exception === 'timeout') {
                                                alert('Time out error. [Timeout]');
                                            }
                                            else if (exception === 'abort') {
                                                alert('Ajax request aborted. [Aborted]');
                                            }
                                            else {
                                                alert('Uncaught Error.n' + jqXHR.responseText);
                                            }
                                        }

                                    });

                                    init3();
                                }
                                init3();

                                //setInterval(function () {


                                //},1 * 10 * 1000);



                                function OnSuccess9(response) {
                                    var customers4 = response.d;
                                    var temp_cpu = "";
                                    var serverip4 = "";
                                    var temp_time4 = "";
                                    var count4 = 0;
                                    $(customers4).each(function () {
                                        //serverip4 = this.serverip;
                                        temp_cpu += this.cpu + ",";
                                        temp_time4 += this.serverip3 + ",";

                                    });
                                    temp_cpu = temp_cpu.slice(0, -1);
                                    temp_time4 = temp_time4.slice(0, -1);
                                    var cpuArray4 = temp_cpu.split(',');
                                    var timeArray4 = temp_time4.split(',');


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
                                    var list7 = document.getElementById("div300");   // Get the <ul> element with id="myList"
                                    list7.removeChild(list7.childNodes[0]);

                                    //CPU
                                    var ctx7 = "";
                                    var myChart7 = "";
                                    $('#div300').html(''); //remove canvas from container
                                    $('#myChart7').remove(); // this is my <canvas> element
                                    $('#div300').html('<canvas id="myChart7" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>'); //add it back to the container
                                    ctx7 = document.getElementById("myChart7").getContext("2d");
                                    var gradientFill = ctx7.createLinearGradient(0, 0, 0, 450);
                                    gradientFill.addColorStop(0, 'rgba(22, 183, 250, 0.3)');
                                    gradientFill.addColorStop(0.5, 'rgba(22, 183, 250, 0.1)');
                                    gradientFill.addColorStop(1, 'rgba(22, 183, 250, 0)');
                                    myChart7 = new Chart(ctx7, {
                                        //plugins: [randomColorPlugin],
                                        type: "bar",
                                        data: {
                                            labels: timeArray4,
                                            datasets: [
                                                {
                                                    label: serverip4,
                                                    data: cpuArray4,
                                                    //backgroundColor: ["rgba(3, 169, 244, 0.4)","rgba(3, 169, 244, 0.4)","rgba(3, 169, 244, 0.4)","rgba(3, 169, 244, 0.4)","rgba(3, 169, 244, 0.4)"],
                                                    backgroundColor: gradientFill,
                                                    borderColor: ["rgba(3, 169, 244, 1)", "rgba(3, 169, 244, 1)", "rgba(3, 169, 244, 1)", "rgba(3, 169, 244, 1)", "rgba(3, 169, 244, 1)"],
                                                    borderWidth: 1,
                                                    fill: "start"
                                                }
                                                //,{
                                                //  label: serverip5,
                                                //  data: trafficArray5,
                                                //  backgroundColor: ["rgba(161, 201, 249, .15)"],
                                                //  borderColor: ["rgba(161, 201, 249, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //  }
                                                //,{
                                                //  label: serverip6,
                                                //  data: trafficArray6,
                                                //  backgroundColor: ["rgba(192, 161, 249, .15)"],
                                                //  borderColor: ["rgba(192, 161, 249, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //}
                                            ]
                                        },
                                        options: {
                                            animation: {
                                                duration: 0
                                            },
                                            tooltips: {
                                                intersect: false,
                                                //backgroundColor: "rgba(113, 88, 203, 1)",
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
                                                        const o = d.datasets.map((ds) => ds.data[t[0].index] + " %    ")

                                                        return o.join(', ');
                                                    },
                                                    label: function (t, d) {
                                                        return d.labels[t.index];
                                                    }
                                                }
                                            },
                                            title: {
                                                text: "CPU",
                                                display: true,
                                                fontColor: "white",
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
                                                    fontColor: "white",
                                                    fontSize: 11
                                                }
                                            },
                                            scales: {
                                                xAxes: [
                                                    {
                                                        ticks: {
                                                            fontColor: "white",
                                                            autoSkip: true,
                                                            maxTicksLimit: 20
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
                                                            fontColor: "white",
                                                            min: 0, // minimum value
                                                            max: 100 // maximum value
                                                        }
                                                    }
                                                ]
                                            }
                                        }
                                    });



                                }


                            </script>

                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div id="div3" class="card shadow grad" runat="server" style="float: left; width: 63%; margin-left: 1%; z-index: 99;">
                                        <div class="grad2">
                                        <p></p><center><font color="white">
                                            <h6>&nbsp; 다운 장비
                                            </h6>
                                        </font></center>
                                        <asp:Table ID="TBLLIST4" runat="server" CssClass="table no-border " Width="100%"></asp:Table>
                                    </div>
                                  </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                            <div class="card shadow grad" style="float: left;  width: 31%; margin-top: auto; margin-left: 1%; margin-right: 4%; z-index: 99;">
                                <div class="grad2">
                                <div id="div400" style="width: 100%; height: 185px; margin-top: 0px;">
                                    <%--<canvas id="myChart8" style="  width:100%; "></canvas>--%>
                                </div>
                                    </div>
                            </div>
                            <script>
                                $(function () {
                                    $.ajax({
                                        type: "POST",
                                        url: "/Chart/Chart2.aspx/chart5",
                                        data: '{}',
                                        contentType: "application/json; charset=utf-8",
                                        dataType: "json",
                                        async: true,
                                        cache: false,
                                        success: OnSuccess10,
                                        failure: function (response) {
                                            alert(response.d);
                                        },
                                        error: function (response) {
                                            alert(response.d);
                                        }
                                    });

                                });

                                //이게왜 있어야 하는지 모르겠는데 있어야 작동하네;;;
                                function init4() {

                                    window.ref = window.setInterval(function () { rewrite4(); }, 10000);
                                }
                                function rewrite4() {
                                    clearInterval(window.ref);
                                    //$.ajax({
                                    //        type: "POST",
                                    //        url: "/Chart/Chart2.aspx/chart5",
                                    //        data: '{}',
                                    //        contentType: "application/json; charset=utf-8",
                                    //        dataType: "json",
                                    //        async: false,
                                    //        cache: false,
                                    //        success: OnSuccess10,
                                    //        failure: function (response) {
                                    //            //alert(response.d);
                                    //        },
                                    //        error: function (response) {
                                    //            //alert(response.d);
                                    //        }
                                    //    });


                                    init4();
                                }
                                init4();

                                //setInterval(function () {
                                //      $.ajax({
                                //        type: "POST",
                                //        url: "/Chart/Chart2.aspx/chart5",
                                //        data: '{}',
                                //        contentType: "application/json; charset=utf-8",
                                //        dataType: "json",
                                //        async: false,
                                //        cache: false,
                                //        success: OnSuccess10,
                                //        failure: function (response) {
                                //            //alert(response.d);
                                //        },
                                //        error: function (response) {
                                //            //alert(response.d);
                                //        }
                                //    });
                                //},5000);


                                function OnSuccess10(response) {
                                    var customers5 = response.d;
                                    var temp_memory = "";
                                    var serverip4 = "";
                                    var temp_time4 = "";
                                    var count4 = 0;
                                    $(customers5).each(function () {
                                        temp_memory += this.memory + ",";
                                        temp_time4 += this.serverip3 + ",";

                                    });
                                    temp_memory = temp_memory.slice(0, -1);
                                    temp_time4 = temp_time4.slice(0, -1);
                                    var memoryArray4 = temp_memory.split(',');
                                    var timeArray4 = temp_time4.split(',');


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

                                    var list8 = document.getElementById("div400");   // Get the <ul> element with id="myList"
                                    list8.removeChild(list8.childNodes[0]);
                                    //memory
                                    var ctx8 = "";
                                    var myChart8 = "";
                                    $('#div400').html(''); //remove canvas from container
                                    $('#myChart8').remove(); // this is my <canvas> element
                                    $('#div400').html('<canvas id="myChart8" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>'); //add it back to the container
                                    ctx8 = document.getElementById("myChart8").getContext("2d");
                                    gradientFill = ctx8.createLinearGradient(0, 0, 0, 450);
                                    gradientFill.addColorStop(0, 'rgba(241, 191, 51, 0.3)');
                                    gradientFill.addColorStop(0.5, 'rgba(241, 191, 51, 0.1)');
                                    gradientFill.addColorStop(1, 'rgba(241, 191, 51, 0)');
                                    myChart8 = new Chart(ctx8, {
                                        //plugins: [randomColorPlugin],
                                        type: "bar",
                                        data: {
                                            labels: timeArray4,
                                            datasets: [
                                                {
                                                    label: serverip4,
                                                    data: memoryArray4,
                                                    //backgroundColor: ["rgba(255, 152, 0, 0.4)","rgba(255, 152, 0, 0.4)","rgba(255, 152, 0, 0.4)","rgba(255, 152, 0, 0.4)","rgba(255, 152, 0, 0.4)"],
                                                    //backgroundColor: ["rgba(255, 152, 0, 0.4)"],
                                                    backgroundColor: gradientFill,
                                                    borderColor: ["rgba(255, 152, 0, 1)","rgba(255, 152, 0, 1)","rgba(255, 152, 0, 1)","rgba(255, 152, 0, 1)","rgba(255, 152, 0, 1)"],
                                                    borderWidth: 1,
                                                    fill: "start"
                                                }
                                                //,{
                                                //  label: serverip5,
                                                //  data: trafficArray5,
                                                //  backgroundColor: ["rgba(161, 201, 249, .15)"],
                                                //  borderColor: ["rgba(161, 201, 249, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //  }
                                                //,{
                                                //  label: serverip6,
                                                //  data: trafficArray6,
                                                //  backgroundColor: ["rgba(192, 161, 249, .15)"],
                                                //  borderColor: ["rgba(192, 161, 249, 1)"],
                                                //  borderWidth: 1,
                                                //  fill: "start"
                                                //}
                                            ]
                                        },
                                        options: {
                                            animation: {
                                                duration: 0
                                            },
                                            tooltips: {
                                                intersect: false,
                                                //backgroundColor: "rgba(113, 88, 203, 1)",
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
                                                        const o = d.datasets.map((ds) => ds.data[t[0].index] + " %    ")

                                                        return o.join(', ');
                                                    },
                                                    label: function (t, d) {
                                                        return d.labels[t.index];
                                                    }
                                                }
                                            },
                                            title: {
                                                text: "메모리",
                                                display: true,
                                                fontColor: "white",
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
                                                    fontColor: "white",
                                                    fontSize: 11
                                                }
                                            },
                                            scales: {
                                                xAxes: [
                                                    {
                                                        ticks: {
                                                            fontColor: "white",
                                                            autoSkip: true,
                                                            maxTicksLimit: 20
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
                                                            fontColor: "white",
                                                            min: 0, // minimum value
                                                            max: 100 // maximum value
                                                        }
                                                    }
                                                ]
                                            }
                                        }
                                    });



                                }
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