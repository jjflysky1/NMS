<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Service.aspx.cs" Inherits="WebApplication1.Service" %>

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
    <%--<script src="../Scripts/AdminLTE.css"></script>--%>

    <title></title>
    <script type="text/javascript">



        function mail_modi(No) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;

       <%= Page.GetPostBackEventReference(Button10) %>

        }

        function go(No, category) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;

        var obj = document.getElementById("<%=HiddenField2.ClientID %>");
        if (obj)
            obj.value = category;

       <%= Page.GetPostBackEventReference(Button2) %>

        }


        function category(No, category) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
       if (obj)
           obj.value = No;

       var obj = document.getElementById("<%=HiddenField2.ClientID %>");
       if (obj)
           obj.value = category;

       <%= Page.GetPostBackEventReference(Button8) %>

        }

        function active(No) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;

       <%= Page.GetPostBackEventReference(Button4) %>

        }
        function inactive(No) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;

       <%= Page.GetPostBackEventReference(Button5) %>

        }

        function active_dash(No) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;

       <%= Page.GetPostBackEventReference(Button1) %>

        }
        function inactive_dash(No) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;

       <%= Page.GetPostBackEventReference(Button11) %>

        }


        function active_mail(No) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;

       <%= Page.GetPostBackEventReference(Button6) %>

        }

        function inactive_mail(No) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;

       <%= Page.GetPostBackEventReference(Button7) %>

        }


        function go2(NO) {
            if (confirm("삭제 하시겠습니까?") == true) {
                var obj = document.getElementById("<%=HiddenField3.ClientID %>");
            if (obj)
                obj.value = NO;


       <%= Page.GetPostBackEventReference(Button9) %>
        }

        function sshgo(No, category, os) {

            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
          if (obj)
              obj.value = No;
          var obj = document.getElementById("<%=HiddenField2.ClientID %>");
          if (obj)
              obj.value = category;
          var obj = document.getElementById("<%=HiddenField4.ClientID %>");
                if (obj)
                    obj.value = os;

                alert(os);




            }






        }

    </script>
</head>
<body class="hold-transition skin-blue sidebar-mini">
    <form id="form1" runat="server" style="height: 100%;">
        <div class="wrapper">
            <uc1:uc_menu ID="uc_menu" runat="server" />
            <div class="content-wrapper">

                <section class="content-header">
                    <h1>장비리스트
                <small>장비리스트들이 보여집니다</small>
                    </h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                        <li class="active"><a href="#">장비리스트</a></li>
                    </ol>
                </section>


                <section class="content">
                    <nav class="navbar ">

                        <!-- Brand and toggle get grouped for better mobile display -->
                        <div class="navbar" style="margin-right: 10px">
                            <ui class="nav navbar-nav navbar-right">
                                <li style="margin-top: 5px; margin-right: 10px">
                                    <asp:Label ID="Label4" runat="server" Text="구분 : " CssClass="navbar-left"></asp:Label>
                                </li>
                                <li style="margin-right: 10px;">
                                    <asp:DropDownList ID="DropDownList1" runat="server" Height="35" Class="form-control navbar-left" Width="100px">
                                    
                                        <asp:ListItem Value="4">아이피</asp:ListItem>
                                        <asp:ListItem Value="5">구분</asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li style="margin-top: 5px; margin-right: 10px">
                                    <asp:Label ID="Label5" runat="server" Text="검색 : " CssClass="navbar-left"></asp:Label>
                                </li>
                                <li style="margin-right: 10px">
                                    <asp:TextBox Height="30" ID="Search" runat="server" Class="form-control navbar-left" Width="100px" AutoComplete="off"></asp:TextBox>
                                </li>
                                <li style="margin-right: 10px">
                                    <asp:Button ID="Button3" runat="server" Text="검색" CssClass="btn btn-primary btn-sm navbar-left" OnClick="Button3_Click" />
                                </li>
                            </ui>
                        </div>

                    </nav>

                    <nav class="navbar " style="margin-top: -50px;">

                        <!-- Brand and toggle get grouped for better mobile display -->
                        <div class="navbar-btn navbar-right" style="margin-right: 10px">
                            <ui class="nav navbar-nav navbar-right">

                                <%--   <li style="margin-top:5px; margin-right:10px">
                        <asp:Label ID="Label6" runat="server" Text="서비스 이름 :" CssClass="navbar-left" ></asp:Label>
                    </li>
                    <li style=" margin-right:10px">
                     <asp:TextBox ID="TextBox1" Height="30" runat="server" Class="form-control "></asp:TextBox>
                    </li>--%>
                                <%--  <li style="text-align:left;">
                    <asp:Button  ID="Button12" runat="server" Text="Putty 접속" CssClass="btn btn-link btn-sm " OnClick="Button12_Click"  />
                        </li>--%>
                                <li style="margin-top: 5px; margin-right: 10px">
                                    <asp:Label ID="Label7" runat="server" Text="장비 아이피 : " CssClass="navbar-left"></asp:Label>
                                </li>
                                <li style="margin-right: 10px">
                                    <asp:TextBox ID="TextBox2" Height="30" runat="server" Class="form-control"></asp:TextBox>
                                </li>


                                <%--   <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label8" runat="server" Text="서버 아이디 : " CssClass="navbar-left"></asp:Label>
                   </li>
                    <li style="margin-right:10px">
                       <asp:TextBox ID="TextBox3" Height="30" runat="server" Class="form-control"></asp:TextBox>
                   </li>

                    <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label9" runat="server" Text="패스워드 : " CssClass="navbar-left"></asp:Label>
                   </li>
                    <li style="margin-right:10px">
                       <asp:TextBox ID="TextBox4" Height="30" runat="server" Class="form-control"></asp:TextBox>
                   </li>--%>

                                <li style="margin-right: 10px">
                                    <asp:Button ID="Button100" runat="server" Text="등록" CssClass="btn btn-primary btn-sm" OnClick="Button100_Click" />
                                </li>
                            </ui>


                        </div>

                    </nav>



                    <br />

                    <div style="text-align: right; margin-right: 10px; margin-top: -60px; margin-bottom: -10px;">

                        <asp:DropDownList ID="DropDownList2" runat="server" Height="30" Class="form-control" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                            <asp:ListItem Value="100">100</asp:ListItem>
                        </asp:DropDownList>


                    </div>


                    <div style="text-align: right; margin-right: 10px;">
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </div>
                    <style>
                        .dropdown-menu1 {
                            top: 0;
                            left: 100%;
                        }
                    </style>
                    <div style="margin-top: 0px;" class="box box-info">
                        <asp:Table ID="TBLLIST" runat="server" CssClass="table  custab table-hover " Width="100%"></asp:Table>
                        <div class="box-footer clearfix" align="center" style="margin-top: 0px; height: 50px;">
                            <div style="margin-top: -40px;">
                                <asp:Label ID="Label2" runat="server" Text="Label" CssClass="pagination"></asp:Label>
                            </div>
                        </div>
                    </div>

                </section>
            </div>

            <uc2:uc_bottom ID="uc_bottom" runat="server" />

            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />
            <asp:HiddenField ID="HiddenField3" runat="server" />
            <asp:HiddenField ID="HiddenField4" runat="server" />
            <asp:Button ID="Button2" runat="server" Text="Button" Visible="false" OnClick="Button2_Click" />
            <asp:Button ID="Button9" runat="server" Text="Button" Visible="false" OnClick="Button9_Click" />
            <asp:Button ID="Button4" runat="server" Text="Button" Visible="false" OnClick="Button4_Click" />
            <asp:Button ID="Button5" runat="server" Text="Button" Visible="false" OnClick="Button5_Click" />
            <asp:Button ID="Button6" runat="server" Text="Button" Visible="false" OnClick="Button6_Click" />
            <asp:Button ID="Button7" runat="server" Text="Button" Visible="false" OnClick="Button7_Click" />
            <asp:Button ID="Button8" runat="server" Text="Button" Visible="false" OnClick="Button8_Click" />
            <asp:Button ID="Button10" runat="server" Text="Button" Visible="false" OnClick="Button10_Click" />
            <asp:Button ID="Button1" runat="server" Text="Button" Visible="false" OnClick="Button1_Click1" />
            <asp:Button ID="Button11" runat="server" Text="Button" Visible="false" OnClick="Button11_Click" />



            <asp:Label ID="Label3" runat="server"></asp:Label>
            <asp:HiddenField ID="TextBox5" runat="server" />
            <div id="myModal" class="modal fade">
                <div class="modal-dialog" style="width: 800px; margin-left: auto; margin-right: auto">
                    <div class="modal-content">
                        <!-- Content will be loaded here from "service_modi1.aspx" file -->
                    </div>
                </div>
            </div>

        </div>




    </form>
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/adminlte.min.js"></script>








</body>
</html>
