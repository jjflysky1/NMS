using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventRead
{
    public class Class1
    {
        int sleep = 10000;
        public void readtext()
        {
            while(true)
            {
                try
                {

                    DBCON.Class1 DBCON = new DBCON.Class1();
                    SqlConnection CON = new SqlConnection(DBCON.DBCON);
                    string SQL = "";
                    SQL = "select * from event_list";
                    SqlDataAdapter ADT = new SqlDataAdapter(SQL, CON);
                    DataSet DBSET = new DataSet();
                    ADT.Fill(DBSET, "BD");
                    string outfilename = "";
                    foreach (DataRow row in DBSET.Tables["BD"].Rows)
                    {
                        string[] fileEntries = Directory.GetFiles(@"C:\SSIM WATCHER\log");
                        foreach (string fileName in fileEntries)
                        {
                            //Console.WriteLine(fileName);
                            outfilename = fileName;
                            string text = File.ReadAllText(fileName, Encoding.Default);
                            Console.WriteLine("{0}", text);
                            if (text.Contains(row["name"].ToString()) == true)
                            {
                                Console.WriteLine(row["name"].ToString() + " 이벤트 발견");
                                mail mail = new mail();
                                mail.Event_sendmail(row["name"].ToString());
                            }
                            else
                            {
                                Console.WriteLine("없다");
                            }
                            
                        }
                    }


                    string sDirPath;
                    sDirPath = @"C:\SSIM WATCHER\log\backup";
                    DirectoryInfo di = new DirectoryInfo(sDirPath);
                    if (di.Exists == false)
                    {
                        di.Create();
                    }

                    FileInfo fileinfo = new FileInfo(outfilename);
                    fileinfo.MoveTo(@"C:\SSIM WATCHER\log\backup\" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt");
                    //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));



                    
                }
               catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Thread.Sleep(sleep);
            }
           
        }

            


    }
}
