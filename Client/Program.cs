using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
           

            Client.Class1 cls1 = new Client.Class1();
     

            
            //foreach (Process process in Process.GetProcesses())
            //{
            //    if (process.ProcessName.StartsWith("SocketClient1"))
            //    {
            //        process.Kill();
            //    }
            //    else
            //    {

            //    }
            //}

            //string strappname = "C:\\SSIM WATCHER\\SocketClient1.exe";
            //Process.Start(strappname);



            try
            {
                //클라이언트
                Thread thread1 = new Thread(cls1.SYSTEMINFO);
                thread1.Start();
            }
            catch (Exception ex)
            {
                    Console.WriteLine(ex.Message);  
            }

            

            //try
            //{
            //    SOAP.Systeminfo soapclient = new SOAP.Systeminfo();
            //    DataSet ds = soapclient.LOGIN("test", "test");

            //    foreach(DataRow row in ds.Tables[0].Rows)
            //    {
            //        Console.WriteLine(row["serverip"].ToString() + " : " + row["computer_name"].ToString());
            //    }

            //    //ds.WriteXml(@"C:\test\test.xml");

            //    ////Console.WriteLine(ds.ReadXml(@"C:\test\test.xml"));

            //    ////string myXMLfile = @"C:\test\test.xml";
            //    ////XmlDocument document = new XmlDocument();
            //    ////document.Load(Path.Combine(Environment.CurrentDirectory, @"C:\test\test.xml"));
            //    ////XmlNode node = document.SelectSingleNode("/NewDataSet");
            //    ////Console.WriteLine("Id: {0}", node["BD"].InnerText);

            //    //XmlTextReader reader = new XmlTextReader(@"C:\test\test.xml");
            //    //while (reader.Read())
            //    //{
            //    //    switch (reader.NodeType)
            //    //    {
            //    //        //case XmlNodeType.Element: // The node is an element.
            //    //        //    Console.Write("<" + reader.Name);
            //    //        //    Console.WriteLine(">");
            //    //        //    break;
            //    //        case XmlNodeType.Text: //Display the text in each element.
            //    //            Console.WriteLine(reader.Value);
            //    //            break;
            //    //        //case XmlNodeType.EndElement: //Display the end of the element.
            //    //        //    Console.Write("</" + reader.Name);
            //    //        //    Console.WriteLine(">");
            //    //        //    break;
            //    //    }
            //    //}


            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}



        }

        
    }
}
