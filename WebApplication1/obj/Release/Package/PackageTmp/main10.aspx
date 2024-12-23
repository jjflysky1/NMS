<%@ Page MaintainScrollPositionOnPostback="true" EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="main10.aspx.cs" Inherits="WebApplication1.main10" %>

<%@ Register Src="~/Common/leftmenu.ascx" TagName="uc_menu" TagPrefix="uc1" %>
<%@ Register Src="~/Common/bottom.ascx" TagName="uc_bottom" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel='stylesheet' href="Content/bootstrap.min.css" />
    <link rel="stylesheet" href="Content/AdminLTE.min.css" />
    <%--<link rel="stylesheet" href="Content/font-awesome.min.css" />--%>
    <link rel="stylesheet" href="Scripts/sb-admin-2.min.css" />
    <link rel="stylesheet" href="Content/grid_card.css" />
    <%--<link rel="stylesheet" href="Content/_all-skins.min.css" />
    <link rel="stylesheet" href="Content/ionicons.min.css" />--%>
    <link rel="stylesheet" href="Content/bootstrap.min2.css" />
    <script src="Scripts/AdminLTE.css"></script>
    <%--<link href="Scripts/Allcss.css" rel="stylesheet" />--%>

    

    <script src="Scripts/bootstrap.min.js"></script>
    <%--<script src="Content/JavaScript.js"></script>--%>
<%--    <script src="Scripts/highcharts.js"></script>
    <script src="Scripts/highcharts-3d.js"></script>--%>
  <%--  <script src="Scripts/exporting.js"></script>
    <script src="Scripts/export-data.js"></script>--%>
    <script src="Scripts/chart.js"></script>
    <script src="Scripts/jquery-3.2.1.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <script src="Scripts/dragdrop.js"></script>
    <script src="Scripts/dash_chart.js"></script>



    <title></title>
    <script type="text/javascript">



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
        function go2(No, category) {
            var obj = document.getElementById("<%=HiddenField1.ClientID %>");
            if (obj)
                obj.value = No;
            var obj = document.getElementById("<%=HiddenField2.ClientID %>");
            if (obj)
                obj.value = category;

       <%= Page.GetPostBackEventReference(Button4) %>

        }
    </script>
</head>
<body id="contain">
    <form id="form1" runat="server" style="height: 100%;">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <section class="content">
            <div class="row">
                <table style="width: 100%; margin-top: 10px;">
                    <tr>
                        <td>
                            <div class="box-header " style="width: 100%; margin-left: 10px; margin-bottom: -50px; margin-top: 20px; z-index: 99;">
                                <div style="float: right; margin-top: 0px; margin-right: 30px; margin-bottom: -20px;">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Timer ID="Timer1" runat="server" Interval="5700"></asp:Timer>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>

                            <div style="margin-left: 20px; margin-bottom: -50px; margin-top: 0px; width: 97.5%; height: 200px;">
                                <div style="margin-left: 1%">
                                    <h4>
                                        <span style="color: #d4d4d4;" class="glyphicon glyphicon-hdd" aria-hidden="true"></span><font color="#d4d4d4">장비 수량</font>
                                    </h4>
                                </div>
                                <center>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                                        <ContentTemplate>
                                            <%--전체수--%>
                                            <div class="card mb-4 py-3 border-left-success" style="width: 13%; height: 100px; float: left; opacity: 0.9;">
                                                <div class="card-body">
                                                    <asp:Table ID="TBLLIST0" BackColor="Transparent" runat="server" CssClass="table no-border" Width="100%"></asp:Table>
                                                </div>
                                            </div>
                                            <%--관리하는수--%>
                                            <div class="card mb-4 py-3 border-left-success" style="width: 13%; height: 100px; float: left; margin-left: 1.5%; opacity: 0.9;">
                                                <div class="card-body">
                                                    <asp:Table ID="TBLLIST01" BackColor="Transparent" runat="server" CssClass="table no-border" Width="100%"></asp:Table>
                                                </div>
                                            </div>
                                            <%--보안--%>
                                            <div class="card mb-4 py-3 border-left-success" style="width: 13%; height: 100px; float: left; margin-left: 1.5%; opacity: 0.9;">
                                                <div class="card-body">
                                                    <asp:Table ID="TBLLIST02" BackColor="Transparent" runat="server" CssClass="table no-border" Width="100%"></asp:Table>
                                                </div>
                                            </div>
                                            <%--서버--%>
                                            <div class="card mb-4 py-3 border-left-success" style="width: 13%; height: 100px; float: left; margin-left: 1.5%; opacity: 0.9;">
                                                <div class="card-body">
                                                    <asp:Table ID="TBLLIST03" BackColor="Transparent" runat="server" CssClass="table no-border" Width="100%"></asp:Table>
                                                </div>
                                            </div>
                                            <%--PC--%>
                                            <div class="card mb-4 py-3 border-left-success" style="width: 13%; height: 100px; float: left; margin-left: 1.5%; opacity: 0.9;">
                                                <div class="card-body">
                                                    <asp:Table ID="TBLLIST04" BackColor="Transparent" runat="server" CssClass="table no-border" Width="100%"></asp:Table>
                                                </div>
                                            </div>
                                            <%--대상제외--%>
                                            <div class="card mb-4 py-3 border-left-success" style="width: 13%; height: 100px; float: left; margin-left: 1.5%; opacity: 0.9;">
                                                <div class="card-body">
                                                    <asp:Table ID="TBLLIST05" BackColor="Transparent" runat="server" CssClass="table no-border" Width="100%"></asp:Table>
                                                </div>
                                            </div>
                                            <%--서비스 다운--%>
                                            <div class="card mb-4 py-3 border-left-danger" style="width: 13%; height: 100px; float: left; margin-left: 1.5%; opacity: 0.9;">
                                                <div class="card-body">
                                                    <asp:Table ID="TBLLIST06" BackColor="Transparent" runat="server" CssClass="table no-border" Width="100%"></asp:Table>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </center>
                            </div>
                        </td>
                    </tr>
                </table>

                <div style="width: 20.5%; height: 100%; float: left;">
                    <div style="float: left; width: 100%; margin-left: 0px; height: 100%; vertical-align: middle;">
                        <div class="card card0 no-border" style="margin-left: 30px; height: 260px; margin-left: 3%; background-color: rgba(0, 0, 0, 0.3);">
                            <div style="margin-left: 2%">
                                <h4>
                                    <span style="color: #d4d4d4;" class="glyphicon glyphicon-tasks" aria-hidden="true"></span><font color="#d4d4d4">네트워크/보안 자원 List</font>
                                </h4>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Table ID="TBLLIST3_1" runat="server" CssClass="table no-border" Width="100%"></asp:Table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="card card0 no-border" style="margin-left: 3%; margin-top: 5px; background-color: rgba(0, 0, 0, 0.3);">
                            <div style="margin-left: 2%">
                                <h4>
                                    <span style="color: #d4d4d4;" class="glyphicon glyphicon-stats" aria-hidden="true"></span><font color="#d4d4d4">네트워크/보안 차트</font>
                                </h4>
                            </div>
                            <div id="div1" style="width: 100%; height: 210px; margin-top: 0px;">
                                <font color="#d4d4d4">
                                    <canvas id="myChart" style="z-index: 200; width: 100%; height: 100%;"></canvas>
                                </font>
                            </div>
                        </div>
                        <div class="card card0 no-border" style="margin-left: 3%; margin-top: 5px; background-color: rgba(0, 0, 0, 0.3);">
                            <div style="margin-left: 2%;">
                                <h4>
                                    <span style="transform: rotate(180deg); color: #d4d4d4;" class="glyphicon glyphicon-tag" aria-hidden="true"></span><font color="#d4d4d4">네트워크/보안 다운 장비</font>
                                </h4>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Table ID="TBLLIST2" runat="server" CssClass="table no-border" Width="100%"></asp:Table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <style>
                    #line {
                        position: absolute;
                        width: 2px;
                        margin-top: -1px;
                        background-color: red;
                    }
                </style>
                <script>
                    function adjustLine(from, to, line) {

                        var fT = from.offsetTop + from.offsetHeight / 2;
                        var tT = to.offsetTop + to.offsetHeight / 2;
                        var fL = from.offsetLeft + from.offsetWidth / 2;
                        var tL = to.offsetLeft + to.offsetWidth / 2;

                        var CA = Math.abs(tT - fT);
                        var CO = Math.abs(tL - fL);
                        var H = Math.sqrt(CA * CA + CO * CO);
                        var ANG = 180 / Math.PI * Math.acos(CA / H);

                        if (tT > fT) {
                            var top = (tT - fT) / 2 + fT;
                        } else {
                            var top = (fT - tT) / 2 + tT;
                        }
                        if (tL > fL) {
                            var left = (tL - fL) / 2 + fL;
                        } else {
                            var left = (fL - tL) / 2 + tL;
                        }

                        if ((fT < tT && fL < tL) || (tT < fT && tL < fL) || (fT > tT && fL > tL) || (tT > fT && tL > fL)) {
                            ANG *= -1;
                        }
                        top -= H / 2;

                        line.style["-webkit-transform"] = 'rotate(' + ANG + 'deg)';
                        line.style["-moz-transform"] = 'rotate(' + ANG + 'deg)';
                        line.style["-ms-transform"] = 'rotate(' + ANG + 'deg)';
                        line.style["-o-transform"] = 'rotate(' + ANG + 'deg)';
                        line.style["-transform"] = 'rotate(' + ANG + 'deg)';
                        line.style.top = top + 'px';
                        line.style.left = left + 'px';
                        line.style.height = H + 'px';
                    }
                    adjustLine(
                        document.getElementById('div100'),
                        document.getElementById('div101'),
                        document.getElementById('line')
                    );

                </script>



                <div id="pan" style="width: 58%; display: inline-block; height: 820px; float: left; margin-left: 0.5%; background-color: rgba(0, 0, 0, 0.3);">
                    <%-- <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>--%>
                    <div id="div3" runat="server" style="float: left; width: 100%; margin-top: auto;">
                        <div style="float: right;">
                            <font color="white" size="1">
                                <input type="button" name="전체초기화" value="전체초기화" onclick="RESET()" style="background: transparent; border: none;" /></font>

                            <font color="white" size="1">
                                <input type="button" name="라인초기화" value="라인초기화" onclick="LINE()" style="background: transparent; border: none;" /></font>

                        </div>
                        <div class="box-header" style="margin-left: 0%; width: 80%;">
                            <i style="color: #d4d4d4;" class="glyphicon glyphicon-tasks"></i>
                            <h4 class="box-title"><font color="#d4d4d4">메인 장비</font></h4>
                        </div>
                        <div id="div33">
                        </div>
                    </div>
                    <%--                            </ContentTemplate>
                        </asp:UpdatePanel>--%>
                </div>

                <div style="padding-left: 0.5%; float: left; width: 20.5%; margin-bottom: 40px; vertical-align: middle;">
                    <div class="card card0 no-border" style="height: 260px; background-color: rgba(0, 0, 0, 0.3);">
                        <div style="margin-left: 2%;">
                            <h4>
                                <span style="color: #d4d4d4;" class="glyphicon glyphicon-tasks" aria-hidden="true"></span><font color="#d4d4d4">서버 자원 List </font>
                            </h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:Table ID="TBLLIST3" runat="server" CssClass="table no-border " Width="100%"></asp:Table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="card card0 no-border" style="margin-top: 5px; background-color: rgba(0, 0, 0, 0.3);">
                        <div style="margin-left: 2%">
                            <h4>
                                <span style="color: #d4d4d4;" class="glyphicon glyphicon-stats" aria-hidden="true"></span><font color="#d4d4d4">서버 차트 </font>
                            </h4>
                        </div>

                        <div id="div2" style="width: 100%; height: 210px; margin-top: 0px;">
                            <canvas id="myChart2" style="width: 100%;"></canvas>
                        </div>
                    </div>

                    <div class="card card0 no-border" style="margin-top: 5px; background-color: rgba(0, 0, 0, 0.3);">
                        <div style="margin-left: 2%;">
                            <h4>
                                <span style="transform: rotate(180deg); color: #d4d4d4;" class="glyphicon glyphicon-tag" aria-hidden="true"></span><font color="#d4d4d4">서버 다운 장비</font>
                            </h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:Table ID="TBLLIST" runat="server" CssClass="table no-border " Width="100%"></asp:Table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
                <div style="width: 100%;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div id="div4" class="card card0 no-border" runat="server" style="float: left; width: 98.3%; margin-left: 0.7%; z-index: 99; margin-top: 2%; background-color: rgba(0, 0, 0, 0.3);">
                                <div style="margin-left: 0.5%;">
                                    <h4>
                                        <span style="transform: rotate(180deg); color: #d4d4d4;" class="glyphicon glyphicon-tag" aria-hidden="true"></span><font color="#d4d4d4">이벤트 로그</font>
                                    </h4>
                                    <asp:Table ID="TBLLIST1" runat="server" CssClass="table no-border" Width="100%"></asp:Table>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </section>



        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
        <asp:Button ID="Button2" runat="server" Text="Button" Visible="false" OnClick="Button2_Click" />
        <asp:Button ID="Button4" runat="server" Text="Button" Visible="false" OnClick="Button4_Click" />
        <asp:Button ID="Button5" runat="server" Text="Button" Visible="false" OnClick="Button5_Click" />
        <asp:Button ID="Button3" runat="server" Text="Button" Visible="false" OnClick="Button3_Click" />
        <asp:Button ID="Button1" Visible="false" runat="server" Text="Button" OnClick="Button1_Click" />

        <asp:Label ID="Label3" runat="server"></asp:Label>
        <asp:HiddenField ID="TextBox5" runat="server" />

    </form>
    <script src="Scripts/jquery.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>
    <script src="Scripts/adminlte.min.js"></script>


    <script src="Scripts/leader-line.min.js"></script>



</body>
</html>
