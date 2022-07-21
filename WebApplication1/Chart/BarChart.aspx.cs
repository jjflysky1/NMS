using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public partial class BarChart : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        string SQL = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            TBLLOAD10();
        }
        private void TBLLOAD10()
        {
            SQL = "select   serverip from Service_v order by CAST(PARSENAME(serverip, 4) AS INT), CAST(PARSENAME(serverip, 3) AS INT), " +
                "CAST(PARSENAME(serverip, 2) AS INT), CAST(PARSENAME(serverip, 1) AS INT)";


            MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            DataSet DBSET = new DataSet();
            ADT.Fill(DBSET, "BD");
            foreach (DataRow row in DBSET.Tables["BD"].Rows)
            {
                DountChart_1(row["serverip"].ToString());
                DountChart_2(row["serverip"].ToString());
            }
        }
        private DataTable GetChartData1(string query)
        {

            MySqlCommand cmd2 = new MySqlCommand(query, DB);
            MySqlDataAdapter ADT2 = new MySqlDataAdapter();
            DataTable DBSET2 = new DataTable();
            cmd2.CommandType = CommandType.Text;
            cmd2.Connection = DB;
            ADT2.SelectCommand = cmd2;
            ADT2.Fill(DBSET2);
            ADT2.Dispose();
            return DBSET2;
        }
        int num = 2;
        private void DountChart_1(string serverip)
        {



            try
            {
                //string query = "select top 1 ISNULL(cpu, 0 ) as  cpu,ISNULL(memory, 0 ) as memory,ISNULL(b.traffic, 0 ) as traffic,a.serverip,status " +
                //    "from service a, server_traffic b where a.ServerIP = '" + serverip + "' and os is not null  and a.ServerIP = b.serverip";
                string query = "select top 1 ISNULL(cpu, 0 ) as  cpu,ISNULL(memory, 0 ) as memory,ISNULL(b.traffic, 0 ) as traffic,a.serverip,status, computer_name " +
                    "from service a, server_traffic b where a.ServerIP = '" + serverip + "' and flag = '1'  and a.ServerIP = b.serverip and category =N'네트워크/보안 장비' and dash_flag = '1'";

                DataTable dt = GetChartData1(query);

                if (dt.Rows.Count != 0)
                {
                    decimal[] x = new decimal[dt.Rows.Count];
                    decimal[] y = new decimal[dt.Rows.Count];
                    decimal[] z = new decimal[dt.Rows.Count];
                    string[] a = new string[dt.Rows.Count];
                    string[] b = new string[dt.Rows.Count];
                    string[] c = new string[dt.Rows.Count];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        x[i] = Convert.ToInt32(dt.Rows[0][0]);
                        y[i] = Convert.ToInt32(dt.Rows[0][1]);
                        z[i] = Convert.ToInt32(dt.Rows[0][2]);
                        a[i] = dt.Rows[0][3].ToString();
                        b[i] = dt.Rows[0][4].ToString();
                        c[i] = dt.Rows[0][5].ToString();


                        Label chart = new Label();
                        chart.ID = "mysecondchart" + num;
                        if (b[i] == "Server Disconnect" || c[i] == "No Access")
                        {
                            chart.Text = "<div style='margin-left:20px; margin-top:-200px;vertical-align:middle; text-align:center;  '><font size='2' color='red'><br>" +
                                  "" + c[i].ToString() + "</font></div>";
                        }
                        else
                        {
                            chart.Text = "<div style='margin-left:20px; margin-top:-200px;vertical-align:middle;text-align:center; '><font size='2' ></font><br>" +
                               "" + c[i].ToString() + "</div>";
                        }
                        chart.Attributes["style"] = "height:150px; width:110px; float:left;margin-top:20px; ";
                        Securitychart.Controls.Add(chart);

                        Label chartlabel = new Label();
                        chartlabel.ID = "Labell" + num;
                        chartlabel.Attributes["style"] = "height:220; width:110px;";
                        if (b[i].Contains("Disconnect") == true || c[i] == "No Access")
                        {
                            chartlabel.Text =
                             "<script type='text/javascript'>" +
                             "Morris.Bar({" +
                             "element: 'mysecondchart" + num + "'," +
                             " data: [" +
                             "{ title: '" + a[i] + "', a: " + x[i] + ", b: " + y[i] + " }" +
                             "]," +
                             "xkey: 'title'," +
                             "ymax : 100," +
                             "ykeys: ['a', 'b']," +
                             "parseTime:false," +
                             "hideHover:true," +
                             "labels: ['CPU', 'Memory']," +
                             "barColors: ['#fe2a2a', '#fe2a2a']," +
                              "grid:false," +
                                 " barRadius: [5, 5, 0, 0]," +
                              "gridTextSize:12" +
                             "});" +
                             "$('#mysecondchart" + num + "').click(function() {" +
                             "var url='../Service/Service_list.aspx?serverip=" + a[i].ToString() + "&category=네트워크';" +
                             "$(location).attr('href', url); " +
                             "});" +
                             "</script>";
                            //  chartlabel.Text =
                            //"<script type='text/javascript'>" +
                            //"var donut= Morris.Donut({" +
                            //"element: 'mysecondchart" + num + "'," +
                            //"data: [" +
                            //"{label:'CPU' , value: " + x[i] + ", formatted:'" + x[i] + "%'}," +
                            //"{label:'Memroy' , value: " + y[i] + ", formatted:'" + y[i] + "%'}" +
                            //"]," +
                            //"select:0," +
                            //"colors: [ " +
                            //  "'#ba0000', " +
                            //  "'#b53939', " +
                            //  "'#c66767', " +
                            //  "'#d79595' " +
                            //"]," +
                            //"formatter: function (x, data) { return data.formatted; }" +
                            //"   });" +
                            //"donut.select(0);" +
                            // "$('#mysecondchart" + num + "').click(function() {" +
                            //"var url='Service/Service_list.aspx?serverip=" + a[i].ToString() + "';" +
                            //"$(location).attr('href', url); " +
                            //"});" +
                            //"</script>";
                        }
                        else
                        {
                            chartlabel.Text =
                             "<script type='text/javascript'>" +
                             "Morris.Bar({" +
                             "element: 'mysecondchart" + num + "'," +
                             " data: [" +
                             "{ title: '" + a[i] + "', a: " + x[i] + ", b: " + y[i] + " }" +
                             "]," +
                             "xkey: 'title'," +
                             "ymax : 100," +
                             "ykeys: ['a', 'b']," +
                             "parseTime:false," +
                             "hideHover:true," +
                             "labels: ['CPU', 'Memory']," +
                             "barColors: ['#0b62a4', '#11a453']," +
                             "grid:false,"+
                                " barRadius: [5, 5, 0, 0]," +
                             "gridTextSize:12" +
                             "});" +
                             "$('#mysecondchart" + num + "').click(function() {" +
                             "var url='../Service/Service_list.aspx?serverip=" + a[i].ToString() + "&category=네트워크';" +
                             "$(location).attr('href', url); " +
                             "});" +
                             "</script>";
                            //  chartlabel.Text =
                            //"<script type='text/javascript'>" +
                            //"var donut= Morris.Donut({" +
                            //"element: 'mysecondchart" + num + "'," +
                            //"data: [" +
                            //"{label:'CPU' , value: " + x[i] + ", formatted:'" + x[i] + "%'}," +
                            //"{label:'Memroy' , value: " + y[i] + ", formatted:'" + y[i] + "%'}" +
                            //"]," +
                            //"select:0," +
                            //"colors: [ " +
                            //  "'#0BA462', " +
                            //  "'#39B580', " +
                            //  "'#67C69D', " +
                            //  "'#95D7BB' " +
                            //"]," +
                            //"formatter: function (x, data) { return data.formatted; }" +
                            //"   });" +
                            //"donut.select(0);" +
                            //"$('#mysecondchart" + num + "').click(function() {" +
                            //"var url='Service/Service_list.aspx?serverip=" + a[i].ToString() + "';" +
                            //"$(location).attr('href', url); " +
                            //"});" +
                            //"</script>";
                        }


                        Securitychart.Controls.Add(chartlabel);



                        num++;
                    }

                }
                else
                {
                  


                }

            }
            catch
            {

            }

        }

        private DataTable GetChartData2(string query)
        {

            MySqlCommand cmd2 = new MySqlCommand(query, DB);
            MySqlDataAdapter ADT2 = new MySqlDataAdapter();
            DataTable DBSET2 = new DataTable();
            cmd2.CommandType = CommandType.Text;
            cmd2.Connection = DB;
            ADT2.SelectCommand = cmd2;
            ADT2.Fill(DBSET2);
            ADT2.Dispose();
            return DBSET2;
        }
        int num2 = 2;
        private void DountChart_2(string serverip)
        {



            try
            {
                //string query = "select top 1 ISNULL(cpu, 0 ) as  cpu,ISNULL(memory, 0 ) as memory,ISNULL(b.traffic, 0 ) as traffic,a.serverip,status " +
                //    "from service a, server_traffic b where a.ServerIP = '" + serverip + "' and os is not null  and a.ServerIP = b.serverip";
                string query = "select top 1 ISNULL(cpu, 0 ) as  cpu,ISNULL(memory, 0 ) as memory,ISNULL(b.traffic, 0 ) as traffic,a.serverip,status,computer_name " +
                    "from service a, server_traffic b where a.ServerIP = '" + serverip + "' and flag = '1'  and a.ServerIP = b.serverip and category =N'서버 장비' and dash_flag = '1'";

                DataTable dt = GetChartData2(query);

                if (dt.Rows.Count != 0)
                {
                    decimal[] x = new decimal[dt.Rows.Count];
                    decimal[] y = new decimal[dt.Rows.Count];
                    decimal[] z = new decimal[dt.Rows.Count];
                    string[] a = new string[dt.Rows.Count];
                    string[] b = new string[dt.Rows.Count];
                    string[] c = new string[dt.Rows.Count];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        x[i] = Convert.ToInt32(dt.Rows[0][0]);
                        y[i] = Convert.ToInt32(dt.Rows[0][1]);
                        z[i] = Convert.ToInt32(dt.Rows[0][2]);
                        a[i] = dt.Rows[0][3].ToString();
                        b[i] = dt.Rows[0][4].ToString();
                        c[i] = dt.Rows[0][5].ToString();

                        Label chart = new Label();
                        chart.ID = "myfristchart" + num2;
                        if (b[i].Contains("Disconnect") == true || c[i] == "No Access")
                        {
                            chart.Text = "<div style='margin-left:20px; margin-top:-200px;vertical-align:middle; text-align:center; '><font size='2' color='red'><br>" +
                                "" + c[i].ToString() + "</font></div>";
                        }
                        else
                        {
                            chart.Text = "<div style='margin-left:20px; margin-top:-200px;vertical-align:middle;text-align:center; '><font size='2' ></font><br>" +
                                "" + c[i].ToString() + "</div>";
                            
                        }
                        chart.Attributes["style"] = "height:150px; width:110px; float:left; margin-top:20px;";
                        serverchart.Controls.Add(chart);



                        Label chartlabel = new Label();
                        chartlabel.ID = "Labelll" + num2;
                        chartlabel.Attributes["style"] = "height:220; width:110px; ";
                        if (b[i].Contains("Disconnect") == true || c[i] == "No Access")
                        {

                            chartlabel.Text =
                             "<script type='text/javascript'>" +
                             "Morris.Bar({" +
                             "element: 'myfristchart" + num2 + "'," +
                             " data: [" +
                             "{ title: '"+ a[i] + "', a: "+ x[i] + ", b: " + y[i] + " }" +
                             "]," +
                             "xkey: 'title'," +
                             "ymax : 100," +
                             "ykeys: ['a', 'b']," +
                             "parseTime:false," +
                             "hideHover:true," +
                             "labels: ['CPU', 'Memory']," +
                             "barColors: ['#fe2a2a', '#fe2a2a']," +
                              "grid:false," +
                             " barRadius: [5, 5, 0, 0]," +
                              "gridTextSize:12" +
                             "});" +
                              "$('#myfristchart" + num2 + "').click(function() {" +
                             "var url='../Service/Service_list.aspx?serverip=" + a[i].ToString() + "';" +
                             "$(location).attr('href', url); " +
                             "});" +
                             "</script>";
                            //  chartlabel.Text =
                            //"<script type='text/javascript'>" +
                            //"var donut= Morris.Donut({" +
                            //"element: 'myfristchart" + num2 + "'," +
                            //"data: [" +
                            //"{label:'CPU' , value: " + x[i] + ", formatted:'" + x[i] + "%'}," +
                            //"{label:'Memroy' , value: " + y[i] + ", formatted:'" + y[i] + "%'}" +
                            //"]," +
                            //"select:0," +
                            //"colors: [ " +
                            //  "'#ba0000', " +
                            //  "'#b53939', " +
                            //  "'#c66767', " +
                            //  "'#d79595' " +
                            //"]," +
                            //"formatter: function (x, data) { return data.formatted; }" +
                            //"   });" +
                            //"donut.select(0);" +
                            // "$('#myfristchart" + num2 + "').click(function() {" +
                            //"var url='Service/Service_list.aspx?serverip=" + a[i].ToString() + "';" +
                            //"$(location).attr('href', url); " +
                            //"});" +
                            //"</script>";
                        }
                        else
                        {
                            chartlabel.Text =
                               "<script type='text/javascript'>" +
                             "Morris.Bar({" +
                             "element: 'myfristchart" + num2 + "'," +
                             " data: [" +
                             "{ title: '" + a[i] + "', a: " + x[i] + ", b: " + y[i] + " }" +
                             "]," +
                             "xkey: 'title'," +
                             "ymax : 100," +
                             "ykeys: ['a', 'b']," +
                             "parseTime:false," +
                             "labels: ['Messages']," +
                             "hideHover:true," +
                             "labels: ['CPU', 'Memory']," +
                             "barColors: ['#0b62a4', '#11a453']," +
                              "grid:false," +
                              " barRadius: [5, 5, 0, 0],"+
                              "gridTextSize:12" +
                             "});" +
                             "$('#myfristchart" + num2 + "').click(function() {" +
                             "var url='../Service/Service_list.aspx?serverip=" + a[i].ToString() + "';" +
                             "$(location).attr('href', url); " +
                             "});" +
                             "</script>";
                  
                        }


                        serverchart.Controls.Add(chartlabel);



                        num2++;
                    }

               
                }
                else
                {
                

                }

            }
            catch
            {

            }

        }
    }

}
