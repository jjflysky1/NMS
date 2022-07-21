<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="WebApplication1.Chart" %>

<%@ Register src="../Common/leftmenu.ascx" tagname="uc_menu" tagprefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

   <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.2.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Content/JavaScript.js"></script>
    <script src="../Content/JavaScript.js"></script>
    <script src="../Scripts/morris.min.js"></script>
    <link href="../Scripts/morris.css" rel="stylesheet" />
    <script src="../Scripts/Raphael-min.js"></script>
   
<%--    
  
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
<script src="//cdnjs.cloudflare.com/ajax/libs/raphael/2.1.0/raphael-min.js"></script>
<script src="//cdnjs.cloudflare.com/ajax/libs/morris.js/0.5.1/morris.min.js"></script>
    --%>

<%--    <link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/css/bootstrap.min.css'>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>

   
    <title></title>
    
<script type="text/javascript">
 


    var showAlert = setTimeout(function () {
        
    }, 10000);


   
        setInterval(function () {
      
            //alert("2초마다 반복 실행됩니다.");
        }, 1000);


</script>

</head>
<body style="background-color:#fafafa; width:30%">

    <style>

       


        .morris-hover{ margin-top:-100px; }
        @import url('https://fonts.googleapis.com/css?family=Mina|Open+Sans');

.page-header {
  font-family: 'Mina', sans-serif;
  width: 600px;
}

#dateButtonGroup {
  position: relative;
  left: 100px;
}

h1, h2, h3, h4 {
  display: inline;
}

.loss {
  color: red;
}

.gain {
  color: green;
}


    </style>
             <%--<center>
                <section class="content-header">
                  <h4>
                   <label id="title" runat="server" ></label>
                  </h4>
                     </section>
           
                
         <div id="myfirstchart" style="z-index:99; width:100%; background-color:white;margin-right:0px; margin-left:0px;  height: 150px; cursor:pointer;"></div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
               
               </center>--%>
  

<%--    <form id="form1" runat="server">
    
    </form>
     <div id = "dvCustomers">
         <table class="tblCustomer" cellpadding="2" cellspacing="0" border="1">
            <tr>
                <th>
                    <b><u><span class="name"></span></u></b>
                </th>
            </tr>
            <tr>
                <td>
                    <b>City: </b><span class="city"></span><br />
                </td>
            </tr>
        </table>
     </div>--%>
    
<div class="chart-container" style="z-index:99; width:100%; background-color:white;margin-right:0px; margin-left:0px;  height: 150px; cursor:pointer;">
  <canvas id="myChart" width="400" height="200"></canvas>
</div>
    
    <script src='../Scripts/chart.min.js'></script>
    <script>
      
        $(function () {
         $.ajax({
                type: "POST",
                url: "/Chart/Chart.aspx/chart3",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: OnSuccess3,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
           $.ajax({
                type: "POST",
                url: "/Chart/Chart.aspx/chart2",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: OnSuccess2,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
         });
         
         
         $.ajax({
                type: "POST",
                url: "/Chart/Chart.aspx/chart1",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        });

       
              
        var temp_traffic3 = "";
        var serverip3 = "";
        var temp_time3 = "";
        var trafficArray3 = "";
        var timeArray3 = "";
        let data3 ="";
        function OnSuccess3(response) {
            var customers3 = response.d;
            $(customers3).each(function () {
                serverip3 = this.serverip3;
                temp_traffic3 += this.traffic3 + ",";
                temp_time3 += this.time3 + ",";
            });
            
            temp_traffic3 = temp_traffic3.slice(0, -1);
            temp_time3 = temp_time3.slice(0, -1);
            trafficArray3 = temp_traffic3.split(',');
            timeArray3 = temp_time3.split(',');
            trafficArray3.reverse();
            timeArray3.reverse();
            data3 = trafficArray3;
        }


        
        var temp_traffic2 = "";
        var serverip2 = "";
        var temp_time2 = "";
        var trafficArray2 = "";
        var timeArray2 = "";
        let data2 ="";
        function OnSuccess2(response) {
            var customers2 = response.d;
            $(customers2).each(function () {
                serverip2 = this.serverip2;
                temp_traffic2 += this.traffic2 + ",";
                temp_time2 += this.time2 + ",";

            });
            
            temp_traffic2 = temp_traffic2.slice(0, -1);
            temp_time2 = temp_time2.slice(0, -1);
            trafficArray2 = temp_traffic2.split(',');
            timeArray2 = temp_time2.split(',');
            trafficArray2.reverse();
            timeArray2.reverse();
            data2 = trafficArray2;
        }


    function OnSuccess(response) {
        var customers = response.d;
        var temp_traffic = "";
        var serverip = "";
        var temp_time = "";
        var count = 0;
         $(customers).each(function () {
             count++;
        });
        $(customers).each(function () {
            serverip = this.serverip;
            temp_traffic += this.traffic + ",";   
            temp_time += this.time + ",";
            
        });
        temp_traffic = temp_traffic.slice(0, -1);
        temp_time = temp_time.slice(0, -1);
        var trafficArray = temp_traffic.split(',');
        var timeArray = temp_time.split(',');
        trafficArray.reverse();
        timeArray.reverse();
        let data = trafficArray;
        //alert(data2);


function addData(chart, data) {
  chart.data.datasets.forEach(dataset => {
    let data = dataset.data;
    const first = data.shift();
    data.push(first);
    dataset.data = data;
  });

  chart.update();
 }




var ctx = document.getElementById("myChart").getContext("2d");
var myChart = new Chart(ctx, {
  type: "line",
  data: {
    labels: timeArray,
    datasets: [
      {
            label: serverip,
            data: trafficArray,
        backgroundColor: ["rgba(113, 88, 203, .15)"],
        borderColor: ["rgba(113, 88, 203, 1)"],
        borderWidth: 1,
        fill: "start"
      }
      ,{
        label: serverip2,
        data: trafficArray2,
        backgroundColor: ["rgba(161, 201, 249, .15)"],
        borderColor: ["rgba(161, 201, 249, 1)"],
        borderWidth: 1,
        fill: "start"
        }
      ,{
        label: serverip3,
        data: trafficArray3,
        backgroundColor: ["rgba(192, 161, 249, .15)"],
        borderColor: ["rgba(192, 161, 249, 1)"],
        borderWidth: 1,
        fill: "start"
      }
    ]
  },
    options: {
       
        animation: {
      duration: 250
    },
    tooltips: {
      intersect: false,
      backgroundColor: "rgba(113, 88, 203, 1)",
      titleFontSize: 16,
      titleFontStyle: "400",
      titleSpacing: 4,
      titleMarginBottom: 8,
      bodyFontSize:	12,
      bodyFontStyle:	'400',
      bodySpacing: 4,
      xPadding: 8,
      yPadding: 8,
      cornerRadius: 4,
      displayColors: false,
      
      callbacks: {
        title: function (t, d) {
          const o = d.datasets.map((ds) => ds.data[t[0].index] + " Mb/s")
          
          return o.join(', ');
        },
        label: function (t, d) {
          return d.labels[t.index];
        }
      }
    },
    title: {
        text: "Public Bandwidth",
        display: false
    },
    maintainAspectRatio: true,
    spanGaps: false,
    elements: {
      line: {
        tension: 0.3
      }
    },
    plugins: {
      filler: {
        propagate: false
      }
    },
    scales: {
        xAxes: [
       
          {
              
          ticks: {
            autoSkip: true,
            maxTicksLimit: 20
          }
        }
      ]
    }
  }
        });
        //setInterval(() => addData(myChart), 5000);

    };
        



    
        


    </script>
   <%-- <form runat="server">
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </form>--%>
</body>
</html>
