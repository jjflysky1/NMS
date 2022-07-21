using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class testt : System.Web.UI.Page
    {
        private MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            //chart1();
        }
        public class Customer
        {
            public string serverip { get; set; }
            public string traffic { get; set; }
            public string time { get; set; }
        }
        [WebMethod]
        public  List<Customer> chart1()
        {
            MySqlConnection DB = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString);
            List<Customer> Parts = new List<Customer>();


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
            MySqlDataAdapter ADT3 = new MySqlDataAdapter("Traffic_top", DB);
            ADT3.SelectCommand.CommandType = CommandType.StoredProcedure;
            ADT3.SelectCommand.Parameters.AddWithValue("@time", "10");
            DataSet DBSET3 = new DataSet();
            ADT3.Fill(DBSET3, "BD3");
            string[] serverip = { };
            string temp = "";
            foreach (DataRow row in DBSET3.Tables["BD3"].Rows)
            {
                temp = temp + row["serverip"].ToString() + ",";


                //그래프
                //string SQL = "select top 5 serverip, traffic, time from Secure_Log where serverip = '" + row["serverip"].ToString() + "' order by time desc";
                //MySqlDataAdapter ADT4 = new MySqlDataAdapter(SQL, DB);
                DataSet DBSET4 = new DataSet();
                //ADT4.Fill(DBSET4, "BD4");
                temp = "";
                foreach (DataRow row1 in DBSET4.Tables["BD4"].Rows)
                {
                    Parts.Add(new Customer
                    {
                        serverip = row1["serverip"].ToString(),
                        traffic = row1["traffic"].ToString(),
                        time = row1["time"].ToString()
                    });
                }
            }


            string iresurlt = "";
            iresurlt = JsonConvert.SerializeObject(Parts);

            
            return Parts;

        }
    }
}