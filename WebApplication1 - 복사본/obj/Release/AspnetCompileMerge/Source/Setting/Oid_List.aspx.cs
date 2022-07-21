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
    public partial class Oid_List : System.Web.UI.Page
    {
        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!Page.IsPostBack)
            {

                Search.Text = Request["search"];
                DropDownList1.SelectedValue = Request["type1"];
                DropDownList2.SelectedValue = Request["type2"];
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
        
        private void PAGEADD(int pagecount, int nowpage)
        {
            StringBuilder SB = new StringBuilder();
            SB.Append("<nav>");
            SB.Append("<ul class='pagination'>");
            SB.Append("<li>" + "<a href='oid_list.aspx?nowpage=" + 1 + "'>" + "<span aria-hidden='true'> &laquo;</span>" + "</a>" + "<li>");
           
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
                SB.Append("<li> " + "<a href='oid_list.aspx?nowpage=" + i + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "'>" + i + "</a>" + " <li>");

            }
            SB.Append("<li>" + "<a href='oid_list.aspx?nowpage=" + (pagecount - 1) + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
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

            string SQL2 = "select count(*) as count from all_oid ";

            if (DropDownList1.SelectedValue == "3")
            {
                SQL2 = "select count(*) as count from all_oid where model like '%" + Search.Text + "%'";

            }
            if (DropDownList1.SelectedValue == "4")
            {
                SQL2 = "select count(*) as count from all_oid where oid like '%" + Search.Text + "%'";

            }
            if (DropDownList1.SelectedValue == "5")
            {
               SQL2 = "select count(*) as count from all_oid where description like '%" + Search.Text + "%'";

            }

            DB.Open();
            SqlCommand comm = new SqlCommand(SQL2, DB);
            Int32 count = (Int32)comm.ExecuteScalar();

            DB.Close();
            int pagenum = Convert.ToInt32(DropDownList2.SelectedValue);
            int pagecount = count / pagenum + 1;

            if (count / pagenum > 0)
            {
                pagecount++;
            }
            

            int start = ((nowpage - 1) * pagenum) + 1;
            int end = nowpage * pagenum;
            PAGEADD(pagecount, nowpage);

            SqlDataAdapter ADT = new SqlDataAdapter("Oid_List_sp", DB);
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
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where model like '%" + Search.Text + "%'");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " tempno >= " + start + " and tempno <= " + end);
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where oid like '%" + Search.Text + "%'");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " tempno >= " + start + " and tempno <= " + end);
               
            }
            else if (DropDownList1.SelectedValue == "5")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where description like '%" + Search.Text + "%'");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " tempno >= " + start + " and tempno <= " + end);
            }
            else
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", " ");
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
                TBLADD(a.ToString(),row["no"].ToString() , row["model"].ToString(), row["oid"].ToString(), 
                    row["description"].ToString());
                a++;
            }
        }


        private void count()
        {
            SQL = "select count(*) as count from all_oid  ";
            if (DropDownList1.SelectedValue == "1")
            {
                SQL = "select count(*) as count from all_oid where os like '%" + Search.Text + "%'";
            }
            else if (DropDownList1.SelectedValue == "2")
            {
                SQL = "select count(distinct serverip) as count from Service where status like '%" + Search.Text + "%' ";
            }
            else if (DropDownList1.SelectedValue == "3")
            {
                SQL = "select count(*) as count from all_oid where model like '%" + Search.Text + "%' ";
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                SQL = "select count(*) as count from all_oid where oid like '%" + Search.Text + "%' ";
            }
            else if (DropDownList1.SelectedValue == "5")
            {
                SQL = "select count(*) as count from all_oid where description like '%" + Search.Text + "%' ";
            }

            SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {

                Label1.Text = "총 수량 : " + row["count"].ToString();
            }
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
            TD.Text = "Model";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Oid";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Description";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Delete";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);
            TBLLIST.Rows.Add(TR);


            TBLLOAD();
        }

        private void TBLADD(string a,string no, string model, string oid, string description)
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
            TD.Width = 30;
            TD.Text = a.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

           


            TD = new TableCell();
            TD.Width = 30;
            TD.Text = model.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 30;
            TD.Text = oid.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 30;
            TD.Text = description.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<asp:button type='button' class='btn btn- btn-middle'  runat='server'><span class='glyphicon glyphicon-remove-circle'></span> Delete</button>";
            TD.Attributes["style"] = "text-align : center;  vertical-align:middle;";
            TD.Attributes.Add("Onclick", "go2('" + no.ToString() + "')");
            TR.Cells.Add(TD);



            TBLLIST.Rows.Add(TR);

        }
      

        public void Button1_Click(object sender, EventArgs e)
        {


            //try
            //{
            //    Ping ping = new Ping();
            //    PingOptions options = new PingOptions();
            //    options.DontFragment = true;
            //    string data = "aaaaaaaaaaaaaaaaa";
            //    byte[] buffer = ASCIIEncoding.ASCII.GetBytes(data);
            //    int timeout = 120;
            //    PingReply reply = ping.Send(IPAddress.Parse(TextBox2.Text), timeout, buffer, options);
            //    if (reply.Status == IPStatus.Success) // 네트워크 사용 가능할 때~~
            //    {

            //        DB.Open();

            //        SqlCommand cmd = new SqlCommand();
            //        cmd.Connection = DB;

            //        cmd.CommandType = System.Data.CommandType.Text;
            //        cmd.CommandText = "insert into Service (name, serverip, serverid, serverpwd) values(@name,@serverip,@serverid,@serverpwd)";
            //        cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = TextBox1.Text;
            //        cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = TextBox2.Text;
            //        cmd.Parameters.Add("@serverid", SqlDbType.NVarChar, 100).Value = TextBox3.Text;
            //        cmd.Parameters.Add("@serverpwd", SqlDbType.NVarChar, 100).Value = TextBox4.Text;
            //        cmd.ExecuteNonQuery();

            //        cmd.Dispose();
            //        cmd = null;

            //        Response.Redirect("Service.aspx");
            //    }
                       
            //    }
            //catch
            //{
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('해당서버와 통신이 되지 않습니다')", true);
            //}


          
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

            Response.Redirect("Service_list.aspx?serverip=" + HiddenField1.Value+"&category="+ HiddenField2.Value);
        }

      

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Oid_List.aspx?nowpage=" + 1 + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update service set flag ='1' where serverip = @serverip";
            cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;
            Response.Redirect("Service.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update service set flag ='2' where serverip = @serverip";
            cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;


            Response.Redirect("Service.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");

        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update service set mail_flag ='1' where serverip = @serverip";
            cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;
            Response.Redirect("Service.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update service set mail_flag ='2' where serverip = @serverip";
            cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;


            Response.Redirect("Service.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update service set category = @category where serverip = @serverip";
            cmd.Parameters.Add("@category", SqlDbType.NVarChar, 100).Value = HiddenField2.Value;
            cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;
            
            Response.Redirect("Service.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "delete from Cisco_oid_list where no = @no";
            cmd.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = HiddenField3.Value;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = null;

            
            DB.Close();
            Response.Redirect("oid_list.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("Oid_List.aspx?nowpage=" + 1 + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "&type2=" + DropDownList2.SelectedValue);
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            Response.Redirect("mail_modi.aspx?no="+ HiddenField1.Value );
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update service set dash_flag ='1' where serverip = @serverip";
            cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;
            Response.Redirect("Oid_List.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update service set dash_flag ='2' where serverip = @serverip";
            cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;
            Response.Redirect("Oid_List.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void Button1_Click2(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into all_oid (model,oid,description) values(@model,@oid,@description)";
            cmd.Parameters.Add("@model", SqlDbType.NVarChar, 100).Value = Model.Text;
            cmd.Parameters.Add("@oid", SqlDbType.NVarChar, 100).Value = OID.Text;
            cmd.Parameters.Add("@description", SqlDbType.NVarChar, 100).Value = Description.SelectedValue;
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;


            Response.Redirect("Oid_List.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }
    }
}