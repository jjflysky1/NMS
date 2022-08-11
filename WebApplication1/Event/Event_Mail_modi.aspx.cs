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
    public partial class Event_Mail_modi : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {


            HiddenField10.Value = Request["no"].ToString();
            //HiddenField10.Value = "192.168.0.11";
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
            TBLSET1();
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
            TD.Width = 50;
            TD.Text = "번호";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "아이디";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "이름";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "직책";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 60;
            //TD.Text = "연락처";
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "메일주소";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 30;
            TD.Text = "수정";
            TR.Cells.Add(TD);
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TBLLIST.Rows.Add(TR);




            TBLLOAD();
        }

        private void TBLSET1()
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
            TD.Width = 50;
            TD.Text = "번호";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "아이디";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "이름";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "직책";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            //TD = new TableHeaderCell();
            //TD.Width = 60;
            //TD.Text = "연락처";
            //TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "메일주소";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 30;
            TD.Text = "수정";
            TR.Cells.Add(TD);
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TBLLIST1.Rows.Add(TR);




            TBLLOAD1();
        }

        private void TBLLOAD()
        {
            SQL = "select distinct no,id,name,duty,email,phone  from  user_ba  order by no desc";

            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD(row["no"].ToString(), row["id"].ToString(), row["name"].ToString(), row["duty"].ToString(), row["email"].ToString() , row["phone"].ToString());

            }


        }

        private void TBLLOAD1()
        {

            SQL = "select distinct  b.no, id,pwd,b.name,duty,c.email,  c.Eventname, c.mailno , b.phone from  user_ba b, event_mail_target c " +
                "where c.Eventname = '" + HiddenField10.Value + "'   and b.no = c.mailno order by no desc";

            MySqlDataAdapter ADT1 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET1 = new DataSet();
            ADT1.Fill(DBSET1, "BD1");
            foreach (DataRow row in DBSET1.Tables["BD1"].Rows)
            {
                TBLADD1(row["no"].ToString(), row["id"].ToString(), row["pwd"].ToString(), row["name"].ToString(), row["duty"].ToString(), row["phone"].ToString()
                    , row["Eventname"].ToString(), row["email"].ToString());

            }



        }
        long a = 1;
        private void TBLADD(string NO,string id, string name, string duty, string email, string phone)
        {
            TableRow TR;
            TableCell TD;



            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 40;
            TD.Text = a.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            TD = new TableCell();
            TD.Width = 40;
            TD.Text = id.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 200;
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
            TD.Text = "<asp:button type='button' ID='button2' class='btn btn-success btn-middle' runat='server' ><span class='glyphicon glyphicon-ok-circle'   style='color:white;'></span> 등록</button>";
            //TD.Text = "<asp:button type='button' ID='Button2' class='btn btn-danger btn-middle' runat='server'><span class='glyphicon glyphicon-remove-circle'></span> InActive</button>";
            TD.Attributes.Add("Onclick", "mail_go('" + NO.ToString() + "','" + email.ToString() + "','" + phone.ToString() +"')");
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            Label2.Text = HiddenField10.Value + " 이벤트 메일 등록";

            a++;
            TBLLIST.Rows.Add(TR);

        }
        long b = 1;
        private void TBLADD1(string NO, string id, string pwd, string name, string duty, string phone, string serverip,  string email)
        {
            TableRow TR;
            TableCell TD;



            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 40;
            TD.Text = b.ToString();
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
            //TD.Text = "<input type='checkbox' class='form-check-input' id='exampleCheck1'>";
            TD.Text = "<asp:button type='button' ID='Button2' class='btn btn-danger btn-middle' runat='server' OnClick='<%=Button5_Click%>'><span class='glyphicon glyphicon-ok-circle'  style='color:white;'></span> 삭제</button>";
            //TD.Text = "<asp:button type='button' ID='Button2' class='btn btn-danger btn-middle' runat='server'><span class='glyphicon glyphicon-remove-circle'></span> InActive</button>";
            TD.Attributes.Add("Onclick", "mail_down('" + NO.ToString() + "')");
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);



            b++;
            TBLLIST1.Rows.Add(TR);

        }
        protected void Button20_Click(object sender, EventArgs e)
        {
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into event_mail_target(eventname,mailno,email,phone) values(@eventname, @mailno ,@email, @phone)";
            cmd.Parameters.Add("@eventname", MySqlDbType.VarChar, 100).Value = HiddenField10.Value;
            cmd.Parameters.Add("@mailno", MySqlDbType.VarChar, 100).Value = HiddenField20.Value;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar, 100).Value = HiddenField30.Value;
            cmd.Parameters.Add("@phone", MySqlDbType.VarChar, 100).Value = HiddenField40.Value;
            cmd.ExecuteNonQuery();

            DB.Close();
            cmd.Dispose();
            cmd = null;

            Response.Redirect(Request.RawUrl);
        }

        protected void Button21_Click(object sender, EventArgs e)
        {
            DB.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "delete from event_mail_target where mailno = @mailno and eventname = @name";
            cmd.Parameters.Add("@name", MySqlDbType.VarChar, 100).Value = HiddenField10.Value;
            cmd.Parameters.Add("@mailno", MySqlDbType.VarChar, 100).Value = HiddenField20.Value;
            cmd.ExecuteNonQuery();
            DB.Close();
            cmd.Dispose();
            cmd = null;
            Response.Redirect(Request.RawUrl);
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("Event_list.aspx");
        }



    }
}