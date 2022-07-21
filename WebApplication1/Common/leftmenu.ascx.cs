using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.Common
{
    public partial class leftmenu : System.Web.UI.UserControl
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Request.Cookies["ID"].Value == Request["id"])
                {
                    try
                    {
                        Label111.Text = Request.Cookies["ID"].Value;
                    }
                    catch
                    {
                        Response.Redirect("/Default.aspx");
                    }
                }
                else
                {
                    Response.Redirect("/Default.aspx");
                }
            }
            catch
            {
                Response.Redirect("/Default.aspx");
            }

            
         

        }

     
      

        protected void Unnamed_Tick(object sender, EventArgs e)
        {
         
            DBCON.Class1 DBCON = new DBCON.Class1();

            string SQL10 = "select  date_format(now(),'%Y%m%d') as time";
            MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL10, DB);
            DataSet DBSET2 = new DataSet();
            ADT2.Fill(DBSET2, "BD2");
            string time = "";
            foreach (DataRow row2 in DBSET2.Tables["BD2"].Rows)
            {
                time = row2["time"].ToString();
            }
            if (Convert.ToInt32(time) > Convert.ToInt32(DBCON.date))
            {
                //Response.Redirect("/Default.aspx");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('라이센스가 만료 되었습니다')", true);
            }
            
        }
    }

  
}