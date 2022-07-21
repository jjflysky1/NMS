<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="WebApplication1.main" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register src="Common/TopMenu.ascx" tagname="uc_menu" tagprefix="uc1" %>
<%@ Register src="Common/Bottom.ascx" tagname="uc_menu1" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="/Scripts/jquery-3.2.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script src="/Content/JavaScript.js"></script>
    <script src="/Content/JavaScript.js"></script>
    <link href="/Content/icon.css" rel="stylesheet" />
    <script src="/Scripts/morris.min.js"></script>
    <link href="/Scripts/morris.css" rel="stylesheet" />
    <script src="/Scripts/Raphael-min.js"></script>

    
  
<%-- <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>--%>
 <%--<script src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>--%>
 <%--<script src="//cdnjs.cloudflare.com/ajax/libs/morris.js/0.5.1/morris.min.js"></script>--%>
   
    <title></title>
<script type="text/javascript">
  

    $(document).ready(function () {
        $("#div1").load("Chart/Chart.aspx");
        $("#div2").load("Chart/Chart2.aspx");
        $("#allchart").load("Chart/BarChart.aspx");
    });
    setInterval(function () {
        $("#div1").load("Chart/Chart.aspx");
        $("#div2").load("Chart/Chart2.aspx");
        $("#allchart").load("Chart/BarChart.aspx");
    },60000)
    //60초

    setInterval(function () {
        //document.getElementById("Button6").click();

    },2000)
    function go(No) {
        var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;

       <%= Page.GetPostBackEventReference(Button2) %>

    }
    function go3(No) {
        var obj = document.getElementById("<%=HiddenField1.ClientID %>");
          if (obj)
              obj.value = No;

       <%= Page.GetPostBackEventReference(Button5) %>

      }
    function go2(No,category) {
        var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;
        var obj = document.getElementById("<%=HiddenField2.ClientID %>");
        if (obj)
            obj.value = category;

       <%= Page.GetPostBackEventReference(Button4) %>

    }





    //$(function () {
    //    $('.left').mouseenter(function () {
    //        $('#inner1').css({ visibility: 'hidden' })
           
    //    })
    //    $('.left').mouseleave(function () {
    //        $('.item').css({ visibility: 'hidden' }).fadeOut(100)

    //    })
    //})
    
   
</script>
    <style>
        
.progress {
    height: 12px;
    background: #ebedef;
    border-radius: 32px;
    box-shadow: none
}

.progress-bar {
    line-height: 12px;
    background: #1abc9c;
    box-shadow: none
}

.progress-bar-success {
    background-color: #2ecc71
}

.progress-bar-warning {
    background-color: #f1c40f
}

.progress-bar-danger {
    background-color: #e74c3c
}

.progress-bar-info {
    background-color: #3498db
}

    </style>
</head>
<body  style="background-color:#ecf0f5;  -ms-overflow-style: none;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
      
 <uc1:uc_menu ID="uc_menu" runat="server" />

   <div class="right" style="" >
        <table style="width:100%; margin-top:10px; ">
             <tr>
                 <td>
                      <div class="box-header " style="width:100%; margin-left:10px; margin-bottom:-50px; margin-top:20px;">    
                          <div style=" margin-top:-60px; margin-right:30px;" >
                               <center>
                           <h2>
                            Dashboard
                            <small>Light Network Management</small>
                          </h2>
                              </center>
                          </div>
                          <div  style="  float:right; margin-top:0px; margin-right:30px; margin-bottom:-20px;" >
                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                      <ContentTemplate>
                                                <asp:Timer ID="Timer1" runat="server" Interval="5700" ></asp:Timer>
                                      <span style=" color:black; " class="glyphicon glyphicon-time" aria-hidden="true"></span>   <font size="3"> <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></font>
                                      </ContentTemplate>
                                      </asp:UpdatePanel>
                          </div>
         
                </div>
                       <div >

                   <div class="box-header " style="margin-left:10px; margin-bottom:0px; " >
                            <section class="content-header">
                      <h1>
                       <span style=" color:black; " class="glyphicon glyphicon-hdd" aria-hidden="true"></span> 장비 수량
                      </h1>
                     </section>
                        </div>
                        <div class="table  custab1" style="margin-left:30px;  margin-bottom:0px;  width:97.5%; height:170px; ">
                          <%--  <div>
                               
                                <img src="SERVER.png" width="70px" height="70px"/> 
                                <img src="COMPUTER.png" width="70px" height="70px"/>     
                            </div>--%>
                           
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                         <asp:Table ID="TBLLIST0"   runat="server" CssClass="table no-border"  Width="100%" ></asp:Table>
                                    </ContentTemplate>
                                 </asp:UpdatePanel>
                </div>
        </div> 
                     <%--<div id="allchart" runat="server" style="margin-left:30px; margin-top:30px; width:98.5%; margin-right:0; vertical-align:middle; "></div>--%>
                 </td>
             </tr>
       
             </table>
    <%--  </div>
      </div>--%>
        
        <div style="width:100%;  ">
            <div style="float:left; width:48.8%; margin-left:0px; margin-right:20px; vertical-align:middle; ">
                 <div class="box-header " style="margin-left:10px;  ">
                      <section class="content-header">
                      <h1>
                        <span style=" color:black;" class="glyphicon glyphicon-tasks" aria-hidden="true"></span> 네트워크/보안 자원 List
                      </h1>
                     </section>
                </div>
                <div class="table  custab1" style=" margin-left:30px; height:227px; ">
                         <asp:UpdatePanel ID="UpdatePanel6" runat="server"  UpdateMode="Always">
                            <ContentTemplate>
                                 <asp:Table ID="TBLLIST3_1"    runat="server" CssClass="table no-border"  Width="100%" ></asp:Table>
                                </ContentTemplate>
                         </asp:UpdatePanel>
                </div>
                  <div class="box-header " style="width:100%; text-align:center; margin-top:-30px; margin-left:20px; margin-bottom:-40px;">
                      <section class="content-header">
                      <h3>
                      Secure State
                      </h3>
                     </section>
                </div>
                <br /><br />
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                          <div class="cssload-loader"  runat="server" id="inner1" style="  margin-left:20px;">
	<div class="cssload-inner cssload-one" runat="server" id="inner1_1"></div>
	<div class="cssload-inner cssload-two" runat="server" id="inner1_2"></div>
	<div class="cssload-inner cssload-three" runat="server" id="inner1_3"></div>
</div>
                            <div class="cssload-loader1"  runat="server" id="Div3" style=" margin-top:-62px; margin-left:20px;">
	<div class="cssload-inner1 cssload-one" runat="server" id="Div4"></div>
	<div class="cssload-inner1 cssload-two" runat="server" id="Div5"></div>
	<div class="cssload-inner1 cssload-three" runat="server" id="Div6"></div>
</div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                         
                
               <%--  <div class="cssload-thecube" runat="server" id="cube" style="float:left; margin-left:50%;">
	<div class="cssload-cube cssload-c1"></div>
	<div class="cssload-cube cssload-c2"></div>
	<div class="cssload-cube cssload-c4"></div>
	<div class="cssload-cube cssload-c3"></div>
</div>--%>
 <br />
                     <div class="box-header " style="margin-left:10px; margin-top:-50px; margin-bottom:-30px;">
                      <section class="content-header">
                      <h1>
                        <span style="color:black;" class="glyphicon glyphicon-stats" aria-hidden="true"></span> 네트워크/보안 차트
                      </h1>
                     </section>
                </div>

                      <div id="chart1" class="table  custab1" style="z-index:100;background-color:white; margin-top:30px; margin-left:30px; margin-right:0px; width:100%; height:220px; float:left; margin-bottom:20px;">
        <div style=" margin-bottom:-50px; float:right; z-index:99; position:relative;">
          <asp:DropDownList ID="DropDownList2"  runat="server" Height="35"  Class="form-control" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                         <asp:ListItem Value="1" >선택</asp:ListItem>
                        <asp:ListItem Value="1">1일</asp:ListItem>
                        <asp:ListItem Value="7">7일</asp:ListItem>
                       <%-- <asp:ListItem Value="30">30일</asp:ListItem>
                        <asp:ListItem Value="365">1년</asp:ListItem>--%>
                </asp:DropDownList>
            </div>
                    
          
                 <div id="div1" class="div1" style="width:100%; background-color:white; height:210px; margin-top:0px; "></div>
            
                                </div>

                  <div  class="box-header ">
                            <section class="content-header">
                      <h1>
                       <span style="transform:rotate(180deg); color:black;" class="glyphicon glyphicon-tag" aria-hidden="true"></span> 네트워크/보안 다운 장비
                      </h1>
                     </section>
                        </div>
                        <div class="table  custab1" style=" margin-left:-5px; padding-right:-30px;margin-left:30px;  ">
                          <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                             <asp:Table ID="TBLLIST2"   runat="server" CssClass="table no-border"  Width="100%" ></asp:Table>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                         </div>
              

            </div>
            <div style=" padding-left:30px; float:left; width:49.2%;margin-left:0px; margin-right:0; margin-bottom:40px;  vertical-align:middle;  ">
                 <div class="box-header " style="margin-left:-20px; ">
                      <section class="content-header">
                      <h1>
                        <span style=" color:black;" class="glyphicon glyphicon-tasks" aria-hidden="true"></span> 서버 자원 List
                      </h1>
                     </section>
                </div>
                <div class="table  custab1" style=" height:227px; ">
                         <asp:UpdatePanel ID="UpdatePanel3" runat="server"  UpdateMode="Always">
                            <ContentTemplate>
                                 <asp:Table ID="TBLLIST3"    runat="server" CssClass="table no-border "  Width="100%" ></asp:Table>
                                </ContentTemplate>
                         </asp:UpdatePanel>
                </div>
                <div class="box-header " style="width:100%; text-align:center; margin-top:-30px; margin-left:20px; margin-bottom:-40px; ">
                      <section class="content-header">
                      <h3>
                      Server State
                      </h3>
                     </section>
                </div>
                <br /><br />
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                            <div class="cssload-loader"  runat="server" id="inner2" style=" margin-left:20px;">
	<div class="cssload-inner cssload-one" runat="server" id="inner2_1"></div>
	<div class="cssload-inner cssload-two" runat="server" id="inner2_2"></div>
	<div class="cssload-inner cssload-three" runat="server" id="inner2_3"></div>
                                </div>
                                     <div class="cssload-loader1"  runat="server" id="Div7" style="  margin-top:-62px; margin-left:20px;">
	<div class="cssload-inner1 cssload-one" runat="server" id="Div8"></div>
	<div class="cssload-inner1 cssload-two" runat="server" id="Div9"></div>
	<div class="cssload-inner1 cssload-three" runat="server" id="Div10"></div>
                                         </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                            
                                    
                 
           

              <%--  <div class="cssload-thecube" runat="server" id="cube2" style="float:left;  margin-left:50%;">
	<div class="cssload-cube cssload-c1"></div>
	<div class="cssload-cube cssload-c2"></div>
	<div class="cssload-cube cssload-c4"></div>
	<div class="cssload-cube cssload-c3"></div>
</div>--%>
                <br />
                   <div class="box-header " style="margin-left:-20px;  margin-top:-50px; margin-bottom:-30px;">
                      <section class="content-header">
                      <h1>
                        <span style=" color:black;" class="glyphicon glyphicon-stats" aria-hidden="true"></span> 서버 차트
                      </h1>
                     </section>
                </div>
                      <div class="table  custab1" style="z-index:100; margin-top:30px; margin-right:0px; width:100%; height:220px; float:left;margin-bottom:20px;">
        <div style=" margin-bottom:-50px; float:right; z-index:99; position:relative;">
          <asp:DropDownList ID="DropDownList1"  runat="server" Height="35"  Class="form-control" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                 <asp:ListItem value="1">선택</asp:ListItem>          
              <asp:ListItem Value="1">1일</asp:ListItem>
                        <asp:ListItem Value="7">7일</asp:ListItem>
                       <%-- <asp:ListItem Value="30">30일</asp:ListItem>
                        <asp:ListItem Value="365">1년</asp:ListItem>--%>
                </asp:DropDownList>
            </div>
                 <div id="div2" style="width:100%; height:210px;margin-top:0px;"></div>
                                </div>

                      


                <div class="box-header " style="margin-left:-20px;  ">
                      <section class="content-header">
                      <h1>
                        <span style="transform:rotate(180deg); color:black;" class="glyphicon glyphicon-tag" aria-hidden="true"></span> 서버 다운 장비
                      </h1>
                     </section>
                </div>
                <div class="table  custab1" style=" ">
                         <asp:UpdatePanel ID="UpdatePanel4" runat="server"  UpdateMode="Always">
                            <ContentTemplate>
                                 <asp:Table ID="TBLLIST"    runat="server" CssClass="table no-border "  Width="100%" ></asp:Table>
                                </ContentTemplate>
                         </asp:UpdatePanel>
                </div>
             
            

         
        </div>
            </div>
       

      
       <%--     <div style="float:left; width:48.8%; margin-left:0px; margin-right:20px; vertical-align:middle; ">
                 <div class="box-header " style="margin-left:10px; ">
                      <section class="content-header">
                      <h1>
                        <span style="transform:rotate(180deg);" class="glyphicon glyphicon-tag" aria-hidden="true"></span> 대역대
                      </h1>
                     </section>
                </div>
                <div class="table  custab1" style="margin-bottom:-30px; margin-left:30px; ">
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server"  UpdateMode="Always">
                            <ContentTemplate>
                                 <asp:Table ID="TBLLIST7"  BackColor="Transparent"   runat="server" CssClass="table "  Width="100%" ></asp:Table>
                                </ContentTemplate>
                         </asp:UpdatePanel>
                </div>

                </div>
--%>


<%--            <div style=" padding-left:30px; float:left; width:49.2%;margin-left:0px; margin-right:0; margin-bottom:40px;  vertical-align:middle; ">
                 <div class="box-header " style="margin-left:-20px; " >
                            <section class="content-header">
                      <h1>
                       <span style="transform:rotate(180deg);" class="glyphicon glyphicon-tag" aria-hidden="true"></span> 트래픽 TOP
                      </h1>
                     </section>
                        </div>
                        <div class="table  custab1" style=" margin-left:-5px; padding-right:-30px; ">
                                 <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                         <asp:Table ID="TBLLIST4" BackColor="Transparent"   runat="server" CssClass="table"  Width="100%" ></asp:Table>
                                    </ContentTemplate>
                                 </asp:UpdatePanel>
                </div>--%>

              
               
                 

          
           
                    <%--<div class="col-md-3 col-sm-6 col-xs-12" style="cursor:pointer; position:relative; width:17%;" onclick="location.href='/Log/System_Log.aspx'">
                      <div class="info-box">
                        <span class="info-box-icon bg-yellow"><i style="margin-top:20px" class="glyphicon glyphicon-signal"></i></span>

                        <div class="info-box-content">
                          <span class="info-box-text">
                              평균 트래픽 TOP</span>
                            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label> 서버
                          <span class="info-box-number">
                              <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label> <small> Bits / S</small>
                          </span>
                        </div>
                        <!-- /.info-box-content -->
                      </div>
                      <!-- /.info-box -->
                    </div>

                    <div class="col-md-3 col-sm-6 col-xs-12" style="cursor:pointer; position:relative; width:17%;" onclick="location.href='/Log/System_Log.aspx'">
                      <div class="info-box">
                        <span class="info-box-icon bg-aqua"><i style="margin-top:20px" class="glyphicon glyphicon-cog"></i></span>

                        <div class="info-box-content">
                          <span class="info-box-text">
                              메모리 TOP</span>
                            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label> 서버
                          <span class="info-box-number">
                              <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label> <small> %</small>
                          </span>
                        </div>
                        <!-- /.info-box-content -->
                      </div>
                      <!-- /.info-box -->
                    </div>--%>

      
                   
        


<%--                        <div class="info-box">
                      <div class="info-box-content" style="margin-left:-12px;  width:100%">
                        <ajaxToolkit:LineChart  ID="LineChart1" runat="server" BorderWidth="0"  ChartWidth="1800" ChartHeight="300px" ChartType="Basic"  ChartTitleColor="#0E426C" CategoryAxisLineColor="#D08AD9" ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB" ></ajaxToolkit:LineChart>
                        </div>
                      </div>--%>
        
      
                   <%--첫번째--%>
		 <%--<table  style="width:100%;">
             <tr>
                   <td style="width:30%;" >
                <div class="">
            <br />
             <div class="box box-info" style="margin-left:25px;">
            <div style="background-color:white; border-color:#eaeaea" class="panel-body table-bordered">
                <div class="box-header with-border">
                    <font size="5"><b>다운 서버 TOP</b></font>
                </div>
                <div class="" style="margin-bottom:-30px;">
                         <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                          
                                 <asp:Table ID="TBLLIST"  runat="server" CssClass="table tabletable-borderedtable-hover"  Width="100%" ></asp:Table>
                            </ContentTemplate>
                         </asp:UpdatePanel>
                </div>
            </div>
         </div>
	        </div>
            <td style="width:3%">
            </td>
             </td>
             <td style="vertical-align:top; width:30%; ">
                <div class=""  >
                 <br />
                    <div class="box box-info" style="margin-right:25px;"">
                    <div style="background-color:white; border-color:#eaeaea" class="panel-body table-bordered">
                        <div  class="box-header with-border">
                            <font size="5"><b>다운 보안 / 네트워크 장비 TOP</b></font>
                        </div>
                        <div class="" style="margin-bottom:-30px;">
                          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                         
                             <asp:Table ID="TBLLIST2"  runat="server" CssClass="table tabletable-bordered "  Width="100%" ></asp:Table>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                </div>
            </div>
        </div>
	</div>
             </td>
                 <td style="width:2%">
                 </td>
             </tr>
           
		 </table>--%>
		
      
        <%--두번째--%>
    

  <%--세번째--%>

            <%--    <div class=""  >
                 <br />
                    
                        <div class="box-header " style="margin-left:-20px; " >
                            <section class="content-header">
                      <h1>
                       <span style="transform:rotate(180deg);" class="glyphicon glyphicon-tag" aria-hidden="true"></span> 트래픽 TOP
                      </h1>
                     </section>
                        </div>
                        <div class="table  custab1" style="margin-bottom:-30px; ">
                                 <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                         <asp:Table ID="Table2" BackColor="Transparent"   runat="server" CssClass="table"  Width="100%" ></asp:Table>
                                    </ContentTemplate>
                                 </asp:UpdatePanel>
                </div>
         
	            </div>--%>

    
  
          <uc1:uc_menu1 ID="uc_menu1" runat="server" />
                  </div>

     
            <asp:HiddenField ID="HiddenField1" runat="server" />
                 <asp:HiddenField ID="HiddenField2" runat="server" />
        <asp:Button ID="Button2" runat="server" Text="Button" Visible="false" OnClick="Button2_Click" />
        <asp:Button ID="Button4" runat="server" Text="Button" Visible="false" OnClick="Button4_Click"/>
        <asp:Button ID="Button5" runat="server" Text="Button" Visible="false" OnClick="Button5_Click" />
        <asp:Button ID="Button3" runat="server" Text="Button" Visible="false" OnClick="Button3_Click" />
    
          <asp:Button  ID="Button1" Visible="false" runat="server" Text="Button" OnClick="Button1_Click" />

        <asp:Label ID="Label3" runat="server" ></asp:Label>
        <asp:HiddenField ID="TextBox5" runat="server" />
        

     
    </form>
    
</body>
</html>
