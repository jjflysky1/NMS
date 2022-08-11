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
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class Event_list : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Search.Attributes["onkeyPress"] = "if (event.keyCode==13){" + Page.GetPostBackEventReference(Button3) + "; return false;}";
            TextBox2.Attributes["onkeyPress"] = "if (event.keyCode==13){" + Page.GetPostBackEventReference(Button100) + "; return false;}";

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

            string SQL2 = "select count(distinct name) as count from event_list ";

            if (DropDownList1.SelectedValue == "4")
            {
                SQL2 = "select count(distinct name) as count from event_list where name like '%" + Search.Text + "%'";

            }
            if (DropDownList1.SelectedValue == "5")
            {
                SQL2 = "select count(distinct serverip) as count from event_list where category like '%" + Search.Text + "%'";

            }

            DB.Open();
            MySqlCommand comm = new MySqlCommand(SQL2, DB);
            Int64 count = (Int64)comm.ExecuteScalar();

            DB.Close();
            int pagenum = Convert.ToInt32(DropDownList2.SelectedValue);
            int pagecount = (Int32)count / pagenum + 1;

            if (count / pagenum > 0)
            {
                pagecount++;
            }
            

            int start = ((nowpage - 1) * pagenum) + 1;
            int end = nowpage * pagenum;
            PAGEADD(pagecount, nowpage);

            MySqlDataAdapter ADT = new MySqlDataAdapter("Event_list_porc", DB);
            ADT.SelectCommand.CommandType = CommandType.StoredProcedure;
            
          

            if (DropDownList1.SelectedValue == "1")
            {
                SQL = "select * from event_list where os like '%" + Search.Text + "%' order by no desc";
            }
            else if (DropDownList1.SelectedValue == "2")
            {
                SQL = "select * from event_list where status like '%" + Search.Text + "%' order by no desc";
            }
            else if (DropDownList1.SelectedValue == "3")
            {
                SQL = "select * from event_list where name like '%" + Search.Text + "%' order by no desc";
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", "where name like '%" + Search.Text + "%'");
                ADT.SelectCommand.Parameters.AddWithValue("@where1", " tempno >= " + start + " and tempno <= " + end);
               
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
                ADT.SelectCommand.Parameters.AddWithValue("@where1", " tempno >= " + start + " and tempno <= " + end);
            }
            else
            {
                ADT.SelectCommand.Parameters.AddWithValue("@search", " ");
                ADT.SelectCommand.Parameters.AddWithValue("@where1", " tempno >= " + start + " and tempno <= " + end);
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
                TBLADD(a.ToString(), row["name"].ToString() ,  row["flag"].ToString());
                a++;
            }
        }


        private void count()
        {
            SQL = "select count(distinct name) as count from event_list  ";
            if (DropDownList1.SelectedValue == "1")
            {
                SQL = "select count(distinct name) as count from event_list where os like '%" + Search.Text + "%'";
            }
            else if (DropDownList1.SelectedValue == "2")
            {
                SQL = "select count(distinct name) as count from event_list where name like '%" + Search.Text + "%' ";
            }
            else if (DropDownList1.SelectedValue == "3")
            {
                SQL = "select count(distinct name) as count from event_list where name like '%" + Search.Text + "%' ";
            }
            else if (DropDownList1.SelectedValue == "4")
            {
                SQL = "select count(distinct name) as count from event_list where name like '%" + Search.Text + "%' ";
            }
            else if (DropDownList1.SelectedValue == "5")
            {
                if (Search.Text == "null")
                {
                    SQL = "select count(distinct name) as count from event_list where name is null";
                }
                else
                {
                    SQL = "select count(distinct name) as count from event_list where name like '%" + Search.Text + "%' ";
                }
                
            }

            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
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
            TD.Text = "번호";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);



            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "이벤트 이름";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "연락처 상태";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);



            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "사용여부";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 10;
            TD.Text = "삭제";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TBLLIST.Rows.Add(TR);


            TBLLOAD();
        }

        private void TBLADD(string a, string name, string flag )
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
            TD.Text = name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 10;
            TD.Text = "<asp:button type='button' ID='Button2' class='btn btn-primary btn-middle' runat='server'><span class='glyphicon glyphicon-share' style='color:white;'></span> 수정</button>";
            TD.Attributes.Add("Onclick", "mail_modi('" + name.ToString() + "')");
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 40;
            if(flag == "1")
            {
                //TD.Text = "<a href='Mail_modi.aspx?no=" + serverip.ToString() + "' data-toggle='modal' data-target='#myModal'><button id='modi1' type='button' class='btn btn-primary btn-middle'    ><span class='glyphicon glyphicon-edit'></span> 수정</button> </a> ";
                TD.Text = "<asp:button type='button' ID='Button2' class='btn btn-success btn-middle' runat='server'><span class='glyphicon glyphicon-ok-circle' style='color:white;'></span> 사용</button>";
                TD.Attributes.Add("Onclick", "inactive('" + name.ToString() + "')");
            }
            else if(flag == "2")
            {
                //TD.Text = "<a href='Mail_modi.aspx?no=" + serverip.ToString() + "' data-toggle='modal' data-target='#myModal'><button id='modi1' type='button' class='btn btn-primary btn-middle'    ><span class='glyphicon glyphicon-edit'></span> 수정</button> </a> ";
                TD.Text = "<asp:button type='button' ID='Button2' class='btn btn- btn-middle' runat='server'><span class='glyphicon glyphicon-remove-circle'></span> 사용안함</button>";
                TD.Attributes.Add("Onclick", "active('" + name.ToString() + "')");
            }
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 60;
            TD.Text = "<asp:button type='button' class='btn btn- btn-middle'  runat='server'><span class='glyphicon glyphicon-remove-circle'></span> 삭제</button>";
            TD.Attributes["style"] = "text-align : center;  vertical-align:middle;";
            TD.Attributes.Add("Onclick", "go2('" + name.ToString() + "')");
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






            TBLLIST.Rows.Add(TR);

        }
      

        public void Button100_Click(object sender, EventArgs e)
        {


            try
            {
              

                    DB.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = DB;

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "insert into event_list ( name,flag) values(@name,'1')";
                    //cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = TextBox1.Text;
                    cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = TextBox2.Text;
                    //cmd.Parameters.Add("@serverid", MySqlDbType.VarChar, 100).Value = TextBox3.Text;
                    //cmd.Parameters.Add("@serverpwd", MySqlDbType.VarChar, 100).Value = TextBox4.Text;
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    cmd = null;

                    Response.Redirect("Event_list.aspx");
                

            }
            catch
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('해당서버와 통신이 되지 않습니다')", true);
            }



        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //DB.Open();

            //MySqlCommand cmd = new MySqlCommand();
            //cmd.Connection = DB;

            //cmd.CommandType = System.Data.CommandType.Text;
            //cmd.CommandText = "delete from service where no = @no";
            //cmd.Parameters.Add("@no", MySqlDbType.VarChar, 100).Value = HiddenField1.Value;

            //cmd.ExecuteNonQuery();

            //cmd.Dispose();
            //cmd = null;

            Response.Redirect("Event_list.aspx?serverip=" + HiddenField1.Value+"&category="+ HiddenField2.Value);
        }

      

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("Event_list.aspx?nowpage=" + 1 + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update event_list set flag ='1' where name = @name";
            cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;
            Response.Redirect("Event_list.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update event_list set flag ='2' where name = @name";
            cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;


            Response.Redirect("Event_list.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");

        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update service set mail_flag ='1' where serverip = @serverip";
            cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;
            Response.Redirect("Service.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update service set mail_flag ='2' where serverip = @serverip";
            cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;


            Response.Redirect("Service.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update service set category = @category where serverip = @serverip";
            cmd.Parameters.Add("@category", MySqlDbType.VarChar, 100).Value = HiddenField2.Value;
            cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;
            
            Response.Redirect("Service.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();
            MySqlCommand cmd2 = new MySqlCommand();
            cmd2.Connection = DB;
            cmd2.CommandType = System.Data.CommandType.Text;
            cmd2.CommandText = "delete from event_list where name = @name";
            cmd2.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = HiddenField3.Value;
            cmd2.ExecuteNonQuery();
            cmd2.Dispose();
            cmd2 = null;
            DB.Close();

            DB.Open();
            MySqlCommand cmd3 = new MySqlCommand();
            cmd3.Connection = DB;
            cmd3.CommandType = System.Data.CommandType.Text;
            cmd3.CommandText = "delete from event_mail_target where eventname = @name";
            cmd3.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = HiddenField3.Value;
            cmd3.ExecuteNonQuery();
            cmd3.Dispose();
            cmd3 = null;
            DB.Close();

            Response.Redirect("Event_list.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("Event_list.aspx?nowpage=" + 1 + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "&type2=" + DropDownList2.SelectedValue);
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            Response.Redirect("event_mail_modi.aspx?no="+ HiddenField1.Value );
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update service set dash_flag ='1' where serverip = @serverip";
            cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;
            Response.Redirect("Service.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update service set dash_flag ='2' where serverip = @serverip";
            cmd.Parameters.Add("@serverip", MySqlDbType.VarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;
            Response.Redirect("Service.aspx?nowpage=" + nowpage + "&search=" + Search.Text + "&type1=" + DropDownList1.SelectedValue + "");
        }
    }
}