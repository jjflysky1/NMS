using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WebApplication2
{
    public partial class mainicon2 : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            javascript();
            javascript2();
            javascript3();
            TBLSET();
            piechart();
        }

        /// <summary>
        /// 
        /// 중앙 파이차트
        /// </summary>
        private void piechart()
        {
            //네트워크/보안
            string SQL = "select  cpu,memory,serverip, category,status, left(os,30) as os  from service where memory is not null and flag = '1' and Category LIKE '%네트워크%' ORDER BY INET_ATON(serverip) LIMIT 10";
            MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET4 = new DataSet();
            ADT4.Fill(DBSET4, "BD4");
            string status = "";
            string status1 = "";
            string status2 = "";
            int i = 1000;
            
            foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
            {
                //Label1.Text = row1["getdate"].ToString() + " ";
                status += row1["status"].ToString();

            }

            //PC
            string SQL2 = "select  cpu,memory,serverip, category,status, left(os,30) as os  from service where memory is not null and flag = '1' and Category LIKE '%PC%' ORDER BY INET_ATON(serverip) LIMIT 10";
            MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL2, DB);
            DataSet DBSET2 = new DataSet();
            ADT2.Fill(DBSET2, "BD4");
            
            foreach (DataRow row1 in DBSET2.Tables["BD4"].Rows)
            {
                //Label1.Text = row1["getdate"].ToString() + " ";
                status1 += row1["status"].ToString();

            }
            //서버
            string SQL3 = "select  cpu,memory,serverip, category,status, left(os,30) as os  from service where memory is not null and flag = '1' and Category LIKE '%서버%' ORDER BY INET_ATON(serverip) LIMIT 10";
            MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL3, DB);
            DataSet DBSET3 = new DataSet();
            ADT3.Fill(DBSET3, "BD4");
            
            foreach (DataRow row1 in DBSET3.Tables["BD4"].Rows)
            {
                //Label1.Text = row1["getdate"].ToString() + " ";
                status2 += row1["status"].ToString();

            }

            string a = "";
            string b = "";
            string c = "";
            string parttitle = "";
            string parttitle1 = "";
            string parttitle2 = "";
            if (status.Contains("Disconnect") == true)
            {
                a = "rgba(243, 104, 88)";
                parttitle = "네트워크/보안 이상"; 
            }
            else
            {
                a = "rgba(0, 166, 90)";
                parttitle = "네트워크/보안 정상";
            }
            if (status1.Contains("Disconnect") == true)
            {
                b = "rgba(243, 104, 88)";
                parttitle1 = "PC 이상"; 
            }
            else
            {
                b = "rgba(0, 166, 90)";
                parttitle1 = "PC 정상";
            }
            if (status2.Contains("Disconnect") == true)
            {
                c = "rgba(243, 104, 88)";
                parttitle2 = "서버 이상"; 
            }
            else
            {
                c = "rgba(0, 166, 90)";
                parttitle2 = "서버 정상";
            }


            //System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("Canvas");
            //createDiv.ID = "myChart" + i;
            //createDiv.InnerHtml = "";
            //createDiv.Attributes["style"] = "width:70%;  z-index:100; ";
            //div101.Controls.Add(createDiv);
            
            Label label = new Label();
            label.ID = "Label" + i;
            div101.Controls.Add(label);
            StringBuilder st = new StringBuilder();
            st.Append("<script>");
           

            st.Append("var data = [");
            st.Append("['kr-4194', 0],");
            st.Append("['kr-kg', 1],");//경기도
            st.Append("['kr-cb', 2],");//전라북도
            st.Append("['kr-kn', 3],");//경상남도
            st.Append("['kr-2685', 4],");//전라남도
            st.Append("['kr-pu', 5],");//부산
            st.Append("['kr-2688', 6],");//경상북도
            st.Append("['kr-sj', 7],");//세종
            st.Append("['kr-tj', 8],");//대전
            st.Append("['kr-ul', 9],");//울산
            st.Append("['kr-in', 10],");//인천
            st.Append("['kr-kw', 11],");//강원도
            st.Append("['kr-gn', 12],");//충청남도
            st.Append("['kr-cj', 13],");//제주도
            st.Append("['kr-gb', 14],");//충청북도
            st.Append("['kr-so', 15],");
            st.Append("['kr-tg', 16],");//대구
            st.Append("['kr-kj', 30]");//광주
            st.Append("];");
            st.Append("Highcharts.mapChart('div101', {");
            st.Append("chart: {map: 'countries/kr/kr-all',backgroundColor: 'rgba(255, 255, 255, 0.0)'," +
                "height: (9 / 16 * 100) + '%'},");
            st.Append("title: {text: '상황판'},");
            st.Append("subtitle: {text: ''},");
            st.Append("navigation: {buttonOptions: {enabled: false}},");
            st.Append("mapNavigation: {"+
                "enabled: false, "+
                "buttonOptions: {" +
                "" +
                "verticalAlign: 'top'," +
                "" +
                "}},");
            st.Append("colorAxis: {" +
                "min: 0," +
                "" +
                "},");
            st.Append("series: [{" +
                "data: data," +
                "name: 'Random data'," +
                "states:{" +
                "hover:{" +
                "color: '#BADA55'" +
                "}},");
            st.Append("exporting: {enabled: false},");
            st.Append("dataLabels: {" +
                "enabled: true," +
                "format: '{point.name}'" +
                "}" +
                "}]" +
                "});");
    
                       
            st.Append("</script>");

            label.Text = st.ToString();

            st = null;

        }

        /// <summary>
        /// 
        /// PC
        /// </summary>
        private void javascript()
        {
            string SQL = "select  cpu,memory,serverip, category, now() as getdate, status , left(os,30) as os from service where memory is not null and flag = '1' and Category = 'PC'  ORDER BY INET_ATON(serverip) LIMIT 10";
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
                os = row1["os"].ToString();



                //라벨 추가
                Label label = new Label();
                label.ID = "Label" + i;
                string roatetion = "";
               
                div2.Controls.Add(label);
                StringBuilder st = new StringBuilder();
               
                st.Append("<div style='width:130px; height:110px; float:left; margin-right:0%;'>");
                if(status.Contains("Disconnect") == true)
                {
                    st.Append("<img src='server2_error.png' width='60' height='80'>");
                }
                else
                {
                    st.Append("<img src='server2.png' width='60' height='80'>");
                }
                st.Append("<font size='2'>" + row1["serverip"].ToString() + "</font>");
                st.Append("</div>");

                label.Text = st.ToString();

                st = null;
                i++;
            }


        }
        /// <summary>
        /// 네트워크/보안
        /// </summary>
        private void javascript2()
        {
            string SQL = "select  cpu,memory,serverip, category, now() as getdate,status , left(os,30) as os from service where memory is not null and flag = '1' and Category like '%네트워크%'  ORDER BY INET_ATON(serverip) LIMIT 10 ";
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
                os = row1["os"].ToString();
                
          


                //라벨 추가
                Label label = new Label();
                label.ID = "Label" + i;
                string roatetion = "";
              
                div1.Controls.Add(label);
                StringBuilder st = new StringBuilder();
                if(i == 100)
                {
                    
                }
                st.Append("<div style='width:130px; height:110px; float:left;  margin-right:0%; '>");
                if (status.Contains("Disconnect") == true)
                {
                    st.Append("<img src='switch_error.png' width='100' height='80'>");
                }
                else
                {
                    st.Append("<img src='switch.png' width='100' height='80'>");
                }
                st.Append("<font size='2'>"+ row1["serverip"].ToString() + "</font>");
                st.Append("</div>");
                label.Text = st.ToString();

                st = null;
                i++;
            }
        }

        /// <summary>
        /// 
        /// 서버
        /// </summary>
        private void javascript3()
        {
            string SQL = "select  cpu,memory,serverip, category,now() as getdate, status , left(os,30) as os from service where memory is not null and flag = '1' and Category like '%서버%'  ORDER BY INET_ATON(serverip) LIMIT 10";
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
                os = row1["os"].ToString();




                //라벨 추가
                Label label = new Label();
                label.ID = "Label" + i;
                string roatetion = "";

                div3.Controls.Add(label);
                StringBuilder st = new StringBuilder();
                if (i == 100)
                {

                }
                st.Append("<div style='width:130px; height:110px; float:left;  margin-right:0%; '>");
                if (status.Contains("Disconnect") == true)
                {
                    st.Append("<img src='security_error.png' width='100' height='80'>");
                }
                else
                {
                    st.Append("<img src='security.png' width='100' height='80'>");
                }
                st.Append("<font size='2'>" + row1["serverip"].ToString() + "</font>");
                st.Append("</div>");
                label.Text = st.ToString();

                st = null;
                i++;
            }
        }


        private void TBLSET()
        {
            TableHeaderRow TR;
            TableHeaderCell TD;

            TR = new TableHeaderRow();

            //TR.BackColor = System.Drawing.Color.WhiteSmoke;

            TD = new TableHeaderCell();
            TD.Width = 40;
            TD.Text = "NO";
            //TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 20;
            TD.Text = "서버 아이피";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "내용";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableHeaderCell();
            TD.Width = 60;
            TD.Text = "시각";
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TBLLIST.Rows.Add(TR);
            TBLLOAD();
        }

        private void TBLLOAD()
        {
            SQL = "select  * from Event_log order by time desc limit 4";

            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                TBLADD(row["serverip"].ToString(), row["event_log"].ToString(), row["time"].ToString());

            }
        }
        long a = 1;
        private void TBLADD(string serverip, string event_log, string time)
        {
            TableRow TR;
            TableCell TD;


            TR = new TableRow();

            TD = new TableCell();
            TD.Width = 20;
            TD.Text = a.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            //TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = serverip.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 60;
            TD.Text = event_log.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);

            TD = new TableCell();
            TD.Width = 200;
            TD.Text = time.ToString();
            TD.Attributes["style"] = "text-align : center; vertical-align:middle;";
            TR.Cells.Add(TD);


            a++;
            TBLLIST.Rows.Add(TR);

        }


        protected void Button20_Click(object sender, EventArgs e)
        {
            //Response.Redirect("http://www.naver.com");


            XDocument doc = new XDocument(
               new XElement("Root",
                   new XElement("targetip", HiddenField1.Value),
                   new XElement("localip", HiddenField3.Value),
                   new XElement("category", HiddenField2.Value),
                   new XElement("os", HiddenField4.Value)
               )
           );
            doc.Save("c:\\SSIM WATCHER\\IP.xml");

            Process.Start("c:\\SSIM WATCHER\\SocketServer.exe");


            Response.Redirect("main2.aspx");
        }
    }
}