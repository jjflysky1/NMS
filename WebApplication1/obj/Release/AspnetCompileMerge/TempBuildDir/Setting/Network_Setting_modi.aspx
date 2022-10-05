<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Network_Setting_modi.aspx.cs" Inherits="WebApplication1.Network_Setting_modi" %>

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
                <h3 class="modal-title">네트워크 설정 수정</h3>
            </div>
            <div class="modal-body">
                <div style="width:500px; margin-left:auto; margin-right:auto; " >
                <div class="form-group" >
							<label for="name" class="cols-sm-2 control-label">네트워크 이름</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="name" id="name"  placeholder="Enter your Name"/>
                                    
								</div>
							</div>
						</div>

						<div class="form-group">
							<label for="email" class="cols-sm-2 control-label">시작 아이피</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-envelope fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="startip" id="startip"  placeholder="Enter your Email"/>
								</div>
							</div>
						</div>

						<div class="form-group">
							<label for="username" class="cols-sm-2 control-label">종료 아이피</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-users fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="endip" id="endip"  placeholder="Enter your Username"/>
								</div>
							</div>
						</div>

						<div class="form-group" style="float:left">
                            <%--<asp:Button ID="Button1" runat="server" CssClass="btn btn-warning  login-button" Text="Back" OnClick="Button1_Click1" />--%>
                           
						</div>
                        <div style="text-align:right;">
                         <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary  login-button" Text="저장" OnClick="Button1_Click" />
                        </div>


                </div>

               
                <asp:HiddenField ID="NAMEHF" runat="server" />
                <asp:HiddenField ID="STARTIPHF" runat="server" />
                <asp:HiddenField ID="ENDIPHF" runat="server" />
      
           </div>
        
         
     

         <asp:Label ID="Label3" runat="server" ></asp:Label>
        <asp:HiddenField ID="TextBox5" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
    </form>
</body>
</html>
