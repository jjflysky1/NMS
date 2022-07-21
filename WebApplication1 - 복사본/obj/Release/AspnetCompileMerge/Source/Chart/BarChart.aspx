<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="BarChart.aspx.cs" Inherits="WebApplication1.BarChart" %>

<%@ Register src="../Common/TopMenu.ascx" tagname="uc_menu" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

  
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.2.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Content/JavaScript.js"></script>
    <script src="../Content/JavaScript.js"></script>
    <link href="../Content/icon.css" rel="stylesheet" />
    <script src="../Scripts/morris.min.js"></script>
   
    <link href="../Scripts/morris.css" rel="stylesheet" />
    <script src="../Scripts/Raphael-min.js"></script>
    
   
 

    <title></title>
<script type="text/javascript">
  
   
</script>

</head>
<body style="background-color:#fafafa; width:100%">
   
   
 
<%-- <label class="switch">
  <input type="checkbox" name="checkbox" id="checkbox" data-toggle="toggle" checked>
  <span class="slider round"></span>
</label>--%>
       <style>
            .custab1 {
    border: 1px solid #ccc;
    padding: 5px;
    margin: 0 0;
    box-shadow: 3px 3px 2px #ccc;
    transition: 0.5s;
}
             .custab1:hover {
        box-shadow: 3px 3px 0px transparent;
        transition: 0.5s;
    }

            
        </style>
    <div style="width:100%; ">
         
         <div id ="Securitychart" runat="server" class="table  custab1"  style="float:left; width:49.5%; margin-left:0px; margin-right:20px; text-align:center; vertical-align:middle; cursor:pointer;">
             <section class="content-header">
      <h1>
        네트워크/보안 장비
      </h1>
                     </section>
    <br />
         </div>

      
        <div id ="serverchart" runat="server" class="table  custab1"  style=" padding-left:0px; float:left; width:48.5%; cursor:pointer;">
         <section class="content-header">
      <h1>
        서버 장비
      </h1>
                     </section>
    <br /> 
        </div>

    </div>

</body>
</html>
