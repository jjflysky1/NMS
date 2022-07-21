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
    public partial class Network_Setting : System.Web.UI.Page
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
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 20;
            TD.Text = "번호";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "대역대 이름";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "시작 아이피";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "종료 아이피";
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
            SQL = "select * from service_range order by no desc";

            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD(row["no"].ToString(), row["name"].ToString(), row["startip"].ToString(), row["endip"].ToString());

            }
        }
        private void count()
        {
            SQL = "select count(*) as count from service_range ";

            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                Label1.Text = "총 수량 : " + row["count"].ToString();
            }
        }

        long a = 1;
        private void TBLADD(string NO , string name, string startip, string endip)
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
            TD.Width = 60;
            TD.Text = name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle; ";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = startip.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 200;
            TD.Text = endip.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 30;
            TD.Text = "<a href='Network_Setting_modi.aspx?no=" + NO.ToString() + "' data-toggle='modal' data-target='#myModal'><button id='modi1' type='button' class='btn btn-primary btn-middle'><span class='glyphicon glyphicon-edit' style='color:white;'></span> 수정</button> </a> ";
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
            MySqlCommand cmd2 = new MySqlCommand("Service_delete", DB);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@number", HiddenField1.Value);
            cmd2.ExecuteNonQuery();

            cmd2.Dispose();
            cmd2 = null;

            DB.Close();


            Response.Redirect("Network_Setting.aspx");
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
            cmd.CommandText = "insert into service_range (name, startip, endip) values(@name,@startip,@endip)";
            cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = TextBox1.Text;
            cmd.Parameters.Add("@startip", MySqlDbType.VarChar, 100).Value = TextBox2.Text;
            cmd.Parameters.Add("@endip", MySqlDbType.VarChar, 100).Value = TextBox3.Text;
         
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;

            Response.Redirect("Network_Setting.aspx");
        }
    }
}