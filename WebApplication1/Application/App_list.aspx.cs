using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class App_list : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                Search.Text = Request["search"];
                DropDownList1.SelectedValue = Request["type"];

            }
            else
            {

            }
            TextBox5.Value = Request["serverip"];
            //TextBox5.Value = "192.168.72.137";
            Label2.Text = Request["serverip"] + "  어플리케이션 리스트";
            
          

           
     
            UISET();
            
          
            if (Request.Cookies["userinfo"] == null)
            {
                Label3.Text = "<script>alert('로그인 해주세요');</script>";
                Response.Redirect("/Default.aspx");
            }



        }

        private void UISET()
        {
            TBLSET();
        }
        private void TBLSET()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();

            TR.BackColor = System.Drawing.Color.WhiteSmoke;

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
            TD.Text = "서버 아이피";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "어플리케이션";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

           

            TBLLIST.Rows.Add(TR);


            TBLLOAD();
        }
        private void PAGEADD(int pagecount, int nowpage)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append("<nav>");
            SB.Append("<ul class='pagination'>");
            //SB.Append("<li>" + "<a href='webform2.aspx?nowpage=" + 1 + "'>" + "<span aria-hidden='true'> &laquo;</span>" + "</a>" + "<li>");
            SB.Append("<li>" + "<a href='App_list.aspx?nowpage=" + 1 + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
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
                SB.Append("<li> " + "<a href='App_list.aspx?nowpage=" + i + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
                "" + TextBox5.Value + "'>" + i + "</a>" + " <li>");

            }
            SB.Append("<li>" + "<a href='App_list.aspx?nowpage=" + (pagecount - 1) + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip=" +
                ""+ TextBox5.Value + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
            //SB.Append("<li>" + "<a href='webform2.aspx?nowpage=" + (pagecount-1) + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
            SB.Append("</ul>");
            SB.Append("</nav>");


            Label4.Text = SB.ToString();
        }

        private void TBLLOAD()
        {

            int nowpage = Convert.ToInt32(Request["nowpage"]);
            if (nowpage == 0)
            {
                nowpage = 1;
            }

            string SQL2 = "select count(serverip) as count from server_app " + "where serverip = '" + TextBox5.Value + "'";

            if (DropDownList1.SelectedValue == "1")
            {
                SQL2 = "select count(serverip) as count from server_app where serverip like '%" + Search.Text + "%'";

            }

            DB.Open();
            MySqlCommand comm = new MySqlCommand(SQL2, DB);
            Int32 count = (Int32)comm.ExecuteScalar();
            Label1.Text = "총 수량 : " + count.ToString();
            DB.Close();

            int pagecount = count / 15 + 1;

            if (count / 15 > 0)
            {
                pagecount++;
            }


            int start = ((nowpage - 1) * 15) + 1;
            int end = nowpage * 15;
            PAGEADD(pagecount, nowpage);

            MySqlDataAdapter ADT = new MySqlDataAdapter("service_app_list", DB);
            ADT.SelectCommand.CommandType = CommandType.StoredProcedure;



            if (DropDownList1.SelectedValue == "1")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where app like '%" + Search.Text + "%'");
                ADT.SelectCommand.Parameters.AddWithValue("@where1", " tempno >= " + start + " and tempno <= " + end);
            }
            else
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", " where serverip = '" + TextBox5.Value + "'");
                ADT.SelectCommand.Parameters.AddWithValue("@where1", " tempno >= " + start + " and tempno <= " + end);
            }


            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            int a = 1;
            for (int i = 1; i < nowpage; i++)
            {
                a += 15;
            }
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD(a.ToString(), row["serverip"].ToString(), row["app"].ToString());
                a++;
            }
        }


       

        
        
        
        private void TBLADD(string a, string serverip, string app)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();

          

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = a.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

           

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = serverip.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = app.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

           


           
            TBLLIST.Rows.Add(TR);

            //Button5.Attributes.Add("href", "Service_modi_all.aspx?no" + NO.ToString());
            //Button5.Attributes.Add("data-toggle", "modal");
            //Button5.Attributes.Add("data-target", "#myModal");

     

        }

       

      
 

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("App.aspx");
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("App_list.aspx?nowpage=" + 1 + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "&serverip="+ TextBox5.Value);
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
    }
}