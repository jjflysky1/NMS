<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Service_modi1.aspx.cs" Inherits="WebApplication1.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  
    <script src="/Content/JavaScript.js"></script>
    <script src="/Content/JavaScript.js"></script>
    

    <title></title>
</head>
<body>
    <form id="form1" runat="server">
            <div class="modal-header">
                <a href="#" data-dismiss="modal" class="class pull-right"><span class="glyphicon glyphicon-remove"></span></a>
                <h3 class="modal-title">서비스 수정</h3>
            </div>
            <div class="modal-body">
                <div style="width:500px; margin-left:auto; margin-right:auto; " >
					    <div class="form-group" >
							<label for="name" class="cols-sm-2 control-label">밴더 이름 (예:SECUI, CISCO, AXGATE)</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="name" id="vandor"  placeholder="Enter your Name"/>
                                    
								</div>
							</div>
						</div>

                <div class="form-group" >
							<label for="name" class="cols-sm-2 control-label">서비스 이름</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="name" id="name"  placeholder="Enter your Name"/>
                                    
								</div>
							</div>
						</div>

                     <div class="form-group" >
							<label for="name" class="cols-sm-2 control-label">SSH PORT (설정안할시 기본 22)</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="SSHPORT" id="SSHPORT"  placeholder="Enter your Port"/>
                                    
								</div>
							</div>
						</div>

                     <div class="form-group" >
							<label for="name" class="cols-sm-2 control-label">Community (설정안할시 public)</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="Community" id="Community"  placeholder="Enter your Community"/>
                                    
								</div>
							</div>
						</div>

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
									<input type="text" runat="server" class="form-control" name="duty" id="serverid"  placeholder="Enter your Username" autocomplete="off"/>
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
                           <%-- <asp:Button ID="Button6" runat="server" CssClass="btn btn-warning  login-button" Text="Back" OnClick="Button6_Click" />--%>
                           
						</div>
                        <div style="text-align:right;">
                         <asp:Button ID="Button8" runat="server" CssClass="btn btn-primary  login-button" Text="수정" OnClick="Button8_Click" />
                        </div>


                </div>
            </div>
  

<asp:HiddenField ID="HiddenField1" runat="server" />
		<asp:HiddenField ID="VANDORHF" runat="server" />
        <asp:HiddenField ID="NAMEHF" runat="server" />
        <asp:HiddenField ID="SERVERIPHF" runat="server" />
        <asp:HiddenField ID="SERVERIDHF" runat="server" />
        <asp:HiddenField ID="SERVERPWDHF" runat="server" />
        <asp:HiddenField ID="SSHPORTHF" runat="server" />
        <asp:HiddenField ID="COMMUNITYHF" runat="server" />
        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
