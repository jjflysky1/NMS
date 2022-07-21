using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        public void SYSTEMINFO()
        {
            try
            {
                // Get the object.
                ManagementObject obj = new ManagementObject();
                ManagementPath path = new ManagementPath(scope.Path + ":CCM_InstalledComponent.Name='SMSClient'");

                obj.Path = path;
                obj.Get();

                // Display a single property.
                Console.WriteLine(obj["DisplayName"].ToString());

                // Display all properties.
                foreach (PropertyData property in obj.Properties)
                {
                    Console.WriteLine(property.Name + " " + property.Value);
                }
            }
            catch (ManagementException e)
            {
                Console.WriteLine("Failed to get component: " + e.Message);
                throw;
            }
        }
    }
}
