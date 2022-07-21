﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using AjaxControlToolkit;

namespace WebApplication1
{
    public partial class Service_Log : System.Web.UI.Page
    {
        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
     
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!Page.IsPostBack)
            {
   
                Search.Text = Request["search"];
                DropDownList1.SelectedValue = Request["type"];
                startdate.Text = Request["startdate"];
                enddate.Text = Request["enddate"];
                DropDownList2.SelectedValue = Request["type2"];
                if (startdate.Text == "")
                {
                    startdate.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                    
                }
                if (enddate.Text == "")
                {
                    enddate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                }
            
            }
            else
            {
               
            }


            Search.Attributes.Add("onkeypress", "if (event.keyCode == 13) {" + Page.GetPostBackEventReference(Button3) + "; return false;}");


            UISET();
            
            
            if (Request.Cookies["userinfo"] == null)
            {
                Label3.Text = "<script>alert('로그인 해주세요');</script>";
                Response.Redirect("/Default.aspx");
            }
            TextBox5.Value = Request["id"];

           
          
        }

        private void UISET()
        {
            TBLSET();
            count();
            
        }
        
        private void TBLSET()
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
            TD.Text = "NO";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Service Name";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Server IP";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Time";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Status";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Delete";

            //TR.Cells.Add(TD);

            TBLLIST.Rows.Add(TR);


            TBLLOAD();
        }
     
        private void TBLLOAD()
        {
          
           int nowpage = Convert.ToInt32(Request["nowpage"]);
            if(nowpage == 0)
            {
                nowpage = 1;
            }
          

                string SQL2 = "select count(*) as count from down_log where time between '" + startdate.Text + "' and '" + enddate.Text + "'";
            if (DropDownList1.SelectedValue == "1")
            {
                 SQL2 = "select count(*) as count from down_log where name like '%" + Search.Text + "%' and time between '" + startdate.Text + "' and '" + enddate.Text + "'";
               
            }
            else if (DropDownList1.SelectedValue == "2")
            {
                 SQL2 = "select count(*) as count from down_log where serverip like '%" + Search.Text + "%'  and time between '" + startdate.Text + "' and '" + enddate.Text + "'";
            }
            else if (DropDownList1.SelectedValue == "3")
            {
                 SQL2 = "select count(*) as count from down_log where time like '%" + Search.Text + "%'  and time between '" + startdate.Text + "' and '" + enddate.Text + "'";
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                 SQL2 = "select count(*) as count from down_log where status like '%" + Search.Text + "%'  and time between '" + startdate.Text + "' and '" + enddate.Text + "'";
            }
          
           

            DB.Open();

            SqlCommand comm = new SqlCommand(SQL2, DB);
            //SqlCommand comm = new SqlCommand("SELECT COUNT(*) as count FROM down_log", DB);

            Int32 count = (Int32)comm.ExecuteScalar();

            DB.Close();
            int pagenum = Convert.ToInt32(DropDownList2.SelectedValue);

            int pagecount = count / pagenum + 1;
      
            if(count/ pagenum > 0)
            {
                pagecount++;
            }
            

            int start = ((nowpage - 1) * pagenum) +1;
            int end = nowpage * pagenum;
           

            //SQL = "select * from down_log where no >= "+ start +" and no <= "+ end +" order by no desc ";
            //SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            SqlDataAdapter ADT = new SqlDataAdapter("Service_Log_List", DB);
            ADT.SelectCommand.CommandType = CommandType.StoredProcedure;


            if (DropDownList1.SelectedValue == "1")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where name like '%" + Search.Text + "%' " +
                    " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " no >= " + start + " and no <= " + end + "");

            }
            else if (DropDownList1.SelectedValue == "2")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where serverip like '%" + Search.Text + "%' " +
                    " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " no >= " + start + " and no <= " + end + "");
            }
            else if (DropDownList1.SelectedValue == "3")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where time like '%" + Search.Text + "%' " +
                    " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " no >= " + start + " and no <= " + end + "");
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where status like '%" + Search.Text + "%'" +
                    " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " no >= " + start + " and no <= " + end + "");
            }
            else
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where time between '" + startdate.Text + "' and '" + enddate.Text + "'");
                //ADT.SelectCommand.Parameters.AddWithValue("@where", " no >= " + start + " and no <= " + end);
                ADT.SelectCommand.Parameters.AddWithValue("@where", " no >= " + start + " and no <= " + end);
            }

            PAGEADD(pagecount, nowpage);


            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD(row["NO"].ToString(), row["Name"].ToString(), row["serverip"].ToString(), row["time"].ToString(), row["status"].ToString());
            }
        }
        private void count()
        {
            SQL = "select count(*) as count from down_log where time between '" + startdate.Text + "' and '" + enddate.Text + "'";

            if (DropDownList1.SelectedValue == "1")
            {
                SQL = "select count(*) as count from down_log where name like '%" + Search.Text + "%' and time between '" + startdate.Text + "' and '" + enddate.Text + "'";

            }
            else if (DropDownList1.SelectedValue == "2")
            {
                SQL = "select count(*) as count from down_log where serverip like '%" + Search.Text + "%'  and time between '" + startdate.Text + "' and '" + enddate.Text + "'";
            }
            else if (DropDownList1.SelectedValue == "3")
            {
                SQL = "select count(*) as count from down_log where time like '%" + Search.Text + "%'  and time between '" + startdate.Text + "' and '" + enddate.Text + "'";
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                SQL = "select count(*) as count from down_log where status like '%" + Search.Text + "%'  and time between '" + startdate.Text + "' and '" + enddate.Text + "'";
            }


            SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                Label1.Text = "총 수량 : " + row["count"].ToString();
            
            }
        }

        long a = 1;
        private void TBLADD(string NO, string name, string serverip, string time, string status)
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
            TD.Text = name.ToString();
            TD.Attributes["style"] = "text-align : center; ";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = serverip.ToString();
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = time.ToString();
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = status.ToString();
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
            TBLLIST.Rows.Add(TR);

        }
        private void PAGEADD(int pagecount, int nowpage)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append("<nav>");
            SB.Append("<ul class='pagination'>");
            //SB.Append("<li>" + "<a href='webform2.aspx?nowpage=" + 1 + "'>" + "<span aria-hidden='true'> &laquo;</span>" + "</a>" + "<li>");
            SB.Append("<li>" + "<a href='Service_Log.aspx?nowpage=" + 1 + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&startdate=" + startdate.Text + "&enddate=" + enddate.Text + "&type2=" + DropDownList2.SelectedValue + "'>" + "<span aria-hidden='true'> &laquo;</span>" + "</a>" + "<li>");
            if (nowpage > 5)
            {
                nowpage = nowpage - 3;
            }
            if(nowpage < 6)
            {
                nowpage = 1;
            }
            for (int i = nowpage; i<pagecount; i++)
            {
               if(nowpage + 10 == i)
                {
                    break;
                }
                SB.Append("<li> "+ "<a href='Service_Log.aspx?nowpage=" + i + "&search=" + Search.Text +"&type=" + DropDownList1.SelectedValue + "&startdate=" + startdate.Text + "&enddate=" + enddate.Text + "&type2=" + DropDownList2.SelectedValue +  "'>" + i + "</a>" + " <li>");
                
            }
            SB.Append("<li>" + "<a href='Service_Log.aspx?nowpage=" + (pagecount - 1) + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&startdate=" + startdate.Text + "&enddate=" + enddate.Text + "&type2=" + DropDownList2.SelectedValue + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
            //SB.Append("<li>" + "<a href='webform2.aspx?nowpage=" + (pagecount-1) + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
            SB.Append("</ul>");
            SB.Append("</nav>");


            Label2.Text = SB.ToString();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Service_Log.aspx?nowpage=" + 1 + "&search=" + Search.Text +"&type=" + DropDownList1.SelectedValue + "&startdate=" + startdate.Text + "&enddate=" + enddate.Text + "");
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            if (nowpage == 0)
            {
                nowpage = 1;
            }


            string SQL2 = "select count(*) as count from down_log where time between '" + startdate.Text + "' and '" + enddate.Text + "'";
            if (DropDownList1.SelectedValue == "1")
            {
                SQL2 = "select count(*) as count from down_log where name like '%" + Search.Text + "%' and time between '" + startdate.Text + "' and '" + enddate.Text + "'";

            }
            else if (DropDownList1.SelectedValue == "2")
            {
                SQL2 = "select count(*) as count from down_log where serverip like '%" + Search.Text + "%'  and time between '" + startdate.Text + "' and '" + enddate.Text + "'";
            }
            else if (DropDownList1.SelectedValue == "3")
            {
                SQL2 = "select count(*) as count from down_log where time like '%" + Search.Text + "%'  and time between '" + startdate.Text + "' and '" + enddate.Text + "'";
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                SQL2 = "select count(*) as count from down_log where status like '%" + Search.Text + "%'  and time between '" + startdate.Text + "' and '" + enddate.Text + "'";
            }



            DB.Open();

            SqlCommand comm = new SqlCommand(SQL2, DB);
            //SqlCommand comm = new SqlCommand("SELECT COUNT(*) as count FROM down_log", DB);

            Int32 count = (Int32)comm.ExecuteScalar();

            DB.Close();

            int pagecount = count / 15 + 1;

            if (count / 15 > 0)
            {
                pagecount++;
            }


            int start = ((nowpage - 1) * 15) + 1;
            int end = nowpage * 15;


            //SQL = "select * from down_log where no >= "+ start +" and no <= "+ end +" order by no desc ";
            //SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            SqlDataAdapter ADT = new SqlDataAdapter("Service_Log_List", DB);
            ADT.SelectCommand.CommandType = CommandType.StoredProcedure;


            if (DropDownList1.SelectedValue == "1")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where name like '%" + Search.Text + "%' " +
                    " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " no >= " + start + " and no <= " + end * pagecount + "");

            }
            else if (DropDownList1.SelectedValue == "2")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where serverip like '%" + Search.Text + "%' " +
                    " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " no >= " + start + " and no <= " + end * pagecount + "");
            }
            else if (DropDownList1.SelectedValue == "3")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where time like '%" + Search.Text + "%' " +
                    " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " no >= " + start + " and no <= " + end * pagecount + "");
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where status like '%" + Search.Text + "%'" +
                    " and time between '" + startdate.Text + "' and '" + enddate.Text + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " no >= " + start + " and no <= " + end * pagecount + "");
            }
            else
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where time between '" + startdate.Text + "' and '" + enddate.Text + "'");
                //ADT.SelectCommand.Parameters.AddWithValue("@where", " no >= " + start + " and no <= " + end);
                ADT.SelectCommand.Parameters.AddWithValue("@where", " no >= " + start + " and no <= " + end * pagecount);
            }

            //PAGEADD(pagecount, nowpage);

            System.Web.HttpResponse objResponse = System.Web.HttpContext.Current.Response;

            objResponse.ClearContent();
            objResponse.ClearHeaders();
            objResponse.ContentType = "application/vnd.msexcel";
            objResponse.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(System.DateTime.Now.ToString("yyyy-MM-dd") + "Service_Log") + ".csv");
            objResponse.Charset = "euc-kr";
            objResponse.ContentEncoding = Encoding.GetEncoding(949);
            string sep = "";
            
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            sep = ",";
            Response.Write("번호");
            Response.Write(sep + "이름");
            Response.Write(sep + "아이피");
            Response.Write(sep + "시간");
            Response.Write(sep + "상태");
            sep = "\n";
            Response.Write(sep);
            
                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                {
                    sep = ",";
                    Response.Write(row["NO"].ToString());
                    Response.Write(sep + row["Name"].ToString());
                    Response.Write(sep + row["serverip"].ToString());
                    Response.Write(sep + row["time"].ToString());
                    Response.Write(sep + row["status"].ToString());
                    sep = "\n";
                    Response.Write(sep);
                }
               
            
            Response.End();
            objResponse.Flush();
            objResponse.Close();
            objResponse.End();

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        { 
            Response.Redirect("Service_Log.aspx?nowpage=" + 1 + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "&startdate=" + startdate.Text + "&enddate=" + enddate.Text + "" +"&type2=" + DropDownList2.SelectedValue);
        }
    }
}