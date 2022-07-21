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
    public partial class WebForm3 : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true)
            {
                NAMEHF.Value = name.Value;
                SERVERIPHF.Value = serverip.Value;
                SERVERIDHF.Value = serverid.Value;
                SERVERPWDHF.Value = serverpwd.Value;
                SSHPORTHF.Value = SSHPORT.Value;
                COMMUNITYHF.Value = Community.Value;
            }

            HiddenField1.Value = Request["no"].ToString();
            TBLLOAD();
            if (Request.Cookies["userinfo"] == null)
            {
                Label3.Text = "<script>alert('로그인 해주세요');</script>";
                Response.Redirect("/Default.aspx");
            }
        }

        private void TBLLOAD()
        {
            SQL = "select * from service where no = " + HiddenField1.Value;
            //SQL = "select * from service where no = 38";
            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                name.Value = row["name"].ToString();
                serverip.Value = row["serverip"].ToString();
                serverid.Value = row["serverid"].ToString();
                serverpwd.Value = row["serverpwd"].ToString();
                SSHPORT.Value = row["sshport"].ToString();
                Community.Value = row["Community"].ToString();
                //TBLADD(row["no"].ToString(), row["id"].ToString(), row["pwd"].ToString(), row["name"].ToString(), row["duty"].ToString());

            }
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            try
            {
                DB.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = DB;
                cmd.CommandType = System.Data.CommandType.Text;
                if (serverpwd.Value == "")
                {
                    cmd.CommandText = "update service set name = @NAME , serverid = @SERVERID,   serverpwd = @serverpwd , sshport = @sshport " +
                        " , Community = @Community where no = @no";
                 
                    cmd.Parameters.Add("@NAME", MySqlDbType.VarChar, 50).Value = NAMEHF.Value;
                   // cmd.Parameters.Add("@SERVERIP", MySqlDbType.VarChar, 50).Value = SERVERIPHF.Value;
                    cmd.Parameters.Add("@SERVERID", MySqlDbType.VarChar, 50).Value = SERVERIDHF.Value;
                    cmd.Parameters.Add("@SERVERPWD", MySqlDbType.VarChar, 50).Value = SERVERPWDHF.Value;
                    cmd.Parameters.Add("@SSHPORT", MySqlDbType.VarChar, 50).Value = SSHPORTHF.Value;
                    cmd.Parameters.Add("@Community", MySqlDbType.VarChar, 50).Value = COMMUNITYHF.Value;
                    cmd.Parameters.Add("@no", MySqlDbType.VarChar, 50).Value = HiddenField1.Value;
                }
                else
                {
                    cmd.CommandText = "update service set name = @NAME , serverid = @SERVERID,   oripwd = serverpwd  , newpwd = @SERVERPWD , sshport = @sshport" +
                        ", Community = @Community where no = @no";
                    cmd.Parameters.Add("@NAME", MySqlDbType.VarChar, 50).Value = NAMEHF.Value;
                    //cmd.Parameters.Add("@SERVERIP", MySqlDbType.VarChar, 50).Value = SERVERIPHF.Value;
                    cmd.Parameters.Add("@SERVERID", MySqlDbType.VarChar, 50).Value = SERVERIDHF.Value;
                    cmd.Parameters.Add("@SERVERPWD", MySqlDbType.VarChar, 50).Value = SERVERPWDHF.Value;
                    cmd.Parameters.Add("@SSHPORT", MySqlDbType.VarChar, 50).Value = SSHPORTHF.Value;
                    cmd.Parameters.Add("@Community", MySqlDbType.VarChar, 50).Value = COMMUNITYHF.Value;
                    cmd.Parameters.Add("@no", MySqlDbType.VarChar, 50).Value = HiddenField1.Value;
                }

        
               

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                cmd = null;
                DB.Close();

                Response.Redirect("Service_list.aspx?serverip=" + SERVERIPHF.Value + "&category=" + Request["category"]);
            }
            catch
            {

            }
        }
    }
}