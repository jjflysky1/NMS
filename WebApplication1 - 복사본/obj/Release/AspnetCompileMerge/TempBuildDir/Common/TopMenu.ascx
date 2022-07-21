<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenu.ascx.cs" Inherits="WebApplication1.Common.WebUserControl1" %>

<style>
    td, th {
    vertical-align: middle !important;
    }
.left, .right {
        float:left;
        height:100vh;
    }
    
.left {
        background: #337ab7;
        display: inline-block;
        white-space: nowrap;
        width: 100%;
        transition: height 1s ;
        height:50px;
    }

.right {
        background: #ecf0f5;
        width: 100%;
        transition: height 1s;
        margin-top:20px;
        height:100%;
     background-color:transparent;
      
    }   
.left:hover {
        width: 100%;
        height:350px;
    
    }    
    
.item:hover {
        /*background-color:#fff;*/
        }
        
.left .glyphicon {
        margin:15px;
        width:20px;
        color:#fff;
    }
    
.right .glyphicon {
        color:#a9a9a9;
    }
span.glyphicon.glyphicon-refresh{
    font-size:17px;
    vertical-align: middle !important;
    }
    
.item {
        height:50px;
        overflow:hidden;
        color:#fff;
        visibility:hidden;
    }
.item1 {
        height:50px;
        overflow:hidden;
        color:#fff;
      
    }
.title {
        background-color:#eee;
        border-style:solid;
        border-color:#ccc;
        border-width:1px;
        box-sizing: border-box;
    }
.search:hover {
        border-color:#4aa9fb;
        border-width:1px;
    }
.search {
    padding:3px 8px 3px !important;
    }
input[type=search] {
    padding: 10px 0px 10px;
	border: 0px solid #fff;
	background: #eee;
	-webkit-appearance: none;
    width:90%;
    float:none;
}
input[type=search]:focus {
    outline:none;
    }
.type{
    height: 47px;
    }
.date{
    background-color:#f7f7f7
    }
.docdate {
    vertical-align:bottom !important;
    }
.distr {
    margin: 0 0 5px;
    font-size: 12px;
    }
.ndoc {
    margin: 0 0 5px;
    }
.storage {
    margin: 0;
    color: #aaa !important;
    font-size: 12px;
    }
        
    
    
   

    
</style>
<script>
    $(function () {
        $('.left').mouseenter(function () {
            $('.item').stop().css({ visibility: 'inherit' }).fadeIn(1000)
          
        })
        $('.left').mouseleave(function () {
            $('.item').stop().css({ visibility: 'hidden' }).fadeOut(100)

        })
    })
</script>
<div class="left">
<div class="item1" >
        <div style="margin-top:-5px">
               <span style="font-size:25px"  class="glyphicon glyphicon-menu-hamburger"></span>           
        </div>
  
        <div style="text-align:center; margin-top:-35px; vertical-align:middle;">
            <a href="../main.aspx"><font color="white" size="4">SSIM Watcher</a></font>
        </div>
        <div style="text-align:right; margin-top:-40px; margin-right:50px; vertical-align:middle;">
            <span class="glyphicon glyphicon-user"></span><font color="white" ><asp:Label ID="Label111" runat="server" Text="Label"></asp:Label></font>&nbsp;&nbsp;&nbsp;&nbsp;
                        <a href="/Default.aspx"><font color="white" >Logout</a></font>
        </div>
    </div>
    <div class="item" >
        <span class="glyphicon glyphicon-th"></span>
        <a href="/main.aspx"><font color="white">Dash Board</font></a>
    </div>

    <div class="item"  >
    <span class="glyphicon glyphicon-th-list"></span>
    <a href="/Service/Service.aspx"><font color="white">장비 리스트</font></a>   
    </div>
    
    <div class="item" >
    <span class="glyphicon glyphicon-log-out"></span>
    <a href="/Log/Service_Log.aspx"><font color="white">로그 리스트</font></a>
     <span class=" glyphicon glyphicon-menu-right"></span> <a href="/Log/Service_Log.aspx"><font color="white">서비스 로그</font></a>
     <span class=" glyphicon glyphicon-menu-right"></span> <a href="/Log/system_log.aspx"><font color="white">시스템 로그</font></a>
     <span class=" glyphicon glyphicon-menu-right"></span> <a href="/Log/Secure_log_main.aspx"><font color="white">장비 로그</font></a>
        
    </div>

    <div class="item" >
    <span class="glyphicon glyphicon-log-in"></span>
    <a href="/User/user.aspx"><font color="white">사용자 정보</font></a>
    </div>

    <div class="item"  >
    <span class="glyphicon glyphicon-random"></span>
    <a href="/Setting/Network_Setting.aspx"><font color="white">설정</font></a>
        <span class=" glyphicon glyphicon-menu-right"></span> <a href="/Setting/Network_Setting.aspx"><font color="white">네트워크 설정</font></a>
     <span class=" glyphicon glyphicon-menu-right"></span> <a href="/Setting/Mail_Setting.aspx"><font color="white">메일 설정</font></a>
     <span class=" glyphicon glyphicon-menu-right"></span> <a href="/Setting/oid_list.aspx"><font color="white">OID 설정</font></a>
    </div>

    <div class="item">
    <span class="glyphicon glyphicon-home"></span>
    <a href="http://www.sungsimit.co.kr"><font color="white">고객지원 센터</font></a>
    </div>
</div>

<%--<div class="item">
<span class="glyphicon glyphicon-th-large"></span>
</div>
<div class="item active">
<span class="glyphicon glyphicon-th-list"></span>
    <a href="/Service/Service.aspx"><font color="white">장비 리스트</font></a></div>
<div class="item">
<span class="glyphicon glyphicon-log-out"></span>
    <a href="/Log/Service_Log.aspx"><font color="white">로그 리스트</font></a></div>
<div class="item">
<span class="glyphicon glyphicon-log-in"></span>
    <a href="/User/user.aspx"><font color="white">사용자 정보</font></a></div> 
<div class="item">
<span class="glyphicon glyphicon-random"></span>
    <a href="/Setting/Network_Setting.aspx"><font color="white">설정</font></a></div>
<div class="item">
<span class="glyphicon glyphicon-remove"></span>
    <a href="http://www.sungsimit.co.kr"><font color="white">고객지원 센터</font></a></div>    --%>
