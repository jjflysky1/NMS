<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="/Scripts/jquery-3.2.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script src="/Content/JavaScript.js"></script>
    <title></title>
</head>
<body style="background-color:#c9c9c9">
   <%-- <form id="form1" runat="server" >--%>
       



<%--        <div style="text-align:center;">
           <br /><br />
            <b><font size="6"><span class="glyphicon glyphicon-alert" aria-hidden="true"></span>    SSIT NMS</font></b>
        </div>

        <div class="container" style="margin-top:30px">
<div class="col-md-4 col-md-offset-4" style="position:center;">
    <div class="panel panel-default">
  <div class="panel-heading"><h3 class="panel-title"><strong>Sign In </strong></h3></div>
  <div class="panel-body">
   <form role="form">
  <div class="form-group">
    <label for="exampleInputEmail1">Username</label>
      <asp:TextBox class="form-control" placeholder="Username"  ID="TextBox1" runat="server" ></asp:TextBox>
  </div>
  <div class="form-group">
    <label for="exampleInputPassword1">Password </label>
    <asp:TextBox TextMode="Password" class="form-control" placeholder="Password"  ID="TextBox2" runat="server" ></asp:TextBox>
  </div>
       <asp:Button ID="Button2"  class="btn btn-sm btn-default" runat="server" Text="로그인" OnClick="LOGIN" />--%>










          <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <script src="../../assets/js/ie10-viewport-bug-workaround.js"></script>
     
    <%--</form>--%>



    
<!DOCTYPE html><html class=''>
<head>
    <%--<script src='//production-assets.codepen.io/assets/editor/live/console_runner-079c09a0e3b9ff743e39ee2d5637b9216b3545af0de366d4b9aad9dc87e26bfd.js'></script><script src='//production-assets.codepen.io/assets/editor/live/events_runner-73716630c22bbc8cff4bd0f07b135f00a0bdc5d14629260c3ec49e5606f98fdd.js'></script><script src='//production-assets.codepen.io/assets/editor/live/css_live_reload_init-2c0dc5167d60a5af3ee189d570b1835129687ea2a61bee3513dee3a50c115a77.js'></script><meta charset='UTF-8'><meta name="robots" content="noindex"><link rel="shortcut icon" type="image/x-icon" href="//production-assets.codepen.io/assets/favicon/favicon-8ea04875e70c4b0bb41da869e81236e54394d63638a1ef12fa558a4a835f1164.ico" /><link rel="mask-icon" type="" href="//production-assets.codepen.io/assets/favicon/logo-pin-f2d2b6d2c61838f7e76325261b7195c27224080bc099486ddd6dccb469b8e8e6.svg" color="#111" /><link rel="canonical" href="https://codepen.io/Koziuk-S/pen/jbJydJ?limit=all&page=85&q=form" />--%>
<link href='https://fonts.googleapis.com/css?family=Open+Sans:400,300,600' rel='stylesheet' type='text/css' />

<style class="cp-pen-styles">* {
    box-sizing: border-box;
    font-family: 'Open Sans', sans-serif;
}
.body {
    background: #dde3e8;
}
.wrapper, 
.container {
    height: 525px;
    width: 337px;
}
.wrapper {
    margin: auto;
    position: absolute;
    top: -10px; right: 0; bottom: 0; left: 0;
}
.container {
    position: relative;
}

.container .border-triangle  {
    position: absolute;
    z-index: 10;
    border-top: 257px solid transparent;
    border-left: 337px solid rgba(255,255,255,0.5);
    border-bottom: 268px solid transparent;  
}
.container .content-triangle {
    position: absolute;
    left: 12px;
    top: 22px;
    border-top: 235px solid transparent;
    border-left: 307px solid #fff;
    border-bottom: 245px solid transparent;
    z-index: 15;
}
.container .content-triangle:before,
.container .content-triangle:after,
.container form .title:after {
    content: '';
    position: absolute;
    width: 10px;
    height: 15px;
    border: 1px solid #71d2ec;
 
}
.container .content-triangle:before {
    border-bottom: none;
    border-right: none;
    transform: matrix(0.85,0.64,-0,0.7,-295,-203);
}
.container .content-triangle:after {
    border-top: none;
    border-right: none;
    transform: matrix(0.85,-0.7,-0,0.7,-294,194);
}
.container .enter-triangle-one {
    position: absolute;
    border-top: 90px solid transparent;
    border-left: 135px solid #fff;
    border-bottom: 76px solid transparent;
    transform: rotate(51.5deg);
    bottom: 5px;
    left: 153px;
    z-index: 5;
}
.container .enter-triangle-one:before {
    content: '';
    position: absolute;
    border-top: 120px solid transparent;
    border-left: 188px solid #fff;
    border-bottom: 141px solid transparent;
    transform: rotate(4deg);
    bottom: -129px;
    left: -207px;
    -webkit-filter: blur(20px);
    filter: blur(20px);
    z-index: 5;
}
.container .enter-triangle-two {
    position: absolute;
    border-top: 111px solid transparent;
    border-left: 108px solid rgba(255, 255, 255, 0.2);
    border-bottom: 47px solid transparent;
    transform: rotate(51deg);
    bottom: 13px;
    left: 148px;
    z-index: 6;
}
.container form {
    position: absolute;
    z-index: 20;
    top: 166px;
    left: 30px;
}
.container form:before,
.container form:after,
.container form .input-inform:before,
.container form .title:before {
    content: '';
    position: absolute;
    background: rgba(204,204,204,0.13);
    height: 1px;
}
.container form:before {
    width: 315px;
    transform: rotate(37.2deg);
    left: -28px;
    top: -11px;
}
/*삼격형 회색 바 밑*/
.container form:after {
    width: 320px;
    left: -31px;
    bottom: 10px;
    transform: rotate(-39deg);
}
/*중간 글 밑 바*/
.container form .title {
    border-bottom: 1px solid #67e2fb;
    margin: 0px 0px 19px 8px;
    width: 140px;
   
    
}
/*중간 짧은 하늘색 바*/
.container form .title:before {
    width: 26px;
    background: #67e2fb;
    top: 50px;
}
/*오른쪽 삼각형*/
.container form .title:after {
    border-top: none;
    border-left: none;
    width: 8px;
    height: 9px;
    transform: matrix(0.85,-0.7,0.8,0.6,240,25);
}
.container form label {
    display: block;
    color: #000000;
    font-size: 20px;
    font-weight: 300;
    line-height: 24px;
}
.container form .input-inform:before {
    width: 1px;
    height: 396px;
    left: -5px;
    top: -102px;
}
.container form input:focus {
    outline: none;
}
.container form label:last-child {
    font-weight: bold;
    letter-spacing: 1px;
}
.container form input[type="text"],
.container form input[type="password"] {
    border: none;
    border-bottom: 1px solid #f2f2f2;
    display: block;
    width: 160px;
    color: #71d2ec;
}
.container form input[type="text"] {
    padding: 0 0 7px 9px;
}
.container form input[type="password"] {
    padding: 17px 0 9px 9px;
}
.container form #forgot-pas,
.container form .enter,
.container form .enter label,
.container form #enter {
   
}
.container form #forgot-pas {
    color: #c5c5c5;
    background: #fff;
    border: 1px solid #f2f2f2;
    font-size: 7px;
    height: 17px;
    width: 96px;
    top: 6px;
    left: 25px;
}
.container form .enter {
    width: 50px;
    height: 50px;
    top: 50px;
    left: 148px;
}
.container form .enter label:before {
    content: '\1F512';
    color: #000000;
    left: 10px;
    top: 7px;
    position: relative;
}
.container form .enter label:hover:before {
    content: '\1F513';
}
.container form #enter {
    color: #000000;
    border: none;
    font-size: 10px;
    background: transparent;
}</style></head><body>
<!-- https://dribbble.com/shots/2358349-Daily-Shmaily-UI-1-Game-Login -->
     
<div class="wrapper">
    <div class="container">
        <div class="stencil">
            <div class="line">
                <div class="line"></div>
            </div>
        </div>
        <div class="border-triangle"></div>
        <div class="content-triangle"></div>
       
        <form runat="server" id="form">
            <div class="title">
                <label>LOG INTO</label>
                <label>NMS SYSTEM</label>
            </div>
            <div class="input-inform" style="">
                 <input type="text" name="name" runat="server"  id="name" placeholder="LOGIN..." />
                <input type="password" name="password" style="width:140px"  runat="server" id="password" placeholder="PASSWORD..." />
            </div>
            <div class="enter">
                <label for="enter"></label>
                 <asp:Button  id="enter" runat="server" Text="ENTER" OnClick="LOGIN" />
            <%--    <input type="submit" name="submit" runat="server" value="ENTER" id="enter" onserverclick="LOGIN"/>--%>
            </div>
        </form>    
    </div>
</div>
    </body>
</body></html>



</html>
