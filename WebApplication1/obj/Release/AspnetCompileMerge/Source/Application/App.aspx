<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="App.aspx.cs" Inherits="WebApplication1.App" %>
<%@ Register src="../Common/Bottom.ascx" tagname="uc_menu1" tagprefix="uc1" %>
<%@ Register src="../Common/TopMenu.ascx" tagname="uc_menu" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="/Scripts/jquery-3.2.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script src="/Content/JavaScript.js"></script>
    <script src="/Content/JavaScript.js"></script>

    <title></title>
<script type = text/javascript>



    function go(No)
    {
        var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No; 

       <%= Page.GetPostBackEventReference(Button2) %>

    }
    

</script>
</head>
<body>
    <form id="form1" runat="server" >

        
        <div id="navbar">    
  <uc1:uc_menu ID="uc_menu" runat="server" />
</div>

        <div class="container-fluid" >
            <div class="alert alert-warning" role="alert"><span class="glyphicon glyphicon-signal" aria-hidden="true"></span> 어플리케이션 관리</div>
         
            <nav class="navbar navbar-default" >
    	  <div class="">
		    <!-- Brand and toggle get grouped for better mobile display -->
		    <div class="navbar-btn navbar-right" style="margin-right: 10px" >
                <ui class="nav navbar-nav navbar-right">
                    <li style="margin-top:5px; margin-right:10px">
                        <asp:Label ID="Label4" runat="server" Text="구분 : " CssClass="navbar-left" ></asp:Label>
                    </li>
                    <li style=" margin-right:10px">
                        <asp:DropDownList ID="DropDownList1" runat="server" Height="30"  Class="form-control navbar-left" Width="130px">
                        <asp:ListItem>선택</asp:ListItem>
                        <asp:ListItem Value="4">아이피</asp:ListItem>
                        <asp:ListItem Value="5">어플리케이션</asp:ListItem>
                        </asp:DropDownList>
                    </li>
                   <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label5" runat="server" Text="검색 : " CssClass="navbar-left"></asp:Label>
                   </li>
                    <li style="margin-right:10px">
                       <asp:TextBox Height="30" ID="Search" runat="server" Class="form-control navbar-left" Width="100px"  AutoComplete="off" ></asp:TextBox>
                   </li>
                    <li style=" margin-right:10px">
                       <asp:Button  ID="Button3" runat="server" Text="검색" CssClass="btn btn-primary btn-sm navbar-left" OnClick="Button3_Click" />
                   </li>
                </ui>
            </div>
              </div>
                </nav>

               

            <br />
            <div style="text-align:right; margin-right:10px; margin-top:-40px">
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
           </div>
             <asp:Table ID="TBLLIST"  runat="server" CssClass="table table-striped table-bordered table-hover"  Width="100%" ></asp:Table>

           
          
            <asp:HiddenField ID="HiddenField1" runat="server" />
           
          
            <asp:Button ID="Button2" runat="server" Text="Button" Visible="false" OnClick="Button2_Click" />
   
            </div>
         <div class="col-md-12" align="center" style="margin-top:-40px">
           <asp:Label ID="Label2" runat="server" Text="Label" CssClass="pagination"></asp:Label>
       </div>
    
        <asp:Label ID="Label3" runat="server" ></asp:Label>
        <asp:HiddenField ID="TextBox5" runat="server" />
        





        <uc1:uc_menu1 ID="uc_menu1" runat="server" />








    </form>
    
</body>
</html>
