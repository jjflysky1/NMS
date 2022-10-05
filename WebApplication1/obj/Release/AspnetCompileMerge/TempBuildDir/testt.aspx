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

        //$(document).ready(function () {

        //    var sPositions = localStorage.positions || "{}",
        //        positions = JSON.parse(sPositions);
        //    $.each(positions, function (id, pos) {
        //        $("#" + id).css(pos)
        //    })
        //    $("#test").draggable({
        //        containment: "#contain",
        //        scroll: false,
        //        stop: function (event, ui) {
        //            positions[this.id] = ui.position
        //            localStorage.positions = JSON.stringify(positions)
        //        }
        //    });
        //});



    </script>



</head>
<body id="contain">
    <script src="Scripts/leader-line.min.js"></script>
  
<div id="start">Start</div>
    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
<div id="destination">Destination</div>
    
      <script>
          var myLine = new LeaderLine(
              document.getElementById('start'),
              document.getElementById('destination'),
              { dash: { animation: true } }
          );


          window.addEventListener("load", function () {
              "use strict";


              // Drag Nodes

              // Drag Nodes and redraw lines
              new PlainDraggable(start, {
                  onMove: function () {
                      myLine.position();
                  },
                  // onMoveStart: function() { line.dash = {animation: true}; },
                  onDragEnd: function () {
                      line.dash = false;
                  }
              });

              new PlainDraggable(destination, {
                  onMove: function () {
                      myLine.position();
                  },
                  // onMoveStart: function() { line.dash = {animation: true}; },
                  onDragEnd: function () {
                      line.dash = false;
                  }
              });

              new PlainDraggable(learningTask, {
                  onMove: function () {
                      learningtask1.position();
                  },
                  // onMoveStart: function() { line.dash = {animation: true}; },
                  onDragEnd: function () {
                      line.dash = false;
                  }
              });
              new PlainDraggable(math);
              new PlainDraggable(logic);
              new PlainDraggable(date);

              var myLine = new LeaderLine(
                  document.getElementById('product0'),
                  document.getElementById('product1'),
                  {
                      dash: { animation: true }
                  }
              );
          });
      </script>

</body>
</html>
