<%@ Page  MaintainScrollPositionOnPostback="true" EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="mainicon.aspx.cs" Inherits="WebApplication1.mainicon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <%--   <link href="/Content/bootstrap.min.css" rel="stylesheet" />
    <script src="/Scripts/jquery-3.2.1.min.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    
    <script src="/Content/JavaScript.js"></script>
    <link href="/Content/icon.css" rel="stylesheet" />
    <script src="/Scripts/Raphael-min.js"></script>
    <link href="/Scripts/Allcss.css" rel="stylesheet" />
    <script src="/Scripts/jschart.js"></script>
    <script src="Scripts/AdminLTE.css"></script>
    <script src="Scripts/AdminLTE.min.css"></script>--%>



    <title></title>
    <script>
        function go(No,category,os) {
        
        var obj = document.getElementById("<%=HiddenField1.ClientID %>");
        if (obj)
            obj.value = No;
          var obj = document.getElementById("<%=HiddenField2.ClientID %>");
        if (obj)
            obj.value = category;
            var obj = document.getElementById("<%=HiddenField4.ClientID %>");
        if (obj)
            obj.value = os;
        
            //alert(os);

     <%--  <%= Page.GetPostBackEventReference(Button20) %>--%>

            document.getElementById('<%=Button20.ClientID%>').click();
         
        
        }

        
function getUserIP(onNewIP) { //  onNewIp - your listener function for new IPs
    //compatibility for firefox and chrome
    var myPeerConnection = window.RTCPeerConnection || window.mozRTCPeerConnection || window.webkitRTCPeerConnection;
    var pc = new myPeerConnection({
        iceServers: []
    }),
    noop = function() {},
    localIPs = {},
    ipRegex = /([0-9]{1,3}(\.[0-9]{1,3}){3}|[a-f0-9]{1,4}(:[a-f0-9]{1,4}){7})/g,
    key;

    function iterateIP(ip) {
        if (!localIPs[ip]) onNewIP(ip);
        localIPs[ip] = true;
    }

     //create a bogus data channel
    pc.createDataChannel("");

    // create offer and set local description
    pc.createOffer().then(function(sdp) {
        sdp.sdp.split('\n').forEach(function(line) {
            if (line.indexOf('candidate') < 0) return;
            line.match(ipRegex).forEach(iterateIP);
        });
        
        pc.setLocalDescription(sdp, noop, noop);
    }).catch(function(reason) {
        // An error occurred, so handle the failure to connect
    });

    //listen for candidate events
    pc.onicecandidate = function(ice) {
        if (!ice || !ice.candidate || !ice.candidate.candidate || !ice.candidate.candidate.match(ipRegex)) return;
        ice.candidate.candidate.match(ipRegex).forEach(iterateIP);
    };
}

// Usage

getUserIP(function (ip) {
     var obj = document.getElementById("<%=HiddenField3.ClientID %>");
        if (obj)
            obj.value = ip;
    //alert("Got IP! :" + ip);
});



    </script>
</head>
<body>
    <form id="form1" runat="server" style="height:100%;">
    
       <div style="width:100%; text-align:right; padding-right:3%;" >
        <span style=" color:black; " class="glyphicon glyphicon-time" aria-hidden="true"></span><font color="black"><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></font>
       </div>

        <div style="float:left; width:30%; margin-left:1%">
               <div style="width:100%; position:relative;">
               <div class="box box-info" id="div1" runat="server"   style="float:left;  width:100%;   margin-top:auto; padding-left:2%;  margin-left:1%;  z-index:99; ">    
               <div class="box-header"  style="margin-left:0%;">
               <i class="glyphicon glyphicon-th"></i>
               <h3 class="box-title">네트워크 / 보안</h3>
               </div>    
               </div>

               <div class="box box-info" id="div3" runat="server"   style="float:left;  width:100%;  margin-top:auto; padding-left:2%;  margin-left:1%;  z-index:99; ">    
               <div class="box-header"  style="margin-left:0%;">
               <i class="glyphicon glyphicon-th"></i>
               <h3 class="box-title">서버</h3>
               </div>    
               </div>

          


        </div>
               <table style="width:100%">
                   <div id="div101" runat="server" style="float:none; position:absolute; width:70%;  margin-top:0px; margin-left:8%;  z-index:99;" >

                   </div>
               </table>
        </div>
           <div style="float:right; width:30%; margin-right:1%">
                    <div style="width:100%; position:relative;">     
                    <div id="div2" class="box box-info" runat="server"  style="  float:left;  width:100%;  margin-top:auto; padding-left:2%;    margin-right:1%;  z-index:99; ">    
                    <div class="box-header" style="margin-left:0%;" >
                    <i class="glyphicon glyphicon-th"></i>
                    <h3 class="box-title">PC</h3>
                    </div>
                    </div>

                    <div id="div4" class="box box-info" runat="server"  style=" float:left;  width:100%;   padding-left:2%;    margin-right:1%;  z-index:99; ">    
                    <div class="box-header" style="margin-left:0%;" >
                    <i class="glyphicon glyphicon-th"></i>
                    <h3 class="box-title">Log</h3>
                    <asp:Table ID="TBLLIST"   runat="server"  CssClass="table  "  Width="100%" ></asp:Table>
                    </div>
                    </div>
                    </div>   
               </div>
       
       <%-- <div id="div2" runat="server"  style="float:left; width:10%; height:100%; margin-top:0px; margin-right:5%; z-index:98;  " >
            </div>
        <div id="div3" runat="server"  style="float:left; width:10%; height:100%; margin-top:0px; z-index:200;">
            </div>
        <div id="div4" runat="server"  style="float:left; width:10%; height:100%; margin-top:0px; margin-right:5%; ">
            </div>
        <div id="div5" runat="server"  style="float:left; width:10%; height:100%; margin-top:0px; z-index:200;">    
        </div>
        <div id="div6" runat="server"  style="float:left; width:10%; height:100%; margin-top:0px; margin-right:5%; ">
            </div>
        <div id="div7" runat="server"  style="float:left; width:10%; height:100%; margin-top:0px; z-index:200;" >
            </div>
        <div id="div8" runat="server"  style="float:left; width:10%; height:100%; margin-top:0px; ">
            </div>--%>
         <asp:HiddenField ID="HiddenField1" runat="server" />
         <asp:HiddenField ID="HiddenField2" runat="server" />
         <asp:HiddenField ID="HiddenField3" runat="server" />
         <asp:HiddenField ID="HiddenField4" runat="server" />
        <br /><br />
        <div style=" visibility:hidden">
            <asp:Button ID="Button20" runat="server" Text=""   OnClick="Button20_Click" />
        </div>

    </form>
    
</body>
</html>
