<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="Network_Setting.aspx.cs" Inherits="WebApplication1.Network_Setting" %>
<%@ Register src="../Common/Bottom.ascx" tagname="uc_menu1" tagprefix="uc1" %>
<%@ Register src="../Common/TopMenu.ascx" tagname="uc_menu" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>NMS</title>


    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="/Scripts/jquery-3.2.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script src="/Content/JavaScript.js"></script>
    <script src="/Content/JavaScript.js"></script>

    <script type = text/javascript>

    function go(NO){
        if (confirm("삭제 하시겠습니까?") == true) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
            if (obj)
                obj.value = NO;

       <%= Page.GetPostBackEventReference(Button2) %>
        }
        }

        function modi(NO) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
            if (obj)
                obj.value = NO;

       <%= Page.GetPostBackEventReference(Button3) %>

        }
</script>
  
</head>
<body>
    <form id="form1" runat="server">

           <uc1:uc_menu ID="uc_menu" runat="server" />
      <div class="right" >
            <div class="container-fluid" >
           
                <div style="text-align:right;" >
                    


                    <nav class="navbar navbar-default" >
    	  <div class="" >
		    <!-- Brand and toggle get grouped for better mobile display -->
		    <div class="navbar-btn " style="margin-right: 10px"  >
                <ui class="nav navbar-nav navbar-left">
                   <li style="margin-top:5px; margin-right:10px;margin-left:10px;">
                        <span class="glyphicon glyphicon-signal" aria-hidden="true"style='color:black;'></span> 네트워크 설정
                    </li>
                </ui>
                <ui class="nav navbar-nav navbar-right" >
                    <li style="margin-top:5px; margin-right:10px">
                        <asp:Label ID="Label6" runat="server" Text="네트워크 이름 :" CssClass="navbar-left" ></asp:Label>
                    </li>
                    <li style=" margin-right:10px">
                        <asp:TextBox ID="TextBox1" Height="30px" runat="server" Class="form-control" ></asp:TextBox>
                    </li>

                   <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label7" runat="server" Text="시작 IP : " CssClass="navbar-left"></asp:Label>
                   </li>
                   <li style="margin-right:10px">
                       <asp:TextBox ID="TextBox2" Height="30px" runat="server" Class="form-control"></asp:TextBox>
                   </li>


                   <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label8" runat="server" Text="종료 IP : " CssClass="navbar-left"></asp:Label>
                   </li>
                    <li style="margin-right:10px">
                       <asp:TextBox ID="TextBox3" Height="30px" runat="server" Class="form-control"></asp:TextBox>
                   </li>


                    <li style="margin-right:10px">
                         <asp:Button ID="Button1" runat="server" Text="등록" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" />
                    </li>
                </ui>
                  
            </div>
              </div>
                </nav>

 
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
           </div>
                <div style="margin-top:-50px;">
             <asp:Table ID="TBLLIST"  runat="server" CssClass="table  custab "  Width="100%" ></asp:Table>
                </div>
                </div>

              <asp:HiddenField ID="HiddenField1" runat="server" />
        
            <asp:Button ID="Button2" runat="server" Text="Button" Visible="false" OnClick="Button2_Click" />
            <asp:Button ID="Button3" runat="server" Text="Button" Visible="false" OnClick="Button3_Click" />
           <uc1:uc_menu1 ID="uc_menu1" runat="server" />
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
    </form>
</body>
</html>
