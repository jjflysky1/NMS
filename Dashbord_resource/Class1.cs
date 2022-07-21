using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dashbord_resource
{
    public class Class1
    {


        public void secure()
        {
            while (true)
            {
                try
                {
                    DBCON.Class1 DBCON = new DBCON.Class1();
                    MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
                    string SQL = "";
                    double count = 0;
                    SQL = "select count(distinct serverip) as count from Secure_Resource_V " +
                        "where cpu is not null  and status = 'Server Connect' ";
                    MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
                    DataSet DBSET = new DataSet();
                    ADT.Fill(DBSET, "BD");
                    foreach (DataRow row in DBSET.Tables["BD"].Rows)
                    {
                        count = Convert.ToInt32(row["count"].ToString());
                        count = count / 3;
                        //Console.WriteLine(Math.Ceiling(count));
                    }
                    int list = 1;
                    for (int i = 0; i < Math.Ceiling(count); i++)
                    {
                        CON.Open();
                        MySqlCommand cmd2 = new MySqlCommand();
                        cmd2.Connection = CON;
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.CommandText = "update Log_Time_Config set secure_source_list = @source_list ";
                        cmd2.Parameters.Add("@source_list", MySqlDbType.VarChar, 100).Value = list;
                        cmd2.ExecuteNonQuery();
                        cmd2.Dispose();
                        cmd2 = null;
                        CON.Close();
                        list = list + 3;
                        Thread.Sleep(5000);
                    }
                    Thread.Sleep(1000);
                }
                catch(Exception E)
                {
                    Console.WriteLine(E.Message);
                }
            }
        }
        public void server()
        {
            while (true)
            {
                try
                {
                    DBCON.Class1 DBCON = new DBCON.Class1();
                    MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
                    string SQL = "";
                    double count = 0;
                    SQL = "select count(distinct serverip) as count from server_Resource_V " +
                        "where cpu is not null  and status = 'Server Connect' ";
                    MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
                    DataSet DBSET = new DataSet();
                    ADT.Fill(DBSET, "BD");
                    foreach (DataRow row in DBSET.Tables["BD"].Rows)
                    {
                        count = Convert.ToInt32(row["count"].ToString());
                        count = count / 3;
                        //Console.WriteLine(Math.Ceiling(count));
                    }
                    int list = 1;
                    for (int i = 0; i < Math.Ceiling(count); i++)
                    {
                        CON.Open();
                        MySqlCommand cmd2 = new MySqlCommand();
                        cmd2.Connection = CON;
                        cmd2.CommandType = System.Data.CommandType.Text;
                        cmd2.CommandText = "update Log_Time_Config set server_source_list = @source_list ";
                        cmd2.Parameters.Add("@source_list", MySqlDbType.VarChar, 100).Value = list;
                        cmd2.ExecuteNonQuery();
                        cmd2.Dispose();
                        cmd2 = null;
                        CON.Close();
                        list = list + 3;
                        Thread.Sleep(5000);
                    }
                    Thread.Sleep(1000);
                }
                catch(Exception e)
                {
                    Console.WriteLine("에러" + e.Message);
                }
                
            }
        }
    }
}
