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
    public partial class Service : System.Web.UI.Page
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
            SB.Append("<li>" + "<a href='Service.aspx?nowpage=" + 1 + "'>" + "<span aria-hidden='true'> &laquo;</span>" + "</a>" + "<li>");
           
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
                SB.Append("<li> " + "<a href='Service.aspx?nowpage=" + i + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "&type2="+ DropDownList2.SelectedValue +"'>" + i + "</a>" + " <li>");

            }
            SB.Append("<li>" + "<a href='Service.aspx?nowpage=" + (pagecount - 1) + "'>" + "<span aria-hidden='true'> &raquo;</span>" + "</a>" + "<li>");
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

            string SQL2 = "select count(distinct serverip) as count from Service ";

            if (DropDownList1.SelectedValue == "4")
            {
                SQL2 = "select count(distinct serverip) as count from service where serverip like '%" + Search.Text + "%'";

            }
            if (DropDownList1.SelectedValue == "5")
            {
                SQL2 = "select count(distinct serverip) as count from service where category like '%" + Search.Text + "%'";

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

            SqlDataAdapter ADT = new SqlDataAdapter("service_list", DB);
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
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where serverip like '%" + Search.Text + "%'");
                ADT.SelectCommand.Parameters.AddWithValue("@where", " tempno >= " + start + " and tempno <= " + end);
               
            }
            else if (DropDownList1.SelectedValue == "5")
            {
                if(Search.Text == "null")
                {
                    ADT.SelectCommand.Parameters.AddWithValue("@search", "where category is null");
                }
                else
                { 
                    ADT.SelectCommand.Parameters.AddWithValue("@search", "where category like '%" + Search.Text + "%'");
                }
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
                a += pagenum;
            }
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {   
                TBLADD(a.ToString(), row["serverip"].ToString() , row["pingtime"].ToString(), row["flag"].ToString(), 
                    row["mail_flag"].ToString(), row["computer_name"].ToString(), row["network_name"].ToString(), row["category"].ToString(),
                    row["dash_flag"].ToString());
                a++;
            }
        }


        private void count()
        {
            SQL = "select count(distinct serverip) as count from Service  ";
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
                if (Search.Text == "null")
                {
                    SQL = "select count(distinct serverip) as count from Service where category is null";
                }
                else
                {
                    SQL = "select count(distinct serverip) as count from Service where category like '%" + Search.Text + "%' ";
                }
                
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
            TD.Text = "IP";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Category";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Host Name";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Network Band";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "Detail";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "Management State";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 40;
            //TD.Text = "Dash Board";
            //TD.Attributes["style"] = "text-align : center";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "Mail State";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "Delete";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TBLLIST.Rows.Add(TR);


            TBLLOAD();
        }

        private void TBLADD(string a, string serverip, string pingtime, string flag , string mail_flag , string computer_name,
            string network_name, string category, string dash_flag)
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
            TD.Text = serverip.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            StringBuilder SB = new StringBuilder();
            SB.Append("<div class='dropdown' style='margin-left:auto; margin-right:auto;' >");
            if(category.ToString() == "")
            {
                SB.Append("<button style='width:150px' class='btn btn  dropdown-toggle' type='button' data-toggle='dropdown'>" + "미지정" + "");
            }
            else
            {
                SB.Append("<button style='width:150px' class='btn btn  dropdown-toggle' type='button' data-toggle='dropdown'>" + category.ToString() + "");
            }
            SB.Append("  <span class='caret'></span></button>");
            SB.Append("<ul style='margin-left:60%; margin-top:-35px;' class='dropdown-menu'>");
            SB.Append("<li><a href='#' onClick=category('" + serverip.ToString() + "','서버&nbsp;장비');return false;>서버 장비</a></li>");
            SB.Append("<li><a href='#' onClick=category('" + serverip.ToString() + "','네트워크/보안&nbsp;장비');return false;>네트워크/보안 장비</a></li>");
            SB.Append("<li><a href='#' onClick=category('" + serverip.ToString() + "','PC');return false;>PC</a></li>");
            SB.Append("</ul>");
            SB.Append("</div>");





            TD = new TableCell();
            TD.Width = 100;
            TD.Text = SB.ToString();
            TD.Attributes["style"] = "text-align:center; vertical-align:middle;";
            TR.Cells.Add(TD);


         

            TD = new TableCell();
            TD.Width = 40;
            TD.Text = computer_name.ToString();
            TD.Attributes["style"] = " text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 40;
            TD.Text = network_name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);



            TD = new TableCell();
            TD.Width = 40;
            TD.Text = "<asp:button type='button' ID='Button2' class='btn btn-info btn-middle' runat='server'><span class='glyphicon glyphicon-share' style='color:white;'></span> 세부 정보</button>";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TD.Attributes.Add("Onclick", "go('" + serverip.ToString() + "','" + category.ToString()+"')");
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 40;
            if(flag == "1")
            {
                //TD.Text = "<a href='Mail_modi.aspx?no=" + serverip.ToString() + "' data-toggle='modal' data-target='#myModal'><button id='modi1' type='button' class='btn btn-primary btn-middle'    ><span class='glyphicon glyphicon-edit'></span> 수정</button> </a> ";
                TD.Text = "<asp:button type='button' ID='Button2' class='btn btn-success btn-middle' runat='server'><span class='glyphicon glyphicon-ok-circle' style='color:white;'></span> Active</button>";
                TD.Attributes.Add("Onclick", "inactive('" + serverip.ToString() + "')");
            }
            else if(flag == "2")
            {
                //TD.Text = "<a href='Mail_modi.aspx?no=" + serverip.ToString() + "' data-toggle='modal' data-target='#myModal'><button id='modi1' type='button' class='btn btn-primary btn-middle'    ><span class='glyphicon glyphicon-edit'></span> 수정</button> </a> ";
                TD.Text = "<asp:button type='button' ID='Button2' class='btn btn- btn-middle' runat='server'><span class='glyphicon glyphicon-remove-circle'></span> InActive</button>";
                TD.Attributes.Add("Onclick", "active('" + serverip.ToString() + "')");
            }
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            //TD = new TableCell();
            //TD.Width = 40;
            //if (dash_flag == "1")
            //{
            //    //TD.Text = "<a href='Mail_modi.aspx?no=" + serverip.ToString() + "' data-toggle='modal' data-target='#myModal'><button id='modi1' type='button' class='btn btn-primary btn-middle'    ><span class='glyphicon glyphicon-edit'></span> 수정</button> </a> ";
            //    TD.Text = "<asp:button type='button' ID='Button2' class='btn btn-success btn-middle' runat='server'><span class='glyphicon glyphicon-ok-circle' style='color:white;'></span> Active</button>";
            //    TD.Attributes.Add("Onclick", "inactive_dash('" + serverip.ToString() + "')");
            //}
            //else if (dash_flag == "2")
            //{
            //    //TD.Text = "<a href='Mail_modi.aspx?no=" + serverip.ToString() + "' data-toggle='modal' data-target='#myModal'><button id='modi1' type='button' class='btn btn-primary btn-middle'    ><span class='glyphicon glyphicon-edit'></span> 수정</button> </a> ";
            //    TD.Text = "<asp:button type='button' ID='Button2' class='btn btn- btn-middle' runat='server'><span class='glyphicon glyphicon-remove-circle'></span> InActive</button>";
            //    TD.Attributes.Add("Onclick", "active_dash('" + serverip.ToString() + "')");
            //}
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 10;
            
                TD.Text = "<asp:button type='button' ID='Button2' class='btn btn-primary btn-middle' runat='server'><span class='glyphicon glyphicon-share' style='color:white;'></span> Modify</button>";
                //TD.Text = "<asp:button type='button' ID='Button2' class='btn btn-success btn-middle' runat='server'><span class='glyphicon glyphicon-ok-circle'></span> Active</button>";
                TD.Attributes.Add("Onclick", "mail_modi('" + serverip.ToString() + "')");
                //TD.Attributes.Add("Onclick", "inactive_mail('" + serverip.ToString() + "')");
           
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<asp:button type='button' class='btn btn- btn-middle'  runat='server'><span class='glyphicon glyphicon-remove-circle'></span> Delete</button>";
            TD.Attributes["style"] = "text-align : center;  vertical-align:middle;";
            TD.Attributes.Add("Onclick", "go2('" + serverip.ToString() + "')");
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
            Response.Redirect("Service.aspx?nowpage=" + 1 + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
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
            cmd.CommandText = "delete from service where serverip = @serverip";
            cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = HiddenField3.Value;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = null;

            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = DB;
            cmd1.CommandType = System.Data.CommandType.Text;
            cmd1.CommandText = "delete from Server_HD where serverip = @serverip";
            cmd1.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = HiddenField3.Value;
            cmd1.ExecuteNonQuery();
            cmd1.Dispose();
            cmd1 = null;

            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = DB;
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.CommandText = "delete from Server_Traffic where serverip = @serverip";
            cmd2.Parameters.Add("@serverip", SqlDbType.NVarChar, 100).Value = HiddenField3.Value;
            cmd2.ExecuteNonQuery();
            cmd2.Dispose();
            cmd2 = null;

            DB.Close();
            Response.Redirect("Service.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("Service.aspx?nowpage=" + 1 + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "&type2=" + DropDownList2.SelectedValue);
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
            Response.Redirect("Service.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
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
            Response.Redirect("Service.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }
    }
}