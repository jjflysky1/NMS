using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace WebApplication1
{
    public partial class Service_list : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            
            if (!Page.IsPostBack)
            {
               
                Search.Text = Request["search"];
                DropDownList1.SelectedValue = Request["type"];
                DropDownList1.SelectedValue = Request["dropdownlist2"];
                //startdate.Text = Request["startdate"];
                //enddate.Text = Request["enddate"];
                //DropDownList4.SelectedValue = Request["logcount"];
                //if (enddate.Text == "")
                //{
                //    startdate.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                //}
                //else
                //{

                //}
                //if (enddate.Text == "")
                //{
                //    enddate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                //}
                //else
                //{

                //}
            }
            else
            {

            }
            TextBox5.Value = Request["serverip"];
            //TextBox5.Value = "192.168.0.200";
            HiddenField3.Value = Request["category"];
            //HiddenField3.Value = "네트워크";



            drop.Visible = false;
            maclist.Visible = false;
            snmpadd.Visible = false;
            snmplist.Visible = false;
            snmplist2.Visible = false;
            portdiv.Visible = false;
            TBLSET2();
            TBLSET();

            if (HiddenField3.Value.Contains("네트워크") == true)
            {
                TBLSET1();
                TBLSET3();
                drop.Visible = true;
                history.Visible = true;
                history2.Visible = true;
                snmpadd.Visible = true;
                snmplist.Visible = true;
                snmplist2.Visible = true;
                history_pageing.Visible = true;
                portdiv.Visible = true;
                //maclistpage.Visible = true;
            }
            if (HiddenField3.Value.Contains("AP") == true)
            {
                //TBLSET1();
                TBLSET3();
                TBLSET6();
                drop.Visible = true;
                history.Visible = true;
                history2.Visible = true;
                snmpadd.Visible = true;
                snmplist.Visible = true;
                snmplist2.Visible = true;
                history_pageing.Visible = true;
                maclist.Visible = true;
                //maclistpage.Visible = true;
            }
            if (HiddenField3.Value.Contains("서버") == true)
            {
                //TBLSET1();
                TBLSET3();
                drop.Visible = true;
                history.Visible = true;
                history2.Visible = true;
                snmpadd.Visible = true;
                snmplist.Visible = true;
                snmplist2.Visible = true;
                history_pageing.Visible = true;
            }
            TBLSET4();
          
            //TBLSET5();
            // UISET();
            count();


            //BindChart("192.168.0.137");
            //DountChart("192.168.0.137");

            if (Request.Cookies["userinfo"] == null)
            {
                Label3.Text = "<script>alert('로그인 해주세요');</script>";
                Response.Redirect("/Default.aspx");
            }

        
            Bind_DD();
  

        }


        private void Bind_DD()
        {
            if (HiddenField5.Value == "")
            {
                DataTable dt = new DataTable();

                using (MySqlConnection con2 = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
                {
                    con2.Open();
                    MySqlCommand cmd1 = new MySqlCommand("SELECT  distinct no, Equipment_name  FROM Equipment_name_list order by no asc", con2);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd1);
                    sda.Fill(dt);
                }

                MYDDL.DataSource = dt;
                MYDDL.DataTextField = "Equipment_name";
                MYDDL.DataValueField = "Equipment_name";
                MYDDL.DataBind();

            }
            HiddenField5.Value = MYDDL.SelectedValue;
            //TextBox6.Text = ListBox1.SelectedItem;

            HiddenField6.Value = MYDDL2.SelectedValue;

            DataTable dt2 = new DataTable();

            using (MySqlConnection con2 = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
            {
                con2.Open();
                MySqlCommand cmd1 = new MySqlCommand("select '선택' as model union all SELECT distinct  model  FROM All_oid where model like '%" + MYDDL.SelectedValue + "%'", con2);
                MySqlDataAdapter sda2 = new MySqlDataAdapter(cmd1);
                sda2.Fill(dt2);
            }

            MYDDL2.DataSource = dt2;
            MYDDL2.DataTextField = "model";
            MYDDL2.DataValueField = "model";
            MYDDL2.DataBind();

            try
            {
                if (HiddenField6.Value != "")
                {
                    MYDDL2.SelectedValue = HiddenField6.Value;
                }
            }
            catch
            {

            }

            if (HiddenField7.Value == "")
            {
                DataTable dt3 = new DataTable();

                using (MySqlConnection con2 = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString))
                {
                    con2.Open();
                    MySqlCommand cmd1 = new MySqlCommand("SELECT distinct  Description  FROM All_oid where model like '%" + HiddenField6.Value + "%'", con2);
                    MySqlDataAdapter sda3 = new MySqlDataAdapter(cmd1);
                    sda3.Fill(dt3);
                }

                MYDDL3.DataSource = dt3;
                MYDDL3.DataTextField = "Description";
                MYDDL3.DataValueField = "Description";
                MYDDL3.DataBind();

            }

            HiddenField7.Value = MYDDL3.SelectedValue;
        }



        private void UISET()
        {
            TBLSET();
            
            TBLSET2();
            TBLSET3();
          
        }
        private void TBLSET()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();



            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "번호";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "운영체제";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "상태";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "서비스 이름";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Linux SSH PORT";
            TD.Attributes["style"] = "text-align : center";
            TD.Attributes["id"] = "sshport";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "SNMP Community";
            TD.Attributes["style"] = "text-align : center";
            TD.Attributes["id"] = "sshport";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "HD";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "아이피";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "아이디";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);



            TD = new TableHeaderCell();
            TD.Width = 30;
            TD.Text = "수정";
            TR.Cells.Add(TD);
            TD.Attributes["style"] = "text-align : center";

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "삭제";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TBLLIST.Rows.Add(TR);


            TBLLOAD();
        }

        private void TBLSET1()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();
            TD = new TableHeaderCell();
            TR.Height = 10;


            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "NO";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Port name";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Use Status";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Traffic";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);



            //TBLLIST1.Rows.Add(TR);


            TBLLOAD1();
        }

       

        private void TBLSET2()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();
            TR.Height = 10;


            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "번호";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "제목";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 300;
            TD.Text = "내용";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "시간";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);



            TBLLIST2.Rows.Add(TR);


            TBLLOAD2();
        }

        private void TBLSET3()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();



            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "번호";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "아이피";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Model";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Function";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Delete";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TBLLIST3.Rows.Add(TR);


            TBLLOAD3();
        }
        private void TBLSET4()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();
            TR.Height = 10;


            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "번호";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "아이피";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "시간";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "비고";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TBLLIST4.Rows.Add(TR);


            TBLLOAD4();
        }
        private void TBLSET6()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();
            
            //TD = new TableHeaderCell();
            //TD.Width = 60;
            //TD.Text = "아이피";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "연결되어있는 장비 맥 주소";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);



            Table1.Rows.Add(TR);

            TBLLOAD6();
        }
        private void TBLSET5()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;


            TR = new TableHeaderRow();

            //TR.BackColor = System.Drawing.Color.WhiteSmoke;

            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "번호";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "아이피";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "CPU";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Memory";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "HD";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "트래픽";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "시간";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "삭제";

            //TR.Cells.Add(TD);

            //TBLLIST5.Rows.Add(TR);


            //TBLLOAD5();
        }

       
        private void PAGEADD(int pagecount, int nowpage)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append("<nav>");
            SB.Append("<ul class='pagination'>");
            //SB.Append("<li>" + "<a href='webform2.aspx?nowpage=" + 1 + "'>" + "<span aria-hidden='true'> &laquo;</span>" + "</a>" + "<li>");
            SB.Append("<li>" + "<a href='Service_list.aspx?nowpage=" + 1 + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
                "" + TextBox5.Value + "'>" + "<span aria-hidden='true'> &laquo;</span>" + "</a>" + "<li>");
            if (nowpage > 5)
            {
                nowpage = nowpage - 3;
            }
            if (nowpage < 6)
            {
                nowpage = 1;
            }
            for (int i = nowpage; i < pagecount; i++)
            {
                if (nowpage + 10 == i)
                {
                    break;
                }
                SB.Append("<li> " + "<a href='Service_list.aspx?nowpage=" + i + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
                "" + TextBox5.Value + "'>" + i + "</a>" + " <li>");

            }
            SB.Append("<li>" + "<a href='Service_list.aspx?nowpage=" + (pagecount - 1) + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
                "" + TextBox5.Value + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
            //SB.Append("<li>" + "<a href='webform2.aspx?nowpage=" + (pagecount-1) + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
            SB.Append("</ul>");
            SB.Append("</nav>");


            Label4.Text = SB.ToString();
        }

        private void PAGEADD2(int pagecount, int nowpage)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append("<nav>");
            SB.Append("<ul class='pagination'>");
            //SB.Append("<li>" + "<a href='webform2.aspx?nowpage=" + 1 + "'>" + "<span aria-hidden='true'> &laquo;</span>" + "</a>" + "<li>");
            SB.Append("<li>" + "<a href='Service_list.aspx?nowpage=" + 1 + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
                "" + TextBox5.Value + "&category=" + Request["category"] + "'>" + "<span aria-hidden='true'> &laquo;</span>" + "</a>" + "<li>");
            if (nowpage > 5)
            {
                nowpage = nowpage - 3;
            }
            if (nowpage < 6)
            {
                nowpage = 1;
            }
            for (int i = nowpage; i < pagecount; i++)
            {
                if (nowpage + 10 == i)
                {
                    break;
                }
                SB.Append("<li> " + "<a href='Service_list.aspx?nowpage=" + i + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
                "" + TextBox5.Value + "&category=" + Request["category"] + "'>" + i + "</a>" + " <li>");

            }
            SB.Append("<li>" + "<a href='Service_list.aspx?nowpage=" + (pagecount - 1) + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
                "" + TextBox5.Value + "&category=" + Request["category"] + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
            //SB.Append("<li>" + "<a href='webform2.aspx?nowpage=" + (pagecount-1) + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
            SB.Append("</ul>");
            SB.Append("</nav>");


            Label1.Text = SB.ToString();
        }

        private void PAGEADD3(int pagecount, int nowpage)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append("<nav>");
            SB.Append("<ul class='pagination'>");
            //SB.Append("<li>" + "<a href='webform2.aspx?nowpage=" + 1 + "'>" + "<span aria-hidden='true'> &laquo;</span>" + "</a>" + "<li>");
            SB.Append("<li>" + "<a href='Service_list.aspx?nowpage2=" + 1 + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
                "" + TextBox5.Value + "&category=" + Request["category"] + "'>" + "<span aria-hidden='true'> &laquo;</span>" + "</a>" + "<li>");
            if (nowpage > 5)
            {
                nowpage = nowpage - 3;
            }
            if (nowpage < 6)
            {
                nowpage = 1;
            }
            for (int i = nowpage; i < pagecount; i++)
            {
                if (nowpage + 10 == i)
                {
                    break;
                }
                SB.Append("<li> " + "<a href='Service_list.aspx?nowpage2=" + i + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
                "" + TextBox5.Value + "&category=" + Request["category"] + "'>" + i + "</a>" + " <li>");

            }
            SB.Append("<li>" + "<a href='Service_list.aspx?nowpage2=" + (pagecount - 1) + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
                "" + TextBox5.Value + "&category=" + Request["category"] + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
            //SB.Append("<li>" + "<a href='webform2.aspx?nowpage=" + (pagecount-1) + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
            SB.Append("</ul>");
            SB.Append("</nav>");


            Label18.Text = SB.ToString();
        }

        private void PAGEADD6(int pagecount, int nowpage)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append("<nav>");
            SB.Append("<ul class='pagination'>");
            //SB.Append("<li>" + "<a href='webform2.aspx?nowpage=" + 1 + "'>" + "<span aria-hidden='true'> &laquo;</span>" + "</a>" + "<li>");
            SB.Append("<li>" + "<a href='Service_list.aspx?nowpage2=" + 1 + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
                "" + TextBox5.Value + "&category=" + Request["category"] + "'>" + "<span aria-hidden='true'> &laquo;</span>" + "</a>" + "<li>");
            if (nowpage > 5)
            {
                nowpage = nowpage - 3;
            }
            if (nowpage < 6)
            {
                nowpage = 1;
            }
            for (int i = nowpage; i < pagecount; i++)
            {
                if (nowpage + 10 == i)
                {
                    break;
                }
                SB.Append("<li> " + "<a href='Service_list.aspx?nowpage2=" + i + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
                "" + TextBox5.Value + "&category=" + Request["category"] + "'>" + i + "</a>" + " <li>");

            }
            SB.Append("<li>" + "<a href='Service_list.aspx?nowpage2=" + (pagecount - 1) + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
                "" + TextBox5.Value + "&category=" + Request["category"] + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
            //SB.Append("<li>" + "<a href='webform2.aspx?nowpage=" + (pagecount-1) + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
            SB.Append("</ul>");
            SB.Append("</nav>");


            //Label11.Text = SB.ToString();
        }
        private void TBLLOAD()
        {


            //BindChart(Request["serverip"]);
            DountChart(Request["serverip"]);

            int nowpage = Convert.ToInt32(Request["nowpage"]);
            if (nowpage == 0)
            {
                nowpage = 1;
            }


            string SQL2 = "select count(*) as count from service where serverip = '" + TextBox5.Value + "' ";
            if (DropDownList1.SelectedValue == "1")
            {
                SQL2 = "select count(*) as count from service where os like '%" + Search.Text + "%' and serverip = '" + TextBox5.Value + "'";

            }
            else if (DropDownList1.SelectedValue == "2")
            {
                SQL2 = "select count(*) as count from service where status like '%" + Search.Text + "%' and serverip = '" + TextBox5.Value + "'";
            }
            else if (DropDownList1.SelectedValue == "3")
            {
                SQL2 = "select count(*) as count from service where name like '%" + Search.Text + "%' and serverip = '" + TextBox5.Value + "'";
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                SQL2 = "select count(*) as count from service where serverip like '%" + Search.Text + "%' and serverip = '" + TextBox5.Value + "'";
            }
            else if (DropDownList1.SelectedValue == "5")
            {
                SQL2 = "select count(*) as count from service where serverid like '%" + Search.Text + "%' and serverip = '" + TextBox5.Value + "'";
            }



            DB.Open();

            MySqlCommand comm = new MySqlCommand(SQL2, DB);
            //MySqlCommand comm = new MySqlCommand("SELECT COUNT(*) as count FROM down_log", DB);

            Int64 count = (Int64)comm.ExecuteScalar();

            DB.Close();

            int pagecount = (Int32)count / 15 + 1;

            if (count / 15 > 0)
            {
                pagecount++;
            }


            int start = ((nowpage - 1) * 15) + 1;
            int end = nowpage * 15;
            PAGEADD(pagecount, nowpage);




            MySqlDataAdapter ADT = new MySqlDataAdapter("Service_Search", DB);
            ADT.SelectCommand.CommandType = CommandType.StoredProcedure;


            if (DropDownList1.SelectedValue == "1")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "and a.OS like '%" + Search.Text + "%' and a.serverip = '" + TextBox5.Value + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where1", "where tempno >= " + start + " and tempno <= " + end);
            }
            else if (DropDownList1.SelectedValue == "2")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "and a.status like '%" + Search.Text + "%' and a.serverip = '" + TextBox5.Value + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where1", "where tempno >= " + start + " and tempno <= " + end);
            }
            else if (DropDownList1.SelectedValue == "3")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "and a.name like '%" + Search.Text + "%' and a.serverip = '" + TextBox5.Value + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where1", "where tempno >= " + start + " and tempno <= " + end);
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "and a.serverip like '%" + Search.Text + "%' and a.serverip = '" + TextBox5.Value + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where1", "where tempno >= " + start + " and tempno <= " + end);
            }
            else if (DropDownList1.SelectedValue == "5")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "and a.serverid like '%" + Search.Text + "%' and a.serverip = '" + TextBox5.Value + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where1", "where tempno >= " + start + " and tempno <= " + end);
            }
            else
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "and a.serverip = '" + TextBox5.Value + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where1", "where tempno >= " + start + " and tempno <= " + end);
            }



            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            string uptime = "";
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD(row["NO"].ToString(), row["tempno"].ToString(), row["os"].ToString(), row["Name"].ToString(), row["serverip"].ToString(),
                    row["serverid"].ToString(), row["serverpwd"].ToString(), row["status"].ToString(), row["hd"].ToString(), row["category"].ToString()
                    , row["sshport"].ToString(), row["Community"].ToString());
                uptime = row["uptime"].ToString();
            }


            Label2.Text = TextBox5.Value + "  Server List";
            Label17.Text = "Uptime : " + uptime.ToString();
        }

        
        public void TBLLOAD1()
        {
            int count = 0;
            SQL = "select count(*)+1 as count from Secure_Port_Traffic where serverip = '" + TextBox5.Value + "'";

            MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET1 = new DataSet();
            ADT1.Fill(DBSET1, "BD1");
            foreach (DataRow row in DBSET1.Tables["BD1"].Rows)
            {
                count = Convert.ToInt32(row["count"].ToString());
            }


            SQL = "select * from Secure_Port_Traffic where serverip = '" + TextBox5.Value + "' AND portname != 'Null' group by no,serverip,portname,live,traffic";
            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {

                TBLADD1(row["serverip"].ToString(), row["portname"].ToString(), row["live"].ToString(), row["traffic"].ToString(), count.ToString());
            }


            
            
          
            int j = 0;
            
            
            List<Tuple<string, string>> live = new List<Tuple<string, string>>();

            SQL = "select * from Secure_Port_Traffic where serverip = '" + TextBox5.Value + "' AND portname != 'Null' group by no,serverip,portname,live,traffic";
            MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET2 = new DataSet();
            ADT2.Fill(DBSET2, "BD");
            foreach (DataRow row in DBSET2.Tables["BD"].Rows)
            {

                live.Add(new Tuple<string, string>(row["live"].ToString(), row["portname"].ToString()));
                
                
            }
            int a = 10;
            a = live.Count() / a  ;
                
          
            TBLADD11(live);
           
            



        }

        private void TBLLOAD2()
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            if (nowpage == 0)
            {
                nowpage = 1;
            }


            string SQL2 = "select count(*) as count from server_history where serverip = '" + TextBox5.Value + "' ";

            DB.Open();

            MySqlCommand comm = new MySqlCommand(SQL2, DB);
            //MySqlCommand comm = new MySqlCommand("SELECT COUNT(*) as count FROM down_log", DB);

            Int64 count = (Int64)comm.ExecuteScalar();

            DB.Close();

            int pagecount = (Int32)count / 5 + 1;

            if (count / 5 > 0)
            {
                pagecount++;
            }


            int start = ((nowpage - 1) * 5) + 1;
            int end = nowpage * 5;
            PAGEADD2(pagecount, nowpage);

            MySqlDataAdapter ADT = new MySqlDataAdapter("History_list", DB);
            ADT.SelectCommand.CommandType = CommandType.StoredProcedure;
            ADT.SelectCommand.Parameters.AddWithValue("@search", "serverip = '" + TextBox5.Value + "'");
            ADT.SelectCommand.Parameters.AddWithValue("@where1", "where tempno >= " + start + " and tempno <= " + end);
          
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {

                TBLADD2(row["no"].ToString(), row["serverip"].ToString(), row["title"].ToString(), row["body"].ToString(), row["time"].ToString());
            }

        }

        private void TBLLOAD3()
        {


            SQL = "select distinct serverip, model, description from Server_oid_list where serverip = '" + TextBox5.Value + "' ";
            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {

                TBLADD3(row["serverip"].ToString(), row["model"].ToString(), row["description"].ToString());
            }

        }

        private void TBLLOAD4()
        {
            int nowpage2 = Convert.ToInt32(Request["nowpage2"]);
            if (nowpage2 == 0)
            {
                nowpage2 = 1;
            }


            string SQL2 = "select count(*) as count from Server_Down_Log where serverip like '%" + TextBox5.Value + "%' ";

            DB.Open();

            MySqlCommand comm = new MySqlCommand(SQL2, DB);
            //MySqlCommand comm = new MySqlCommand("SELECT COUNT(*) as count FROM down_log", DB);

            Int64 count = (Int64)comm.ExecuteScalar();

            DB.Close();

            int pagecount = (Int32)count / 5 + 1;

            if (count / 5 > 0)
            {
                pagecount++;
            }


            int start = ((nowpage2 - 1) * 5) + 1;
            int end = nowpage2 * 5;
            PAGEADD3(pagecount, nowpage2);

            MySqlDataAdapter ADT = new MySqlDataAdapter("serverdown_Log_List", DB);
            ADT.SelectCommand.CommandType = CommandType.StoredProcedure;

            ADT.SelectCommand.Parameters.AddWithValue("@search", "where serverip like '%" + TextBox5.Value + "%' ");
            ADT.SelectCommand.Parameters.AddWithValue("@where1", " no >= " + start + " and no <= " + end);

            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {

                TBLADD4(row["no"].ToString(),row["tempno"].ToString(), row["serverip"].ToString(), row["time"].ToString(),row["text"].ToString());
            }

        }

        //맥정보
        private void TBLLOAD6()
        {

            int nowpage = Convert.ToInt32(Request["nowpage"]);
            if (nowpage == 0)
            {
                nowpage = 1;
            }


            string SQL2 = "select count(*) as count from ap_mac where serverip = '" + TextBox5.Value + "' ";

            DB.Open();

            MySqlCommand comm = new MySqlCommand(SQL2, DB);
            //MySqlCommand comm = new MySqlCommand("SELECT COUNT(*) as count FROM down_log", DB);

            Int64 count = (Int64)comm.ExecuteScalar();

            DB.Close();

            int pagecount = (Int32)count / 5 + 1;

            if (count / 5 > 0)
            {
                pagecount++;
            }


            int start = ((nowpage - 1) * 5) + 1;
            int end = nowpage * 5;
            PAGEADD6(pagecount, nowpage);


            SQL = "select GROUP_CONCAT(mac SEPARATOR '  ,  ') as mac FROM ap_mac where serverip = '" + TextBox5.Value + "' ";
            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD6(row["mac"].ToString());
            }
            
        }
        
        private void TBLADD6( string mac)
        {
            TableRow TR;
            TableCell TD;

            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = mac.ToString();
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);
                
            Table1.Rows.Add(TR);
           
            
            a++;
            

        }

        private void count()
        {
            MySqlDataAdapter ADT = new MySqlDataAdapter("service_search_count", DB);
            ADT.SelectCommand.CommandType = CommandType.StoredProcedure;


            if (DropDownList1.SelectedValue == "1")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@where1", " os like '%" + Search.Text + "%' and serverip = '" + TextBox5.Value + "'");
            }
            else if (DropDownList1.SelectedValue == "2")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@where1", " status like '%" + Search.Text + "%' and serverip = '" + TextBox5.Value + "'");
            }
            else if (DropDownList1.SelectedValue == "3")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@where1", " name like '%" + Search.Text + "%' and serverip = '" + TextBox5.Value + "'");
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@where1", " serverip like '%" + Search.Text + "%' and serverip = '" + TextBox5.Value + "'");
            }
            else if (DropDownList1.SelectedValue == "5")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@where1", " serverid like '%" + Search.Text + "%' and serverip = '" + TextBox5.Value + "'");
            }
            else
            {
                ADT.SelectCommand.Parameters.AddWithValue("@where1", " serverip = '" + TextBox5.Value + "'");
            }


            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {

            }
        }
        //private void TBLLOAD5()
        //{

        //    int start = 1;
        //    int end = Convert.ToInt32(DropDownList4.SelectedValue);

        //    string SQL2 = "select count(*) as count from System_Log where time between '" + startdate.Text + "' and '" + enddate.Text + "'";
        //    if (DropDownList1.SelectedValue == "1")
        //    {
        //        SQL2 = "select count(*) as count from System_Log where serverip like '%" + Search.Text + "%' and time between '" + startdate.Text + "' and '" + enddate.Text + "'";

        //    }
        //    else if (DropDownList1.SelectedValue == "2")
        //    {
        //        SQL2 = "select count(*) as count from System_Log where serverip like '%" + Search.Text + "%'  and time between '" + startdate.Text + "' and '" + enddate.Text + "'";
        //    }
        //    else if (DropDownList1.SelectedValue == "3")
        //    {
        //        SQL2 = "select count(*) as count from System_Log where time like '%" + Search.Text + "%'  and time between '" + startdate.Text + "' and '" + enddate.Text + "'";
        //    }
        //    else if (DropDownList1.SelectedValue == "4")
        //    {
        //        SQL2 = "select count(*) as count from System_Log where status like '%" + Search.Text + "%'  and time between '" + startdate.Text + "' and '" + enddate.Text + "'";
        //    }



        //    DB.Open();

        //    MySqlCommand comm = new MySqlCommand(SQL2, DB);
        //    //MySqlCommand comm = new MySqlCommand("SELECT COUNT(*) as count FROM down_log", DB);

        //    Int32 count = (Int32)comm.ExecuteScalar();

        //    DB.Close();
          

        //    //SQL = "select * from down_log where no >= "+ start +" and no <= "+ end +" order by no desc ";
        //    //MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
        //    if(HiddenField3.Value.Contains("네트워크") == true)
        //    {
        //        MySqlDataAdapter ADT = new MySqlDataAdapter("Secure_Log_list", DB);
        //        ADT.SelectCommand.CommandType = CommandType.StoredProcedure;


        //        if (DropDownList1.SelectedValue == "1")
        //        {
        //            ADT.SelectCommand.Parameters.AddWithValue("@search", "where serverip like '%" + TextBox5.Value + "%' " +
        //               " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
        //            ADT.SelectCommand.Parameters.AddWithValue("@where1", " no >= " + start + " and no <= " + end + "");
        //        }
        //        else if (DropDownList1.SelectedValue == "2")
        //        {
        //            ADT.SelectCommand.Parameters.AddWithValue("@search", "where serverip like '%" + Search.Text + "%'");
        //            ADT.SelectCommand.Parameters.AddWithValue("@where1", " no >= " + start + " and no <= " + end +
        //                " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
        //        }
        //        else if (DropDownList1.SelectedValue == "3")
        //        {
        //            ADT.SelectCommand.Parameters.AddWithValue("@search", "where time like '%" + Search.Text + "%'");
        //            ADT.SelectCommand.Parameters.AddWithValue("@where1", " no >= " + start + " and no <= " + end +
        //                " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
        //        }
        //        else if (DropDownList1.SelectedValue == "4")
        //        {
        //            ADT.SelectCommand.Parameters.AddWithValue("@search", "where status like '%" + Search.Text + "%'");
        //            ADT.SelectCommand.Parameters.AddWithValue("@where1", " no >= " + start + " and no <= " + end +
        //                " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
        //        }
        //        else
        //        {
        //            ADT.SelectCommand.Parameters.AddWithValue("@search", "where serverip like '%" + TextBox5.Value + "%' and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
        //            //ADT.SelectCommand.Parameters.AddWithValue("@where1", " no >= " + start + " and no <= " + end);
        //            ADT.SelectCommand.Parameters.AddWithValue("@where1", " no >= " + start + " and no <= " + end);
        //        }




        //        DataSet DBSET = new DataSet();
        //        ADT.Fill(DBSET, "BD");
        //        foreach (DataRow row in DBSET.Tables["BD"].Rows)
        //        {
        //            TBLADD5(row["no"].ToString(), row["serverip"].ToString(), row["cpu"].ToString(), row["memory"].ToString(),
        //                row["traffic"].ToString(), row["time"].ToString(), row["log_time"].ToString(), row["HD"].ToString());
        //        }
        //    }
        //    else
        //    {
        //        MySqlDataAdapter ADT = new MySqlDataAdapter("System_Log_list", DB);
        //        ADT.SelectCommand.CommandType = CommandType.StoredProcedure;


        //        if (DropDownList1.SelectedValue == "1")
        //        {
        //            ADT.SelectCommand.Parameters.AddWithValue("@search", "where serverip like '%" + TextBox5.Value + "%' " +
        //               " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
        //            ADT.SelectCommand.Parameters.AddWithValue("@where1", " no >= " + start + " and no <= " + end + "");
        //        }
        //        else if (DropDownList1.SelectedValue == "2")
        //        {
        //            ADT.SelectCommand.Parameters.AddWithValue("@search", "where serverip like '%" + Search.Text + "%'");
        //            ADT.SelectCommand.Parameters.AddWithValue("@where1", " no >= " + start + " and no <= " + end +
        //                " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
        //        }
        //        else if (DropDownList1.SelectedValue == "3")
        //        {
        //            ADT.SelectCommand.Parameters.AddWithValue("@search", "where time like '%" + Search.Text + "%'");
        //            ADT.SelectCommand.Parameters.AddWithValue("@where1", " no >= " + start + " and no <= " + end +
        //                " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
        //        }
        //        else if (DropDownList1.SelectedValue == "4")
        //        {
        //            ADT.SelectCommand.Parameters.AddWithValue("@search", "where status like '%" + Search.Text + "%'");
        //            ADT.SelectCommand.Parameters.AddWithValue("@where1", " no >= " + start + " and no <= " + end +
        //                " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
        //        }
        //        else
        //        {
        //            ADT.SelectCommand.Parameters.AddWithValue("@search", "where serverip like '%" + TextBox5.Value + "%' and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
        //            //ADT.SelectCommand.Parameters.AddWithValue("@where1", " no >= " + start + " and no <= " + end);
        //            ADT.SelectCommand.Parameters.AddWithValue("@where1", " no >= " + start + " and no <= " + end);
        //        }




        //        DataSet DBSET = new DataSet();
        //        ADT.Fill(DBSET, "BD");
        //        foreach (DataRow row in DBSET.Tables["BD"].Rows)
        //        {
        //            TBLADD5(row["no"].ToString(), row["serverip"].ToString(), row["cpu"].ToString(), row["memory"].ToString(),
        //                row["traffic"].ToString(), row["time"].ToString(), row["log_time"].ToString(), row["HD"].ToString());
        //        }
        //    }
           

        //}
        private void TBLADD5(string NO, string serverip, string cpu, string memory, string traffic, string time, string log_time, string hd)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 40;
            TD.Text = NO.ToString();
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = a.ToString();
            TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = serverip.ToString();
            TD.Attributes["style"] = "text-align : center; ";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = cpu.ToString() + " %";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = memory.ToString() + " %";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = hd.ToString();
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 60;
            TD.Text = traffic.ToString() + " KB/S";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = time.ToString();
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "Delete";
            TD.CssClass = "btn btn-primary btn-middle";
            TD.Attributes["style"] = "text-align : center;";
            TD.Attributes.Add("Onclick", "go(" + NO.ToString() + ")");
            //TR.Cells.Add(TD);



            a++;
            //TBLLIST5.Rows.Add(TR);
           
        }
        long a = 1;

        private void TBLADD(string NO, string tempno, string os, string name, string serverip, string serverid, string serverpwd, string status
            , string hd, string category, string sshport, string Community)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 40;
            TD.Text = NO.ToString();
            TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = tempno.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = os.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = status.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            if (sshport.ToString() == "" && os.ToString().Contains("Linux") == true)
            {
                TD.Text = "22";
            }
            else
            {
                TD.Text = sshport.ToString();
            }
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;

            TD.Text = Community.ToString();

            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);



            TD = new TableCell();
            TD.Width = 60;
            TD.Text = hd.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = serverip.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = serverid.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            if (category.ToString() == "3")
            {
                TD = new TableCell();
                TD.Width = 30;
                TD.Text = "<button id='modi1' type='button' class='btn btn-primary btn-middle disabled'    ><span class='glyphicon glyphicon-edit' style='color:white;'></span> 수정</button> ";
                TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
                //TD.Attributes.Add("Onclick", "modi(" + NO.ToString() + ")");
                //TD.Attributes.Add("Onclick", GETMODIDATA(NO.ToString()));
                TR.Cells.Add(TD);


                Button5.Visible = false;
            }
            else
            {
                TD = new TableCell();
                TD.Width = 30;
                TD.Text = "<a href='Service_modi1.aspx?no=" + NO.ToString() + "&category=" + Request["category"] + "' data-toggle='modal' data-target='#myModal'><button id='modi1' type='button' class='btn btn-primary btn-middle'    ><span class='glyphicon glyphicon-edit' style='color:white;'></span> 수정</button> </a> ";
                TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
                //TD.Attributes.Add("Onclick", "modi(" + NO.ToString() + ")");
                //TD.Attributes.Add("Onclick", GETMODIDATA(NO.ToString()));
                TR.Cells.Add(TD);
            }




            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<asp:button type='button' class='btn btn- btn-middle'  runat='server'><span class='glyphicon glyphicon-remove-circle'></span> Delete</button>";
            TD.Attributes["style"] = "text-align : center;  vertical-align:middle;";
            TD.Attributes.Add("Onclick", "go(" + NO.ToString() + ")");
            TR.Cells.Add(TD);


            a++;
            TBLLIST.Rows.Add(TR);

            //Button5.Attributes.Add("href", "Service_modi_all.aspx?no" + NO.ToString());
            //Button5.Attributes.Add("data-toggle", "modal");
            //Button5.Attributes.Add("data-target", "#myModal");

            HiddenField2.Value = NO.ToString();

        }

        long b = 1;
        private void TBLADD1(string serverip, string portname, string live, string traffic, string count)
        {
            TableRow TR;
            TableCell TD;
            TR = new TableHeaderRow();
            TD = new TableHeaderCell();

            if (DropDownList2.Items.Count < Convert.ToInt32(count))
            {
                if(live == "2" )
                {
                    
                    DropDownList2.Items.Add(portname.ToString() + " [Down]");
                    int a = DropDownList2.Items.Count - 1;
                    //DropDownList2.Items[a].Attributes.Add("style", "color:Red");
                    //DropDownList2.BackColor = Color.Red;
                    //DropDownList2.ForeColor = Color.Red;
                }
                else
                {
                    //DropDownList2.Attributes.Add("style", "background-color:white");
                    DropDownList2.Items.Add(portname.ToString() + " [Up]");
                   
                }
                
            }
            DropDownList2.SelectedIndexChanged += work;

   

            //TD = new TableCell();
            //TD.Width = 60;
            ////TD.Text = count.ToString() ;
            //TD.Text = b.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            //TD = new TableCell();
            //TD.Width = 60;
            //TD.Text = portname.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

        

            //try
            //{
            //    if (live.ToString() == "1")
            //    {
            //        TD.Text = "<img src='/img/port_use.png' width='50px' height='50px' /><br>" + portname;
            //    }
            //    if (live.ToString() == "2")
            //    {
            //        TD.Text = "<img src='/img/port_notuse.png' width='50px' height='50px' /><br>" + portname;
            //    }
            
            //}
            //catch
            //{

            //}
          
            




            //TD = new TableCell();
            //TD.Width = 60;
            //if (traffic.ToString() == "0")
            //{
            //    //TD.Text = (Convert.ToDouble(traffic) / 1024) / 1024 + " Bytes";
            //}
            //else
            //{
            //    TD.Text = (Convert.ToDouble(traffic) / 1024) / 1024 + " Bytes";
            //    //TD.Text = String.Format("{0:#,###}", (Convert.ToDouble(traffic) / 1024) / 1024) + " Bytes";
            //}
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle; cursor:pointer; ";
            //TD.Attributes.Add("Onclick", "gochart('" + serverip.ToString() + " " + portname.ToString() + "')");
            //TR.Cells.Add(TD);



            b++;
            TBLLIST1.Rows.Add(TR);

            //Button5.Attributes.Add("href", "Service_modi_all.aspx?no" + NO.ToString());
            //Button5.Attributes.Add("data-toggle", "modal");
            //Button5.Attributes.Add("data-target", "#myModal");



        }
        private void TBLADD11(List<Tuple<string, string>> live)
        {
            TableRow TR;
            TableCell TD;
            TR = new TableHeaderRow();
            TD = new TableCell();

            int up = 0;
            int down = 0;
            foreach (var lives in live)
            {
               
                TD.Width = new Unit("10%");
                TD.Attributes["style"] = "display:table-cell; padding-left:20px; vertical-align:baseline; margin-top:10px;  margin-bottom:10px;";
                if (lives.Item1 == "1")
                {
                    TD.Text += "<div style='width:50px; float:left; margin-right:5%;  text-align:center; cursor:pointer;' onclick=port('" + lives.Item2 +"')>";
                    TD.Text += "<img src='/img/port_use.png' width='50px' height='50px' style='margin-right:10px;  vertical-align:bottom; ' /></a>";
                    TD.Text += "[UP] " + lives.Item2 + " </div> ";
                    up++;
                }
                if (lives.Item1 == "2")
                {
                    TD.Text += "<div style='width:50px; float:left; margin-right:5%;  text-align:center;cursor:pointer;' onclick=port('" + lives.Item2 + "')>";
                    TD.Text += "<img src='/img/port_notuse.png' width='50px' height='50px' style='margin-right:10px;  vertical-align:bottom; ' />";
                    TD.Text += "[DOWN] " + lives.Item2 + " </div> ";
                    down++;
                }
                TR.Cells.Add(TD);

            }

            int all = Convert.ToInt32(up) + Convert.ToInt32(down);
            TD.Text += "<br><br><div style='width:100%; float:left;  margin-right:5%; text-align:center;'>";
            TD.Text += "<b><font size = '4'>ALL : "+ all  + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;UP : " + up + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;DOWN : " + down +"</font></b>";
            TD.Text += "</div>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);




            TBLLIST1.Rows.Add(TR);



        }

        long c = 1;
        private void TBLADD2(string no, string serverip, string title, string body, string time)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();


            TD = new TableCell();
            TD.Width = 10;
            TD.Text = c.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = title.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            if (body.ToString().Length > 10)
            {
                TD.Text = "<a href='Service_history.aspx?no=" + no.ToString() + "&serverip=" + serverip.ToString() + "&flag=1' data-toggle='modal' data-target='#myModal' data-backdrop='static' data-keyboard='false'><font color='black'>" + body.Substring(0,10) +  "...</a></font>";
            }
            else
            {
                TD.Text = "<a href='Service_history.aspx?no=" + no.ToString() + "&serverip=" + serverip.ToString() + "&flag=1' data-toggle='modal' data-target='#myModal' data-backdrop='static' data-keyboard='false'><font color='black'>" + body.ToString() + "</a></font>";
            }
                
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = time.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            c++;
            TBLLIST2.Rows.Add(TR);

        }

        long f = 1;
        private void TBLADD4(string NO,string tempno, string serverip, string time, string text)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 40;
            TD.Text = NO.ToString();
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = a.ToString();
            TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = serverip.ToString();
            TD.Attributes["style"] = "text-align : center; ";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = time.ToString();
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            if(text.ToString().Length > 10)
            {
                TD.Text = "<a href='Service_Down_History.aspx?no=" + tempno.ToString() + "&serverip=" + serverip.ToString() + "&flag=1' data-toggle='modal' data-target='#myModal3' data-backdrop='static' data-keyboard='false'>" + text.Substring(0,10) +"..." + "</a>";
            }
            else if (text.ToString().Length == 0)
            {
                TD.Text = "<a href='Service_Down_History.aspx?no=" + tempno.ToString() + "&serverip=" + serverip.ToString() + "&flag=1' data-toggle='modal' data-target='#myModal3' data-backdrop='static' data-keyboard='false'>NONE" + "</a>";
            }
            else
            {
                TD.Text = "<a href='Service_Down_History.aspx?no=" + tempno.ToString() + "&serverip=" + serverip.ToString() + "&flag=1' data-toggle='modal' data-target='#myModal3' data-backdrop='static' data-keyboard='false'>" + text.ToString() + "</a>";
            }
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            f++;
            TBLLIST4.Rows.Add(TR);

        }

        long d = 1;
        private void TBLADD3(string serverip, string model, string description)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();


            TD = new TableCell();
            TD.Width = 10;
            TD.Text = d.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = serverip.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 60;
            TD.Text = model.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = description.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<asp:button type='button' class='btn btn- btn-middle'  runat='server'><span class='glyphicon glyphicon-remove-circle'></span> Delete</button>";
            TD.Attributes["style"] = "text-align : center;  vertical-align:middle;";
            TD.Attributes.Add("Onclick", "go2('" + serverip.ToString() + "','" + description.ToString() + "')");
            TR.Cells.Add(TD);

            d++;
            TBLLIST3.Rows.Add(TR);

        }
        protected void work(object sender, EventArgs e)
        {
            //string count2 = "";
            //SQL = "select service_list_chart from Log_Time_Config";
            //MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL, DB);
            //DataSet DBSET1 = new DataSet();
            //ADT1.Fill(DBSET1, "BD4");
            //foreach (DataRow row1 in DBSET1.Tables["BD4"].Rows)
            //{

            //    count2 = row1["service_list_chart"].ToString();
            //}

            HiddenField4.Value = DropDownList2.SelectedValue;
            //BindChartport(TextBox5.Value + " " + HiddenField4.Value, count2);
            //Response.Redirect("Service_list.aspx?serverip="+ TextBox5.Value+"&category="+HiddenField3.Value+"&dropdownlist2="+HiddenField4.Value);
        }

        public void Button1_Click(object sender, EventArgs e)
        {


            try
            {
                Ping ping = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                string data = "aaaaaaaaaaaaaaaaa";
                byte[] buffer = ASCIIEncoding.ASCII.GetBytes(data);
                int timeout = 120;
                PingReply reply = ping.Send(IPAddress.Parse(TextBox2.Text), timeout, buffer, options);
                if (reply.Status == IPStatus.Success) // 네트워크 사용 가능할 때~~
                {

                    DB.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = DB;

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "insert into Service (name, serverip, serverid, serverpwd,create_date) values(@name,@serverip,@serverid,@serverpwd," +
                        "(select top 1 create_date from service where serverip = @serverip))";
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = TextBox1.Text;
                    cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = TextBox2.Text;
                    cmd.Parameters.Add("@serverid", MySqlDbType.VarChar, 100).Value = TextBox3.Text;
                    cmd.Parameters.Add("@serverpwd", MySqlDbType.VarChar, 100).Value = TextBox4.Text;
                    cmd.ExecuteNonQuery();
                    DB.Close();
                    cmd.Dispose();
                    cmd = null;

                    Response.Redirect("Service_list.aspx?serverip=" + TextBox5.Value);
                }

            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('해당서버와 통신이 되지 않습니다')", true);
            }



        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "delete from service where no = @no";
            cmd.Parameters.Add("@no", MySqlDbType.VarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();
            DB.Close();
            cmd.Dispose();
            cmd = null;

            Response.Redirect("Service_list.aspx?serverip=" + TextBox5.Value);
        }



        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("Service.aspx");
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Service_list.aspx?nowpage=" + 1 + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" + TextBox5.Value);
        }

        protected void Button7_Click(object sender, EventArgs e)
        {

            Response.Redirect("Service_modi.aspx?no=" + HiddenField1.Value);

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("Service_modi_all.aspx?no=" + HiddenField1.Value);
        }

        protected void Button6_Click(object sender, EventArgs e)
        {

        }





        private DataTable GetChartData(string query)
        {

            MySqlCommand cmd = new MySqlCommand(query, DB);
            MySqlDataAdapter ADT = new MySqlDataAdapter();
            DataTable DBSET = new DataTable();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = DB;
            ADT.SelectCommand = cmd;
            ADT.Fill(DBSET);
            ADT.Dispose();
            return DBSET;
        }

        private DataTable GetChartData2(string query2)
        {

            MySqlCommand cmd2 = new MySqlCommand(query2, DB);
            MySqlDataAdapter ADT2 = new MySqlDataAdapter();
            DataTable DBSET2 = new DataTable();
            cmd2.CommandType = CommandType.Text;
            cmd2.Connection = DB;
            ADT2.SelectCommand = cmd2;
            ADT2.Fill(DBSET2);
            ADT2.Dispose();
            return DBSET2;
        }

        private void BindChart(string serverip)
        {

            string query = "select  substring(convert(varchar(16), time, 120),0,16) as time, ifnull(traffic,0), serverip from System_log_traffic where time " +
                "< convert(varchar(16), now()+1, 23) and serverip = '" + serverip + "' order by time desc limit 20";

            DataTable dt = GetChartData(query);

            if (dt.Rows.Count != 0)
            {
                string[] x = new string[dt.Rows.Count];
                decimal[] y = new decimal[dt.Rows.Count];
                int j = dt.Rows.Count - 1;
                string name = dt.Rows[0][2].ToString();
                //title.InnerText = name + " Traffic Line Chart";
                StringBuilder st = new StringBuilder();
                st.Append("<script type='text/javascript'>");
                st.Append("new Morris.Line({");
                st.Append("element: 'myfirstchart',");
                st.Append("data: [");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x[i] = dt.Rows[j][0].ToString();
                    y[i] = Convert.ToInt32(dt.Rows[j][1]);

                    st.Append("{ time: '" + x[i].Substring(5, 8) + " Hour', value: " + y[i] + "},");

                    j--;
                }
                st.Append("],");

                st.Append("xkey: 'time',");
                st.Append("ykeys: ['value'],");
                st.Append("parseTime: false,");
                st.Append("labels: ['Traffic'],");
                st.Append("resize: true");
                st.Append("   });");
                st.Append("</script>");

                //Label11.Text = st.ToString();

                st = null;
            }
            else
            {
                StringBuilder st = new StringBuilder();
                st.Append("<script type='text/javascript'>");
                st.Append("new Morris.Line({");
                st.Append("element: 'myfirstchart',");
                st.Append("data: [");
                st.Append(" { time: '', value: 0 }");
                st.Append("],");

                st.Append("xkey: 'time',");
                st.Append("ykeys: ['value'],");
                st.Append("parseTime: false,");
                st.Append("labels: ['데이터가 없습니다'],");
                st.Append("resize: true");
                st.Append("   });");
                st.Append("</script>");

                //Label11.Text = st.ToString();

                st = null;
            }
        }

        private void BindChartport(string serverip, string count)
        {
            string query = "select substring(convert(varchar(16), time, 120),0,16) as time, IFNULL(traffic,0) traffic, serverip from Secure_Log where " +
              "serverip = '" + serverip + "' and time between convert(varchar(16), now()-" + count + ", 23) and convert(varchar(16), now()+1, 23)  order by time desc";
            //string query = "select top 20 substring(convert(varchar(16), time, 120),0,16) as time,  ISNULL(traffic,0) as traffic, serverip from Secure_Log where time " +
            //    "< convert(varchar(16), now()+1, 23) and serverip = '" + serverip + "' order by time desc";

            DataTable dt = GetChartData(query);

            if (dt.Rows.Count != 0)
            {
                string[] x = new string[dt.Rows.Count];
                decimal[] y = new decimal[dt.Rows.Count];
                int j = dt.Rows.Count - 1;
                string name = dt.Rows[0][2].ToString();
                //title.InnerText = name + " Traffic Line Chart";
                StringBuilder st = new StringBuilder();
                st.Append("<script type='text/javascript'>");
                st.Append("new Morris.Area({");
                st.Append("element: 'myfirstchart',");
                st.Append("data: [");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x[i] = dt.Rows[j][0].ToString();
                    y[i] = Convert.ToInt64(dt.Rows[j][1]);


                    st.Append("{ time: '" + x[i].Substring(5, 8) + " Hour', value: " + Math.Round((y[i] / 1024 / 1024), 2) + "},");

                    j--;
                }
                st.Append("],");

                st.Append("xkey: 'time',");
                st.Append("ykeys: ['value'],");
                st.Append("parseTime: false,");
                st.Append("labels: ['Traffic'],");
                st.Append(" lineColors: ['#3c8dbc'],");
                st.Append("pointSize: 0,");
                st.Append(" postUnits: ' Mb/s',");
                st.Append("resize: true");
                st.Append("   });");
                st.Append("</script>");

                //Label11.Text = st.ToString();

                st = null;
            }
            else
            {
                StringBuilder st = new StringBuilder();
                st.Append("<script type='text/javascript'>");
                st.Append("new Morris.Line({");
                st.Append("element: 'myfirstchart',");
                st.Append("data: [");
                st.Append(" { time: '', value: 0 }");
                st.Append("],");

                st.Append("xkey: 'time',");
                st.Append("ykeys: ['value'],");
                st.Append("parseTime: false,");
                st.Append("labels: ['데이터가 없습니다'],");
                st.Append("resize: true");
                st.Append("   });");
                st.Append("</script>");

                //Label11.Text = st.ToString();

                st = null;
            }
        }

        private void DountChart(string serverip)
        {

            string query = "select  cpu,ifnull(memory,0) as memory,serverip from Service where ServerIP = '" + serverip + "' and cpu is not null limit 1";

            DataTable dt = GetChartData2(query);

            if (dt.Rows.Count != 0)
            {
                decimal[] x = new decimal[dt.Rows.Count];
                decimal[] y = new decimal[dt.Rows.Count];


                StringBuilder st = new StringBuilder();
                st.Append("<script type='text/javascript'>");
                st.Append("var donut= Morris.Donut({");
                st.Append("element: 'mysecondchart',");
                st.Append("data: [");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x[i] = Convert.ToInt32(dt.Rows[0][0]);
                    y[i] = Convert.ToInt32(dt.Rows[0][1]);
                    //st.Append("{ time: '" + x[i].Substring(0, 8) + " Hour', value: " + y[i] + "},");
                    st.Append("{label:'CPU' , value: " + x[i] + ", formatted:'" + x[i] + "%'},");
                    st.Append("{label:'NONE' , value: " + (100 - x[i]) + ", formatted:'" + (100 - x[i]) + "%'}");
                }
                st.Append("],");
                st.Append("select:0,");
                st.Append("formatter: function (x, data) { return data.formatted; }");
                st.Append("   });");
                st.Append("donut.select(0);");
                st.Append("</script>");
                Label12.Text = st.ToString();

                st = null;


                StringBuilder st2 = new StringBuilder();
                st2.Append("<script type='text/javascript'>");
                st2.Append("var donut= Morris.Donut({");
                st2.Append("element: 'mythirdchart',");
                st2.Append("data: [");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    x[i] = Convert.ToInt32(dt.Rows[0][0]);
                    y[i] = Convert.ToInt32(dt.Rows[0][1]);
                    //st.Append("{ time: '" + x[i].Substring(0, 8) + " Hour', value: " + y[i] + "},");
                    st2.Append("{label:'Memory' , value: " + y[i] + ", formatted:'" + y[i] + "%'},");
                    st2.Append("{label:'NONE' , value: " + (100 - y[i]) + ", formatted:'" + (100 - y[i]) + "% '}");
                }
                st2.Append("],");
                st2.Append("select:0,");
                st2.Append("formatter: function (x, data) { return data.formatted; }");
                st2.Append("   });");
                st2.Append("donut.select(0);");
                st2.Append("</script>");
                Label13.Text = st2.ToString();

                st2 = null;
            }
            else
            {
                StringBuilder st = new StringBuilder();
                st.Append("<script type='text/javascript'>");
                st.Append("var donut= Morris.Donut({");
                st.Append("element: 'mysecondchart',");
                st.Append("data: [");
                st.Append("    { value: 0, label: '데이터가 없습니다', formatted: '' }");

                st.Append("],");
                st.Append("select:0,");
                st.Append("formatter: function (x, data) { return data.formatted; }");
                st.Append("   });");
                st.Append("donut.select(0);");
                st.Append("</script>");
                Label12.Text = st.ToString();

                st = null;
                StringBuilder st2 = new StringBuilder();
                st2.Append("<script type='text/javascript'>");
                st2.Append("var donut= Morris.Donut({");
                st2.Append("element: 'mythirdchart',");
                st2.Append("data: [");
                st2.Append("    { value: 0, label: '데이터가 없습니다', formatted: '' }");
                st2.Append("],");
                st2.Append("select:0,");
                st2.Append("formatter: function (x, data) { return data.formatted; }");
                st2.Append("   });");
                st2.Append("donut.select(0);");
                st2.Append("</script>");
                Label13.Text = st2.ToString();

                st2 = null;
            }

        }

        //protected void Button6_Click1(object sender, EventArgs e)
        //{
        //    BindChartport(HiddenField1.Value , count);
        //}

        protected void Button9_Click(object sender, EventArgs e)
        {
            DB.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "delete from server_oid_list where serverip = @serverip and description = @description";
            cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = HiddenField1.Value;
            cmd.Parameters.Add("@description", MySqlDbType.VarChar, 100).Value = HiddenField8.Value;
            cmd.ExecuteNonQuery();
            DB.Close();
            cmd.Dispose();
            cmd = null;
            Response.Redirect("Service_list.aspx?serverip=" + TextBox5.Value + "&category=" + HiddenField3.Value);
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into server_oid_list ( serverip, model, oid, description) " +
                "select @serverip, @model1 , oid ,@description1 from all_oid where model = @model1 and description = @description1";
            cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = TextBox5.Value;
            cmd.Parameters.Add("@model1", MySqlDbType.VarChar, 100).Value = HiddenField6.Value;
            cmd.Parameters.Add("@description1", MySqlDbType.VarChar, 100).Value = HiddenField7.Value;
            cmd.ExecuteNonQuery();
            DB.Close();
            cmd.Dispose();
            cmd = null;

            Response.Redirect("Service_list.aspx?serverip=" + TextBox5.Value + "&category=" + HiddenField3.Value);
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

            //DB.Open();

            //MySqlCommand cmd = new MySqlCommand();
            //cmd.Connection = DB;

            //cmd.CommandType = System.Data.CommandType.Text;
            //cmd.CommandText = "update log_time_config set service_list_chart = @area_chart";
            //cmd.Parameters.Add("@area_chart", MySqlDbType.VarChar, 100).Value = DropDownList3.SelectedValue;
            //cmd.ExecuteNonQuery();
            //DB.Close();
            //cmd.Dispose();
            //cmd = null;

            //Response.Redirect(Request.RawUrl);
        }
     
     

        protected void ExportToImage(object sender, EventArgs e)
        {
            string base64 = Request.Form[hfImageData.UniqueID].Split(',')[1];
            byte[] bytes = Convert.FromBase64String(base64);
            Response.Clear();
            //Response.ContentType = "image/jpg";
            //Response.AddHeader("Content-Disposition", "attachment; filename=Result.jpg");
            Response.ContentType = "image/jpg";
            Response.AddHeader("Content-Disposition", "attachment; filename=Result.jpg");
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();


        }

        //protected void Button6_Click1(object sender, EventArgs e)
        //{
        //    Response.Redirect("Service_list.aspx?serverip="+ TextBox5.Value +"&startdate=" + startdate.Text + "&enddate=" + enddate.Text + " &category="+ HiddenField3.Value+"");
        //}

        //protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Response.Redirect("Service_list.aspx?serverip=" + TextBox5.Value + "&startdate=" + startdate.Text + "&enddate=" + enddate.Text + " &category=" + HiddenField3.Value + " &logcount="+DropDownList4.SelectedValue);
        //}



        public class Customer
        {
            public string serverip { get; set; }
            public string traffic { get; set; }
            public string time { get; set; }
        }
        [WebMethod]
        public static List<Customer> chart1(string serverip, string port)
        {
            MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            List<Customer> Parts = new List<Customer>();
            Parts.Clear();

            //string count = "";
            //string SQL = "select area_chart from Log_Time_Config";
            //MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            //DataSet DBSET = new DataSet();
            //ADT.Fill(DBSET, "BD");
            //foreach (DataRow row2 in DBSET.Tables["BD"].Rows)
            //{

            //    count = row2["area_chart"].ToString();
            //}

            //string time1 = DateTime.Now.ToString("HH");
            //MySqlDataAdapter ADT3 = new MySqlDataAdapter("Traffic_top", DB);
            //ADT3.SelectCommand.CommandType = CommandType.StoredProcedure;
            //ADT3.SelectCommand.Parameters.AddWithValue("@time1", time1);
            //DataSet DBSET3 = new DataSet();
            //ADT3.Fill(DBSET3, "BD3");
            //string[] serverip = { };
            //string temp = "";
            //foreach (DataRow row in DBSET3.Tables["BD3"].Rows)
            //{
            //    //그래프
            //    //temp = row["serverip"].ToString()+",";
            //    temp += row["serverip"].ToString()+",";
            //}
            //serverip = temp.Split(',');
            //select distinct  serverip, sum(traffic) AS traffic from Secure_Log where time >= DATE_SUB(NOW(), INTERVAL 10 MINUTE) GROUP BY serverip order by traffic desc LIMIT 10


            //string SQL1 = "select distinct  serverip, sum(traffic) AS traffic from Secure_Log where time >= DATE_SUB(NOW(), INTERVAL 10 MINUTE) GROUP BY serverip order by traffic desc LIMIT 10";
            //MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL1, DB);
            //DataSet DBSET1 = new DataSet();
            //ADT1.Fill(DBSET1, "BD");
            //string[] serverip = { };
            //string temp = "";
            //foreach (DataRow row in DBSET1.Tables["BD"].Rows)
            //{
            //    temp += row["serverip"].ToString() + ",";
            //}
            //serverip = temp.Split(',');

            string SQL3 = "select mac from Secure_Port_Traffic where concat(serverip , ' ' , portname) ='" + serverip[0] + "'";
            MySqlDataAdapter ADT5 = new MySqlDataAdapter(SQL3, DB);
            DataSet DBSET5 = new DataSet();
            ADT5.Fill(DBSET5, "BD");
            string mac = "";
            foreach (DataRow row in DBSET5.Tables["BD"].Rows)
            {
                mac = row["mac"].ToString();
            }


            //string count2 = "";
            //string SQL = "select service_list_chart from Log_Time_Config";
            //MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL, DB);
            //DataSet DBSET1 = new DataSet();
            //ADT1.Fill(DBSET1, "BD5");
            //foreach (DataRow row1 in DBSET1.Tables["BD5"].Rows)
            //{
            //    count2 = row1["service_list_chart"].ToString();
            //}


            //string SQL2 = "select  serverip, round((traffic / 1024 / 1024),2) as traffic, left(CONVERT(CHAR(8), time, 3),2) + '일' + left(CONVERT(CHAR(8), time, 24),5)  as temptime from Secure_Log where serverip = " +
            // "'" + serverip[0] + "' and time between convert(varchar(16), now()-" + count + ", 23) and convert(varchar(16), now()+1, 23)   order by time desc";
            string SQL2 = "select serverip, round((traffic / 1024 / 1024),2) as traffic, concat(DATE_FORMAT(time, '%e'), '일', left(DATE_FORMAT(time, '%T'),5)) as temptime from Secure_Log where serverip = " +
             "'" + serverip + " " + port + "'     order by time desc limit 60";
            //and time between DATE_ADD(now(), INTERVAL -" + count + " DAY) and DATE_ADD(now(), INTERVAL 1 DAY) 
            //원래 limit 60 = 10분
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL2, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                Parts.Add(new Customer
                {
                    serverip = row1["serverip"].ToString() + " 맥주소 :  " + mac.ToString(),
                    traffic = row1["traffic"].ToString(),
                    time = row1["temptime"].ToString()
                });
            }
            //string iresurlt = "";
            //iresurlt = JsonConvert.SerializeObject(Parts);




            return Parts;

        }

        //public static List<Customer> chart1(string serverip, string port)
        //{
        //    MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        //    List<Customer> Parts = new List<Customer>();

        //    string[] pp = port.Split('[');
        //    string SQL3 = "select mac from Secure_Port_Traffic where serverip = '" + serverip + "' and portname= '" + pp[0].Trim() + "'";
        //    MySqlDataAdapter ADT = new MySqlDataAdapter(SQL3, DB);
        //    DataSet DBSET = new DataSet();
        //    ADT.Fill(DBSET, "BD");
        //    string mac = "";
        //    foreach (DataRow row in DBSET.Tables["BD"].Rows)
        //    {
        //        mac = row["mac"].ToString();
        //    }

        //    string count2 = "";
        //    string SQL = "select service_list_chart from Log_Time_Config";
        //    MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL, DB);
        //    DataSet DBSET1 = new DataSet();
        //    ADT1.Fill(DBSET1, "BD4");
        //    foreach (DataRow row1 in DBSET1.Tables["BD4"].Rows)
        //    {
        //        count2 = row1["service_list_chart"].ToString();
        //    }

        //    string SQL2 = "select concat(DATE_FORMAT(time, '%e'), '일',left(DATE_FORMAT(time, '%T'),5)) as temptime, round((traffic / 1024 / 1024),2) as traffic, serverip from Secure_Log where " +
        //      "serverip = '" + serverip + " " + port + "' and time between DATE_ADD(now(), INTERVAL -" + count2 + " DAY) and DATE_ADD(now(), INTERVAL 1 DAY)    order by time desc";
        //    MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL2, DB);
        //    DataSet DBSET4 = new DataSet();
        //    ADT4.Fill(DBSET4, "BD4");
        //    foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
        //    {
        //        Parts.Add(new Customer
        //        {
        //            serverip = row1["serverip"].ToString() + " 맥주소 :  " + mac.ToString(),
        //            traffic = row1["traffic"].ToString(),
        //            time = row1["temptime"].ToString()
        //        });
        //    }
        //    //string iresurlt = "";
        //    //iresurlt = JsonConvert.SerializeObject(Parts);



        //    return Parts;

        //}

        protected void Button6_ServerClick(object sender, EventArgs e)
        {
            SQL = "select * FROM ap_mac where serverip = '" + TextBox5.Value + "' ";
            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            
            System.Web.HttpResponse objResponse = System.Web.HttpContext.Current.Response;

            objResponse.ClearContent();
            objResponse.ClearHeaders();
            objResponse.ContentType = "application/vnd.msexcel";
            objResponse.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(System.DateTime.Now.ToString("yyyy-MM-dd") + TextBox5.Value + " mac adress") + ".csv");
            objResponse.Charset = "euc-kr";
            objResponse.ContentEncoding = Encoding.GetEncoding(949);
            string sep = "";

            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            sep = ",";
            Response.Write("번호");
            Response.Write(sep + "아이피");
            Response.Write(sep + "맥 주소");
            sep = "\n";
            Response.Write(sep);

            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                sep = ",";
                Response.Write(row["NO"].ToString());
                Response.Write(sep + row["serverip"].ToString());
                Response.Write(sep + row["mac"].ToString());
                sep = "\n";
                Response.Write(sep);
            }


            Response.End();
            objResponse.Flush();
            objResponse.Close();
            objResponse.End();
        }

        protected void Button10_ServerClick(object sender, EventArgs e)
        {
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "delete from ap_mac";
            

            cmd.ExecuteNonQuery();
            DB.Close();
            cmd.Dispose();
            cmd = null;

            Response.Redirect("Service_list.aspx?&category=" + Request["category"] + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" + TextBox5.Value);
        }

      
    }
}
       

