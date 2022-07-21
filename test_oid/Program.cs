using SnmpSharpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_oid
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double percen = 0;
            try
            {
                SimpleSnmp snmp = new SimpleSnmp("192.168.0.170", "public");
                Pdu pdu = new Pdu();
                Pdu pdu1 = new Pdu();
                Pdu pdu2 = new Pdu();
                Pdu pdu3 = new Pdu();

                pdu.VbList.Add(".1.3.6.1.4.1.2021.4.5.0");//토탈
                pdu1.VbList.Add(".1.3.6.1.4.1.2021.4.6.0");//유즈
                pdu2.VbList.Add(".1.3.6.1.4.1.2021.4.14.0");//버퍼
                pdu3.VbList.Add(".1.3.6.1.4.1.2021.4.15.0");//캐시
                Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, pdu); //.GetNext(pdu);
                Dictionary<Oid, AsnType> result1 = snmp.Get(SnmpVersion.Ver2, pdu1); //.GetNext(pdu);
                Dictionary<Oid, AsnType> result2 = snmp.Get(SnmpVersion.Ver2, pdu2); //.GetNext(pdu);
                Dictionary<Oid, AsnType> result3 = snmp.Get(SnmpVersion.Ver2, pdu3); //.GetNext(pdu);

                if (result == null)
                {
                    Console.WriteLine("Request failed.");
                }
                else
                {
                    double total = 0;
                    double use = 0;
                    double buffer = 0;
                    double cache = 0;
                    foreach (KeyValuePair<Oid, AsnType> entry in result)
                    {
                        total = Convert.ToDouble(entry.Value.ToString());
                        //Console.WriteLine(entry.Value.ToString());
                    }
                    foreach (KeyValuePair<Oid, AsnType> entry1 in result1)
                    {
                        use = Convert.ToDouble(entry1.Value.ToString());
                        //Console.WriteLine(entry1.Value.ToString());
                    }
                    foreach (KeyValuePair<Oid, AsnType> entry2 in result2)
                    {
                        buffer = Convert.ToDouble(entry2.Value.ToString());
                        //Console.WriteLine(entry1.Value.ToString());
                    }
                    foreach (KeyValuePair<Oid, AsnType> entry1 in result3)
                    {
                        cache = Convert.ToDouble(entry1.Value.ToString());
                        //Console.WriteLine(entry1.Value.ToString());
                    }
                    Console.WriteLine("total : " + total);
                    Console.WriteLine("use : " + use);
                    Console.WriteLine("buffer : " + buffer);
                    Console.WriteLine("cache : " + cache);
                    percen = total / (total - cache) * 0.01;
                    Console.WriteLine(percen);

                    use = total - (use - (buffer + cache));
                    percen = total / (total - cache);
                    Console.WriteLine(percen.ToString("#.#") + "%");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex + Environment.NewLine + "-------------------------------------------------------");
            }
        }
    }
}
