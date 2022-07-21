using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class user : System.Web.UI.Page
    {
        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
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
            TD.Text = "NO";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "ID";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Name";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Duty";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Email";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 30;
            TD.Text = "Login Permission";
            TR.Cells.Add(TD);
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TBLLIST.Rows.Add(TR);

            TD = new TableHeaderCell();
            TD.Width = 30;
            TD.Text = "Modify";
            TR.Cells.Add(TD);
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TBLLIST.Rows.Add(TR);

            TD = new TableHeaderCell();
            TD.Width = 30;
            TD.Text = "Delete";
            TR.Cells.Add(TD);
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TBLLIST.Rows.Add(TR);


            TBLLOAD();
        }

        private void TBLLOAD()
        {
            SQL = "select * from user_ba order by no desc";

            SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD(row["no"].ToString(), row["id"].ToString(), row["pwd"].ToString(), row["name"].ToString(), row["duty"].ToString(), row["email"].ToString()
                    ,row["flag"].ToString());

            }
        }
        private void count()
        {
            SQL = "select count(*) as count from user_ba ";

            SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                Label1.Text = "총 수량 : " + row["count"].ToString();
            }
        }

        long a = 1;
        private void TBLADD(string NO , string id, string pwd, string name, string duty, string email, string flag)
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
            TD.Text = id.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = name.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 200;
            TD.Text = duty.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 200;
            TD.Text = email.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 40;
            if (flag == "1")
            {
                //TD.Text = "<a href='Mail_modi.aspx?no=" + serverip.ToString() + "' data-toggle='modal' data-target='#myModal'><button id='modi1' type='button' class='btn btn-primary btn-middle'    ><span class='glyphicon glyphicon-edit'></span> 수정</button> </a> ";
                TD.Text = "<asp:button type='button' ID='Button2' class='btn btn-success btn-middle' runat='server'><span class='glyphicon glyphicon-ok-circle' style='color:white;'></span> Active</button>";
                TD.Attributes.Add("Onclick", "inactive('" + NO.ToString() + "')");
            }
            else if (flag == "2")
            {
                //TD.Text = "<a href='Mail_modi.aspx?no=" + serverip.ToString() + "' data-toggle='modal' data-target='#myModal'><button id='modi1' type='button' class='btn btn-primary btn-middle'    ><span class='glyphicon glyphicon-edit'></span> 수정</button> </a> ";
                TD.Text = "<asp:button type='button' ID='Button2' class='btn btn- btn-middle' runat='server'><span class='glyphicon glyphicon-remove-circle'></span> InActive</button>";
                TD.Attributes.Add("Onclick", "active('" + NO.ToString() + "')");
            }
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 30;
            TD.Text = "<a href='user_modi1.aspx?no=" + NO.ToString() + "' data-toggle='modal' data-target='#myModal'><button id='modi1' type='button' class='btn btn-primary btn-middle'><span class='glyphicon glyphicon-edit' style='color:white;'></span> Modify</button> </a> ";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TD.Attributes.Add("Onclick", "modi(" + NO.ToString() + ")");

            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 30;
            TD.Text = "<asp:button type='button' class='btn btn- btn-middle'  runat='server'><span class='glyphicon glyphicon-remove-circle'></span> Delete</button>";
            TD.Attributes["style"] = "text-align : center;vertical-align:middle;";
            TD.Attributes.Add("Onclick", "go2('" + NO.ToString() + "')");
            TR.Cells.Add(TD);

            a++;
            TBLLIST.Rows.Add(TR);

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            DB.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "delete from user_ba where no = @no";
            cmd.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;

            Response.Redirect("user.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("user_modi.aspx?no="+ HiddenField1.Value );
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DB.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;


            if (TextBox2.Text == "")
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "insert into user_ba (id, pwd, name, duty, email) values(@id,null,@name,@duty,@email)";
                cmd.Parameters.Add("@id", SqlDbType.NVarChar, 100).Value = TextBox1.Text;
                cmd.Parameters.Add("@pwd", SqlDbType.NVarChar, 100).Value = TextBox2.Text;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = TextBox3.Text;
                cmd.Parameters.Add("@duty", SqlDbType.NVarChar, 100).Value = TextBox4.Text;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = TextBox6.Text;
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "insert into user_ba (id, pwd, name, duty, email) values(@id,@pwd,@name,@duty,@email)";
                cmd.Parameters.Add("@id", SqlDbType.NVarChar, 100).Value = TextBox1.Text;
                cmd.Parameters.Add("@pwd", SqlDbType.NVarChar, 100).Value = TextBox2.Text;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = TextBox3.Text;
                cmd.Parameters.Add("@duty", SqlDbType.NVarChar, 100).Value = TextBox4.Text;
                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = TextBox6.Text;
                cmd.ExecuteNonQuery();
            }
         

            cmd.Dispose();
            cmd = null;

            Response.Redirect("user.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update user_ba set flag ='1' where no = @no";
            cmd.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;
            Response.Redirect("user.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            int nowpage = Convert.ToInt32(Request["nowpage"]);
            DB.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update user_ba set flag ='2' where no = @no";
            cmd.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = HiddenField1.Value;

            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;


            Response.Redirect("user.aspx");

        }
    }
}