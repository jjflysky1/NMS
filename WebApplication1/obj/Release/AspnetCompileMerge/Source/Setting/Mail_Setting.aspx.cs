using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class Mail_Setting : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["userinfo"] == null)
            {
                Label3.Text = "<script>alert('로그인 해주세요');</script>";
                Response.Redirect("/Default.aspx");
            }
            TextBox5.Value = Request["id"];

            UISET();
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
            TD.Text = "번호";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 80;
            TD.Text = "메일서버주소";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 30;
            TD.Text = "받는 사람";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "CPU 기준";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 80;
            TD.Text = "Memory 기준";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "트래픽 기준(MB/S)";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);



            TD = new TableHeaderCell();
            TD.Width = 30;
            TD.Text = "상태";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 30;
            TD.Text = "수정";
            TR.Cells.Add(TD);
            TD.Attributes["style"] = "text-align : center";
            TBLLIST.Rows.Add(TR);

            TD = new TableHeaderCell();
            TD.Width = 30;
            TD.Text = "삭제";
            TR.Cells.Add(TD);
            TD.Attributes["style"] = "text-align : center";
            TBLLIST.Rows.Add(TR);


            TBLLOAD();
        }

        private void TBLLOAD()
        {
            SQL = "select no, mailip, phone, flag, cpulimit, memorylimit, (trafficlimit/1024/1024) AS trafficlimit, mail_sender from mail_info order by no desc";

            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD(row["no"].ToString(), row["mailip"].ToString(),  row["phone"].ToString(), row["flag"].ToString(),row["cpulimit"].ToString()
                    ,row["memorylimit"].ToString(),row["trafficlimit"].ToString(),row["mail_sender"].ToString());

            }
        }
        private void count()
        {
            SQL = "select count(*) as count from mail_info ";

            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                Label1.Text = "총 수량 : " + row["count"].ToString();
            }
        }

        long a = 1;
        private void TBLADD(string NO , string mailip,  string phone, string flag, string cpulimit, string memorylimit, string trafficlimit, string mail_sender)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 20;
            TD.Text = a.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 80;
            TD.Text = mailip.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = mail_sender.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = cpulimit.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = memorylimit.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            
            TD = new TableCell();
            TD.Width = 60;
            TD.Text = trafficlimit.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 60;
            if (flag == "1")
            {
                TD.Text = "<asp:button type='button' ID='Button2' class='btn btn-success btn-middle' runat='server'><span class='glyphicon glyphicon-ok-circle' style='color:white;'></span> 사용</button>";
                TD.Attributes.Add("Onclick", "inactive('" + NO.ToString() + "')");
            }
            else if (flag == "2")
            {
                TD.Text = "<asp:button type='button' ID='Button2' class='btn btn-danger btn-middle' runat='server'><span class='glyphicon glyphicon-remove-circle'></span> 사용안함</button>";
                TD.Attributes.Add("Onclick", "active('" + NO.ToString() + "')");
            }
            TD.Attributes["style"] = "text-align : center; ";
            TR.Cells.Add(TD);



            TD = new TableCell();
            TD.Width = 30;
            TD.Text = "<a href='Mail_Setting_modi.aspx?no=" + NO.ToString() + "' data-toggle='modal' data-target='#myModal'><button id='modi1' type='button' class='btn btn-primary btn-middle'><span class='glyphicon glyphicon-edit' style='color:white;'></span> 수정</button> </a> ";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TD.Attributes.Add("Onclick", "modi(" + NO.ToString() + ")");

            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 30;
            TD.Text = "<asp:button type='button' class='btn btn- btn-middle'  runat='server'><span class='glyphicon glyphicon-remove-circle'></span> 삭제</button>";
            TD.Attributes["style"] = "text-align : center;";
            TD.Attributes.Add("Onclick", "go(" + NO.ToString() + ")");
            TR.Cells.Add(TD);

            a++;
            TBLLIST.Rows.Add(TR);

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "delete from mail_info where no = @no";
            cmd.Parameters.Add("@no", MySqlDbType.VarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;

            Response.Redirect("mail_setting.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("user_modi.aspx?no="+ HiddenField1.Value );
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into mail_info (mailip, cpulimit, memorylimit, trafficlimit) values(@mailip,@cpulimit,@memorylimit,@trafficlimit)";
            cmd.Parameters.Add("@mailip", MySqlDbType.VarChar, 100).Value = TextBox1.Text;
            cmd.Parameters.Add("@cpulimit", MySqlDbType.VarChar, 100).Value = TextBox2.Text;
            cmd.Parameters.Add("@memorylimit", MySqlDbType.VarChar, 100).Value = TextBox3.Text;
            cmd.Parameters.Add("@trafficlimit", MySqlDbType.VarChar, 100).Value = (Convert.ToInt32(TextBox4.Text) * 1024 * 1024);
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;

            Response.Redirect("mail_setting.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {

            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update mail_info set flag ='1' where no = @no";
            cmd.Parameters.Add("@no", MySqlDbType.VarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;
            Response.Redirect("mail_setting.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update mail_info set flag ='2' where no = @no";
            cmd.Parameters.Add("@no", MySqlDbType.VarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;


            Response.Redirect("mail_setting.aspx");
        }
    }
}