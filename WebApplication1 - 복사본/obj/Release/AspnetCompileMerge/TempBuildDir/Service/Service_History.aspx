<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Service_History.aspx.cs" Inherits="WebApplication1.Service_History" %>

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
                <a href="javascript:window.location.reload(true)"  class="class pull-right"><span class="glyphicon glyphicon-remove"></span></a>
                <h3 class="modal-title">History</h3>
            </div>
            <div class="modal-body">
                <div style="width:500px; margin-left:auto; margin-right:auto; " >
                <div class="form-group" >
							<label for="name" class="cols-sm-2 control-label">제목</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
									<input type="text" runat="server" class="form-control" name="title" id="title"  placeholder="Enter Title"/>
                                    
								</div>
							</div>
						</div>

                     <div class="form-group" >
							<label for="name" class="cols-sm-2 control-label">내용</label>
							<div class="cols-sm-10">
								<div class="input-group">
									<span class="input-group-addon"><i class="fa fa-user fa" aria-hidden="true"></i></span>
                                    <asp:TextBox ID="body" runat="server" TextMode="MultiLine" class="form-control" Height="120px"></asp:TextBox>
								
                                    
								</div>
							</div>
						</div>

				
                  
                         
                      <div>
                        
                         <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning btn-middle" Text="Delete" OnClick="Button1_Click" />
                      </div>
                        <div style="text-align:right; margin-top:-35px;">
                         <asp:Button ID="Button8" runat="server" CssClass="btn btn-primary  login-button" Text="Register" OnClick="Button8_Click" />
                        </div>


                </div>
            </div>
  

        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="HiddenField2" runat="server" />
        <asp:HiddenField ID="HiddenField3" runat="server" />
        <asp:HiddenField ID="TITLEHF" runat="server" />
        <asp:HiddenField ID="BODYHF" runat="server" />
       
        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
