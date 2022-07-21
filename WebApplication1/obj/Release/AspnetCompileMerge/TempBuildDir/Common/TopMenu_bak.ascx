<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenu_bak.ascx.cs" Inherits="WebApplication1.Common.TopMenu_bak" %>

                <nav class="navbar navbar-default navbar-static-top" role="navigation">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar-collapse-1">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand" href="/main.aspx">LNM</a>
                    </div>

                    <div class="collapse navbar-collapse" id="navbar-collapse-1">
                        <ul class="nav navbar-nav">
                            <li ><a href="/Service/Service.aspx">
                                장비 리스트</a></li>
                           <%-- <li class="dropdown">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">서비스 자원 관리 <b class="caret"></b></a>
                                <ul class="dropdown-menu" >
                                    <li ><a href="/Service/Service.aspx"> 장비 리스트</a></li>
                                    <li class="divider"></li>
                                    <li><a href="/Application/App.aspx"> 어플리케이션 관리</a></li>
                                </ul>
                            </li>--%>
                            <li class="dropdown">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">로그 리스트 <b class="caret"></b></a>
                                <ul class="dropdown-menu" >
                                    <li ><a href="/Log/Service_Log.aspx"> 서비스 로그</a></li>
                                    <li class="divider"></li>
                                    <li><a href="/Log/System_Log.aspx"> 시스템 로그</a></li>
                                     <li class="divider"></li>
                                    <li><a href="/Log/Secure_Log_main.aspx"> 장비 로그</a></li>
                                </ul>
                            </li>
                            <li><a href="/User/user.aspx">사용자 정보</a></li>
                            <li class="dropdown">
                                <a href="/Setting/network_Setting.aspx" class="dropdown-toggle" data-toggle="dropdown">설정 <b class="caret"></b></a>
                                <ul class="dropdown-menu" >
                                    <li ><a href="/Setting/Network_Setting.aspx">네트워크 설정</a></li>
                                    <li class="divider"></li>
                                    <li><a href="/Setting/Mail_Setting.aspx">메일 설정</a></li>
                                    <li class="divider"></li>
                                    <li><a href="/Setting/oid_list.aspx">OID 설정</a></li>
                                </ul>
                            </li>
                            <li><a href="http://www.sungsimit.co.kr">고객 기술지원</a></li>
                        </ul>


                         <div class="nav navbar-nav navbar-right" style=" margin-right: 30px; margin-top:15px;">
                             <span class="glyphicon glyphicon-user"></span><asp:Label ID="Label111" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                        <a href="/Default.aspx">Logout</a>
                    </div>
                    </div>
                    <!-- /.navbar-collapse -->
                </nav>
