using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Configuration;

namespace WebApplication1
{
    public partial class App : System.Web.UI.Page
    {
        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
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
            UISET();
            count();


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

            //TD = new TableHeaderCell();
            //TD.Width = 60;
            //TD.Text = "OS";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 60;
            //TD.Text = "Status";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 60;
            //TD.Text = "Service Name";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "아이피";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "응답속도";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 60;
            //TD.Text = "Server PWD";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);



            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "세부정보";
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
            SB.Append("<li>" + "<a href='App.aspx?nowpage=" + 1 + "'>" + "<span aria-hidden='true'> &laquo;</span>" + "</a>" + "<li>");
           
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
                SB.Append("<li> " + "<a href='App.aspx?nowpage=" + i + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "'>" + i + "</a>" + " <li>");

            }
            SB.Append("<li>" + "<a href='App.aspx?nowpage=" + (pagecount - 1) + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
            SB.Append("</ul>");
            SB.Append("</nav>");


            Label2.Text = SB.ToString();
        }

        private void TBLLOAD()
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            if (nowpage == 0)
            {
                nowpage = 1;
            }

            string SQL2 = "select count(distinct serverip) as count from server_app ";

            if (DropDownList1.SelectedValue == "4")
            {
                SQL2 = "select count(distinct serverip) as count from server_app where serverip like '%" + Search.Text + "%'";

            }
            if (DropDownList1.SelectedValue == "5")
            {
                SQL2 = "select count(distinct serverip) as count from server_app where serverip like '%" + Search.Text + "%'";

            }

            DB.Open();
            SqlCommand comm = new SqlCommand(SQL2, DB);
            Int32 count = (Int32)comm.ExecuteScalar();

            DB.Close();

            int pagecount = count / 15 +1;

            if (count / 15 > 0)
            {
                pagecount++;
            }


            int start = ((nowpage - 1) * 15) + 1;
            int end = nowpage * 15;
            PAGEADD(pagecount, nowpage);

            SqlDataAdapter ADT = new SqlDataAdapter("service_app", DB);
            ADT.SelectCommand.CommandType = CommandType.StoredProcedure;
            
          

            if (DropDownList1.SelectedValue == "1")
            {
                SQL = "select * from Service where os like '%" + Search.Text + "%' order by no desc";
            }
            else if (DropDownList1.SelectedValue == "2")
            {
                SQL = "select * from Service where status like '%" + Search.Text + "%' order by no desc";
            }
            else if (DropDownList1.SelectedValue == "3")
            {
                SQL = "select * from Service where name like '%" + Search.Text + "%' order by no desc";
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", " and serverip like '%" + Search.Text + "%'");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " tempno >= " + start + " and tempno <= " + end);
               
            }
            else if (DropDownList1.SelectedValue == "5")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", " ");
                ADT.SelectCommand.Parameters.AddWithValue("@search2", " where app like '%" + Search.Text + "%'");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " tempno >= " + start + " and tempno <= " + end);
            }
            else
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", " ");
                ADT.SelectCommand.Parameters.AddWithValue("@search2", " ");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " tempno >= " + start + " and tempno <= " + end);
            }

            
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            int a = 1;
            for (int i = 1; i<nowpage; i++)
            {
                a += 15;
            }
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {   
                TBLADD(a.ToString(), row["serverip"].ToString() , row["pingtime"].ToString(), row["flag"].ToString(), row["mail_flag"].ToString());
                a++;
            }
        }


        private void count()
        {
            SQL = "select count(distinct serverip) as count from server_app  ";
            if (DropDownList1.SelectedValue == "1")
            {
                SQL = "select count(distinct serverip) as count from Service where os like '%" + Search.Text + "%'";
            }
            else if (DropDownList1.SelectedValue == "2")
            {
                SQL = "select count(distinct serverip) as count from Service where status like '%" + Search.Text + "%' ";
            }
            else if (DropDownList1.SelectedValue == "3")
            {
                SQL = "select ccount(distinct serverip) as count from Service where name like '%" + Search.Text + "%' ";
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                SQL = "select count(distinct serverip) as count from Service where serverip like '%" + Search.Text + "%' ";
            }
            else if (DropDownList1.SelectedValue == "5")
            {
                SQL = "select count(distinct serverip) as count from Service where serverid like '%" + Search.Text + "%' ";
            }

            SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {

                Label1.Text = "총 수량 : " + row["count"].ToString();
            }
        }


       
        private void TBLADD(string a, string serverip, string pingtime, string flag , string mail_flag)
        {
            TableRow TR;
            TableCell TD;

            
            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 40;
            //TD.Text = no.ToString();
            TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = a.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            //TD = new TableCell();
            //TD.Width = 60;
            //TD.Text = os.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            //TD = new TableCell();
            //TD.Width = 60;
            //TD.Text = status.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            //TD = new TableCell();
            //TD.Width = 60;
            //TD.Text = name.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = serverip.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = pingtime.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            //TD = new TableCell();
            //TD.Width = 60;
            //TD.Text = serverpwd.ToString();
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);



            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<asp:button type='button' ID='Button2' class='btn btn-info btn-middle' runat='server'><span class='glyphicon glyphicon-share'></span> 세부 정보</button>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TD.Attributes.Add("Onclick", "go('" + serverip.ToString() + "')");
            TR.Cells.Add(TD);


           




            TBLLIST.Rows.Add(TR);

        }
      

        

        protected void Button2_Click(object sender, EventArgs e)
        {
            //DB.Open();

            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = DB;

            //cmd.CommandType = System.Data.CommandType.Text;
            //cmd.CommandText = "delete from service where no = @no";
            //cmd.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = HiddenField1.Value;

            //cmd.ExecuteNonQuery();

            //cmd.Dispose();
            //cmd = null;

            Response.Redirect("App_list.aspx?serverip=" + HiddenField1.Value);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("App.aspx?nowpage=" + 1 + "&search=" + Search.Text + "&type=" + DropDownList1.SelectedValue + "");
        }

     
    }
}