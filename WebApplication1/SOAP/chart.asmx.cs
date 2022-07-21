using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;

namespace WebApplication1.SOAP
{
    /// <summary>
    /// chart의 요약 설명입니다.
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // ASP.NET AJAX를 사용하여 스크립트에서 이 웹 서비스를 호출하려면 다음 줄의 주석 처리를 제거합니다. 
    // [System.Web.Script.Services.ScriptService]
    public class chart : System.Web.Services.WebService
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        private DataTable GetChartData(string query)
        {

            MySqlCommand cmd = new MySqlCommand(query, DB);
            MySqlDataAdapter ADT = new MySqlDataAdapter();
            DataTable DBSET = new DataTable();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = DB;
            ADT.SelectCommand = cmd;
            ADT.Fill(DBSET);
            ADT.Dispose();
            return DBSET;
        }
        public class Customer
        {
            public string serverip { get; set; }
            public string taffic { get; set; }
        }
        [WebMethod]
        public string chart1()
        {
            List<string> Parts = new List<string>();


            //string count = "";
            //string SQL = "select area_chart from Log_Time_Config";
            //MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, DB);
            //DataSet DBSET = new DataSet();
            //ADT.Fill(DBSET, "BD4");
            //foreach (DataRow row1 in DBSET.Tables["BD4"].Rows)
            //{

            //    count = row1["area_chart"].ToString();
            //}

            string time1 = DateTime.Now.ToString("HH");
            string IN = "";
            //string time2 = DateTime.Now.ToString(row2["time"].ToString());
            //SQL = "select top 1 serverip, round(traffic,2) as traffic from System_Log_Traffic where serverip " +
            //    "IN (select serverip from Service where flag = '1') " +
            //    " and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = "+ time1.ToString() +"" +
            //    //" and SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = SUBSTRING(CONVERT(NVARCHAR, dateadd(HOUR, -1, now()), 121), 12, 2)" +
            //    " and left(time, 10) = LEFT(now(), 10) order by traffic desc";
            //MySqlDataAdapter ADT3 = new MySqlDataAdapter(SQL, DB);
            MySqlDataAdapter ADT3 = new MySqlDataAdapter("Traffic_top", DB);
            ADT3.SelectCommand.CommandType = CommandType.StoredProcedure;
            ADT3.SelectCommand.Parameters.AddWithValue("@time", time1);
            DataSet DBSET3 = new DataSet();
            ADT3.Fill(DBSET3, "BD3");
            string[] serverip = { };
            string temp = "";
            foreach (DataRow row in DBSET3.Tables["BD3"].Rows)
            {
                temp = temp + row["serverip"].ToString() + ",";
                Parts.Add(row["serverip"].ToString() + "," + row["traffic"].ToString());

                IN = "in";

                if (row["serverip"].ToString() == "")
                {
                    //그래프
                    string SQL = "select top 3 serverip, round(traffic,2) as traffic from Secure_Log " +
                        "where SUBSTRING(CONVERT(NVARCHAR, time, 121), 12, 2) = SUBSTRING(CONVERT(NVARCHAR, dateadd(HOUR, -1, now()), 121), 12, 2)" +
                        "  and left(time, 10) = LEFT(now(), 10) order by traffic desc";
                    MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, DB);
                    DataSet DBSET4 = new DataSet();
                    ADT4.Fill(DBSET4, "BD4");
                    temp = "";

                    foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
                    {

                        temp = temp + row["serverip"].ToString() + ",";

                    }
                }


            }


            string iresurlt = "";
            iresurlt = JsonConvert.SerializeObject(Parts);


            return iresurlt;

        }
    }
}
