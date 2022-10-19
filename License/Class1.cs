using System;
using System.Collections.Generic;
using System.Management;
using System.Net.NetworkInformation;

namespace License
{
    public class Class1
    {
        public bool mac(string value)
        {
            Dictionary<string, long> macaddresses = new Dictionary<string, long>();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                    macaddresses[nic.GetPhysicalAddress().ToString()] = nic.GetIPStatistics().BytesSent + nic.GetIPStatistics().BytesReceived;
            }

            long maxValue = 0;
            string mac = "";
            foreach (KeyValuePair<string, long> pair in macaddresses)
            {
                if (pair.Value > maxValue)
                {
                    mac = pair.Key;
                    maxValue = pair.Value;
                }
            }
            string macformat = "";
            char[] chararr = mac.ToCharArray();
            for (int i = 0; i < chararr.Length; i++)
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

            Console.WriteLine(macformat);

            if (macformat == value)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool uuid(string value)
        {
            string uidvalue = "";
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
                    //Console.WriteLine("{0,-35} {1,-40}", "UUID", WmiObject["UUID"]);// String                     
                    uidvalue = WmiObject["UUID"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format("Exception {0} Trace {1}", e.Message, e.StackTrace));
            }

            if (uidvalue == value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
