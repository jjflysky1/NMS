<%@ Page MaintainScrollPositionOnPostback="true" EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="main8.aspx.cs" Inherits="WebApplication1.main8" %>

<%@ Register Src="~/Common/leftmenu.ascx" TagName="uc_menu" TagPrefix="uc1" %>
<%@ Register Src="~/Common/bottom.ascx" TagName="uc_bottom" TagPrefix="uc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel='stylesheet' href="Content/bootstrap.min.css" />
    <link rel="stylesheet" href="Content/AdminLTE.min.css" />
    <link rel="stylesheet" href="Content/font-awesome.min.css" />


    <link rel="stylesheet" href="Content/_all-skins.min.css" />
    <link rel="stylesheet" href="Content/ionicons.min.css" />
    <link rel="stylesheet" href="Content/bootstrap.min2.css" />
    <%--<link rel="stylesheet" href="Content/style.css" />--%>

    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Content/JavaScript.js"></script>
    <script src="Scripts/AdminLTE.css"></script>


    <script src="Scripts/highcharts.js"></script>
    <script src="Scripts/highcharts-3d.js"></script>
    <script src="Scripts/exporting.js"></script>
    <script src="Scripts/export-data.js"></script>
    <script src="Scripts/jschart.js"></script>
    <link href="Scripts/Allcss.css" rel="stylesheet" />

    <script src="Scripts/jquery-3.2.1.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>

    <script src="Scripts/dragdrop.js"></script>

    <script src="Scripts/dash_chart.js"></script>
    <%--<script src="https://d3js.org/d3.v4.min.js"></script>--%>
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

                            <div style="margin-left: 30px; margin-bottom: 0px; margin-top: 0px; width: 97.5%; height: 200px;">
                                <div style="margin-left: 1%">
                                    <h4>
                                        <span style="color: #d4d4d4;" class="glyphicon glyphicon-hdd" aria-hidden="true"></span><<font color="#d4d4d4"> 장비 수량</font>
                                    </h4>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:Table ID="TBLLIST0" BackColor="Transparent" runat="server" CssClass="table no-border" Width="100%"></asp:Table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                </table>

                <div style="width: 100%; height: 100%; margin-top: -20px;">
                    <div style="float: left; width: 20%; margin-left: 0px; height: 100%; vertical-align: middle;">
                        <div class="card card0 border" style="margin-left: 30px; height: 260px; margin-left: 3%; background-color: rgba(0, 0, 0, 0.3);">
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
                        <div class="card card0 border" style="margin-left: 3%; margin-top: 5px; background-color: rgba(0, 0, 0, 0.3);">
                            <div style="margin-left: 2%">
                                <h4>
                                    <span style="color: #d4d4d4;" class="glyphicon glyphicon-stats" aria-hidden="true"></span><font color="#d4d4d4">네트워크/보안 차트</font>
                                </h4>
                            </div>
                            <div id="div1" style="width: 100%; height: 210px; margin-top: 0px;">
                                <canvas id="myChart" style="z-index: 200; width: 100%; height: 100%; color: #d4d4d4;"></canvas>
                            </div>
                        </div>
                        <div class="card card0 border" style="margin-left: 3%; margin-top: 5px; background-color: rgba(0, 0, 0, 0.3);">
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


                <div id="pan" style="width: 59%; height: 820px; position: absolute; margin-left: 20.8%; padding-left: 0px; background-color: rgba(0, 0, 0, 0.3); ">
                    <%-- <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>--%>
                    <div id="div3" runat="server" style="float: left; width: 100%; margin-top: auto; ">
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

                <div style="padding-left: 30px; float: right; width: 20%; height: auto; margin-right: 1%; margin-bottom: 40px; vertical-align: middle;">
                    <div class="card card0 border" style="height: 260px; background-color: rgba(0, 0, 0, 0.3);">
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

                    <div class="card card0 border" style="margin-top: 5px; background-color: rgba(0, 0, 0, 0.3);">
                        <div style="margin-left: 2%">
                            <h4>
                                <span style="color: #d4d4d4;" class="glyphicon glyphicon-stats" aria-hidden="true"></span><font color="#d4d4d4">서버 차트 </font>
                            </h4>
                        </div>

                        <div id="div2" style="width: 100%; height: 210px; margin-top: 0px;">
                            <canvas id="myChart2" style="width: 100%;"></canvas>
                        </div>
                    </div>

                    <div class="card card0 border" style="margin-top: 5px; background-color: rgba(0, 0, 0, 0.3);">
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

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="div4" class="card card0 border" runat="server" style="float: left; width: 98.3%; margin-left: 0.7%; z-index: 99; margin-top: 2%; background-color: rgba(0, 0, 0, 0.3);">
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
        </section>

        <style>
            .grad {
                /*background-image:linear-gradient(356deg, rgba(2,0,36,1) 0%, rgba(9,9,121,1) 55%, rgba(0,212,255,1) 100%)}*/
                /*background: linear-gradient(to right, #0f2027, #203a43, #2c5364);*/
                background: rgb(25,34,55);
            }

            .grad2 {
                /*background: radial-gradient(circle, rgba(40,47,77,1) 0%, rgba(31,55,86,1) 100%);*/
            }

            .card {
                position: relative;
                display: -ms-flexbox;
                display: flex;
                -ms-flex-direction: column;
                flex-direction: column;
                min-width: 0;
                word-wrap: break-word;
                background-color: #fff;
                background-clip: border-box;
                border: 1px solid rgba(0,0,0,.125);
                border-radius: .25rem
            }

                .card > hr {
                    margin-right: 0;
                    margin-left: 0
                }

                .card > .list-group:first-child .list-group-item:first-child {
                    border-top-left-radius: .25rem;
                    border-top-right-radius: .25rem
                }

                .card > .list-group:last-child .list-group-item:last-child {
                    border-bottom-right-radius: .25rem;
                    border-bottom-left-radius: .25rem
                }

            .card-body {
                -ms-flex: 1 1 auto;
                flex: 1 1 auto;
                padding: 1.25rem
            }

            .card-title {
                margin-bottom: .75rem
            }

            .card-subtitle {
                margin-top: -.375rem;
                margin-bottom: 0
            }

            .card-text:last-child {
                margin-bottom: 0
            }

            .card-link:hover {
                text-decoration: none
            }

            .card-link + .card-link {
                margin-left: 1.25rem
            }

            .card-header {
                padding: .75rem 1.25rem;
                margin-bottom: 0;
                background-color: rgba(0,0,0,.03);
                border-bottom: 1px solid rgba(0,0,0,.125)
            }

                .card-header:first-child {
                    border-radius: calc(.25rem - 1px) calc(.25rem - 1px) 0 0
                }

                .card-header + .list-group .list-group-item:first-child {
                    border-top: 0
                }

            .card-footer {
                padding: .75rem 1.25rem;
                background-color: rgba(0,0,0,.03);
                border-top: 1px solid rgba(0,0,0,.125)
            }

                .card-footer:last-child {
                    border-radius: 0 0 calc(.25rem - 1px) calc(.25rem - 1px)
                }

            .card-header-tabs {
                margin-right: -.625rem;
                margin-bottom: -.75rem;
                margin-left: -.625rem;
                border-bottom: 0
            }

            .card-header-pills {
                margin-right: -.625rem;
                margin-left: -.625rem
            }

            .card-img-overlay {
                position: absolute;
                top: 0;
                right: 0;
                bottom: 0;
                left: 0;
                padding: 1.25rem
            }

            .card-img {
                width: 100%;
                border-radius: calc(.25rem - 1px)
            }

            .card-img-top {
                width: 100%;
                border-top-left-radius: calc(.25rem - 1px);
                border-top-right-radius: calc(.25rem - 1px)
            }

            .card-img-bottom {
                width: 100%;
                border-bottom-right-radius: calc(.25rem - 1px);
                border-bottom-left-radius: calc(.25rem - 1px)
            }

            .card-deck {
                display: -ms-flexbox;
                display: flex;
                -ms-flex-direction: column;
                flex-direction: column
            }

                .card-deck .card {
                    margin-bottom: 15px
                }

            @media (min-width:576px) {
                .card-deck {
                    -ms-flex-flow: row wrap;
                    flex-flow: row wrap;
                    margin-right: -15px;
                    margin-left: -15px
                }

                    .card-deck .card {
                        display: -ms-flexbox;
                        display: flex;
                        -ms-flex: 1 0 0%;
                        flex: 1 0 0%;
                        -ms-flex-direction: column;
                        flex-direction: column;
                        margin-right: 15px;
                        margin-bottom: 0;
                        margin-left: 15px
                    }
            }

            .card-group {
                display: -ms-flexbox;
                display: flex;
                -ms-flex-direction: column;
                flex-direction: column
            }

                .card-group > .card {
                    margin-bottom: 15px
                }

            @media (min-width:576px) {
                .card-group {
                    -ms-flex-flow: row wrap;
                    flex-flow: row wrap
                }

                    .card-group > .card {
                        -ms-flex: 1 0 0%;
                        flex: 1 0 0%;
                        margin-bottom: 0
                    }

                        .card-group > .card + .card {
                            margin-left: 0;
                            border-left: 0
                        }

                        .card-group > .card:not(:last-child) {
                            border-top-right-radius: 0;
                            border-bottom-right-radius: 0
                        }

                            .card-group > .card:not(:last-child) .card-header, .card-group > .card:not(:last-child) .card-img-top {
                                border-top-right-radius: 0
                            }

                            .card-group > .card:not(:last-child) .card-footer, .card-group > .card:not(:last-child) .card-img-bottom {
                                border-bottom-right-radius: 0
                            }

                        .card-group > .card:not(:first-child) {
                            border-top-left-radius: 0;
                            border-bottom-left-radius: 0
                        }

                            .card-group > .card:not(:first-child) .card-header, .card-group > .card:not(:first-child) .card-img-top {
                                border-top-left-radius: 0
                            }

                            .card-group > .card:not(:first-child) .card-footer, .card-group > .card:not(:first-child) .card-img-bottom {
                                border-bottom-left-radius: 0
                            }
            }

            .card-columns .card {
                margin-bottom: .75rem
            }

            @media (min-width:576px) {
                .card-columns {
                    -webkit-column-count: 3;
                    -moz-column-count: 3;
                    column-count: 3;
                    -webkit-column-gap: 1.25rem;
                    -moz-column-gap: 1.25rem;
                    column-gap: 1.25rem;
                    orphans: 1;
                    widows: 1
                }

                    .card-columns .card {
                        display: inline-block;
                        width: 100%
                    }
            }

            .accordion > .card {
                overflow: hidden
            }

                .accordion > .card:not(:first-of-type) .card-header:first-child {
                    border-radius: 0
                }

                .accordion > .card:not(:first-of-type):not(:last-of-type) {
                    border-bottom: 0;
                    border-radius: 0
                }

                .accordion > .card:first-of-type {
                    border-bottom: 0;
                    border-bottom-right-radius: 0;
                    border-bottom-left-radius: 0
                }

                .accordion > .card:last-of-type {
                    border-top-left-radius: 0;
                    border-top-right-radius: 0
                }

                .accordion > .card .card-header {
                    margin-bottom: -1px
                }

            .progress {
                height: 15px;
                background: #ebedef;
                border-radius: 32px;
                box-shadow: none
            }

            .progress-bar {
                line-height: 12px;
                background: #38cff4;
                box-shadow: none
            }

            .progress-bar-success {
                background-color: #58bc50
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

            .progress-bar-primary {
                background-color: #38cff4
            }

            body {
                overflow: overlay;
            }

                body::-webkit-scrollbar {
                    display: none;
                }

            ::-webkit-scrollbar-track {
                display: none;
            }

            body {
                /*background-image: url("img/international.jpg");*/
                background-image: url("img/enterprise.jpg");
                /*background-color:#1e1b1e;*/
                /*            background: linear-gradient(to top, #2f323a 0%, #2f323a 100%);*/
                background-repeat: repeat;
                background-size: 100% 100%;
                /* opacity: 0.5;*/
            }

            html {
                height: 100%;
                -ms-overflow-style: none; /* IE and Edge */
                scrollbar-width: none; /* Firefox */
            }
        </style>

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
    <script>


        window.onload = function () {
         
        }
        

     
      
    </script>

</body>
</html>
