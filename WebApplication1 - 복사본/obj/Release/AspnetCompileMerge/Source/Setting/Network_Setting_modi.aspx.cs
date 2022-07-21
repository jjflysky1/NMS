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
    public partial class Network_Setting_modi : System.Web.UI.Page
    {
        private SqlConnection DB = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack == true)
            {
                NAMEHF.Value = name.Value;
                STARTIPHF.Value = startip.Value;
                ENDIPHF.Value = endip.Value;
              
            }
            HiddenField2.Value = Request["no"].ToString();

            //HiddenField2.Value = "1";
            TBLLOAD();
        }

        private void TBLLOAD()
        {
            SQL = "select * from service_range where no = " + HiddenField2.Value;

            SqlDataAdapter ADT = new SqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                
                name.Value = row["name"].ToString();
                startip.Value = row["startip"].ToString();
                endip.Value = row["endip"].ToString();
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
                    cmd.CommandText = "update service_range set name = @name ,startip = @startip, endip = @endip where no = @no";
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = NAMEHF.Value;
                    cmd.Parameters.Add("@startip", SqlDbType.NVarChar, 50).Value = STARTIPHF.Value;
                    cmd.Parameters.Add("@endip", SqlDbType.NVarChar, 50).Value = ENDIPHF.Value;
                    cmd.Parameters.Add("@no", SqlDbType.NVarChar, 50).Value = HiddenField2.Value;
                cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cmd = null;
                    DB.Close();

                    Response.Redirect("Network_Setting.aspx");
                }
                catch
                {

                }
          
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            Response.Redirect("Network_Setting.aspx");
        }
    }
}