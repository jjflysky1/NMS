using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace test8
{
    internal class Program
    {

        // 클래스 정의
        public class Person
        {
            // 필드
            private string name;
            private int age;

            // 생성자
            public Person(string name, int age)
            {
                this.name = name;
                this.age = age;
            }

            // 속성
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public int Age
            {
                get { return age; }
                set
                {
                    if (value > 0)
                        age = value;
                    else
                        throw new ArgumentException("Age must be positive.");
                }
            }

            // 메서드
            public void Introduce()
            {
                Console.WriteLine($"안녕하세요! 저는 {Name}이고, {Age}살입니다.");
            }
        }
        static async Task Main(string[] args)
        {
            // 객체 생성
            Person person1 = new Person("홍길동", 25);
            person1.Introduce();

            // 속성 변경
            person1.Name = "김철수";
            person1.Age = 1;

            person1.Introduce();
        }

        static async void a()
        {
            string subnet = "192.168.0"; // 네트워크의 서브넷을 설정하세요
            int startRange = 1; // 스캔을 시작할 IP 주소 범위의 시작 번호를 설정하세요
            int endRange = 255; // 스캔을 시작할 IP 주소 범위의 끝 번호를 설정하세요
            int port = 80; // 스캔할 포트 번호를 설정하세요

            var tasks = new Task[endRange - startRange + 1];

            for (int i = startRange; i <= endRange; i++)
            {
                string ip = $"{subnet}.{i}";
                tasks[i - startRange] = ScanHostAsync(ip, port);
            }

            await Task.WhenAll(tasks);
        }
        static async Task b()
        {
            string subnet = "192.168.0"; // 네트워크의 서브넷을 설정하세요
            int startRange = 1; // 스캔을 시작할 IP 주소 범위의 시작 번호를 설정하세요
            int endRange = 255; // 스캔을 시작할 IP 주소 범위의 끝 번호를 설정하세요
            int port = 80; // 스캔할 포트 번호를 설정하세요

            var task = new Task[endRange - startRange + 1];
            Parallel.For(startRange, endRange + 1, i =>
            {
                string ip = $"{subnet}.{i}";
                task[i - startRange] = ScanHost(ip, port);
            });
            await Task.WhenAll(task);
        }

        static async Task ScanHost(string ip, int port)
        {
            if (await IsHostAlive(ip))
            {
                Console.WriteLine($"Host {ip} is alive.");

                //if (IsPortOpen(ip, port).Result)
                //{
                //    Console.WriteLine($"Port {port} is open on {ip}.");
                //}
                //else
                //{
                //    Console.WriteLine($"Port {port} is closed on {ip}.");
                //}
            }
        }

        static async Task ScanHostAsync(string ip, int port)
        {
            //if (await IsHostAlive(ip))
            //{
            Ping ping = new Ping();
            try
            {
                PingReply reply = await ping.SendPingAsync(ip, 1000); // 1000 milliseconds timeout
                if(reply.Status == IPStatus.Success)
                {
                    Console.WriteLine($"Host {ip} is alive.");
                }
            }
            catch
            {
                Console.WriteLine($"Host {ip} is dead.");
            }

           

                //if (await IsPortOpen(ip, port))
                //{
                //    Console.WriteLine($"Port {port} is open on {ip}.");
                //}
                //else
                //{
                //    Console.WriteLine($"Port {port} is closed on {ip}.");
                //}
            //}
        }

        static async Task<bool> IsHostAlive(string ip)
        {
            Ping ping = new Ping();
            try
            {
                PingReply reply = await ping.SendPingAsync(ip, 1000); // 1000 milliseconds timeout
                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        static async Task<bool> IsPortOpen(string ip, int port)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    await client.ConnectAsync(ip, port);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
