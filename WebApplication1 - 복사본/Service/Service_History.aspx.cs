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
    public partial class Service_History : System.Web.UI.Page
    {
        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true)
            {
                TITLEHF.Value = title.Value;
                BODYHF.Value = body.Text;
              
            }

            HiddenField1.Value = Request["no"].ToString();
            HiddenField2.Value = Request["serverip"].ToString();
            HiddenField3.Value = Request["flag"].ToString();
            TBLLOAD();
            if (Request.Cookies["userinfo"] == null)
            {
                Label3.Text = "<script>alert('로그인 해주세요');</script>";
                Response.Redirect("/Default.aspx");
            }
        }

        private void TBLLOAD()
        {
            if (HiddenField3.Value == "1")
            {

                SQL = "select * from server_history where no = " + HiddenField1.Value;
                //SQL = "select * from service where no = 38";
                SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
                DataSet DBSET = new DataSet();
                ADT.Fill(DBSET, "BD");
                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                {
                    title.Value = row["title"].ToString();
                    body.Text = row["body"].ToString();
                    //TBLADD(row["no"].ToString(), row["id"].ToString(), row["pwd"].ToString(), row["name"].ToString(), row["duty"].ToString());

                }
            }
          
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            try
            {
                if(HiddenField3.Value == "0")
                {
                    DB.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = DB;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "insert into server_history (serverip, title, body, time) values(@serverip,@title,@body,getdate()) ";
                    cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 50).Value = HiddenField2.Value;
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar, 50).Value = TITLEHF.Value;
                    cmd.Parameters.Add("@body", SqlDbType.NVarChar, 50).Value = BODYHF.Value;
                    cmd.Parameters.Add("@no", SqlDbType.NVarChar, 50).Value = HiddenField1.Value;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cmd = null;
                    DB.Close();
                    Response.Redirect("Service_list.aspx?serverip=" + HiddenField2.Value + "&category=네트워크");
                }

                if (HiddenField3.Value == "1")
                {
                    DB.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = DB;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "update server_history set title = @title , body = @body where no = @no";
                    cmd.Parameters.Add("@serverip", SqlDbType.NVarChar, 50).Value = HiddenField2.Value;
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar, 50).Value = TITLEHF.Value;
                    cmd.Parameters.Add("@body", SqlDbType.NVarChar, 50).Value = BODYHF.Value;
                    cmd.Parameters.Add("@no", SqlDbType.NVarChar, 50).Value = HiddenField1.Value;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cmd = null;
                    DB.Close();
                    Response.Redirect("Service_list.aspx?serverip=" + HiddenField2.Value + "&category=네트워크");
                }

            }
            catch
            {

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DB.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = DB;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "delete from server_history where no = @no";
            cmd.Parameters.Add("@no", SqlDbType.NVarChar, 50).Value = HiddenField1.Value;
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = null;
            DB.Close();
            Response.Redirect("Service_list.aspx?serverip=" + HiddenField2.Value +"&category=네트워크");
        }
    }
}