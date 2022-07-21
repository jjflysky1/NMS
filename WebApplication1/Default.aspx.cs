using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class Default : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        private String SQL = "";


        protected void Page_Load(object sender, EventArgs e)
        {
           
        }


        protected void LOGIN(object sender, EventArgs e)
        {
            HttpCookie COOKIE = new HttpCookie("userinfo");
            COOKIE["ID"] = name.Value;
            COOKIE["SetDate"] = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss");
            COOKIE.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(COOKIE);
            Response.Cookies["ID"].Value = name.Value;
            LOGIN_DB();



        }

        protected void LOGIN_DB()
        {

            SQL = "select * from user_ba where id = @id and pwd = @pwd and flag = 1";

            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.SelectCommand.Parameters.Add("@id", MySqlDbType.VarChar, 50).Value = name.Value;
            ADT.SelectCommand.Parameters.Add("@pwd", MySqlDbType.VarChar, 50).Value = password.Value;

            try
            {
                ADT.Fill(DBSET);
                if (DBSET.Tables[0].Rows.Count > 0)
                {
                    HttpRequest currentRequest = HttpContext.Current.Request;
                  
                    string ip = currentRequest.ServerVariables["REMOTE_ADDR"];



                    DB.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = DB;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "insert into login_log_list (Clientip, id, time) values(@Clientip, @id, now())";
                    cmd.Parameters.Add("@id", MySqlDbType.VarChar, 100).Value = name.Value;
                    cmd.Parameters.Add("@Clientip", MySqlDbType.VarChar, 100).Value = ip;
                    cmd.ExecuteNonQuery();
                    DB.Close();
                    cmd.Dispose();
                    cmd = null;

                    Label1.Text = "<script>alert('환영합니다.');</script>";
                    Response.Redirect("main5.aspx?id=" + name.Value);
                }
                else
                {
                    this.Label1.Text = "<script>alert('사용자 계정을 확인해 주세요');</script>";
                }
                DBSET.Clear();
            }
            catch (Exception EX)
            {
                this.Label1.Text = EX.ToString();
            }
            DBSET.Dispose();
            ADT.Dispose();

            DBSET = null;
            ADT = null;


        

            return;

        }
    }
}