using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class maindounut : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            javascript();
            javascript2();
        }

        /// <summary>
        /// 
        /// 메모리
        /// </summary>
        private void javascript()
        {
            string SQL = "select  cpu,memory,serverip,now() as getdate, category, status from service where memory is not null and flag = '1' order by " +
                "CAST(PARSENAME(ServerIP, 4) AS INT),CAST(PARSENAME(ServerIP, 3) AS INT),CAST(PARSENAME(ServerIP, 2) AS INT),CAST(PARSENAME(ServerIP, 1) AS INT) ";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            string cpu, memory, serverip, category,status,os = "";
            int i = 1000;
            int rote = 1;


            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                Label1.Text = row1["getdate"].ToString() + " ";
                cpu = row1["cpu"].ToString();
                memory = row1["memory"].ToString();
                serverip = row1["serverip"].ToString();
                category = row1["category"].ToString();
                status = row1["status"].ToString();
                

                //캔버스 추가
                if (rote == 4)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("Canvas");
                    createDiv.ID = "myChart" + i;
                    createDiv.InnerHtml = "";
                    createDiv.Attributes["style"] = "width:100%; height:100%; z-index:100; margin-top:50px; margin-left:-70px;";
                    createDiv.Attributes.Add("Onclick", "go('" + serverip + "','" + category + "')");
                    div8.Controls.Add(createDiv);
                    //rote = 1;
                }
                if (rote == 3)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("Canvas");
                    createDiv.ID = "myChart" + i;
                    createDiv.InnerHtml = "";
                    createDiv.Attributes["style"] = "width:100%; height:100%; z-index:100; margin-top:50px; margin-left:-70px;";
                    createDiv.Attributes.Add("Onclick", "go('" + serverip + "','" + category + "')");
                    div6.Controls.Add(createDiv);
                    //rote = 1;
                }
                if (rote == 2)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("Canvas");
                    createDiv.ID = "myChart" + i;
                    createDiv.InnerHtml = "";
                    createDiv.Attributes["style"] = "width:100%; height:100%; z-index:100; margin-top:50px; margin-left:-70px;";
                    createDiv.Attributes.Add("Onclick", "go('" + serverip + "','" + category + "')");
                    div4.Controls.Add(createDiv);
                    //rote = 3;
                }
                if (rote == 1)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("Canvas");
                    createDiv.ID = "myChart" + i;
                    createDiv.InnerHtml = "";
                    createDiv.Attributes["style"] = "width:100%; height:100%; z-index:100; margin-top:50px; margin-left:-70px; ";
                    createDiv.Attributes.Add("Onclick", "go('" + serverip + "','" + category + "')");
                    div2.Controls.Add(createDiv);
                    //rote = 2;
                }


                //라벨 추가
                Label label = new Label();
                label.ID = "Label" + i;
                string roatetion = "";
                if (rote == 4)
                {
                    div8.Controls.Add(label);
                    rote = 1;
                    roatetion = "4";
                }
                if (rote == 3)
                {
                    div6.Controls.Add(label);
                    rote = 4;
                }
                if (rote == 2)
                {
                    div4.Controls.Add(label);
                    rote = 3;
                }
                if (rote == 1 && roatetion == "")
                {
                    div2.Controls.Add(label);
                    rote = 2;
                }

                StringBuilder st = new StringBuilder();
                st.Append("<script>");
                //st.Append("$('#div2').html('');");
                //st.Append("$('#div2').html('<canvas id='myChart1' style='z-index:200; position:relative; width:100%; height:100%;'></canvas>');");
                st.Append("var ctx2 = document.getElementById('myChart" + i + "').getContext('2d');");
                st.Append("var myChart2 = new Chart(ctx2, {");
                st.Append("type: 'doughnut',");
                st.Append("data: {");
                //st.Append("labels: ['CPU','Memory'],");
                st.Append("labels: ['Memory'],");
                st.Append("datasets: [{");
                //st.Append("data:['" + row1["cpu"].ToString() + "','"+ row1["memory"].ToString() + "'],");
                double total = 100;
                total = total - Convert.ToDouble(row1["memory"].ToString());
                st.Append("data:['" + row1["memory"].ToString() + "','" + total + "'],");
                //st.Append("backgroundColor: ['rgba(55, 123, 179, .7)','rgba(41, 186, 156, .7)']");
                if(status == "Server Connect")
                {
                    st.Append("backgroundColor: ['rgba(41, 186, 156, .7)']");
                }
                else
                {
                    st.Append("backgroundColor: ['rgba(186, 41, 41, .7)']");
                }
                st.Append("}]");
                st.Append("},");
                st.Append("options: {");
                st.Append("title: {");
                st.Append("display: false,");
                st.Append("text: '" + row1["serverip"].ToString() + "'");
                st.Append("}," +
                    "animation:{duration:0}" + // duration:0
                    "}");
                st.Append("});");
                st.Append("</script>");


                label.Text = st.ToString();

                st = null;
                i++;
            }


        }
        /// <summary>
        /// CPU
        /// </summary>
        private void javascript2()
        {
            string SQL = "select  cpu,memory,serverip,now() as getdate, category,status from service where memory is not null and flag = '1' order by " +
                "CAST(PARSENAME(ServerIP, 4) AS INT),CAST(PARSENAME(ServerIP, 3) AS INT),CAST(PARSENAME(ServerIP, 2) AS INT),CAST(PARSENAME(ServerIP, 1) AS INT) ";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            string cpu, memory, serverip, category, status, os = "";
            int i = 100;
            int rote = 1;


            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                Label1.Text = row1["getdate"].ToString() + " ";
                cpu = row1["cpu"].ToString();
                memory = row1["memory"].ToString();
                serverip = row1["serverip"].ToString();
                category = row1["category"].ToString();
                status = row1["status"].ToString();
                
                
                //캔버스 추가
                if (rote == 4)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("Canvas");
                    createDiv.ID = "myChart" + i;
                    createDiv.InnerHtml = "";
                    createDiv.Attributes["style"] = "width:100%; height:100%; z-index:200; margin-top:50px;  ";
                    createDiv.Attributes.Add("Onclick", "go('" + serverip + "','" + category + "')");
                    div7.Controls.Add(createDiv);
                    //rote = 1;
                }
                if (rote == 3)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("Canvas");
                    createDiv.ID = "myChart" + i;
                    createDiv.InnerHtml = "";
                    createDiv.Attributes["style"] = "width:100%; height:100%; z-index:200; margin-top:50px;";
                    createDiv.Attributes.Add("Onclick", "go('" + serverip + "','" + category + "')");
                    div5.Controls.Add(createDiv);
                    
                    //rote = 1;
                }
                if (rote == 2)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("Canvas");
                    createDiv.ID = "myChart" + i;
                    createDiv.InnerHtml = "";
                    createDiv.Attributes["style"] = "width:100%; height:100%; z-index:200; margin-top:50px;";
                    createDiv.Attributes.Add("Onclick", "go('" + serverip + "','" + category + "')");
                    div3.Controls.Add(createDiv);
                    //rote = 3;
                }
                if (rote == 1)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("Canvas");
                    createDiv.ID = "myChart" + i;
                    createDiv.InnerHtml = "";
                    createDiv.Attributes["style"] = "width:100%; height:100%; z-index:200;  margin-top:50px; ";
                    createDiv.Attributes.Add("Onclick", "go('" + serverip + "','" + category + "')");
                    div1.Controls.Add(createDiv);
                    //rote = 2;
                }


                //라벨 추가
                Label label = new Label();
                label.ID = "Label" + i;
                string roatetion = "";
                if (rote == 4)
                {
                    div7.Controls.Add(label);
                    rote = 1;
                    roatetion = "4";
                }
                if (rote == 3)
                {
                    div5.Controls.Add(label);
                    rote = 4;
                }
                if (rote == 2)
                {
                    div3.Controls.Add(label);
                    rote = 3;
                }
                if (rote == 1 && roatetion == "")
                {
                    div1.Controls.Add(label);
                    rote = 2;
                }

                StringBuilder st = new StringBuilder();
                st.Append("<script>");
                st.Append("var ctx2 = document.getElementById('myChart" + i + "').getContext('2d');");
                st.Append("var myChart2 = new Chart(ctx2, {");
                st.Append("type: 'doughnut',");
                st.Append("data: {");
                //st.Append("labels: ['CPU','Memory'],");
                st.Append("labels: ['CPU'],");
                st.Append("datasets: [{");
                //st.Append("data:['" + row1["cpu"].ToString() + "','"+ row1["memory"].ToString() + "'],");
                int total = 100;
                total = total - Convert.ToInt32(row1["cpu"].ToString());
                st.Append("data:['" + row1["cpu"].ToString() + "','" + total + "'],");
                //st.Append("backgroundColor: ['rgba(55, 123, 179, .7)','rgba(41, 186, 156, .7)']");
                if (status == "Server Connect")
                {
                    st.Append("backgroundColor: ['rgba(55, 123, 179, .7)']");
                }
                else
                {
                    st.Append("backgroundColor: ['rgba(186, 41, 41, .7)']");
                }
                st.Append("}]");
                st.Append("},");
                st.Append("options: {");
                st.Append("title: {");
                st.Append("display: true,");
                st.Append("text: '" + row1["serverip"].ToString() + "'");
                st.Append("}," +
                    "animation:{duration:0}" +
                    "}");
                st.Append("});");
                st.Append("</script>");


                label.Text = st.ToString();

                st = null;
                i++;
            }
        }
    
        protected void Button20_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://www.naver.com");


            XDocument doc = new XDocument(
               new XElement("Root",
                   new XElement("targetip", HiddenField1.Value),
                   new XElement("localip", HiddenField3.Value),
                   new XElement("category", HiddenField2.Value)
               )
           );
            doc.Save("c:\\SSIM WATCHER\\IP.xml");
           
        }
    }
}