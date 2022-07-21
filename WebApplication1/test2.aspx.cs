using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class test2 : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            javascript2();
            //javascript3();
        }
        /// <summary>
        /// 네트워크/보안
        /// </summary>
        private void javascript2()
        {
            string SQL = "select  cpu,memory,serverip, category, now() as getdate,status , left(os,30) as os from service where memory is not null and flag = '1' and Category like '%네트워크%'  ORDER BY INET_ATON(serverip) LIMIT 3 ";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            string cpu, memory, serverip,  category, status, os = "";
            int i = 100;
            int rote = 1;
            int j = 1;

            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                //Label1.Text = row1["getdate"].ToString() + " ";
                cpu = row1["cpu"].ToString();
                memory = row1["memory"].ToString();
                serverip = row1["serverip"].ToString() + ",";
                category = row1["category"].ToString();
                status = row1["status"].ToString();
                os = row1["os"].ToString();




                //라벨 추가
                Label label = new Label();
                label.ID = "Label" + i;
                string roatetion = "";


                System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                createDiv.ID = "div" + j;
                createDiv.Attributes.Add("class", "div"+j);
                this.Controls.Add(createDiv);
                
                //this.Controls.Add(label);
                StringBuilder st = new StringBuilder();
                if (i == 100)
                {

                }
                st.Append("<div style='width:100px; height:80px; text-align:center;'>");
                if (status.Contains("Disconnect") == true)
                {
                    st.Append("<img src='ap_error.png' width='100' height='80'>");
                }
                else
                {
                    st.Append("<img src='ap.png' width='100' height='80'>");
                }
                st.Append("<font size='1'>" + row1["serverip"].ToString() + "</font>");
                st.Append("</div>");
                label.Text = st.ToString();
                createDiv.InnerHtml = label.Text;
                st = null;
                i++;
                j++;
            }
        }
        private void javascript3()
        {
            string SQL = "select  cpu,memory,serverip, category, now() as getdate,status , left(os,30) as os from service where memory is not null and flag = '1' and Category like '%네트워크%'  ORDER BY INET_ATON(serverip) LIMIT 1 ";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            string cpu, memory, serverip, category, status, os = "";
            int i = 100;
            int rote = 1;


            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                //Label1.Text = row1["getdate"].ToString() + " ";
                cpu = row1["cpu"].ToString();
                memory = row1["memory"].ToString();
                serverip = row1["serverip"].ToString();
                category = row1["category"].ToString();
                status = row1["status"].ToString();
                os = row1["os"].ToString();




                //라벨 추가
                Label label = new Label();
                label.ID = "Label" + i;
                string roatetion = "";

                //div2.Controls.Add(label);
                StringBuilder st = new StringBuilder();
                if (i == 100)
                {

                }
                st.Append("<div style='width:130px; height:110px; float:left;  margin-right:0%; '>");
                if (status.Contains("Disconnect") == true)
                {
                    st.Append("<img src='switch_error.png' width='100' height='80'>");
                }
                else
                {
                    st.Append("<img src='ap.png' width='100' height='80'>");
                }
                st.Append("<font size='2'>" + row1["serverip"].ToString() + "</font>");
                st.Append("</div>");
                label.Text = st.ToString();

                st = null;
                i++;
            }
        }
    }
}