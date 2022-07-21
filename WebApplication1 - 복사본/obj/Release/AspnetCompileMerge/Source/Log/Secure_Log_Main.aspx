<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="Secure_Log_Main.aspx.cs" Inherits="WebApplication1.Secure_Log_Main" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register src="../Common/TopMenu.ascx" tagname="uc_menu" tagprefix="uc1" %>
<%@ Register src="../Common/Bottom.ascx" tagname="uc_menu1" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="/Scripts/jquery-3.2.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script src="/Content/JavaScript.js"></script>
    <script src="/Content/JavaScript.js"></script>
    <link rel="stylesheet" type="text/css" href="/Scripts/datepicker3.css" />
    <script type="text/javascript" src="/Scripts/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="$/Scripts/bootstrap-datepicker.kr.js"></script>


    <script type="text/javascript">


  

        function go(No) {
            var obj = document.getElementById("<%=serverip.ClientID %>");
                if (obj)
                    obj.value = No;

       <%= Page.GetPostBackEventReference(Button4) %>

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
        <div>
             <uc1:uc_menu ID="uc_menu" runat="server" />
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
               
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <nav class="navbar navbar-default" >
    	  <div class="">
		    <!-- Brand and toggle get grouped for better mobile display -->
		    <div class="navbar-btn " style="margin-left:10px;margin-right: 10px" >
                <ui class="nav navbar-nav navbar-left">
                     <li  style="margin-top:5px; margin-right:10px;margin-left:10px;">
                        <span class="glyphicon glyphicon-signal" aria-hidden="true" style='color:black;'></span>장비 로그
                    </li>
                    <li>
                       <Button ID="Button1" runat="server"   OnserverClick="Button6_Click" Class="btn btn-primary btn-sm">
                              <span class="glyphicon glyphicon-save-file" style='color:white;'></span> 엑셀 저장
                        </button>
                    </li>
                </ui>
                <ui class="nav navbar-nav navbar-right">
                   
                    <li style="margin-top:5px; margin-right:10px">
                        <asp:Label ID="Label4" runat="server" Text="구분 : " CssClass="navbar-left" ></asp:Label>
                    </li>
                    <li style=" margin-right:10px">
                        <asp:DropDownList ID="DropDownList1" runat="server" Height="30"  Class="form-control" Width="100px">
                        <asp:ListItem>선택</asp:ListItem>
                        <asp:ListItem Value="1">아이피</asp:ListItem>
                        </asp:DropDownList>
                    </li>


                   <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label5" runat="server" Text="검색 : " CssClass="navbar-left"></asp:Label>
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


    

                </div>

            <div class="container-fluid">
                <div style="float:left; text-align: right;  margin-right: 10px; margin-top:-10px; margin-bottom:-10px">
                    <asp:DropDownList ID="DropDownList2" runat="server" Height="30"  Class="form-control" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                        </asp:DropDownList>
                    

                </div>
                <div style=" margin-top:-6px; margin-bottom:10px; margin-right:10px; float:left;">
                    <asp:RadioButton ID="RadioButton1" GroupName="log"  runat="server" Text=" 30분"  OnCheckedChanged="RadioButton1_CheckedChanged"  />
                    <asp:RadioButton ID="RadioButton2" GroupName="log"  runat="server" Text=" 1시간"  OnCheckedChanged="RadioButton2_CheckedChanged" />
                </div>
                <div style=" margin-top:-10px; margin-bottom:10px; float:left;">
                    <asp:Button ID="Button2" runat="server" Text="적용" CssClass="btn btn-primary btn-sm" OnClick="Button2_Click" />
                </div>
                <div style=" text-align: right; margin-right: 10px;">
                     <asp:Label ID="Label1" runat="server" Text="Label" ></asp:Label>
                </div>
                <div style="margin-top:-50px;" >
                <asp:Table ID="TBLLIST" runat="server" CssClass="table custab " Width="100%"></asp:Table>
                </div>
                <div>
                </div>
                <div class="col-md-12" align="center" style="margin-top: -80px">
                    <asp:Label ID="Label2" runat="server" Text="Label" CssClass="pagination"></asp:Label>
                </div>
                <asp:Button ID="Button4" runat="server" Text="Button" Visible="false"  OnClick="Button4_Click"/>
                <asp:Label ID="Label3" runat="server"></asp:Label>
                <asp:HiddenField ID="TextBox5" runat="server" />
                <asp:HiddenField ID="searchHF" runat="server" />
                <asp:HiddenField ID="startdateHF" runat="server" />
                <asp:HiddenField ID="enddateHF" runat="server" />
                <asp:HiddenField ID="serverip" runat="server" />

          <uc1:uc_menu1 ID="uc_menu1" runat="server" />

                    </div>


    </form>
</body>
</html>
