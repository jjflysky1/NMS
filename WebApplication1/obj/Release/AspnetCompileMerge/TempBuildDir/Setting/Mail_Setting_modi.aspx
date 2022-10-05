<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mail_Setting_modi.aspx.cs" Inherits="WebApplication1.Mail_Setting_modi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <script src="/Content/JavaScript.js"></script>
    <script src="/Content/JavaScript.js"></script>
    
    <%--<link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js" integrity="sha384-b/U6ypiBEHpOf/4+1nzFpr53nxSS+GLCkfwBdFNTxtclqqenISfwAzpKaMNFNmj4" crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta/js/bootstrap.min.js" integrity="sha384-h0AbiXch4ZDo7tp9hKZ4TsHbi047NrKGLO3SEJAg45jXxnGIfYzk4Si90RDIqNm1" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>--%>

</head>
<body>
    <form id="form1" runat="server">

    <div class="modal-header">
                <a href="#" data-dismiss="modal" class="class pull-right"><span class="glyphicon glyphicon-remove"></span></a>
                <h3 class="modal-title">메일설정 수정</h3>
            </div>
            <div class="modal-body">
                <div style="width:500px; margin-left:auto; margin-right:auto; " >
  
						<div class="form-group">
							<label for="email" class="cols-sm-2 control-label">연락처</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="startip" id="phone"  placeholder="Enter your Phone"/>
								</div>
							</div>
						</div>

                    <div class="form-group">
							<label for="email" class="cols-sm-2 control-label">보내는 사람</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="mail_sender" id="mail_sender"  placeholder="Enter your Email"/>
								</div>
							</div>
						</div>

						<div class="form-group">
							<label for="username" class="cols-sm-2 control-label">CPU 기준(%)</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-users fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="endip" id="cpulimit"  placeholder="Enter your Username"/>
								</div>
							</div>
						</div>

                    <div class="form-group">
							<label for="username" class="cols-sm-2 control-label">메모리 기준(%)</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-users fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="endip" id="memorylimit"  placeholder="Enter your Username"/>
								</div>
							</div>
						</div>

                    <div class="form-group">
							<label for="username" class="cols-sm-2 control-label">트래픽 기준(MB/S)</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-users fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="endip" id="trafficlimit"  placeholder="Enter your Username"/>
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

        <asp:HiddenField ID="MAILSENDER" runat="server" />
                <asp:HiddenField ID="PHONEHF" runat="server" />
                <asp:HiddenField ID="CPUHF" runat="server" />
                <asp:HiddenField ID="MEMORYHF" runat="server" />
                 <asp:HiddenField ID="TRAFFICHF" runat="server" />
           </div>
        
         
     

         <asp:Label ID="Label3" runat="server" ></asp:Label>
        <asp:HiddenField ID="TextBox5" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
    </form>
</body>
</html>
