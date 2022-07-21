using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Management;

namespace License
{
    class Program
    {
        static Socket sck;
        static void Main(string[] args)
        {



            Random adomRng = new Random();
            string rndString = string.Empty;
            char c;

            for (int i = 1; i < 25; i++)
            {
                while (!Regex.IsMatch((c = Convert.ToChar(adomRng.Next(48, 128))).ToString(), "[A-Z0-9]")) ;
                if (i % 5 == 0)
                {
                    rndString += "-";
                }
                rndString += c;
            }

            //키에서 핵스로
            string resultHex = string.Empty;
            byte[] arr_byteStr = Encoding.Default.GetBytes("W37I-6V38A-W0W5L-6SBAK-6E27S");

            foreach (byte byteStr in arr_byteStr)
            {
                resultHex += string.Format("{0:X2}", byteStr);
            }
            Console.WriteLine("키에서 헥스로 : " + resultHex);

            //헥스에서 string으로
            string InputText = "573337492D36563338412D573057354C2D365342414B2D3645323753";
            byte[] bb = Enumerable.Range(0, InputText.Length)
                      .Where(x => x % 2 == 0)
                      .Select(x => Convert.ToByte(InputText.Substring(x, 2), 16))
                      .ToArray();
            //return Convert.ToBase64String(bb);
            char[] chars = new char[bb.Length / sizeof(char)];
            System.Buffer.BlockCopy(bb, 0, chars, 0, bb.Length);
            Console.WriteLine("헥스에서 키로 : " + System.Text.Encoding.ASCII.GetString(bb));
            

            //내컴퓨터 맥어드레스 가져오기
            Dictionary<string, long> macaddresses = new Dictionary<string, long>();
            foreach(NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                    macaddresses[nic.GetPhysicalAddress().ToString()] = nic.GetIPStatistics().BytesSent + nic.GetIPStatistics().BytesReceived;
            }

            long maxValue = 0; 
            string mac = "";
            foreach(KeyValuePair<string, long> pair in macaddresses)
            {
                if(pair.Value > maxValue)
                {
                    mac = pair.Key;
                    maxValue = pair.Value;
                }
            }
            string macformat = "";
            char[] chararr = mac.ToCharArray();
            for(int i =0; i< chararr.Length; i++)
            {
                if (i % 2 == 0)
                {
                    macformat += chararr[i].ToString();
                }
                else
                {
                    macformat += chararr[i].ToString();
                    if (i != chararr.Length - 1)
                        macformat += "-";
                }
                    
            }
            Console.WriteLine("내컴퓨터 맥주소 : "+macformat);

            
            //sha1 암호화
            byte[] data = Encoding.ASCII.GetBytes("test");
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] result = sha.ComputeHash(data);
            Console.WriteLine("sha1 암호화 : "+Convert.ToBase64String(result));


            //메인보드 아이디
            var search = new ManagementObjectSearcher("SELECT * FROM Win32_baseboard");
            var mobos = search.Get();
            foreach (var m in mobos)
            {
                var serial = m["SerialNumber"]; // ProcessorID if you use Win32_CPU
                Console.WriteLine("Baseboard ID : " + serial);
            }

            //CPU 아이디
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                Console.WriteLine("CPU ID : " + id);
            }

            //링크 쿼리
            int[] scores = new int[] { 97, 92, 81, 60 };

            // Define the query expression.
            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 70
                select score;

            // Execute the query.
            foreach (int i in scoreQuery)
            {
                Console.WriteLine(i + " ");
            }



            //컴퓨터 바이오스 uuid 값
            try
            {
                string ComputerName = "localhost";
                ManagementScope Scope;
                Scope = new ManagementScope(String.Format("\\\\{0}\\root\\CIMV2", ComputerName), null);
                Scope.Connect();
                ObjectQuery Query = new ObjectQuery("SELECT UUID FROM Win32_ComputerSystemProduct");
                ManagementObjectSearcher Searcher = new ManagementObjectSearcher(Scope, Query);

                foreach (ManagementObject WmiObject in Searcher.Get())
                {
                    Console.WriteLine("{0,-35} {1,-40}", "UUID", WmiObject["UUID"]);// String                     
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format("Exception {0} Trace {1}", e.Message, e.StackTrace));
            }

            Console.ReadLine();
        }

}
}
