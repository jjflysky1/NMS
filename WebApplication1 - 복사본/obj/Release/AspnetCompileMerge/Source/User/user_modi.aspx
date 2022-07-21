<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_modi.aspx.cs" Inherits="WebApplication1.user_modi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <script src="/Content/JavaScript.js"></script>
    <script src="/Content/JavaScript.js"></script>
    
 <%--   <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js" integrity="sha384-b/U6ypiBEHpOf/4+1nzFpr53nxSS+GLCkfwBdFNTxtclqqenISfwAzpKaMNFNmj4" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/js/bootstrap.min.js" integrity="sha384-h0AbiXch4ZDo7tp9hKZ4TsHbi047NrKGLO3SEJAg45jXxnGIfYzk4Si90RDIqNm1" crossorigin="anonymous"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
     <div>
            <div id="navbar">    
  <nav class="navbar navbar-default navbar-static-top" role="navigation">
       <div style="text-align:right; margin-right:20px">
                    <a href="Default.aspx">Logout</a>
                </div>
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar-collapse-1">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
              <a class="navbar-brand" href="main.aspx">Manage</a>
            </div>
            
            <div class="collapse navbar-collapse" id="navbar-collapse-1">
                <ul class="nav navbar-nav">
                    <li ><a href="webform1.aspx">1. 서비스 복구 리스트</a></li>
                    <li ><a href="webform2.aspx">2. 로그 리스트</a></li>
                    <li class="active"><a href="user.aspx">3. 사용자 정보</a></li>
                    <li><a href="http://www.sungsimit.co.kr">4. 고객 기술지원</a></li>
                </ul>
               
            </div><!-- /.navbar-collapse -->

        </nav>
</div>
            <div class="container-fluid" >
            <div class="alert alert-warning" role="alert"><span class="glyphicon glyphicon-signal" aria-hidden="true"></span> User Info</div>
                <div style="text-align:center; margin-right:10px">
    
           </div>
             
                <div style="width:500px; margin-left:auto; margin-right:auto; " >
                <div class="form-group" >
							<label for="name" class="cols-sm-2 control-label">User ID</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="name" id="id"  placeholder="Enter your Name"/>
                                    
								</div>
							</div>
						</div>

						<div class="form-group">
							<label for="email" class="cols-sm-2 control-label">User Name</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="name" id="name"  placeholder="Enter your Email"/>
								</div>
							</div>
						</div>

						<div class="form-group">
							<label for="username" class="cols-sm-2 control-label">User Duty</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-users fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="duty" id="duty"  placeholder="Enter your Username"/>
								</div>
							</div>
						</div>

						<div class="form-group">
							<label for="password" class="cols-sm-2 control-label">Password</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
									<input type="password"  runat="server" class="form-control" name="password" id="password"  placeholder="Enter your Password"/>
								</div>
							</div>
						</div>

						<div class="form-group">
							<label for="confirm" class="cols-sm-2 control-label">Confirm Password</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
									<input type="password" runat="server" class="form-control" name="confirm" id="confirm"  placeholder="Confirm your Password"/>
								</div>
							</div>
						</div>

						<div class="form-group" style="float:left">
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning  login-button" Text="Back" OnClick="Button1_Click1" />
                           
						</div>
                        <div style="text-align:right;">
                         <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary  login-button" Text="Register" OnClick="Button1_Click" />
                        </div>


                </div>

                <asp:HiddenField ID="IDHF" runat="server" />
                <asp:HiddenField ID="NAMEHF" runat="server" />
                <asp:HiddenField ID="DUTYHF" runat="server" />
                <asp:HiddenField ID="PASSWORDHF" runat="server" />
                <asp:HiddenField ID="CONFIRMHF" runat="server" />
           </div>

         
        </div>

         <asp:Label ID="Label3" runat="server" ></asp:Label>
        <asp:HiddenField ID="TextBox5" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
    </form>
</body>
</html>
