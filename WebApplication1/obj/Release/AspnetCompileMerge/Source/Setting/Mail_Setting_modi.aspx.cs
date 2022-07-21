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
    public partial class Mail_Setting_modi : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack == true)
            {
                PHONEHF.Value = phone.Value;
                CPUHF.Value = cpulimit.Value;
                MEMORYHF.Value = memorylimit.Value;
                TRAFFICHF.Value = trafficlimit.Value;
                MAILSENDER.Value = mail_sender.Value;
            }
            HiddenField2.Value = Request["no"].ToString();

            //HiddenField2.Value = "1";
            TBLLOAD();
        }

        private void TBLLOAD()
        {
            SQL = "select no, mailip, phone, flag, cpulimit, memorylimit, (trafficlimit/1024/1024) AS trafficlimit, mail_sender from mail_info where no = " + HiddenField2.Value;

            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {

                phone.Value = row["phone"].ToString();
                cpulimit.Value = row["cpulimit"].ToString();
                memorylimit.Value = row["memorylimit"].ToString();
                trafficlimit.Value = row["trafficlimit"].ToString();
                mail_sender.Value = row["mail_sender"].ToString();
                //TBLADD(row["no"].ToString(), row["id"].ToString(), row["pwd"].ToString(), row["name"].ToString(), row["duty"].ToString());

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
          
                try
                {
                    DB.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = DB;

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "update mail_info set phone = @phone, mail_sender = @mail_sender ,cpulimit = @cpu, memorylimit = @memory, trafficlimit = @traffic where no = @no";
                    cmd.Parameters.Add("@phone", MySqlDbType.VarChar, 50).Value = PHONEHF.Value;
                    cmd.Parameters.Add("@mail_sender", MySqlDbType.VarChar, 50).Value = MAILSENDER.Value;
                    cmd.Parameters.Add("@cpu", MySqlDbType.VarChar, 50).Value = CPUHF.Value;
                    cmd.Parameters.Add("@memory", MySqlDbType.VarChar, 50).Value = MEMORYHF.Value;
                    cmd.Parameters.Add("@traffic", MySqlDbType.LongText, 255).Value = (Convert.ToDouble(TRAFFICHF.Value) * 1024 * 1024);
                    cmd.Parameters.Add("@no", MySqlDbType.VarChar, 50).Value = HiddenField2.Value;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cmd = null;
                    DB.Close();

                    Response.Redirect("mail_setting.aspx");
                }
                catch
                {

                }
          
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            Response.Redirect("mail_setting.aspx");
        }
    }
}