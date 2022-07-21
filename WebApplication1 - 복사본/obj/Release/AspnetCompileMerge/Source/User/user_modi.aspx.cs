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
    public partial class user_modi : System.Web.UI.Page
    {
        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack == true)
            {
                IDHF.Value = id.Value;
                NAMEHF.Value = name.Value;
                DUTYHF.Value = duty.Value;
                PASSWORDHF.Value = password.Value;
                CONFIRMHF.Value = confirm.Value;
            }
            HiddenField2.Value = Request["no"].ToString();

            //HiddenField2.Value = "1";
            TBLLOAD();
        }

        private void TBLLOAD()
        {
            SQL = "select * from user_ba where no = " + HiddenField2.Value;

            SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                id.Value = row["id"].ToString();
                name.Value = row["name"].ToString();
                duty.Value = row["duty"].ToString();
                password.Value = row["pwd"].ToString();
                //TBLADD(row["no"].ToString(), row["id"].ToString(), row["pwd"].ToString(), row["name"].ToString(), row["duty"].ToString());

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            if(PASSWORDHF.Value == CONFIRMHF.Value && PASSWORDHF.Value != "" && CONFIRMHF.Value != "")
            { 
                try
                {
                    DB.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = DB;

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "update user_ba set id = @id ,pwd = @pwd, name = @name,duty = @duty where no = @no";
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar, 50).Value = IDHF.Value;
                    cmd.Parameters.Add("@pwd", SqlDbType.NVarChar, 50).Value = PASSWORDHF.Value;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = NAMEHF.Value;
                    cmd.Parameters.Add("@duty", SqlDbType.NVarChar, 50).Value = DUTYHF.Value;
                    cmd.Parameters.Add("@no", SqlDbType.NVarChar, 50).Value = HiddenField2.Value;

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cmd = null;
                    DB.Close();

                    Response.Redirect("user.aspx");
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    DB.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = DB;

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "update user_ba set id = @id , name = @name,duty = @duty where no = @no";
                    cmd.Parameters.Add("@id", SqlDbType.NVarChar, 50).Value = IDHF.Value;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = NAMEHF.Value;
                    cmd.Parameters.Add("@duty", SqlDbType.NVarChar, 50).Value = DUTYHF.Value;
                    cmd.Parameters.Add("@no", SqlDbType.NVarChar, 50).Value = HiddenField2.Value;

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cmd = null;
                    DB.Close();

                    Response.Redirect("user.aspx");
                }
                catch
                {

                }
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('비밀번호를 확인해주세요')", true);
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            Response.Redirect("user.aspx");
        }
    }
}