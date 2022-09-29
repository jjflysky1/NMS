<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testt.aspx.cs" Inherits="WebApplication1.testt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />




    <script src="Scripts/jquery-3.2.1.min.js"></script>
    <script src="Scripts/jquery-ui.min.js"></script>

    <title></title>

    
    <script>
        //function drag_start(event) {
        //    var style = window.getComputedStyle(event.target, null);
        //    var str = (parseInt(style.getPropertyValue("left")) - event.clientX) + ',' + (parseInt(style.getPropertyValue("top")) - event.clientY) + ',' + event.target.id;
        //    event.dataTransfer.setData("Text", str);
        //}

        //function drop(event) {
        //    var offset = event.dataTransfer.getData("Text").split(',');
        //    var dm = document.getElementById(offset[2]);
        //    dm.style.left = (event.clientX + parseInt(offset[0], 10)) + 'px';
        //    dm.style.top = (event.clientY + parseInt(offset[1], 10)) + 'px';
        //    event.preventDefault();
        //    return false;
        //}

        //function drag_over(event) {
        //    event.preventDefault();
        //    return false;
        //}

        $(document).ready(function () {
       
            var sPositions = localStorage.positions || "{}",
                positions = JSON.parse(sPositions);
            $.each(positions, function (id, pos) {
                $("#" + id).css(pos)
            })
            $("#test").draggable({
                containment: "#contain",
                scroll: false,
                stop: function (event, ui) {
                    positions[this.id] = ui.position
                    localStorage.positions = JSON.stringify(positions)
                }
            });
        });

  
    </script>

    <style>
        .draggable {
            width: 90px;
            height: 90px;
            padding: 0.5em;
            float: left;
            margin: 0 10px 10px 0;
        }

        #draggable, #draggable2 {
            margin-bottom: 20px;
        }

        #draggable {
            cursor: n-resize;
        }

        #draggable3 {
            cursor: move;
        }

        #containment-wrapper {
            width: 700px;
            height: 500px;
            border: 1px solid #000;
            padding: 5px;
        }

 
    </style>
    

</head>
<body id="contain">
    
   <div id="containment-wrapper">
    <div id="test">
        <p>DRAG ME</p>
    
    </div>
       </div>



</body>
</html>
