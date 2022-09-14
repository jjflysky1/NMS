<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="day_traffic.aspx.cs" Inherits="WebApplication1.day_traffic" %>
<%@ Register src="~/Common/leftmenu.ascx" tagname="uc_menu" tagprefix="uc1" %>
<%@ Register src="~/Common/bottom.ascx" tagname="uc_bottom" tagprefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
    <link rel="stylesheet"  href="../Scripts/datepicker3.min.css" />
    

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
<body class="hold-transition skin-blue sidebar-mini" >
    <form id="form1" runat="server" style="height:100%;" >
   <div class="wrapper"> 
   <uc1:uc_menu ID="uc_menu" runat="server" />
       <div class="content-wrapper">
   
            <section class="content-header" style="margin-bottom:-60px;">
              <h1>
                일일트래픽
                <small>일일트래픽들이 보여집니다</small>
              </h1>
              <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                <li class="active"><a href="#">일일트래픽</a></li>
              </ol>
            </section>


    <section class="content">
           <div class="right" >
            <%--<div id="navbar">
                <nav class="navbar navbar-default navbar-static-top" role="navigation">
                    <div style="text-align: right; margin-right: 20px">
                        <a href="Default.aspx">Logout</a>
                    </div>
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar-collapse-1">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand" href="main.aspx">Manage</a>
                    </div>

                    <div class="collapse navbar-collapse" id="navbar-collapse-1">
                        <ul class="nav navbar-nav">
                            <li><a href="webform1.aspx">1. 서비스 복구 리스트</a></li>
                            <li class="dropdown">
                                <a href="Setting.aspx" class="dropdown-toggle" data-toggle="dropdown">2. 로그 리스트 <b class="caret"></b></a>
                                <ul class="dropdown-menu" >
                                    <li ><a href="WebForm2.aspx"> 서비스 로그</a></li>
                                    <li class="divider"></li>
                                    <li><a href="mail_setting.aspx"> 시스템 로그</a></li>
                                </ul>
                            </li>
                            <li><a href="user.aspx">3. 사용자 정보</a></li>
                            <li class="dropdown">
                                <a href="Setting.aspx" class="dropdown-toggle" data-toggle="dropdown">4. 설정 <b class="caret"></b></a>
                                <ul class="dropdown-menu" >
                                    <li ><a href="network_Setting.aspx">네트워크 설정</a></li>
                                    <li class="divider"></li>
                                    <li><a href="mail_setting.aspx">메일 설정</a></li>
                                </ul>
                            </li>
                            <li><a href="http://www.sungsimit.co.kr">5. 고객 기술지원</a></li>
                        </ul>

                    </div>
                    <!-- /.navbar-collapse -->

                </nav>
            </div>--%>
            <div class="container-fluid">
 
                <nav class="navbar " >
    	  <div class="">
		    <!-- Brand and toggle get grouped for better mobile display -->
		    <div class="navbar-btn " style="margin-left:10px;margin-right: 10px" >
                <ui class="nav navbar-nav navbar-left">
                     
                    <li>
                     <%--  <Button ID="Button1" runat="server"   OnserverClick="Button6_Click" Class="btn btn-primary btn-sm">
                              <span class="glyphicon glyphicon-save-file" style='color:white;'></span> 엑셀 저장
                        </button>--%>
                    </li>
                </ui>
                <ui class="nav navbar-nav navbar-right">
                    <li style="margin-top:5px; margin-right:10px">
                        
                    </li>
                    <li style=" margin-right:10px">
                        
                        
                    </li>
                      <li style="margin-top:5px; margin-right:10px">
                          
                          </li>
                    <li style=" margin-right:10px; visibility:hidden">
                          <asp:TextBox ID="enddate" runat="server" Class="form-control " Width="100px" ></asp:TextBox>
                        
                    </li>
                   <%-- <li style="margin-top:5px; margin-right:10px">
                        <asp:Label ID="Label4" runat="server" Text="구분 : " CssClass="navbar-left" ></asp:Label>
                    </li>
                    <li style=" margin-right:10px">
                        <asp:DropDownList ID="DropDownList1" runat="server" Height="30"  Class="form-control" Width="100px">
                        <asp:ListItem>선택</asp:ListItem>
                        <asp:ListItem Value="1">아이피</asp:ListItem>
                        </asp:DropDownList>
                    </li>--%>


                 <%--  <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label5" runat="server" Text="검색 : " CssClass="navbar-left"></asp:Label>
                   </li>
                    <li style="margin-right:10px">
                       <asp:TextBox Height="30" ID="Search" runat="server" Class="form-control" Width="100px"></asp:TextBox>
                   </li>--%>
                    <li style=" margin-right:10px">
                       <%--<asp:Button ID="Button3" runat="server" Text="검색" CssClass="btn btn-primary btn-sm" OnClick="Button3_Click" />--%>
                   </li>
                </ui>
            </div>
              </div>
                </nav>


    

                </div>

            <div class="container-fluid">
          
           
                <%--<div style=" margin-top:-6px; margin-bottom:10px; margin-right:10px; float:left; " >
                    <asp:RadioButton ID="RadioButton1" GroupName="log"  runat="server" Text=" 30분"  OnCheckedChanged="RadioButton1_CheckedChanged"  />
                    <asp:RadioButton ID="RadioButton2" GroupName="log"  runat="server" Text=" 1시간"  OnCheckedChanged="RadioButton2_CheckedChanged" />
                </div>--%>
                <div style=" margin-top:0px; margin-bottom:10px; float:left;">
                    <asp:Button ID="Button2" runat="server" Text="이전" CssClass="btn btn-primary btn-sm" OnClick="Button2_Click" />
                    <%--<asp:Button ID="btnExport" Text="Export to JPG" Class="btn btn-primary btn-sm" runat="server" UseSubmitBehavior="false" OnClick="ExportToImage" OnClientClick="return ConvertToImage(this)" />--%>

                </div>
                
                <div  style=" float:right; margin-right: 20px;">
                    <asp:TextBox ID="startdate" runat="server" Class="form-control " Width="100px"  AutoPostBack="true"></asp:TextBox>
                </div>
                <div style=" float:right; margin-right: 10px; margin-top:8px;">
                    <asp:Label ID="Label6" runat="server" Text="기간 : " CssClass="navbar-left"  ></asp:Label>
                </div>
                
                <div class="pdfview" id="pdfview" runat="server" style=" height:500px;  width: 100%">
                <br />
                  <section class="content-header">
                  <h4>
                   <label id="title" runat="server" visible="false" ></label>
                  </h4>
                     </section>
                <div style="margin-top:0px;padding-right:10px;  width:48% ; float:left; text-align:right" class="box box-info">
                    <b><center>1분 트래픽</center></b>
                <div id="div100" class="div100" style="width:100%;  height:210px; margin-top:0px; ">
                        <canvas id="myChart" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>
                              </div>
    <script src='../Scripts/chart.min.js'></script>
    <script>
        $(function () {
                             var serverip = document.getElementById("<%=serverip.ClientID %>").value;
                             var startdate = document.getElementById("<%=startdate.ClientID %>").value;
                             var enddate = document.getElementById("<%=enddate.ClientID %>").value;
      
         
         $.ajax({
                type: "POST",
                url: "day_traffic.aspx/chart1",
                data: "{'serverip':'" + serverip + "', 'startdate':'" + startdate + "', 'enddate':'" + enddate + "'}",
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
             },
             timeout: 60000
            });
        });

        setInterval(function () {
              var serverip = document.getElementById("<%=serverip.ClientID %>").value;
              var startdate = document.getElementById("<%=startdate.ClientID %>").value;
              var enddate = document.getElementById("<%=enddate.ClientID %>").value;
             $.ajax({
                type: "POST",
                url: "day_traffic.aspx/chart1",
                data: "{'serverip':'" + serverip + "', 'startdate':'" + startdate + "', 'enddate':'" + enddate + "'}",
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
                 },
                 timeout: 60000
            });
      
            $.ajax({
                type: "POST",
                url: "day_traffic.aspx/chart2",
                data: "{'serverip':'" + serverip + "', 'startdate':'" + startdate + "', 'enddate':'" + enddate + "'}",
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
                },
                timeout: 60000
            });

            $.ajax({
                type: "POST",
                url: "day_traffic.aspx/chart3",
                data: "{'serverip':'" + serverip + "', 'startdate':'" + startdate + "', 'enddate':'" + enddate + "'}",
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
                },
                timeout: 60000
            });

            $.ajax({
                type: "POST",
                url: "day_traffic.aspx/chart4",
                data: "{'serverip':'" + serverip + "', 'startdate':'" + startdate + "', 'enddate':'" + enddate + "'}",
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
                },
                timeout: 60000
            });
        },1 * 10 * 10000);

    function OnSuccess(response) {
        var customers = response.d;
        var temp_traffic = "";
        var serverip = "";
        var temp_time = "";
        var count = 0;
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
        document.getElementById('<%=Label40.ClientID %>').innerHTML = max;

function addData(chart, data) {
  chart.data.datasets.forEach(dataset => {
    let data = dataset.data;
    const first = data.shift();
    data.push(first);
    dataset.data = data;
  });

  chart.update();
 }


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
      bodyFontSize:	12,
      bodyFontStyle:	'400',
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
    scales: {
        xAxes: [
       
          {
              
          ticks: {
                autoSkip: true,
            maxTicksLimit: 20
            
          }
        }
      ]
    }
  }
});

    };
        
    </script>
                    최대값<br />
                    <asp:Label ID="Label40" runat="server" Text="Label" Font-Size="Smaller"></asp:Label> <font size="2">MB/s</font><br />
                    
                    
                </div>

                <div style="margin-left:30px; padding-right:10px; margin-top:0px; width:48%; float:left;text-align:right " class="box box-info">
                    <b><center>5분 트래픽</center></b>
                    <div id="div200" class="div200"   style="width:100%;  height:210px; margin-top:0px; ">
                        <canvas id="myChart2" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>
                              </div>
    <script src='../Scripts/chart.min.js'></script>
    <script>
$(function () {
                             var serverip = document.getElementById("<%=serverip.ClientID %>").value;
                             var startdate = document.getElementById("<%=startdate.ClientID %>").value;
                             var enddate = document.getElementById("<%=enddate.ClientID %>").value;
      
           var randomColorPlugin = {
    // We affect the `beforeUpdate` event
    beforeUpdate: function(chart) {
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
Chart.pluginService.register(randomColorPlugin);


         $.ajax({
                type: "POST",
                url: "day_traffic.aspx/chart2",
                data: "{'serverip':'" + serverip + "', 'startdate':'" + startdate + "', 'enddate':'" + enddate + "'}",
                contentType: "application/json; charset=utf-8",
             dataType: "json",
             async: true,
             cache: false,
                success: OnSuccess2,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
             },
             timeout: 60000
            });
        });

      

      
        function OnSuccess2(response) {
        var customers = response.d;
        var temp_traffic = "";
        var serverip = "";
        var temp_time = "";
        var count = 0;
         $(customers).each(function () {
             count++;
        });
        $(customers).each(function () {
            serverip = this.serverip2;
            temp_traffic += this.traffic2 + ",";   
            temp_time += this.time2 + ",";
            
        });
        temp_traffic = temp_traffic.slice(0, -1);
        temp_time = temp_time.slice(0, -1);
        var trafficArray = temp_traffic.split(',');
        var timeArray = temp_time.split(',');
        trafficArray.reverse();
        timeArray.reverse();
        let data2 = trafficArray;
        //alert(data2);
        max = Math.max.apply(null, trafficArray);
        document.getElementById('<%=Label43.ClientID %>').innerHTML = max;

////function addData(chart, data) {
////  chart.data.datasets.forEach(dataset => {
////    let data = dataset.data;
////    const first = data.shift();
////    data.push(first);
////    dataset.data = data;
////  });

////  chart.update();
//// }

            var ctx2 = "";
        var myChart2 = "";
        $('#div200').html(''); //remove canvas from container
        $('#div200').html('<canvas id="myChart2" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>'); //add it back to the container
 ctx2 = document.getElementById("myChart2").getContext("2d");
 myChart2 = new Chart(ctx2, {
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
      bodyFontSize:	12,
      bodyFontStyle:	'400',
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
    scales: {
        xAxes: [
       
          {
              
          ticks: {
                autoSkip: true,
            maxTicksLimit: 20
            
          }
        }
      ]
    }
  }
});

    };
        
    </script>
                    최대값<br />
                    <asp:Label ID="Label43" runat="server" Text="Label" Font-Size="Smaller"></asp:Label> <font size="2">MB/s</font><br />
                    
                </div>

                <div style="margin-top:0px;padding-right:10px;  width:48% ; float:left;text-align:right" class="box box-info">
                    <b><center>30분 트래픽</center></b>
               <div id="div300" class="div300" style="width:100%;  height:210px; margin-top:0px; ">
                        <canvas id="myChart3" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>
                              </div>
    <script src='../Scripts/chart.min.js'></script>
    <script>
$(function () {
                             var serverip = document.getElementById("<%=serverip.ClientID %>").value;
                             var startdate = document.getElementById("<%=startdate.ClientID %>").value;
                             var enddate = document.getElementById("<%=enddate.ClientID %>").value;
      
         
         $.ajax({
                type: "POST",
                url: "day_traffic.aspx/chart3",
                data: "{'serverip':'" + serverip + "', 'startdate':'" + startdate + "', 'enddate':'" + enddate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
             async: true,
             cache: false,
                success: OnSuccess3,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
             },
             timeout: 60000
            });
        });

    function OnSuccess3(response) {
        var customers = response.d;
        var temp_traffic = "";
        var serverip = "";
        var temp_time = "";
        var count = 0;
         $(customers).each(function () {
             count++;
        });
        $(customers).each(function () {
            serverip = this.serverip3;
            temp_traffic += this.traffic3 + ",";   
            temp_time += this.time3 + ",";
            
        });
        temp_traffic = temp_traffic.slice(0, -1);
        temp_time = temp_time.slice(0, -1);
        var trafficArray = temp_traffic.split(',');
        var timeArray = temp_time.split(',');
        trafficArray.reverse();
        timeArray.reverse();
        let data3 = trafficArray;
        //alert(data2);
        max = Math.max.apply(null, trafficArray);
        document.getElementById('<%=Label46.ClientID %>').innerHTML = max;

function addData(chart, data) {
  chart.data.datasets.forEach(dataset => {
    let data = dataset.data;
    const first = data.shift();
    data.push(first);
    dataset.data = data;
  });

  chart.update();
 }


          var ctx = "";
        var myChart = "";
          $('#div300').html(''); //remove canvas from container
        $('#div300').html('<canvas id="myChart3" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>'); //add it back to the container
 ctx = document.getElementById("myChart3").getContext("2d");
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
      bodyFontSize:	12,
      bodyFontStyle:	'400',
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
    scales: {
        xAxes: [
       
          {
              
          ticks: {
                autoSkip: true,
            maxTicksLimit: 20
            
          }
        }
      ]
    }
  }
});

    };
        
    </script>
                    최대값<br />
                    <asp:Label ID="Label46" runat="server" Text="Label" Font-Size="Smaller"></asp:Label> <font size="2">MB/s</font><br />
                    
                </div>

                <div style="margin-left:30px; padding-right:10px;  margin-top:0px; width:48%; float:left;text-align:right" class="box box-info">
                    <b><center>1시간 트래픽</center></b>
                    <div id="div400" class="div400" style="width:100%;  height:210px; margin-top:0px; ">
                        <canvas id="myChart4" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>
                              </div>
    <script src='../Scripts/chart.min.js'></script>
    <script>
$(function () {
                             var serverip = document.getElementById("<%=serverip.ClientID %>").value;
                             var startdate = document.getElementById("<%=startdate.ClientID %>").value;
                             var enddate = document.getElementById("<%=enddate.ClientID %>").value;
      
         
         $.ajax({
                type: "POST",
                url: "day_traffic.aspx/chart4",
                data: "{'serverip':'" + serverip + "', 'startdate':'" + startdate + "', 'enddate':'" + enddate + "'}",
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
                },
                timeout: 60000
            });
        });

    function OnSuccess4(response) {
        var customers = response.d;
        var temp_traffic = "";
        var serverip = "";
        var temp_time = "";
        var count = 0;
         $(customers).each(function () {
             count++;
        });
        $(customers).each(function () {
            serverip = this.serverip4;
            temp_traffic += this.traffic4 + ",";   
            temp_time += this.time4 + ",";
            
        });
        temp_traffic = temp_traffic.slice(0, -1);
        temp_time = temp_time.slice(0, -1);
        var trafficArray = temp_traffic.split(',');
        var timeArray = temp_time.split(',');
        trafficArray.reverse();
        timeArray.reverse();
        let data4 = trafficArray;
        //alert(data2);
        max = Math.max.apply(null, trafficArray);
        document.getElementById('<%=Label49.ClientID %>').innerHTML = max;

            function addData(chart, data) {
                chart.data.datasets.forEach(dataset => {
                    let data = dataset.data;
                    const first = data.shift();
                    data.push(first);
                    dataset.data = data;
                });

                chart.update();
            }


            var ctx = "";
            var myChart = "";
            $('#div400').html(''); //remove canvas from container
            $('#div400').html('<canvas id="myChart4" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>'); //add it back to the container
            ctx = document.getElementById("myChart4").getContext("2d");
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
                    scales: {
                        xAxes: [

                            {

                                ticks: {
                                    autoSkip: true,
                                    maxTicksLimit: 20

                                }
                            }
                        ]
                    }
                }
            });

        };

    </script>
                    최대값<br />
                    <asp:Label ID="Label49" runat="server" Text="Label" Font-Size="Smaller"></asp:Label> <font size="2">MB/s</font><br />
                    
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
                <asp:HiddenField ID="serverip" runat="server" />

                <asp:HiddenField ID="hfImageData" runat="server" />
       </div>


        
        
       </form>
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/adminlte.min.js"></script>
       <script type="text/javascript" src="../Scripts/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap-datepicker.kr.js"></script>
       
       
       
       

        

</body>
</html>
