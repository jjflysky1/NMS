<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="WebApplication1.Chart" %>

<%@ Register src="../Common/TopMenu.ascx" tagname="uc_menu" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

<%--   <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.2.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Content/JavaScript.js"></script>
    <script src="../Content/JavaScript.js"></script>
    <script src="../Scripts/morris.min.js"></script>
    <link href="../Scripts/morris.css" rel="stylesheet" />
    <script src="../Scripts/Raphael-min.js"></script>
   --%>
    
  
<%-- <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>--%>
 <%--<script src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>--%>
 <%--<script src="//cdnjs.cloudflare.com/ajax/libs/morris.js/0.5.1/morris.min.js"></script>--%>
   
    <title></title>
<script type="text/javascript">
  

    var showAlert = setTimeout(function () {
        
    }, 10000);


   
        setInterval(function () {
      
            //alert("2초마다 반복 실행됩니다.");
        }, 1000);


    
</script>

</head>
<body style="background-color:#fafafa; width:30%">

    <style>
        .morris-hover{ margin-top:-100px; }
    </style>
             <center>
                <section class="content-header">
                  <h4>
                   <label id="title" runat="server" ></label>
                  </h4>
                     </section>
           
                
         <div id="myfirstchart" style="z-index:99; width:100%; background-color:white;margin-right:0px; margin-left:0px;  height: 150px; cursor:pointer;"></div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
               
               </center>
  
   <%-- <form runat="server">
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </form>--%>
</body>
</html>
