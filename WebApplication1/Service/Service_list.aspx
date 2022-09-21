<%@ Page MaintainScrollPositionOnPostback="true" EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="Service_list.aspx.cs" Inherits="WebApplication1.Service_list" %>

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
        <script src="../Scripts/jspdf.min.js"></script>
    <script src="../Scripts/jquery-3.2.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Content/JavaScript.js"></script>
    <script src="../Scripts/AdminLTE.css"></script>
     <script src="../Scripts/Raphael-min.js"></script>
    <link rel="stylesheet"  href="../Scripts/datepicker3.min.css" />
    <script type="text/javascript" src="../Scripts/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap-datepicker.kr.js"></script>
    <script src="../Scripts/morris.min.js"></script>
    <link href="../Scripts/morris.css" rel="stylesheet" />

    
    <title></title>
    <script type="text/javascript">




        $(window).bind('beforeunload', function () {
            $("#Progress_Loading").show();
            $("#form1").css({ opacity: 0.5 });
        });

        $(window).ready(function () {
            $("#Progress_Loading").hide();
        });


        function go(NO) {
            if (confirm("삭제 하시겠습니까?") === true) {
                var obj = document.getElementById("<%=HiddenField1.ClientID %>");
                if (obj)
                    obj.value = NO;


       <%= Page.GetPostBackEventReference(Button2) %>
            }

        }

        function go2(NO, description) {
            if (confirm("삭제 하시겠습니까?") === true) {

                var obj = document.getElementById("<%=HiddenField1.ClientID %>");
                if (obj)
                    obj.value = NO;
                var obj = document.getElementById("<%=HiddenField8.ClientID %>");
                if (obj)
                    obj.value = description;

       <%= Page.GetPostBackEventReference(Button9) %>
            }

        }

<%--    function gochart(NO) {
       
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
            if (obj)
                obj.value = NO;


       <%= Page.GetPostBackEventReference(Button6) %>
      

    }--%>

        function port(portnum) {
            var serverip = document.getElementById("<%=TextBox5.ClientID %>").value;
            var port = portnum;
            // alert(serverip + " " + port);

            //alert("test");

            $.ajax({
                type: "POST",
                url: "/Service/Service_list.aspx/chart1",
                //url: "/Service/Service_list.aspx/chart1",
                data: "{'serverip':'" + serverip + "', 'port':'" + port + "'}",
                //data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    //alert(serverip + " " + port[0]);
                    alert(response.d);
                }
            });
        }

        function ConvertToImage(btnExport) {
            html2canvas($("#pdfview")).then(function (canvas) {
                var imgdata = canvas.toDataURL('image/jpeg', 1.0)
                var imgWidth = 200; // 이미지 가로 길이(mm) A4 기준
                var pageHeight = imgWidth * 1.414;  // 출력 페이지 세로 길이 계산 A4 기준

                var imgHeight = canvas.height * imgWidth / canvas.width;
                var heightLeft = imgHeight;
                var doc = new jspdf('p', 'mm', [297, 210]);
                var position = 10;
                doc.addImage(imgdata, 'jpeg', 5, position, imgWidth, imgHeight);

                // 첫 페이지 출력
                heightLeft -= pageHeight;
                //alert(imgHeight);
                // 한 페이지 이상일 경우 루프 돌면서 출력
                while (heightLeft >= 20) {
                    position = heightLeft - imgHeight;
                    doc.addPage();
                    doc.addImage(imgdata, 'jpeg', 5, position, imgWidth, imgHeight);
                    heightLeft -= pageHeight;
                }




                var today = new Date();
                var dd = today.getDate();
                var mm = today.getMonth() + 1; //January is 0!
                var yyyy = today.getFullYear();
                if (dd < 10) {
                    dd = '0' + dd;
                }
                if (mm < 10) {
                    mm = '0' + mm;
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

        //$(function () {
        //    $('#startdate').datepicker({
        //        calendarWeeks: false,
        //        todayHighlight: true,
        //        autoclose: true,
        //        format: "yyyy-mm-dd",
        //        language: "kr"
        //    });
        //});
        //$(function () {
        //    $('#enddate').datepicker({
        //        calendarWeeks: false,
        //        todayHighlight: true,
        //        autoclose: true,
        //        format: "yyyy-mm-dd",
        //        language: "kr"
        //    });
        //});



    </script>
</head>
<body class="hold-transition skin-blue sidebar-mini">
    <form id="form1" runat="server" >

   <div class="wrapper"> 
   <uc1:uc_menu ID="uc_menu" runat="server" />
       <div class="content-wrapper">  
            <section class="content-header" >
              <h1>
                세부정보
                <small>세부정보들이 보여집니다</small>
              </h1>
              <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                <li class="active"><a href="#">장비리스트</a></li>
              </ol>
            </section>

           
    <section class="content" >
            <nav class="navbar ">
                    
                        <!-- Brand and toggle get grouped for better mobile display -->
                        <div class="navbar-btn" style="margin-right: 10px">
                            <ui class="nav navbar-nav navbar-right">
                    <li style="margin-top:5px; margin-right:10px">
                        <asp:Label ID="Label5" runat="server" Text="구분 : " CssClass="navbar-left" ></asp:Label>
                    </li>
                    <li style=" margin-right:10px">
                         <asp:DropDownList ID="DropDownList1" runat="server" Height="30" Class="form-control" Width="100px">
                        <asp:ListItem >선택</asp:ListItem>
                        <asp:ListItem Value="1">운영체제</asp:ListItem>
                        <asp:ListItem Value="2">상태</asp:ListItem>
                        <asp:ListItem Value="3">서비스 이름</asp:ListItem>
                        <asp:ListItem Value="4">아이피</asp:ListItem>
                        <asp:ListItem Value="5">아이디</asp:ListItem>
                    </asp:DropDownList>
                    </li>
                   <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label6" runat="server" Text="검색 : " CssClass="navbar-left"></asp:Label>
                   </li>
                    <li style="margin-right:10px">
                     <asp:TextBox Height="30" ID="Search" runat="server" Class="form-control" Width="100px"></asp:TextBox>
                   </li>
                    <li style=" margin-right:10px">
                        <asp:Button ID="Button3" runat="server" Text="검색" CssClass="btn btn-primary btn-sm" OnClick="Button3_Click" />
                   </li>
                </ui>
                        </div>
                    
                </nav>



                <nav class="navbar " >
                    
                        <!-- Brand and toggle get grouped for better mobile display -->
                        <div class="navbar-btn navbar-right" style="margin-right: 10px">
                            <ui class="nav navbar-nav navbar-right">

                    <li style="margin-top:5px; margin-right:10px">
                        <asp:Label ID="Label7" runat="server" Text="서비스 이름 :" CssClass="navbar-left" ></asp:Label>
                    </li>
                    <li style=" margin-right:10px">
                        <asp:TextBox ID="TextBox1" Width="180" Height="30" runat="server" Class="form-control"></asp:TextBox>
                    </li>

                   <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label8" runat="server" Text="서버 아이피 : " CssClass="navbar-left"></asp:Label>
                   </li>
                   <li style="margin-right:10px">
                       <asp:TextBox ID="TextBox2" Width="180" Height="30" runat="server" Class="form-control"></asp:TextBox>
                   </li>


                   <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label9" runat="server" Text="서버 아이디 : " CssClass="navbar-left"></asp:Label>
                   </li>
                    <li style="margin-right:10px">
                       <asp:TextBox ID="TextBox3" Width="180" Height="30" runat="server" Class="form-control"></asp:TextBox>
                   </li>

                    <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label10" runat="server" Text="패스워드 : " CssClass="navbar-left"></asp:Label>
                   </li>
                    <li style="margin-right:10px">
                       <asp:TextBox ID="TextBox4" Width="180" Height="30" runat="server" Class="form-control"></asp:TextBox>
                   </li>

                    <li style="margin-right:10px">
                         <asp:Button ID="Button1" runat="server" Text="등록" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" />
                    </li>
                </ui>
                        
                    </div>
                </nav>
                        <%--<div class="pdfview" id="pdfview" runat="server" style=" height: 1700px; width: 100%">--%>
                     <table style="width: 100%;  ">
                        <td>
                            <center><asp:Label ID="Label2" runat="server" Text="Label" Font-Size="Large" Font-Bold="true"></asp:Label><br />
                        <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label>
                        <br />
                        <div id="drop" runat="server" style="visibility:hidden">
                         포트 차트 선택 : 
                           <%-- <asp:ListBox ID="ListBox1" runat="server" Width="100px" Height="300px">
                             
                            </asp:ListBox>--%>
                            <asp:DropDownList ID="DropDownList2"  runat="server"   AutoPostBack="true"  Width="80px">
                             <asp:ListItem>선택</asp:ListItem>
                                    </asp:DropDownList>
                            <script>
                                var options = document.getElementById('DropDownList2').options;

                                for (var i = 0; i < options.length; i++) {
                                    var a = options[i].value.split('[');
                                    if (a[1] === "Down]") {
                                        //options[i].selected = true;

                                        options[i].setAttribute("style", "color:red;");


                                        //dropdownlist1.Items[0].Attributes.CssStyle.Add("color", "Black");
                                        //alert("1");
                                    }
                                }
                            </script>
                           <%--    <asp:DropDownList ID="DropDownList3" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                   <asp:ListItem Value="0">선택</asp:ListItem>
                        <asp:ListItem Value="1">1일</asp:ListItem>
                        <asp:ListItem Value="7">7일</asp:ListItem>
                        
                </asp:DropDownList>--%>
                        </div>
                    </center>
                        </td>
                    </table>
                    <div style="float: left; margin-top: -25px; text-align: center;">
                        <button id="Button4" runat="server" class="btn btn-primary " onserverclick="Button4_Click">
                            <span class="glyphicon glyphicon-chevron-left" style='color: white;'></span>이전    
                        </button>
                      <%--  <asp:Button ID="btnExport" Text="Export to PDF" Class="btn btn-primary " runat="server" UseSubmitBehavior="false"
                            OnClick="ExportToImage" OnClientClick="return ConvertToImage(this)" />--%>
                    </div>

                    <div style="text-align: right; margin-right: 10px; margin-top: -25px; margin-bottom: 10px;">


                        <%--<a href="Service_modi_all.aspx?no=<%=HiddenField1.Value%>" data-toggle="modal" data-target="#myModal2">--%>
                        <a href="Service_modi_all.aspx?no=<%=HiddenField2.Value%>" data-toggle="modal" data-target="#myModal2">
                            <button id="Button5" runat="server" class="btn btn-primary ">
                                <span class="glyphicon glyphicon-edit" style='color: white;'></span>일괄변경    
                            </button>

                        </a>
                    </div>
                         <div id="portdiv" runat="server" class="box box-info" style="width:98%; padding-top:10px; padding-left:30px; padding-bottom:10px;  ">
                        <asp:Table ID="TBLLIST1" runat="server"  Width="100%" ></asp:Table>    
                        </div>
                     <div id="div1" class="div1" style="width:83%; background-color:white; height:250px; margin-top:0px; float:left;  ">
                        <canvas id="myChart" style="z-index:200;  width:83%;  height: 100%;    "></canvas>
                              </div>
     
                    <script src='../Scripts/chart.min.js'></script>
    <script>
        $(function () {
            var serverip = document.getElementById("<%=TextBox5.ClientID %>").value;
            var port = document.getElementById("<%=HiddenField4.ClientID %>").value.split('[');

            //alert(serverip +" " +port[0]);

            $.ajax({
                type: "POST",
                url: "/Service/Service_list.aspx/chart1",
                //url: "/Service/Service_list.aspx/chart1",
                data: "{'serverip':'" + serverip + "', 'port':'" + port[0] + "'}",
                //data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    //alert(serverip + " " + port[0]);
                    alert(response.d);
                }
            });
        });





        setInterval(function () {
            $.ajax({
                type: "POST",
                url: "/Service/Service_list.aspx/chart1",
                data: "{'serverip':'" + serverip + "', 'port':'" + port + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,

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



        };
    </script>



<%--                    <div id="chartpart" runat="server" class="info-box" style="margin-left: 10px; margin-right: 0px; width: 83%; float: left;">
                        <div class="info-box-content" style="margin: 0px 0px 0px 0px;">
                            <center>
                 <font size="4">
            <label id="title" runat="server" class="label label-success">Traffic Line Chart</label>
                 </font>
         <div id="myfirstchart" style="margin-right:0px;  height: 250px;"></div>
        <asp:Label ID="Label11"  runat="server" Text="Label"></asp:Label>
               </center>
                        </div>
                    </div>--%>
                    <div id="chartpart1" runat="server" style="width: 15%; float: left; z-index:100000">
                        <div id="mysecondchart" style="height: 150px;"></div>
                        <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
                        <div id="mythirdchart" style="height: 150px;"></div>
                        <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
                    </div>

          
           <div id="maclist" runat="server" style="margin-left: 10px; margin-bottom: 0px; width: 98%">
                        <font size="5">
                맥 정보
          </font>
               <Button ID="Button6" text="엑셀 저장" runat="server"  onserverclick="Button6_ServerClick" class="btn btn-primary btn-sm" style="margin-bottom:5px;">
                              <span class="glyphicon glyphicon-save-file" style='color:white;'></span> 엑셀 저장
                        </button>
               <Button ID="Button10" text="엑셀 저장" runat="server"  onserverclick="Button10_ServerClick" class="btn btn-primary btn-sm" style="margin-bottom:5px;">
                              <span class="glyphicon glyphicon-refresh" style='color:white;'></span> 맥주소 초기화
                        </button>
                    </div >
        <div  style="width:98%">
            <asp:Table ID="Table1" runat="server" CssClass="table table-striped custab " Width="100%"></asp:Table>
        </div>
         <%--<div id="maclistpage" runat="server" class="col-md-12" align="center" style="margin-top: -50px">
                <asp:Label ID="Label11" runat="server" Text="Label" CssClass="pagination"></asp:Label>
            </div>--%>
                    
                    <br />
                    
                    <div id="snmpadd" runat="server" style="width:100%; position:relative; margin-left: 10px;">
                        <font size="5">
                Snmp 추가 
                 </font>
                                              
                        <br />
              
                                
                                    <asp:Label ID="Label14" runat="server" Text="장비 : "></asp:Label>
                                    <asp:DropDownList ID="MYDDL" runat="server" AutoPostBack="true" ></asp:DropDownList>
                                            &nbsp;&nbsp;
                                    <asp:Label ID="Label15" runat="server" Text="모델 : "></asp:Label>
                                    <asp:DropDownList ID="MYDDL2" AutoPostBack="true"  runat="server"></asp:DropDownList>
                                            &nbsp;&nbsp;
                                    <asp:Label ID="Label16" runat="server" Text="기능 : "></asp:Label>
                                    <asp:DropDownList ID="MYDDL3" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            &nbsp;&nbsp;
                                <asp:Button ID="Button11" runat="server" Text="Add" Class="btn btn-primary btn-xs" OnClick="Button11_Click" />
                        <style>
                            .spinner {
                                content: '...loading';
                                display: block;
                                position: fixed;
                                z-index: 1031;
                                top: 50%;
                                right: 50%; /* or: left: 50%; */
                                margin-top: -..px; /* have of the elements height */
                                margin-right: -..px; /* have of the elements widht */
                                background: rgba(0,0,0,.5);
                            }
                        </style>
                           <div id = "Progress_Loading" class="spinner" runat="server" style="background-color:transparent" ><!-- 로딩바 -->
                        <img src="../Img/Progress_Loading.gif"/ >
                        </div>
                        
                    </div>



                    <br />
          
                    <asp:HiddenField ID="HiddenField5" runat="server" />
                    <asp:HiddenField ID="HiddenField6" runat="server" />
                    
                    <asp:HiddenField ID="HiddenField7" runat="server" />
                    <div id="snmplist" runat="server" style="margin-left: 10px; margin-bottom: 0px; width: 98%">
                        <font size="5">
                Snmp 리스트
               </font>
                    </div >
        <div id="snmplist2" runat="server" class="box box-info" style="width:98%">
            <asp:Table ID="TBLLIST3" runat="server" CssClass="table table-striped custab " Width="100%"></asp:Table>
        </div>
                    
                    <br />


                    <div style="float: left; width: 48%; margin-right: 20px;">
                        <div id="history" runat="server" style="text-align: right; margin-right: 37px; margin-top: -25px; margin-bottom: -30px;">
                            <a href="Service_History.aspx?no=<%=HiddenField2.Value%>&serverip=<%=TextBox5.Value%>&flag=0" data-toggle="modal" data-target="#myModal2">
                                <button id="Button8" runat="server" class="btn btn-primary ">
                                    <span class="glyphicon glyphicon-edit" style='color: white;'></span>History    
                                </button>
                            </a>
                        </div>
                        <div id="history2" runat="server" style="margin-left: 10px; margin-bottom: 0px; width: 98%">
                            <font size="5">
                History
               </font>
                        </div>
                          <div class="box box-info" style="width:98%" >
                        <asp:Table ID="TBLLIST2" runat="server" CssClass="table table-striped custab" Width="100%"></asp:Table>
                              </div>
                        
                        <div id="history_pageing" runat="server" class="col-md-12" align="center" style="margin-top: -50px">
                            <asp:Label ID="Label1" runat="server" Text="Label" CssClass="pagination"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 50%; margin-top: -22px;">

                        <div id="Div2" runat="server" style="margin-left: 10px; margin-bottom: 0px; margin-top: 0px; width: 98%">
                            <font size="5">
               다운 로그
               </font>
                        </div>
                        <div class="box box-info" style="width:98%">
                        <asp:Table ID="TBLLIST4" runat="server" CssClass="table table-striped custab" Width="100%"></asp:Table>    
                        </div>
                        
                        <div id="Div3" runat="server" class="col-md-12" align="center" style="margin-top: -50px">
                            <asp:Label ID="Label18" runat="server" Text="Label" CssClass="pagination"></asp:Label>
                        </div>
                    </div>

        <div style="width: 100%; ">


               <div id="Div4" runat="server" style="margin-left: 10px; margin-bottom: 0px; width:50%">
                        <font size="5">
              정보
               </font>
                    </div >
            <div class="box box-info" style="width:98%">
                        <asp:Table ID="TBLLIST" runat="server" CssClass="table table-striped custab " Width="100%"></asp:Table>
            </div>
        
                          <div class="" align="center" style="margin-top:-50px; margin-bottom:-40px;">
           <asp:Label ID="Label4" runat="server" Text="Label" CssClass="pagination"></asp:Label>
           </div>
        </div>
       
                            <%--</div>--%>
          </section>
           </div>
       

       <uc2:uc_bottom ID="uc_bottom" runat="server" />
            
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="HiddenField8" runat="server" />
                    <asp:HiddenField ID="HiddenField4" runat="server" />
                    <asp:Button ID="Button2" runat="server" Text="Button" Visible="false" OnClick="Button2_Click" />
                    <asp:Button ID="Button7" runat="server" Text="Button" Visible="false" OnClick="Button7_Click" />
                    <%--<asp:Button ID="Button6" runat="server" Text="Button" Visible="false"  OnClick="Button6_Click1"/>--%>
                    <asp:Button ID="Button9" runat="server" Text="Button" Visible="false" OnClick="Button9_Click" />
                </div>


                <asp:Label ID="Label3" runat="server"></asp:Label>
                <asp:HiddenField ID="TextBox5" runat="server" />
                <asp:HiddenField ID="HiddenField3" runat="server" />



                <!-- Modal HTML -->
                <div id="myModal" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <!-- Content will be loaded here from "service_modi1.aspx" file -->
                        </div>
                    </div>
                </div>


                <div id="myModal2" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <!-- Content will be loaded here from "service_modi1.aspx" file -->
                        </div>
                    </div>
                </div>

                <div id="myModal3" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <!-- Content will be loaded here from "service_modi1.aspx" file -->
                        </div>
                    </div>
                </div>
                
                <asp:HiddenField ID="hfImageData" runat="server" />
        
     


        
        
       </form>
    
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/adminlte.min.js"></script>
       
       
       
       
       

        

</body>
</html>