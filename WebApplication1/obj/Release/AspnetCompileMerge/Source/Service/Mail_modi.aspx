<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="Mail_modi.aspx.cs" Inherits="WebApplication1.Mail_modi" %>
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
    
    <title></title>
    <script type = text/javascript>

    function mail_go(No, mail,phone) {

        var obj = document.getElementById("<%=HiddenField20.ClientID %>");
         if (obj)
            obj.value = No;

         var obj = document.getElementById("<%=HiddenField30.ClientID %>");
         if (obj)
            obj.value = mail;

        var obj = document.getElementById("<%=HiddenField40.ClientID %>");
         if (obj)
             obj.value = phone;


       <%= Page.GetPostBackEventReference(Button20) %>

    }

    function mail_down(No, mail, phone) {
        var obj = document.getElementById("<%=HiddenField10.ClientID %>");
            if (obj)
                obj.value = No;
            var obj = document.getElementById("<%=HiddenField20.ClientID %>");
        if (obj)
            obj.value = No;
        var obj = document.getElementById("<%=HiddenField40.ClientID %>");
         if (obj)
            obj.value = phone;

       <%= Page.GetPostBackEventReference(Button21) %>

    }



</script>
</head>
<body class="hold-transition skin-blue sidebar-mini">
    <form id="form1" runat="server"  style="height:100%;">
   <div class="wrapper" > 
   <uc1:uc_menu ID="uc_menu" runat="server" />
       <div class="content-wrapper">
   
            <section class="content-header">
              <h1>
                메일리스트
                <small>메일리스트들이 보여집니다</small>
              </h1>
              <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                <li class="active"><a href="#">메일리스트</a></li>
              </ol>
            </section>


    <section class="content">
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
                <Button ID="Button3" runat="server"   Class="btn btn-primary " OnserverClick="Button4_Click" >
                <span class="glyphicon glyphicon-chevron-left" style="color:white;"></span> 이전    
                </Button>
            </div>

             <br />
        
              <center>
                  
                  <div style="float:left; width:48%; margin-right:20px; margin-top:0px;" class="box box-info">
                     <br /><b>유저 리스트</b><br /><br />
                    <asp:Table ID="TBLLIST"  runat="server" CssClass="table  custab table-hover "  Width="100%" ></asp:Table>  
	             </div>
                  <div style="float:left; width:48%; margin-top:0px;" class="box box-info">
                      <br /><b>등록된 유저</b><br /><br />
                    <asp:Table ID="TBLLIST1"  runat="server" CssClass="table  custab table-hover "  Width="100%" ></asp:Table>
                  </div>
                   

                  </center>
						
      
                     
       
                </div>
            


        <asp:HiddenField ID="HiddenField10" runat="server" />
        <asp:HiddenField ID="HiddenField20" runat="server" />
        <asp:HiddenField ID="HiddenField30" runat="server" />
                  <asp:HiddenField ID="HiddenField40" runat="server" />
          <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
        

      </div>
          </section>
           </div>

       <uc2:uc_bottom ID="uc_bottom" runat="server" />
       
        
        
       </form>
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/adminlte.min.js"></script>
       
       
       
       
       

        

</body>
</html>