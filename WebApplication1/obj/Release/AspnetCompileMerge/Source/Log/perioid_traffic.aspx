<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="perioid_traffic.aspx.cs" Inherits="WebApplication1.perioid_traffic" %>

<%@ Register Src="~/Common/leftmenu.ascx" TagName="uc_menu" TagPrefix="uc1" %>
<%@ Register Src="~/Common/bottom.ascx" TagName="uc_bottom" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel='stylesheet' href="../Content/bootstrap.min.css" />
    <link rel="stylesheet" href="../Content/AdminLTE.min.css" />
    <link rel="stylesheet" href="../Content/font-awesome.min.css" />
    <link rel="stylesheet" href="../Content/_all-skins.min.css" />
    <link rel="stylesheet" href="../Content/ionicons.min.css" />
    <link rel="stylesheet" href="../Content/bootstrap.min2.css" />

    <script src="../Scripts/jquery-3.2.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Content/JavaScript.js"></script>
    <script src="../Scripts/AdminLTE.css"></script>
    <link rel="stylesheet" href="../Scripts/datepicker3.min.css" />


    <title></title>
    <script type="text/javascript">

        function ConvertToImage(btnExport) {
            html2canvas($("#pdfview")).then(function (canvas) {


                var imgdata = canvas.toDataURL('image/jpeg', 1.0)
                var imgWidth = 200; // 이미지 가로 길이(mm) A4 기준
                var pageHeight = imgWidth * 1.414;  // 출력 페이지 세로 길이 계산 A4 기준

                var imgHeight = canvas.height * imgWidth / canvas.width;
                var heightLeft = imgHeight;
                //var doc = new jspdf('p', 'mm', [100, 210]);

                var position = 10;
                doc.addImage(imgdata, 'jpeg', 5, position, imgWidth, imgHeight);

                // 첫 페이지 출력
                heightLeft -= pageHeight;
                //alert(imgHeight);
                // 한 페이지 이상일 경우 루프 돌면서 출력
                //while (heightLeft >= 20) {
                //    position = heightLeft - imgHeight;
                //    doc.addPage();
                //    doc.addImage(imgdata, 'jpeg', 5, position, imgWidth, imgHeight);
                //    heightLeft -= pageHeight;
                //}




                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1; //January is 0!
                var yyyy = today.getFullYear();
                if (dd < 10) {
                    dd = '0' + dd
                }
                if (mm < 10) {
                    mm = '0' + mm
                }
                today = mm + '/' + dd + '/' + yyyy;
                // 파일 저장
                doc.save(today + 'Result.pdf');

                // 파일 저장
                //var base64 = canvas.toDataURL();
                //$("[id*=hfImageData]").val(base64);

                //alert(imgHeight);

                //var doc = new jspdf('p', 'mm', [297, 210]);
                //doc.addImage(imgdata, 'jpeg', 5, 5, 200, 210);


                //__doPostBack(btnExport.name, "");

                //var doc = new jspdf('p', 'mm');
                //doc.addImage(imgdata, 'jpeg', 10, position, imgWidth, imgHeight);
                //doc.save('test.pdf');


                //heightLeft -= pageHeight;



            });


            return false;
        }


        function go(NO) {
            if (confirm("삭제 하시겠습니까?") == true) {
                var obj = document.getElementById("<%#searchHF.ClientID %>");
                if (obj)
                    obj.value = NO;

       <%--<%# Page.GetPostBackEventReference(Button3) %>--%>
            }

        }



        $(function () {
            $('#startdate').datepicker({
                calendarWeeks: false,
                todayHighlight: true,
                autoclose: true,
                format: "yyyy-mm-dd",
                language: "kr"

            });
        });
        $(function () {
            $('#enddate').datepicker({
                calendarWeeks: false,
                todayHighlight: true,
                autoclose: true,
                format: "yyyy-mm-dd",
                language: "kr"
            });
        });




    </script>
</head>
<body class="hold-transition skin-blue sidebar-mini">
    <form id="form1" runat="server" style="height: 100%;">
        <div class="wrapper">
            <uc1:uc_menu ID="uc_menu" runat="server" />
            <div class="content-wrapper">

                <section class="content-header" style="margin-bottom: -40px">
                    <h1>기간별트래픽
                <small>기간별트래픽 확인페이지</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                        <li class="active"><a href="#">기간별트래픽</a></li>
                    </ol>
                </section>


                <section class="content">
                    <div class="right">
                        <div class="container-fluid">

                            <nav class="navbar ">
                                <!-- Brand and toggle get grouped for better mobile display -->
                                <div class="navbar-btn ">
                                    <ui class="nav navbar-nav navbar-left">

                                        <li>
                                            <%--  <Button ID="Button1" runat="server"   OnserverClick="Button6_Click" Class="btn btn-primary btn-sm">
                              <span class="glyphicon glyphicon-save-file" style='color:white;'></span> 엑셀 저장
                        </button>--%>
                                        </li>
                                    </ui>
                                    <ui class="nav navbar-nav navbar-right">
                                        <li style="margin-top: 5px; margin-right: 10px"></li>
                                        <li style="margin-right: 10px"></li>
                                        <li style="margin-top: 5px; margin-right: 10px"></li>
                                        <li style="margin-right: 10px"></li>
                                        <li style="margin-right: 10px">

                                            <input runat="server" id="start" name="start" ng-model="start" type="text" class="form-control start" style="width: 100px; z-index: 99" visible="false" />
                                        </li>
                                        <li style="margin-right: 10px">
                                            <input runat="server" id="end" name="end" ng-model="end" type="text" class="form-control end" style="width: 100px; z-index: 99" visible="false" />
                                        </li>
                                        <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="HiddenField2" runat="server"></asp:HiddenField>
                                        <asp:HiddenField ID="serverip" runat="server" />

                                        <script>
                                            $('.start, .end').timepicker({
                                                showInputs: false,
                                                minuteStep: 1,
                                            });
                                            $("#end, #start").change(function () {

                                                var time = $("#start").val();
                                                var hours = Number(time.match(/^(\d+)/)[1]);
                                                var minutes = Number(time.match(/:(\d+)/)[1]);
                                                var AMPM = time.match(/\s(.*)$/)[1];
                                                //if(AMPM == "PM" && hours<12) hours = hours+12;
                                                //if(AMPM == "AM" && hours==12) hours = hours-12;
                                                var sHours = hours.toString();
                                                var sMinutes = minutes.toString();
                                                if (hours < 10) sHours = "0" + sHours;
                                                if (minutes < 10) sMinutes = "0" + sMinutes;
                                                var time2 = $("#end").val();


                                                if (time2 == "") {
                                                    var time2 = "00:00 AM";
                                                }
                                                var hours2 = Number(time2.match(/^(\d+)/)[1]);
                                                var minutes2 = Number(time2.match(/:(\d+)/)[1]);
                                                var AMPM2 = time2.match(/\s(.*)$/)[1];
                                                //if(AMPM2 == "PM" && hours2<12) hours2 = hours2+12;
                                                //if(AMPM2 == "AM" && hours2==12) hours2 = hours2-12;
                                                var sHours2 = hours2.toString();
                                                var sMinutes2 = minutes2.toString();
                                                if (hours2 < 10) sHours2 = "0" + sHours2;
                                                if (minutes2 < 10) sMinutes2 = "0" + sMinutes2;
                                                //alert(sHours + ":" + sMinutes);
                                                var comparehour = sHours2 - sHours;
                                                var comparemin = sMinutes2 - sMinutes;
                                                if (comparehour < 0) {
                                                    $("#end").val(time);
                                                }
                                                else if ((comparehour == 0) && (comparemin < 0)) {
                                                    $("#end").val(time);
                                                }
                                            });
                                        </script>

                                        <li style="margin-right: 10px"></li>
                                    </ui>
                                </div>
                            </nav>
                        </div>

                        <div class="container-fluid">
                            <div style="margin-top: -10px; margin-bottom: 10px; float: left;">
                                <asp:Button ID="Button2" runat="server" Text="이전" CssClass="btn btn-primary btn-sm" OnClick="Button2_Click" />
                                <%--<asp:Button ID="btnExport" Text="Export to JPG" Class="btn btn-primary btn-sm" runat="server" UseSubmitBehavior="false" OnClick="ExportToImage" OnClientClick="return ConvertToImage(this)" />--%>
                            </div>
                            <div style="float: right; margin-top: -10px;">
                                <asp:Button ID="Button3" runat="server" Text="검색" CssClass="btn btn-primary btn-sm" OnClick="Button3_Click" />
                            </div>

                            <div style="float: right; margin-top: -10px; margin-right: 10px;">
                                <asp:TextBox ID="enddate" runat="server" Class="form-control " Width="100px"></asp:TextBox>
                            </div>
                            <div style="float: right; margin-top: -5px; margin-right: 10px;">
                                ~ 
                            </div>
                            <div style="float: right; margin-top: -10px; margin-right: 10px;">
                                <asp:TextBox ID="startdate" runat="server" Class="form-control " Width="100px"></asp:TextBox>
                            </div>
                            <div style="float: right; margin-top: -2px; margin-right: 10px;">
                                <asp:Label ID="Label6" runat="server" Text="기간 : " CssClass="navbar-left"></asp:Label>
                            </div>
                            <div class="box box-info" style="overflow: auto; padding-right: 10px;">
                                <div class="pdfview" id="pdfview" runat="server" style="height: 100%; width: 100%">
                                    <h4>
                                        <center>
                                            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                        </center>
                                        <label id="title" runat="server" visible="false"></label>
                                    </h4>
                                    <div id="div100" class="div100" style="margin-top: 0px; width: 48%; text-align: right; float: left; height: 100%;">
                                        <canvas id="myChart" width="400" height="200"></canvas>
                                    </div>
                                    <div style="position: absolute; bottom: 0px; width: 48%;">
                                        <b>
                                            <center>일별 3시간 트래픽</center>
                                        </b>
                                        <div style="display: inline-block; float: right; margin-right: 10px;">
                                            평균<br />
                                            <asp:Label ID="Label102" runat="server" Text="Label" Font-Size="Smaller"></asp:Label>
                                            <font size="2">mbps</font><br />
                                            <asp:Label ID="Label103" runat="server" Text="Label" Font-Size="Smaller" Visible="false"></asp:Label><br />
                                        </div>
                                        <div style="display: inline-block; float: right; margin-right: 10px;">
                                            최대값<br />
                                            <asp:Label ID="Label43" runat="server" Text="Label" Font-Size="Smaller"></asp:Label>
                                            <font size="2">mbps</font><br />
                                            <asp:Label ID="Label44" runat="server" Text="Label" Font-Size="Smaller" Visible="false"></asp:Label><br />
                                            <asp:Label ID="Label45" runat="server" Text="Label" Font-Size="Smaller" Visible="false"></asp:Label>
                                        </div>
                                    </div>

                                    <script>

                                        $(function () {
                                            var serverip = document.getElementById("<%=serverip.ClientID %>").value;
                             var startdate = document.getElementById("<%=startdate.ClientID %>").value;
                             var enddate = document.getElementById("<%=enddate.ClientID %>").value;
                             var starttime = document.getElementById("<%=HiddenField1.ClientID %>").value;
                             var endtime = document.getElementById("<%=HiddenField2.ClientID %>").value;



                             $.ajax({
                                 type: "POST",
                                 url: "/Log/perioid_traffic.aspx/chart1",
                                 data: "{'serverip':'" + serverip + "', 'startdate':'" + startdate + "', 'enddate':'" + enddate + "' , 'HiddenField1':'" + starttime + "' , 'HiddenField2':'" + endtime + "'}",
                                 contentType: "application/json; charset=utf-8",
                                 dataType: "json",
                                 async: true,
                                 cache: false,
                                 success: OnSuccess,
                                 failure: function (response) {
                                     alert(response.d);
                                 },
                                 error: function (response) {
                                     alert(response.d);
                                 }
                             });
                         });
                                        setInterval(function () {
                                            var serverip = document.getElementById("<%=serverip.ClientID %>").value;
             var startdate = document.getElementById("<%=startdate.ClientID %>").value;
             var enddate = document.getElementById("<%=enddate.ClientID %>").value;
             var starttime = document.getElementById("<%=HiddenField1.ClientID %>").value;
             var endtime = document.getElementById("<%=HiddenField2.ClientID %>").value;
             $.ajax({
                 type: "POST",
                 url: "/Log/perioid_traffic.aspx/chart1",
                 data: "{'serverip':'" + serverip + "', 'startdate':'" + startdate + "', 'enddate':'" + enddate + "' , 'HiddenField1':'" + starttime + "' , 'HiddenField2':'" + endtime + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 async: true,
                 cache: false,
                 success: OnSuccess,
                 failure: function (response) {
                     alert(response.d);
                 },
                 error: function (response) {
                     alert(response.d);
                 }
             });
             $.ajax({
                 type: "POST",
                 url: "/Log/perioid_traffic.aspx/chart2",
                 data: "{'serverip':'" + serverip + "', 'startdate':'" + startdate + "', 'enddate':'" + enddate + "' , 'HiddenField1':'" + starttime + "' , 'HiddenField2':'" + endtime + "'}",
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 async: true,
                 cache: false,
                 success: OnSuccess4,
                 failure: function (response) {
                     alert(response.d);
                 },
                 error: function (response) {
                     alert(response.d);
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
                                            var customers = response.d;
                                            var temp_traffic = "";
                                            var serverip = "";
                                            var temp_time = "";
                                            var count = 0;
                                            var max = "";
                                            $(customers).each(function () {
                                                count++;
                                            });
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
                                            //alert(data2);
                                            max = Math.max.apply(null, trafficArray);
                                            var sum = 0.0;
                                            for (var i = 0; i < trafficArray.length; i++) {
                                                sum += parseInt(trafficArray[i]);
                                            }
                                            sum = sum / trafficArray.length;
                                            document.getElementById('<%=Label102.ClientID %>').innerHTML = Number(sum).toFixed(2);
        document.getElementById('<%=Label43.ClientID %>').innerHTML = max;


                                            var ctx = "";
                                            var myChart = "";
                                            $('#div100').html(''); //remove canvas from container
                                            $('#div100').html('<canvas id="myChart" width="400" height="200"></canvas>'); //add it back to the container
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

                                                    ]
                                                },
                                                options: {

                                                    animation: {
                                                        duration: 250
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
                                                    scales: {
                                                        xAxes: [

                                                            {

                                                                ticks: {
                                                                    autoSkip: true,
                                                                    maxTicksLimit: 20
                                                                },
                                                                gridLines: {
                                                                    display: true,
                                                                    drawOnChartArea: false,
                                                                }
                                                            }
                                                        ]
                                                    }
                                                }
                                            });
                                            //setInterval(() => addData(myChart), 5000);

                                        };








                                    </script>

                                    <div id="div200" class="div200" style="width: 48%; float: right; text-align: right; height: 100%;">
                                        <canvas id="myChart2" width="400" height="200"></canvas>
                                    </div>
                                    <div style="bottom: 0px; float: right; width: 48%;">
                                        <b>
                                            <center>일별 트래픽</center>
                                        </b>
                                        <div style="display: inline-block; float: right; margin-right: 10px;">
                                            평균<br />
                                            <asp:Label ID="Label100" runat="server" Text="Label" Font-Size="Smaller"></asp:Label>
                                            <font size="2">mbps</font><br />
                                            <asp:Label ID="Label101" runat="server" Text="Label" Font-Size="Smaller" Visible="false"></asp:Label><br />
                                        </div>
                                        <div style="display: inline-block; float: right; margin-right: 20px;">
                                            최대값<br />
                                            <asp:Label ID="Label40" runat="server" Text="Label" Font-Size="Smaller"></asp:Label>
                                            <font size="2">mbps</font><br />
                                            <asp:Label ID="Label41" runat="server" Text="Label" Font-Size="Smaller" Visible="false"></asp:Label><br />
                                            <asp:Label ID="Label42" runat="server" Text="Label" Font-Size="Smaller" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <script>
                                $(function () {
                                    var serverip = document.getElementById("<%=serverip.ClientID %>").value;
        var startdate = document.getElementById("<%=startdate.ClientID %>").value;
        var enddate = document.getElementById("<%=enddate.ClientID %>").value;
        var starttime = document.getElementById("<%=HiddenField1.ClientID %>").value;
        var endtime = document.getElementById("<%=HiddenField2.ClientID %>").value;




        $.ajax({
            type: "POST",
            url: "/Log/perioid_traffic.aspx/chart2",
            data: "{'serverip':'" + serverip + "', 'startdate':'" + startdate + "', 'enddate':'" + enddate + "' , 'HiddenField1':'" + starttime + "' , 'HiddenField2':'" + endtime + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: OnSuccess4,
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    });


                                var temp_traffic6 = "";
                                var serverip6 = "";
                                var temp_time6 = "";
                                var trafficArray6 = "";
                                var timeArray6 = "";
                                let data6 = "";
                                function OnSuccess6(response) {
                                    var customers3 = response.d;
                                    $(customers3).each(function () {
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
                                    var customers2 = response.d;
                                    $(customers2).each(function () {
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
                                    var customers = response.d;
                                    var temp_traffic4 = "";
                                    var serverip4 = "";
                                    var temp_time4 = "";
                                    var count4 = 0;
                                    $(customers).each(function () {
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
                                    //let data6 = trafficArray4;
                                    //alert(timeArray4);

                                    max = Math.max.apply(null, trafficArray4);
                                    var sum = 0;
                                    for (var i = 0; i < trafficArray4.length; i++) {
                                        sum += parseInt(trafficArray4[i]);
                                    }
                                    sum = sum / trafficArray4.length;
                                    document.getElementById('<%=Label100.ClientID %>').innerHTML = Number(sum).toFixed(2);
        document.getElementById('<%=Label40.ClientID %>').innerHTML = max;

                                    //function addData(chart, data) {
                                    //  chart.data.datasets.forEach(dataset => {
                                    //    let data = dataset.data;
                                    //    const first = data.shift();
                                    //    data.push(first);
                                    //    dataset.data = data;
                                    //  });

                                    //  chart.update();
                                    // }


                                    // 랜덤 색상을 생성하는 플러그인
                                    const randomColorPlugin = {
                                        id: 'randomColorPlugin', // 플러그인 ID
                                        beforeUpdate(chart) {
                                            const backgroundColor = [];
                                            const borderColor = [];

                                            // 데이터 개수만큼 랜덤 색상 생성
                                            const dataset = chart.config.data.datasets[0];
                                            for (let i = 0; i < dataset.data.length; i++) {
                                                const color = `rgba(${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)}, ${Math.floor(Math.random() * 255)},`;

                                                backgroundColor.push(color + "0.2)"); // 투명 배경색
                                                borderColor.push(color + "1)");      // 불투명 테두리 색
                                            }

                                            // 생성된 색상 배열을 데이터셋에 적용
                                            dataset.backgroundColor = backgroundColor;
                                            //dataset.borderColor = borderColor;
                                        }
                                    };

                                    // Chart.js에 플러그인 등록
                                    Chart.register(randomColorPlugin);



                                    var ctx2 = "";
                                    var myChart2 = "";
                                    $('#div200').html(''); //remove canvas from container
                                    $('#div200').html('<canvas id="myChart2" width="400" height="200"></canvas>'); //add it back to the container
                                    ctx2 = document.getElementById("myChart2").getContext("2d");
                                    myChart2 = new Chart(ctx2, {

                                        type: "bar",
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

                                            ]
                                        },
                                        options: {
                                            animation: {
                                                duration: 250
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
                                                        const o = d.datasets.map((ds) => ds.data[t[0].index] + " mbps");
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

                                                        },
                                                        gridLines: {
                                                            display: true,
                                                            drawOnChartArea: false,
                                                        }
                                                    }
                                                ],
                                                yAxes: [{
                                                    ticks: {
                                                        beginAtZero: true
                                                    }
                                                }]
                                            }
                                        }

                                    });




                                };



                            </script>
                            <div style="margin-top: 0px; width: 100%; float: left; text-align: right">
                            </div>

                        </div>



                        <div class="col-md-12" align="center" style="margin-top: -80px">
                        </div>
                </section>
            </div>


            <uc2:uc_bottom ID="uc_bottom" runat="server" />

            <asp:Label ID="Label3" runat="server"></asp:Label>
            <asp:HiddenField ID="TextBox5" runat="server" />
            <asp:HiddenField ID="searchHF" runat="server" />
            <asp:HiddenField ID="startdateHF" runat="server" />
            <asp:HiddenField ID="enddateHF" runat="server" />


            <div style="width: 100%; float: right">
                <br />
                <br />
                <br />
                <br />


                <asp:HiddenField ID="hfImageData" runat="server" />
            </div>
        </div>




    </form>
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/adminlte.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap-datepicker.kr.js"></script>
    <script src='../Scripts/chart.min.js'></script>







</body>
</html>
