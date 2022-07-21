using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SocketServer
{
    class Program
    {
        static Socket sck;
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load("c:\\SSIM WATCHER\\IP.xml");
            var authors = doc.Descendants("targetip");
            var authors2 = doc.Descendants("localip");
            var authors3 = doc.Descendants("category");
            var authors4 = doc.Descendants("os");

            string targetip = "";
            string localip = "";
            string category = "";
            string os = "";
            foreach (var author in authors)
            {
                targetip = author.Value;
            }
            foreach (var author in authors2)
            {
                localip = author.Value;
            }
            foreach (var author in authors3)
            {
                category = author.Value;
            }
            foreach (var author in authors4)
            {
                os = author.Value;
            }

            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(localip), 1234);

            try
            {
                sck.Connect(localEndPoint);
            }
            catch
            {
                Console.Write("Unable to connect to remote end point!\r\n");
                Main(args);
            }

            Console.Write("Enter Text: ");
            //string text = Console.ReadLine();
            byte[] data = Encoding.UTF8.GetBytes(targetip + "," + os);

            sck.Send(data);
            //Console.Write("Data Sent!\r\n");
            //Console.Write("Press any key To continue...");
            //Console.Read();
            sck.Close();

        }
    }
}
