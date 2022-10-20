<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="Mail_Setting.aspx.cs" Inherits="WebApplication1.Mail_Setting" %>
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
  function go(NO) {
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

</script>
</head>
<body class="hold-transition skin-blue sidebar-mini" >
    <form id="form1" runat="server" style="height:100%;" >
   <div class="wrapper"> 
   <uc1:uc_menu ID="uc_menu" runat="server" />
       <div class="content-wrapper">
   
            <section class="content-header">
              <h1>
                연락처 설정
                <small>연락처 설정</small>
              </h1>
              <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                <li class="active"><a href="#">연락처 설정</a></li>
              </ol>
            </section>


    <section class="content">
            <div class="right" >
            <div class="container-fluid" >

                <div style="text-align:right;" >
                  


                        <nav class="navbar " >
    	  <div  class="">
		    <!-- Brand and toggle get grouped for better mobile display -->
		    <div  class="navbar-btn" style="margin-right: 10px" >
                 
                <ui  class="nav navbar-nav navbar-right">
                    <li style="margin-top:5px; margin-right:10px">
                        <asp:Label ID="Label6" runat="server" Text="메일 :" CssClass="navbar-left" ></asp:Label>
                    </li>
                    <li style=" margin-right:10px">
                        <asp:TextBox ID="TextBox1" Height="30px" Width="140px" runat="server" Class="form-control" ></asp:TextBox>
                    </li>

                   <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label7" runat="server" Text="CPU 기준 : " CssClass="navbar-left"></asp:Label>
                   </li>
                   <li style="margin-right:10px">
                       <asp:TextBox ID="TextBox2" Height="30px" Width="140px" runat="server" Class="form-control"></asp:TextBox>
                   </li>


                   <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label8" runat="server" Text="Memory 기준 : " CssClass="navbar-left"></asp:Label>
                   </li>
                    <li style="margin-right:10px">
                       <asp:TextBox ID="TextBox3" Height="30px" Width="140px" runat="server" Class="form-control"></asp:TextBox>
                   </li>

                    <li style="margin-top:5px;  margin-right:10px">
                       <asp:Label ID="Label2" runat="server" Text="Traffic 기준(MB/S) : " CssClass="navbar-left"></asp:Label>
                   </li>
                    <li style="margin-right:10px">
                       <asp:TextBox ID="TextBox4" Height="30px" Width="140px" runat="server" Class="form-control"></asp:TextBox>
                   </li>

                    <li style="margin-right:10px">
                         <asp:Button ID="Button3" runat="server" Text="등록" CssClass="btn btn-primary btn-sm" OnClick="Button1_Click" />
                    </li>
                </ui>
                  
            </div>
              </div>
                </nav>
         
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
           </div>
                <div style="margin-top:0px;" class="box box-info">
             <asp:Table ID="TBLLIST"  runat="server" CssClass="table  custab"  Width="100%" ></asp:Table>
                </div>
           <div class="" align="center" style="margin-top:-50px; margin-bottom:-40px;">
           
           </div>
          </section>
           </div>

       <uc2:uc_bottom ID="uc_bottom" runat="server" />
       
           <asp:HiddenField ID="HiddenField1" runat="server" />
        
            <asp:Button ID="Button2" runat="server" Text="Button" Visible="false" OnClick="Button2_Click" />
            <asp:Button ID="Button1" runat="server" Text="Button" Visible="false" OnClick="Button3_Click" />
            <asp:Button ID="Button4" runat="server" Text="Button" Visible="false" OnClick="Button4_Click"/>
            <asp:Button ID="Button5" runat="server" Text="Button" Visible="false" OnClick="Button5_Click"/>
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
       </div>


        
        
       </form>
    <script src="../Scripts/jquery.min.js"></script>
    <script src="../Scripts/adminlte.min.js"></script>
       
       
       
       
       

        

</body>
</html>