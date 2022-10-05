<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="System_Log_Main.aspx.cs" Inherits="WebApplication1.System_Log_Main" %>
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
<body class="hold-transition skin-blue sidebar-mini" >
    <form id="form1" runat="server" style="height:100%;" >
   <div class="wrapper"> 
   <uc1:uc_menu ID="uc_menu" runat="server" />
       <div class="content-wrapper">
   
            <section class="content-header">
              <h1>
                장비로그
                <small>장비로그들이 보여집니다</small>
              </h1>
              <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                <li class="active"><a href="#">장비로그</a></li>
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
                        <asp:ListItem Value="1">아이피</asp:ListItem>
                            <asp:ListItem>선택</asp:ListItem>
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
                <div style=" margin-top:-6px; margin-bottom:10px; margin-right:10px; float:left; visibility:hidden;">
                    <asp:RadioButton ID="RadioButton1" GroupName="log"  runat="server" Text=" 30분"  OnCheckedChanged="RadioButton1_CheckedChanged"  />
                    <asp:RadioButton ID="RadioButton2" GroupName="log"  runat="server" Text=" 1시간"  OnCheckedChanged="RadioButton2_CheckedChanged" />
                </div>
                <div style=" margin-top:-10px; margin-bottom:10px; float:left; visibility:hidden;">
                    <asp:Button ID="Button2" runat="server" Text="적용" CssClass="btn btn-primary btn-sm" OnClick="Button2_Click" />
                </div>
                <div style=" text-align: right; margin-right: 10px;">
                     <asp:Label ID="Label1" runat="server" Text="Label" ></asp:Label>
                </div>
                <div style="margin-top:10px;" class="box box-info" >
                <asp:Table ID="TBLLIST" runat="server" CssClass="table custab " Width="100%"></asp:Table>
                         <div class="box-footer clearfix" align="center" style="margin-top:0px; height:50px;" >
                          <div style="margin-top:-40px;">
           <asp:Label ID="Label2" runat="server" Text="Label" CssClass="pagination"></asp:Label>
                              </div>
           </div>
                </div>
       
          </section>
           </div>

       <uc2:uc_bottom ID="uc_bottom" runat="server" />
  <asp:Button ID="Button4" runat="server" Text="Button" Visible="false"  OnClick="Button4_Click"/>
                <asp:Label ID="Label3" runat="server"></asp:Label>
                <asp:HiddenField ID="TextBox5" runat="server" />
                <asp:HiddenField ID="searchHF" runat="server" />
                <asp:HiddenField ID="startdateHF" runat="server" />
                <asp:HiddenField ID="enddateHF" runat="server" />
                <asp:HiddenField ID="serverip" runat="server" />

       </div>


        
        
       </form>
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/adminlte.min.js"></script>
       <script type="text/javascript" src="../Scripts/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap-datepicker.kr.js"></script>
       
       
       
       

        

</body>
</html>
