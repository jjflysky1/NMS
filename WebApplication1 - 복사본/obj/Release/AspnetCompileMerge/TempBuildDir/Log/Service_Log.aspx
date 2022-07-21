<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Service_Log.aspx.cs" Inherits="WebApplication1.Service_Log" %>

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
        function go(NO) {
            if (confirm("삭제 하시겠습니까?") == true) {
                var obj = document.getElementById("<%#searchHF.ClientID %>");
            if (obj)
                obj.value = NO;

       <%# Page.GetPostBackEventReference(Button3) %>
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
<body>
    <form id="form1" runat="server">

       
          <uc1:uc_menu ID="uc_menu" runat="server" />
            <div class="right" >
          <div class="container-fluid">
            
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 



               <nav class="navbar navbar-default" >
    	  <div class="">
		    <!-- Brand and toggle get grouped for better mobile display -->
		    <div class="navbar-btn " style="margin-left:10px;margin-right: 10px" >
                <ui class="nav navbar-nav navbar-left" >
                    <li  style="margin-top:5px; margin-right:10px;margin-left:10px;">
                        <span class="glyphicon glyphicon-signal" aria-hidden="true" style='color:black;'></span>서비스 로그
                    </li>
                    <li>
                        <Button ID="Button6" text="엑셀 저장" runat="server"  onserverclick="Button6_Click" class="btn btn-primary btn-sm">
                              <span class="glyphicon glyphicon-save-file" style='color:white;'></span> 엑셀 저장
                        </button>
                    </li>
                </ui>
                <ui class="nav navbar-nav navbar-right">
                    <li style="margin-top:5px; margin-right:10px">
                        <asp:Label ID="Label6" runat="server" Text="기간 : " CssClass="navbar-left" ></asp:Label>
                    </li>
                    <li style=" margin-right:10px">
                          <asp:TextBox ID="startdate" runat="server" Class="form-control " Width="100px" ></asp:TextBox>

                    </li>
                      <li style="margin-top:5px; margin-right:10px">
                          ~
                          </li>
                    <li style=" margin-right:10px">
                          <asp:TextBox ID="enddate" runat="server" Class="form-control " Width="100px"></asp:TextBox>
                    

                    </li>
                    <li style="margin-top:5px; margin-right:10px">
                        <asp:Label ID="Label4" runat="server" Text="구분 : " CssClass="navbar-left" ></asp:Label>
                    </li>
                    <li style=" margin-right:10px">
                        <asp:DropDownList ID="DropDownList1" runat="server" Height="30"  Class="form-control" Width="100px">
                        <asp:ListItem>선택</asp:ListItem>
                        <asp:ListItem Value="1">이름</asp:ListItem>
                        <asp:ListItem Value="2">아이피</asp:ListItem>
                        </asp:DropDownList>
                    </li>


                   <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label5" runat="server" Text="검색 : " CssClass="navbar-left"></asp:Label>
                   </li>
                    <li style="margin-right:10px">
                       <asp:TextBox Height="30" ID="Search" runat="server" Class="form-control" Width="100px" AutoComplete="off"></asp:TextBox>
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
               
                <div style="text-align: right;  margin-right: 10px; margin-top:-10px; margin-bottom:-10px">
                    <asp:DropDownList ID="DropDownList2" runat="server" Height="30"  Class="form-control" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                        </asp:DropDownList>
                </div>
                <div style=" text-align: right; margin-right: 10px;">
                     <asp:Label ID="Label1" runat="server" Text="Label" ></asp:Label>
                </div>
                <div style="margin-top:-50px;">
                <asp:Table ID="TBLLIST" runat="server" CssClass="table  custab " Width="100%"></asp:Table>
                    </div>
                <div>
                </div>
                <div class="col-md-12" align="center" style="margin-top: -80px">
                    <asp:Label ID="Label2" runat="server" Text="Label" CssClass="pagination"></asp:Label>
                </div>

                <asp:Label ID="Label3" runat="server"></asp:Label>
                <asp:HiddenField ID="TextBox5" runat="server" />
                <asp:HiddenField ID="searchHF" runat="server" />
    <uc1:uc_menu1 ID="uc_menu1" runat="server" />
                </div>
                </form>
</body>
</html>
