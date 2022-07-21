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
    public partial class Mail_Setting_modi : System.Web.UI.Page
    {
        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack == true)
            {
                MAILIPHF.Value = mailip.Value;
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
            SQL = "select * from mail_info where no = " + HiddenField2.Value;

            SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                
                mailip.Value = row["mailip"].ToString();
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

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = DB;

                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "update mail_info set mailip = @mailip, mail_sender = @mail_sender ,cpulimit = @cpu, memorylimit = @memory, trafficlimit = @traffic where no = @no";
                    cmd.Parameters.Add("@mailip", SqlDbType.NVarChar, 50).Value = MAILIPHF.Value;
                    cmd.Parameters.Add("@mail_sender", SqlDbType.NVarChar, 50).Value = MAILSENDER.Value;
                    cmd.Parameters.Add("@cpu", SqlDbType.NVarChar, 50).Value = CPUHF.Value;
                    cmd.Parameters.Add("@memory", SqlDbType.NVarChar, 50).Value = MEMORYHF.Value;
                    cmd.Parameters.Add("@traffic", SqlDbType.NVarChar, 50).Value = TRAFFICHF.Value;
                    cmd.Parameters.Add("@no", SqlDbType.NVarChar, 50).Value = HiddenField2.Value;
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