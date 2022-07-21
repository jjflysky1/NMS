<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="App_list.aspx.cs" Inherits="WebApplication1.App_list" %>
<%@ Register src="../Common/Bottom.ascx" tagname="uc_menu1" tagprefix="uc1" %>
<%@ Register src="../Common/TopMenu.ascx" tagname="uc_menu" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
 <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="/Scripts/jquery-3.2.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script src="/Content/JavaScript.js"></script>
    <script src="/Content/JavaScript.js"></script>
    
    <script src="/Content/JavaScript.js"></script>

    <title></title>
<script type = text/javascript>


  <%--  function go(NO)
    {
        if (confirm("삭제 하시겠습니까?") == true)
        {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
            if (obj)
                obj.value = NO;

      
       <%= Page.GetPostBackEventReference(Button2) %>
        }
       
    }--%>
   

   
  
    
</script>
</head>
<body>
    <form id="form1" runat="server">

        
        <div id="navbar">    
  <uc1:uc_menu ID="uc_menu" runat="server" />
</div>

        <div class="container-fluid" >


            <div class="alert alert-warning" role="alert"><span class="glyphicon glyphicon-signal" aria-hidden="true"></span> 어플리케이션 리스트</div>


            <nav class="navbar navbar-default" >
    	  <div class="">
		    <!-- Brand and toggle get grouped for better mobile display -->
		    <div class="navbar-btn navbar-right" style="margin-right: 10px" >
                <ui class="nav navbar-nav navbar-right">
                    <li style="margin-top:5px; margin-right:10px">
                        <asp:Label ID="Label5" runat="server" Text="구분 : " CssClass="navbar-left" ></asp:Label>
                    </li>
                    <li style=" margin-right:10px">
                         <asp:DropDownList ID="DropDownList1" runat="server" Height="30" Class="form-control" Width="130px">
                        <asp:ListItem >선택</asp:ListItem>
                        <asp:ListItem Value="1">어플리케이션</asp:ListItem>
                        
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



         


            
            <table style="width:100%; ">
                <td>
                    <center><asp:Label ID="Label2" runat="server" Text="Label" Font-Size="Large" Font-Bold="true"></asp:Label></center>
                </td>
            </table>
            <div style="float:left; margin-top:-38px; text-align:center;">
                <Button ID="Button4" runat="server"   Class="btn btn-primary " OnserverClick="Button4_Click" >
                <span class="glyphicon glyphicon-chevron-left"></span> 이전    
                </Button>
            </div>

            <div style="text-align:right; margin-right:10px; margin-top:-25px; margin-bottom:10px;">

                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                 <%--<a href="Service_modi_all.aspx?no=<%=HiddenField1.Value%>" data-toggle="modal" data-target="#myModal2">--%> 
                <a href="Service_modi_all.aspx?no=<%=HiddenField2.Value%>" data-toggle="modal" data-target="#myModal2">
                    
                 </a>
            </div>

             <asp:Table ID="TBLLIST"  runat="server" CssClass="table table-striped table-bordered table-hover"  Width="100%" ></asp:Table>

           
            
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />
           
            </div>
        <div class="col-md-12" align="center" style="margin-top:-40px">
           <asp:Label ID="Label4" runat="server" Text="Label" CssClass="pagination"></asp:Label>
       </div>
    
        <asp:Label ID="Label3" runat="server" ></asp:Label>
        <asp:HiddenField ID="TextBox5" runat="server" />

      

   
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

  <uc1:uc_menu1 ID="uc_menu1" runat="server" />

    </form>
    
</body>
</html>
