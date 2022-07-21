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
    public partial class Network_Setting : System.Web.UI.Page
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
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Network Name";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "Start IP";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "End IP";
            TD.Attributes["style"] = "text-align : center";
            TR.Cells.Add(TD);


            TD = new TableHeaderCell();
            TD.Width = 30;
            TD.Text = "Modify";
            TR.Cells.Add(TD);
            TD.Attributes["style"] = "text-align : center";
            TBLLIST.Rows.Add(TR);

            TD = new TableHeaderCell();
            TD.Width = 30;
            TD.Text = "Delete";
            TR.Cells.Add(TD);
            TD.Attributes["style"] = "text-align : center";
            TBLLIST.Rows.Add(TR);


            TBLLOAD();
        }

        private void TBLLOAD()
        {
            SQL = "select * from service_range order by no desc";

            SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
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

            SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
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
            TD.Text = "<a href='Network_Setting_modi.aspx?no=" + NO.ToString() + "' data-toggle='modal' data-target='#myModal'><button id='modi1' type='button' class='btn btn-primary btn-middle'><span class='glyphicon glyphicon-edit' style='color:white;'></span> Modify</button> </a> ";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TD.Attributes.Add("Onclick", "modi(" + NO.ToString() + ")");

            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 30;
            TD.Text = "<asp:button type='button' class='btn btn- btn-middle'  runat='server'><span class='glyphicon glyphicon-remove-circle'></span> Delete</button>";
            TD.Attributes["style"] = "text-align : center;";
            TD.Attributes.Add("Onclick", "go(" + NO.ToString() + ")");
            TR.Cells.Add(TD);

            a++;
            TBLLIST.Rows.Add(TR);

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            DB.Open();



            SqlCommand cmd2 = new SqlCommand("Service_delete", DB);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@no", HiddenField1.Value);
            cmd2.ExecuteNonQuery();

            cmd2.Dispose();
            cmd2 = null;


            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = DB;

            //cmd.CommandType = System.Data.CommandType.Text;
            //cmd.CommandText = "delete from service_range where no = @no";
            //cmd.Parameters.Add("@no", SqlDbType.NVarChar, 100).Value = HiddenField1.Value;

            //cmd.ExecuteNonQuery();

            //cmd.Dispose();
            //cmd = null;


         

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

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into service_range (name, startip, endip) values(@name,@startip,@endip)";
            cmd.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = TextBox1.Text;
            cmd.Parameters.Add("@startip", SqlDbType.NVarChar, 100).Value = TextBox2.Text;
            cmd.Parameters.Add("@endip", SqlDbType.NVarChar, 100).Value = TextBox3.Text;
         
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            cmd = null;

            Response.Redirect("Network_Setting.aspx");
        }
    }
}