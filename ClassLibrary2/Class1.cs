using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

using System.Threading;

using System.Diagnostics;
using System.Text.RegularExpressions;

using System.Management;
using Renci.SshNet;

namespace Service2
{
    //자동 서버 추가
    public class Service2
    {

       
        public void autoadd()
        {
            DBCON.Class1 DBCON = new DBCON.Class1();
            SqlConnection CON = new SqlConnection(DBCON.DBCON);
            string SQL = "";

            while (true)
            {
                SQL = "select  * from service_range";
                SqlDataAdapter ADT = new SqlDataAdapter(SQL, CON);
                DataSet DBSET = new DataSet();
                ADT.Fill(DBSET, "BD");

                foreach (DataRow row in DBSET.Tables["BD"].Rows)
                {
                    try
                    {
                        string[] startipsplit = { };
                        string startip = "";
                        string[] endipsplit = { };
                        string endip = "";
                        string startipDB = row["startip"].ToString();
                        string endipDB = row["endip"].ToString();
                        string network_name = row["name"].ToString();

                        startipsplit = startipDB.ToString().Split('.');
                        for (int i = 0; i < 3; i++)
                        {
                            startip += startipsplit[i] + '.';
                        }
                        endipsplit = endipDB.ToString().Split('.');
                        for (int i = 0; i < 4; i++)
                        {
                            endip = endipsplit[i];
                        }


                        string network = startip;
                        string ip = "";

                        for (int i = 1; i < Convert.ToInt32(endip); i++)
                        {
                            ip = network + i.ToString();
                            var reply = new Ping().Send(ip, 120);
                            if (reply.Status == IPStatus.Success)
                            {
                                Console.WriteLine(reply.Address.ToString() + " 검사");
                                CON.Open();
                                SqlCommand cmd = new SqlCommand("Auto_add_server", CON);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@serverip", reply.Address.ToString());
                                cmd.Parameters.AddWithValue("@network_name", network_name);
                                cmd.ExecuteNonQuery();
                                cmd.Dispose();
                                cmd = null;
                                CON.Close();

                            }



                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);


                    }
                }

            }



        }


    }
    }

