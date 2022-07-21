<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
      <link rel='stylesheet' href="Content/bootstrap.min.css" />
      <%--<link rel="stylesheet" href="Content/login.css">--%>
     <link rel="stylesheet" href="Content/AdminLTE.min.css" />
    <link rel="stylesheet" href="Content/font-awesome.min.css" />
    <link rel="stylesheet" href="Content/ionicons.min.css" />
    <link rel="stylesheet" href="Content/blue.css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">
    <title></title>
</head>
   
    <body class="hold-transition login-page">
<div class="login-box">
  <div class="login-logo">
    <a href="default.aspx"><b>SSIM</b>  WATCHER</a>
  </div>
  <!-- /.login-logo -->
  <div class="login-box-body">
    <p class="login-box-msg">로그인하여 주십시오</p>

    <form  runat="server" id="form">
      <div class="form-group has-feedback">
          <input type="text" runat="server" class="form-control" name="name"  id ="name" placeholder="ID" required="" autofocus="" />
        <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
      </div>
      <div class="form-group has-feedback">
          <input type="password" runat="server" class="form-control" name="password" id="password" placeholder="Password" required=""/>     
        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div>
      <div class="row">
        <div class="col-xs-8">
          <div class="checkbox icheck">
            <label>
              
            </label>
          </div>
        </div>
        <!-- /.col -->
        <div class="col-xs-4">
            <asp:Button  id="enter" runat="server" Text="Login" OnClick="LOGIN" CssClass="btn btn-primary btn-block btn-flat" />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>
        <!-- /.col -->
      </div>
    </form>
  </div>
  <!-- /.login-box-body -->
</div>
<!-- /.login-box -->

<!-- jQuery 3 -->
<script src="Scripts/jquery.min.js"></script>
    
<!-- Bootstrap 3.3.7 -->
<script src="Scripts/bootstrap.min.js"></script>
<!-- iCheck -->


</body>


<%--<body>
    
 <div class="wrapper">
    <form class="form-signin" runat="server"  id="form">       
      <h2 class="form-signin-heading">Please login</h2>
      <input type="text" runat="server" class="form-control" name="name"  id ="name" placeholder="Email Address" required="" autofocus="" />
      <input type="password" runat="server" class="form-control" name="password" id="password" placeholder="Password" required=""/>      
      <label class="checkbox">
        <input type="checkbox" value="remember-me" id="rememberMe" name="rememberMe"> Remember me
      </label>
      
        <asp:Button  id="enter" runat="server" Text="Login" OnClick="LOGIN" CssClass="btn btn-lg btn-primary btn-block" />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </form>
  </div>
   
</body>--%>

   
</html>
