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
    public partial class Service_modi_all : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == true)
            {
                //NAMEHF.Value = name.Value;
                SERVERIPHF.Value = serverip.Value;
                SERVERIDHF.Value = serverid.Value;
                SERVERPWDHF.Value = serverpwd.Value;

            }
            HiddenField2.Value = Request["no"].ToString();

            //HiddenField2.Value = "1";
            TBLLOAD();
        }

        private void TBLLOAD()
        {
            SQL = "select * from service where no = " + HiddenField2.Value;

            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                //name.Value = row["name"].ToString();
                serverip.Value = row["serverip"].ToString();
                serverid.Value = row["serverid"].ToString();
                serverpwd.Value = row["serverpwd"].ToString();
                //TBLADD(row["no"].ToString(), row["id"].ToString(), row["pwd"].ToString(), row["name"].ToString(), row["duty"].ToString());

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            try
            {
                DB.Open();

               
               

                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = "update service set serverip = @SERVERIP, serverid = @SERVERID, serverpwd = @SERVERPWD where ServerIP = @targetserverip ";
                //cmd.Parameters.Add("@SERVERIP", MySqlDbType.VarChar, 50).Value = SERVERIPHF.Value;
                //cmd.Parameters.Add("@SERVERID", MySqlDbType.VarChar, 50).Value = SERVERIDHF.Value;
                //cmd.Parameters.Add("@SERVERPWD", MySqlDbType.VarChar, 50).Value = SERVERPWDHF.Value;
                //cmd.Parameters.Add("@targetserverip", MySqlDbType.VarChar, 50).Value = serverip.Value;


                if (serverpwd.Value == "")
                {
                    MySqlCommand cmd = new MySqlCommand("Service_modi_all", DB);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SERVERIP", SERVERIPHF.Value);
                    cmd.Parameters.AddWithValue("@SERVERID", SERVERIDHF.Value);
                    cmd.Parameters.AddWithValue("@SERVERPWD", SERVERPWDHF.Value);
                    cmd.Parameters.AddWithValue("@targetserverip", serverip.Value);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cmd = null;


                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("Service_modi_all_pwd", DB);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SERVERIP", SERVERIPHF.Value);
                    cmd.Parameters.AddWithValue("@SERVERID", SERVERIDHF.Value);
                    cmd.Parameters.AddWithValue("@SERVERPWD", SERVERPWDHF.Value);
                    cmd.Parameters.AddWithValue("@targetserverip", serverip.Value);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cmd = null;


                }

                //cmd.ExecuteNonQuery();
                //cmd.Dispose();
                //cmd = null;
              
                DB.Close();

                Response.Redirect("Service_list.aspx?serverip=" + SERVERIPHF.Value);
            }
            catch
            {

            }

            //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('비밀번호를 확인해주세요')", true);
        }

      
    }
}

