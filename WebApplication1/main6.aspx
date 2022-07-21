<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="main6.aspx.cs" Inherits="WebApplication1.main6" %>

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

    <link rel="stylesheet" href="Content/font-awesome.min.css" />


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


    <script src="Scripts/gauge.js"></script>


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
            
            <div class="content-wrapper" style="background:white; ">
                <section class="content-header" style="margin-top: -15px;">
                   <%-- <font color="black"><h1>Dashboard
                        <small>Version 2.0</small>
                    </h1></font>--%>
                    <%--<asp:Button  ID="Button12" runat="server" Text="Putty 접속" CssClass="btn btn-link btn-sm navbar-left" OnClick="Button12_Click"  />--%>
                    
                  <%--  <ol class="breadcrumb" >
                        <li><font color="black"><i class="fa fa-dashboard"></i>Home</li>
                        <li class="active">Dashboard</font></li>
                    </ol>
                        --%>
                </section>
                
                 
                <br />

                <section class="content">
                    <div class="row">
                        <%--<div id="maindounut" style="width:100%; margin-bottom:20px;  ">      
               
        </div>  --%>
            <style>
                             .grad {
                                 /*background-image:linear-gradient(356deg, rgba(2,0,36,1) 0%, rgba(9,9,121,1) 55%, rgba(0,212,255,1) 100%)}*/
                                 /*background: linear-gradient(to right, #0f2027, #203a43, #2c5364);*/
                                 background: rgb(25,34,55);
                             }
                             .grad2{
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
                                border-radius: .25rem;   
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
                         <script>  
                             $(document).ready(function () {
                                 $("#btn1").click(function () {
                                     var x = $('.content').width() - 30;
                                     $("#box1").animate({ height: "100%;", width: x });
                                     $("#box1").css('z-index', 100);
                                     $("#box1").css('position', "absolute");
                                     $("#btn100").css('visibility', "visible");
                                 });
                                 $("#btn1-1").click(function () {
                                     $("#box1").animate({ height: "100%;", width: "31%" });
                                     $("#box1").css('z-index', 99);
                                     $("#box1").css('position', "");
                                     $("#btn100").css('visibility', "hidden");
                                 });
                                 $("#btn2").click(function () {
                                     var x = $('.content').width() - 30;
                                     $("#box2").animate({ height: "100%;", width: x });
                                     $("#box2").css('z-index', 100);
                                     $("#box2").css('position', "absolute");
                                     $("#btn101").css('visibility', "visible");
                                 });
                                 $("#btn2-1").click(function () {
                                     $("#box2").animate({ height: "100%;", width: "31%" });
                                     $("#box2").css('z-index', 99);
                                     $("#box2").css('position', "");
                                     $("#btn101").css('visibility', "hidden");
                                 });
                                 $("#btn3").click(function () {
                                     var x = $('.content').width() - 30;
                                     $("#box3").animate({ height: "100%;", width: x });
                                     $("#box3").css('z-index', 100);
                                     $("#box3").css('position', "absolute");
                                     $("#btn102").css('visibility', "visible");
                                 });
                                 $("#btn3-1").click(function () {
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


                                 function downloadURI(uri, name) {
                                     var link = document.createElement("a");
                                     link.download = name;
                                     link.href = uri;
                                     document.body.appendChild(link);
                                     link.click();
                                 }
                             });
                         </script>

                  
                        <!-- 전체 -->
                        <div class="col-xl-3 col-md-3 mb-1">
                          <div class="card border-left-primary shadow h-100 py-2">
                            <div class="card-body">
                              <div class="row no-gutters align-items-center" >
                                <div class="col mr-2" style="margin-left:5%;">
                                  <div class="text-s font-weight-bold text-primary text-uppercase mb-1">전체</div>
                                  <div class="h5 mb-0 font-weight-bold text-gray-800">158</div>
                                </div>
                              </div>
                            </div>
                          </div>
                        </div>
             

                        <!-- 서버 -->
                        <div class="col-xl-3 col-md-3 mb-4">
                          <div class="card border-left-success shadow h-100 py-2">
                            <div class="card-body">
                              <div class="row no-gutters align-items-center">
                                <div class="col mr-2"  style="margin-left:5%;">
                                  <div class="text-s font-weight-bold text-success text-uppercase mb-1">서버</div>
                                  <div class="h5 mb-0 font-weight-bold text-gray-800">20</div>
                                </div>
                              </div>
                            </div>
                          </div>
                        </div>

                        <!-- 모바일 -->
                        <div class="col-xl-3 col-md-3 mb-4">
                          <div class="card border-left-success shadow h-100 py-2">
                            <div class="card-body">
                              <div class="row no-gutters align-items-center">
                                <div class="col mr-2"  style="margin-left:5%;">
                                  <div class="text-s font-weight-bold text-success text-uppercase mb-1">모바일</div>
                                  <div class="h5 mb-0 font-weight-bold text-gray-800">120</div>
                                </div>
                              </div>
                            </div>
                          </div>
                        </div>

                        <!-- 장애 -->
                        <div class="col-xl-3 col-md-3 mb-4">
                          <div class="card border-left-danger shadow h-100 py-2">
                            <div class="card-body">
                              <div class="row no-gutters align-items-center">
                                <div class="col mr-2"  style="margin-left:5%;">
                                  <div class="text-s font-weight-bold text-danger text-uppercase mb-1">장애</div>
                                  <div class="h5 mb-0 font-weight-bold text-gray-800">18</div>
                                </div>
                              </div>
                            </div>
                          </div>
                        </div>
                   


                    <!--접속갯수-->
                           <div class="col-xl-8 col-lg-12">
                            <div class="card shadow mb-4">
                                <!-- Card Header - Dropdown -->
                                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                                  <h6 class="m-0 font-weight-bold text-primary">접속</h6>
                                </div>
                                <!-- Card Body -->
                                <div class="card-body">
                                  <div class="chart-area">
                                    <div id="chartDiv11" style="width: 20%; float:left; "></div>
                                      <div id="chartDiv12" style="width: 20%; float:left; "></div>
                                      <div id="chartDiv13" style="width: 20%; float:left; "></div>
                                      <div id="chartDiv14" style="width: 20%; float:left; "></div>
                                      <div id="chartDiv15" style="width: 20%; float:left; "></div>
                                  </div>
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
                                  success: OnSuccess11,
                                  failure: function (response) {
                                      alert(response.d);
                                  },
                                  error: function (response) {
                                      alert(response.d);
                                  }
                              });

                          });
                          function init6() {
                              window.ref = window.setInterval(function () { rewrite6(); }, 10000);
                          }
                          function rewrite6() {
                              clearInterval(window.ref);
                              $.ajax({
                                  type: "POST",
                                  url: "/Chart/Chart2.aspx/chart5",
                                  data: '{}',
                                  contentType: "application/json; charset=utf-8",
                                  dataType: "json",
                                  async: true,
                                  cache: false,
                                  success: OnSuccess11,
                                  failure: function (response) {
                                      //alert(response.d);
                                  },
                                  error: function (response) {
                                      //alert(response.d);
                                  }
                              });
                              init6();
                          }
                          init6();

                          function OnSuccess11(response) {
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
                        
                              
                              //memory
                              var ctx11 = "";
                              var myChart11 = "";
                              $('#chartDiv11').html(''); //remove canvas from container
                              $('#myChart200').remove(); // this is my <canvas> element
                              $('#chartDiv11').html('<canvas id="myChart200" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>'); //add it back to the container
                              ctx11 = document.getElementById("myChart200").getContext("2d");
                              gradientFill = ctx11.createLinearGradient(0, 0, 0, 450);
                              gradientFill.addColorStop(0, 'rgba(15, 220, 99, 0.3)');
                              gradientFill.addColorStop(0.5, 'rgba(15, 220, 99, 0.1)');
                              gradientFill.addColorStop(1, 'rgba(15, 220, 99, 0)');
                              myChart11 = new Chart(ctx11, {
                                  //plugins: [randomColorPlugin],
                                  type: "horizontalBar",
                                  data: {
                                      labels: [timeArray4[0]],
                                      datasets: [
                                          {
                                              label: serverip4,
                                              data: [memoryArray4[0]],
                                              backgroundColor: gradientFill,
                                              borderColor: ["rgba(15, 220, 99, 1)"],
                                              borderWidth: 1,
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
                                                  const o = d.datasets.map((ds) => ds.data[t[0].index] + " 대    ")

                                                  return o.join(', ');
                                              },
                                              label: function (t, d) {
                                                  return d.labels[t.index];
                                              }
                                          }
                                      },
                                      title: {
                                          text: "사용댓수",
                                          display: true,
                                          fontColor: "black",
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
                                              fontColor: "black",
                                              fontSize: 11
                                          }
                                      },
                                      scales: {
                                          xAxes: [
                                              {
                                                  ticks: {
                                                      fontColor: "black",
                                                      autoSkip: true,
                                                      maxTicksLimit: 20,
                                                      min: 0, // minimum value
                                                      max: 100 // maximum value
                                                  },
                                                  gridLines: {
                                                      display: false,
                                                      drawOnChartArea: false,
                                                  }
                                              }
                                          ],
                                          yAxes: [
                                              {
                                                  ticks: {
                                                      fontColor: "black",
                                                      min: 0, // minimum value
                                                      max: 100 // maximum value
                                                  },
                                                  gridLines: {
                                                      display: false,
                                                      drawOnChartArea: false,
                                                  }
                                              }
                                          ]
                                      }
                                  }
                              });

                              //memory
                              var ctx11 = "";
                              var myChart11 = "";
                              $('#chartDiv12').html(''); //remove canvas from container
                              $('#myChart202').remove(); // this is my <canvas> element
                              $('#chartDiv12').html('<canvas id="myChart202" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>'); //add it back to the container
                              ctx11 = document.getElementById("myChart202").getContext("2d");
                              gradientFill = ctx11.createLinearGradient(0, 0, 0, 450);
                              gradientFill.addColorStop(0, 'rgba(15, 220, 99, 0.3)');
                              gradientFill.addColorStop(0.5, 'rgba(15, 220, 99, 0.1)');
                              gradientFill.addColorStop(1, 'rgba(15, 220, 99, 0)');
                              myChart11 = new Chart(ctx11, {
                                  //plugins: [randomColorPlugin],
                                  type: "horizontalBar",
                                  data: {
                                      labels: [timeArray4[1]],
                                      datasets: [
                                          {
                                              label: serverip4,
                                              data: [memoryArray4[1]],
                                              backgroundColor: gradientFill,
                                              borderColor: ["rgba(15, 220, 99, 1)"],
                                              borderWidth: 1,
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
                                                  const o = d.datasets.map((ds) => ds.data[t[0].index] + " 대    ")

                                                  return o.join(', ');
                                              },
                                              label: function (t, d) {
                                                  return d.labels[t.index];
                                              }
                                          }
                                      },
                                      title: {
                                          text: "사용댓수",
                                          display: true,
                                          fontColor: "black",
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
                                              fontColor: "black",
                                              fontSize: 11
                                          }
                                      },
                                      scales: {
                                          xAxes: [
                                              {
                                                  ticks: {
                                                      fontColor: "black",
                                                      autoSkip: true,
                                                      maxTicksLimit: 20,
                                                      min: 0, // minimum value
                                                      max: 100 // maximum value
                                                  },
                                                  gridLines: {
                                                      display: false,
                                                      drawOnChartArea: false,
                                                  }
                                              }
                                          ],
                                          yAxes: [
                                              {
                                                  ticks: {
                                                      fontColor: "black",
                                                      min: 0, // minimum value
                                                      max: 100 // maximum value
                                                  },
                                                  gridLines: {
                                                      display: false,
                                                      drawOnChartArea: false,
                                                  }
                                              }
                                          ]
                                      }
                                  }
                              });

                              //memory
                              var ctx11 = "";
                              var myChart11 = "";
                              $('#chartDiv13').html(''); //remove canvas from container
                              $('#myChart203').remove(); // this is my <canvas> element
                              $('#chartDiv13').html('<canvas id="myChart203" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>'); //add it back to the container
                              ctx11 = document.getElementById("myChart203").getContext("2d");
                              gradientFill = ctx11.createLinearGradient(0, 0, 0, 450);
                              gradientFill.addColorStop(0, 'rgba(15, 220, 99, 0.3)');
                              gradientFill.addColorStop(0.5, 'rgba(15, 220, 99, 0.1)');
                              gradientFill.addColorStop(1, 'rgba(15, 220, 99, 0)');
                              myChart11 = new Chart(ctx11, {
                                  //plugins: [randomColorPlugin],
                                  type: "horizontalBar",
                                  data: {
                                      labels: [timeArray4[2]],
                                      datasets: [
                                          {
                                              label: serverip4,
                                              data: [memoryArray4[2]],
                                              backgroundColor: gradientFill,
                                              borderColor: ["rgba(15, 220, 99, 1)"],
                                              borderWidth: 1,
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
                                                  const o = d.datasets.map((ds) => ds.data[t[0].index] + " 대    ")

                                                  return o.join(', ');
                                              },
                                              label: function (t, d) {
                                                  return d.labels[t.index];
                                              }
                                          }
                                      },
                                      title: {
                                          text: "사용댓수",
                                          display: true,
                                          fontColor: "black",
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
                                              fontColor: "black",
                                              fontSize: 11
                                          }
                                      },
                                      scales: {
                                          xAxes: [
                                              {
                                                  ticks: {
                                                      fontColor: "black",
                                                      autoSkip: true,
                                                      maxTicksLimit: 20,
                                                      min: 0, // minimum value
                                                      max: 100 // maximum value
                                                  },
                                                  gridLines: {
                                                      display: false,
                                                      drawOnChartArea: false,
                                                  }
                                              }
                                          ],
                                          yAxes: [
                                              {
                                                  ticks: {
                                                      fontColor: "black",
                                                      min: 0, // minimum value
                                                      max: 100 // maximum value
                                                  },
                                                  gridLines: {
                                                      display: false,
                                                      drawOnChartArea: false,
                                                  }
                                              }
                                          ]
                                      }
                                  }
                              });

                              //memory
                              var ctx11 = "";
                              var myChart11 = "";
                              $('#chartDiv14').html(''); //remove canvas from container
                              $('#myChart204').remove(); // this is my <canvas> element
                              $('#chartDiv14').html('<canvas id="myChart204" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>'); //add it back to the container
                              ctx11 = document.getElementById("myChart204").getContext("2d");
                              gradientFill = ctx11.createLinearGradient(0, 0, 0, 450);
                              gradientFill.addColorStop(0, 'rgba(15, 220, 99, 0.3)');
                              gradientFill.addColorStop(0.5, 'rgba(15, 220, 99, 0.1)');
                              gradientFill.addColorStop(1, 'rgba(15, 220, 99, 0)');
                              myChart11 = new Chart(ctx11, {
                                  //plugins: [randomColorPlugin],
                                  type: "horizontalBar",
                                  data: {
                                      labels: [timeArray4[3]],
                                      datasets: [
                                          {
                                              label: serverip4,
                                              data: [memoryArray4[3]],
                                              backgroundColor: gradientFill,
                                              borderColor: ["rgba(15, 220, 99, 1)"],
                                              borderWidth: 1,
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
                                                  const o = d.datasets.map((ds) => ds.data[t[0].index] + " 대    ")

                                                  return o.join(', ');
                                              },
                                              label: function (t, d) {
                                                  return d.labels[t.index];
                                              }
                                          }
                                      },
                                      title: {
                                          text: "사용댓수",
                                          display: true,
                                          fontColor: "black",
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
                                              fontColor: "black",
                                              fontSize: 11
                                          }
                                      },
                                      scales: {
                                          xAxes: [
                                              {
                                                  ticks: {
                                                      fontColor: "black",
                                                      autoSkip: true,
                                                      maxTicksLimit: 20,
                                                      min: 0, // minimum value
                                                      max: 100 // maximum value
                                                  },
                                                  gridLines: {
                                                      display: false,
                                                      drawOnChartArea: false,
                                                  }
                                              }
                                          ],
                                          yAxes: [
                                              {
                                                  ticks: {
                                                      fontColor: "black",
                                                      min: 0, // minimum value
                                                      max: 100 // maximum value
                                                  },
                                                  gridLines: {
                                                      display: false,
                                                      drawOnChartArea: false,
                                                  }
                                              }
                                          ]
                                      }
                                  }
                              });

                              //memory
                              var ctx11 = "";
                              var myChart11 = "";
                              $('#chartDiv15').html(''); //remove canvas from container
                              $('#myChart205').remove(); // this is my <canvas> element
                              $('#chartDiv15').html('<canvas id="myChart205" style="z-index:200; position:relative;  width:100%;  height: 100%;  "></canvas>'); //add it back to the container
                              ctx11 = document.getElementById("myChart205").getContext("2d");
                              gradientFill = ctx11.createLinearGradient(0, 0, 0, 450);
                              gradientFill.addColorStop(0, 'rgba(15, 220, 99, 0.3)');
                              gradientFill.addColorStop(0.5, 'rgba(15, 220, 99, 0.1)');
                              gradientFill.addColorStop(1, 'rgba(15, 220, 99, 0)');
                              myChart11 = new Chart(ctx11, {
                                  //plugins: [randomColorPlugin],
                                  type: "horizontalBar",
                                  data: {
                                      labels: [timeArray4[4]],
                                      datasets: [
                                          {
                                              label: serverip4,
                                              data: [memoryArray4[4]],
                                              backgroundColor: gradientFill,
                                              borderColor: ["rgba(15, 220, 99, 1)"],
                                              borderWidth: 1,
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
                                                  const o = d.datasets.map((ds) => ds.data[t[0].index] + " 대    ")

                                                  return o.join(', ');
                                              },
                                              label: function (t, d) {
                                                  return d.labels[t.index];
                                              }
                                          }
                                      },
                                      title: {
                                          text: "사용댓수",
                                          display: true,
                                          fontColor: "black",
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
                                              fontColor: "black",
                                              fontSize: 11
                                          }
                                      },
                                      scales: {
                                          xAxes: [
                                              {
                                                  ticks: {
                                                      fontColor: "black",
                                                      autoSkip: true,
                                                      maxTicksLimit: 20,
                                                      min: 0, // minimum value
                                                      max: 100 // maximum value
                                                  },
                                                  gridLines: {
                                                      display: false,
                                                      drawOnChartArea: false,
                                                  }
                                              }
                                          ],
                                          yAxes: [
                                              {
                                                  ticks: {
                                                      fontColor: "black",
                                                      min: 0, // minimum value
                                                      max: 100 // maximum value
                                                  },
                                                  gridLines: {
                                                      display: false,
                                                      drawOnChartArea: false,
                                                  }
                                              }
                                          ]
                                      }
                                  }
                              });
                          }
                      </script>


                     <!--cpu-->
                      <div class="col-xl-8 col-lg-12">
                            <div class="card shadow mb-4">
                                <!-- Card Header - Dropdown -->
                                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                                  <h6 class="m-0 font-weight-bold text-primary">CPU 사용률</h6>
                                </div>
                                <!-- Card Body -->
                                <div class="card-body">
                                  <div class="chart-area">
                                    <div id="chartDiv" style="width: 20%; float:left; "></div>
                                    <div id="chartDiv2" style="width: 20%; float:left;"></div>
                                    <div id="chartDiv3" style="width: 20%; float:left;"></div>
                                    <div id="chartDiv4" style="width: 20%; float:left;"></div>
                                    <div id="chartDiv5" style="width: 20%; float:left;"></div>
                                  </div>
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
                                        init3();
                                    }
                                    init3();
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


                                        $('#chartDiv').html(''); //remove canvas from container
                                        $('#myChart100').remove(); // this is my <canvas> element
                                        $('#chartDiv').html('<canvas id="myChart100" style="z-index:200; position:relative;  width:100%;    "></canvas>'); //add it back to the container
                                        var ctx2 = document.getElementById("myChart100").getContext("2d");
                                        new Chart(ctx2, {
                                            type: "tsgauge",
                                            data: {
                                                datasets: [{
                                                    label: timeArray4[0],
                                                    backgroundColor: ["#0fdc63", "#fd9704", "#ff7143"],
                                                    borderWidth: 0,
                                                    gaugeData: {
                                                        value: cpuArray4[0],
                                                        valueColor: "#ff7143"
                                                    },
                                                    gaugeLimits: [0, 50, 80, 100]
                                                }]
                                            },
                                            options: {
                                                events: [],
                                                showMarkers: true,
                                                animation: {
                                                    duration: 0
                                                },

                                                title: {
                                                    text: timeArray4[0],
                                                    display: true,
                                                    fontColor: "black",
                                                    fontSize: 15,
                                                    fontFamily: "Arial"
                                                }
                                            }
                                        });

                                        $('#chartDiv2').html(''); //remove canvas from container
                                        $('#myChart101').remove(); // this is my <canvas> element
                                        $('#chartDiv2').html('<canvas id="myChart101" style="z-index:200; position:relative;  width:100%;    "></canvas>'); //add it back to the container
                                        var ctx3 = document.getElementById("myChart101").getContext("2d");
                                        new Chart(ctx3, {
                                            type: "tsgauge",
                                            data: {
                                                datasets: [{
                                                    label: timeArray4[0],
                                                    backgroundColor: ["#0fdc63", "#fd9704", "#ff7143"],
                                                    borderWidth: 0,
                                                    gaugeData: {
                                                        value: cpuArray4[1],
                                                        valueColor: "#ff7143"
                                                    },
                                                    gaugeLimits: [0, 50, 80, 100]
                                                }]
                                            },
                                            options: {
                                                events: [],
                                                showMarkers: true,
                                                animation: {
                                                    duration: 0
                                                },

                                                title: {
                                                    text: timeArray4[1],
                                                    display: true,
                                                    fontColor: "black",
                                                    fontSize: 15,
                                                    fontFamily: "Arial"
                                                }
                                            }
                                        });

                                        $('#chartDiv3').html(''); //remove canvas from container
                                        $('#myChart102').remove(); // this is my <canvas> element
                                        $('#chartDiv3').html('<canvas id="myChart102" style="z-index:200; position:relative;  width:100%;    "></canvas>'); //add it back to the container
                                        var ctx2 = document.getElementById("myChart102").getContext("2d");
                                        new Chart(ctx2, {
                                            type: "tsgauge",
                                            data: {
                                                datasets: [{
                                                    label: timeArray4[0],
                                                    backgroundColor: ["#0fdc63", "#fd9704", "#ff7143"],
                                                    borderWidth: 0,
                                                    gaugeData: {
                                                        value: cpuArray4[2],
                                                        valueColor: "#ff7143"
                                                    },
                                                    gaugeLimits: [0, 50, 80, 100]
                                                }]
                                            },
                                            options: {
                                                events: [],
                                                showMarkers: true,
                                                animation: {
                                                    duration: 0
                                                },

                                                title: {
                                                    text: timeArray4[2],
                                                    display: true,
                                                    fontColor: "black",
                                                    fontSize: 15,
                                                    fontFamily: "Arial"
                                                }
                                            }
                                        });

                                        $('#chartDiv4').html(''); //remove canvas from container
                                        $('#myChart103').remove(); // this is my <canvas> element
                                        $('#chartDiv4').html('<canvas id="myChart103" style="z-index:200; position:relative;  width:100%;    "></canvas>'); //add it back to the container
                                        var ctx2 = document.getElementById("myChart103").getContext("2d");
                                        new Chart(ctx2, {
                                            type: "tsgauge",
                                            data: {
                                                datasets: [{
                                                    label: timeArray4[3],
                                                    backgroundColor: ["#0fdc63", "#fd9704", "#ff7143"],
                                                    borderWidth: 0,
                                                    gaugeData: {
                                                        value: cpuArray4[3],
                                                        valueColor: "#ff7143"
                                                    },
                                                    gaugeLimits: [0, 50, 80, 100]
                                                }]
                                            },
                                            options: {
                                                events: [],
                                                showMarkers: true,
                                                animation: {
                                                    duration: 0
                                                },

                                                title: {
                                                    text: timeArray4[3],
                                                    display: true,
                                                    fontColor: "black",
                                                    fontSize: 15,
                                                    fontFamily: "Arial"
                                                }
                                            }
                                        });

                                        $('#chartDiv5').html(''); //remove canvas from container
                                        $('#myChart104').remove(); // this is my <canvas> element
                                        $('#chartDiv5').html('<canvas id="myChart104" style="z-index:200; position:relative;  width:100%;    "></canvas>'); //add it back to the container
                                        var ctx2 = document.getElementById("myChart104").getContext("2d");
                                        new Chart(ctx2, {
                                            type: "tsgauge",
                                            data: {
                                                datasets: [{
                                                    label: timeArray4[4],
                                                    backgroundColor: ["#0fdc63", "#fd9704", "#ff7143"],
                                                    borderWidth: 0,
                                                    gaugeData: {
                                                        value: cpuArray4[4],
                                                        valueColor: "#ff7143"
                                                    },
                                                    gaugeLimits: [0, 50, 80, 100]
                                                }]
                                            },
                                            options: {
                                                events: [],
                                                showMarkers: true,
                                                animation: {
                                                    duration: 0
                                                },

                                                title: {
                                                    text: timeArray4[4],
                                                    display: true,
                                                    fontColor: "black",
                                                    fontSize: 15,
                                                    fontFamily: "Arial"
                                                }
                                            }
                                        });
                                    }


                                </script>

                     <!--memory-->
                      <div class="col-xl-8 col-lg-12">
                            <div class="card shadow mb-4">
                                <!-- Card Header - Dropdown -->
                                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                                  <h6 class="m-0 font-weight-bold text-primary">메모리 사용률</h6>
                                </div>
                                <!-- Card Body -->
                                <div class="card-body">
                                  <div class="chart-area">
                                    <div id="chartDiv6" style="width: 20%; float:left; "></div>
                                    <div id="chartDiv7" style="width: 20%; float:left;"></div>
                                    <div id="chartDiv8" style="width: 20%; float:left;"></div>
                                    <div id="chartDiv9" style="width: 20%; float:left;"></div>
                                    <div id="chartDiv10" style="width: 20%; float:left;"></div>
                                  </div>
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
                        function init4() {
                            window.ref = window.setInterval(function () { rewrite4(); }, 10000);
                        }
                        function rewrite4() {
                            clearInterval(window.ref);
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
                                error: function (response) {
                                    //alert(response.d);
                                }
                            });
                            init4();
                        }
                        init4();

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


                            $('#chartDiv6').html(''); //remove canvas from container
                            $('#myChart105').remove(); // this is my <canvas> element
                            $('#chartDiv6').html('<canvas id="myChart105" style="z-index:200; position:relative;  width:100%;    "></canvas>'); //add it back to the container
                            var ctx2 = document.getElementById("myChart105").getContext("2d");
                            new Chart(ctx2, {
                                type: "tsgauge",
                                data: {
                                    datasets: [{
                                        backgroundColor: ["#0fdc63", "#fd9704", "#ff7143"],
                                        borderWidth: 0,
                                        gaugeData: {
                                            value: memoryArray4[0],
                                            valueColor: "#ff7143"
                                        },
                                        gaugeLimits: [0, 50, 80, 100]
                                    }]
                                },
                                options: {
                                    events: [],
                                    showMarkers: true,
                                    animation: {
                                        duration: 0
                                    },

                                    title: {
                                        text: timeArray4[0],
                                        display: true,
                                        fontColor: "black",
                                        fontSize: 15,
                                        fontFamily: "Arial"
                                    }
                                }
                            });

                            $('#chartDiv7').html(''); //remove canvas from container
                            $('#myChart106').remove(); // this is my <canvas> element
                            $('#chartDiv7').html('<canvas id="myChart106" style="z-index:200; position:relative;  width:100%;    "></canvas>'); //add it back to the container
                            var ctx3 = document.getElementById("myChart106").getContext("2d");
                            new Chart(ctx3, {
                                type: "tsgauge",
                                data: {
                                    datasets: [{
                                        label: timeArray4[0],
                                        backgroundColor: ["#0fdc63", "#fd9704", "#ff7143"],
                                        borderWidth: 0,
                                        gaugeData: {
                                            value: memoryArray4[1],
                                            valueColor: "#ff7143"
                                        },
                                        gaugeLimits: [0, 50, 80, 100]
                                    }]
                                },
                                options: {
                                    events: [],
                                    showMarkers: true,
                                    animation: {
                                        duration: 0
                                    },

                                    title: {
                                        text: timeArray4[1],
                                        display: true,
                                        fontColor: "black",
                                        fontSize: 15,
                                        fontFamily: "Arial"
                                    }
                                }
                            });

                            $('#chartDiv8').html(''); //remove canvas from container
                            $('#myChart107').remove(); // this is my <canvas> element
                            $('#chartDiv8').html('<canvas id="myChart107" style="z-index:200; position:relative;  width:100%;    "></canvas>'); //add it back to the container
                            var ctx2 = document.getElementById("myChart107").getContext("2d");
                            new Chart(ctx2, {
                                type: "tsgauge",
                                data: {
                                    datasets: [{
                                        label: timeArray4[0],
                                        backgroundColor: ["#0fdc63", "#fd9704", "#ff7143"],
                                        borderWidth: 0,
                                        gaugeData: {
                                            value: memoryArray4[2],
                                            valueColor: "#ff7143"
                                        },
                                        gaugeLimits: [0, 50, 80, 100]
                                    }]
                                },
                                options: {
                                    events: [],
                                    showMarkers: true,
                                    animation: {
                                        duration: 0
                                    },

                                    title: {
                                        text: timeArray4[2],
                                        display: true,
                                        fontColor: "black",
                                        fontSize: 15,
                                        fontFamily: "Arial"
                                    }
                                }
                            });

                            $('#chartDiv9').html(''); //remove canvas from container
                            $('#myChart108').remove(); // this is my <canvas> element
                            $('#chartDiv9').html('<canvas id="myChart108" style="z-index:200; position:relative;  width:100%;    "></canvas>'); //add it back to the container
                            var ctx2 = document.getElementById("myChart108").getContext("2d");
                            new Chart(ctx2, {
                                type: "tsgauge",
                                data: {
                                    datasets: [{
                                        label: timeArray4[3],
                                        backgroundColor: ["#0fdc63", "#fd9704", "#ff7143"],
                                        borderWidth: 0,
                                        gaugeData: {
                                            value: memoryArray4[3],
                                            valueColor: "#ff7143"
                                        },
                                        gaugeLimits: [0, 50, 80, 100]
                                    }]
                                },
                                options: {
                                    events: [],
                                    showMarkers: true,
                                    animation: {
                                        duration: 0
                                    },

                                    title: {
                                        text: timeArray4[3],
                                        display: true,
                                        fontColor: "black",
                                        fontSize: 15,
                                        fontFamily: "Arial"
                                    }
                                }
                            });

                            $('#chartDiv10').html(''); //remove canvas from container
                            $('#myChart109').remove(); // this is my <canvas> element
                            $('#chartDiv10').html('<canvas id="myChart109" style="z-index:200; position:relative;  width:100%;    "></canvas>'); //add it back to the container
                            var ctx2 = document.getElementById("myChart109").getContext("2d");
                            new Chart(ctx2, {
                                type: "tsgauge",
                                data: {
                                    datasets: [{
                                        label: timeArray4[4],
                                        backgroundColor: ["#0fdc63", "#fd9704", "#ff7143"],
                                        borderWidth: 0,
                                        gaugeData: {
                                            value: memoryArray4[4],
                                            valueColor: "#ff7143"
                                        },
                                        gaugeLimits: [0, 50, 80, 100]
                                    }]
                                },
                                options: {
                                    events: [],
                                    showMarkers: true,
                                    animation: {
                                        duration: 0
                                    },

                                    title: {
                                        text: timeArray4[4],
                                        display: true,
                                        fontColor: "black",
                                        fontSize: 15,
                                        fontFamily: "Arial"

                                    }
                                }
                            });
                        }

                    
                    </script>


                         <div class="col-xl-8 col-lg-12">
                            <div class="card shadow mb-4">
                                <!-- Card Header - Dropdown -->
                                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                                  <h6 class="m-0 font-weight-bold text-primary">트래픽 사용률</h6>
                                </div>
                                <!-- Card Body -->
                                <div class="card-body">
                                  <div class="chart-area">
                                         <div id="div100" style="width:50%; height:200px; float:left; margin-top: 0px;">
                                            <%--<canvas id="myChart" style="z-index:200;   width:100%;  height: 100%;  " ></canvas>--%>
                                        </div>
                                        <div id="div101" style="width:50%; height:200px; float:left;  margin-top: 0px;">
                                            <%--<canvas id="myChart" style="z-index:200;   width:100%;  height: 100%;  " ></canvas>--%>
                                        </div>
                                  </div>
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
                                        //var list3 = document.getElementById("div102");   // Get the <ul> element with id="myList"
                                        //list3.removeChild(list3.childNodes[0]);

                                        //네트워크 보안1
                                        var ctx = "";
                                        var myChart = "";
                                        $('#div100').html(''); //remove canvas from container
                                        $('#myChart').remove(); // this is my <canvas> element
                                        $('#div100').html('<canvas id="myChart" style="z-index:200; position:relative;  width:100%; height:200px;   "></canvas>'); //add it back to the container
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
                                                responsive: true,
                                                maintainAspectRatio: true,
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
                                                    fontColor: "black",
                                                },
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
                                                        fontColor: "black",
                                                        fontSize: 11
                                                    }
                                                },
                                                scales: {
                                                    xAxes: [
                                                        {
                                                            ticks: {
                                                                fontColor: "black",
                                                                autoSkip: true,
                                                                maxTicksLimit: 20,
                                                                display: true
                                                            },
                                                            gridLines: {
                                                                display: false,
                                                                drawOnChartArea: false,
                                                            }
                                                        }
                                                    ],
                                                    yAxes: [
                                                        {
                                                            ticks: {
                                                                fontColor: "black"
                                                            },
                                                            gridLines: {
                                                                display: false,
                                                                drawOnChartArea: false,
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
                                        $('#div101').html('<canvas id="myChart2" style="z-index:200; position:relative;  width:100%;  height:200px  "></canvas>'); //add it back to the container
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
                                                responsive: true,
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
                                                    fontColor: "black"
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
                                                        fontColor: "black",
                                                        fontSize: 11
                                                    }
                                                },
                                                scales: {
                                                    xAxes: [
                                                        {
                                                            ticks: {
                                                                fontColor: "black",
                                                                autoSkip: true,
                                                                maxTicksLimit: 20,
                                                                display: true
                                                            },
                                                            gridLines: {
                                                                display: false,
                                                                drawOnChartArea: false,
                                                            }
                                                        }
                                                    ],
                                                    yAxes: [
                                                        {
                                                            ticks: {
                                                                fontColor: "black"
                                                            },
                                                            gridLines: {
                                                                display: false,
                                                                drawOnChartArea: false,
                                                            }
                                                        }
                                                    ]
                                                }
                                            }
                                        });
                                    };

                                    //이게왜 있어야 하는지 모르겠는데 있어야 작동하네;;;
                                    function init5() {

                                        window.ref = window.setInterval(function () { rewrite5(); }, 10000);
                                    }
                                    function rewrite5() {
                                        clearInterval(window.ref);
                                        init5();
                                    }
                                    init5();


                                </script>
                      
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
    <!-- iCheck -->
<style>
      

.card0 {
    box-shadow: 0px 4px 10px 0px #cacaca;
    border-radius: 0px
}


.border-line {
    border-right: 1px solid #EEEEEE
}

        </style>



</body>
</html>