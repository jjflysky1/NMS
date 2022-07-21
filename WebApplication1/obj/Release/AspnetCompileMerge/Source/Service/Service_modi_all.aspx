<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Service_modi_all.aspx.cs" Inherits="WebApplication1.Service_modi_all" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="/Scripts/jquery-3.2.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script src="/Content/JavaScript.js"></script>
    <script src="/Content/JavaScript.js"></script>
</head>
<body>
    <form id="form1" runat="server">
     <div class="modal-header">
                <a href="#" data-dismiss="modal" class="class pull-right"><span class="glyphicon glyphicon-remove"></span></a>
                <h3 class="modal-title">일괄변경</h3>
            </div>
            <div class="modal-body">
                <div style="width:500px; margin-left:auto; margin-right:auto; " >
               
						<div class="form-group">
							<label for="email" class="cols-sm-2 control-label">서버 아이피</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="name" id="serverip"  placeholder="Enter your Email"/>
								</div>
							</div>
						</div>

						<div class="form-group">
							<label for="username" class="cols-sm-2 control-label">서버 아이디</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-users fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="duty" id="serverid"  placeholder="Enter your Username"/>
								</div>
							</div>
						</div>

						<div class="form-group">
							<label for="password" class="cols-sm-2 control-label">서버 패스워드</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-lock fa-lg" aria-hidden="true"></i></span>
									<input type="text"  runat="server" class="form-control" name="password" id="serverpwd"  placeholder="Enter your Password"/>
								</div>
							</div>
						</div>

					
						<div class="form-group" style="float:left">
                            <%--<asp:Button ID="Button1" runat="server" CssClass="btn btn-warning  login-button" Text="Back" OnClick="Button1_Click1" />--%>
                           
						</div>
                        <div style="text-align:right;">
                         <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary  login-button" Text="수정" OnClick="Button1_Click" />
                        </div>


                </div>

                <asp:HiddenField ID="NAMEHF" runat="server" />
                <asp:HiddenField ID="SERVERIPHF" runat="server" />
                <asp:HiddenField ID="SERVERIDHF" runat="server" />
                <asp:HiddenField ID="SERVERPWDHF" runat="server" />
              
           </div>

         
      
         <asp:Label ID="Label3" runat="server" ></asp:Label>
        <asp:HiddenField ID="TextBox5" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
    </form>
</body>
</html>
