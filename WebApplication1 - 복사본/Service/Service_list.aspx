<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="Service_list.aspx.cs" Inherits="WebApplication1.Service_list" %>

<%@ Register Src="../Common/Bottom.ascx" TagName="uc_menu1" TagPrefix="uc1" %>
<%@ Register Src="../Common/TopMenu.ascx" TagName="uc_menu" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />



    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="/Scripts/jquery-3.2.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script src="/Content/JavaScript.js"></script>
    <script src="/Content/JavaScript.js"></script>
    <script src="/Scripts/morris.min.js"></script>
    <link href="/Scripts/morris.css" rel="stylesheet" />
    <script src="/Scripts/Raphael-min.js"></script>
    <script src="/Scripts/jspdf.min.js"></script>
    <script src="/Scripts/html2canvas.min.js"></script>
    <script src="/Scripts/bluebird.min.js"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/datepicker3.css" />
    <script type="text/javascript" src="/Scripts/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="/Scripts/bootstrap-datepicker.kr.js"></script>


    <title></title>
    <script type="text/javascript">


        function go(NO) {
            if (confirm("삭제 하시겠습니까?") == true) {
                var obj = document.getElementById("<%=HiddenField1.ClientID %>");
                if (obj)
                    obj.value = NO;


       <%= Page.GetPostBackEventReference(Button2) %>
            }

        }

        function go2(NO, description) {
            if (confirm("삭제 하시겠습니까?") == true) {

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
<body>
    <form id="form1" runat="server">
        <uc1:uc_menu ID="uc_menu" runat="server" />

        <asp:HiddenField ID="HiddenField9" runat="server" />
        <div class="right" id="right" runat="server" style="background: white; ">
            <div class="container-fluid">
                <nav class="navbar navbar-default">
                    <div class="">
                        <!-- Brand and toggle get grouped for better mobile display -->
                        <div class="navbar-btn" style="margin-right: 10px">
                            <ui class="nav navbar-nav navbar-left">
                   <li style="margin-top:5px; margin-right:10px;margin-left:10px;">
                        <span class="glyphicon glyphicon-signal" aria-hidden="true" style='color:black;'></span> 세부정보
                    </li>
                </ui>


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
                    </div>
                </nav>



                <nav class="navbar navbar-default">
                    <div class="">
                        <!-- Brand and toggle get grouped for better mobile display -->
                        <div class="navbar-btn navbar-right" style="margin-right: 10px">
                            <ui class="nav navbar-nav navbar-right">

                    <li style="margin-top:5px; margin-right:10px">
                        <asp:Label ID="Label7" runat="server" Text="서비스 이름 :" CssClass="navbar-left" ></asp:Label>
                    </li>
                    <li style=" margin-right:10px">
                        <asp:TextBox ID="TextBox1" Height="30" runat="server" Class="form-control"></asp:TextBox>
                    </li>

                   <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label8" runat="server" Text="서버 아이피 : " CssClass="navbar-left"></asp:Label>
                   </li>
                   <li style="margin-right:10px">
                       <asp:TextBox ID="TextBox2" Height="30" runat="server" Class="form-control"></asp:TextBox>
                   </li>


                   <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label9" runat="server" Text="서버 아이디 : " CssClass="navbar-left"></asp:Label>
                   </li>
                    <li style="margin-right:10px">
                       <asp:TextBox ID="TextBox3" Height="30" runat="server" Class="form-control"></asp:TextBox>
                   </li>

                    <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label10" runat="server" Text="패스워드 : " CssClass="navbar-left"></asp:Label>
                   </li>
                    <li style="margin-right:10px">
                       <asp:TextBox ID="TextBox4" Height="30" runat="server" Class="form-control"></asp:TextBox>
                   </li>

                    <li style="margin-right:10px">
                         <asp:Button ID="Button1" runat="server" Text="등록" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" />
                    </li>
                </ui>
                        </div>
                    </div>
                </nav>

                <div class="pdfview" id="pdfview" runat="server" style="background: white; height: 1700px; width: 100%">

                    <table style="width: 100%;">
                        <td>
                            <center><asp:Label ID="Label2" runat="server" Text="Label" Font-Size="Large" Font-Bold="true"></asp:Label><br />
                        <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label>
                        <br />
                        <div id="drop" runat="server">
                         포트 차트 선택 : <asp:DropDownList ID="DropDownList2"  runat="server"   AutoPostBack="true"  Width="80px">
                             <asp:ListItem>선택</asp:ListItem>
                                    </asp:DropDownList>
                               <asp:DropDownList ID="DropDownList3" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                                   <asp:ListItem Value="0">선택</asp:ListItem>
                        <asp:ListItem Value="1">1일</asp:ListItem>
                        <asp:ListItem Value="7">7일</asp:ListItem>
                        <asp:ListItem Value="30">30일</asp:ListItem>
                        <asp:ListItem Value="365">1년</asp:ListItem>
                </asp:DropDownList>
                        </div>
                    </center>
                        </td>
                    </table>
                    <div style="float: left; margin-top: -25px; text-align: center;">
                        <button id="Button4" runat="server" class="btn btn-primary " onserverclick="Button4_Click">
                            <span class="glyphicon glyphicon-chevron-left" style='color: white;'></span>이전    
                        </button>
                        <asp:Button ID="btnExport" Text="Export to PDF" Class="btn btn-primary " runat="server" UseSubmitBehavior="false"
                            OnClick="ExportToImage" OnClientClick="return ConvertToImage(this)" />
                    </div>

                    <div style="text-align: right; margin-right: 10px; margin-top: -25px; margin-bottom: 10px;">


                        <%--<a href="Service_modi_all.aspx?no=<%=HiddenField1.Value%>" data-toggle="modal" data-target="#myModal2">--%>
                        <a href="Service_modi_all.aspx?no=<%=HiddenField2.Value%>" data-toggle="modal" data-target="#myModal2">
                            <button id="Button5" runat="server" class="btn btn-primary ">
                                <span class="glyphicon glyphicon-edit" style='color: white;'></span>일괄변경    
                            </button>

                        </a>
                    </div>


                    <div id="chartpart" runat="server" class="info-box" style="margin-left: 10px; margin-right: 0px; width: 83%; float: left;">
                        <div class="info-box-content" style="margin: 0px 0px 0px 0px;">
                            <center>
                 <font size="4">
            <label id="title" runat="server" class="label label-success">Traffic Line Chart</label>
                 </font>
         <div id="myfirstchart" style="margin-right:0px;  height: 250px;"></div>
        <asp:Label ID="Label11"  runat="server" Text="Label"></asp:Label>
               </center>
                        </div>
                    </div>
                    <div id="chartpart1" runat="server" style="width: 15%; float: left;">
                        <div id="mysecondchart" style="height: 150px;"></div>
                        <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
                        <div id="mythirdchart" style="height: 150px;"></div>
                        <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
                    </div>

                    <div id="snmpadd" runat="server" style="margin-left: 10px;">
                        <font size="5">
                Snmp Add 
                 </font>
                        <br />
                        <asp:Label ID="Label14" runat="server" Text="장비 : "></asp:Label><asp:DropDownList ID="MYDDL" DataTextField="PRJ_TITLE" AutoPostBack="true" DataValueField="PRJ_ID" runat="server">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label15" runat="server" Text="모델 : "></asp:Label><asp:DropDownList ID="MYDDL2" DataTextField="PRJ_TITLE" AutoPostBack="true" DataValueField="PRJ_ID" runat="server">
                </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label16" runat="server" Text="기능 : "></asp:Label><asp:DropDownList ID="MYDDL3" DataTextField="PRJ_TITLE" AutoPostBack="true" DataValueField="PRJ_ID" runat="server">
                </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button11" runat="server" Text="Add" Class="btn btn-primary btn-xs" OnClick="Button11_Click" />

                    </div>
                    <br />
                    <asp:HiddenField ID="HiddenField5" runat="server" />
                    <asp:HiddenField ID="HiddenField6" runat="server" />
                    <asp:HiddenField ID="HiddenField7" runat="server" />
                    <div id="snmplist" runat="server" style="margin-left: 10px; margin-bottom: -90px; width: 98%">
                        <font size="5">
                Snmp List
               </font>
                    </div>
                    <asp:Table ID="TBLLIST3" runat="server" CssClass="table table-striped custab " Width="98%"></asp:Table>
                    <br />


                    <div style="float: left; width: 48%; margin-right: 20px;">
                        <div id="history" runat="server" style="text-align: right; margin-right: 37px; margin-top: -25px; margin-bottom: -30px;">
                            <a href="Service_History.aspx?no=<%=HiddenField2.Value%>&serverip=<%=TextBox5.Value%>&flag=0" data-toggle="modal" data-target="#myModal2">
                                <button id="Button8" runat="server" class="btn btn-primary ">
                                    <span class="glyphicon glyphicon-edit" style='color: white;'></span>History    
                                </button>
                            </a>
                        </div>
                        <div id="history2" runat="server" style="margin-left: 10px; margin-bottom: -40px; width: 98%">
                            <font size="5">
                History
               </font>
                        </div>
                        <asp:Table ID="TBLLIST2" runat="server" CssClass="table table-striped custab" Width="98%"></asp:Table>
                        <div id="history_pageing" runat="server" class="col-md-12" align="center" style="margin-top: -60px">
                            <asp:Label ID="Label1" runat="server" Text="Label" CssClass="pagination"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 50%; margin-top: -22px;">

                        <div id="Div2" runat="server" style="margin-left: 10px; margin-bottom: -40px; margin-top: -2px; width: 98%">
                            <font size="5">
               Down Log List
               </font>
                        </div>
                        <asp:Table ID="TBLLIST4" runat="server" CssClass="table table-striped custab" Width="98%"></asp:Table>
                        <div id="Div3" runat="server" class="col-md-12" align="center" style="margin-top: -60px">
                            <asp:Label ID="Label18" runat="server" Text="Label" CssClass="pagination"></asp:Label>
                        </div>
                    </div>


                    <div style="width: 100%; float: left">
                        <div style="margin-left: 10px; margin-bottom: -90px; width: 98%">
                            <font size="5">
                Info
               </font>
                        </div>
                        <asp:Table ID="TBLLIST" runat="server" CssClass="table table-striped custab " Width="98%"></asp:Table>
                        <div class="col-md-12" align="center" style="margin-top: -110px">
                            <asp:Label ID="Label4" runat="server" Text="Label" CssClass="pagination"></asp:Label>
                        </div>
                    </div>

                  <%--  <ui class="nav navbar-nav navbar-left">
                          </li>
                    <li style=" margin-right:10px">
                          <asp:TextBox ID="startdate" runat="server" Class="form-control " Width="100px"></asp:TextBox>
                       

                    </li>
                      <li style="margin-top:5px; margin-right:10px">
                          ~
                          </li>
                    <li style=" margin-right:10px">
                          <asp:TextBox ID="enddate" runat="server" Class="form-control " Width="100px"></asp:TextBox>
                        
                    </li>
                          <li style=" margin-right:10px">
                       <asp:Button ID="Button6" runat="server" Text="검색" CssClass="btn btn-primary btn-sm" OnClick="Button6_Click1" />
                   </li>
                          <li>
                               <asp:DropDownList ID="DropDownList4" runat="server" Height="30"  Class="form-control" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                        </asp:DropDownList>
                          </li>
                          </ui>
                    <div style="width: 100%; float: left">
                        <div style="margin-left: 10px; margin-bottom: -90px; width: 98%">
                            <font size="5">
                System Log
               </font>
                        </div>
                        <asp:Table ID="TBLLIST5" runat="server" CssClass="table table-striped custab " Width="98%"></asp:Table>
                        <div class="col-md-12" align="center" style="margin-top: -110px">
                        </div>
                    </div>--%>



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
                <uc1:uc_menu1 ID="uc_menu1" runat="server" />
                <asp:HiddenField ID="hfImageData" runat="server" />
            </div>
            <%--<div style="height:435px;">

            </div>--%>
        </div>


    </form>

</body>
</html>
