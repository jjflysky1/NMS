<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="Mail_modi.aspx.cs" Inherits="WebApplication1.Mail_modi" %>
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

  
    function mail_go(No, mail) {

        var obj = document.getElementById("<%=HiddenField20.ClientID %>");
         if (obj)
            obj.value = No;

         var obj = document.getElementById("<%=HiddenField30.ClientID %>");
         if (obj)
             obj.value = mail;


       <%= Page.GetPostBackEventReference(Button20) %>

    }

    function mail_down(No, mail) {
        var obj = document.getElementById("<%=HiddenField10.ClientID %>");
            if (obj)
                obj.value = No;
            var obj = document.getElementById("<%=HiddenField20.ClientID %>");
        if (obj)
            obj.value = No;

       <%= Page.GetPostBackEventReference(Button21) %>

    }


</script>
</head>
<body>
    <form id="form1" runat="server" >
       <%-- <asp:Button ID="Button20" runat="server" Text="Button"   Visible="false"/>
        <asp:Button ID="Button21" runat="server" Text="Button"  Visible="false"/>--%>
      
        
       
  <uc1:uc_menu ID="uc_menu" runat="server" />

        <div class="right" >
      
      <asp:Button ID="Button20" runat="server" Text="Button"  OnClick="Button20_Click" Visible="false"/>
        <asp:Button ID="Button21" runat="server" Text="Button" OnClick="Button21_Click" Visible="false"/>

         <div class="container-fluid" >
         <table style="width:100%; ">
                <td>
                    <center><asp:Label ID="Label2" runat="server" Text="Label" Font-Size="Large" Font-Bold="true"></asp:Label></center>
                </td>
            </table>
            <div style="float:left; margin-top:-25px; text-align:center;">
                <Button ID="Button4" runat="server"   Class="btn btn-primary " OnserverClick="Button4_Click" >
                <span class="glyphicon glyphicon-chevron-left" style="color:white;"></span> 이전    
                </Button>
            </div>

             <br />
        
              <center>
                  
                  <div style="float:left; width:48%; margin-right:20px; margin-top:-20px;">
                     <b>유저 리스트</b>
                    <asp:Table ID="TBLLIST"  runat="server" CssClass="table  custab "  Width="100%" ></asp:Table>  
	             </div>
                  <div style="float:left; width:48%; margin-top:-20px;">
                      <b>등록된 유저</b>
                    <asp:Table ID="TBLLIST1"  runat="server" CssClass="table  custab "  Width="100%" ></asp:Table>
                  </div>
                   

                  </center>
						
      
                     
       
                </div>
            


<asp:HiddenField ID="HiddenField10" runat="server" />
        <asp:HiddenField ID="HiddenField20" runat="server" />
        <asp:HiddenField ID="HiddenField30" runat="server" />
        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
        <uc1:uc_menu1 ID="uc_menu1" runat="server" />

      </div>
    </form>
    
</body>
</html>
