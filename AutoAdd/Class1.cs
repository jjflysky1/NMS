using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Net.NetworkInformation;
using System.Threading;

namespace autoadd
{
    //자동 서버 추가
    public class Service2
    {


        public void autoadd()
        {
            try
            {
                DBCON.Class1 DBCON = new DBCON.Class1();

                MySqlConnection CON = new MySqlConnection(DBCON.DBCON);
                string SQL = "";

                while (true)
                {
                    int count = 0;
                    SQL = "select DISTINCT count(ServerIP) as count from service";
                    MySqlDataAdapter ADT2 = new MySqlDataAdapter(SQL, CON);
                    DataSet DBSET2 = new DataSet();
                    ADT2.Fill(DBSET2, "BD");
                    foreach (DataRow row in DBSET2.Tables["BD"].Rows)
                    {
                        count = Convert.ToInt32(row["count"].ToString());

                    }
                    if (DBCON.Liesence + 1 > count)
                    {
                        Console.WriteLine("서비스 숫자 : " + count);
                        Thread.Sleep(5000);
                        SQL = "select * from service_range";
                        MySqlDataAdapter ADT = new MySqlDataAdapter(SQL, CON);
                        DataSet DBSET = new DataSet();
                        ADT.Fill(DBSET, "BD");
                        foreach (DataRow row in DBSET.Tables["BD"].Rows)
                        {
                            try
                            {
                                string[] startipsplit = { };
                                string startip = "";
                                string startip_right = "";
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


                                startipsplit = startipDB.ToString().Split('.');
                                for (int i = 0; i < 4; i++)
                                {
                                    startip_right = startipsplit[i];
                                }
                                endipsplit = endipDB.ToString().Split('.');
                                for (int i = 0; i < 4; i++)
                                {
                                    endip = endipsplit[i];
                                }

                                Console.WriteLine("시작 " + startipDB);
                                Console.WriteLine("종료 " + endipDB);

                                string network = startip;
                                string ip = "";

                                //try {
                                //    Console.WriteLine("대역대 " + startip);
                                //    Console.WriteLine("시작 아이피 끝 " + startip_right);
                                //    Console.WriteLine("끝 아이피 끝 " + endip);
                                //}
                                //catch(Exception E) {
                                //    Console.WriteLine (E.Message);
                                //}


                                for (int i = Convert.ToInt32(startip_right); i <= Convert.ToInt32(endip); i++)
                                {
                                    ip = network + i.ToString();
                                    //Console.WriteLine("반복문 들어온 아이피 " + ip);
                                    var reply = new Ping().Send(ip, 120);

                                    if (reply.Status == IPStatus.Success)
                                    {
                                        Console.WriteLine(reply.Address.ToString() + " 검사");
                                        if (CON.State != ConnectionState.Open)
                                        {
                                            CON.Open();
                                        }
                                        MySqlCommand cmd = new MySqlCommand("Auto_add_server", CON);
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.Parameters.AddWithValue("@serverip1", reply.Address.ToString());
                                        cmd.Parameters.AddWithValue("@network_name1", network_name);
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
                    else
                    {
                        Console.WriteLine("장비등록 라이선스 오버");
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

