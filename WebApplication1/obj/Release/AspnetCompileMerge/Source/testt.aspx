﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testt.aspx.cs" Inherits="WebApplication1.testt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    
     
    
    <script src="Scripts/jschart.js"></script>

    <title></title>

  
    
</head>
<body >
    <form id="form1" runat="server" >
      <div class="cssload-wrap">
	<div class="cssload-container">
	<span class="cssload-dots"></span>
	<span class="cssload-dots"></span>
	<span class="cssload-dots"></span>
	<span class="cssload-dots"></span>
	<span class="cssload-dots"></span>
	<span class="cssload-dots"></span>
	<span class="cssload-dots"></span>
	<span class="cssload-dots"></span>
	<span class="cssload-dots"></span>
	<span class="cssload-dots"></span>
</div>
</div>
           <style>
               .cssload-wrap {
	text-align: center;
	line-height: 195px;
}
	
.cssload-container {
	display: inline-block;
}

.cssload-dots {
	display: inline-block;
	position: relative;
}
.cssload-dots:not(:last-child) {
	margin-right: 9px;
}
.cssload-dots:before, .cssload-dots:after {
	content: "";
	display: inline-block;
	width: 6px;
	height: 6px;
	border-radius: 50%;
	position: absolute;
}
.cssload-dots:nth-child(1):before {
	transform: translateY(-200%);
		-o-transform: translateY(-200%);
		-ms-transform: translateY(-200%);
		-webkit-transform: translateY(-200%);
		-moz-transform: translateY(-200%);
	animation: cssload-animBefore 1.15s linear infinite;
		-o-animation: cssload-animBefore 1.15s linear infinite;
		-ms-animation: cssload-animBefore 1.15s linear infinite;
		-webkit-animation: cssload-animBefore 1.15s linear infinite;
		-moz-animation: cssload-animBefore 1.15s linear infinite;
	animation-delay: -1.04s;
		-o-animation-delay: -1.04s;
		-ms-animation-delay: -1.04s;
		-webkit-animation-delay: -1.04s;
		-moz-animation-delay: -1.04s;
	background-color: rgb(255,0,0);
}
.cssload-dots:nth-child(1):after {
	transform: translateY(200%);
		-o-transform: translateY(200%);
		-ms-transform: translateY(200%);
		-webkit-transform: translateY(200%);
		-moz-transform: translateY(200%);
	animation: cssload-animAfter 1.15s linear infinite;
		-o-animation: cssload-animAfter 1.15s linear infinite;
		-ms-animation: cssload-animAfter 1.15s linear infinite;
		-webkit-animation: cssload-animAfter 1.15s linear infinite;
		-moz-animation: cssload-animAfter 1.15s linear infinite;
	animation-delay: -1.04s;
		-o-animation-delay: -1.04s;
		-ms-animation-delay: -1.04s;
		-webkit-animation-delay: -1.04s;
		-moz-animation-delay: -1.04s;
	background-color: rgb(119,119,119);
}
.cssload-dots:nth-child(2):before {
	transform: translateY(-200%);
		-o-transform: translateY(-200%);
		-ms-transform: translateY(-200%);
		-webkit-transform: translateY(-200%);
		-moz-transform: translateY(-200%);
	animation: cssload-animBefore 1.15s linear infinite;
		-o-animation: cssload-animBefore 1.15s linear infinite;
		-ms-animation: cssload-animBefore 1.15s linear infinite;
		-webkit-animation: cssload-animBefore 1.15s linear infinite;
		-moz-animation: cssload-animBefore 1.15s linear infinite;
	animation-delay: -2.07s;
		-o-animation-delay: -2.07s;
		-ms-animation-delay: -2.07s;
		-webkit-animation-delay: -2.07s;
		-moz-animation-delay: -2.07s;
	background-color: rgb(255,0,0);
}
.cssload-dots:nth-child(2):after {
	transform: translateY(200%);
		-o-transform: translateY(200%);
		-ms-transform: translateY(200%);
		-webkit-transform: translateY(200%);
		-moz-transform: translateY(200%);
	animation: cssload-animAfter 1.15s linear infinite;
		-o-animation: cssload-animAfter 1.15s linear infinite;
		-ms-animation: cssload-animAfter 1.15s linear infinite;
		-webkit-animation: cssload-animAfter 1.15s linear infinite;
		-moz-animation: cssload-animAfter 1.15s linear infinite;
	animation-delay: -2.07s;
		-o-animation-delay: -2.07s;
		-ms-animation-delay: -2.07s;
		-webkit-animation-delay: -2.07s;
		-moz-animation-delay: -2.07s;
	background-color: rgb(119,119,119);
}
.cssload-dots:nth-child(3):before {
	transform: translateY(-200%);
		-o-transform: translateY(-200%);
		-ms-transform: translateY(-200%);
		-webkit-transform: translateY(-200%);
		-moz-transform: translateY(-200%);
	animation: cssload-animBefore 1.15s linear infinite;
		-o-animation: cssload-animBefore 1.15s linear infinite;
		-ms-animation: cssload-animBefore 1.15s linear infinite;
		-webkit-animation: cssload-animBefore 1.15s linear infinite;
		-moz-animation: cssload-animBefore 1.15s linear infinite;
	animation-delay: -3.11s;
		-o-animation-delay: -3.11s;
		-ms-animation-delay: -3.11s;
		-webkit-animation-delay: -3.11s;
		-moz-animation-delay: -3.11s;
	background-color: rgb(255,0,0);
}
.cssload-dots:nth-child(3):after {
	transform: translateY(200%);
		-o-transform: translateY(200%);
		-ms-transform: translateY(200%);
		-webkit-transform: translateY(200%);
		-moz-transform: translateY(200%);
	animation: cssload-animAfter 1.15s linear infinite;
		-o-animation: cssload-animAfter 1.15s linear infinite;
		-ms-animation: cssload-animAfter 1.15s linear infinite;
		-webkit-animation: cssload-animAfter 1.15s linear infinite;
		-moz-animation: cssload-animAfter 1.15s linear infinite;
	animation-delay: -3.11s;
		-o-animation-delay: -3.11s;
		-ms-animation-delay: -3.11s;
		-webkit-animation-delay: -3.11s;
		-moz-animation-delay: -3.11s;
	background-color: rgb(119,119,119);
}
.cssload-dots:nth-child(4):before {
	transform: translateY(-200%);
		-o-transform: translateY(-200%);
		-ms-transform: translateY(-200%);
		-webkit-transform: translateY(-200%);
		-moz-transform: translateY(-200%);
	animation: cssload-animBefore 1.15s linear infinite;
		-o-animation: cssload-animBefore 1.15s linear infinite;
		-ms-animation: cssload-animBefore 1.15s linear infinite;
		-webkit-animation: cssload-animBefore 1.15s linear infinite;
		-moz-animation: cssload-animBefore 1.15s linear infinite;
	animation-delay: -4.14s;
		-o-animation-delay: -4.14s;
		-ms-animation-delay: -4.14s;
		-webkit-animation-delay: -4.14s;
		-moz-animation-delay: -4.14s;
	background-color: rgb(255,0,0);
}
.cssload-dots:nth-child(4):after {
	transform: translateY(200%);
		-o-transform: translateY(200%);
		-ms-transform: translateY(200%);
		-webkit-transform: translateY(200%);
		-moz-transform: translateY(200%);
	animation: cssload-animAfter 1.15s linear infinite;
		-o-animation: cssload-animAfter 1.15s linear infinite;
		-ms-animation: cssload-animAfter 1.15s linear infinite;
		-webkit-animation: cssload-animAfter 1.15s linear infinite;
		-moz-animation: cssload-animAfter 1.15s linear infinite;
	animation-delay: -4.14s;
		-o-animation-delay: -4.14s;
		-ms-animation-delay: -4.14s;
		-webkit-animation-delay: -4.14s;
		-moz-animation-delay: -4.14s;
	background-color: rgb(119,119,119);
}
.cssload-dots:nth-child(5):before {
	transform: translateY(-200%);
		-o-transform: translateY(-200%);
		-ms-transform: translateY(-200%);
		-webkit-transform: translateY(-200%);
		-moz-transform: translateY(-200%);
	animation: cssload-animBefore 1.15s linear infinite;
		-o-animation: cssload-animBefore 1.15s linear infinite;
		-ms-animation: cssload-animBefore 1.15s linear infinite;
		-webkit-animation: cssload-animBefore 1.15s linear infinite;
		-moz-animation: cssload-animBefore 1.15s linear infinite;
	animation-delay: -5.18s;
		-o-animation-delay: -5.18s;
		-ms-animation-delay: -5.18s;
		-webkit-animation-delay: -5.18s;
		-moz-animation-delay: -5.18s;
	background-color: rgb(255,0,0);
}
.cssload-dots:nth-child(5):after {
	transform: translateY(200%);
		-o-transform: translateY(200%);
		-ms-transform: translateY(200%);
		-webkit-transform: translateY(200%);
		-moz-transform: translateY(200%);
	animation: cssload-animAfter 1.15s linear infinite;
		-o-animation: cssload-animAfter 1.15s linear infinite;
		-ms-animation: cssload-animAfter 1.15s linear infinite;
		-webkit-animation: cssload-animAfter 1.15s linear infinite;
		-moz-animation: cssload-animAfter 1.15s linear infinite;
	animation-delay: -5.18s;
		-o-animation-delay: -5.18s;
		-ms-animation-delay: -5.18s;
		-webkit-animation-delay: -5.18s;
		-moz-animation-delay: -5.18s;
	background-color: rgb(119,119,119);
}
.cssload-dots:nth-child(6):before {
	transform: translateY(-200%);
		-o-transform: translateY(-200%);
		-ms-transform: translateY(-200%);
		-webkit-transform: translateY(-200%);
		-moz-transform: translateY(-200%);
	animation: cssload-animBefore 1.15s linear infinite;
		-o-animation: cssload-animBefore 1.15s linear infinite;
		-ms-animation: cssload-animBefore 1.15s linear infinite;
		-webkit-animation: cssload-animBefore 1.15s linear infinite;
		-moz-animation: cssload-animBefore 1.15s linear infinite;
	animation-delay: -6.21s;
		-o-animation-delay: -6.21s;
		-ms-animation-delay: -6.21s;
		-webkit-animation-delay: -6.21s;
		-moz-animation-delay: -6.21s;
	background-color: rgb(255,0,0);
}
.cssload-dots:nth-child(6):after {
	transform: translateY(200%);
		-o-transform: translateY(200%);
		-ms-transform: translateY(200%);
		-webkit-transform: translateY(200%);
		-moz-transform: translateY(200%);
	animation: cssload-animAfter 1.15s linear infinite;
		-o-animation: cssload-animAfter 1.15s linear infinite;
		-ms-animation: cssload-animAfter 1.15s linear infinite;
		-webkit-animation: cssload-animAfter 1.15s linear infinite;
		-moz-animation: cssload-animAfter 1.15s linear infinite;
	animation-delay: -6.21s;
		-o-animation-delay: -6.21s;
		-ms-animation-delay: -6.21s;
		-webkit-animation-delay: -6.21s;
		-moz-animation-delay: -6.21s;
	background-color: rgb(119,119,119);
}
.cssload-dots:nth-child(7):before {
	transform: translateY(-200%);
		-o-transform: translateY(-200%);
		-ms-transform: translateY(-200%);
		-webkit-transform: translateY(-200%);
		-moz-transform: translateY(-200%);
	animation: cssload-animBefore 1.15s linear infinite;
		-o-animation: cssload-animBefore 1.15s linear infinite;
		-ms-animation: cssload-animBefore 1.15s linear infinite;
		-webkit-animation: cssload-animBefore 1.15s linear infinite;
		-moz-animation: cssload-animBefore 1.15s linear infinite;
	animation-delay: -7.25s;
		-o-animation-delay: -7.25s;
		-ms-animation-delay: -7.25s;
		-webkit-animation-delay: -7.25s;
		-moz-animation-delay: -7.25s;
	background-color: rgb(255,0,0);
}
.cssload-dots:nth-child(7):after {
	transform: translateY(200%);
		-o-transform: translateY(200%);
		-ms-transform: translateY(200%);
		-webkit-transform: translateY(200%);
		-moz-transform: translateY(200%);
	animation: cssload-animAfter 1.15s linear infinite;
		-o-animation: cssload-animAfter 1.15s linear infinite;
		-ms-animation: cssload-animAfter 1.15s linear infinite;
		-webkit-animation: cssload-animAfter 1.15s linear infinite;
		-moz-animation: cssload-animAfter 1.15s linear infinite;
	animation-delay: -7.25s;
		-o-animation-delay: -7.25s;
		-ms-animation-delay: -7.25s;
		-webkit-animation-delay: -7.25s;
		-moz-animation-delay: -7.25s;
	background-color: rgb(119,119,119);
}
.cssload-dots:nth-child(8):before {
	transform: translateY(-200%);
		-o-transform: translateY(-200%);
		-ms-transform: translateY(-200%);
		-webkit-transform: translateY(-200%);
		-moz-transform: translateY(-200%);
	animation: cssload-animBefore 1.15s linear infinite;
		-o-animation: cssload-animBefore 1.15s linear infinite;
		-ms-animation: cssload-animBefore 1.15s linear infinite;
		-webkit-animation: cssload-animBefore 1.15s linear infinite;
		-moz-animation: cssload-animBefore 1.15s linear infinite;
	animation-delay: -8.28s;
		-o-animation-delay: -8.28s;
		-ms-animation-delay: -8.28s;
		-webkit-animation-delay: -8.28s;
		-moz-animation-delay: -8.28s;
	background-color: rgb(255,0,0);
}
.cssload-dots:nth-child(8):after {
	transform: translateY(200%);
		-o-transform: translateY(200%);
		-ms-transform: translateY(200%);
		-webkit-transform: translateY(200%);
		-moz-transform: translateY(200%);
	animation: cssload-animAfter 1.15s linear infinite;
		-o-animation: cssload-animAfter 1.15s linear infinite;
		-ms-animation: cssload-animAfter 1.15s linear infinite;
		-webkit-animation: cssload-animAfter 1.15s linear infinite;
		-moz-animation: cssload-animAfter 1.15s linear infinite;
	animation-delay: -8.28s;
		-o-animation-delay: -8.28s;
		-ms-animation-delay: -8.28s;
		-webkit-animation-delay: -8.28s;
		-moz-animation-delay: -8.28s;
	background-color: rgb(119,119,119);
}
.cssload-dots:nth-child(9):before {
	transform: translateY(-200%);
		-o-transform: translateY(-200%);
		-ms-transform: translateY(-200%);
		-webkit-transform: translateY(-200%);
		-moz-transform: translateY(-200%);
	animation: cssload-animBefore 1.15s linear infinite;
		-o-animation: cssload-animBefore 1.15s linear infinite;
		-ms-animation: cssload-animBefore 1.15s linear infinite;
		-webkit-animation: cssload-animBefore 1.15s linear infinite;
		-moz-animation: cssload-animBefore 1.15s linear infinite;
	animation-delay: -9.32s;
		-o-animation-delay: -9.32s;
		-ms-animation-delay: -9.32s;
		-webkit-animation-delay: -9.32s;
		-moz-animation-delay: -9.32s;
	background-color: #F00;
}
.cssload-dots:nth-child(9):after {
	transform: translateY(200%);
		-o-transform: translateY(200%);
		-ms-transform: translateY(200%);
		-webkit-transform: translateY(200%);
		-moz-transform: translateY(200%);
	animation: cssload-animAfter 1.15s linear infinite;
		-o-animation: cssload-animAfter 1.15s linear infinite;
		-ms-animation: cssload-animAfter 1.15s linear infinite;
		-webkit-animation: cssload-animAfter 1.15s linear infinite;
		-moz-animation: cssload-animAfter 1.15s linear infinite;
	animation-delay: -9.32s;
		-o-animation-delay: -9.32s;
		-ms-animation-delay: -9.32s;
		-webkit-animation-delay: -9.32s;
		-moz-animation-delay: -9.32s;
	background-color: #777;
}
.cssload-dots:nth-child(10):before {
	transform: translateY(-200%);
		-o-transform: translateY(-200%);
		-ms-transform: translateY(-200%);
		-webkit-transform: translateY(-200%);
		-moz-transform: translateY(-200%);
	animation: cssload-animBefore 1.15s linear infinite;
		-o-animation: cssload-animBefore 1.15s linear infinite;
		-ms-animation: cssload-animBefore 1.15s linear infinite;
		-webkit-animation: cssload-animBefore 1.15s linear infinite;
		-moz-animation: cssload-animBefore 1.15s linear infinite;
	animation-delay: -10.35s;
		-o-animation-delay: -10.35s;
		-ms-animation-delay: -10.35s;
		-webkit-animation-delay: -10.35s;
		-moz-animation-delay: -10.35s;
	background-color: #F00;
}
.cssload-dots:nth-child(10):after {
	transform: translateY(200%);
		-o-transform: translateY(200%);
		-ms-transform: translateY(200%);
		-webkit-transform: translateY(200%);
		-moz-transform: translateY(200%);
	animation: cssload-animAfter 1.15s linear infinite;
		-o-animation: cssload-animAfter 1.15s linear infinite;
		-ms-animation: cssload-animAfter 1.15s linear infinite;
		-webkit-animation: cssload-animAfter 1.15s linear infinite;
		-moz-animation: cssload-animAfter 1.15s linear infinite;
	animation-delay: -10.35s;
		-o-animation-delay: -10.35s;
		-ms-animation-delay: -10.35s;
		-webkit-animation-delay: -10.35s;
		-moz-animation-delay: -10.35s;
	background-color: #777;
}




@keyframes cssload-animBefore {
	0% {
		transform: scale(1) translateY(-200%);
		z-index: 1;
	}
	25% {
		transform: scale(1.3) translateY(0);
		z-index: 1;
	}
	50% {
		transform: scale(1) translateY(200%);
		z-index: -1;
	}
	75% {
		transform: scale(0.7) translateY(0);
		z-index: -1;
	}
	100% {
		transform: scale(1) translateY(-200%);
		z-index: -1;
	}
}

@-o-keyframes cssload-animBefore {
	0% {
		-o-transform: scale(1) translateY(-200%);
		z-index: 1;
	}
	25% {
		-o-transform: scale(1.3) translateY(0);
		z-index: 1;
	}
	50% {
		-o-transform: scale(1) translateY(200%);
		z-index: -1;
	}
	75% {
		-o-transform: scale(0.7) translateY(0);
		z-index: -1;
	}
	100% {
		-o-transform: scale(1) translateY(-200%);
		z-index: -1;
	}
}

@-ms-keyframes cssload-animBefore {
	0% {
		-ms-transform: scale(1) translateY(-200%);
		z-index: 1;
	}
	25% {
		-ms-transform: scale(1.3) translateY(0);
		z-index: 1;
	}
	50% {
		-ms-transform: scale(1) translateY(200%);
		z-index: -1;
	}
	75% {
		-ms-transform: scale(0.7) translateY(0);
		z-index: -1;
	}
	100% {
		-ms-transform: scale(1) translateY(-200%);
		z-index: -1;
	}
}

@-webkit-keyframes cssload-animBefore {
	0% {
		-webkit-transform: scale(1) translateY(-200%);
		z-index: 1;
	}
	25% {
		-webkit-transform: scale(1.3) translateY(0);
		z-index: 1;
	}
	50% {
		-webkit-transform: scale(1) translateY(200%);
		z-index: -1;
	}
	75% {
		-webkit-transform: scale(0.7) translateY(0);
		z-index: -1;
	}
	100% {
		-webkit-transform: scale(1) translateY(-200%);
		z-index: -1;
	}
}

@-moz-keyframes cssload-animBefore {
	0% {
		-moz-transform: scale(1) translateY(-200%);
		z-index: 1;
	}
	25% {
		-moz-transform: scale(1.3) translateY(0);
		z-index: 1;
	}
	50% {
		-moz-transform: scale(1) translateY(200%);
		z-index: -1;
	}
	75% {
		-moz-transform: scale(0.7) translateY(0);
		z-index: -1;
	}
	100% {
		-moz-transform: scale(1) translateY(-200%);
		z-index: -1;
	}
}

@keyframes cssload-animAfter {
	0% {
		transform: scale(1) translateY(200%);
		z-index: -1;
	}
	25% {
		transform: scale(0.7) translateY(0);
		z-index: -1;
	}
	50% {
		transform: scale(1) translateY(-200%);
		z-index: 1;
	}
	75% {
		transform: scale(1.3) translateY(0);
		z-index: 1;
	}
	100% {
		transform: scale(1) translateY(200%);
		z-index: 1;
	}
}

@-o-keyframes cssload-animAfter {
	0% {
		-o-transform: scale(1) translateY(200%);
		z-index: -1;
	}
	25% {
		-o-transform: scale(0.7) translateY(0);
		z-index: -1;
	}
	50% {
		-o-transform: scale(1) translateY(-200%);
		z-index: 1;
	}
	75% {
		-o-transform: scale(1.3) translateY(0);
		z-index: 1;
	}
	100% {
		-o-transform: scale(1) translateY(200%);
		z-index: 1;
	}
}

@-ms-keyframes cssload-animAfter {
	0% {
		-ms-transform: scale(1) translateY(200%);
		z-index: -1;
	}
	25% {
		-ms-transform: scale(0.7) translateY(0);
		z-index: -1;
	}
	50% {
		-ms-transform: scale(1) translateY(-200%);
		z-index: 1;
	}
	75% {
		-ms-transform: scale(1.3) translateY(0);
		z-index: 1;
	}
	100% {
		-ms-transform: scale(1) translateY(200%);
		z-index: 1;
	}
}

@-webkit-keyframes cssload-animAfter {
	0% {
		-webkit-transform: scale(1) translateY(200%);
		z-index: -1;
	}
	25% {
		-webkit-transform: scale(0.7) translateY(0);
		z-index: -1;
	}
	50% {
		-webkit-transform: scale(1) translateY(-200%);
		z-index: 1;
	}
	75% {
		-webkit-transform: scale(1.3) translateY(0);
		z-index: 1;
	}
	100% {
		-webkit-transform: scale(1) translateY(200%);
		z-index: 1;
	}
}

@-moz-keyframes cssload-animAfter {
	0% {
		-moz-transform: scale(1) translateY(200%);
		z-index: -1;
	}
	25% {
		-moz-transform: scale(0.7) translateY(0);
		z-index: -1;
	}
	50% {
		-moz-transform: scale(1) translateY(-200%);
		z-index: 1;
	}
	75% {
		-moz-transform: scale(1.3) translateY(0);
		z-index: 1;
	}
	100% {
		-moz-transform: scale(1) translateY(200%);
		z-index: 1;
	}
}
           </style>


    </form>
</body>
</html>
