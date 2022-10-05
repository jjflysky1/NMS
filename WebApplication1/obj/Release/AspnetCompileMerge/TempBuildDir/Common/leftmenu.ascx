 <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="leftmenu.ascx.cs" Inherits="WebApplication1.Common.leftmenu" %>


 <script type = text/javascript>

    
   
   
     /** add active class and stay opened when selected */
var url = window.location;

// for sidebar menu entirely but not cover treeview
$('ul.sidebar-menu a').filter(function() {
	 return this.href == url;
}).parent().addClass('active');

// for treeview
$('ul.treeview-menu a').filter(function() {
	 return this.href == url;
     }).parentsUntil(".sidebar-menu > .treeview-menu").addClass('active');

     var mySkins = [
         'skin-blue',
         'skin-black',
         'skin-red',
         'skin-yellow',
         'skin-purple',
         'skin-green',
         'skin-blue-light',
         'skin-black-light',
         'skin-red-light',
         'skin-yellow-light',
         'skin-purple-light',
         'skin-green-light'
     ];


 function changeSkin(cls) {
     $.each(mySkins, function (i) {
         $('body').removeClass(mySkins[i]);
     });
     $('body').addClass(cls);
     store('skin', cls);
     return false;
     }

       function store(name, val) {
    if (typeof (Storage) !== 'undefined') {
        localStorage.setItem(name, val);
    } else {
        window.alert('Please use a modern browser to properly view this template!');
    }
  }
       function get(name) {
    if (typeof (Storage) !== 'undefined') {
      return localStorage.getItem(name);
    } else {
        window.alert('Please use a modern browser to properly view this template!');
    }
     }


       function setup() {
           var tmp = get('skin');
           if (tmp && $.inArray(tmp, mySkins))
               changeSkin(tmp);

    // Add the change skin listener
           $('[data-skin]').on('click', function (e) {
               if ($(this).hasClass('knob'))
                   return;
               e.preventDefault();
               changeSkin($(this).data('skin'));
           });

    // Add the layout manager
           $('[data-layout]').on('click', function () {
               changeLayout($(this).data('layout'));
           });

           $('[data-controlsidebar]').on('click', function () {
               changeLayout($(this).data('controlsidebar'));
               var slide = !$controlSidebar.options.slide;

               $controlSidebar.options.slide = slide;
               if (!slide)
                   $('.control-sidebar').removeClass('control-sidebar-open');
           });

           $('[data-sidebarskin="toggle"]').on('click', function () {
               var $sidebar = $('.control-sidebar');
               if ($sidebar.hasClass('control-sidebar-dark')) {
                   $sidebar.removeClass('control-sidebar-dark');
                   $sidebar.addClass('control-sidebar-light');
               } else {
                   $sidebar.removeClass('control-sidebar-light');
                   $sidebar.addClass('control-sidebar-dark');
               }
           });

           $('[data-enable="expandOnHover"]').on('click', function () {
               $(this).attr('disabled', true);
               $pushMenu.expandOnHover();
               if (!$('body').hasClass('sidebar-collapse'))
                   $('[data-layout="sidebar-collapse"]').click();
           });

    //  Reset options
    if ($('body').hasClass('fixed')) {
        $('[data-layout="fixed"]').attr('checked', 'checked');
    }
    if ($('body').hasClass('layout-boxed')) {
        $('[data-layout="layout-boxed"]').attr('checked', 'checked');
    }
    if ($('body').hasClass('sidebar-collapse')) {
        $('[data-layout="sidebar-collapse"]').attr('checked', 'checked');
    }
  }

     setup();


          function logout() {
            if (confirm("로그아웃 하시겠습니까?") == true) {
              window.location.href="Default.aspx";
            }
          }

 </script>





<header class="main-header"   >
    <!-- Logo -->
    <a href="/main5.aspx" class="logo" style="height:52px;" >
      <!-- mini logo for sidebar mini 50x50 pixels -->
      <span class="logo-mini"><b>S</b>WC</span>
      <!-- logo for regular state and mobile devices -->
      <span class="logo-lg"><b>SSIM</b> WATCHER</span>
    </a>
    <!-- Header Navbar: style can be found in header.less -->
    <nav class="navbar navbar-static-top" style="margin-bottom:-10px;" >
      <!-- Sidebar toggle button-->
      <a href="#" class="sidebar-toggle" data-toggle="push-menu" role="button">
        <span class="sr-only">Toggle navigation</span>
      </a>

      <div class="navbar-custom-menu" >
        <ul class="nav navbar-nav" >


           <li class="dropdown user user-menu">
            <a href="../User/user.aspx">   <span class="hidden-xs"><i class="fa fa-user"></i>
           <asp:Label ID="Label111" runat="server" Text="Label"></asp:Label></span>
                </a>
           
           
          </li>
          <!-- Control Sidebar Toggle Button -->
          <li >
              <a href='javascript:void(0);' onclick="logout();" class="btn btn-flat "> <i class="fa  fa-sign-out"></i> 로그아웃</a>
          </li>

           <li class="dropdown user user-menu" >
               <a href="#" class="dropdown-toggle" data-toggle="dropdown">  
              <span class="hidden-xs"><i class="fa fa-gears"></i></span>
            </a>
               <ul class="dropdown-menu" style="width:400px">
              <!-- User image -->
                    <li class="user-header" style="height:80px">
                        <p>
                         DashBoard
                        </p> 
                     </li>
                     <li class="user-body">
                <div class="row" style="margin-bottom:15px; font-size:12px;">
                  <div class="col-xs-4 text-center">
                    <a href='main5.aspx' ">V1</a>
                  </div>
                  <div class="col-xs-4 text-center">
                    <a href='main.aspx' ">V2</a>
                  </div>
                  <div class="col-xs-4 text-center">
                    <a href='main2.aspx' ">V3</a>
                  </div>
                </div>
                <!-- /.row -->
              </li>
                      <li class="user-header" style="height:80px">
                        <p>
                         Skins
                        </p> 
                     </li>
              <!-- Menu Body -->
              <li class="user-body">
                <div class="row" style="margin-bottom:15px; font-size:12px;">
                  <div class="col-xs-4 text-center">
                    <a href='javascript:void(0);' onclick="changeSkin('skin-blue');">Blue</a>
                  </div>
                  <div class="col-xs-4 text-center">
                    <a href='javascript:void(0);' onclick="changeSkin('skin-black');">Black</a>
                  </div>
                  <div class="col-xs-4 text-center">
                    <a href='javascript:void(0);' onclick="changeSkin('skin-purple');">Purple</a>
                  </div>
                </div>
                  <div class="row" style="margin-bottom:15px; font-size:12px;">
                  <div class="col-xs-4 text-center">
                    <a href='javascript:void(0);' onclick="changeSkin('skin-green');">Green</a>
                  </div>
                  <div class="col-xs-4 text-center">
                    <a href='javascript:void(0);' onclick="changeSkin('skin-red');">Red</a>
                  </div>
                  <div class="col-xs-4 text-center">
                    <a href='javascript:void(0);' onclick="changeSkin('skin-yellow');">Yellow</a>
                  </div>
                </div>
                  <div class="row" style="margin-bottom:15px; font-size:12px;">
                  <div class="col-xs-4 text-center">
                    <a href='javascript:void(0);' onclick="changeSkin('skin-blue-light');">Blue Light</a>
                  </div>
                  <div class="col-xs-4 text-center">
                    <a href='javascript:void(0);' onclick="changeSkin('skin-black-light');">Black Light</a>
                  </div>
                  <div class="col-xs-4 text-center">
                    <a href='javascript:void(0);' onclick="changeSkin('skin-purple-light');">Purple Light</a>
                  </div>
                </div>
                  <div class="row" style="margin-bottom:15px; font-size:12px;">
                  <div class="col-xs-4 text-center">
                    <a href='javascript:void(0);' onclick="changeSkin('skin-green-light');">Green Light</a>
                  </div>
                  <div class="col-xs-4 text-center">
                    <a href='javascript:void(0);' onclick="changeSkin('skin-red-light');">Red Light</a>
                  </div>
                  <div class="col-xs-4 text-center">
                    <a href='javascript:void(0);' onclick="changeSkin('skin-yellow-light');">Yellow Light</a>
                  </div>
                </div>
                <!-- /.row -->
              </li>
                   
              <!-- Menu Footer-->
           <%--   <li class="user-footer">
                <div class="pull-left">
                  <a href="#" class="btn btn-default btn-flat">Profile</a>
                </div>
                <div class="pull-right">
                  <a href="#" class="btn btn-default btn-flat">Sign out</a>
                </div>
              </li>--%>
            </ul>
          </li>
        </ul>
      </div>
    </nav>
  </header>

<aside class="main-sidebar" <%--style="background:rgba(31,55,86,1) " --%>>
    <!-- sidebar: style can be found in sidebar.less -->
    <section class="sidebar">
      <!-- sidebar menu: : style can be found in sidebar.less -->
      <ul class="sidebar-menu" data-widget="tree">
        <li class="header">MAIN NAVIGATION</li>
          <li class="treeview">
          <a href="#">
            <i class="fa fa-dashboard"></i> <span>Dashboard</span>
            <span class="pull-right-container">
              <i class="fa fa-angle-left pull-right"></i>
            </span>
          </a>
          <ul class="treeview-menu">
            <li><a href="/main5.aspx"><i class="fa fa-circle-o"></i><font size="2"> Dashboard v1</font></a></li>
            <li><a href="/main2.aspx"><i class="fa fa-circle-o"></i><font size="2"> Dashboard v2</font></a></li>
            <li><a href="/main7.aspx"><i class="fa fa-circle-o"></i><font size="2"> Dashboard v3</font></a></li>
          </ul>
        </li>
        <li class="treeview">
          <a href="#">
            <i class="fa fa-book"></i> <span>장비리스트</span>
              <i class="fa fa-angle-left pull-right" style="top:50%;" ></i>
          </a>
          <ul class="treeview-menu">
            <li ><a href="/Service/Service.aspx"><i class="fa fa-circle-o"></i><font size="2"> 장비리스트 </font></a></li>
          </ul>
        </li>
        <li class="treeview">
          <a href="#">
            <i class="fa fa-files-o"></i>
            <span>로그 리스트</span>
            <i class="fa fa-angle-left pull-right" style="top:50%;"></i>
          </a>
          <ul class="treeview-menu">
            <li><a href="/Log/Service_Log.aspx"><i class="fa fa-circle-o"></i><font size="2"> 서비스 로그</font></a></li>
            <li><a href="/Log/system_log_main.aspx"><i class="fa fa-circle-o"></i><font size="2"> 시스템 로그</font></a></li>
            <li><a href="/Log/Secure_log_main.aspx"><i class="fa fa-circle-o"></i><font size="2"> 장비 로그</font></a></li>
            <li><a href="/Log/Event_log.aspx"><i class="fa fa-circle-o"></i><font size="2"> 이벤트 로그</font></a></li>
            <li><a href="/Log/day_traffic_main.aspx"><i class="fa fa-circle-o"></i><font size="2"> 일일 트래픽</font></a></li>
            <li><a href="/Log/period_traffic_main.aspx"><i class="fa fa-circle-o"></i><font size="2"> 기간별 트래픽</font></a></li>
          </ul>
        </li>
        <li class="treeview">
          <a href="#">
            <i class="fa fa-files-o"></i>
            <span>사용자 정보</span>
            <i class="fa fa-angle-left pull-right" style="top:50%;"></i>
          </a>
          <ul class="treeview-menu">
            <li><a href="/User/user.aspx"><i class="fa fa-circle-o"></i><font size="2"> 사용자 정보</font></a></li>
          </ul>
        </li>
        <li class="treeview">
          <a href="#">
            <i class="fa fa-share"></i>
            <span>설정</span>
              <i class="fa fa-angle-left pull-right"style="top:50%;"></i>
          </a>
          <ul class="treeview-menu">
            <li><a href="/Setting/Network_Setting.aspx"><i class="fa fa-circle-o"></i><font size="2"> 네트워크 설정</font></a></li>
            <li><a href="/Setting/Mail_Setting.aspx"><i class="fa fa-circle-o"></i><font size="2"> 알림 설정</font></a></li>
            <li><a href="/Setting/oid_list.aspx"><i class="fa fa-circle-o"></i><font size="2"> OID 등록</font></a></li>
            <li><a href="/Event/Event_list.aspx"><i class="fa fa-circle-o"></i><font size="2"> 이벤트 설정</font></a></li>
          </ul>
        </li>
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
          <%--1시간--%>
          <asp:Timer runat="server" Interval="3600000" OnTick="Unnamed_Tick"></asp:Timer>
          <%--<asp:Timer runat="server" Interval="20000" OnTick="Unnamed_Tick"></asp:Timer>--%>
          <!-- Modal HTML -->
   

      </ul>
    </section>
    <!-- /.sidebar -->
  </aside>



