using System;
using System.Threading;

namespace LNM_TRA
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //라이센스
                License.Class1 License = new License.Class1();
                //00-E0-4C-62-22-EC
                //if (License.uuid("20244D56-F6BD-E22A-1D52-3669DF035654") == true)
                //{

                    //리눅스,보안,스위치장비 서비스
                    SecurityServer.Class1 Sercurity = new SecurityServer.Class1();
                ////AP장비
                //AP.Class1 APThread = new AP.Class1();

                ////보안장비
                Thread thread11 = new Thread(Sercurity.Securethread);
                thread11.Start();

                ///이제 안씀 리눅스 자체 클라이언트 만듬
                ////리눅스
                //Thread thread12 = new Thread(Sercurity.Linuxthread);
                //thread12.Start();

                //////AP장비
                //Thread thread17 = new Thread(APThread.APThread);
                //thread17.Start();
                //}
                //else
                //{
                //    Console.WriteLine("라이선스가 틀립니다");
                //}
            }
            catch (Exception e)
            {
                //Console.WriteLine("에러");

                //string sDirPath;
                //sDirPath = @"C:\NMS(maria)\debug";
                //DirectoryInfo di = new DirectoryInfo(sDirPath);
                //if (di.Exists == false)
                //{
                //    di.Create();
                //}

                //string path = @"C:\NMS(maria)\debug\error.txt"; // path to file
                //string txt = e.Message + "\n";
                //File.AppendAllText(path, txt);
            }

        }
    }
}
